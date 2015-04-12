using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_input : ITransformable
    {
        private const string tagName = "input";

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_input.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            // Needed!
            IHTMLInputElement inputElement = element as IHTMLInputElement;

            VisualInput node = null;
            if (inputElement != null)
            {
                node = new VisualInput(parent);
                node.Element = inputElement;

                //node.Type = VisualInput.InputType

            }
            return node;
        }

        #endregion
    }
}
