using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_span : ITransformable
    {
        private const string tagName = "span";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_span.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement spanElement = element as IHTMLElement;

            VisualSpan node = null;
            if (spanElement != null)
            {
                node = new VisualSpan(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
