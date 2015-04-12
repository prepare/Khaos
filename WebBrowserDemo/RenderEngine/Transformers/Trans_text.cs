using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_text
    {

        public VisualNode TransformText(DOMCore.IText element, VisualNode parent)
        {
            VisualText node = new VisualText(parent);
            node.Text = element.Data;

            return node;
        }
    }
}
