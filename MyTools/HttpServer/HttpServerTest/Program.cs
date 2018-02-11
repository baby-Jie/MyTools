using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener httpListener = new HttpListener();

            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            httpListener.Prefixes.Add("http://192.168.1.189:8090/test/");
            httpListener.Start();
            new Thread(new ThreadStart(delegate
            {
                while (true)
                {

                    HttpListenerContext httpListenerContext = httpListener.GetContext();
                    httpListenerContext.Response.StatusCode = 200;
                    HttpListenerRequest request =  httpListenerContext.Request;
                    Console.WriteLine("Method:{0}",request.HttpMethod);
                    Console.WriteLine("contetntType:{0}",request.ContentType);
                    Console.WriteLine("encoding:" + request.ContentEncoding);
                    Console.WriteLine("UserHostAddress:{0}", request.UserHostAddress);
                    Console.WriteLine("remotePoint:{0}", request.RemoteEndPoint.ToString());
                    Console.WriteLine("hostname:{0}", request.UserHostName);
                    Console.WriteLine("UserAgent:{0}", request.UserAgent);
                    Console.WriteLine("RawUrl:{0}", request.RawUrl);

                    foreach (var item in request.QueryString)
                    {
                        Console.WriteLine("queryStr:{0}, value:{1}", item.ToString(), request.QueryString[item.ToString()]);
                    }
                    using (StreamReader sr = new StreamReader(request.InputStream))
                    {
                        string str = sr.ReadToEnd();
                        Console.WriteLine("input:{0}", str);
                    }
                    //   Console.WriteLine("cookies:{0}", request.Cookies);

                    using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
                    {
                        writer.WriteLine("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/><title>测试服务器</title></head><body>");
                        writer.WriteLine("<div style=\"height:20px;color:blue;text-align:center;\"><p> hello my name is sun mingxing </p></div>");
                        writer.WriteLine("<ul>");
                        writer.WriteLine("</ul>");
                        writer.WriteLine("</body></html>");
                    }
                }
            })).Start();
            Console.ReadKey();
        }
    }
}
