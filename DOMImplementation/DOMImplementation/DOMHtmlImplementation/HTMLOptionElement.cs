
namespace DOMCore
{

    /**
     * A selectable choice. See the OPTION element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLOptionElement : HTMLElement, IHTMLOptionElement
    {
        public HTMLOptionElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLOptionElement(TagInfo tag, IDocument doc)
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
         * Represents the value of the Html selected attribute. The value of this 
         * attribute does not change if the state of the corresponding form 
         * control, in an interactive user agent, changes. See the selected 
         * attribute definition in Html 4.01.
         * @version DOM Level 2
         */
        public bool DefaultSelected 
        {
            get
            {
                return (bool.TrueString == getAttribute("selected"));
            }
            set
            {
                setAttribute("selected", value.ToString());
            }
        }

        /**
         * The text contained within the option element. 
         */
        public string Text 
        { 
            get 
            { 
                return null; 
            } 
        }

        /**
         * The index of this <code>OPTION</code> in its parent <code>SELECT</code>
         * , starting from 0.
         * @version DOM Level 2
         */
        public int Index 
        { 
            get 
            { 
                return 0; 
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
         * Option label for use in hierarchical menus. See the label attribute 
         * definition in Html 4.01.
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

        /**
         * Represents the current state of the corresponding form control, in an 
         * interactive user agent. Changing this attribute changes the state of 
         * the form control, but does not change the value of the Html selected 
         * attribute of the element.
         */
        public bool Selected
        {
            get
            {
                return false;
            }
            set
            {
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
                setAttribute("value",value);
            }
        }

    }

}