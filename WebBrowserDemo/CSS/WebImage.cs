using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CascadingStyleSheets
{
    public class WebImage : IStyleProperty<string>, IStyleInherits
    {
        private const string noneString = "none";
        private const string inheritString = "inherit";

        private string url = null;
        private string webValue = noneString;
        private bool isNone = true;
        private bool isInherit = false;
        private BitmapImage image;
        private bool imageLoaded = false;
        private PhoenixOI.Request request;

        public event EventHandler ImageChanged;

        public WebImage(PhoenixOI.Request request)
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

                    // Check for inherit
                    if (lowered == inheritString)
                    {
                        isInherit = true;
                        isNone = false;
                    }
                    // Check for tansparent
                    else if (lowered == noneString)
                    {
                        isInherit = false;
                        isNone = true;
                    }
                    else // Check for Url
                    {
                        if (value.StartsWith("url"))
                        {
                            int start = value.IndexOf('(');
                            int end = value.IndexOf(')');

                            if (start != -1)
                            {
                                if (start < end && end != -1)
                                {
                                    start++;
                                    url = value.Substring(start, end - start).Trim();
                                    webValue = value;
                                    image = null;
                                    isInherit = false;
                                    isNone = false;

                                    LoadImage();
                                }
                                else
                                {
                                    Logger.RecordWarning("')' character missing or misplaced");
                                }
                            }
                            else
                            {
                                Logger.RecordWarning("'(' missing after url");
                            }
                        }
                        else
                        {
                            Logger.RecordWarning("prefix url missing");
                        }
                    }
                }
            }
            get
            { 
                return webValue;
            }
        }


        public string RealValue
        {
            get { return url; }
            set 
            {
                if (value == null)
                {
                    isInherit = false;
                    isNone = true;
                }
                else
                {
                    url = value;
                    webValue = "url(" + value + ")";

                    isInherit = false;
                    isNone = false;

                    image = null;

                    LoadImage();
                }
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
            Uri imageAddress = null;

            try
            {
                imageAddress = new Uri(url, UriKind.RelativeOrAbsolute);
                if (!imageAddress.IsAbsoluteUri)
                    if (request != null)
                    {
                        imageAddress = request.ResolveAddress(url);
                    }
                    else
                    {
                        Logger.RecordWarning("Reques is null: WebImage.cs, 145");
                        return;
                    }
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
                image.EndInit();
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

        public bool IsInherit
        {
            get { return isInherit; }
        }
    }
}
