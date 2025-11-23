using System;

namespace ADOPM3_10_01a
{
    class Program
    {
        static void Main(string[] args)
        {
            //Base-64 conversion

            //Step 1: Original
            //Either a string with or already a byte[], perhaps an image
            string strOrigin = "This is a string to demonstrate and generate a hash in byte[]";
            byte[] bytesOrigin = System.Text.Encoding.UTF8.GetBytes(strOrigin);
            Console.WriteLine();
            Console.WriteLine(strOrigin);

            //Step2: Convert to Base64:
            string strBase64 = Convert.ToBase64String(bytesOrigin);

            //char[] charsBase64 = new char[strBase64.Length];
            //Convert.ToBase64CharArray(bytesOrigin, 0, bytesOrigin.Length, charsBase64, 0);
            Console.WriteLine(strBase64); //The text representation of the string, byte[] or image

            //Step3: Convert back to byte[] or string
            //First the Base64 string
            byte[] bytesConverted = Convert.FromBase64String(strBase64);
            string stringConverted = System.Text.Encoding.UTF8.GetString(bytesConverted);
            Console.WriteLine(stringConverted);
        }
    }
}
