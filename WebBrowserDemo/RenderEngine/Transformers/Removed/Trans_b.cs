using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_b : ITransformable
    {
        private const string tagName = "b";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_b.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement bElement = element as IHTMLElement;

            VisualBold node = null;
            if (bElement != null)
            {
                node = new VisualBold(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
