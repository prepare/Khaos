using System;
namespace DOMCore
{
    public interface IHTMLTextAreaElement : IHTMLElement
    {
        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in Html 4.01.
         */
        string AccessKey { get; set; }

        /**
         * Removes keyboard focus from this element.
         */
        void Blur();

        /**
         * Width of control (in characters). See the cols attribute definition in 
         * Html 4.01.
         */
        int Cols { get; set; }

        /**
         * Represents the contents of the element. The value of this attribute 
         * does not change if the contents of the corresponding form control, in 
         * an interactive user agent, changes.
         * @version DOM Level 2
         */
        string DefaultValue { get; set; }

        /**
         * The control is unavailable in this context. See the disabled attribute 
         * definition in Html 4.01.
         */
        bool Disabled { get; set; }

        /**
         * Gives keyboard focus to this element.
         */
        void Focus();

        /**
         * Returns the <code>FORM</code> element containing this control. Returns 
         * <code>null</code> if this control is not within the context of a 
         * form. 
         */
        IHTMLFormElement Form { get; }

        /**
         * Form control or object name when submitted with a form. See the name 
         * attribute definition in Html 4.01.
         */
        string Name { get; set; }

        /**
         * This control is read-only. See the readonly attribute definition in 
         * Html 4.01.
         */
        bool ReadOnly { get; set; }

        /**
         * Number of text rows. See the rows attribute definition in Html 4.01.
         */
        int Rows { get; set; }

        /**
         * Select the contents of the <code>TEXTAREA</code>.
         */
        void Select();

        /**
         * Index that represents the element's position in the tabbing order. See 
         * the tabindex attribute definition in Html 4.01.
         */
        int TabIndex { get; set; }

        /**
         * The type of this form control. This the string "textarea".
         */
        string Type { get; set; }

        /**
         * Represents the current contents of the corresponding form control, in 
         * an interactive user agent. Changing this attribute changes the 
         * contents of the form control, but does not change the contents of the 
         * element. If the entirety of the data can not fit into a single 
         * <code>DOMString</code>, the implementation may truncate the data.
         */
        string Value { get; set; }
    }
}
