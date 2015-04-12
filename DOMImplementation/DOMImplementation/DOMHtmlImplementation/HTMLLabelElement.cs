
namespace DOMCore
{

    /**
     * Form field label text. See the LABEL element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLLabelElement : HTMLElement, IHTMLLabelElement
    {
        public HTMLLabelElement(string tag, IDocument doc) 
            : base(tag,doc)
        { }

        public HTMLLabelElement(TagInfo tag, IDocument doc)
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
                return tempParent as IHTMLFormElement; 
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
         * This attribute links this label with another form control by 
         * <code>id</code> attribute. See the for attribute definition in Html 
         * 4.01.
         */
        public string HtmlFor 
        {
            get
            {
                return getAttribute("for");
            }
            set
            {
                setAttribute("for", value);
            }
        }

    }

}