using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_noframes : ITransformable
    {
        private const string tagName = "noframes";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_noframes.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement brElement = element as IHTMLElement;

            VisualNoFrames node = null;
            if (brElement != null)
            {
                node = new VisualNoFrames(parent);
                node.Element = brElement;
            }
            return node;
        }

        #endregion
    }
}
