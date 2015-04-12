
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CascadingStyleSheets
{
    public class WebImageLoader : IStyleProperty<string>
    {
        private const string noneString = "none";

        private string url = null;
        private bool isNone = true;
        private BitmapImage image;
        private bool imageLoaded = false;
        private PhoenixOI.Request request;

        public event EventHandler ImageChanged;

        public WebImageLoader(PhoenixOI.Request request)
        {
            this.request = request;
        }

        public string StyleValue
        {
            set
            {
                value = value.Trim();

                if (value != null && value != "")
                {

                    string lowered = value.ToLower();

                    // Check for tansparent
                    if (lowered == noneString)
                    {
                        isNone = true;
                    }
                    else // Check for Url
                    {
                        url = value;

                        image = null;
                        isNone = false;

                        LoadImage();
                    }
                }
            }
            get
            {
                return url;
            }
        }

        public string RealValue
        {
            get { return url; }
            set
            {
                if (value == null)
                {
                    isNone = true;
                }
                else
                {
                    url = value;

                    isNone = false;

                    image = null;

                    LoadImage();
                }
            }
        }

        public void LoadImage(Uri uri)
        {
            imageLoaded = false;

            if (image == null)
            {
                image = new BitmapImage();

                image.BeginInit();
                image.DownloadCompleted += new EventHandler(Image_Changed);
                image.Changed += new EventHandler(Image_Changed);
                image.UriSource = uri;
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.EndInit();
            }
        }

        public ImageSource Image
        {
            get
            {
                if (isNone) return null;
                return image;
            }
        }

        private void LoadImage()
        {
            imageLoaded = false;

            //var request = new PhoenixOI.Request();
            //request.CreateWebRequest(url);
            //request.GetResponse();
            //request.Response.Close();
            //bitmap.UriSource = new Uri(url);

            Uri imageAddress = null;

            try
            {
                imageAddress = new Uri(url, UriKind.RelativeOrAbsolute);
                if (!imageAddress.IsAbsoluteUri)
                    imageAddress = request.ResolveAddress(url);
            }
            catch (ArgumentException ex)
            {
                Logger.RecordWarning(ex.Message);
            }
            catch (UriFormatException ex)
            {
                Logger.RecordWarning(ex.Message);
            }

            if (image == null && imageAddress != null)
            {
                image = new BitmapImage();

                image.BeginInit();
                image.DownloadCompleted += new EventHandler(Image_Changed);
                image.Changed += new EventHandler(Image_Changed);

                image.UriSource = imageAddress;
                //bitmap.StreamSource = request.Response.GetResponseStream();
                //image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;

                image.EndInit();
                //bitmap.Freeze();
            }
        }

        private void Image_Changed(object sender, EventArgs e)
        {
            imageLoaded = true;

            if (ImageChanged != null)
                ImageChanged(sender, e);

        }

        public bool ImageLoaded
        {
            get { return imageLoaded; }
        }

        public bool IsNone
        {
            get { return isNone; }
        }
    }
}
