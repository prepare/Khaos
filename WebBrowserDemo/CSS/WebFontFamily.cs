using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CascadingStyleSheets
{
    public class WebFontFamily : IStyleProperty<FontFamily>
    {
        private FontFamily family;
        private string styleValue;

        public WebFontFamily()
        {
            RealValue = new FontFamily("Times New Roman");
        }

        public string StyleValue
        {
            get
            {
                if (styleValue == null) styleValue = family.Source;
                return styleValue;
            }
            set
            {
                value = value.Trim();

                family = new FontFamily(value);
                styleValue = value;
            }
        }

        public FontFamily RealValue
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
                styleValue = family.Source;
            }
        }
    }
}
