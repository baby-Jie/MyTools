using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using ToolCollection.Encrypts;

namespace MyTools
{
    /// <summary>
    /// Interaction logic for EncryptWindow.xaml
    /// </summary>
    public partial class EncryptWindow : Window
    {
        public EncryptWindow()
        {
            InitializeComponent();
        }

        private void btnMD5EncryptString_Click(object sender, RoutedEventArgs e)
        {
            string decryptText =  MD5Encrypt.GetEncrptStringByMD5(txtBxEncryptText.Text.Trim());
            txtBxDecryptText.Text = decryptText;
        }

        private void btnMD5EncryptFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "所有文件(*.*)|*.*";
            dialog.Title = "请选择文件";

            if ((bool)dialog.ShowDialog())
            {
                string fileName = dialog.FileName;
                string decryptText = MD5Encrypt.GetEncryptFileByMD5(fileName);

                txtBxEncryptText.Text = fileName;
                txtBxDecryptText.Text = decryptText;
            }
        }

        private void btnDesEncryptString_Click(object sender, RoutedEventArgs e)
        {
            string decryptText = DESEncrypt.DESEncryptString(txtBxEncryptText.Text.Trim());
            txtBxDecryptText.Text = decryptText;
        }

        private void btnDESDecryptFile_Click(object sender, RoutedEventArgs e)
        {
            string cryptText = txtBxDecryptText.Text.Trim();
            txtBxEncryptText.Text = DESEncrypt.DESDecryptString(cryptText);
        }
    }
}
