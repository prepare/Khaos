using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore.DOMParserRules
{
    public class DPRText : DOMParserRule
    {
        new public static string Name = "text";

        public DPRText(DOMParser p)
            : base(p)
        {
        }

        public override int ExecuteRule()
        {
            return PlainText();
        }
    }
}
