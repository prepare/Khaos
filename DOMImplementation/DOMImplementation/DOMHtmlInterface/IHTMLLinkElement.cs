using System;
namespace DOMCore
{
    public interface IHTMLLinkElement : IHTMLElement
    {
        /**
         * The character encoding of the resource being linked to. See the charset 
         * attribute definition in Html 4.01.
         */
        string Charset { get; set; }

        /**
         * Enables/disables the link. This is currently only used for style sheet 
         * links, and may be used to activate or deactivate style sheets. 
         */
        bool Disabled { get; set; }

        /**
         * The URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute definition in 
         * Html 4.01.
         */
        string Href { get; set; }

        /**
         * Language code of the linked resource. See the hreflang attribute 
         * definition in Html 4.01.
         */
        string Hreflang { get; set; }

        /**
         * Designed for use with one or more target media. See the media attribute 
         * definition in Html 4.01.
         */
        string Media { get; set; }

        /**
         * Forward link type. See the rel attribute definition in Html 4.01.
         */
        string Rel { get; set; }

        /**
         * Reverse link type. See the rev attribute definition in Html 4.01.
         */
        string Rev { get; set; }

        /**
         * Frame to render the resource in. See the target attribute definition in 
         * Html 4.01.
         */
        string Target { get; set; }

        /**
         * Advisory content type. See the type attribute definition in Html 4.01.
         */
        string Type { get; set; }
    }
}
