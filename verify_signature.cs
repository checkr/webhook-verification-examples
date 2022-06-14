using System;
using System.Text;
using System.Security.Cryptography;

public static class Program
{
	public static void Main(string[] args)
	{
		string payload = "{\"monday\":\"75F\",\"tuesday\":\"80F\"}";
		string payloadSignature = "b7412f05e981a473b5ecbdb5393afaea02a679db6d7c8e56803512ec4ba98151";
		string secret = "test-secret";
		bool result = Program.Verify(secret, payload, payloadSignature);
		Console.WriteLine(result); // should print True
	}

	public static bool Verify(string secret, string payload, string signature)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(secret);
		HMAC hmac = new HMACSHA256(bytes);
		bytes = Encoding.UTF8.GetBytes(payload);
		string computedHash = Convert.ToHexString(hmac.ComputeHash(bytes));
		
		ReadOnlySpan<byte> computedSignatureBytes = Convert.FromHexString(computedHash);
		ReadOnlySpan<byte> signatureBytes = Convert.FromHexString(signature);

		return CryptographicOperations.FixedTimeEquals(computedSignatureBytes, signatureBytes);
  }
}