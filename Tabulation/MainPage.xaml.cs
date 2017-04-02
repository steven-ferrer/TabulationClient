using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tabulation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static string _judgeID;
        public static string _JudgeID
        {
            get { return _judgeID; }
            set { _judgeID = value; }
        }

        private static string _serverAddress;
        public static string _ServerAddress
        {
            get { return _serverAddress; }
            set { _serverAddress = value; }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        public async void setServerAddress(object sender, RoutedEventArgs e)
        {
            //Create HTTP client object
            HttpClient webClient = new HttpClient();

            //user-agent header to the GET request
            var headers = webClient.DefaultRequestHeaders;
            //string header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

            //assign URI string for cheer
            string uriString = "http://" + tb_serverAdd.Text + "/tabulation/";
            Uri requestUri = new Uri(uriString);

            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage webResponse = new HttpResponseMessage();
            string webResponseBody = string.Empty;
            try
            {
                webResponse = await webClient.GetAsync(requestUri);
                webResponse.EnsureSuccessStatusCode();
                webResponseBody = await webResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MessageDialog exerror = new MessageDialog("Invalid Server Address");
            }

            if()

            if (tb_judgeName.Text == "")
                return;
            if (tb_serverAdd.Text == "")
                return;

            _ServerAddress = tb_serverAdd.Text;
            _JudgeID = tb_judgeName.Text;
            this.Frame.Navigate(typeof(Event));
        }
    }
}
