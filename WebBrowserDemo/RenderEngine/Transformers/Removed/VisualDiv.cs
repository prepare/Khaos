using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderEngine
{
    public class VisualDiv : VisualNode
    {
        public VisualDiv(VisualNode parentNode)
            : base(parentNode)
        {
            CssStyle.WebDisplay.RealValue = CascadingStyleSheets.DisplayEnum.Block;
        }
    }
}
