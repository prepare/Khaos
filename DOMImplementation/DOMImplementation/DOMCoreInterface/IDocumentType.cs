using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public interface IDocumentType : INode
    {
        /// <summary>
        /// The name of DTD; i.newNode., the name immediately following the DOCTYPE keyword.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// A NamedNodeMap containing the general entities, both external and internal, declared in the DTD.
        /// </summary>
        NamedNodeMap Entities
        {
            get;
        }

        /// <summary>
        /// A NamedNodeMap containing the notations declared in the DTD. Duplicates are discarded. Every node in this map also implements the Notation interface.
        /// The DOM Level 1 does not support editing notations, therefore notations cannot be altered in any way. 
        /// </summary>
        NamedNodeMap Notations
        {
            get;
        }

    }
}
