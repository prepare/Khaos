using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace CascadingStyleSheets
{
    public class WebBorderWidth : IStyleProperty<int>, IComparable<WebBorderWidth>
    {
        private int wpfValue = 0;
        private string webValue = "0px";

        public string StyleValue
        {
            set
            {
                value = value.Trim();
                string lowered = value.ToLower();

                if (lowered == "thin")
                {
                    webValue = value;
                    wpfValue = 1;
                }
                else if (lowered == "medium")
                {
                    webValue = value;
                    wpfValue = 5;
                }
                else if (lowered == "thick")
                {
                    webValue = value;
                    wpfValue = 10;
                }
                else
                {
                    int point = lowered.LastIndexOf("px");
                    if (point != -1)
                    {
                        lowered = lowered.Remove(point);
                    }

                    if (int.TryParse(lowered, out wpfValue))
                    {
                        webValue =  wpfValue + "px";
                    }
                    else
                    {
                        Logger.RecordWarning("invalid length in Web Border Width.cs: " + lowered);
                    }
                }
            }
            get
            { 
                return webValue;
            }
        }

        public int RealValue
        {
            get 
            { 
                return wpfValue; 
            }
            set 
            {
                wpfValue = value;
                webValue = wpfValue + "px";
            }
        }

        #region IComparable<WebBorderWidth> Members

        public int CompareTo(WebBorderWidth other)
        {
            return wpfValue.CompareTo(other.RealValue);
        }

        #endregion
    }
}
