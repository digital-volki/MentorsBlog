using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MentorsBlog.Core.Common
{
    public static class CryptographyService
    {
        public static string HashToSHA256(this string source)
        {
            var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(source));
            return hashBytes.Aggregate(string.Empty, (current, theByte) 
                => current + theByte.ToString("x2"));
        }
    }
}