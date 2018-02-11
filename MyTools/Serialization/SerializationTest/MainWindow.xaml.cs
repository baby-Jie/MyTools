using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SerializationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student stu;
        public MainWindow()
        {
            InitializeComponent();
            stu = new Student() { Name = "smx", Age = 10};

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = File.Open("binary.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                binaryFormatter.Serialize(fs, stu);
            }
                
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student));
            using (FileStream fs = File.Open("xmltest.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                xmlSerializer.Serialize(fs, stu);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string str = JsonConvert.SerializeObject(stu);
            textBox.Text = str;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string str = textBox.Text;
            Student stutest = (Student)JsonConvert.DeserializeObject(str, typeof(Student));
            MessageBox.Show("name:" + stutest.Name + " age:" + stutest.Age.ToString());
        }
    }
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
