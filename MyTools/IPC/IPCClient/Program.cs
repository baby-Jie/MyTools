using MessageObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Text;
using System.Threading.Tasks;

namespace IPCClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteObject obj = ConnectServer();
            SendMessage(obj);
        }

        static RemoteObject ConnectServer()
        {
            IpcClientChannel clientChannel = new IpcClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);

           return (RemoteObject)Activator.GetObject(typeof(RemoteObject), "ipc://serverChannel/ipcTest");
        }

        static void SendMessage(RemoteObject obj)
        {
            while (true)
            {
                string str = Console.ReadLine();
                obj.AddMessage(str);
            }
        }
    }
}
