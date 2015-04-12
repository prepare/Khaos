using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class DocumentFragment : Node, IDocumentFragment
    {
        public DocumentFragment()
        {
            nodeName = "#document-fragment";
            nodeType = NodeType.DOCUMENT_FRAGMENT_NODE;
        }
    }
}
