using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class Node : INode
    {
        public Node()
        {
        }

        public Node(string name, IDocument doc)
        {
            nodeName = name;
            ownerDocument = doc;
        }

        public string nodeName;
        public string nodeValue;
        public NodeType nodeType;
        public INode parentNode;
        private NodeList childNodes;
        //public Node firstChild;
        //public Node lastChild;
        public INode previousSibling;
        public INode nextSibling;
        public NamedNodeMap attributes;
        public IDocument ownerDocument;

        /// <summary>
        /// The name of this node, depending on its type; see the table above. 
        /// </summary>
        public string NodeName
        {
            get
            {
                return nodeName;
            }
        }

        /// <summary>
        /// The value of this node, depending on its type; see the table above. 
        /// </summary>
        public string NodeValue
        {
            get
            {
                return nodeValue;
            }
            set
            {
                nodeValue = value;
            }
        }
        // raises(DOMException) on setting
        // raises(DOMException) on retrieval

        /// <summary>
        /// A code representing the type of the underlying object, as defined above.
        /// </summary>
        public NodeType NodeType
        {
            get
            {
                return nodeType;
            }
        }

        /// <summary>
        /// The parent of this node. All nodes, except Document, DocumentFragment, and Attr may have a parent. However, if a node has just been created and not yet added to the tree, or if it has been removed from the tree, this is null.
        /// </summary>
        public INode ParentNode
        {
            get
            {
                return parentNode;
            }
            set
            {
                parentNode = value;
            }
        }

        /// <summary>
        /// A NodeList that contains all children of this node. If there are no children, this is a NodeList containing no nodes. The content of the returned NodeList is "live" in the sense that, for instance, changes to the children of the node object that it was created from are immediately reflected in the nodes returned by the NodeList accessors; it is not a static snapshot of the content of the node. This is true for every NodeList, including the ones returned by the getElementsByTagName method.
        /// </summary>
        public NodeList ChildNodes
        {
            get
            {
                //NodeList nl;
                /*List<Node> lnl = new List<Node>();

                Node currentNode = FirstChild;
                while (currentNode != null)
                {
                    if (currentNode.NextSibling != null)
                        lnl.Add(currentNode.NextSibling);
                    currentNode = currentNode.NextSibling;
                }
                nl = new NodeList(lnl);*/
                //nl = childNodes;
                if (childNodes == null)
                    childNodes = new NodeList(new List<INode>());
                return childNodes;
            }
            set
            {
                childNodes = value;
            }
        }

        /// <summary>
        /// The first child of this node. If there is no such node, this returns null.
        /// </summary>
        public INode FirstChild
        {
            get
            {
                return childNodes[0];
                //return firstChild;
            }
        }

        /// <summary>
        /// The last child of this node. If there is no such node, this returns null.
        /// </summary>
        public INode LastChild
        {
            get
            {
                return childNodes[childNodes.Length-1];
                //return lastChild;
            }
        }

        /// <summary>
        /// The node immediately preceding this node. If there is no such node, this returns null.
        /// </summary>
        public INode PreviousSibling
        {
            get
            {
                return previousSibling;
            }
            set
            {
                previousSibling = value;
            }
        }

        /// <summary>
        /// The node immediately following this node. If there is no such node, this returns null.
        /// </summary>
        public INode NextSibling
        {
            get
            {
                return nextSibling;
            }
            set
            {
                nextSibling = value;
            }
        }

        /// <summary>
        /// A NamedNodeMap containing the attributes of this node (if it is an Element) or null otherwise.
        /// </summary>
        public NamedNodeMap Attributes
        {
            get
            {
                if (attributes == null)
                    attributes = new NamedNodeMap(new List<INode>());
                return attributes;
            }
        }

        /// <summary>
        /// The Document object associated with this node. This is also the Document object used to create new nodes. When this node is a Document this is null.
        /// </summary>
        public IDocument OwnerDocument
        {
            get
            {
                return ownerDocument;
            }
        }

        /// <summary>
        /// Inserts the node newChild before the existing child node refChild. If refChild is null, insert newChild at the end of the list of children.
        /// If newChild is a DocumentFragment object, all of its children are inserted, in the same order, before refChild. If the newChild is already in the tree, it is first removed. 
        /// </summary>
        /// <param name="newChild">The node to insert.</param>
        /// <param name="refChild">The reference node, i.newNode., the node before which the new node must be inserted.</param>
        /// <returns>The node being inserted.</returns>
        public INode insertBefore(INode newChild, INode refChild)
        {
            //remove newChild from tree if already there
            /*List<Node> nodesBuffer2 = new List<Node>();
            if (FirstChild != null)
            {
                nodesBuffer2.Add(FirstChild);
                while (nodesBuffer2.Count > 0)
                {
                    Node currentNode = nodesBuffer2[0];
                    nodesBuffer2.Remove(currentNode);

                    if (currentNode == newChild)
                    {
                        removeChild(newChild);
                        break;
                    }
                    if (currentNode.FirstChild != null)
                        nodesBuffer2.Add(currentNode.FirstChild);

                    if (currentNode.NextSibling != null)
                    {
                        nodesBuffer2.Insert(0, currentNode.NextSibling);
                    }
                }
            }*/

            newChild.ParentNode.removeChild(newChild);

            //append to end of this nodes children list
            if (refChild == null)
            {
                appendChild(newChild);
                return newChild;
            }

            // if newChild is a DocumentFragment just append all its children instead
            if (newChild.GetType().ToString() == "DOMbase.DocumentFragment")
            {
                while (newChild.FirstChild != null)
                {
                    insertBefore(FirstChild, refChild);
                }
                return newChild;
            }

            /*Node parentNode = this;
            Node nextNode = FirstChild;
            Node prevNode = null;
            List<Node> nodesBuffer = new List<Node>();

            if (FirstChild != null)
            {
                nodesBuffer.Add(FirstChild);
                while (nodesBuffer.Count > 0)
                {
                    //choose new current node
                    nextNode = nodesBuffer[0];
                    nodesBuffer.Remove(nextNode);

                    //traverse list of children of current node to find refChild
                    while (nextNode != null)
                    {
                        parentNode = nextNode.ParentNode;
                        prevNode = nextNode.PreviousSibling;

                        //insert new node before reference node
                        if (nextNode == refChild)
                        {
                            newChild.parentNode = parentNode;
                            nextNode.previousSibling = newChild;
                            newChild.nextSibling = nextNode;
                            //prepend to children
                            if (prevNode == null && nextNode != null)
                            {
                                parentNode.firstChild = newChild;
                                newChild.previousSibling = null;
                            }
                            //insert into children
                            if (prevNode != null && nextNode != null)
                            {
                                prevNode.nextSibling = newChild;
                                newChild.previousSibling = prevNode;
                            }
                            return newChild;
                        }
                        //add children to buffer if any
                        if (nextNode.FirstChild != null)
                        {
                            nodesBuffer.Add(nextNode.FirstChild);
                        }
                        nextNode = nextNode.NextSibling;
                    }
                }
            }*/

            List<INode> lnl = new List<INode>();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                if (ChildNodes[i] == refChild)
                    lnl.Add(newChild);
                lnl.Add(ChildNodes[i]);
            }
            childNodes = new NodeList(lnl);
            updateChildren();

            return null;
        }
        //raises(DOMException);

        /// <summary>
        /// Replaces the child node oldChild with newChild in the list of children, and returns the oldChild node. If the newChild is already in the tree, it is first removed. 
        /// </summary>
        /// <param name="newChild">The new node to put in the child list.</param>
        /// <param name="oldChild">The node being replaced in the list.</param>
        /// <returns>The node replaced.</returns>
        public INode replaceChild(INode newChild, INode oldChild)
        {
            //remove newChild from tree if already there
            /*List<Node> nodesBuffer = new List<Node>();
            if (FirstChild != null)
            {
                nodesBuffer.Add(FirstChild);
                while (nodesBuffer.Count > 0)
                {
                    Node currentNode = nodesBuffer[0];
                    nodesBuffer.Remove(currentNode);

                    if (currentNode == newChild)
                    {
                        removeChild(newChild);
                        break;
                    }
                    if (currentNode.FirstChild != null)
                        nodesBuffer.Add(currentNode.FirstChild);

                    if (currentNode.NextSibling != null)
                    {
                        nodesBuffer.Insert(0, currentNode.NextSibling);
                    }
                }
            }

            //replace oldChild with newChild
            newChild.parentNode = oldChild.ParentNode;
            newChild.previousSibling = oldChild.PreviousSibling;
            newChild.nextSibling = oldChild.NextSibling;

            removeChild(oldChild);

            //replaced node was first child
            if (newChild.PreviousSibling == null)
            {
                newChild.ParentNode.firstChild = newChild;
            }
            //update link to previous child
            if (newChild.PreviousSibling != null)
            {
                newChild.PreviousSibling.nextSibling = newChild;
            }
            //update link to next child
            if (newChild.NextSibling != null)
            {
                newChild.NextSibling.previousSibling = newChild;
            }*/

            newChild.ParentNode.removeChild(newChild);

            List<INode> lnl = new List<INode>();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                if (ChildNodes[i] == oldChild)
                    lnl.Add(newChild);
                else
                    lnl.Add(ChildNodes[i]);
            }
            childNodes = new NodeList(lnl);
            updateChildren();

            return newChild;
        }
        //raises(DOMException);

        /// <summary>
        /// Removes the child node indicated by oldChild from the list of children, and returns it. 
        /// </summary>
        /// <param name="oldChild">The node being removed.</param>
        /// <returns>The node removed.</returns>
        public INode removeChild(INode oldChild)
        {
            //is first child of its parent node
            /*if (oldChild.PreviousSibling == null)
            {
                if (oldChild.NextSibling == null)
                {
                    oldChild.ParentNode.firstChild = null;
                }
                else
                {
                    oldChild.ParentNode.firstChild = oldChild.NextSibling;
                }
            }
            if (oldChild.PreviousSibling != null)
            {
                oldChild.PreviousSibling.nextSibling = oldChild.NextSibling;
            }
            if (oldChild.NextSibling != null)
            {
                oldChild.NextSibling.previousSibling = oldChild.PreviousSibling;
            }*/

            List<INode> lnl = new List<INode>();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                if (ChildNodes[i] != oldChild)
                    lnl.Add(ChildNodes[i]);
            }
            childNodes = new NodeList(lnl);
            updateChildren();

            oldChild.ParentNode = null;
            oldChild.PreviousSibling = null;
            oldChild.NextSibling = null;

            return oldChild;
        }
        //raises(DOMException);

        /// <summary>
        /// Adds the node newChild to the end of the list of children of this node. If the newChild is already in the tree, it is first removed. 
        /// </summary>
        /// <param name="newChild">The node to add. If it is a DocumentFragment  object, the entire contents of the document fragment are moved into the child list of this node.</param>
        /// <returns>The node added.</returns>
        public INode appendChild(INode newChild)
        {
            //remove newChild from tree if already there
            /*List<Node> nodesBuffer = new List<Node>();
            if (FirstChild != null)
            {
                nodesBuffer.Add(FirstChild);
                while (nodesBuffer.Count > 0)
                {
                    Node currentNode = nodesBuffer[0];
                    nodesBuffer.Remove(currentNode);

                    if (currentNode == newChild)
                    {
                        removeChild(newChild);
                        break;
                    }
                    if (currentNode.FirstChild != null)
                        nodesBuffer.Add(currentNode.FirstChild);

                    if (currentNode.NextSibling != null)
                    {
                        nodesBuffer.Insert(0, currentNode.NextSibling);
                    }
                }
            }

            // if newChild is a DocumentFragment just append all its children instead
            if (newChild.GetType().ToString() == "DOMbase.DocumentFragment")
            {
                while (newChild.FirstChild != null)
                {
                    appendChild(FirstChild);
                }
            }

            //append to end of this nodes children list
            newChild.parentNode = this;
            newChild.nextSibling = null;
            //this node doesn't have children so far
            if (FirstChild == null)
            {
                this.firstChild = newChild;
                newChild.previousSibling = null;
            }
            //this node has other children already
            else
            {
                Node nextNode = FirstChild;
                Node prevNode = null;

                //traverse list of children to find last child
                while (nextNode != null)
                {
                    prevNode = nextNode;
                    nextNode = nextNode.NextSibling;
                }

                prevNode.nextSibling = newChild;
                newChild.previousSibling = prevNode;
            }*/

            //remove newChild from childNotes if already there
            if (newChild != null && newChild.ParentNode != null)
                newChild.ParentNode.removeChild(newChild);

            List<INode> lnl = new List<INode>();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                lnl.Add(ChildNodes[i]);
            }
            lnl.Add(newChild);
            childNodes = new NodeList(lnl);
            updateChildren();

            return newChild;
        }
        //raises(DOMException);


        private void updateChildren()
        {
            for (int i = 0; i < childNodes.Length; i++)
            {
                childNodes[i].ParentNode = this;
                if (i == 0)
                {
                    childNodes[i].PreviousSibling = null;
                    //firstChild = childNodes.Item(i);
                }
                else
                {
                    childNodes[i].PreviousSibling = childNodes[i-1];
                }
                if (i == (childNodes.Length - 1))
                {
                    childNodes[i].NextSibling = null;
                    //lastChild = childNodes.Item(i);
                }
                else
                {
                    childNodes[i].NextSibling = childNodes[i + 1];
                }
            }
        }

        /// <summary>
        /// This is a convenience method to allow easy determination of whether a node has any children.
        /// </summary>
        /// <returns>true if the node has any children, false if the node has no children.</returns>
        public bool hasChildNodes()
        {
            return (FirstChild != null);
        }

        /// <summary>
        /// Returns a duplicate of this node, i.newNode., serves as a generic copy constructor for nodes. The duplicate node has no parent (parentNode returns null.).
        /// Cloning an Element copies all attributes and their values, including those generated by the XML processor to represent defaulted attributes, but this method does not copy any text it contains unless it is a deep clone, since the text is contained in a child Text node. Cloning any other type of node simply returns a copy of this node. 
        /// </summary>
        /// <param name="deep">If true, recursively clone the subtree under the specified node; if false, clone only the node itself (and its attributes, if it is an Element).</param>
        /// <returns>The duplicate node.</returns>
        public INode cloneNode(bool deep)
        {
            INode newNode = new Node(nodeName,ownerDocument);

            if (deep)
            {
                //append to end of this nodes children list
                if (FirstChild != null)
                {
                    INode nextNode = FirstChild;
                    INode prevNode = null;

                    //traverse list of children cloning each child
                    while (nextNode != null)
                    {
                        newNode.appendChild(nextNode.cloneNode(deep));
                        prevNode = nextNode;
                        nextNode = nextNode.NextSibling;
                    }

                }
            }

            return newNode;
        }
    }
}
