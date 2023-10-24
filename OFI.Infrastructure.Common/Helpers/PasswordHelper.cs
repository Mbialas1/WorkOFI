using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Common.Helpers
{
    public class PasswordHelper
    {
        public static string GenerateHash(string password, string salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(salt)))
            {
                var hashed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashed);
            }
        }
        public static string GenerateSalt()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

    }
}
