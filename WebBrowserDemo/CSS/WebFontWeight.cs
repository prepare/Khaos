using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace CascadingStyleSheets
{
    public class WebFontWeight : IStyleProperty<FontWeight>
    {
        private FontWeight weight;
        private string styleValue;

        public WebFontWeight()
        {
            RealValue = FontWeights.Normal;
        }

        public string StyleValue
        {
            get
            {
                return styleValue;
            }
            set
            {
                if (value == "bolder")
                {
                    value = "extrabold";
                }

                value = value.Trim();

                FontWeightConverter fwc = new FontWeightConverter();

                weight = (FontWeight)fwc.ConvertFrom(value);
                styleValue = value;
            }
        }

        public FontWeight RealValue
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;

                FontWeightConverter fwc = new FontWeightConverter();
                styleValue = (string)fwc.ConvertTo(weight, typeof(string));
            }
        }
    }
}
