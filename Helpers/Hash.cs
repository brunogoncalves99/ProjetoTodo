using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ToDo.Helpers
{
    public class Hash
    {
        public static string Create(string password, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public static bool Validate(string password, string salt, string hash)
            => Create(password, salt) == hash;
    }
}