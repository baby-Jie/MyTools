using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ToolCollection;

namespace MyTools
{
    /// <summary>
    /// Interaction logic for UdpServerWindow.xaml
    /// </summary>
    public partial class UdpServerWindow : Window
    {
        public UdpServerWindow()
        {
            InitializeComponent();
        }

        #region UdpServer示例
        private void btnShowLocalIp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CommanTool.GetLocalIp().ToString());
        }

        UdpServerHelper udpServer;
        private void btnUdpOpen_Click(object sender, RoutedEventArgs e)
        {
            udpServer = new UdpServerHelper(int.Parse(txtBoxUdpPort.Text));
            udpServer.ReceiveStringDataEvent += UdpServer_ReceiveStringDataEvent;
            udpServer.StartUdpListen();
        }

        private void UdpServer_ReceiveStringDataEvent(string obj)
        {
            ShowUdpMessage(obj);
        }

        void ShowUdpMessage(string message)
        {
            txtRecvFromUdp.Dispatcher.BeginInvoke((Action)(() =>
            {
                txtRecvFromUdp.Text = message;
            }));
        }

        private void btnUdpClose_Click(object sender, RoutedEventArgs e)
        {
            if (null != udpServer)
            {
                udpServer.CloseUdp();
            }
        }
        #endregion
    }
}
