using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Capa.LogicaNegocio
{
    public class EncryptionService : IEncryptionService
    {
        private readonly string _key = "DINET2026";

        public string Encrypt(string text)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_key.PadRight(32));
            byte[] iv = new byte[16];

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor();
                byte[] bytes = Encoding.UTF8.GetBytes(text);

                byte[] encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);

                return Convert.ToBase64String(encrypted)
                        .Replace("+", "-")
                        .Replace("/", "_")
                        .Replace("=", "");
            }
        }

        public string Decrypt(string cipher)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_key.PadRight(32));
            byte[] iv = new byte[16];

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor();
                string incoming = cipher
                        .Replace("-", "+")
                        .Replace("_", "/");

                                    switch (incoming.Length % 4)
                                    {
                                        case 2: incoming += "=="; break;
                                        case 3: incoming += "="; break;
                                    }

                byte[] buffer = Convert.FromBase64String(incoming);

                byte[] decrypted = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}
