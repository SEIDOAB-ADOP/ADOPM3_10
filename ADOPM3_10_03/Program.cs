using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ADOPM3_10_03
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step1: The Sender informs the Recipient that he/she wants to send a message, 
            //typically a key for symmetric encryption
            byte[] data = Encoding.UTF8.GetBytes("Key for symmetric encryption");

            Console.WriteLine($"Message Sender wants to send to Reciever: {Encoding.UTF8.GetString(data)}");

            //Step2: Receiever creates a public/private keypair, possibly save to disk:
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                File.WriteAllText(fname("Example11_03_PublicKey.xml"), rsa.ToXmlString(false));
                System.Console.WriteLine($"Public key stored in: {fname("Example11_03_PublicKey.xml")}");

                File.WriteAllText(fname("Example11_03_PrivateKey.xml"), rsa.ToXmlString(true));
                System.Console.WriteLine($"Private key stored in: {fname("Example11_03_PrivateKey.xml")}");
            }

            //Reciever keeps the private Key super secret
            string publicPrivate = File.ReadAllText(fname("Example11_03_PrivateKey.xml"));

            //Step 3: Receiever gives ONLY the public key to the sender
            string publicKeyOnly = File.ReadAllText(fname("Example11_03_PublicKey.xml"));


            //Step 4: Sender encrypts the message using the public key and sends it to Reciever
            byte[] encrypted, decrypted;
            using (var rsaPublicOnly = new RSACryptoServiceProvider())
            {
                rsaPublicOnly.FromXmlString(publicKeyOnly);
                encrypted = rsaPublicOnly.Encrypt(data, true);
           }

            //Step 5: Reciever decrypts the message using the Private Key
            using (var rsaPublicPrivate = new RSACryptoServiceProvider())
            {
                // With the private key we can successfully decrypt:
                rsaPublicPrivate.FromXmlString(publicPrivate);
                decrypted = rsaPublicPrivate.Decrypt(encrypted, true);
            }

            Console.WriteLine($"Message recieved: {Encoding.UTF8.GetString(decrypted)}");

            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                documentPath = Path.Combine(documentPath, "ADOP", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }
        }
    }
    //Exercise:
    //1.    Try to decrypt the message using the public Key. What happens?
    //2.    Tamper with the Key values in the XML file and try to Encrypt and Decrypt. What happens?
}
