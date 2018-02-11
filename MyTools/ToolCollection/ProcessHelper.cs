using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCollection
{
    public class ProcessHelper
    {

        public event Action<string> DataRecvHandler;
        public static async  Task<string> StartProcessOnceAsync(string cmd, string args)
        {
            string str = string.Empty;
            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                str = await p.StandardOutput.ReadToEndAsync();
                p.WaitForExit();
                p.Close();
            }
            return str;
        }

        public  void StartProcessLast(string cmd, string args)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.OutputDataReceived += P_OutputDataReceived;
                p.Start();
                p.BeginOutputReadLine();
             
                //p.WaitForExit();//会卡主线程
                //p.Close();
            }
        }

        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            DataRecvHandler(e.Data);
        }
    }
}
