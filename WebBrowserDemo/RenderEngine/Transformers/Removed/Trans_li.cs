using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_li : ITransformable
    {
        private const string tagName = "li";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_li.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement liElement = element as IHTMLElement;

            VisualListItem node = null;
            if (liElement != null)
            {
                node = new VisualListItem(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
