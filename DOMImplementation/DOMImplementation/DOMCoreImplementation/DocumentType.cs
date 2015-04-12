using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class DocumentType : Node, IDocumentType
    {
        public DocumentType()
        {
            nodeType = NodeType.DOCUMENT_TYPE_NODE;
        }

        /// <summary>
        /// The name of DTD; i.newNode., the name immediately following the DOCTYPE keyword.
        /// </summary>
        public string Name
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// A NamedNodeMap containing the general entities, both external and internal, declared in the DTD.
        /// </summary>
        public NamedNodeMap Entities
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// A NamedNodeMap containing the notations declared in the DTD. Duplicates are discarded. Every node in this map also implements the Notation interface.
        /// The DOM Level 1 does not support editing notations, therefore notations cannot be altered in any way. 
        /// </summary>
        public NamedNodeMap Notations
        {
            get
            {
                return null;
            }
        }
    }
}
