using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class Document : Node, IDocument
    {
        public Document()
        {
            nodeName = "#document";
            nodeType = NodeType.DOCUMENT_NODE;
        }

        /// <summary>
        /// The Document Type Declaration (see DocumentType) associated with this document. For HTML documents as well as XML documents without a document type declaration this returns null. The DOM Level 1 does not support editing the Document Type Declaration, therefore docType cannot be altered in any way.
        /// </summary>
        public DocumentType Doctype
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// The DOMImplementation object that handles this document. A DOM application may use objects from multiple implementations.
        /// </summary>
        public DOMImplementation Implementation
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// This is a convenience attribute that allows direct access to the child node that is the root element of the document. For HTML documents, this is the element with the tagName "HTML".
        /// </summary>
        public IElement DocumentElement
        {
            get
            {
                NodeList nl = getElementsByTagName("html");
                if (nl.Length > 0)
                    return nl[0] as IElement;
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates an element of the type specified. Note that the instance returned implements the Element interface, so attributes can be specified directly on the returned object.
        /// </summary>
        /// <param name="tagName">The name of the element type to instantiate. For XML, this is case-sensitive. For HTML, the tagName parameter may be provided in any case, but it must be mapped to the canonical uppercase form by the DOM implementation. </param>
        /// <returns>A new Element object.</returns>
        public IElement createElement(string tagName)
        {
            return new Element(tagName,this);
        }
        //raises(DOMException);

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>A new DocumentFragment.</returns>
        public DocumentFragment createDocumentFragment()
        {
            return new DocumentFragment();
        }

        /// <summary>
        /// Creates a Text node given the specified string.
        /// </summary>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new Text object.</returns>
        public Text createTextNode(string data)
        {
            return new Text(data);
        }

        /// <summary>
        /// Creates a Comment node given the specified string.
        /// </summary>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new Comment object.</returns>
        public Comment createComment(string data)
        {
            return new Comment(data);
        }

        /// <summary>
        /// Creates a CDATASection node whose value is the specified string.
        /// </summary>
        /// <param name="data">The data for the CDATASection contents.</param>
        /// <returns>The new CDATASection object.</returns>
        public CDATASection createCDATASection(string data)
        {
            return new CDATASection(data);
        }
        //raises(DOMException);

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and data strings.
        /// </summary>
        /// <param name="target">The target part of the processing instruction.</param>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new ProcessingInstruction object.</returns>
        public ProcessingInstruction createProcessingInstruction(string target, string data)
        {
            return new ProcessingInstruction(target,data);
        }
        //raises(DOMException);

        /// <summary>
        /// Creates an Attr of the given name. Note that the Attr instance can then be set on an Element using the setAttribute method.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>A new Attr object.</returns>
        public Attr createAttribute(string name)
        {
            return new Attr(name);
        }
        //raises(DOMException);

        /// <summary>
        /// Creates an EntityReference object.
        /// </summary>
        /// <param name="name">The name of the entity to reference.</param>
        /// <returns>The new EntityReference object.</returns>
        public EntityReference createEntityReference(string name)
        {
            return new EntityReference();
        }
        //raises(DOMException);

        /// <summary>
        /// Returns a NodeList of all the Elements with a given tag name in the order in which they would be encountered in a preorder traversal of the Document tree.
        /// </summary>
        /// <param name="tagname">The name of the tag to match on. The special value "*" matches all tags.</param>
        /// <returns>A new NodeList object containing all the matched Elements.</returns>
        public NodeList getElementsByTagName(string tagname)
        {
            List<INode> lnl = new List<INode>();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                if (ChildNodes[i].NodeName.Contains("#"))
                    continue;
                if (ChildNodes[i].NodeName == tagname)
                    lnl.Add(ChildNodes[i]);
                NodeList nl = ((IElement)ChildNodes[i]).getElementsByTagName(tagname);
                for (int j = 0; j < nl.Length; j++)
                {
                    if (nl[j].NodeName == tagname)
                        lnl.Add(nl[j]);
                }
            }
            return new NodeList(lnl);
        }
    }
}
