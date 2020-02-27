using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PoeTradeDesktop.UI.Custom
{
    public class ImageBox : System.Windows.Controls.Image
    {
        public string CacheSource
        {
            get { return (string)GetValue(CacheSourceProperty); }
            set { SetValue(CacheSourceProperty, value); }
        }

        public ImageBox() : base()
        {
            Loaded += ImageBoxLoaded;
        }

        private void ImageBoxLoaded(object sender, RoutedEventArgs e)
        {
            string url = CacheSource;
            string imageFileName = url.Substring(url.IndexOf("v=") + 2);
            string path = Path.Combine(Environment.CurrentDirectory, $@"ImageCache\{imageFileName}.png");

            if (!File.Exists(path))
            {
                /*
                using (WebClient wc = new WebClient())
                {
                    using (Stream s = wc.OpenRead(url))
                    {
                        using (Bitmap bmp = new Bitmap(s))
                        {
                            bmp.Save(path);
                        }
                    }
                }*/

                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                Bitmap bmp = new Bitmap(responseStream);
                bmp.Save(path);
            }

            BitmapImage image = new BitmapImage(new Uri(path, UriKind.Absolute));
            Source = image;
        }

        public static readonly DependencyProperty CacheSourceProperty = DependencyProperty.Register("CacheSource", typeof(string), typeof(ImageBox));

    }
}
