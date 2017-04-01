using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tabulation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pageant : Page
    {
        private ObservableCollection<CandidateModel> dataList = new ObservableCollection<CandidateModel>();
        public Pageant()
        {
            this.InitializeComponent();
            initCandidates();
            CandidateList.ItemsSource = dataList;
        }

        public async void initCandidates()
        {
            //Create HTTP client object
            HttpClient webClient = new HttpClient();

            //user-agent header to the GET request
            var headers = webClient.DefaultRequestHeaders;
            //string header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

            //assign user-agents
            string uriString = "http://" + MainPage._ServerAddress + "/tabulation/fetch/2";
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
                webResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            Debug.WriteLine(webResponseBody);
            //Categories categs = JsonConvert.DeserializeObject<Categories>(webResponseBody);

        }

        public void vote_submit(object sender, RoutedEventArgs e)
        {

        }

    }


    public class Categories
    {
        public Category[] categs { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
