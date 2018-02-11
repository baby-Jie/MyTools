using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection
{
    public class UdpServerHelper
    {

        public event Action<string> ReceiveStringDataEvent;
        public event Action<byte[]> ReceiveBinaryDataEvent;

        IPEndPoint endPoint;
        UdpClient udpServer;
        TaskFactory factory;
        Task udpServerRecvTask;
        bool isCancelTask = false;

        public UdpServerHelper(int port)
        {
            udpServer = new UdpClient(port, AddressFamily.InterNetwork);
            endPoint = new IPEndPoint(IPAddress.Any, port);
            factory = new TaskFactory();
        }

        /// <summary>
        /// 开始监听接收字符串
        /// </summary>
        public void StartUdpListen()
        {
            if (null == factory)
            {
                return;
            }

            isCancelTask = false;
            udpServerRecvTask = factory.StartNew(() =>
            {
                while (!isCancelTask)
                {
                    try
                    {
                        byte[] rcvBytes = udpServer.Receive(ref endPoint);
                        string rcvStr = Encoding.Default.GetString(rcvBytes);
                        if (null != ReceiveStringDataEvent)
                        {
                            ReceiveStringDataEvent(rcvStr);
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogWriter.Instance.ActionLogger.Error(ex.Message);
                    }
                   
                }
            });
        }

        /// <summary>
        /// 开始监听接收字符数组
        /// </summary>
        public void StartUpListenBinary()
        {
            if (null == factory)
            {
                return;
            }

            isCancelTask = false;
            udpServerRecvTask = factory.StartNew(() =>
            {
                try
                {
                    while (!isCancelTask)
                    {
                        byte[] rcvBytes = udpServer.Receive(ref endPoint);

                        if (null != ReceiveBinaryDataEvent)
                        {
                            ReceiveBinaryDataEvent(rcvBytes);
                        }
                    }
                }
                catch (Exception)
                {
                }

            });
        }


        /// <summary>
        /// 关闭Server
        /// </summary>
        public void CloseUdp()
        {
            StopUdpListen();
            udpServer.Close();
        }


        /// <summary>
        /// 关闭监听
        /// </summary>
        private void StopUdpListen()
        {
            isCancelTask = true;
        }

        /// <summary>
        /// 给udp发送数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="ipEndPoint"></param>
        public void SendMessage(byte[] bytes, IPEndPoint ipEndPoint)
        {
            try
            {
                udpServer.Send(bytes, bytes.Length, ipEndPoint);
            }
            catch (Exception ex)
            {
                //LogWriter.Instance.ActionLogger.Error(ex.Message);
            }

        }
    }
}
