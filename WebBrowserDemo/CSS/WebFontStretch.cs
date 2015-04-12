using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace CascadingStyleSheets
{
    public class WebFontStretch : IStyleProperty<FontStretch>
    {
        private FontStretch stretch;
        private string styleValue;

        public WebFontStretch()
        {
            RealValue = FontStretches.Normal;
        }

        public string StyleValue
        {
            get
            {
                return styleValue;
            }
            set
            {
                value = value.Trim();

                FontStretchConverter fsc = new FontStretchConverter();

                stretch = (FontStretch)fsc.ConvertFrom(value);
                styleValue = value;
            }
        }

        public FontStretch RealValue
        {
            get
            {
                return stretch;
            }
            set
            {
                stretch = value;

                FontStretchConverter fsc = new FontStretchConverter();
                styleValue = (string)fsc.ConvertTo(stretch, typeof(string));
            }
        }
    }
}
