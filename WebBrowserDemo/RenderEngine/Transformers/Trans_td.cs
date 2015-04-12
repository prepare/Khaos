using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_td : ITransformable
    {
        private const string tagName = "td";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_td.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            // Needed!
            IHTMLTableCellElement cellElement = element as IHTMLTableCellElement;

            VisualNode node = null;
            if (cellElement != null)
            {
                if (parent is VisualTableRow)
                {
                    node = new VisualTableCell(parent as VisualTableRow);
                    node.Element = cellElement;

                    //node.Type = VisualInput.InputType
                }
                else
                {
                    node = new VisualNode(parent);
                    node.Element = cellElement;
                }
            }
            return node;
        }

        #endregion
    }
}
