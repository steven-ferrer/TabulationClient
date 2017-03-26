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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tabulation
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Event : Page
    {
        private static string _choiceId;
        public static string choiceID
        {
            get { return _choiceId; }
            set { _choiceId = value; }
        }
        private ObservableCollection<EventListItem> dataList = new ObservableCollection<EventListItem>();
        public Event()
        {
            this.InitializeComponent();
            initEventList();
            EventList.ItemsSource = dataList;
            
        }

        private void initEventList()
        {
            //TODO: make this fetch from the REST services
            dataList.Add( new EventListItem() { EventID = "1", EventName = "CheerLeading"} );
            dataList.Add( new EventListItem() { EventID = "2", EventName = "Mr. And Miss Intrams"} );
        }

        public void ItemIsClicked(object sender, ItemClickEventArgs e )
        {
            EventListItem item = (EventListItem) e.ClickedItem;
            choiceID = item.EventID;
            if (choiceID == "1")
            {
                this.Frame.Navigate(typeof(CheerLeading));
            }
            else if (choiceID == "2")
            {
                this.Frame.Navigate(typeof(Pageant));
            }
            
        }


    }

    public class EventListItem
    {
        public string EventName { get; set; }
        public string EventID { get; set; }
    }
}
