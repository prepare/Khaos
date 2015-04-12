
namespace DOMCore
{
    public interface IHTMLAnchorElement : IHTMLElement
    {
        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in Html 4.01.
         */
        string AccessKey { get; set; }

        /**
         * The character encoding of the linked resource. See the charset 
         * attribute definition in Html 4.01.
         */
        string Charset { get; set; }

        /**
         * Comma-separated list of lengths, defining an active region geometry. 
         * See also <code>shape</code> for the shape of the region. See the 
         * coords attribute definition in Html 4.01.
         */
        string Coords { get; set; }

        /**
         * The absolute URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute 
         * definition in Html 4.01.
         */
        string Href { get; set; }

        /**
         * Language code of the linked resource. See the hreflang attribute 
         * definition in Html 4.01.
         */
        string Hreflang { get; set; }

        /**
         * Anchor name. See the name attribute definition in Html 4.01.
         */
        string Name { get; set; }

        /**
         * Forward link type. See the rel attribute definition in Html 4.01.
         */
        string Rel { get; set; }

        /**
         * Reverse link type. See the rev attribute definition in Html 4.01.
         */
        string Rev { get; set; }

        /**
         * The shape of the active area. The coordinates are given by 
         * <code>coords</code>. See the shape attribute definition in Html 4.01.
         */
        string Shape { get; set; }

        /**
         * Index that represents the element'coreRule position in the tabbing order. See 
         * the tabindex attribute definition in Html 4.01.
         */
        string TabIndex { get; set; }

        /**
         * Frame to render the resource in. See the target attribute definition in 
         * Html 4.01.
         */
        string Target { get; set; }

        /**
         * Advisory content type. See the type attribute definition in Html 4.01.
         */
        string Type { get; set; }

        /**
         * Removes keyboard focus from this element.
         */
        void Blur();

        /**
         * Gives keyboard focus to this element.
         */
        void Focus();
    }
}
