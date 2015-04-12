using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_noscript : ITransformable
    {
        private const string tagName = "noscript";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_noscript.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement brElement = element as IHTMLElement;

            VisualNoScript node = null;
            if (brElement != null)
            {
                node = new VisualNoScript(parent);
                node.Element = brElement;

            }
            return node;
        }

        #endregion
    }
}
