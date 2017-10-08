﻿using System.Collections.Generic;

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
				[0x07] = "TOOK-ATTACK",
				[0x08] = "SAW-KEYS",
				[0x09] = "SAW-DOMESIG",
				[0x0A] = "SAW-BARRELSIG",
				[0x0B] = "SAW-DOMEPACKET",
				[0x0C] = "SAW-BARRELPACKET",
				[0x0D] = "SENT-DOMESIG",
				[0x0E] = "SENT-BARRELSIG",
				[0x0F] = "SENT-DOMEPACKET",
				[0x10] = "SENT-BARRELPACKET",
				[0x11] = "PLAYING-SOUND",
				[0x18] = "COUNT-DOWN",
				[0x19] = "MODE-CHANGED",
				[0x20] = "VARIABLE-CONTENTS",
				[0x21] = "USER-CONTENTS",
				[0x22] = "GAME-CONTENTS",
				[0x23] = "SECONDARY-CONTENTS",
				[0x24] = "SPECIAL-CONTENTS",
				[0x30] = "DEBRIEF-DATA",
				[0x31] = "GROUP1-DATA",
				[0x32] = "GROUP2-DATA",
				[0x33] = "GROUP3-DATA",
				[0x3A] = "ERRORS-CONTENTS",
				[0x3B] = "VOLUME-CONTENTS",
				[0x3C] = "DATARATE-CONTENTS",
				[0x3D] = "CODE-VERSION",
				[0x3E] = "TEST-RESULTS",
				[0x3F] = "LOCATION-CONTENTS",
				[0x40] = "GAME-MODE",
				[0x41] = "AUTO-PROCESSES",
				[0x42] = "AUTO-REPORTS",
				[0x43] = "WEAPON-MODE",
				[0x4E] = "ECHO-REPLY",
				[0x4F] = "CHALLENGE-CODE",
				[0x50] = "COMMAND-RESULT",
				[0x86] = "TAKE-HIT",
				[0x87] = "TAKE-ATTACK",
				[0x88] = "INJECT-KEYS",
				[0x89] = "INJECT-DOMESIG",
				[0x8A] = "INJECT-BARRELSIG",
				[0x8B] = "INJECT-DOMEPACKET",
				[0x8C] = "INJECT-BARRELPACKET",
				[0x8D] = "SEND-DOMESIG",
				[0x8E] = "SEND-BARRELSIG",
				[0x8F] = "SEND-DOMEPACKET",
				[0x90] = "SEND-BARRELPACKET",
				[0x91] = "PLAY-SOUND",
				[0x92] = "FLASH-DOME",
				[0x93] = "FLASH-FIRE",
				[0x94] = "RAISE-SHIELD",
				[0x95] = "LOWER-SHIELD",
				[0x96] = "LOAD-AMMO",
				[0x97] = "FIRE-TAG",
				[0x98] = "NEUTRALIZE-PLAYER",
				[0x99] = "UNNEUTRALIZE-PLAYER",
				[0x9E] = "DOME-DEBRIEF",
				[0x9F] = "BARREL-DEBRIEF",
				[0xA0] = "READ-VARIABLE",
				[0xA1] = "READ-USER",
				[0xA2] = "READ-GAME",
				[0xA3] = "READ-SECONDARY",
				[0xA4] = "READ-SPECIAL",
				[0xB0] = "REPORT-DEBRIEF",
				[0xBA] = "READ-ERRORS",
				[0xBB] = "READ-VOLUME",
				[0xBC] = "READ-DATARATE",
				[0xBD] = "READ-VERSION",
				[0xBE] = "SELF-TEST",
				[0xBF] = "READ-LOCATION",
				[0xC0] = "WRITE-VARIABLE",
				[0xC1] = "WRITE-USER",
				[0xC2] = "WRITE-GAME",
				[0xC3] = "WRITE-SECONDARY",
				[0xC4] = "WRITE-SPECIAL",
				[0xC5] = "ADJUST-VARIABLE",
				[0xC8] = "ENABLE-WRITE",
				[0xDA] = "CLEAR-ERRORS",
				[0xDB] = "WRITE-VOLUME",
				[0xDC] = "WRITE-DATARATE",
				[0xDF] = "WRITE-LOCATION",
				[0xE0] = "CHANGE-MODE",
				[0xE1] = "ENABLE-PROCESSES",
				[0xE2] = "ENABLE-REPORTS",
				[0xE3] = "SELECT-WEAPON",
				[0xEE] = "ECHO-REQUEST",
				[0xEF] = "CHALLENGE-RESPONSE",
				[0xF0] = "READ-MODE",
				[0xF1] = "READ-PROCESSES",
				[0xF2] = "READ-REPORTS",
				[0xF3] = "READ-WEAPON"
			};

			return names.ContainsKey(blockType)
				? names[blockType]
				: null;
		}
	}
}