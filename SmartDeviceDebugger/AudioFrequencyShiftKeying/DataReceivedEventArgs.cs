using System;
using System.Collections.Generic;

namespace SmartDevice.AudioFrequencyShiftKeying
{
	internal sealed class DataReceivedEventArgs
		: EventArgs
	{
		internal DataReceivedEventArgs(IEnumerable<byte> data)
		{
			Data = data;
		}

		public IEnumerable<byte> Data { get; }
	}
}
