
namespace DOMCore
{

    /**
     * Push button. See the BUTTON element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLButtonElement : HTMLElement, IHTMLButtonElement
    {
        public HTMLButtonElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLButtonElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Returns the <code>FORM</code> element containing this control. Returns 
         * <code>null</code> if this control is not within the context of a 
         * form. 
         */
        public IHTMLFormElement Form 
        { 
            get
            {
                IHTMLElement tempParent = getParentByTagName("form");
                return (IHTMLFormElement)tempParent;
            } 
        }

        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in Html 4.01.
         */
        public string AccessKey 
        {
            get
            {
                return getAttribute("accesskey");
            }
            set
            {
                setAttribute("accesskey", value);
            }
        }

        /**
         * The control is unavailable in this context. See the disabled attribute 
         * definition in Html 4.01.
         */
        public bool Disabled 
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
         * Form control or object name when submitted with a form. See the name 
         * attribute definition in Html 4.01.
         */
        public string Name 
        {
            get
            {
                return getAttribute("name");
            }
            set
            {
                setAttribute("name", value);
            }
        }

        /**
         * Index that represents the element's position in the tabbing order. See 
         * the tabindex attribute definition in Html 4.01.
         */
        public int TabIndex 
        {
            get
            {
                return int.Parse(getAttribute("tabindex"));
            }
            set
            {
                setAttribute("tabindex", value.ToString());
            }
        }

        /**
         * The type of button (all lower case). See the type attribute definition 
         * in Html 4.01.
         */
        public string Type 
        { 
            get 
            { 
                return getAttribute("type"); 
            } 
        }

        /**
         * The current form control value. See the value attribute definition in 
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
