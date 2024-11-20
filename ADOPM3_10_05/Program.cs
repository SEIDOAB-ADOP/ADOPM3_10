using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ADOPM3_10_05
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hash a password using salt and streching
            byte[] encrypted = KeyDerivation.Pbkdf2(
                password: "Mupparnasjulsaga",
                salt: Encoding.UTF8.GetBytes("j78Y#p)/saREN!y3@"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 64);

            Console.WriteLine("Encrypted password");
            foreach (byte b in encrypted) Console.Write($"{b:x2} ");

            Console.WriteLine("\n\nEncrypted password in Base64 string");
            string Base64encryption = Convert.ToBase64String(encrypted);
            Console.WriteLine(Base64encryption);

        }
    }
}
//Exercise:
//1.    Use the KeyDerivation.Pbkdf2 to create a key of 16 bytes from a sentence "Stockholm is nice in spring"
//2.    Modify the code generating the key in the symetric encryption/decryption scheme, ADOPM3_10_02,
//      to use key generated from  "Stockholm is nice in spring". This means that is the magic sentence Reciever and Sender is now sharing

