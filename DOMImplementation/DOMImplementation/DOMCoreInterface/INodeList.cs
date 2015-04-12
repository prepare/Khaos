using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public interface INodeList
    {
        /// <summary>
        /// The number of nodes in the list. The range of valid child node indices is 0 to length-1 inclusive.
        /// </summary>
        int Length
        {
            get;
        }

        INode this[int index]
        {
            get;
            set;
        }

        void InsertItem(int index, INode item);
    }
}
