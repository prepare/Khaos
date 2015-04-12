
namespace DOMCore
{

    /**
     * The Html document body. This element is always present in the DOM API, even 
     * if the tags are not present in the source document. See the BODY element 
     * definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLBodyElement : HTMLElement, IHTMLBodyElement
    {
        public HTMLBodyElement(string tag, IDocument doc)
            : base(tag,doc)
        { }

        public HTMLBodyElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Color of active links (after mouse-button down, but before mouse-button 
         * up). See the alink attribute definition in Html 4.01. This attribute 
         * is deprecated in Html 4.01.
         */
        public string ALink {
            get { return getAttribute("alink"); }
            set { setAttribute("alink", value); }
        }

        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the background texture tile image. See the background attribute 
         * definition in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public string Background {
            get { return getAttribute("background"); }
            set { setAttribute("background", value); }
        }

        /**
         * Document background color. See the bgcolor attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        public string BgColor {
            get { return getAttribute("bgcolor"); }
            set { setAttribute("bgcolor", value); }
        }

        /**
         * Color of links that are not active and unvisited. See the link 
         * attribute definition in Html 4.01. This attribute is deprecated in 
         * Html 4.01.
         */
        public string Link {
            get { return getAttribute("link"); }
            set { setAttribute("link", value); }
        }

        /**
         * Document text color. See the text attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Text {
            get { return getAttribute("text"); }
            set { setAttribute("text", value); }
        }

        /**
         * Color of links that have been visited by the user. See the vlink 
         * attribute definition in Html 4.01. This attribute is deprecated in 
         * Html 4.01.
         */
        public string VLink {
            get { return getAttribute("vlink"); }
            set { setAttribute("vlink", value); }
        }

    }
}
