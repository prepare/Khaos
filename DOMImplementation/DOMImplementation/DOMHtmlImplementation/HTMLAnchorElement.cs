
namespace DOMCore
{

    //'http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'

    public class HTMLAnchorElement : HTMLElement, IHTMLAnchorElement
    {
        public HTMLAnchorElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLAnchorElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * A single character access key to give access to the form control. See 
         * the accesskey attribute definition in Html 4.01.
         */
        public string AccessKey 
        { 
            get 
            { 
                return this.getAttribute("accesskey"); 
            } 
            set 
            { 
                this.setAttribute("accesskey", value); 
            } 
        }

        /**
         * The character encoding of the linked resource. See the charset 
         * attribute definition in Html 4.01.
         */
        public string Charset 
        { 
            get 
            { 
                return this.getAttribute("charset"); 
            } 
            set 
            { 
                this.setAttribute("charset", value); 
            } 
        }

        /**
         * Comma-separated list of lengths, defining an active region geometry. 
         * See also <code>shape</code> for the shape of the region. See the 
         * coords attribute definition in Html 4.01.
         */
        public string Coords 
        { 
            get 
            { 
                return this.getAttribute("coords"); 
            } 
            set 
            { 
                this.setAttribute("coords", value); 
            } 
        }

        /**
         * The absolute URI [<a href='http://www.ietf.org/rfc/rfc2396.txt'>IETF RFC 2396</a>] of the linked resource. See the href attribute 
         * definition in Html 4.01.
         */
        public string Href 
        { 
            get 
            { 
                return this.getAttribute("href"); 
            } 
            set 
            { 
                this.setAttribute("href", value); 
            } 
        }

        /**
         * Language code of the linked resource. See the hreflang attribute 
         * definition in Html 4.01.
         */
        public string Hreflang 
        { 
            get 
            { 
                return this.getAttribute("hreflang"); 
            } 
            set 
            { 
                this.setAttribute("hreflang", value); 
            } 
        }

        /**
         * Anchor name. See the name attribute definition in Html 4.01.
         */
        public string Name 
        { 
            get 
            { 
                return this.getAttribute("name"); 
            } 
            set 
            { 
                this.setAttribute("name", value); 
            } 
        }

        /**
         * Forward link type. See the rel attribute definition in Html 4.01.
         */
        public string Rel 
        { 
            get 
            { 
                return this.getAttribute("rel"); 
            } 
            set 
            { 
                this.setAttribute("rel", value); 
            } 
        }

        /**
         * Reverse link type. See the rev attribute definition in Html 4.01.
         */
        public string Rev 
        { 
            get 
            { 
                return this.getAttribute("rev"); 
            } 
            set 
            { 
                this.setAttribute("rev", value); 
            } 
        }

        /**
         * The shape of the active area. The coordinates are given by 
         * <code>coords</code>. See the shape attribute definition in Html 4.01.
         */
        public string Shape 
        { 
            get 
            { 
                return this.getAttribute("shape"); 
            } 
            set 
            { 
                this.setAttribute("shape", value); 
            } 
        }

        /**
         * Index that represents the element'coreRule position in the tabbing order. See 
         * the tabindex attribute definition in Html 4.01.
         */
        public string TabIndex 
        { 
            get 
            { 
                return this.getAttribute("tabindex"); 
            } 
            set 
            { 
                this.setAttribute("tabindex", value); 
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
                return this.getAttribute("target"); 
            } 
            set 
            { 
                this.setAttribute("target", value); 
            } 
        }

        /**
         * Advisory content type. See the type attribute definition in Html 4.01.
         */
        public string Type 
        { 
            get 
            { 
                return this.getAttribute("type"); 
            }
            set 
            { 
                this.setAttribute("type", value); 
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

    }
}