
namespace DOMCore
{

    /**
     * The <code>THEAD</code>, <code>TFOOT</code>, and <code>TBODY</code> 
     * elements. 
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTableSectionElement : HTMLElement, IHTMLTableSectionElement
    {
        public HTMLTableSectionElement(string tag, IDocument doc) 
            : base(tag,doc)
        { }

        public HTMLTableSectionElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * Horizontal alignment of data in cells. See the <code>align</code> 
         * attribute for HtmlTheadElement for details. 
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
         * Vertical alignment of data in cells. See the <code>valign</code> 
         * attribute for HtmlTheadElement for details. 
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
         * The collection of rows in this table section. 
         */
        public HTMLCollection Rows 
        { 
            get
            {
                NodeList nl = getElementsByTagName("tr");
                return new HTMLCollection(nl);
            } 
        }

        /**
         * Insert a row into this section. The new row is inserted immediately 
         * before the current <code>index</code>th row in this section. If 
         * <code>index</code> is -1 or equal to the number of rows in this 
         * section, the new row is appended.
         * @param index The row number where to insert a new row. This index 
         *   starts from 0 and is relative only to the rows contained inside 
         *   this section, not all the rows in the table.
         * @return The newly created row.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if the specified index is greater than the 
         *   number of rows of if the index is a negative number other than -1.
         * @version DOM Level 2
         */
        public IHTMLElement InsertRow(int index)
        {
            return null;
        }

        /**
         * Delete a row from this section.
         * @param index The index of the row to be deleted, or -1 to delete the 
         *   last row. This index starts from 0 and is relative only to the rows 
         *   contained inside this section, not all the rows in the table.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if the specified index is greater than or 
         *   equal to the number of rows or if the index is a negative number 
         *   other than -1.
         * @version DOM Level 2
         */
        public void DeleteRow(int index)
        { }

    }

}