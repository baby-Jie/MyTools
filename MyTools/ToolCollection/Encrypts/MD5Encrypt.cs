using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection.Encrypts
{
    public class MD5Encrypt
    {
        public static string GetEncrptStringByMD5(string source, int length = 32)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                MD5 md5 = MD5.Create();
                byte[] srcBytes = Encoding.UTF8.GetBytes(source);
                byte[] outBytes = md5.ComputeHash(srcBytes);
                
                switch (length)
                {
                    case 16:
                        for (int i = 4; i < 12; i++)
                        {
                            sb.Append(outBytes[i].ToString("X2"));
                        }
                        break;
                    case 32:
                        for (int i = 0; i < 16; i++)
                        {
                            sb.Append(outBytes[i].ToString("X2"));
                        }
                        break;
                    default:
                        for (int i = 0; i < outBytes.Length; i++)
                        {
                            sb.Append(outBytes[i].ToString("X2"));
                        }
                        break;
                }


            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.Message);
            }
            return sb.ToString();
        }

        public static string GetEncryptFileByMD5(string filePath, int length = 32)
        {
            StringBuilder sb = new StringBuilder();

           
                MD5 md5 = MD5.Create();
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] outBytes = md5.ComputeHash(fs);

                    switch (length)
                    {
                        case 16:
                            for (int i = 4; i < 12; i++)
                            {
                                sb.Append(outBytes[i].ToString("X2"));
                            }
                            break;
                        case 32:
                            for (int i = 0; i < 16; i++)
                            {
                                sb.Append(outBytes[i].ToString("X2"));
                            }
                            break;
                        default:
                            for (int i = 0; i < outBytes.Length; i++)
                            {
                                sb.Append(outBytes[i].ToString("X2"));
                            }
                            break;
                    }
                }

           
            return sb.ToString();
        }
    }
}
