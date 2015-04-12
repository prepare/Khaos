
namespace DOMCore
{

    /**
     * For the <code>H1</code> to <code>H6</code> elements. See the H1 element 
     * definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLHeadingElement : HTMLElement, IHTMLHeadingElement
    {
        public HTMLHeadingElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLHeadingElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Horizontal text alignment. See the align attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        public string Align 
        { 
            get
            {
                return getAttribute("align");
            }
            set
            {
                setAttribute("align",value);
            }
        }
    }

}