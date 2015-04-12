using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CascadingStyleSheets
{
    public class WebLength : IStyleProperty<Unit>
    {
        private Unit wpfValue = null;
        private string webValue = "auto";
        bool isAuto = true;

        public string StyleValue
        {
            set
            {
                if (value.StartsWith(".")) value = '0' + value;

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
                        wpfValue = Unit.Parse(value);

                        webValue = value;
                        isAuto = false;
                    }
                    catch (Exception)
                    {
                        Logger.RecordError("invalid length " + value);
                    }
                }
            }
            get
            { 
                return webValue;
            }
        }

        public Unit RealValue
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
