using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WindowServiceManagment
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            string currentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = System.IO.Path.Combine(currentDirectory, "Services");

            Process process = new Process();
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();


            System.Environment.CurrentDirectory = currentDirectory;
        }

        private void btnUninstall_Click(object sender, RoutedEventArgs e)
        {
            string currentDirectory = System.Environment.CurrentDirectory;
            System.Environment.CurrentDirectory = System.IO.Path.Combine(currentDirectory, "Services");

            Process process = new Process();
            process.StartInfo.FileName = "Uninstall.bat";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();


            System.Environment.CurrentDirectory = currentDirectory;
        }
    }
}
