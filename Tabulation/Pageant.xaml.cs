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

        public void initCandidates()
        {
            dataList.Add(new CandidateModel() { ID = "1", college = "CCS", name = "Eden E. Fernandez Jr." });
            dataList.Add(new CandidateModel() { ID = "1", college = "CBA", name = "GreenTigers" });
            dataList.Add(new CandidateModel() { ID = "1", college = "CoEng", name = "MaroonSharks" });
        }

        public void vote_submit(object sender, RoutedEventArgs e)
        {

        }
    }
}
