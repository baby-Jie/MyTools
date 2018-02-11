using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection
{
    public class HttpHelper
    {
        #region 请求方法
        /// <summary>
        /// 无认证的Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string GetHttpWithoutIdentification(string url, int timeout = 5000)
        {
            string content = string.Empty;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = timeout;
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogWriter.Instance.ActionLogger.Error(ex.Message);
            }

            return content;
        }

        /// <summary>
        /// Digest认证的Http的Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string GetHttpWithDigestIdentifiction(string url, string name, string pass, int timeout)
        {
            string content = string.Empty;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = timeout;

                //加认证
                webRequest.Credentials = GetCredentialCache(url, name, pass, "Digest");
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.Message);
            }
            return content;
        }

        /// <summary>
        /// Basic认证的Http的Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string GetHttpWithBasicIdentifiction(string url, string name, string pass, int timeout)
        {
            string content = string.Empty;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = timeout;
                //加Baisc认证
                webRequest.Headers.Add("Authorization", GetBasicAuthorization(name, pass));
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.Message);
            }
            return content;
        }


        public static string HttpPostWithoutIdentification(string url, string data, int timeout)
        {
            string str = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeout;
            request.ContentType = "application/json";

            using (Stream stream = request.GetRequestStream())
            {
                byte[] dataBytes = Encoding.ASCII.GetBytes(data);
                stream.Write(dataBytes, 0, dataBytes.Length);
            }

            WebResponse reponse = request.GetResponse();
            using (StreamReader sr = new StreamReader(reponse.GetResponseStream()))
            {
                str = sr.ReadToEnd();
            }

            return str;
        }
     //   public static string HttpPostWithBasicIdentifiction(string url)



        /// <summary>
        /// CredentialCache
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <param name="authType"></param>
        /// <returns></returns>
        private static CredentialCache GetCredentialCache(string Url, string name, string pass, string authType)
        {
            CredentialCache cache = new CredentialCache();
            cache.Add(new Uri(Url), authType, new NetworkCredential(name, pass));
            return cache;
        }


        /// <summary>
        /// Basic认证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private static string GetBasicAuthorization(string name, string pass)
        {
            string auth = string.Format("{0}:{1}", name, pass);
            return "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(auth));
        }

        private static string GetDigestAuthorization(string name, string pass)
        {
            string auth = string.Format("{0}:{1}", name, pass);
            return "Digest " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(auth));
        }

        #endregion


        public static string SendHttpCommand(HttpInfo httpInfo)
        {
            string retStr = string.Empty;

            try
            {
                #region Get请求
                if (httpInfo.Method == HttpMethod.GET)
                {
                    //认证
                    if (httpInfo.IsIdentifition)
                    {
                        //Basic认证
                        if (httpInfo.Authentication == HttpAuthenticationType.Basic)
                        {
                            retStr = GetHttpWithBasicIdentifiction(httpInfo.Url, httpInfo.Name, httpInfo.Password, httpInfo.TimeOut);
                        }
                        //Digest认证
                        else if (httpInfo.Authentication == HttpAuthenticationType.Digest)
                        {
                            retStr = GetHttpWithDigestIdentifiction(httpInfo.Url, httpInfo.Name, httpInfo.Password, httpInfo.TimeOut);
                        }
                    }
                    //无认证
                    else
                    {
                        retStr = GetHttpWithoutIdentification(httpInfo.Url, httpInfo.TimeOut);
                    }
                }
                #endregion
                #region Post请求
                else if(httpInfo.Method == HttpMethod.POST)
                {
                    //认证
                    if (httpInfo.IsIdentifition)
                    { }
                    //无认证
                    else
                    {
                        retStr = HttpPostWithoutIdentification(httpInfo.Url, httpInfo.DataBody, httpInfo.TimeOut);
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                LogHelper.Log("SendHttpCommand Err:{0}", ex.Message);
            }

            return retStr;
        }

       
    }

    public class HttpInfo
    {
        public HttpMethod Method { get; set; }

        public bool IsIdentifition { get; set; }

        public HttpAuthenticationType Authentication { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        private int timeout = 5000;

        public int TimeOut
        {
            get { return timeout; }
            set { timeout = value; }
        }

        public string DataBody { get; set; }
    }

    public enum HttpMethod
    {
        GET,
        POST
    }

    public enum HttpAuthenticationType
    {
        Digest,
        Basic
    }
}
