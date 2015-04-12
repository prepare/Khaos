using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore.DOMParserRules
{
    public class DPRP_Tag : DOMParserRule
    {
        new public static string Name = "<p>";

        public DPRP_Tag(DOMParser p)
            : base(p)
        {
        }

        public override HTMLElement CreateElement(TagInfo tag)
        {
            //create an HTMLElement
            HTMLElement e = null;
            if (tag.Name == "p")
                e = new HTMLParagraphElement(tag, parser.doc);
            else
                e = new HTMLElement(tag, parser.doc);

            return e;
        }

        public override int ExecuteRule()
        {
            return Rule("<p>");
        }
    }
}
