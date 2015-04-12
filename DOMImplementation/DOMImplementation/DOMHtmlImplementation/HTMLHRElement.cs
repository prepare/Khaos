
namespace DOMCore
{

    /**
     * Create a horizontal rule. See the HR element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLHRElement : HTMLElement, IHTMLHRElement
    {
        public HTMLHRElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLHRElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Align the rule on the page. See the align attribute definition in Html 
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
                setAttribute("align", value); 
            } 
        }

        /**
         * Indicates to the user agent that there should be no shading in the 
         * rendering of this element. See the noshade attribute definition in 
         * Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public string NoShade 
        { 
            get 
            { 
                return getAttribute("noshade"); 
            } 
            set 
            { 
                setAttribute("noshade", value); 
            } 
        }


        /**
         * The height of the rule. See the size attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Size 
        { 
            get 
            { 
                return getAttribute("size"); 
            } 
            set 
            { 
                setAttribute("size", value); 
            } 
        }

        /**
         * The width of the rule. See the width attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Width 
        { 
            get 
            { 
                return getAttribute("width"); 
            } 
            set 
            { 
                setAttribute("width", value); 
            } 
        }


    }

}