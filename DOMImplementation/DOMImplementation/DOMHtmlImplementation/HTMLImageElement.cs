
namespace DOMCore
{

    /**
     * Embedded image. See the IMG element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLImageElement : HTMLElement, IHTMLImageElement
    {
        public HTMLImageElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLImageElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * The name of the element (for backwards compatibility). 
         */
        public string Name 
        {
            get
            {
                return getAttribute("name");
            }
            set
            {
                setAttribute("name",value);
            }
        }

        /**
         * Aligns this object (vertically or horizontally) with respect to its 
         * surrounding text. See the align attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Align 
        {
            get
            {
                return getAttribute("align");
            }
            set
            {
                setAttribute("align", value);
            }
        }

        /**
         * Alternate text for user agents not rendering the normal content of this 
         * element. See the alt attribute definition in Html 4.01.
         */
        public string Alt 
        {
            get
            {
                return getAttribute("alt");
            }
            set
            {
                setAttribute("alt", value);
            }
        }

        /**
         * Width of border around image. See the  border attribute definition in 
         * Html 4.01. This attribute is deprecated in Html 4.01. Note that the 
         * type of this attribute was <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>]
         * .
         */
        public string Border 
        {
            get
            {
                return getAttribute("border");
            }
            set
            {
                setAttribute("border",value);
            }
        }

        /**
         * Height of the image in pixels. See the height attribute definition in 
         * Html 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        public string Height 
        {
            get
            {
                return getAttribute("height");
            }
            set
            {
                setAttribute("height", value);
            }
        }

        /**
         * Horizontal space to the left and right of this image in pixels. See the 
         * hspace attribute definition in Html 4.01. This attribute is 
         * deprecated in Html 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        public string Hspace 
        {
            get
            {
                return getAttribute("hspace");
            }
            set
            {
                setAttribute("hspace",value);
            }
        }

        /**
         * Use server-side image map. See the ismap attribute definition in Html 
         * 4.01.
         */
        public bool IsMap 
        {
            get
            {
                return (bool.TrueString == getAttribute("ismap"));
            }
            set
            {
                setAttribute("ismap",value.ToString());
            }
        }

        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating a long description of this image or frame. See the 
         * longdesc attribute definition in Html 4.01.
         */
        public string LongDesc 
        {
            get
            {
                return getAttribute("longdesc");
            }
            set
            {
                setAttribute("longdesc",value);
            }
        }

        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating the source of this image. See the src attribute 
         * definition in Html 4.01.
         */
        public string Src 
        {
            get
            {
                return getAttribute("src");
            }
            set
            {
                setAttribute("src",value);
            }
        }

        /**
         * Use client-side image map. See the usemap attribute definition in Html 
         * 4.01.
         */
        public string UseMap 
        {
            get
            {
                return getAttribute("usemap");
            }
            set
            {
                setAttribute("usemap",value);
            }
        }

        /**
         * Vertical space above and below this image in pixels. See the vspace 
         * attribute definition in Html 4.01. This attribute is deprecated in 
         * Html 4.01. Note that the type of this attribute was "DOMString" in 
         * DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        public int Vspace 
        {
            get
            {
                return int.Parse(getAttribute("vspace"));
            }
            set
            {
                setAttribute("vspace",value.ToString());
            }
        }

        /**
         * The width of the image in pixels. See the width attribute definition in 
         * Html 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        public string Width 
        {
            get
            {
                return getAttribute("width");
            }
            set
            {
                setAttribute("width",value);
            }
        }
    }

}