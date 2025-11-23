using System;
using System.IO;
using System.Security.Cryptography;

namespace ADOPM3_10_01b
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] hashMD5, hashSHA1;

            var stringToHash = "The quick brown fox jumps over the lazy dog." +
                "\nThe quick brown fox jumps over the lazy dog." +
                "\nThe quick brown fox jumps over the lazy dog.";

            // Compute hash from this file:
            File.WriteAllText(fname("Example10_01b.txt"), stringToHash);

            //Compute MD5 hash from file
            using (Stream fs = File.OpenRead(fname("Example10_01b.txt")))
                hashMD5 = MD5.Create().ComputeHash(fs);   

            Console.WriteLine("MD5 from Example10_01b.txt");
            foreach (byte b in hashMD5) Console.Write($"{b:x2} ");

            //Compute SHA1 hash from file
            using (Stream fs = File.OpenRead(fname("Example10_01b.txt")))
                hashSHA1 = SHA1.Create().ComputeHash(fs);   

            Console.WriteLine("\n\nSHA1 from Example10_01b.txt");
            foreach (byte b in hashSHA1) Console.Write($"{b:x2} "); 


            // Compute various hash from string:
            byte[] data = System.Text.Encoding.UTF8.GetBytes(stringToHash);
            byte[] hash1 = MD5.Create().ComputeHash(data);
            byte[] hash2 = SHA1.Create().ComputeHash(data);
            byte[] hash3 = SHA256.Create().ComputeHash(data);
            byte[] hash4 = SHA512.Create().ComputeHash(data);

            Console.WriteLine($"\n\n\nMD5 is {hash1.Length} bytes long");
            foreach (byte b in hash1) Console.Write($"{b:x2} ");
            System.Console.WriteLine($"\nAs base64: {Convert.ToBase64String(hash1)}");

            Console.WriteLine($"\n\nSHA1 is {hash2.Length} bytes long");
            foreach (byte b in hash2) Console.Write($"{b:x2} "); 
            System.Console.WriteLine($"\nAs base64: {Convert.ToBase64String(hash2)}");

            Console.WriteLine($"\n\nSHA256 is {hash3.Length} bytes long");
            foreach (byte b in hash3) Console.Write($"{b:x2} "); 
            System.Console.WriteLine($"\nAs base64: {Convert.ToBase64String(hash3)}");

            Console.WriteLine($"\n\nSHA512 is {hash4.Length} bytes long");
            foreach (byte b in hash4) Console.Write($"{b:x2} ");
            var hash4Base64 = Convert.ToBase64String(hash4);
            System.Console.WriteLine($"\nAs base64: {hash4Base64}");

            Console.WriteLine("\n\nConvert hash in Base64 string back to hash");
            var hash4Again = Convert.FromBase64String(hash4Base64);
            Console.WriteLine($"\n\nSHA512 is {hash4Again.Length} bytes long");
            foreach (byte b in hash4Again) Console.Write($"{b:x2} ");


            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                documentPath = Path.Combine(documentPath, "ADOP", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }
        }

        //Exercise:
        //1.    Modify file content and string content and generate new Hash. Compare with old
        //2.    Convert the Encrypted string to Base64 and write it to a text file.
    }
}
