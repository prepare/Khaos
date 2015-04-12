
namespace DOMCore
{
    /**
     * Unordered list. See the UL element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLUListElement : HTMLElement, IHTMLUListElement
    {
        public HTMLUListElement(string tag, IDocument doc)
            : base(tag,doc)
        { }

        public HTMLUListElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Reduce spacing between list items. See the compact attribute definition 
         * in Html 4.01. This attribute is deprecated in Html 4.01.
         */
        public bool Compact {
            get 
            {
                if (getAttribute("compact") == "true")
                    return true;
                else
                    return false;
            }
            set 
            { 
                setAttribute("compact", value.ToString()); 
            }
        }

        /**
         * Bullet style. See the type attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        public string Type {
            get { return getAttribute("type"); }
            set { setAttribute("type", value); }
        }

    }

}