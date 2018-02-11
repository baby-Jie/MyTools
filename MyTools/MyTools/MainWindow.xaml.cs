using MyTools.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolCollection;

namespace MyTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHttpTool_Click(object sender, RoutedEventArgs e)
        {
            HttpGetAndPostTest httpTool = new HttpGetAndPostTest();
            httpTool.ShowDialog();
        }

        private void btnUdpServer_Click(object sender, RoutedEventArgs e)
        {
            UdpServerWindow udpTool = new UdpServerWindow();
            udpTool.ShowDialog();
        }

        private void btnProcessDefending_Click(object sender, RoutedEventArgs e)
        {
            ProcesDefendingWindow processTool = new ProcesDefendingWindow();
            processTool.ShowDialog();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            EncryptWindow encrypt = new EncryptWindow();
            encrypt.ShowDialog();
        }

        private void btnTestProcess_Click(object sender, RoutedEventArgs e)
        {
            ProcessWin win = new ProcessWin();
            win.ShowDialog();
        }

        private void btnTestDevices_Click(object sender, RoutedEventArgs e)
        {
            AudioAndVideoWin win = new AudioAndVideoWin();
            win.ShowDialog();
        }
    }
}
