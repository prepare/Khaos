using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_strong : ITransformable
    {
        private const string tagName = "strong";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_strong.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement strongElement = element as IHTMLElement;

            VisualStrong node = null;
            if (strongElement != null)
            {
                node = new VisualStrong(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
