using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderEngine
{
    public class VisualNoScript : VisualNode
    {
        public VisualNoScript(VisualNode partent)
            : base(partent)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
