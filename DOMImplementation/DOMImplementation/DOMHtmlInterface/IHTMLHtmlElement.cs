using System;
namespace DOMCore
{
    public interface IHTMLHtmlElement : IHTMLElement
    {
        /**
         * Version information about the document'coreRule DTD. See the version attribute 
         * definition in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        string Version { get; set; }
    }
}
