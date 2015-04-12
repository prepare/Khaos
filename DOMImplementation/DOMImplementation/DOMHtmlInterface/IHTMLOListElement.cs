using System;
namespace DOMCore
{
    public interface IHTMLOListElement : IHTMLElement
    {
        /**
         * Reduce spacing between list items. See the compact attribute definition 
         * in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        bool Compact { get; set; }

        /**
         * Starting sequence number. See the start attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        int Start { get; set; }

        /**
         * Numbering style. See the type attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        string Type { get; set; }
    }
}
