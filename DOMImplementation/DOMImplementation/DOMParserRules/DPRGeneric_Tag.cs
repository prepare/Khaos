using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore.DOMParserRules
{
    public class DPRGeneric_Tag : DOMParserRule
    {
        new public static string Name = "generic_tag";

        public DPRGeneric_Tag(DOMParser p)
            : base(p)
        {
        }

        public override int ExecuteRule()
        {
            return Tag();
        }
    }
}
