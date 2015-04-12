
namespace DOMCore
{

    /**
     * Multi-line text field. See the TEXTAREA element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTextAreaElement : HTMLElement, IHTMLTextAreaElement
    {
        public HTMLTextAreaElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLTextAreaElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Represents the contents of the element. The value of this attribute 
         * does not change if the contents of the corresponding form control, in 
         * an interactive user agent, changes.
         * @version DOM Level 2
         */
        public string DefaultValue 
        { 
            get 
            { 
                return getAttribute("defaultvalue"); 
            } 
            set 
            { 
                setAttribute("defaultvalue", value); 
            } 
        }

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
         * Width of control (in characters). See the cols attribute definition in 
         * Html 4.01.
         */
        public int Cols 
        { 
            get 
            { 
                return int.Parse(getAttribute("cols")); 
            } 
            set 
            { 
                setAttribute("cols", value.ToString()); 
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
         * This control is read-only. See the readonly attribute definition in 
         * Html 4.01.
         */
        public bool ReadOnly 
        { 
            get 
            { 
                return (bool.TrueString == getAttribute("readonly")); 
            } 
            set 
            { 
                setAttribute("readonly", value.ToString()); 
            } 
        }

        /**
         * Number of text rows. See the rows attribute definition in Html 4.01.
         */
        public int Rows 
        { 
            get 
            { 
                return int.Parse(getAttribute("rows")); 
            } 
            set 
            { 
                setAttribute("rows", value.ToString()); 
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
         * The type of this form control. This the string "textarea".
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
         * Represents the current contents of the corresponding form control, in 
         * an interactive user agent. Changing this attribute changes the 
         * contents of the form control, but does not change the contents of the 
         * element. If the entirety of the data can not fit into a single 
         * <code>DOMString</code>, the implementation may truncate the data.
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

        /**
         * Removes keyboard focus from this element.
         */
        public void Blur() {

        }

        /**
         * Gives keyboard focus to this element.
         */
        public void Focus()
        { 
        }

        /**
         * Select the contents of the <code>TEXTAREA</code>.
         */
        public void Select()
        {
        }

    }

}