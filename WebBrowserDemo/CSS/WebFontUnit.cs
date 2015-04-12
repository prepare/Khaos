using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CascadingStyleSheets
{
    public class WebFontUnit : IStyleProperty<Unit>
    {
        private Unit wpfValue = new Unit(12, UnitType.Pixel);
        private string webValue = "12px";

        public WebFontUnit()
        {
            wpfValue = new Unit(12, UnitType.Pixel);
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

                    if (wpfValue.Value < 0)
                        wpfValue = new Unit(12, UnitType.Pixel);
                }
                catch (Exception)
                {
                    Logger.RecordWarning("invalid length");
                    wpfValue = new Unit(12, UnitType.Pixel);
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
