using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderEngine
{
    public class VisualSpan : VisualNode
    {
        public VisualSpan(VisualNode parentNode)
            : base(parentNode)
        {
            CssStyle.WebDisplay.RealValue = CascadingStyleSheets.DisplayEnum.Inline;
        }
    }
}
