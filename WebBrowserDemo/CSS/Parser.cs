using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CascadingStyleSheets
{
    public class Parser
    {
        public void ParseSheet(string text)
        {
            ParseSheet(PhoenixOI.Request.StringToStream(text));
        }

        public void ParseSheet(Stream text)
        {
            throw new NotImplementedException();
        }
    
        public void ParseStyle()
        {

        }

        public void ParseInlineStyle(Style elementStyle, string style)
        {
            //break the expression into a string[] containing each line of property value
            //e.g. font-family: 'Segoe UI';
            string[] propertiesPart = style.Split(';');
            if (propertiesPart.Length > 0)
            {
                for (int i = 0; i < propertiesPart.Length; i++)
                {
                    //break the expression into a string[] containing attribute values
                    //e.g. font-family and 'Segoe UI'
                    string[] attributes = propertiesPart[i].Split(':');
                    if (attributes.Length == 2)
                    {
                        elementStyle[attributes[0].Trim()] = attributes[1].Trim().TrimEnd(';');
                    }
                }
            }
        }

    }
}
