using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Service.Helpers
{
    public class Helper
    {
        private const int SaltSize = 16; // Salt size in bytes
        private const int HashSize = 32; // Hash size in bytes
        private const int Iterations = 10000; // Number of iterations for the PBKDF2 algorithm


        public static string HashPassword(string password)
        {
            // Generate a random salt
            var salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }


            // Hash the password
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Combine the salt and hash
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert the byte array to a string and return it
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Convert the base64-encoded string back to a byte array
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt from the hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Hash the input password using the same salt and iterations
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Compare the hash values
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;
            }
            return true;
        }

    }
}
