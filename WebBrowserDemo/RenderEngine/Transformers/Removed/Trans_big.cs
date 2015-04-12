using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_big : ITransformable
    {
        private const string tagName = "big";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_big.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement bigElement = element as IHTMLElement;

            VisualBig node = null;
            if (bigElement != null)
            {
                node = new VisualBig(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
