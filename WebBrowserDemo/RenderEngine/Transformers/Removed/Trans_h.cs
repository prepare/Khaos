using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_h : ITransformable
    {
        private int level = 0;
        
        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            if (tagName.StartsWith("h") && tagName.Length == 2)
            {
                char charlevel = tagName[1];
                if (char.IsDigit(charlevel))
                {
                    level = int.Parse(charlevel.ToString());
                    return true;
                }
            }
            return false;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement hElement = element as IHTMLElement;

            VisualHeading node = null;
            if (hElement != null)
            {
                node = new VisualHeading(parent);

                //level

                TransformManager.LoadStyle(element, node);
                node.ItemName = element.TagName;

            }
            return node;
        }

        #endregion
    }
}
