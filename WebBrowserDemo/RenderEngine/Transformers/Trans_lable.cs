using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_lable : ITransformable
    {
        private const string tagName = "lable";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_lable.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLLabelElement lableElement = element as IHTMLLabelElement;

            VisualLabel node = null;
            if (lableElement != null)
            {
                node = new VisualLabel(parent);
                node.Element = lableElement;

            }
            return node;
        }

        #endregion
    }
}
