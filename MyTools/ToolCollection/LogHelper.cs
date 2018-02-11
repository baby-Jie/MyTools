using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection
{
    public class LogHelper
    {
        public static void Log(string message, params object[] parms)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("log.txt"))
                {
                    string str = DateTime.Now + ":" + string.Format(message, parms);
                    sw.WriteLine(str);
                }
            }
            catch (Exception)
            {

               
            }
        }
    }
}
