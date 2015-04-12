using System;
namespace DOMCore
{
    public interface IHTMLImageElement : IHTMLElement
    {
        /**
         * Aligns this object (vertically or horizontally) with respect to its 
         * surrounding text. See the align attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        string Align { get; set; }

        /**
         * Alternate text for user agents not rendering the normal content of this 
         * element. See the alt attribute definition in Html 4.01.
         */
        string Alt { get; set; }

        /**
         * Width of border around image. See the  border attribute definition in 
         * Html 4.01. This attribute is deprecated in Html 4.01. Note that the 
         * type of this attribute was <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>]
         * .
         */
        string Border { get; set; }

        /**
         * Height of the image in pixels. See the height attribute definition in 
         * Html 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        string Height { get; set; }

        /**
         * Horizontal space to the left and right of this image in pixels. See the 
         * hspace attribute definition in Html 4.01. This attribute is 
         * deprecated in Html 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        string Hspace { get; set; }

        /**
         * Use server-side image map. See the ismap attribute definition in Html 
         * 4.01.
         */
        bool IsMap { get; set; }

        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating a long description of this image or frame. See the 
         * longdesc attribute definition in Html 4.01.
         */
        string LongDesc { get; set; }

        /**
         * The name of the element (for backwards compatibility). 
         */
        string Name { get; set; }

        /**
         * URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] designating the source of this image. See the src attribute 
         * definition in Html 4.01.
         */
        string Src { get; set; }

        /**
         * Use client-side image map. See the usemap attribute definition in Html 
         * 4.01.
         */
        string UseMap { get; set; }

        /**
         * Vertical space above and below this image in pixels. See the vspace 
         * attribute definition in Html 4.01. This attribute is deprecated in 
         * Html 4.01. Note that the type of this attribute was "DOMString" in 
         * DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        int Vspace { get; set; }

        /**
         * The width of the image in pixels. See the width attribute definition in 
         * Html 4.01. Note that the type of this attribute was 
         * <code>DOMString</code> in DOM Level 1 Html [<a href='http://www.w3.org/TR/1998/REC-DOM-Level-1-19981001'>DOM Level 1</a>].
         * @version DOM Level 2
         */
        string Width { get; set; }
    }
}
