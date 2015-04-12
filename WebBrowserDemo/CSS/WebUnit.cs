using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CascadingStyleSheets
{
    public class WebUnit : IStyleProperty<Unit>
    {
        private Unit wpfValue = new Unit(0, UnitType.Percentage);
        private string webValue = "0px";

        public WebUnit()
        {
            wpfValue = new Unit(0, UnitType.Percentage);
        }

        public string StyleValue
        {
            set
            {
                try
                {
                    value = value.Trim();
                    wpfValue = Unit.Parse(value);
                    webValue = value;
                }
                catch (Exception)
                {
                    Logger.RecordError("invalid length");
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
            }
        }
    }
}
