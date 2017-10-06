using System.Collections.Generic;

namespace SmartDevice.SmartDeviceProtocol
{
	internal class Encoder
	{
		public Encoder(AudioFrequencyShiftKeying.Encoder afskEncoder)
		{
			_afskEncoder = afskEncoder;
		}

		public void Send(Block block)
		{

			SendFrame(block.BlockType);

			foreach (var data in block.Data)
			{
				SendFrame(data);
			}

			block.PopulateChecksum();
			SendFrame(block.Checksum);

			SendSpacers(20);
		}

		private void SendFrame(byte value)
		{
			var frame = new List<byte> {0}; // start bit

			for (var bit = 0; bit < 8; bit++) // data bits
			{
				frame.Add((byte) ((value >> bit) & 0x1));
			}

			frame.Add(1); // stop bit
			frame.Add(1); // spacer

			_afskEncoder.Send(frame);
		}

		private void SendSpacers(int count)
		{
			var spacers = new byte[count];
			for (var i = 0; i < count; i++)
			{
				spacers[i] = 1;
			}
			_afskEncoder.Send(spacers);
		}

		private readonly AudioFrequencyShiftKeying.Encoder _afskEncoder;
	}
}
