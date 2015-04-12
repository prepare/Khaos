using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_ol : ITransformable
    {
        private const string tagName = "ol";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_ol.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement olElement = element as IHTMLElement;

            VisualOrderedList node = null;
            if (olElement != null)
            {
                node = new VisualOrderedList(parent);
                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
