using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_div : ITransformable
    {
        private const string tagName = "div";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_div.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement divElement = element as IHTMLElement;

            VisualDiv node = null;
            if (divElement != null)
            {
                node = new VisualDiv(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
