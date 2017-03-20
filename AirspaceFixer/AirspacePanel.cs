using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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

namespace AirspaceFixer
{
    public class AirspacePanel : ContentControl
    {
        public static readonly DependencyProperty FixAirspaceProperty =
            DependencyProperty.Register("FixAirspace",
                                        typeof(bool),
                                        typeof(AirspacePanel),
                                        new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnFixAirspaceChanged)));

        public bool FixAirspace
        {
            get { return (bool)GetValue(FixAirspaceProperty); }
            set { SetValue(FixAirspaceProperty, value); }
        }


        private Image _airspaceScreenshot;
        private ContentControl _airspaceContent;

        static AirspacePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirspacePanel), new FrameworkPropertyMetadata(typeof(AirspacePanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _airspaceContent = GetTemplateChild("PART_AirspaceContent") as ContentControl;
            _airspaceScreenshot = GetTemplateChild("PART_AirspaceScreenshot") as Image;
        }

        private static void OnFixAirspaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as AirspacePanel;

            if (panel == null || panel.ActualWidth == 0 || panel.ActualHeight == 0 || PresentationSource.FromVisual(panel) == null)
                return;

            if ((bool)e.NewValue)
            {
                panel.CreateScreenshotFromContent();
                panel._airspaceContent.Visibility = Visibility.Hidden;
                panel._airspaceScreenshot.Visibility = Visibility.Visible;
            }
            else
            {
                panel._airspaceContent.Visibility = Visibility.Visible;
                panel._airspaceScreenshot.Visibility = Visibility.Hidden;
                panel._airspaceScreenshot.Source = null;
            }
        }

        private void CreateScreenshotFromContent()
        {
            Point upperLeftPoint = _airspaceContent.PointToScreen(new Point(0, 0));
            var bounds = new System.Drawing.Rectangle((int)upperLeftPoint.X,
                                                      (int)upperLeftPoint.Y,
                                                      (int)_airspaceContent.RenderSize.Width,
                                                      (int)_airspaceContent.RenderSize.Height);

            using (var bitmap = new System.Drawing.Bitmap((int)bounds.Width, (int)bounds.Height))
            {
                using (var g = System.Drawing.Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top),
                                     System.Drawing.Point.Empty,
                                     new System.Drawing.Size((int)bounds.Width, (int)bounds.Height));
                }

                _airspaceScreenshot.Source = GetImageSourceFromBitmap(bitmap);
            }
        }

        public ImageSource GetImageSourceFromBitmap(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
