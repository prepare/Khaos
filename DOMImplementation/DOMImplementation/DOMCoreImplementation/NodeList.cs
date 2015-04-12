using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class NodeList : INodeList
    {
        private List<INode> nodes = new List<INode>();

        public NodeList()
        {
        }

        public NodeList(List<INode> newNodes)
        {
            nodes.AddRange(newNodes);
        }

        /// <summary>
        /// The number of nodes in the list. The range of valid child node indices is 0 to length-1 inclusive.
        /// </summary>
        public int Length
        {
            get
            {
                return nodes.Count;
            }
        }

        public void InsertItem(int index, INode item)
        {
            nodes.Insert(index, item);
        }

        public INode this[int index]
        {
            get
            {
                if (index >= 0 && index < Length)
                    return nodes[index];
                else
                    throw new Exception("Tried to access index out fo range in NodeList!");
            }
            set
            {
                if (index >= 0 && index < Length)
                    nodes[index] = value;
                else
                    throw new Exception("Tried to set index out fo range in NodeList!");
            }
        }
    }
}
