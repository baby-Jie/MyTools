using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ToolCollection
{
    /// <summary>
    /// 用于启动一个进程并守护它，可以重定向输入、输出、错误流，可以不显示窗口，用cmd的话，只定义了重定向输入流会有点问题。
    /// </summary>
    public class DefendProcess
    {
        string cmdText;
        string arguments;

        Process process;
        ProcessStartInfo startInfo;
        Timer timerToDefendProcess;
        public bool IsDefending;

        /// <summary>
        /// 带参数构造函数，进程的命令和参数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="arguments"></param>
        public DefendProcess(string cmdText, string arguments)
        {

            this.cmdText = cmdText;
            this.arguments = arguments;
            InitialStartInfo();
        }



        public void StartDefendingProcess()
        {
            InitialDefendTimer();
            timerToDefendProcess.Start();
            IsDefending = true;
        }
        public void EndDefendingProcess()
        {
            try
            {
              
                EraseProcess();
                CloseDefendTimer();
                if (process != null && !process.HasExited)
                {
                    process.Kill();
                }
                IsDefending = false;
            }
            catch (Exception ex)
            {
             //   LogWriter.Instance.ActionLogger.Error(ex.Message);
            }

        }
        void StartProcess()
        {
            EraseProcess();
            process = new Process();
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //如果进程不存在了，清扫垃圾，重启进程
            if (!IsProcessExists())
            {
                StartProcess();
            }
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }

        /// <summary>
        /// 根据文件路径获取文件名（不带后缀exe）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetProgramName(string path)
        {
            string fileName = System.IO.Path.GetFileName(path);
            if (!string.IsNullOrWhiteSpace(fileName) && fileName.Contains("."))
            {
                fileName = fileName.Split('.')[0];
            }
            return fileName;
        }

        /// <summary>
        /// 判断进程是否存在
        /// </summary>
        /// <returns></returns>
        bool IsProcessExists()
        {
            string programName = GetProgramName(cmdText);
            if (process == null)
                return false;
            IEnumerable<Process> pros = Process.GetProcessesByName(programName).Where(pro => pro.Id == process.Id);
            if (pros.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 初始化进程启动信息
        /// </summary>
        void InitialStartInfo()
        {
            startInfo = new ProcessStartInfo();
            startInfo.FileName = cmdText;
            startInfo.Arguments = arguments;

            //
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
        }

        /// <summary>
        /// 初始化守护进程的定时器
        /// </summary>
        void InitialDefendTimer()
        {
            timerToDefendProcess = new Timer(3 * 1000);
            timerToDefendProcess.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

       
        /// <summary>
        /// 销毁守护进程的定时器
        /// </summary>
        void CloseDefendTimer()
        {
            if (null != timerToDefendProcess)
            {
                timerToDefendProcess.Elapsed -= timer_Elapsed;
                timerToDefendProcess.Stop();
                timerToDefendProcess.Close();
                timerToDefendProcess.Dispose();
                timerToDefendProcess = null;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        void EraseProcess()
        {
            if (null != process)
            {
                process.ErrorDataReceived -= Process_ErrorDataReceived;
                process.OutputDataReceived -= Process_OutputDataReceived;
            }
        }
    }
}
