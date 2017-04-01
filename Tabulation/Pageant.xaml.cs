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
using Newtonsoft.Json;

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
            initCriteriaBox();
            CandidateList.ItemsSource = dataList;
        }

        public async void initCriteriaBox()
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

            Categs_Candids_Container ccc = JsonConvert.DeserializeObject<Categs_Candids_Container>(webResponseBody);
            
            // initialize category
            ObservableCollection<Categ> categ = new ObservableCollection<Categ>(ccc.categs_candids[0].categs);

            critiaList.ItemsSource = categ;

            //TODO: remove this thing
            foreach (Categ categx in ccc.categs_candids[0].categs)
            {
                Debug.WriteLine("ID:{0} Name:{1}", categx.id, categx.name);
            }

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

    }


    public class Categs_Candids_Container
    {
        public Categs_Candids[] categs_candids { get; set; }
    }

    public class Categs_Candids
    {
        public Categ[] categs { get; set; }
        public Candid[] candids { get; set; }
    }

    public class Categ
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Candid
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string college { get; set; }
        public string gender { get; set; }
        public string event_ID { get; set; }
    }


}
