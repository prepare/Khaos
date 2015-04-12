
namespace DOMCore
{

    /**
     * Generic block container. See the DIV element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLDivElement : HTMLElement, IHTMLDivElement
    {
        public HTMLDivElement(string tag, IDocument doc)
            : base(tag,doc)
        { }

        public HTMLDivElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Horizontal text alignment. See the align attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        public string Align {
            set { setAttribute("align", value); } 
            get { return getAttribute("align"); } 
        }
        
    }

}