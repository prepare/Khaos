using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebBackgroundRepeat : WebEnumInheriting<BackgroundRepeatEnum>
    {
        public WebBackgroundRepeat()
            : base(BackgroundRepeatEnum.Inherit)
        { }
    }
}
