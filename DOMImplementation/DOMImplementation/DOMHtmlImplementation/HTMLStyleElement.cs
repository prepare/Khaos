using CSS;
using CascadingStyleSheets;

namespace DOMCore
{

    /**
     *  CSSStyle information. See the STYLE element definition in Html 4.01, the CSS 
     * module [<a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-CSSStyle-20001113'>DOM Level 2 CSSStyle Sheets and CSS</a>] and the <code>LinkStyle</code> class in the StyleSheets 
     * module [<a href='http://www.w3.org/TR/2000/REC-DOM-Level-2-CSSStyle-20001113'>DOM Level 2 CSSStyle Sheets and CSS</a>]. 
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLStyleElement : HTMLElement, IHTMLStyleElement
    {
        public HTMLStyleElement(string tag, IDocument doc)
            : base(tag,doc)
        { }

        public HTMLStyleElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Enables/disables the style styles. 
         */
        public bool Disabled { 
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
         * Designed for use with one or more target media. See the media attribute 
         * definition in Html 4.01.
         */
        public string Media { 
            get 
            { 
                return getAttribute("media"); 
            } 
            set 
            { 
                setAttribute("media", value); 
            } 
        }

        /**
         * The content type of the style styles language. See the type attribute 
         * definition in Html 4.01.
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

        public StyleSheet StyleSheet
        {
            get
            {
                if (ChildNodes.Length > 0)
                    if (ChildNodes[0] is Text)
                        return CSSParser.Parse(((Text)ChildNodes[0]).Data, request);
                return null;
            }
        }
    }
}