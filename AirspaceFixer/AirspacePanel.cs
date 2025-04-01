using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private Image airspaceScreenshot;
        private ContentControl airspaceContent;

        static AirspacePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AirspacePanel), new FrameworkPropertyMetadata(typeof(AirspacePanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            airspaceContent = GetTemplateChild("PART_AirspaceContent") as ContentControl;
            airspaceScreenshot = GetTemplateChild("PART_AirspaceScreenshot") as Image;
        }

        private void AirspacePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // On resize, make sure to update the image
            if (FixAirspace)
                FixAirspaceShared(true);
        }

        private static void OnFixAirspaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AirspacePanel panel)
                panel.FixAirspaceShared((bool)e.NewValue);
        }

        private void FixAirspaceShared(bool fix)
        {
            if (ActualWidth == 0 || ActualHeight == 0 || PresentationSource.FromVisual(this) == null)
                return;

            if (fix)
            {
                CreateScreenshotFromContent();
                airspaceContent.Visibility = Visibility.Hidden;
                airspaceScreenshot.Visibility = Visibility.Visible;
            }
            else
            {
                airspaceContent.Visibility = Visibility.Visible;
                airspaceScreenshot.Visibility = Visibility.Hidden;
                airspaceScreenshot.Source = null;
            }
        }

        private void CreateScreenshotFromContent()
        {
            // https://stackoverflow.com/questions/1918877/how-can-i-get-the-dpi-in-wpf
            double scalingFactor = PresentationSource.FromVisual(this)?.CompositionTarget?.TransformToDevice.M11 ?? 1.0;

            Point upperLeftPoint = airspaceContent.PointToScreen(new Point(0, 0));
            var bounds = new System.Drawing.Rectangle((int)(upperLeftPoint.X * scalingFactor),
                                                      (int)(upperLeftPoint.Y * scalingFactor),
                                                      (int)(airspaceContent.RenderSize.Width * scalingFactor),
                                                      (int)(airspaceContent.RenderSize.Height * scalingFactor));

            if (bounds.Width == 0 || bounds.Height == 0)
                return;

            using (var bitmap = new System.Drawing.Bitmap((int)bounds.Width, (int)bounds.Height))
            {
                using (var g = System.Drawing.Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top),
                                     System.Drawing.Point.Empty,
                                     new System.Drawing.Size((int)bounds.Width, (int)bounds.Height));
                }

                airspaceScreenshot.Source = GetImageSourceFromBitmap(bitmap);
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