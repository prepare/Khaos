
namespace DOMCore
{

    /**
     * Root of an Html document. See the Html element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLHtmlElement : HTMLElement, IHTMLHtmlElement
    {
        public HTMLHtmlElement(string tag, IDocument doc) : base(tag, doc)
        { }

        public HTMLHtmlElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Version information about the document'coreRule DTD. See the version attribute 
         * definition in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public string Version 
        { 
            get 
            { 
                return this.getAttribute("version"); 
            } 
            set 
            { 
                this.setAttribute("version", value); 
            } 
        }

    }

}