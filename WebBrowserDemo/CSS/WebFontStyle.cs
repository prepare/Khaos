using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace CascadingStyleSheets
{
    public class WebFontStyle : IStyleProperty<FontStyle>
    {
        private FontStyle style;
        private string styleValue;

        public WebFontStyle()
        {
            RealValue = FontStyles.Normal;
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

                FontStyleConverter fsc = new FontStyleConverter();

                style = (FontStyle)fsc.ConvertFrom(value);
                styleValue = value;
            }
        }

        public FontStyle RealValue
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                FontStyleConverter fsc = new FontStyleConverter();
                styleValue = (string)fsc.ConvertTo(style, typeof(string));
            }
        }
    }
}
