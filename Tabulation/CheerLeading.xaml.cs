using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
        List<TextBox> boxes = new List<TextBox>();
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
            string uriString = string.Format("http://{0}/tabulation/fetch/1", MainPage._ServerAddress);
            
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
                webResponseBody = string.Format("Error: {0} Message: {1}", ex.HResult.ToString("X"), ex.Message);
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

        public async void vote_submit(object sender, RoutedEventArgs e)
        {
            if(CandidateList.SelectedItem == null)
            {
                MessageDialog error = new MessageDialog("Please Select a Candidate");
                await error.ShowAsync();
            }

            if( ! allFilled())
            {
                MessageDialog error = new MessageDialog("All Fields are Required. Check For Empty Fields");
                await error.ShowAsync();
                return;
            }

            Candid selected = CandidateList.SelectedItem as Candid;
            string candid_id = selected.ID;

            try
            {
                int cheer_total = 0;
                cheer_total += Int32.Parse(cheer_vc.Text);
                cheer_total += Int32.Parse(cheer_m.Text);
                cheer_total += Int32.Parse(cheer_ss.Text);
                cheer_total += Int32.Parse(cheer_ce.Text);
                cheer_total += Int32.Parse(cheer_oe.Text);

                int tumbling_total = 0;
                tumbling_total += Int32.Parse(st_diff.Text);
                tumbling_total += Int32.Parse(st_exec.Text);
                tumbling_total += Int32.Parse(ru_diff.Text);
                tumbling_total += Int32.Parse(ru_exec.Text);
                tumbling_total += Int32.Parse(sy_exec_diff.Text);
                tumbling_total += Int32.Parse(ju_diff.Text);
                tumbling_total += Int32.Parse(ju_exec.Text);
                tumbling_total += Int32.Parse(tumb_oe_exec_diff.Text);

                int building_total = 0;
                building_total += Int32.Parse(stunts_diff.Text);
                building_total += Int32.Parse(stunts_exec.Text);
                building_total += Int32.Parse(py_diff.Text);
                building_total += Int32.Parse(py_exec.Text);
                building_total += Int32.Parse(to_diff.Text);
                building_total += Int32.Parse(to_exec.Text);
                building_total += Int32.Parse(build_oe_exec_diff.Text);

                int dance_total = 0;
                dance_total += Int32.Parse(dance_diff.Text);
                dance_total += Int32.Parse(dance_exec.Text);
                dance_total += Int32.Parse(form_diff.Text);
                dance_total += Int32.Parse(form_exec.Text);
                dance_total += Int32.Parse(dance_oe_exec_diff.Text);

                //Create HTTP client object
                HttpClient webClient = new HttpClient();

                //user-agent header to the GET request
                var headers = webClient.DefaultRequestHeaders;
                //string header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

                //assign URI string for cheer
                string uriString = string.Format("http://{0}/tabulation/commit_vote/{1}/{2}/5/{3}",
                                                MainPage._ServerAddress, MainPage._JudgeID, candid_id, cheer_total);
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
                    webResponseBody = string.Format("Error: {0} Message: {1}", 
                                                    ex.HResult.ToString("X"), ex.Message);
                }

                if (!(webResponseBody == "ok"))
                {
                    return;
                }

                uriString = string.Format("http://{0}/tabulation/commit_vote/{1}/{2}/6/{3}",
                                          MainPage._ServerAddress, MainPage._JudgeID, candid_id, dance_total);

                requestUri = new Uri(uriString);
                webResponseBody = string.Empty;
                try
                {
                    webResponse = await webClient.GetAsync(requestUri);
                    webResponse.EnsureSuccessStatusCode();
                    webResponseBody = await webResponse.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    webResponseBody = string.Format("Error: {0} Message: {1}", 
                                            ex.HResult.ToString("X"), ex.Message);
                }

                if (!(webResponseBody == "ok"))
                {
                    return;
                }


                uriString = string.Format("http://{0}/tabulation/commit_vote/{1}/{2}/7/{3}",
                                            MainPage._ServerAddress, MainPage._JudgeID, candid_id, building_total);

                requestUri = new Uri(uriString);
                webResponseBody = string.Empty;
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

                if (!(webResponseBody == "ok"))
                {
                    return;
                }

                uriString = string.Format("http://{0}/tabulation/commit_vote/{1}/{2}/10/{3}",
                                        MainPage._ServerAddress, MainPage._JudgeID, candid_id, tumbling_total);

                requestUri = new Uri(uriString);
                webResponseBody = string.Empty;
                try
                {
                    webResponse = await webClient.GetAsync(requestUri);
                    webResponse.EnsureSuccessStatusCode();
                    webResponseBody = await webResponse.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    webResponseBody = string.Format("Error: {0} Message: {1}", 
                                                ex.HResult.ToString("X"), ex.Message);
                }

                if (!(webResponseBody == "ok"))
                {
                    return;
                }

            }
            catch(Exception exx)
            {
                Debug.WriteLine(exx.Message);
            }

            
            MessageDialog finish = new MessageDialog("Voting Done");
            await finish.ShowAsync();

            foreach (TextBox box in boxes)
            {
                box.Text = string.Empty;
            }
        }

        private bool allFilled()
        {
            boxes.Add(cheer_vc);
            boxes.Add(cheer_m);
            boxes.Add(cheer_ss);
            boxes.Add(cheer_ce);
            boxes.Add(cheer_oe);
            boxes.Add(st_diff);
            boxes.Add(st_exec);
            boxes.Add(ru_diff);
            boxes.Add(ru_exec);
            boxes.Add(sy_exec_diff);
            boxes.Add(ju_diff);
            boxes.Add(ju_exec);
            boxes.Add(tumb_oe_exec_diff);
            boxes.Add(stunts_diff);
            boxes.Add(stunts_exec);
            boxes.Add(py_exec);
            boxes.Add(py_diff);
            boxes.Add(to_exec);
            boxes.Add(to_diff);
            boxes.Add(build_oe_exec_diff);
            boxes.Add(dance_diff);
            boxes.Add(dance_exec);
            boxes.Add(form_diff);
            boxes.Add(form_exec);
            boxes.Add(dance_oe_exec_diff);

            if(boxes.Any(box => box.Text == ""))
            {
                
                return false;
            }
            return true;
        }
    }
}
