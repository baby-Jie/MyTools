using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

namespace MyTools.Views
{
    /// <summary>
    /// Interaction logic for AudioAndVideoWin.xaml
    /// </summary>
    public partial class AudioAndVideoWin : Window
    {
        List<string> audioDevices = new List<string>();
        public AudioAndVideoWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_SoundDevice");
            
            foreach (var item in mos.Get())
            {
                audioDevices.Add(item["Name"].ToString());
            }
            lstbxAudioDevices.ItemsSource = audioDevices;

           // LyncClient client = LyncClient.GetClient();
        }
    }
}
