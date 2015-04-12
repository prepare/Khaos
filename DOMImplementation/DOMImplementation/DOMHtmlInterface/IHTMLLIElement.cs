using System;
namespace DOMCore
{
    public interface IHTMLLIElement
    {
        /**
         * List item bullet style. See the type attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        string Type { get; set; }

        /**
         * Reset sequence number when used in <code>OL</code>. See the value 
         * attribute definition in Html 4.01. This attribute is deprecated in 
         * Html 4.01.
         */
        string Value { get; set; }
    }
}
