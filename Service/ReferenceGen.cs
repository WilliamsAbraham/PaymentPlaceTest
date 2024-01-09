namespace PaymentPlaceTest.Service
{
	public static class ReferenceGen
	{
		static Random random = new Random();

		public static string GenerateRandomString()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Characters to use in the random string
			return new string(Enumerable.Repeat(chars, 6)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
