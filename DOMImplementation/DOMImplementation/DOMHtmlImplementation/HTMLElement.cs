using System;
using System.Collections.Generic;
using System.Text;
using CSS;
using CascadingStyleSheets;
using PhoenixOI;

namespace DOMCore
{
    public class HTMLElement : Element, IHTMLElement
    {
        protected Style myStyle;
        protected Request request;

        public HTMLElement(string tag, IDocument doc) : base(tag,doc)
        {
        }

        internal Request Request
        {
            set { this.request = value; }   
        }

        public HTMLElement(TagInfo al, IDocument doc)
            : base(al.Name,doc)
        {
            //add all existing attributes to element
            for (int i = 0; i < al.Attributes.Count; i++)
            {
                setAttribute(al.Attributes.GetKey(i), al.Attributes.Get(i));
            }
        }

        /// <summary>
        /// The element'coreRule identifier. See the id attribute definition  in HTML 4.0.
        /// </summary>
        public string Id
        {
            get
            {
                return getAttribute("id");
            }
            set
            {
                setAttribute("id", value);
            }
        }

        /// <summary>
        /// The element'coreRule advisory title. See the title attribute definition  in HTML 4.0.
        /// </summary>
        public string Title
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        /// <summary>
        /// Language code defined in RFC 1766. See the lang attribute definition  in HTML 4.0.
        /// </summary>
        public string Lang
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        /// <summary>
        /// Specifies the base direction of directionally neutral text and the directionality of tables. See the dir attribute definition  in HTML 4.0.
        /// </summary>
        public string Dir
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        /// <summary>
        /// The class attribute of the element. This attribute has been renamed due to conflicts with the "class" keyword exposed by many languages. See the class attribute definition  in HTML 4.0.
        /// </summary>
        public string ClassName
        {
            get
            {
                return getAttribute("class");
            }
            set
            {
                setAttribute("class", value);
            }
        }

        /// <summary>
        /// Get first parent node with specified tag name.
        /// </summary>
        /// <param name="tname">Tag name of the parent to find.</param>
        /// <returns>The first parent node with given tag name or null.</returns>
        public IHTMLElement getParentByTagName(string tname)
        {
            INode parent = null;
            while (this.ParentNode != null)
            {
                parent = this.ParentNode;
                if (parent.NodeName == tname)
                {
                    return parent as IHTMLElement;
                }
            }
            return null;
        }

        public Style CSSStyle
        {
            get
            {
                if (myStyle != null)
                    return myStyle;
                else
                {
                    HTMLDocument htmlDoc = this.OwnerDocument as HTMLDocument;
                    if (htmlDoc != null)
                    {
                        Style inlineStyle = CSSParser.ParseStyleBody(getAttribute("style"), request);
                        myStyle = htmlDoc.CSSMgr.GetStyleFor(this);

                        //try to inherit stuff from parent
                        Style parentStyle = null;
                        if (ParentNode != null && ParentNode is IHTMLElement)
                        {
                            parentStyle = ((IHTMLElement)ParentNode).CSSStyle;
                            myStyle.InheritFrom(parentStyle);
                        }
                        inlineStyle.CascadeFrom(myStyle);
                        return inlineStyle;
                    }
                }
                return null;
            }
        }
    }
}
