using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebTextDecoration : WebEnumInheriting<TextDecorationEnum>
    {
        public WebTextDecoration()
            : base(TextDecorationEnum.Inherit)
        { }
    }
}
