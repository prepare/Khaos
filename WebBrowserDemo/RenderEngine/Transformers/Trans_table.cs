using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_table : ITransformable
    {
        private const string tagName = "table";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_table.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            // Needed!
            IHTMLTableElement tableElement = element as IHTMLTableElement;

            VisualTable node = null;
            if (tableElement != null)
            {
                node = new VisualTable(parent);
                node.Element = tableElement;

                //node.Type = VisualInput.InputType

            }
            return node;
        }

        #endregion
    }
}
