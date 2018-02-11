using MessageObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPCServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
            ReceiveMessage();
        }

        static void StartServer()
        {
            IpcServerChannel serverChannel = new IpcServerChannel("serverChannel");
            ChannelServices.RegisterChannel(serverChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject), "ipcTest", WellKnownObjectMode.SingleCall);
        }

        static void ReceiveMessage()
        {
            while (true)
            {
                Queue<string> qMessage = RemoteObject.qMessage;

                if (qMessage != null && qMessage.Count > 0)
                {
                    string message = qMessage.Dequeue();
                    Console.WriteLine(message);
                }
                Thread.Sleep(1000);
            }
        }
    }
}
