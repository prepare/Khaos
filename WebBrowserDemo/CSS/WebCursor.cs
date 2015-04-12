using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebCursor : WebEnumInheriting<CursorEnum>
    {
        public WebCursor()
            : base(CursorEnum.Inherit)
        { }

        public bool ForText { get; set; }
    }
}
