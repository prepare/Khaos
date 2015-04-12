using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebBackgroundPosition : WebEnumInheriting<BackgroundPositionEnum>
    {
        public WebBackgroundPosition()
            : base(BackgroundPositionEnum.Inherit)
        { }
    }
}
