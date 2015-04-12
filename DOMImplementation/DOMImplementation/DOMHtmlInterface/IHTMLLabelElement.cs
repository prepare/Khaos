using System;
namespace DOMCore
{
    public interface IHTMLLabelElement : IHTMLElement
    {
        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in Html 4.01.
         */
        string AccessKey { get; set; }

        /**
         * Returns the <code>FORM</code> element containing this control. Returns 
         * <code>null</code> if this control is not within the context of a 
         * form. 
         */
        IHTMLFormElement Form { get; }

        /**
         * This attribute links this label with another form control by 
         * <code>id</code> attribute. See the for attribute definition in Html 
         * 4.01.
         */
        string HtmlFor { get; set; }
    }
}
