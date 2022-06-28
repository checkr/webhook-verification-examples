using System;
using System.Text;
using System.Security.Cryptography;

public static class Program
{
	public static void Main(string[] args)
	{
		// Warning! JSON must be compact. Pretty JSON with spaces will not work.
		string compactJsonPayload = "{\"monday\":\"75F\",\"tuesday\":\"80F\"}";
		string payloadSignature = "b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151";
		string secret = "test-secret";
		bool result = Program.Verify(secret, compactJsonPayload, payloadSignature);
		Console.WriteLine(result); // should print True
	}

	public static bool Verify(string secret, string compactJsonPayload, string signature)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(secret);
		HMAC hmac = new HMACSHA256(bytes);
		bytes = Encoding.UTF8.GetBytes(compactJsonPayload);
		string computedHash = Convert.ToHexString(hmac.ComputeHash(bytes));
		
		ReadOnlySpan<byte> computedSignatureBytes = Convert.FromHexString(computedHash);
		ReadOnlySpan<byte> signatureBytes = Convert.FromHexString(signature);

		return CryptographicOperations.FixedTimeEquals(computedSignatureBytes, signatureBytes);
	}
}