using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_form : ITransformable
    {
        private const string tagName = "form";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_form.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLFormElement formElement = element as IHTMLFormElement;

            VisualForm node = null;
            if (formElement != null)
            {
                node = new VisualForm(parent);
                node.Element = formElement;

            }
            return node;
        }

        #endregion
    }
}
