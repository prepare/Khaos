using System.Xml;
namespace DOMCore
{

    /**
     * Local change to font. See the FONT element definition in Html 4.01. This 
     * element is deprecated in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLFontElement : HTMLElement, IHTMLFontElement
    {
        public HTMLFontElement(string tag, IDocument doc)
            : base(tag,doc)
        { }

        public HTMLFontElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Font color. See the color attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        public string Color {
            get { return getAttribute("color"); }
            set { setAttribute("color", value); }
        }

        /**
         * Font face identifier. See the face attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Face {
            get { return getAttribute("face"); }
            set { setAttribute("face", value); }
        }

        /**
         * Font size. See the size attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        public string Size {
            get { return getAttribute("size"); }
            set { setAttribute("size", value); }
        }

    }

}