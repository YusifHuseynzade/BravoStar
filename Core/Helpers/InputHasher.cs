using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers
{
    public static class InputHasher
    {
        public static string HashInputSHA256(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));


            var stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
