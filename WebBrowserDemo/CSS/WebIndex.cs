using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CascadingStyleSheets
{
    public class WebIndex : IStyleProperty<int>
    {
        private int wpfValue = -1;
        private string webValue = "auto";
        bool isAuto = true;

        public string StyleValue
        {
            set
            {
                value = value.Trim();
                string lowered = value.ToLower();

                if (lowered == "auto")
                {
                    isAuto = true;
                }
                else
                {
                    try
                    {
                        wpfValue = int.Parse(value);

                        webValue = value;
                        isAuto = false;
                    }
                    catch (Exception ex)
                    {
                        Logger.RecordError(ex);
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
                webValue = wpfValue.ToString();
                isAuto = false;
            }
        }

        public bool IsAuto
        {
            get { return isAuto; }
        }
    }
}
