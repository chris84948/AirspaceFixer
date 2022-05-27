using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AirspaceFixerSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += WebBrowserView_Loaded;
        }

        private void WebBrowserView_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.Navigate("http://www.google.com");
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (Browser != null && Browser.CanGoForward)
                Browser.GoForward();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Browser != null && Browser.CanGoBack)
                Browser.GoBack();
        }

        private void Browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            txtUrl.Text = e.Uri.OriginalString;
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            Browser.Navigate(txtUrl.Text);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            airspacePanel.FixAirspace = true;
            gridDialog.Visibility = Visibility.Visible;
        }

        private void btnCloseDialog_Click(object sender, RoutedEventArgs e)
        {
            airspacePanel.FixAirspace = false;
            gridDialog.Visibility = Visibility.Hidden;
        }
    }
}
