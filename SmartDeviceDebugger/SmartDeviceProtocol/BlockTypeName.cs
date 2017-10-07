using System.Collections.Generic;

namespace SmartDevice.SmartDeviceProtocol
{
	internal static class BlockTypeName
	{
		public static string Get(byte blockType)
		{
			var names = new Dictionary<byte, string>
			{
				[0x01] = "PRIORITY-UPDATE",
				[0x02] = "TAGGER-STATUS",
				[0x04] = "PROXIMITY-DETECT",
				[0x05] = "LOCKED-ON",
				[0x06] = "TOOK-HIT",
				[0x08] = "SAW-KEYS",
				[0x09] = "SAW-DOMESIG",
				[0x0A] = "SAW-BARRELSIG",
				[0x0B] = "SAW-DOMEPACKET",
				[0x0C] = "SAW-BARRELPACKET",
				[0x18] = "COUNT-DOWN",
				[0x19] = "MODE-CHANGED",
				[0x20] = "VARIABLE-CONTENTS",
				[0x21] = "21-CONTENTS",
				[0x22] = "GAME-CONTENTS",
				[0x23] = "23-CONTENTS",
				[0x24] = "24-CONTENTS",
				[0x3B] = "3B-CONTENTS",
				[0x40] = "40-CONTENTS",
				[0x41] = "41-CONTENTS",
				[0x42] = "42-CONTENTS",
				[0x43] = "43-CONTENTS",
				[0x4F] = "CHALLENGE-CODE",
				[0x50] = "COMMAND-RESULT",
				[0x8D] = "SEND-DOMESIG",
				[0x8E] = "SEND-BARRELSIG",
				[0x8F] = "SEND-DOMEPACKET",
				[0x90] = "SEND-BARRELPACKET",
				[0xA0] = "READ-VARIABLE",
				[0xA1] = "READ-21",
				[0xA2] = "READ-GAME",
				[0xA3] = "READ-23",
				[0xA4] = "READ-24",
				[0xBB] = "READ-3B",
				[0xC0] = "WRITE-VARIABLE",
				[0xC1] = "WRITE-21",
				[0xC2] = "WRITE-GAME",
				[0xC3] = "WRITE-23",
				[0xC4] = "WRITE-24",
				[0xDB] = "WRITE-3B",
				[0xE0] = "WRITE-40",
				[0xE1] = "WRITE-41",
				[0xE2] = "WRITE-42",
				[0xE3] = "WRITE-43",
				[0xEF] = "CHALLENGE-RESPONSE",
				[0xF0] = "READ-40",
				[0xF1] = "READ-41",
				[0xF2] = "READ-42",
				[0xF3] = "READ-43",
			};

			return names.ContainsKey(blockType)
				? names[blockType]
				: null;
		}
	}
}