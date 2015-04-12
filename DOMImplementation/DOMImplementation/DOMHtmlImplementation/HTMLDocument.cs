using System;
using System.Collections.Generic;
using System.Text;
using CSS;

namespace DOMCore
{
    public class HTMLDocument : Document, IHTMLDocument
    {
        public CSSManager cssMgr;

        public CSSManager CSSMgr
        {
            get
            {
                return cssMgr;
            }
        }

        /// <summary>
        /// The title of a document as specified by the TITLE element in the head of the document.
        /// </summary>
        public string Title
        {
            get
            {
                NodeList nl = getElementsByTagName("title");
                if (nl.Length > 0)
                {
                    HTMLTitleElement t = (HTMLTitleElement)nl[0];
                    return t.Text;
                }
                return "";
            }
            set
            {
            }
        }

        /// <summary>
        /// Returns the URI of the page that linked to this page. The value is an empty string if the user navigated to the page directly (not through a link, but, for example, via a bookmark).
        /// </summary>
        public string Referrer
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// The domain name of the server that served the document, or a null string if the server cannot be identified by a domain name.
        /// </summary>
        public string Domain
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// The complete URI of the document.
        /// </summary>
        public string URL
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// The element that contains the content for the document. In documents with BODY contents, returns the BODY element, and in frameset documents, this returns the outermost FRAMESET element.
        /// </summary>
        public IHTMLElement Body
        {
            get
            {
                NodeList nl = getElementsByTagName("frameset");
                if (nl.Length > 0)
                    return (IHTMLElement)nl[0];

                nl = getElementsByTagName("body");
                if (nl.Length > 0)
                    return (IHTMLElement)nl[0];

                return null;
            }
            set
            {
            }
        }

        /// <summary>
        /// A collection of all the IMG elements in a document. The behavior is limited to IMG elements for backwards compatibility.
        /// </summary>
        public HTMLCollection Images
        {
            get
            {
                NodeList nl = getElementsByTagName("img");
                return new HTMLCollection(nl);
            }
        }

        /// <summary>
        /// A collection of all the OBJECT elements that include applets and APPLET (deprecated) elements in a document.
        /// </summary>
        public HTMLCollection Applets
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// A collection of all AREA elements and anchor (A) elements in a document with a value for the href attribute.
        /// </summary>
        public HTMLCollection Links
        {
            get
            {
                List<INode> lnl = new List<INode>();
                NodeList nl = getElementsByTagName("area");
                for (int i = 0; i < nl.Length; i++)
                {
                    if (nl[i] is IHTMLElement && ((IHTMLElement)nl[i]).getAttribute("href") != "")
                    {
                        lnl.Add(nl[i]);
                    }
                }
                nl = getElementsByTagName("a");
                for (int i = 0; i < nl.Length; i++)
                {
                    if (nl[i] is IHTMLElement && ((IHTMLElement)nl[i]).getAttribute("href") != "")
                    {
                        lnl.Add(nl[i]);
                    }
                }
                return new HTMLCollection(lnl);
            }
        }

        /// <summary>
        /// A collection of all the forms of a document.
        /// </summary>
        public HTMLCollection Forms
        {
            get
            {
                NodeList nl = getElementsByTagName("form");
                return new HTMLCollection(nl);
            }
        }

        /// <summary>
        /// A collection of all the anchor (A) elements in a document with a value for the name attribute.Note. For reasons of backwards compatibility, the returned set of anchors only contains those anchors created with the name attribute, not those created with the id attribute.
        /// </summary>
        public HTMLCollection Anchors
        {
            get
            {
                List<INode> lnl = new List<INode>();
                NodeList nl = getElementsByTagName("a");
                for (int i = 0; i < nl.Length; i++)
                {
                    if (nl[i] is IHTMLElement && ((IHTMLElement)nl[i]).getAttribute("name") != "")
                    {
                        lnl.Add(nl[i]);
                    }
                }
                return new HTMLCollection(lnl);
            }
        }

        /// <summary>
        /// The cookies associated with this document. If there are none, the value is an empty string. Otherwise, the value is a string: a semicolon-delimited list of "name, value" pairs for all the cookies associated with the page. For example, name=value;expires=date.
        /// </summary>
        public string Cookie
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
        /// Open a document stream for writing. If a document exists in the target, this method clears it.
        /// </summary>
        public void open()
        {
        }

        /// <summary>
        /// Closes a document stream opened by open() and forces rendering.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Write a string of text to a document stream opened by open(). The text is parsed into the document'coreRule structure model.
        /// </summary>
        /// <param name="text">The string to be parsed into some structure in the document structure model.</param>
        public void write(string text)
        {
        }

        /// <summary>
        /// Write a string of text followed by a newline character to a document stream opened by open(). The text is parsed into the document'coreRule structure model.
        /// </summary>
        /// <param name="text">The string to be parsed into some structure in the document structure model.</param>
        public void writeln(string text)
        {
        }

        /// <summary>
        /// Returns the Element whose id is given by elementId. If no such element exists, returns null. Behavior is not defined if more than one element has this id.
        /// </summary>
        /// <param name="elementId">The unique id value for an element.</param>
        /// <returns>The matching element.</returns>
        public Element getElementById(string elementId)
        {
            return null;
        }

        /// <summary>
        /// Returns the (possibly empty) collection of elements whose name value is given by elementName.
        /// </summary>
        /// <param name="elementName">The name attribute value for an element.</param>
        /// <returns>The matching elements.</returns>
        public NodeList getElementsByName(string elementName)
        {
            return null;
        }
    }
}
