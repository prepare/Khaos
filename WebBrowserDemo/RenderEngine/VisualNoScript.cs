using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderEngine
{
    public class VisualNoFrames : VisualNode
    {
        public VisualNoFrames(VisualNode partent)
            : base(partent)
        {
            if (!PhoenixSettings.Phoenix.UserAgentContext.ScriptingEnabled)
                this.Visibility = System.Windows.Visibility.Hidden; 
        }
    }
}
