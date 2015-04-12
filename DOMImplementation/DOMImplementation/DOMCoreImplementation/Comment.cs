using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class Comment : CharacterData, IComment
    {
        public Comment()
            : base()
        {
            nodeName = "#comment";
            nodeType = NodeType.COMMENT_NODE;
        }
        public Comment(string data)
            : base(data)
        {
            nodeName = "#comment";
            nodeType = NodeType.COMMENT_NODE;
        }
    }
}
