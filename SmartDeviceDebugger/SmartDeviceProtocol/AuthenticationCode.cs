namespace SmartDevice.SmartDeviceProtocol
{
	internal static class AuthenticationCode
	{
		public static int GenerateResponse(int challenge)
		{
			var shifted = challenge << 1;
			shifted |= (challenge & 0x8) >> 3 ^
			           (challenge & 0x1000) >> 12 ^
			           (challenge & 0x4000) >> 14 ^
			           (challenge & 0x8000) >> 15;
			return (shifted & 0xf) << 12 |
			       (shifted & 0xf0) << 4 |
			       (shifted & 0xf00) >> 4 |
			       (shifted & 0xf000) >> 12;
		}
	}
}
