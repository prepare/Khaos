using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_tr : ITransformable
    {
        private const string tagName = "tr";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_tr.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLTableRowElement tableRowElement = element as IHTMLTableRowElement;

            VisualNode node = null;
            if (tableRowElement != null)
            {
                if (parent is VisualTable)
                {
                    node = new VisualTableRow(parent as VisualTable);
                    node.Element = tableRowElement;

                    //node.Type = VisualInput.InputType
                }
                else
                {
                    node = new VisualNode(parent);
                    node.Element = tableRowElement;
                }
            }
            return node;
        }

        #endregion
    }
}
