using System.Collections.Generic;
using System.Linq;

namespace SmartDevice.SmartDeviceProtocol
{
	internal class Block
	{
		public Block()
		{
			Data = new List<byte>();
		}

		public Block(byte blockType)
			: this()
		{
			BlockType = blockType;
		}

		private Block(IEnumerable<byte> frames)
			: this()
		{
			var framesArray = frames.ToArray();
			if (framesArray.Length < 2) return;

			BlockType = framesArray[0];

			Data.Clear();
			for (var index = 1; index < framesArray.Length - 1; index++)
			{
				Data.Add(framesArray[index]);
			}

			Checksum = framesArray.Last();
		}

		public static Block FromBytes(IEnumerable<byte> frames)
		{
			return new Block(frames);
		}

		public byte BlockType { get; set; }
		public List<byte> Data { get; }
		public byte Checksum { get; set; }

		public bool ChecksumValid => Checksum == CalculateChecksum(this);

		public void PopulateChecksum()
		{
			Checksum = CalculateChecksum(this);
		}

		public override string ToString()
		{
			var parts = new List<string>
			{
				$"{BlockType:X2}"
			};

			var name = BlockTypeName.Get(BlockType);
			if (name != null) parts.Add(name);

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var value in Data)
			{
				parts.Add($"{value:X2}");
			}

			var checksumFlag = ChecksumValid ? "" : "!";
			parts.Add($"({Checksum:X2}{checksumFlag})");

			return string.Join(" ", parts);
		}

		private static byte CalculateChecksum(Block block)
		{
			var checksum = 0;
			checksum += block.BlockType;
			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var value in block.Data)
			{
				checksum += value;
			}

			return (byte) (checksum ^ 0xFF);
		}
	}
}
