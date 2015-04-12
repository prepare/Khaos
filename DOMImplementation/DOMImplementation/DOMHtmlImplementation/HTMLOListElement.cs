
namespace DOMCore
{

    /**
     * Ordered list. See the OL element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLOListElement : HTMLElement, IHTMLOListElement
    {
        public HTMLOListElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLOListElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Reduce spacing between list items. See the compact attribute definition 
         * in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public bool Compact 
        { 
            get 
            { 
                return (bool.TrueString == getAttribute("compact")); 
            } 
            set 
            { 
                setAttribute("compact", value.ToString()); 
            } 
        }

        /**
         * Starting sequence number. See the start attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        public int Start 
        { 
            get 
            { 
                return int.Parse(getAttribute("start")); 
            } 
            set 
            { 
                setAttribute("start", value.ToString()); 
            } 
        }

        /**
         * Numbering style. See the type attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
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
    }

}