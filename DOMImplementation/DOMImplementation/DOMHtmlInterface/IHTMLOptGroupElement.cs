using System;
namespace DOMCore
{
    public interface IHTMLOptGroupElement : IHTMLElement
    {
        /**
         * The control is unavailable in this context. See the disabled attribute 
         * definition in Html 4.01.
         */
        bool Disables { get; set; }

        /**
         * Assigns a label to this option group. See the label attribute definition
         *  in Html 4.01.
         */
        string Label { get; set; }
    }
}
