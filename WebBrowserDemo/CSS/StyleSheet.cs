using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class StyleSheet
    {
        private string path;
        private string name;

        public Dictionary<string, Style> styles = new Dictionary<string,Style>();

        public Style this[string s]
        {
            get
            {
                if (styles.ContainsKey(s))
                    return styles[s];
                return null;
            }
            set
            {
                if (styles.ContainsKey(s))
                {
                    value.CascadeFrom(styles[s]);
                    styles[s] = value;
                }
                else
                    styles.Add(s, value);

            }
        }
    }
}
