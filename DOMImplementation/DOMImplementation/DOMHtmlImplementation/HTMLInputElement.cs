
namespace DOMCore
{

    /**
     * Form control.Depending upon the environment in which the page is being 
     * viewed, the value property may be read-only for the file upload input 
     * type. For the "password" input type, the actual value returned may be 
     * masked to prevent unauthorized use. See the INPUT element definition in [<a href='http://www.w3.org/TR/1999/REC-html401-19991224'>Html 4.01</a>].
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLInputElement : HTMLElement, IHTMLInputElement
    {
        public HTMLInputElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLInputElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * When the <code>type</code> attribute of the element has the value 
         * "text", "file" or "password", this represents the Html value 
         * attribute of the element. The value of this attribute does not change 
         * if the contents of the corresponding form control, in an interactive 
         * user agent, changes. See the value attribute definition in Html 4.01.
         */
        public string DefaultValue { get; set; }

        /**
         * When <code>type</code> has the value "radio" or "checkbox", this 
         * represents the Html checked attribute of the element. The value of 
         * this attribute does not change if the state of the corresponding form 
         * control, in an interactive user agent, changes. See the checked 
         * attribute definition in Html 4.01.
         */
        public bool DefaultChecked
        {
            get
            {
                return (bool.TrueString == getAttribute("checked"));
            }
            set
            {
                setAttribute("checked", value.ToString());
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
         * A comma-separated list of content types that a server processing this 
         * form will handle correctly. See the accept attribute definition in 
         * Html 4.01.
         */
        public string Accept { get; set; }

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
         * Aligns this object (vertically or horizontally) with respect to its 
         * surrounding text. See the align attribute definition in Html 4.01. 
         * This attribute is deprecated in Html 4.01.
         */
        public string Align
        {
            get
            {
                return getAttribute("align");
            }
            set
            {
                setAttribute("align", value);
            }
        }

        /**
         * Alternate text for user agents not rendering the normal content of this 
         * element. See the alt attribute definition in Html 4.01.
         */
        public string Alt
        {
            get
            {
                return getAttribute("alt");
            }
            set
            {
                setAttribute("alt", value);
            }
        }

        /**
         * When the <code>type</code> attribute of the element has the value 
         * "radio" or "checkbox", this represents the current state of the form 
         * control, in an interactive user agent. Changes to this attribute 
         * change the state of the form control, but do not change the value of 
         * the Html checked attribute of the INPUT element.During the handling 
         * of a click event on an input element with a type attribute that has 
         * the value "radio" or "checkbox", some implementations may change the 
         * value of this property before the event is being dispatched in the 
         * document. If the default action of the event is canceled, the value 
         * of the property may be changed back to its original value. This means 
         * that the value of this property during the handling of click events 
         * is implementation dependent.
         */
        public bool Checked
        {
            get
            {
                return (bool.TrueString == getAttribute("checked"));
            }
            set
            {
                setAttribute("checked", value.ToString());
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
                string result = getAttribute("disabled");
                return result != null? (bool.TrueString == result) : true;
            }
            set
            {
                setAttribute("disabled", value.ToString());
            } 
        }

        /**
         * Maximum number of characters for text fields, when <code>type</code> 
         * has the value "text" or "password". See the maxlength attribute 
         * definition in Html 4.01.
         */
        public int MaxLength 
        {
            get
            {
                int result;
                if (int.TryParse(getAttribute("maxlength"), out result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                setAttribute("maxlength", value.ToString());
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
         * This control is read-only. Relevant only when <code>type</code> has the 
         * value "text" or "password". See the readonly attribute definition in 
         * Html 4.01.
         */
        public bool ReadOnly
        {
            get
            {
                string result = getAttribute("readonly");
                return result != null ? (bool.TrueString == result) : false;
            }
            set
            {
                setAttribute("readonly", value.ToString());
            }
        }

        /**
         * Size information. The precise meaning is specific to each type of 
         * field. See the size attribute definition in Html 4.01.
         * @version DOM Level 2
         */
        public int Size
        {
            get
            {
                return int.Parse(getAttribute("size"));
            }
            set
            {
                setAttribute("size", value.ToString());
            }
        }

        /**
         * When the <code>type</code> attribute has the value "image", this 
         * attribute specifies the location of the image to be used to decorate 
         * the graphical submit button. See the src attribute definition in Html 
         * 4.01.
         */
        public string Src
        {
            get
            {
                return getAttribute("src");
            }
            set
            {
                setAttribute("src", value);
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
         * The type of control created (all lower case). See the type attribute 
         * definition in Html 4.01.
         * @version DOM Level 2
         */
        public string Type 
        {
            get
            {
                return getAttribute("type");
            } 
            set
            {
                setAttribute("type",value);
            } 
        }

        /**
         * Use client-side image map. See the usemap attribute definition in Html 
         * 4.01.
         */
        public string UseMap
        {
            get
            {
                return getAttribute("usemap");
            }
            set
            {
                setAttribute("usemap", value);
            }
        }

        /**
         * When the <code>type</code> attribute of the element has the value 
         * "text", "file" or "password", this represents the current contents of 
         * the corresponding form control, in an interactive user agent. 
         * Changing this attribute changes the contents of the form control, but 
         * does not change the value of the Html value attribute of the element. 
         * When the <code>type</code> attribute of the element has the value 
         * "button", "hidden", "submit", "reset", "image", "checkbox" or 
         * "radio", this represents the Html value attribute of the element. See 
         * the value attribute definition in Html 4.01.
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
        public void Blur()
        { }

        /**
         * Gives keyboard focus to this element.
         */
        public void Focus()
        { }

        /**
         * Select the contents of the text area. For <code>INPUT</code> elements 
         * whose <code>type</code> attribute has one of the following values: 
         * "text", "file", or "password".
         */
        public void Select()
        { }

        /**
         * Simulate a mouse-click. For <code>INPUT</code> elements whose 
         * <code>type</code> attribute has one of the following values: 
         * "button", "checkbox", "radio", "reset", or "submit".
         */
        public void Click()
        { }

    }

}