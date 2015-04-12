using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class WebBackgroundAttachment : WebEnumInheriting<BackgroundAttachmentEnum>
    {
        public WebBackgroundAttachment()
            : base(BackgroundAttachmentEnum.Inherit)
        { }
    }
}
