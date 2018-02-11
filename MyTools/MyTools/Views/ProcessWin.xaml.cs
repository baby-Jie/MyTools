using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToolCollection;

namespace MyTools.Views
{
    /// <summary>
    /// Interaction logic for ProcessWin.xaml
    /// </summary>
    public partial class ProcessWin : Window
    {
        public ProcessWin()
        {
            InitializeComponent();
        }


        private async void ShowProcessReturnAsync()
        {
            Console.WriteLine("start:{0}",Thread.CurrentThread.ManagedThreadId);
            string str = await ProcessHelper.StartProcessOnceAsync(txtBoxCommand.Text.Trim(), txtBoxArguments.Text.Trim());
            Console.WriteLine("after one wait:{0}", Thread.CurrentThread.ManagedThreadId);
            txtBoxShowMessage.Text = str;
            str = await Task.Run(()=> 
            {
                Console.WriteLine("in two wait:{0}", Thread.CurrentThread.ManagedThreadId);
                return Test();
            });
          str = txtBoxArguments.Text; //不会卡主线程
            Console.WriteLine("after two wait:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ui:{0}", Thread.CurrentThread.ManagedThreadId);
            ShowProcessReturnAsync();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(20);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProcessHelper help = new ProcessHelper();
            help.DataRecvHandler += Help_DataRecvHandler;
            help.StartProcessLast(txtBoxCommand.Text.Trim(), txtBoxArguments.Text.Trim());
        }

        private void Help_DataRecvHandler(string obj)
        {
            this.Dispatcher.BeginInvoke((Action)(()=>
            {
                txtBoxShowMessage.AppendText(obj + "\n");
            }));
        }

        private string  Test()
        {
          //  Thread.Sleep(2000);
       //     string str = txtBoxArguments.Text;//会卡主线程  
            return "1234";
        }
    }
}
