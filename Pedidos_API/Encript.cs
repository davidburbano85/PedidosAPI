using System.Security.Cryptography;
using System.Text;

namespace Pedidos_API
{
    public class Encript
    {
        public static string GetSHA256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] stream = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < stream.Length; i++)
                {
                    sb.Append(stream[i].ToString("x2"));
                }
                return sb.ToString();
            }

        }
    }

}
