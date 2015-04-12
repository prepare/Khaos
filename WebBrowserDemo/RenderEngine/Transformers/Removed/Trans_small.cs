using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_small : ITransformable
    {
        private const string tagName = "small";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_small.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement smallElement = element as IHTMLElement;

            VisualSmall node = null;
            if (smallElement != null)
            {
                node = new VisualSmall(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
