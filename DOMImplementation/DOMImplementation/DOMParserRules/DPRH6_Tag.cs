using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore.DOMParserRules
{
    public class DPRH6_Tag : DOMParserRule
    {
        new public static string Name = "<h6>";

        public DPRH6_Tag(DOMParser p)
            : base(p)
        {
        }

        public override HTMLElement CreateElement(TagInfo tag)
        {
            //create an HTMLElement
            HTMLElement e = null;
            if (tag.Name == "h6")
                e = new HTMLHeadingElement(tag, parser.doc);
            else
                e = new HTMLElement(tag, parser.doc);

            return e;
        }

        public override int ExecuteRule()
        {
            return Rule("<h6>");
        }
    }
}
