using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebClear : WebEnumInheriting<ClearingEnum>
    {
        public WebClear()
            : base(ClearingEnum.Inherit)
        { }
    }
}
