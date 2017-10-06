using System;

namespace SmartDevice.SmartDeviceProtocol
{
	internal sealed class BlockSentEventArgs
		: EventArgs
	{
		internal BlockSentEventArgs(Block block)
		{
			Block = block;
		}

		public Block Block { get; }
	}
}