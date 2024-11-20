using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ADOPM3_10_04
{
    class Program
    {
        static void Main(string[] args)
        {
			byte[] data = Encoding.UTF8.GetBytes("File/Document/Image or other Data to sign");
			byte[] publicKey;
			byte[] signature;
			object hasher = SHA512.Create();         

			// Generate a new key pair, then sign the data with it:
			using (var publicPrivate = new RSACryptoServiceProvider(2048))
			{
				signature = publicPrivate.SignData(data, hasher);
				publicKey = publicPrivate.ExportCspBlob(false);    // get public key
			}

			// Create a fresh RSA using just the public key, then test the signature.
			using (var publicOnly = new RSACryptoServiceProvider())
			{
				publicOnly.ImportCspBlob(publicKey);
				Console.WriteLine(publicOnly.VerifyData(data, hasher, signature)); // True

				// Let's now tamper with the data, and recheck the signature:
				data[0] = 0;
				Console.WriteLine(publicOnly.VerifyData(data, hasher, signature)); // False
			}
        }
    }
	//Exercise
	//1.	Try to sign the data using the public key only
	//2.	Create a text file, Read it as a FileStream and sign it, store the signature, public and private key on file.
	//		Read the text file, public key and signature and verify the signature.
	//3.	Tamper with the text file and show that the signature is not longer valid
}
