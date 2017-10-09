using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CSCore.CoreAudioAPI;
using Microsoft.Win32;
using SmartDevice.SmartDeviceProtocol;

namespace SmartDevice
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			Registry.CurrentUser.CreateSubKey(@"Software\Astrospark Technologies\Smart Device Debugger");

			_blocks = new List<Block>();

			var keepAliveTimer = new Timer()
			{
				Interval = 30000
			};
			keepAliveTimer.Tick += keepAliveTimer_Tick;
			keepAliveTimer.Start();

			// Input
			_afskDecoder = new AudioFrequencyShiftKeying.Decoder();
			_smartDeviceProtocolDecoder = new SmartDeviceProtocol.Decoder(_afskDecoder);
			_smartDeviceProtocolDecoder.BlockReceived += smartDeviceProtocolDecoder_BlockReceived;

			// Output
			_afskEncoder = new AudioFrequencyShiftKeying.Encoder();
			_smartDeviceProtocolEncoder = new SmartDeviceProtocol.Encoder(_afskEncoder);
			_smartDeviceProtocolEncoder.BlockSent += smartDeviceProtocolEncoder_BlockSent;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			GetInputDevice();
			GetOutputDevice();
		}

		private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Stop();
		}

		private void GetInputDevice()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			string defaultDeviceID = null;
			try
			{
				defaultDeviceID = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications).DeviceID;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			_inputDeviceID = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Input Device ID", defaultDeviceID) as string;
		}

		private void GetOutputDevice()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			string defaultDeviceID = null;
			try
			{
				defaultDeviceID = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Communications).DeviceID;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			_outputDeviceID = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Output Device ID", defaultDeviceID) as string;

			var phaseInvertString = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Phase Invert", "true") as string;
			_afskEncoder.PhaseInvert = phaseInvertString != null && phaseInvertString != "false";
		}

		private void startStopButton_Click(object sender, EventArgs e)
		{
			if (!_started)
			{
				_started = true;
				// ReSharper disable once LocalizableElement
				startStopButton.Text = "&Stop";
				optionsButton.Enabled = false;
				keepAliveCheckBox.Enabled = true;
				memoryButton.Enabled = true;
				variablesButton.Enabled = true;
				commandTextBox.Enabled = true;
				sendButton.Enabled = true;
				Start();
			}
			else
			{
				_started = false;
				// ReSharper disable once LocalizableElement
				startStopButton.Text = "&Start";
				optionsButton.Enabled = true;
				keepAliveCheckBox.Enabled = false;
				memoryButton.Enabled = false;
				variablesButton.Enabled = false;
				commandTextBox.Enabled = false;
				sendButton.Enabled = false;
				Stop();
			}
		}

		private void optionsButton_Click(object sender, EventArgs e)
		{
			var optionsForm = new OptionsForm
			{
				SelectedInputDeviceID = _inputDeviceID,
				SelectedOutputDeviceID = _outputDeviceID,
				PhaseInvert = _afskEncoder.PhaseInvert
			};

			if (optionsForm.ShowDialog(this) != DialogResult.OK) return;

			_inputDeviceID = optionsForm.SelectedInputDeviceID;
			_outputDeviceID = optionsForm.SelectedOutputDeviceID;
			_afskEncoder.PhaseInvert = optionsForm.PhaseInvert;

			Registry.SetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Input Device ID", _inputDeviceID, RegistryValueKind.String);
			Registry.SetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Output Device ID", _outputDeviceID, RegistryValueKind.String);
			Registry.SetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Phase Invert", _afskEncoder.PhaseInvert ? "true" : "false", RegistryValueKind.String);
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			_blocks.Clear();
			blocksListView.Items.Clear();
		}

		private void smartDeviceProtocolEncoder_BlockSent(object sender, BlockSentEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new EventHandler<BlockSentEventArgs>(smartDeviceProtocolEncoder_BlockSent), sender, e);
				return;
			}

			_blocks.Add(e.Block);
			ApplyFilters();
		}

		private void smartDeviceProtocolDecoder_BlockReceived(object sender, BlockReceivedEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new EventHandler<BlockReceivedEventArgs>(smartDeviceProtocolDecoder_BlockReceived), sender, e);
				return;
			}

			var block = e.Block;

			_blocks.Add(block);
			ProcessBlock(block);
			ApplyFilters();
		}

		private void AddBlock(Block block)
		{
			var blockType = $"{block.BlockType:X2}";
			var name = BlockTypeName.Get(block.BlockType);
			if (name != null) blockType = $"{name} ({blockType})";

			var item = blocksListView.Items.Add(blockType);
			item.Tag = block;

			var data = new List<string>();
			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var value in block.Data)
			{
				data.Add($"{value:X2}");
			}
			item.SubItems.Add(string.Join(" ", data));

			var checksumFlag = block.ChecksumValid ? "" : "!";
			item.SubItems.Add($"{block.Checksum:X2}{checksumFlag}");
		}

		private void filterTextBox_Leave(object sender, EventArgs e)
		{
			includeTextBox.Text = SanitizeHex(includeTextBox.Text, true);
			excludeTextBox.Text = SanitizeHex(excludeTextBox.Text, true);
			ApplyFilters();
		}

		private void blocksListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			var block = GetSelectedBlock();
			if (block == null)
			{
				detailsTextBox.Clear();
				return;
			}

			GetDetails(block);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var stringBuilder = new StringBuilder();

			foreach (var block in GetSelectedBlocks())
			{
				stringBuilder.AppendLine(block.ToString());
			}

			if (stringBuilder.Length > 0) Clipboard.SetText(stringBuilder.ToString());
		}

		private Block GetSelectedBlock()
		{
			ListViewItem item = null;
			if (blocksListView.SelectedItems.Count == 1) item = blocksListView.SelectedItems[0];
			return item?.Tag as Block;
		}

		private IEnumerable<Block> GetSelectedBlocks()
		{
			var blocks = new List<Block>();
			foreach (ListViewItem item in blocksListView.SelectedItems)
			{
				if (item.Tag is Block block) blocks.Add(block);
			}
			return blocks;
		}

		private void GetDetails(Block block)
		{
			var stringBuilder = new StringBuilder();

			var name = BlockTypeName.Get(block.BlockType) ?? "UNKNOWN";
			stringBuilder.AppendLine($"BType\t0x{block.BlockType:X2}\t{name}");

			var bitNumber = 0;
			foreach (var value in block.Data)
			{
				var binary = Convert.ToString(value, 2);
				binary = binary.PadLeft(8, '0');
				stringBuilder.AppendLine($"BData{bitNumber}\t0x{value:X2}\t{value:D}\t{binary}");
				bitNumber++;
			}

			var valid = block.ChecksumValid ? "Valid" : "Invalid";
			stringBuilder.AppendLine($"BSum\t0x{block.Checksum:X2}\t{valid}");

			detailsTextBox.Text = stringBuilder.ToString();
		}

		private void Start()
		{
			_afskEncoder.Start(_outputDeviceID, 1.0f);
			_afskDecoder.Start(_inputDeviceID, 1.0f);
		}

		private void Stop()
		{
			_afskEncoder.Stop();
			_afskDecoder.Stop();
		}

		private void ApplyFilters()
		{
			blocksListView.BeginUpdate();
			blocksListView.Items.Clear();

			var includeFilter = ParseHex(includeTextBox.Text);
			var excludeFilter = ParseHex(excludeTextBox.Text);

			foreach (var block in _blocks)
			{
				var blockType = block.BlockType;

				if ((includeFilter.Count == 0 ||
				     includeFilter.Contains(blockType)) &&
				    !excludeFilter.Contains(blockType))
				{
					AddBlock(block);
				}
			}

			if (autoScrollCheckBox.Checked && blocksListView.Items.Count > 0)
			{
				var index = blocksListView.Items.Count - 1;
				blocksListView.EnsureVisible(index);
				blocksListView.Items[index].Selected = true;
			}

			blocksListView.EndUpdate();
		}

		private void ProcessBlock(Block block)
		{
			switch (block.BlockType)
			{
				case 0x4F:
					if (block.Data.Count >= 2 && block.ChecksumValid)
					{
						var challenge = block.Data[0] << 8 | block.Data[1];
						var response = AuthenticationCode.GenerateResponse(challenge);
						var responseBlock = new Block(0xEF);
						responseBlock.Data.Add((byte) (response >> 8));
						responseBlock.Data.Add((byte) response);
						_smartDeviceProtocolEncoder.Send(responseBlock);
					}
					break;
			}
		}

		private void memoryButton_Click(object sender, EventArgs e)
		{
			if (_memoryForm != null && _memoryForm.Visible) return;
			_memoryForm = new MemoryForm(_smartDeviceProtocolEncoder, _smartDeviceProtocolDecoder);
			_memoryForm.Show(this);
		}

		private void variablesButton_Click(object sender, EventArgs e)
		{
			if (_variablesForm != null && _variablesForm.Visible) return;
			_variablesForm = new VariablesForm(_smartDeviceProtocolEncoder, _smartDeviceProtocolDecoder);
			_variablesForm.Show(this);
		}

		private static string SanitizeHex(string hex, bool unique = false)
		{
			var rawParts = hex.Split(new[] { ' ', ',', '.', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

			var filterParts = new List<string>();
			var filterTypes = new List<byte>();
			foreach (var part in rawParts)
			{
				var hexValue = Regex.Replace(part, @"0[xX]", string.Empty); // remove 0x
				hexValue = Regex.Replace(hexValue, @"[^0-9a-fA-F]", string.Empty); // remove non hex characters
				if (string.IsNullOrEmpty(hexValue)) continue;
				if (hexValue.Length > 2) hexValue = hexValue.Substring(hexValue.Length - 2, 2);

				byte value;
				try
				{
					value = (byte)Convert.ToInt32(hexValue, 16);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					continue;
				}

				if (unique && filterTypes.Contains(value)) continue;

				filterParts.Add($"{value:X2}");
				filterTypes.Add(value);
			}

			return string.Join(" ", filterParts);
		}

		private static List<byte> ParseHex(string hex)
		{
			if (string.IsNullOrWhiteSpace(hex)) return new List<byte>();

			var filterTypes = new List<byte>();
			foreach (var hexValue in SanitizeHex(hex).Split(' '))
			{
				try
				{
					filterTypes.Add((byte) Convert.ToInt32(hexValue, 16));
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
				}
			}
			return filterTypes;
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in blocksListView.Items)
			{
				item.Selected = true;
			}
		}

		private void includeBlockTypeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var count = 0;
			foreach (var block in GetSelectedBlocks())
			{
				// ReSharper disable once LocalizableElement
				includeTextBox.Text += $" {block.BlockType:X2}";
				count++;
			}
			if (count>0) ApplyFilters();
		}

		private void excludeBlockTypeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var count = 0;
			foreach (var block in GetSelectedBlocks())
			{
				// ReSharper disable once LocalizableElement
				excludeTextBox.Text += $" {block.BlockType:X2}";
				count++;
			}
			if (count > 0) ApplyFilters();
		}

		private void commandTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) Keys.Return)
			{
				SendCommand();
				e.Handled = true;
			}
		}

		private void commandTextBox_Leave(object sender, EventArgs e)
		{
			commandTextBox.Text = SanitizeHex(commandTextBox.Text);
		}

		private void sendButton_Click(object sender, EventArgs e)
		{
			SendCommand();
		}

		private void SendCommand()
		{
			var bytes = ParseHex(commandTextBox.Text);
			if (bytes == null || bytes.Count < 1) return;

			var block = new Block(bytes[0]);
			for (var index = 1; index < bytes.Count; index++)
			{
				block.Data.Add(bytes[index]);
			}
			_smartDeviceProtocolEncoder.Send(block);

			commandTextBox.SelectAll();
		}

		private void keepAliveTimer_Tick(object sender, EventArgs e)
		{
			if (!_started || !keepAliveCheckBox.Checked || _smartDeviceProtocolEncoder == null) return;
			var block = new Block(0xC0); // WRITE-VARIABLE
			block.Data.Add(0x22); // DeadMan Timer Minutes
			block.Data.Add(0x00);
			_smartDeviceProtocolEncoder.Send(block);
		}

		private readonly List<Block> _blocks;
		private bool _started;
		private MemoryForm _memoryForm;
		private VariablesForm _variablesForm;

		// Input
		private string _inputDeviceID;
		private readonly AudioFrequencyShiftKeying.Decoder _afskDecoder;
		private readonly SmartDeviceProtocol.Decoder _smartDeviceProtocolDecoder;

		// Output
		private string _outputDeviceID;
		private readonly AudioFrequencyShiftKeying.Encoder _afskEncoder;
		private readonly SmartDeviceProtocol.Encoder _smartDeviceProtocolEncoder;
	}
}
