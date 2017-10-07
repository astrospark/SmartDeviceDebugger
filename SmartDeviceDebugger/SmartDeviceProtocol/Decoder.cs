using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataReceivedEventArgs = SmartDevice.AudioFrequencyShiftKeying.DataReceivedEventArgs;

namespace SmartDevice.SmartDeviceProtocol
{
	internal class Decoder
	{
		public Decoder(AudioFrequencyShiftKeying.Decoder afskDecoder)
		{
			afskDecoder.DataReceived += afsk_DataReceived;

			_state = State.Idle;
			_currentFrameBits = new List<byte>();
			_currentBlockData = new List<byte>();
			_spacerCount = 0;
		}

		public EventHandler<BlockReceivedEventArgs> BlockReceived;

		private void afsk_DataReceived(object sender, DataReceivedEventArgs e)
		{
			foreach (var bit in e.Data)
			{
				DecodeBit(bit);
			}
		}

		private void DecodeBit(byte bit)
		{
			if (bit < 2)
			{
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch (_state)
				{
					case State.Idle:
						if (bit == 0)
						{
							_currentFrameBits.Add(bit);
							_state = State.Data;
						}
						break;
					case State.Data:
						_currentFrameBits.Add(bit);
						if (_currentFrameBits.Count >= 9) _state = State.FrameStop;
						break;
					case State.FrameStop:
						if (bit == 1) // End of frame data
						{
							_currentFrameBits.Add(bit);
							var data =
								_currentFrameBits[1] |
								_currentFrameBits[2] << 1 |
								_currentFrameBits[3] << 2 |
								_currentFrameBits[4] << 3 |
								_currentFrameBits[5] << 4 |
								_currentFrameBits[6] << 5 |
								_currentFrameBits[7] << 6 |
								_currentFrameBits[8] << 7;
							_currentBlockData.Add((byte) data);
							_currentFrameBits.Clear();
							_state = State.WaitingForBlockEnd;
						}
						else
						{
							ResetDecoderState();
							//Debug.WriteLine($"Framing error: unexpected stop bit. {bit}");
						}
						break;
					case State.WaitingForBlockEnd:
						if (bit == 1) // Spacer bit between frames or blocks
						{
							if (_spacerCount < 14)
							{
								_spacerCount++;
							}
							else
							{
								BlockReceived(this, new BlockReceivedEventArgs(Block.FromBytes(_currentBlockData)));
								_currentBlockData.Clear();
								_spacerCount = 0;
								_state = State.Idle;
							}
						}
						else // Start bit of a new frame
						{
							if (_spacerCount < 10)
							{
								_currentFrameBits.Add(bit);
								_spacerCount = 0;
								_state = State.Data;
							}
							else
							{
								ResetDecoderState();
								//Debug.WriteLine("Framing error: unexpected start of frame.");
							}
						}
						break;
				}
			}
			else
			{
				ResetDecoderState();
			}
		}

		private void ResetDecoderState()
		{
			_currentFrameBits.Clear();
			_currentBlockData.Clear();
			_spacerCount = 0;
			_state = State.Idle;
		}

		private State _state;
		private readonly List<byte> _currentFrameBits;
		private readonly List<byte> _currentBlockData;
		private int _spacerCount;

		private enum State
		{
			Idle,
			Data,
			FrameStop,
			WaitingForBlockEnd,
		}
	}
}
