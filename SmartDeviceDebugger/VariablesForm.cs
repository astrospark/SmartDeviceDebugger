using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SmartDevice.SmartDeviceProtocol;
using Decoder = SmartDevice.SmartDeviceProtocol.Decoder;
using Encoder = SmartDevice.SmartDeviceProtocol.Encoder;

namespace SmartDevice
{
	internal partial class VariablesForm : Form
	{
		public VariablesForm()
		{
			InitializeComponent();

			_getQueue = new Queue<byte>();

			_timer = new Timer
			{
				Interval = 500
			};
			_timer.Tick += timer_Tick;
		}

		public VariablesForm(Encoder encoder, Decoder decoder)
			: this()
		{
			_encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
			var decoder1 = decoder ?? throw new ArgumentNullException(nameof(decoder));
			decoder1.BlockReceived += decoder_BlockReceived;
		}

		private void VariablesForm_Load(object sender, EventArgs e)
		{
			variablesListView.Items.Clear();
			for (var variable = 0; variable <= 0x35; variable++)
			{
				var item = variablesListView.Items.Add($"0x{variable:X2}");
				item.SubItems.Add(VariableName.Get((byte) variable));
				item.SubItems.Add(string.Empty);
				item.SubItems.Add(string.Empty);
				item.SubItems.Add(string.Empty);
			}
		}

		private void getSelectedButton_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in variablesListView.SelectedItems)
			{
				GetVariable((byte) item.Index);
			}
		}

		private void getAllButton_Click(object sender, EventArgs e)
		{
			for (byte variable = 0; variable <= 0x35; variable++)
			{
				GetVariable(variable);
			}
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			GetStop();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var stringBuilder = new StringBuilder();
			var parts = new List<string>();
			foreach (ListViewItem item in variablesListView.SelectedItems)
			{
				// ReSharper disable once LoopCanBeConvertedToQuery
				foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
				{
					if (string.IsNullOrWhiteSpace(subItem.Text)) continue;
					parts.Add(subItem.Text);
				}
				if (parts.Count > 0) stringBuilder.AppendLine(string.Join(" ", parts));
				parts.Clear();
			}
			if (stringBuilder.Length > 0) Clipboard.SetText(stringBuilder.ToString());
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in variablesListView.Items)
			{
				item.Selected = true;
			}
		}

		private void decoder_BlockReceived(object sender, BlockReceivedEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new EventHandler<BlockReceivedEventArgs>(decoder_BlockReceived), sender, e);
				return;
			}

			var block = e.Block;
			if (block.BlockType != 0x20 || block.Data.Count < 2 || !block.ChecksumValid) return;

			var variable = block.Data[0];
			UpdateVariable(variable, block.Data[1]);

			if (variable == _getVariable) GetNext();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			Get();
		}

		private void GetVariable(byte variable)
		{
			if (_getQueue.Contains(variable)) return;
			_getQueue.Enqueue(variable);
			_getQueueStartCount++;
			GetStart();
		}

		private void GetStart()
		{
			if (_getActive) return;

			_getActive = true;
			GetNext();

			progressBar.Visible = true;
			cancelButton.Visible = true;
		}

		private void GetStop()
		{
			_getActive = false;
			_timer.Stop();
			progressBar.Visible = false;
			cancelButton.Visible = false;
			_getQueueStartCount = 0;
			_getQueue.Clear();
		}

		private void Get()
		{
			if (!_getActive) return;

			var readVariableBlock = new Block(0xA0); // READ-VARIABLE
			readVariableBlock.Data.Add(_getVariable);
			_encoder.Send(readVariableBlock);

			_timer.Stop(); // Reset
			_timer.Start();
		}

		private void GetNext()
		{
			if (!_getActive) return;
			if (_getQueue.Count < 1)
			{
				GetStop();
				return;
			}
			_getVariable = _getQueue.Dequeue();
			Get();
			UpdateProgress();
		}

		private void UpdateVariable(byte variable, byte value)
		{
			if (variablesListView.Items.Count < variable + 1) return;
			var item = variablesListView.Items[variable];
			if (item.SubItems.Count < 5) return;
			item.SubItems[2].Text = $@"0x{value:X2}";
			item.SubItems[3].Text = value.ToString();
			item.SubItems[4].Text = Convert.ToString(value, 2).PadLeft(8, '0');
		}

		private void UpdateProgress()
		{
			progressBar.Maximum = _getQueueStartCount;
			var value = _getQueueStartCount - _getQueue.Count - 1;
			if (value < 0) value = 0;
			progressBar.Value = value;
		}

		private readonly Queue<byte> _getQueue;
		private readonly Timer _timer;
		private readonly Encoder _encoder;
		private bool _getActive;
		private int _getQueueStartCount;
		private byte _getVariable;
	}
}
