using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SmartDevice.SmartDeviceProtocol;
using Decoder = SmartDevice.SmartDeviceProtocol.Decoder;
using Encoder = SmartDevice.SmartDeviceProtocol.Encoder;

namespace SmartDevice
{
	internal partial class MemoryForm : Form
	{
		public MemoryForm()
		{
			InitializeComponent();

			_timer = new Timer
			{
				Interval = 1000
			};
			_timer.Tick += timer_Tick;
		}

		public MemoryForm(Encoder encoder, Decoder decoder)
			: this()
		{
			_encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
			var decoder1 = decoder ?? throw new ArgumentNullException(nameof(decoder));
			decoder1.BlockReceived += decoder_BlockReceived;
		}

		private void getAllButton_Click(object sender, EventArgs e)
		{
			if (_getActive) return;
			GetStart();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			GetStop();
		}

		private void decoder_BlockReceived(object sender, BlockReceivedEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new EventHandler<BlockReceivedEventArgs>(decoder_BlockReceived), sender, e);
				return;
			}

			var block = e.Block;
			if (block.BlockType != 0x3F || // LOCATION-CONTENTS
			    block.Data.Count < 8 ||
			    !block.ChecksumValid) return;

			UpdateMemory(_getBank, _getLocation, block.Data);

			GetNext();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			Get();
		}

		private void GetStart()
		{
			if (_getActive) return;

			memoryTextBox.Clear();

			_getActive = true;
			_getBank = 0;
			_getLocation = 0;
			Get();

			progressBar.Visible = true;
			cancelButton.Visible = true;
		}

		private void GetStop()
		{
			_getActive = false;
			_timer.Stop();
			progressBar.Visible = false;
			cancelButton.Visible = false;
		}

		private void Get()
		{
			if (!_getActive) return;

			var readLocationBlock = new Block(0xBF); // READ-VARIABLE
			readLocationBlock.Data.Add(8); // Read 8 bytes
			readLocationBlock.Data.Add(_getBank);
			readLocationBlock.Data.Add(_getLocation);
			_encoder.Send(readLocationBlock);

			_timer.Stop(); // Reset
			_timer.Start();
		}

		private void GetNext()
		{
			if (!_getActive) return;

			if ((_getBank == 0 && _getLocation + 8 > 0xFF) ||
			    (_getBank == 2 && _getLocation + 8 > 0xCF))
			{
				if (_getBank == 0)
				{
					_getBank = 2;
					_getLocation = 0;
				}
				else
				{
					GetStop();
				}
			}
			else
			{
				_getLocation += 8;
			}

			Get();
			UpdateProgress();
		}

		private void UpdateMemory(int bank, int location, List<byte> data)
		{
			var stringBuilder = new StringBuilder(memoryTextBox.Text);

			if (_getBank == 2 && _getLocation == 0) stringBuilder.AppendLine();

			if (_getLocation % 0x10 == 0)
			{
				stringBuilder.Append($"{bank:X1}{location:X2} ");
			}
			else
			{
				stringBuilder.Append(" ");
			}

			foreach (var value in data)
			{
				stringBuilder.Append($" {value:X2}");
			}

			if (_getLocation % 0x10 != 0) stringBuilder.AppendLine();

			memoryTextBox.Text = stringBuilder.ToString();
		}

		private void UpdateProgress()
		{
			progressBar.Maximum = 0x1D0;
			var value = (_getBank == 2 ? 0x100 : 0) + _getLocation - 1;
			if (value < 0) value = 0;
			progressBar.Value = value;
		}

		private readonly Timer _timer;
		private readonly Encoder _encoder;
		private bool _getActive;
		private byte _getBank;
		private byte _getLocation;
	}
}
