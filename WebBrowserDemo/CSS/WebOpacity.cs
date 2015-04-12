using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CascadingStyleSheets
{
    public class WebOpacity : IStyleProperty<byte>
    {
        private byte wpfValue = 255;
        private string webValue = "1";

        public string StyleValue
        {
            set
            {
                value = value.Trim();
                try
                {
                    float percent = 0f;
                    if (float.TryParse(value, out percent))
                    {
                        float clamped = (percent > 1) ? 1 : ((percent < 0) ? 0 : percent);
                        wpfValue = (byte)(clamped * 255);
                        webValue = value;
                    }
                }
                catch (Exception ex)
                {
                    Logger.RecordError(ex);
                }
            }
            get
            { 
                return webValue;
            }
        }

        public byte RealValue
        {
            get 
            { 
                return wpfValue; 
            }
            set 
            {
                wpfValue = value;
                webValue = ((float)value / 255f).ToString(); 
            }
        }
    }
}
