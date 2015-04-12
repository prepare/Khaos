using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_p : ITransformable
    {
        private const string tagName = "p";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_p.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement pElement = element as IHTMLElement;

            VisualParagraph node = null;
            if (pElement != null)
            {
                node = new VisualParagraph(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
