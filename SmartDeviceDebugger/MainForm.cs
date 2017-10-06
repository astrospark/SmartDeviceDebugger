using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using NAudio.CoreAudioApi;
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

			// Input
			_afskDecoder = new AudioFrequencyShiftKeying.Decoder();
			_smartDeviceProtocolDecoder = new SmartDeviceProtocol.Decoder(_afskDecoder);
			_smartDeviceProtocolDecoder.BlockReceived += smartDeviceProtocolDecoder_BlockReceived;

			// Output
			_afskEncoder = new AudioFrequencyShiftKeying.Encoder();
			_smartDeviceProtocolEncoder = new SmartDeviceProtocol.Encoder(_afskEncoder);
			_smartDeviceProtocolEncoder.BlockSent += smartDeviceProtocolEncoder_BlockSent;
		}

		private void mainForm_Load(object sender, EventArgs e)
		{
			PopulateInputDevices();
			PopulateOutputDevices();
		}

		private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Stop();

			Registry.SetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Input Device ID", inputDeviceComboBox.SelectedValue, RegistryValueKind.String);
			Registry.SetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Output Device ID", outputDeviceComboBox.SelectedValue, RegistryValueKind.String);
		}

		private void PopulateInputDevices()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			inputDeviceComboBox.DataSource = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
			inputDeviceComboBox.ValueMember = nameof(MMDevice.ID);
			inputDeviceComboBox.DisplayMember = nameof(MMDevice.FriendlyName);

			string defaultDeviceID = null;
			try
			{
				defaultDeviceID = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications).ID;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			var deviceID = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Input Device ID", defaultDeviceID) as string;
			if (deviceID != null) inputDeviceComboBox.SelectedValue = deviceID;
		}

		private void PopulateOutputDevices()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			outputDeviceComboBox.DataSource = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
			outputDeviceComboBox.ValueMember = nameof(MMDevice.ID);
			outputDeviceComboBox.DisplayMember = nameof(MMDevice.FriendlyName);

			string defaultDeviceID = null;
			try
			{
				defaultDeviceID = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Communications).ID;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
			var deviceID = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Astrospark Technologies\Smart Device Debugger", @"Output Device ID", defaultDeviceID) as string;
			if (deviceID != null) outputDeviceComboBox.SelectedValue = deviceID;
		}

		private void startStopButton_Click(object sender, EventArgs e)
		{
			if (!_started)
			{
				_started = true;
				// ReSharper disable once LocalizableElement
				startStopButton.Text = "&Stop";
				inputDeviceComboBox.Enabled = false;
				outputDeviceComboBox.Enabled = false;
				variablesButton.Enabled = true;
				Start();
			}
			else
			{
				_started = false;
				// ReSharper disable once LocalizableElement
				startStopButton.Text = "&Start";
				inputDeviceComboBox.Enabled = true;
				outputDeviceComboBox.Enabled = true;
				variablesButton.Enabled = false;
				Stop();
			}
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

			_blocks.Add(e.Block);
			ProcessBlock(e.Block);
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
			includeTextBox.Text = SanitizeFilter(includeTextBox.Text);
			excludeTextBox.Text = SanitizeFilter(excludeTextBox.Text);
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
			_afskEncoder.Start(outputDeviceComboBox.SelectedValue as string, 0.5f);
			_afskDecoder.Start(inputDeviceComboBox.SelectedValue as string, 1.0f);
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

			var includeFilter = ParseFilter(includeTextBox.Text);
			var excludeFilter = ParseFilter(excludeTextBox.Text);

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

		private void variablesButton_Click(object sender, EventArgs e)
		{
			var variablesForm = new VariablesForm(_smartDeviceProtocolEncoder,_smartDeviceProtocolDecoder);
			variablesForm.Show(this);
		}

		private static string SanitizeFilter(string filter)
		{
			var rawParts = filter.Split(new[] { ' ', ',', '.', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

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

				if (filterTypes.Contains(value)) continue;

				filterParts.Add($"{value:X2}");
				filterTypes.Add(value);
			}

			return string.Join(" ", filterParts);
		}

		private static List<byte> ParseFilter(string filter)
		{
			if (string.IsNullOrWhiteSpace(filter)) return new List<byte>();

			var filterTypes = new List<byte>();
			foreach (var hexValue in SanitizeFilter(filter).Split(' '))
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

		private readonly List<Block> _blocks;
		private bool _started;

		// Input
		private readonly AudioFrequencyShiftKeying.Decoder _afskDecoder;
		private readonly SmartDeviceProtocol.Decoder _smartDeviceProtocolDecoder;

		// Output
		private readonly AudioFrequencyShiftKeying.Encoder _afskEncoder;
		private readonly SmartDeviceProtocol.Encoder _smartDeviceProtocolEncoder;
	}
}
