using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for HttpGetAndPostTest.xaml
    /// </summary>
    public partial class HttpGetAndPostTest : Window
    {
        public HttpGetAndPostTest()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
          
            HttpInfo httpInfo = GetHttpInfo();
            GetHttpMessage(httpInfo); 
        }

        private HttpInfo GetHttpInfo()
        {
            HttpInfo httpInfo = new HttpInfo();
            string url = txtBoxUrl.Text.Trim();
            string name = txtBoxName.Text.Trim();
            string password = txtBoxPass.Text.Trim();
            string postData = txtBoxBody.Text.Trim();
            bool isIdentifition = !((bool)radioBtnNoCache.IsChecked);

            HttpMethod method;
            if ((bool)radioBtnGet.IsChecked)
            {
                method = HttpMethod.GET;
            }
            else if ((bool)radioBtnPost.IsChecked)
            {
                method = HttpMethod.POST;
            }
            else
            {
                method = HttpMethod.GET;
            }

            HttpAuthenticationType authType = HttpAuthenticationType.Basic;
            if (isIdentifition)
            {
                if ((bool)radioBtnBasic.IsChecked)
                    authType = HttpAuthenticationType.Basic;
                else
                    authType = HttpAuthenticationType.Digest;
            }

            httpInfo.Url = url;
            httpInfo.Name = name;
            httpInfo.Password = password;
            httpInfo.DataBody = postData;
            httpInfo.IsIdentifition = isIdentifition;
            httpInfo.Method = method;
            httpInfo.Authentication = authType;


            return httpInfo;
        }

        async void GetHttpMessage(HttpInfo httpInfo)
        {
            string content = string.Empty;
            await Task.Run((Action)(()=> 
            {
                content = HttpHelper.SendHttpCommand(httpInfo);
            }));
            if (string.IsNullOrWhiteSpace( content))
                ShowMessage("error");
            else
                ShowMessage(content);
        }

       
        void ShowMessage(string message)
        {
            txtBoxShowReceiveMessage.Dispatcher.BeginInvoke((Action)(()=> 
            {
                txtBoxShowReceiveMessage.Text = message;
            }));
        }
    }
   
}
