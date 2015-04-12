
namespace DOMCore
{

    /**
     * This contains generic meta-information about the document. See the META 
     * element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLMetaElement : HTMLElement, IHTMLMetaElement
    {
        public HTMLMetaElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLMetaElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Associated information. See the content attribute definition in Html 
         * 4.01.
         */
        public string Content 
        {
            get
            {
                return getAttribute("content");
            }
            set
            {
                setAttribute("content", value);
            }
        }

        /**
         * HTTP response header name [<a href='http://www.ietf.org/rfc/rfc2616.txt'>IETF RFC 2616</a>]. See the http-equiv attribute definition in 
         * Html 4.01.
         */
        public string HttpEquiv 
        {
            get
            {
                return getAttribute("http-equiv");
            }
            set
            {
                setAttribute("http-equiv", value);
            }
        }

        /**
         * Meta information name. See the name attribute definition in Html 4.01.
         */
        public string Name 
        {
            get
            {
                return getAttribute("name");
            }
            set
            {
                setAttribute("name", value);
            }
        }

        /**
         * Select form of content. See the scheme attribute definition in Html 
         * 4.01.
         */
        public string Scheme 
        {
            get
            {
                return getAttribute("scheme");
            }
            set
            {
                setAttribute("scheme",value);
            }
        }
    }
}