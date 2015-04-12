using System;

namespace DOMCore
{

    /**
     * Regroups the <code>COL</code> and <code>COLGROUP</code> elements. See the 
     * COL element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTableColElement : HTMLElement, IHTMLTableColElement
    {
        public HTMLTableColElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLTableColElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Horizontal alignment of cell data in column. See the align attribute 
         * definition in Html 4.01.
         */
        public string Align 
        { 
            get 
            { 
                return this.getAttribute("align"); 
            } 
            set 
            { 
                this.setAttribute("align", value); 
            } 
        }

        /**
         * Alignment character for cells in a column. See the char attribute 
         * definition in Html 4.01.
         */
        public string Ch 
        { 
            get 
            { 
                return this.getAttribute("ch"); 
            } 
            set 
            { 
                this.setAttribute("ch", value); 
            } 
        }

        /**
         * Offset of alignment character. See the charoff attribute definition in 
         * Html 4.01.
         */
        public string ChOff 
        { 
            get 
            { 
                return this.getAttribute("choff"); 
            } 
            set 
            { 
                this.setAttribute("choff", value); 
            } 
        }

        /**
         * Indicates the number of columns in a group or affected by a grouping. 
         * See the span attribute definition in Html 4.01.
         */
        public int Span 
        { 
            get 
            { 
                return Int32.Parse(this.getAttribute("span")); 
            } 
            set 
            { 
                this.setAttribute("span", value.ToString()); 
            } 
        }

        /**
         * Vertical alignment of cell data in column. See the valign attribute 
         * definition in Html 4.01.
         */
        public string VAlign 
        { 
            get 
            { 
                return this.getAttribute("valign"); 
            } 
            set 
            { 
                this.setAttribute("valign", value); 
            } 
        }

        /**
         * Default column width. See the width attribute definition in Html 4.01.
         */
        public string Width 
        { 
            get 
            { 
                return this.getAttribute("width"); 
            } 
            set 
            { 
                this.setAttribute("width", value); 
            } 
        }

    }

}