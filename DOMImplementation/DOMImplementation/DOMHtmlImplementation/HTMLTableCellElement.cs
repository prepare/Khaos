using System;

namespace DOMCore
{

    /**
     * The object used to represent the <code>TH</code> and <code>TD</code> 
     * elements. See the TD element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTableCellElement : HTMLElement, IHTMLTableCellElement
    {
        public HTMLTableCellElement(string tag, IDocument doc)
            : base(tag,doc)
        { }
        
        public HTMLTableCellElement(TagInfo tag, IDocument doc)
            : base(tag,doc)
        { }

        /**
         * The index of this cell in the row, starting from 0. This index is in 
         * document tree order and not display order.
         */
        public int CellIndex 
        { 
            get 
            { 
                return 0; 
            } 
        }

        /**
         * Abbreviation for header cells. See the abbr attribute definition in 
         * Html 4.01.
         */
        public string Abbr 
        {
            get
            {
                return this.getAttribute("abbr");
            }
            set
            {
                this.setAttribute("abbr",value);
            }
        }

        /**
         * Horizontal alignment of data in cell. See the align attribute definition
         *  in Html 4.01.
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
         * Names group of related headers. See the axis attribute definition in 
         * Html 4.01.
         */
        public string Axis
        {
            get
            {
                return this.getAttribute("axis");
            }
            set
            {
                this.setAttribute("axis", value);
            }
        }

        /**
         * Cell background color. See the bgcolor attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        public string BgColor
        {
            get
            {
                return this.getAttribute("bgcolor");
            }
            set
            {
                this.setAttribute("bgcolor", value);
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
                return this.getAttribute("char");
            }
            set
            {
                this.setAttribute("char", value);
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
                return this.getAttribute("charoff");
            }
            set
            {
                this.setAttribute("charoff", value);
            }
        }

        /**
         * Number of columns spanned by cell. See the colspan attribute definition 
         * in Html 4.01.
         */
        public int ColSpan
        {
            get
            {
                return Int32.Parse(this.getAttribute("colspan"));
            }
            set
            {
                this.setAttribute("abbr", value.ToString());
            }
        }

        /**
         * List of <code>id</code> attribute values for header cells. See the 
         * headers attribute definition in Html 4.01.
         */
        public string Headers
        {
            get
            {
                return this.getAttribute("headers");
            }
            set
            {
                this.setAttribute("headers", value);
            }
        }

        /**
         * Cell height. See the height attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        public string Height
        {
            get
            {
                return this.getAttribute("height");
            }
            set
            {
                this.setAttribute("height", value);
            }
        }

        /**
         * Suppress word wrapping. See the nowrap attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        public string NoWrap
        {
            get
            {
                return this.getAttribute("nowrap");
            }
            set
            {
                this.setAttribute("nowrap", value);
            }
        }

        /**
         * Number of rows spanned by cell. See the rowspan attribute definition in 
         * Html 4.01.
         */
        public int RowSpan
        {
            get
            {
                return Int32.Parse(this.getAttribute("rowspan"));
            }
            set
            {
                this.setAttribute("rowspan", value.ToString());
            }
        }

        /**
         * Scope covered by header cells. See the scope attribute definition in 
         * Html 4.01.
         */
        public string Scope
        {
            get
            {
                return this.getAttribute("scope");
            }
            set
            {
                this.setAttribute("scope", value);
            }
        }

        /**
         * Vertical alignment of data in cell. See the valign attribute definition 
         * in Html 4.01.
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
         * Cell width. See the width attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
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