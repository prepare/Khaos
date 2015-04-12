using System;
namespace DOMCore
{
    public interface IHTMLOptionElement : IHTMLElement
    {
        /**
         * Represents the value of the Html selected attribute. The value of this 
         * attribute does not change if the state of the corresponding form 
         * control, in an interactive user agent, changes. See the selected 
         * attribute definition in Html 4.01.
         * @version DOM Level 2
         */
        bool DefaultSelected { get; set; }

        /**
         * The control is unavailable in this context. See the disabled attribute 
         * definition in Html 4.01.
         */
        bool Disabled { get; set; }

        /**
         * Returns the <code>FORM</code> element containing this control. Returns 
         * <code>null</code> if this control is not within the context of a 
         * form. 
         */
        IHTMLFormElement Form { get; }

        /**
         * The index of this <code>OPTION</code> in its parent <code>SELECT</code>
         * , starting from 0.
         * @version DOM Level 2
         */
        int Index { get; }

        /**
         * Option label for use in hierarchical menus. See the label attribute 
         * definition in Html 4.01.
         */
        string Label { get; set; }

        /**
         * Represents the current state of the corresponding form control, in an 
         * interactive user agent. Changing this attribute changes the state of 
         * the form control, but does not change the value of the Html selected 
         * attribute of the element.
         */
        bool Selected { get; set; }

        /**
         * The text contained within the option element. 
         */
        string Text { get; }

        /**
         * The current form control value. See the value attribute definition in 
         * Html 4.01.
         */
        string Value { get; set; }
    }
}
