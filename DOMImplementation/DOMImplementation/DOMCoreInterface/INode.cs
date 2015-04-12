using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    /// <summary>
    /// An integer indicating which type of node this is.
    /// </summary>
    public enum NodeType
    {
        // NodeType
        ELEMENT_NODE = 1,
        ATTRIBUTE_NODE = 2,
        TEXT_NODE = 3,
        CDATA_SECTION_NODE = 4,
        ENTITY_REFERENCE_NODE = 5,
        ENTITY_NODE = 6,
        PROCESSING_INSTRUCTION_NODE = 7,
        COMMENT_NODE = 8,
        DOCUMENT_NODE = 9,
        DOCUMENT_TYPE_NODE = 10,
        DOCUMENT_FRAGMENT_NODE = 11,
        NOTATION_NODE = 12
    }

    /// <summary>
    /// http://www.w3.org/TR/REC-DOM-Level-1/level-one-core.html#ID-1950641247
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// The name of this node, depending on its type; see the table above. 
        /// </summary>
        string NodeName
        {
            get;
        }

        /// <summary>
        /// The value of this node, depending on its type; see the table above. 
        /// </summary>
        string NodeValue
        {
            get;
            set;
        }
                                                 // raises(DOMException) on setting
                                                 // raises(DOMException) on retrieval

        /// <summary>
        /// A code representing the type of the underlying object, as defined above.
        /// </summary>
        NodeType NodeType
        {
            get;
        }

        /// <summary>
        /// The parent of this node. All nodes, except Document, DocumentFragment, and Attr may have a parent. However, if a node has just been created and not yet added to the tree, or if it has been removed from the tree, this is null.
        /// </summary>
        INode ParentNode
        {
            get;
            set;
        }

        /// <summary>
        /// A NodeList that contains all children of this node. If there are no children, this is a NodeList containing no nodes. The content of the returned NodeList is "live" in the sense that, for instance, changes to the children of the node object that it was created from are immediately reflected in the nodes returned by the NodeList accessors; it is not a static snapshot of the content of the node. This is true for every NodeList, including the ones returned by the getElementsByTagName method.
        /// </summary>
        NodeList ChildNodes
        {
            get;
            set;
        }

        /// <summary>
        /// The first child of this node. If there is no such node, this returns null.
        /// </summary>
        INode FirstChild
        {
            get;
        }

        /// <summary>
        /// The last child of this node. If there is no such node, this returns null.
        /// </summary>
        INode LastChild
        {
            get;
        }

        /// <summary>
        /// The node immediately preceding this node. If there is no such node, this returns null.
        /// </summary>
        INode PreviousSibling
        {
            get;
            set;
        }

        /// <summary>
        /// The node immediately following this node. If there is no such node, this returns null.
        /// </summary>
        INode NextSibling
        {
            get;
            set;
        }

        /// <summary>
        /// A NamedNodeMap containing the attributes of this node (if it is an Element) or null otherwise.
        /// </summary>
        NamedNodeMap Attributes
        {
            get;
        }

        /// <summary>
        /// The Document object associated with this node. This is also the Document object used to create new nodes. When this node is a Document this is null.
        /// </summary>
        IDocument OwnerDocument
        {
            get;
        }

        /// <summary>
        /// Inserts the node newChild before the existing child node refChild. If refChild is null, insert newChild at the end of the list of children.
        /// If newChild is a DocumentFragment object, all of its children are inserted, in the same order, before refChild. If the newChild is already in the tree, it is first removed. 
        /// </summary>
        /// <param name="newChild">The node to insert.</param>
        /// <param name="refChild">The reference node, i.newNode., the node before which the new node must be inserted.</param>
        /// <returns>The node being inserted.</returns>
        INode insertBefore(INode newChild, INode refChild);
                                         //raises(DOMException);

        /// <summary>
        /// Replaces the child node oldChild with newChild in the list of children, and returns the oldChild node. If the newChild is already in the tree, it is first removed. 
        /// </summary>
        /// <param name="newChild">The new node to put in the child list.</param>
        /// <param name="oldChild">The node being replaced in the list.</param>
        /// <returns>The node replaced.</returns>
        INode replaceChild(INode newChild, INode oldChild);
                                         //raises(DOMException);

        /// <summary>
        /// Removes the child node indicated by oldChild from the list of children, and returns it. 
        /// </summary>
        /// <param name="oldChild">The node being removed.</param>
        /// <returns>The node removed.</returns>
        INode removeChild(INode oldChild);
                                        //raises(DOMException);

        /// <summary>
        /// Adds the node newChild to the end of the list of children of this node. If the newChild is already in the tree, it is first removed. 
        /// </summary>
        /// <param name="newChild">The node to add. If it is a DocumentFragment  object, the entire contents of the document fragment are moved into the child list of this node.</param>
        /// <returns>The node added.</returns>
        INode appendChild(INode newChild);
                                        //raises(DOMException);

        /// <summary>
        /// This is a convenience method to allow easy determination of whether a node has any children.
        /// </summary>
        /// <returns>true if the node has any children, false if the node has no children.</returns>
        bool hasChildNodes();

        /// <summary>
        /// Returns a duplicate of this node, i.newNode., serves as a generic copy constructor for nodes. The duplicate node has no parent (parentNode returns null.).
        /// Cloning an Element copies all attributes and their values, including those generated by the XML processor to represent defaulted attributes, but this method does not copy any text it contains unless it is a deep clone, since the text is contained in a child Text node. Cloning any other type of node simply returns a copy of this node. 
        /// </summary>
        /// <param name="deep">If true, recursively clone the subtree under the specified node; if false, clone only the node itself (and its attributes, if it is an Element).</param>
        /// <returns>The duplicate node.</returns>
        INode cloneNode(bool deep);

    }
}
