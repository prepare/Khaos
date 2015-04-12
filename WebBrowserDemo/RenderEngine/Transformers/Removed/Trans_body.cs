using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_body : ITransformable
    {
        private const string tagName = "body";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_body.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement bodyElement = element as IHTMLElement;

            VisualBody node = null;
            if (bodyElement != null)
            {
                node = new VisualBody(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
