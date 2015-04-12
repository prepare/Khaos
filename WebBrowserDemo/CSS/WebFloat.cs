using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebFloat : WebEnumInheriting<FloatEnum>
    {
        public WebFloat()
            : base(FloatEnum.Inherit)
        { }
    }
}
