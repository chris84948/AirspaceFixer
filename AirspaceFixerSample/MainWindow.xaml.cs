using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
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
