using System;
using System.Diagnostics;
using System.Windows.Forms;
using SmartDevice.SmartDeviceProtocol;
using Decoder = SmartDevice.SmartDeviceProtocol.Decoder;
using Encoder = SmartDevice.SmartDeviceProtocol.Encoder;

namespace SmartDevice
{
	internal class EnableWrite
	{
		public EnableWrite(Encoder encoder, Decoder decoder)
		{
			_encoder = encoder;
			var decoder1 = decoder;
			decoder1.BlockReceived += decoder_BlockReceived;

			_timer = new System.Timers.Timer
			{
				AutoReset = false,
				Interval = 1000
			};
			_timer.Elapsed += timer_Elapsed;
		}

		public bool Active { get; private set; }

		public void Start()
		{
			if (Active) return;

			var enableReportsBlock = new Block(0xE2); // ENABLE-REPORTS
			enableReportsBlock.Data.Add(0x00);
			enableReportsBlock.Data.Add(0x00);
			enableReportsBlock.Data.Add(0x00);
			enableReportsBlock.Data.Add(0x0C);
			enableReportsBlock.Data.Add(0x78);
			enableReportsBlock.Data.Add(0x7C);
			enableReportsBlock.Data.Add(0x54);
			enableReportsBlock.Data.Add(0x2B);
			enableReportsBlock.Data.Add(0x11);
			_encoder.Send(enableReportsBlock);

			var writeVariableBlock = new Block(0xC0); // WRITE-VARIABLE
			enableReportsBlock.Data.Add(0x11);
			enableReportsBlock.Data.Add(0x61);
			_encoder.Send(writeVariableBlock);

			writeVariableBlock = new Block(0xC0); // WRITE-VARIABLE
			enableReportsBlock.Data.Add(0x13);
			enableReportsBlock.Data.Add(0x00);
			_encoder.Send(writeVariableBlock);

			Active = true;
			_getValue = 0;
			_gotEnableResult = false;
			Get();
		}

		public void Stop()
		{
			Active = false;
			_timer.Stop();
		}

		private void decoder_BlockReceived(object sender, BlockReceivedEventArgs e)
		{
			var block = e.Block;

			if (!block.ChecksumValid) return;

			if (!_gotEnableResult &&
				block.BlockType == 0x50 && // COMMAND-RESULT
				block.Data.Count >= 2 &&
				block.Data[0] == 0xC8) // ENABLE-WRITE
			{
				_gotEnableResult = true;
				Get();
			}

			if (_gotEnableResult &&
			    block.BlockType == 0x50 && // COMMAND-RESULT
			    block.Data.Count >= 2 &&
			    block.Data[0] == 0xDF) // WRITE-LOCATION
			{
				GetNext();
			}

			if (block.BlockType == 0x3F) // LOCATION-CONTENTS
			{
				Debug.WriteLine($@"0x{_getValue:X4}");
				MessageBox.Show($@"0x{_getValue:X4}");
				Stop();
			}
		}

		private void timer_Elapsed(object sender, EventArgs e)
		{
			Get();
		}

		private void Get()
		{
			if (!Active) return;

			if (!_gotEnableResult)
			{
				SendEnableWrite();
			}
			else
			{
				SendWriteLocation();
			}
		}

		private void SendWriteLocation()
		{
			var writeLocationBlock = new Block(0xDF); // WRITE-LOCATION
			writeLocationBlock.Data.Add(1); // 1 byte
			writeLocationBlock.Data.Add(2); // bank 2
			writeLocationBlock.Data.Add(0x0F); // location 0x0F
			writeLocationBlock.Data.Add(0x00); // value 0x00
			_encoder.Send(writeLocationBlock);

			_timer.Stop();
			_timer.Start();
		}

		private void SendEnableWrite()
		{
			_gotEnableResult = false;

			var enableWriteBlock = new Block(0xC8); // ENABLE-WRITE
			enableWriteBlock.Data.Add((byte) (_getValue >> 8));
			enableWriteBlock.Data.Add((byte) _getValue);
			_encoder.Send(enableWriteBlock);

			_timer.Stop(); // Reset
			_timer.Start();
		}

		private void GetNext()
		{
			if (!Active) return;

			if (_getValue + 1 > 0xFFFF) Stop();
			_getValue++;

			SendEnableWrite();
		}

		private readonly System.Timers.Timer _timer;
		private readonly Encoder _encoder;
		private short _getValue;
		private bool _gotEnableResult;
	}
}
