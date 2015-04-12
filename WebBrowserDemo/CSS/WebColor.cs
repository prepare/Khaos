using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Diagnostics;

namespace CascadingStyleSheets
{
    public class WebColor : IStyleProperty<SolidColorBrush>, IStyleInherits, IComparable<WebColor>
    {
        private const string transString = "transparent";
        private const string inheritString = "inherit";

        private SolidColorBrush wpfValue = null;
        private string webValue = transString;
        private bool isTrans = true;
        private bool isInherit = false;

        public string StyleValue
        {
            set
            {
                value = value.Trim();
                string lowered = value.ToLower();

                // Check for inherit
                if (lowered == inheritString)
                {
                    isInherit = true;
                    isTrans = false;
                }
                // Check for tansparent
                else if (lowered == transString)
                {
                    isInherit = false;
                    isTrans = true;
                }
                else // Check for Color
                {
                    try
                    {
                        BrushConverter bc = new BrushConverter();
                        SolidColorBrush temp = (SolidColorBrush)bc.ConvertFrom(value);

                        // Convertion Worked..
                        webValue = value;
                        wpfValue = temp;

                        isInherit = false;
                        isTrans = false;
                    }
                    catch (NotSupportedException ex)
                    {
                        Logger.RecordError(ex);
                    }
                    catch (Exception ex)
                    {
                        Logger.RecordWarning(ex.Message);
                    }
                }
            }
            get
            { 
                return webValue;
            }
        }

        public SolidColorBrush RealValue
        {
            get 
            {
                return wpfValue;
            }
            set
            {
                wpfValue = value;

                // To get rid of alpha value
                webValue = string.Format("#{0}", value.ToString().Substring(3));

                isInherit = false;
                isTrans = false;
            }
        }

        public bool IsTransparent
        { 
            get { return isTrans; }
            set
            {
                isInherit = value;
                wpfValue = null;
                webValue = transString;
            }
        }

        public bool IsInherit
        {
            get { return isInherit; }
            set 
            {
                isInherit = value;
                wpfValue = null;
                webValue = inheritString;
            }
        }

        #region IComparable<WebColor> Members

        public int CompareTo(WebColor other)
        {
            if (this.isTrans == other.isTrans &&
                this.isInherit == other.isInherit &&
                this.webValue == other.webValue)
                return 0;
            else
                return -1;
        }

        #endregion
    }
}
