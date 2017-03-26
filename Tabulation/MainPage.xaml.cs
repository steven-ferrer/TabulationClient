using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tabulation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
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

        public void setServerAddress(object sender, RoutedEventArgs e)
        {
            _ServerAddress = tb_serverAdd.Text;
            this.Frame.Navigate(typeof(Event));
        }
    }
}
