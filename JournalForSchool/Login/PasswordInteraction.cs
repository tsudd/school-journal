using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Login
{
    public static class PasswordInteraction
    {
        public static string GetPasswordHash(string password)
        {
            var hasher = new SHA256Managed();
            var unhashed = System.Text.Encoding.Unicode.GetBytes(password);
            var hashed = hasher.ComputeHash(unhashed);

            string hashedPassword = System.Text.Encoding.Default.GetString(hashed);

            return hashedPassword;
        }
    }
}
