using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_hr : ITransformable
    {
        private const string tagName = "hr";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_hr.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement hrElement = element as IHTMLElement;

            VisualHorizontalRule node = null;
            if (hrElement != null)
            {
                node = new VisualHorizontalRule(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
