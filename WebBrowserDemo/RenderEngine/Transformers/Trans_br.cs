using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_br : ITransformable
    {
        private const string tagName = "br";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_br.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLBRElement brElement = element as IHTMLBRElement;

            VisualBreak node = null;
            if (brElement != null)
            {
                node = new VisualBreak(parent);
                node.Element = brElement;

            }
            return node;
        }

        #endregion
    }
}
