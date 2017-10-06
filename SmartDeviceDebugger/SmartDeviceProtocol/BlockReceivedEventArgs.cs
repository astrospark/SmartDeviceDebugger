using System;

namespace SmartDevice.SmartDeviceProtocol
{
	internal sealed class BlockReceivedEventArgs
		: EventArgs
	{
		internal BlockReceivedEventArgs(Block block)
		{
			Block = block;
		}

		public Block Block { get; }
	}
}
