using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;
using PhoenixOI;

namespace RenderEngine.Transformers
{
    public class Trans_html : ITransformable
    {
        private const string tagName = "html";

        public Request WebRequest { get; set;}
        public IPageHost PageHost { get; set; }


        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return tagName == Trans_html.tagName;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            IHTMLElement htmlDocument = element as IHTMLElement;

            VisualDocument node = null;
            if (htmlDocument != null)
            {
                //WebRequest
                node = VisualDocument.CreateVisualRoot(PageHost, WebRequest);
                node.Element = htmlDocument;
                node.SetDefaultStyle();

            }
            return node;
        }

        #endregion
    }
}
