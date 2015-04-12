using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_i : ITransformable
    {
        private const string tagName = "i";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_i.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement iElement = element as IHTMLElement;

            VisualItalic node = null;
            if (iElement != null)
            {
                node = new VisualItalic(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
