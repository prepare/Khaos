using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public interface ITransformable
    {
        bool SupportsTag(string tagName);
        VisualNode TransformElement(IElement element, VisualNode parent);
    }
}
