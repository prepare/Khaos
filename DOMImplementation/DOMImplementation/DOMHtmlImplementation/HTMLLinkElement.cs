
namespace DOMCore
{

    /**
     * The <code>LINK</code> element specifies a link to an external resource, and 
     * defines this document's relationship to that resource (or vice versa). 
     * See the LINK element definition in Html 4.01 (see also the 
     * <code>LinkStyle</code> class in the StyleSheet module [<a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-CSSStyle-20001113'>DOM Level 2 CSSStyle Sheets and CSS</a>]).
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLLinkElement : HTMLElement, IHTMLLinkElement
    {
        public HTMLLinkElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLLinkElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Enables/disables the link. This is currently only used for style sheet 
         * links, and may be used to activate or deactivate style sheets. 
         */
        public bool Disabled 
        {
            get
            {
                return (bool.TrueString == getAttribute("disabled"));
            }
            set
            {
                setAttribute("disabled", value.ToString());
            }
        }

        /**
         * The character encoding of the resource being linked to. See the charset 
         * attribute definition in Html 4.01.
         */
        public string Charset 
        {
            get
            {
                return getAttribute("charset");
            }
            set
            {
                setAttribute("charset", value);
            }
        }

        /**
         * The URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute definition in 
         * Html 4.01.
         */
        public string Href 
        {
            get
            {
                return getAttribute("href");
            }
            set
            {
                setAttribute("href",value);
            }
        }

        /**
         * Language code of the linked resource. See the hreflang attribute 
         * definition in Html 4.01.
         */
        public string Hreflang 
        {
            get
            {
                return getAttribute("hreflang");
            }
            set
            {
                setAttribute("hreflang",value);
            }
        }

        /**
         * Designed for use with one or more target media. See the media attribute 
         * definition in Html 4.01.
         */
        public string Media 
        {
            get
            {
                return getAttribute("media");
            }
            set
            {
                setAttribute("media",value);
            }
        }

        /**
         * Forward link type. See the rel attribute definition in Html 4.01.
         */
        public string Rel 
        {
            get
            {
                return getAttribute("rel");
            }
            set
            {
                setAttribute("rel", value);
            }
        }

        /**
         * Reverse link type. See the rev attribute definition in Html 4.01.
         */
        public string Rev 
        {
            get
            {
                return getAttribute("rev");
            }
            set
            {
                setAttribute("rev", value);
            }
        }

        /**
         * Frame to render the resource in. See the target attribute definition in 
         * Html 4.01.
         */
        public string Target 
        {
            get
            {
                return getAttribute("target");
            }
            set
            {
                setAttribute("target", value);
            }
        }

        /**
         * Advisory content type. See the type attribute definition in Html 4.01.
         */
        public string Type 
        {
            get
            {
                return getAttribute("type");
            }
            set
            {
                setAttribute("type",value);
            }
        }
    }
}