
namespace DOMCore
{

    /**
     * Group options together in logical subdivisions. See the OPTGROUP element 
     * definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLOptGroupElement : HTMLElement, IHTMLOptGroupElement
    {
        public HTMLOptGroupElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLOptGroupElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * The control is unavailable in this context. See the disabled attribute 
         * definition in Html 4.01.
         */
        public bool Disables 
        { 
            get 
            { 
                return (bool.TrueString == getAttribute("disabled")); 
            } 
            set 
            { 
                setAttribute("disabled", value.ToString()); 
            } 
        }

        /**
         * Assigns a label to this option group. See the label attribute definition
         *  in Html 4.01.
         */
        public string Label 
        { 
            get 
            { 
                return getAttribute("label"); 
            } 
            set 
            { 
                setAttribute("label", value); 
            } 
        }
    }

}