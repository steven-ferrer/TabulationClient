using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tabulation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CheerLeading : Page
    {
        public CheerLeading()
        {
            this.InitializeComponent();
            initCandid();
        }

        public async void initCandid()
        {
            //Create HTTP client object
            HttpClient webClient = new HttpClient();

            //user-agent header to the GET request
            var headers = webClient.DefaultRequestHeaders;
            //string header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

            //assign user-agents
            string uriString = "http://" + MainPage._ServerAddress + "/tabulation/fetch/1";
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

            Categs_Candids_Container ccc = JsonConvert.DeserializeObject<Categs_Candids_Container>(webResponseBody);

            //initialize Candidates
            ObservableCollection<Candid> candid = new ObservableCollection<Candid>(ccc.categs_candids[1].candids);

            CandidateList.ItemsSource = candid;

            //TODO: remove this thing
            foreach (Candid candidx in ccc.categs_candids[1].candids)
            {
                Debug.WriteLine("ID:{0} Name:{1}", candidx.ID, candidx.name);
            }
        }

        public void vote_submit(object sender, RoutedEventArgs e)
        {

        }

        private bool allFilled()
        {
            

            return true;
        }
    }
}
