using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_img : ITransformable
    {
        private const string tagName = "img";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_img.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLImageElement imgElement = element as IHTMLImageElement;

            VisualImage node = null;
            if (imgElement != null)
            {
                node = new VisualImage(parent);
                node.Element = imgElement;
            }
            return node;
        }

        #endregion
    }
}
