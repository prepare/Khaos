using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;
using System.Windows.Media;

namespace RenderEngine.Transformers
{
    public class Trans_a : ITransformable
    {
        private const string tagName = "a";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_a.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLAnchorElement aElement = element as IHTMLAnchorElement;

            VisualAnchor node = null;
            if (aElement != null)
            {
                node = new VisualAnchor(parent);
                node.Element = aElement;
                node.CssStyle.WebColor = new CascadingStyleSheets.WebColor() { RealValue = new System.Windows.Media.SolidColorBrush(Colors.Blue) };
                node.CssStyle.WebCursor = new CascadingStyleSheets.WebCursor() { RealValue = CascadingStyleSheets.CursorEnum.Hand };
            }
            return node;
        }

        #endregion
    }
}
