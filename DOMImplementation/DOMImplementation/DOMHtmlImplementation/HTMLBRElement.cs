
namespace DOMCore
{

    /**
     * Force a line break. See the BR element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLBRElement : HTMLElement, IHTMLBRElement
    {
        public HTMLBRElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLBRElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Control flow of text around floats. See the clear attribute definition 
         * in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public string Clear 
        {
            get
            {
                return getAttribute("clear");
            }
            set
            {
                setAttribute("clear", value);
            }
        }

    }
}