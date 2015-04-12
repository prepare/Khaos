using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderEngine
{
    public class VisualBreak : VisualNode
    {
        public VisualBreak(VisualNode parentNode)
            : base(parentNode)
        {
            CssStyle.WebHeight.RealValue = new CascadingStyleSheets.Unit(12, CascadingStyleSheets.UnitType.Pixel);
            CssStyle.WebDisplay.RealValue = CascadingStyleSheets.DisplayEnum.Inline;
            CssStyle.WebLineBreak.RealValue = CascadingStyleSheets.LineBreakEnum.Normal;
        }
    }
}
