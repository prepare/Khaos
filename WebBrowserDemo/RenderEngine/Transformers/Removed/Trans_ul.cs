using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_ul : ITransformable
    {
        private const string tagName = "ul";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_ul.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement ulElement = element as IHTMLElement;

            VisualList node = null;
            if (ulElement != null)
            {
                node = new VisualList(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
