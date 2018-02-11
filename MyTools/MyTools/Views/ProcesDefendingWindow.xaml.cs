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
    /// Interaction logic for ProcesDefendingWindow.xaml
    /// </summary>
    public partial class ProcesDefendingWindow : Window
    {
        public ProcesDefendingWindow()
        {
            InitializeComponent();
        }

        #region 守护进程示例
        DefendProcess defend;
        private void btnDefendProcess_Click(object sender, RoutedEventArgs e)
        {
            defend = new DefendProcess("ping.exe", "192.168.1.1 /t");
            defend.StartDefendingProcess();
        }

        private void btnEndDefend_Click(object sender, RoutedEventArgs e)
        {
            if (null != defend)
            {
                defend.EndDefendingProcess();
            }
        }
        #endregion
    }
}
