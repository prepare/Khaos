
namespace DOMCore
{

    /**
     * The <code>FORM</code> element encompasses behavior similar to a collection 
     * and an element. It provides direct access to the contained form controls 
     * as well as the attributes of the form element. See the FORM element 
     * definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLFormElement : HTMLElement, IHTMLFormElement
    {
        public HTMLFormElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLFormElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Returns a collection of all form control elements in the form. 
         */
        public HTMLCollection Elements 
        { 
            get 
            { 
                return null; 
            } 
        }

        /**
         * The number of form controls in the form.
         */
        public int Length 
        { 
            get 
            { 
                return 0; 
            } 
        }

        /**
         * Names the form. 
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
         * List of character sets supported by the server. See the accept-charset 
         * attribute definition in Html 4.01.
         */
        public string AcceptCharset 
        {
            get
            {
                return getAttribute("charset");
            }
            set
            {
                setAttribute("charset", value);
            }
        }

        /**
         * Server-side form handler. See the action attribute definition in Html 
         * 4.01.
         */
        public string Action 
        {
            get
            {
                return getAttribute("action");
            }
            set
            {
                setAttribute("action", value);
            }
        }

        /**
         * The content type of the submitted form, generally 
         * "application/x-www-form-urlencoded". See the enctype attribute 
         * definition in Html 4.01. The onsubmit even handler is not guaranteed 
         * to be triggered when invoking this method. The behavior is 
         * inconsistent for historical reasons and authors should not rely on a 
         * particular one. 
         */
        public string Enctype 
        {
            get
            {
                return getAttribute("enctype");
            }
            set
            {
                setAttribute("enctype",value);
            }
        }

        /**
         * HTTP method [<a href='http://www.ietf.org/rfc/rfc2616.txt'>IETF RFC 2616</a>] used to submit form. See the method attribute definition 
         * in Html 4.01.
         */
        public string Method 
        {
            get
            {
                return getAttribute("method");
            }
            set
            {
                setAttribute("method",value);
            }
        }

        /**
         * Frame to render the resource in. See the target attribute definition in 
         * Html 4.01.
         */
        public string Target 
        {
            get
            {
                return getAttribute("target");
            }
            set
            {
                setAttribute("target", value);
            }
        }

        /**
         * Submits the form. It performs the same action as a submit button.
         */
        public void Submit()
        { }

        /**
         * Restores a form element's default values. It performs the same action 
         * as a reset button.
         */
        public void Reset()
        { }

    }

}