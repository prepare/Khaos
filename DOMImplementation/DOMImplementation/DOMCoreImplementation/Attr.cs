using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class Attr : Node, IAttr
    {
        public Attr(string name)
        {
            nodeName = name;
            nodeType = NodeType.ATTRIBUTE_NODE;
        }

        /// <summary>
        /// Returns the name of this attribute.
        /// </summary>
        public string Name
        {
            get
            {
                return NodeName;
            }
        }

        /// <summary>
        /// If this attribute was explicitly given a value in the original document, this is true; otherwise, it is false. Note that the implementation is in charge of this attribute, not the user. If the user changes the value of the attribute (even if it ends up having the same value as the default value) then the specified  flag is automatically flipped to true. To re-specify the attribute as the default value from the DTD, the user must delete the attribute. The implementation will then make a new attribute available with specified set to false and the default value (if one exists).
        /// </summary>
        public bool Specified
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// On retrieval, the value of the attribute is returned as a string. Character and general entity references are replaced with their values.
        /// On setting, this creates a Text node with the unparsed contents of the string. 
        /// </summary>
        public string Value
        {
            get
            {
                return NodeValue;
            }
            set
            {
                NodeValue = value;
            }
        }
    }
}
