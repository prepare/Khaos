
namespace DOMCore
{

    /**
     * List item. See the LI element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLLIElement : HTMLElement, IHTMLLIElement
    {
        public HTMLLIElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLLIElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * List item bullet style. See the type attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Type 
        { 
            get 
            { 
                return getAttribute("type"); 
            } 
            set 
            { 
                setAttribute("type", value); 
            } 
        }

        /**
         * Reset sequence number when used in <code>OL</code>. See the value 
         * attribute definition in Html 4.01. This attribute is deprecated in 
         * Html 4.01.
         */
        public string Value 
        { 
            get 
            { 
                return getAttribute("value"); 
            } 
            set 
            { 
                setAttribute("value", value); 
            } 
        }
    }

}