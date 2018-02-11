using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection
{
    public class CommanTool
    {

        #region 文件IO操作

        /// <summary>
        /// 通过路径文件获取不带后缀的文件名
        /// </summary>
        /// <param name="fielPath"></param>
        /// <returns></returns>
        public static string GetFileNameWithOutExtension(string fielPath)
        {
            return Path.GetFileNameWithoutExtension(fielPath);
        }

        /// <summary>
        /// 通过文件路径获取带后缀的文件名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileNameWithExtension(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 通过文件路径获取文件的后缀名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 通过文件路径获取文件的目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        #endregion

        #region 网络操作


        /// <summary>
        /// 获取本地Ip
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIp()
        {
            string hostName = Dns.GetHostName();//获取本地主机名

            IPAddress[] ipLists = Dns.GetHostAddresses(hostName);

            if (null == ipLists || ipLists.Count() <= 0)
            {
                return null;
            }
            IPAddress localIp = null;
            try
            {
                localIp = ipLists.Where((ip) => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First();
            }
            catch (Exception ex)
            {
                //LogWriter.Instance.ActionLogger.Error(ex.Message);
            }
            return localIp;
        }

        /// <summary>
        /// 获取本地ip列表
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] GetLocalIpList()
        {
            string hostName = Dns.GetHostName();//获取本地主机名

            IPAddress[] ipLists = Dns.GetHostAddresses(hostName);

            return ipLists;
        }

        #endregion

        #region 字符串操作
        /// <summary>
        /// 将字节数组转换成16进制字符串
        /// </summary>
        /// <param name="tagBytes"></param>
        /// <returns></returns>
        public static string ChangeBytesToHexStr(byte[] tagBytes)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in tagBytes)
            {
                str.Append(item.ToString("X2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// 将16进制字符串（以英文逗号分隔）转换成字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ChangeHexStrToBytes(string str)
        {
            string[] cmdStrs = str.Split(',');
            List<byte> sendBytes = new List<byte>();
            foreach (var item in cmdStrs)
            {
                sendBytes.Add(Convert.ToByte(item, 16));
            }
            return sendBytes.ToArray();
        }



        #endregion

        #region 集合操作

        /// <summary>
        /// 获取子数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldArr"></param>
        /// <param name="startIndex"></param>
        /// <param name="getNum"></param>
        /// <returns></returns>
        public static T[] GetSubArray<T>(T[] oldArr, int startIndex, int getNum)
        {
            return oldArr.Skip(startIndex).Take(getNum).ToArray();
        }

        #endregion

    }
}
