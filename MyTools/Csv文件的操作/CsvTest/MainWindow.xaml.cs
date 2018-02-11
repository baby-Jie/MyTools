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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsvTest
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

        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CsvStreamWriter csvWriter = new CsvStreamWriter("test.csv");
                csvWriter[3, 2] = "test";
                csvWriter.Save();
            }
            catch (Exception ex)
            {
                //LogWriter.Instance.ActionLogger.Error(ex.Message);
            }
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CsvStreamReader csvReader = new CsvStreamReader("test.csv");
                string str = csvReader[3,2];
                Console.WriteLine(str);
            }
            catch (Exception ex)
            {
                //LogWriter.Instance.ActionLogger.Error(ex.Message);
            }
        }
    }
}
