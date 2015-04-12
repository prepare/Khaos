
namespace DOMCore
{

    /**
     * Table caption See the CAPTION element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTableCaptionElement : HTMLElement, IHTMLTableCaptionElement
    {
        public HTMLTableCaptionElement(string tag, IDocument doc)
            : base(tag,doc)
        { }

        public HTMLTableCaptionElement(TagInfo tag, IDocument doc)
            : base(tag,doc)
        { }

        /**
         * Caption alignment with respect to the table. See the align attribute 
         * definition in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public string Align
        {
            get
            {
                return this.getAttribute("align");
            }
            set
            {
                this.setAttribute("align", value);
            }
        }

    }

}