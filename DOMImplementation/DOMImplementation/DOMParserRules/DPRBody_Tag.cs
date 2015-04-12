using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore.DOMParserRules
{
    public class DPRBody_Tag : DOMParserRule
    {
        new public static string Name = "<body>";

        public DPRBody_Tag(DOMParser p)
            : base(p)
        {
        }

        public override HTMLElement CreateElement(TagInfo tag)
        {
            //create an HTMLElement
            HTMLElement e = null;
            if (tag.Name == "body")
                e = new HTMLBodyElement(tag, parser.doc);
            else
                e = new HTMLElement(tag, parser.doc);

            return e;
        }

        public override int ExecuteRule()
        {
            return Rule("<body>");
        }
    }
}
