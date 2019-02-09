using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RegistrationForm.Infrastructure.Security
{
    public class PasswordEncrypter : IPasswordEncrypter
    {
        public string Encrypt(string password)
        {
            byte[] salt = new byte[128 / 8];

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
