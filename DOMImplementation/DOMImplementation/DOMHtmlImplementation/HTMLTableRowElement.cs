using System.Collections.Generic;

namespace DOMCore
{

    /**
     * A row in a table. See the TR element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTableRowElement : HTMLElement, IHTMLTableRowElement
    {
        public HTMLTableRowElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLTableRowElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /// <summary>
        /// Return parent table section element.
        /// </summary>
        private IHTMLElement ParentTableSection
        {
            get
            {
                INode parent = null;
                while (this.ParentNode != null)
                {
                    parent = this.ParentNode;
                    if (parent.NodeName == "table" || parent.NodeName == "thead" || parent.NodeName == "tfoot" || parent.NodeName == "tbody")
                    {
                        return parent as IHTMLElement;
                    }
                }
                return null;
            }
        }

        /**
         * This is in logical order and not in document order. The 
         * <code>rowIndex</code> does take into account sections (
         * <code>THEAD</code>, <code>TFOOT</code>, or <code>TBODY</code>) within 
         * the table, placing <code>THEAD</code> rows first in the index, 
         * followed by <code>TBODY</code> rows, followed by <code>TFOOT</code> 
         * rows.
         * @version DOM Level 2
         */
        public int RowIndex {
            get
            {
                //get parent table
                IHTMLElement e = getParentByTagName("table");
                if (e != null)
                {
                    //get rows in parent table
                    HTMLCollection c = ((IHTMLTableElement)e).Rows;
                        
                    //find self in rows of parent table
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (c[i] == this)
                            return i;
                    }
                }
                return 0; 
            } 
        }

        /**
         * The index of this row, relative to the current section (
         * <code>THEAD</code>, <code>TFOOT</code>, or <code>TBODY</code>), 
         * starting from 0.
         * @version DOM Level 2
         */
        public int SectionRowIndex 
        { 
            get
            {
                //get parent section
                IHTMLElement e = ParentTableSection;
                if (e != null)
                {
                    HTMLCollection c = new HTMLCollection();
                    //get rows in parent section
                    if (e.TagName == "table")
                    {
                        c = ((IHTMLTableElement)e).Rows;
                    }
                    if (e.TagName == "thead" || e.TagName == "tfoot" || e.TagName == "tbody")
                    {
                        c = ((IHTMLTableSectionElement)e).Rows;
                    }
                    //find self in rows of parent section
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (c[i] == this)
                            return i;
                    }
                }
                return 0; 
            } 
        }

        /**
         * The collection of cells in this row. 
         * @version DOM Level 2
         */
        public HTMLCollection Cells 
        { 
            get 
            {
                List<INode> lnl = new List<INode>();
                NodeList nl = getElementsByTagName("td");
                for (int i = 0; i < nl.Length; i++)
                {
                    lnl.Add(nl[i]);
                }
                nl = getElementsByTagName("th");
                for (int i = 0; i < nl.Length; i++)
                {
                    lnl.Add(nl[i]);
                }
                return new HTMLCollection(lnl); 
            } 
        }

        /**
         * Horizontal alignment of data within cells of this row. See the align 
         * attribute definition in Html 4.01.
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
         * Background color for rows. See the bgcolor attribute definition in Html 
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
         * Vertical alignment of data within cells of this row. See the valign 
         * attribute definition in Html 4.01.
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
         * Insert an empty <code>TD</code> cell into this row. If 
         * <code>index</code> is -1 or equal to the number of cells, the new 
         * cell is appended.
         * @param index The place to insert the cell, starting from 0.
         * @return The newly created cell.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if the specified <code>index</code> is greater 
         *   than the number of cells or if the index is a negative number other 
         *   than -1.
         * @version DOM Level 2
         */
        public IHTMLElement InsertCell(int index)
        {
            return null;
        }

        /**
         * Delete a cell from the current row.
         * @param index The index of the cell to delete, starting from 0. If the 
         *   index is -1 the last cell in the row is deleted.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if the specified <code>index</code> is greater 
         *   than or equal to the number of cells or if the index is a negative 
         *   number other than -1.
         * @version DOM Level 2
         */
        public void DeleteCell(int index)
        { 
        }
    }

}