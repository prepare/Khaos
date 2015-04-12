using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CascadingStyleSheets;
using DOMCore;
using PhoenixOI;

namespace CSS
{
    public class CSSManager
    {
        HTMLDocument doc;
        Dictionary<string, Style> elementStyles = new Dictionary<string, Style>();
        StyleSheet defaultStyleSheet = new StyleSheet();

        public CSSManager(HTMLDocument doc, Request request)
        {
            this.doc = doc;
            defaultStyleSheet = CSSParser.Parse(DefaultStyleSheet.styletext, request);
        }

        public Style GetStyleFor(IHTMLElement tag)
        {
            List<StyleSheet> styleSheets = new List<StyleSheet>();
            List<Style> lst = new List<Style>();
            Dictionary<string, Style> matchingStyles = new Dictionary<string, Style>();

            

            //get all stylesheets from current webpage
            styleSheets.Add(defaultStyleSheet);
            NodeList nl = doc.getElementsByTagName("style");
            for (int i = 0; i < nl.Length; i++)
            {
                StyleSheet sSheet = ((IHTMLStyleElement)nl[i]).StyleSheet;
                if (sSheet != null) styleSheets.Add(sSheet);
            }

            //find applying styles in each stylesheet
            for (int i = 0; i < styleSheets.Count; i++)
            foreach (KeyValuePair<string, Style> kvp in styleSheets[i].styles)
            {
                //style applies to tag
                if (StyleSelector.StyleMatchesTag(kvp.Value, tag))
                {
                    //if style with same selector exist
                    if (matchingStyles.ContainsKey(kvp.Key))
                    { 
                        //inherit from old one and overwrite in list
                        kvp.Value.CascadeFrom(matchingStyles[kvp.Key]);
                        matchingStyles[kvp.Key] = kvp.Value.Clone();
                    }
                    else
                    {
                        matchingStyles.Add(kvp.Key, kvp.Value.Clone());
                    }
                }
            }

            lst.AddRange(matchingStyles.Values);

            //if no style applying to tag was found, use default style
            if (lst.Count == 0)
                lst.Add(new Style());

            //if multiple styles apply to tag
            if (lst.Count > 1)
            {
                //inherit properties according to importance of styles
                lst.Sort(delegate(Style s1, Style s2) { return StyleSelector.CompareTo(s1,s2); });
                for (int i = 1; i < lst.Count; i++)
                {
                    lst[i - 1].CascadeFrom(lst[i]);
                }
            }

            return lst[0];
        }
    }
}
