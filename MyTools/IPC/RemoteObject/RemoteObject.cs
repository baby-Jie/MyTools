using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageObject
{
    public class RemoteObject: MarshalByRefObject
    {
        public static Queue<string> qMessage = new Queue<string>();

        public string AddMessage(string message)
        {
            if (qMessage == null)
            {
                qMessage = new Queue<string>();
            }
            qMessage.Enqueue(message);
            return message;
        }
    }
}
