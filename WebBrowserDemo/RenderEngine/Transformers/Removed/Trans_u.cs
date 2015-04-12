using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_u : ITransformable
    {
        private const string tagName = "u";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_u.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement uElement = element as IHTMLElement;

            VisualUnderline node = null;
            if (uElement != null)
            {
                node = new VisualUnderline(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
