using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore.DOMParserRules
{
    public class DPRHr_Tag : DOMParserRule
    {
        new public static string Name = "<hr/>";

        public DPRHr_Tag(DOMParser p)
            : base(p)
        {
        }

        public override HTMLElement CreateElement(TagInfo tag)
        {
            //create an HTMLElement
            HTMLElement e = null;
            if (tag.Name == "hr/" || tag.Name == "hr")
                e = new HTMLHRElement(tag, parser.doc);
            else
                e = new HTMLElement(tag, parser.doc);

            return e;
        }

        public override int ExecuteRule()
        {
            return Rule("<hr/>");
        }
    }
}
