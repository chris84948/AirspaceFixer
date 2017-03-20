using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirspaceFixerSample
{
    /// <summary>
    /// Interaction logic for WebBrowserView.xaml
    /// </summary>
    public partial class WebBrowserView : UserControl
    {
        public WebBrowserView()
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

        private void Browser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            txtUrl.Text = e.Uri.OriginalString;
        }

        private void txtUrl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Browser.Navigate(txtUrl.Text);
        }
    }
}
