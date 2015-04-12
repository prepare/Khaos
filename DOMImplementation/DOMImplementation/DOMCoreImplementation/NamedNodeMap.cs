using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class NamedNodeMap : INamedNodeMap
    {
        private Dictionary<string, INode> nodes = new Dictionary<string, INode>();

        public NamedNodeMap()
        {
        }

        public NamedNodeMap(List<INode> lnl)
        {
            foreach (INode n in lnl)
            {
                setNamedItem(n);
            }
        }

        /// <summary>
        /// The number of nodes in the map. The range of valid child node indices is 0 to length-1 inclusive.
        /// </summary>
        public int Length
        {
            get
            {
                return nodes.Count;
            }
        }

        /// <summary>
        /// Retrieves a node specified by name.
        /// </summary>
        /// <param name="name">Name of a node to retrieve.</param>
        /// <returns>A Node (of any type) with the specified name, or null if the specified name did not identify any node in the map.</returns>
        public INode getNamedItem(string name)
        {
            if (nodes.ContainsKey(name))
                return nodes[name];
            else
                return null;
        }

        /// <summary>
        /// Adds a node using its nodeName attribute.
        /// As the nodeName attribute is used to derive the name which the node must be stored under, multiple nodes of certain types (those that have a "special" string value) cannot be stored as the names would clash. This is seen as preferable to allowing nodes to be aliased. 
        /// </summary>
        /// <param name="arg">A node to store in a named node map. The node will later be accessible using the value of the nodeName attribute of the node. If a node with that name is already present in the map, it is replaced by the new one.</param>
        /// <returns>If the new Node replaces an existing node with the same name the previously existing Node is returned, otherwise null is returned.</returns>
        public INode setNamedItem(INode arg)
        {
            INode myNode = getNamedItem(arg.NodeName);
            if (myNode != null)
            {
                nodes.Remove(arg.NodeName);
            }
            nodes.Add(arg.NodeName, arg);
            return myNode;
        }
        //raises(DOMException);

        /// <summary>
        /// Removes a node specified by name. If the removed node is an Attr with a default value it is immediately replaced.
        /// </summary>
        /// <param name="name">The name of a node to remove.</param>
        /// <returns>The node removed from the map or null  if no node with such a name exists.</returns>
        public INode removeNamedItem(string name)
        {
            INode myNode = getNamedItem(name);
            if (myNode != null)
                nodes.Remove(name);
            return myNode;
        }
        //raises(DOMException);

        /// <summary>
        /// Returns the indexth item in the map. If index is greater than or equal to the number of nodes in the map, this returns null.
        /// </summary>
        /// <param name="index">Index into the map.</param>
        /// <returns>The node at the indexth position in the NamedNodeMap, or null if that is not a valid index.</returns>
        public INode item(int index)
        {
            INode myNode = null;
            int i = 0;
            foreach (KeyValuePair<string, INode> kvp in nodes)
            {
                if (i == index)
                    myNode = kvp.Value;
                i++;
            }
            return myNode;
        }
    }
}
