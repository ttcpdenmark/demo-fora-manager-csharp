using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ForaManagerTestClient
{
    class AesHelper
    {
        internal static String Encrypt_AES(String data, String key, String iv)
        {
            byte[] encryptedBytes = null;

            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(data);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 256;
                    AES.Padding = PaddingMode.Zeros;
                    AES.Key = Encoding.UTF8.GetBytes(key);
                    AES.IV = Encoding.UTF8.GetBytes(iv);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(AES.Key, AES.IV), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return Convert.ToBase64String(encryptedBytes, 0, encryptedBytes.Length);
        }

        internal static String Decrypt_AES(String data, String key, String iv)
        {
            string plaintext = null;
            
            byte[] bytesToBeDecrypted = Convert.FromBase64String(data);

            using (MemoryStream ms = new MemoryStream(bytesToBeDecrypted))
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 256;
                    AES.Padding = PaddingMode.Zeros;
                    AES.Key = Encoding.UTF8.GetBytes(key);
                    AES.IV = Encoding.UTF8.GetBytes(iv);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(AES.Key, AES.IV), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(cs))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            plaintext = plaintext.Substring(0,plaintext.LastIndexOf("}")+1);
            return plaintext;
        }
    }
}
