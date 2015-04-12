using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_center : ITransformable
    {
        private const string tagName = "center";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_center.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            // no need
            IHTMLElement centerElement = element as IHTMLElement;

            VisualCenter node = null;
            if (centerElement != null)
            {
                node = new VisualCenter(parent);
                node.Element = centerElement;

            }
            return node;
        }

        #endregion
    }
}
