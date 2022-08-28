using System.Security.Cryptography;
using System.Text;

namespace Core
{
    public class Hasher
    {
        public Hasher()
        {

        }

        public string HashStringSha(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(str)));
        }
    }
}
