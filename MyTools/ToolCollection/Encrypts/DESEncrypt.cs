using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection.Encrypts
{
    public class DESEncrypt
    {
        private static string key = "slovexyj";
        private static string vector = "slovexyj";

        private static byte[] rgbKey = ASCIIEncoding.ASCII.GetBytes(key.Substring(0,8));
        private static byte[] rgbIV = ASCIIEncoding.ASCII.GetBytes(vector.Substring(0,8));

        public static void SetKeyAndVector(string keySet, string vectorSet)
        {
            key = keySet;
            vector = vectorSet;

            rgbKey = ASCIIEncoding.ASCII.GetBytes(key.Substring(0,8));
            rgbIV = ASCIIEncoding.ASCII.GetBytes(vector.Substring(0,8));
        }

        public static string DESEncryptString(string source)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(ms, dsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cryptoStream);
                sw.Write(source);
                sw.Flush();
                cryptoStream.FlushFinalBlock();
                ms.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
        }

        public static string DESDecryptString(string source)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            byte[] inBytes = Convert.FromBase64String(source);
            using (MemoryStream ms = new MemoryStream(inBytes))
            {
                CryptoStream cryptoStream = new CryptoStream(ms, dsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Read);
                using (StreamReader sr = new StreamReader(cryptoStream))
                {
                    string str = sr.ReadToEnd();
                 //   cryptoStream.FlushFinalBlock();
                    return str;
                }
            }
        }
    }
}
