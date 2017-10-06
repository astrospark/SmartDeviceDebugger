using System.Collections.Generic;

namespace SmartDevice.SmartDeviceProtocol
{
	internal class VariableName
	{
		public static string Get(byte blockType)
		{
			var names = new Dictionary<byte, string>
			{
				[0x00] = "User0",
				[0x01] = "User1",
				[0x02] = "User2",
				[0x03] = "User3",
				[0x04] = "User4",
				[0x05] = "User5",
				[0x06] = "User6",
				[0x07] = "User7",
				[0x08] = "HostID",
				[0x09] = "Minutes",
				[0x0A] = "Health",
				[0x0B] = "Reload Ammo (Low byte)",
				[0x0C] = "Reload Ammo (High byte)",
				[0x0D] = "Shields",
				[0x0E] = "Megas",
				[0x0F] = "GameFlags1",
				[0x10] = "GameFlags2",
				[0x11] = "GameFlags3",
				[0x12] = "Loaded Ammo",
				[0x13] = "DeadMan Minutes",
				[0x14] = "Player ID",
				[0x16] = "Group1 Players",
				[0x17] = "Group2 Players",
				[0x18] = "Group3 Players",
				[0x19] = "Team Number",
				[0x1A] = "Player Number",
				[0x20] = "Game Time Minutes",
				[0x21] = "Game Time Seconds",
			};

			return names.ContainsKey(blockType)
				? names[blockType]
				: null;
		}
	}
}
