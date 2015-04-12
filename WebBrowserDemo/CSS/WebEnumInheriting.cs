using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebEnumInheriting<T> : IStyleProperty<T>, IStyleInherits
    {
        //private const int Inherit = 0;
        private string webValue = null;
        private T wpfValue;
        private T inherit;

        public WebEnumInheriting(T inherit)
        {
            this.wpfValue = inherit;
            this.inherit = inherit;
            this.webValue = Enum.GetName(typeof(T), inherit);
        }

        public bool IsInherit
        {
            get { return wpfValue.ToString() == inherit.ToString(); }
        }

        public string StyleValue
        {
            get
            {
                if (webValue == null)
                {
                    return Enum.GetName(typeof(T), wpfValue);
                }
                else
                {
                    return webValue;
                }
            }
            set
            {
                try
                {
                    if (value != null && value != "")
                    {
                        wpfValue = (T)Enum.Parse(typeof(T), OnlyAlpha(value), true);
                        webValue = value;
                    }
                    else
                    {
                        Logger.RecordWarning("Empty string in Web Enum");
                    }
                }
                catch (Exception ex)
                {
                    Logger.RecordWarning(ex.Message + " [WebEnum.cs, 43]");
                }
            }
        }

        public T RealValue
        {
            get
            {
                return wpfValue;
            }
            set
            {
                wpfValue = value;
                webValue = Enum.GetName(typeof(T), value);
            }
        }

        private static string OnlyAlpha(string value)
        {
            StringBuilder builder = new StringBuilder();
            for (int charIndex = 0; charIndex < value.Length; charIndex++)
            {
                if (char.IsLetter(value[charIndex]))
                    builder.Append(  value[charIndex] );
            }
            return builder.ToString();
        }

    }
}
