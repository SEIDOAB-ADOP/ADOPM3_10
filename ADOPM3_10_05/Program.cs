using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace ADOPM3_10_05
{
    public class User
    {
        public string UserName { get; set;}
        public string LoginPassword { get; set;}
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var user = new User(){UserName = "Rudolf", LoginPassword="Mupparnasjulsaga"};

            //https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing
            //Using Identity to create a password, usen when Microsoft Identity framework is used.
            var passwordHasher = new PasswordHasher<User>();

            var registeredPassword = "Mupparnasjulsaga"; 
            var hashedPasswordIdentity = passwordHasher.HashPassword(user, registeredPassword);
            Console.WriteLine($"hashedPassword using Identity: {hashedPasswordIdentity}");

            var result = passwordHasher.VerifyHashedPassword(user, hashedPasswordIdentity, user.LoginPassword); 
            bool isPasswordCorrect = result == PasswordVerificationResult.Success; 
            System.Console.WriteLine($"Is Password Correct: {isPasswordCorrect}");


            //Using standalone  KeyDerivation.Pbkdf2
            //Hash a password using salt and streching
            byte[] registeredPasswordKeyDerivation = KeyDerivation.Pbkdf2(
                password: registeredPassword,
                salt: Encoding.UTF8.GetBytes("j78Y#p)/saREN!y3@"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 64);

            string registeredPasswordKeyDerivationB64 = Convert.ToBase64String(registeredPasswordKeyDerivation);
            Console.WriteLine($"hashedPassword using KeyDerivation: {registeredPasswordKeyDerivationB64}");

            byte[] loginPasswordKeyDerivation = KeyDerivation.Pbkdf2(
                password: user.LoginPassword,
                salt: Encoding.UTF8.GetBytes("j78Y#p)/saREN!y3@"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 64);

            string loginPasswordKeyDerivationB64 = Convert.ToBase64String(loginPasswordKeyDerivation);

            isPasswordCorrect = registeredPasswordKeyDerivationB64 == loginPasswordKeyDerivationB64; 
            System.Console.WriteLine($"Is Password Correct: {isPasswordCorrect}");
        }
    }
}
//Exercise:
//1.    Test by modifiying the user.LoginPassword
