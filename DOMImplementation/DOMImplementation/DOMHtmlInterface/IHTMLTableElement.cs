
namespace DOMCore
{
    /**
     * The create* and delete* methods on the table allow authors to construct and 
     * modify tables. [<a href='http://www.w3.org/TR/1999/REC-html401-19991224'>Html 4.01</a>] specifies that only one of each of the 
     * <code>CAPTION</code>, <code>THEAD</code>, and <code>TFOOT</code> elements 
     * may exist in a table. Therefore, if one exists, and the createTHead() or 
     * createTFoot() method is called, the method returns the existing THead or 
     * TFoot element. See the TABLE element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public interface IHTMLTableElement : IHTMLElement
    {
        /**
         * Specifies the table's position with respect to the rest of the 
         * document. See the align attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        string Align { get; set; }

        /**
         * Cell background color. See the bgcolor attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        string BgColor { get; set; }

        /**
         * The width of the border around the table. See the border attribute 
         * definition in Html 4.01.
         */
        string Border { get; set; }

        /**
         * Returns the table's <code>CAPTION</code>, or void if none exists. 
         * @version DOM Level 2
         */
        HTMLTableCaptionElement Caption { get; set; }

        /**
         * Specifies the horizontal and vertical space between cell content and 
         * cell borders. See the cellpadding attribute definition in Html 4.01.
         */
        string CellPadding { get; set; }

        /**
         * Specifies the horizontal and vertical separation between cells. See the 
         * cellspacing attribute definition in Html 4.01.
         */
        string CellSpacing { get; set; }

        /**
         * Create a new table caption object or return an existing one.
         * @return A <code>CAPTION</code> element.
         */
        IHTMLElement CreateCaption();

        /**
         * Create a table footer row or return an existing one.
         * @return A footer element (<code>TFOOT</code>).
         */
        IHTMLElement CreateTFoot();

        /**
         * Create a table header row or return an existing one.
         * @return A new table header element (<code>THEAD</code>).
         */
        IHTMLElement CreateTHead();

        /**
         * Delete a table row.
         * @param index The index of the row to be deleted. This index starts 
         *   from 0 and is relative to the logical order (not document order) of 
         *   all the rows contained inside the table. If the index is -1 the 
         *   last row in the table is deleted.
         * @exception DOMException
         *   INDEX_SIZE_ERR: Raised if the specified index is greater than or 
         *   equal to the number of rows or if the index is a negative number 
         *   other than -1.
         * @version DOM Level 2
         */
        void DeleteRow(int index);

        /**
         * Delete the footer from the table, if one exists.
         */
        void DeleteTFoot();

        /**
         * Delete the header from the table, if one exists.
         */
        void DeleteTHead();

        /**
         * Specifies which external table borders to render. See the frame 
         * attribute definition in Html 4.01.
         */
        string Frame { get; set; }

        /**
        * Insert a new empty row in the table. The new row is inserted 
        * immediately before and in the same section as the current 
        * <code>index</code>th row in the table. If <code>index</code> is -1 or 
        * equal to the number of rows, the new row is appended. In addition, 
        * when the table is empty the row is inserted into a <code>TBODY</code> 
        * which is created and inserted into the table.A table row cannot be 
        * empty according to [<a href='http://www.w3.org/TR/1999/REC-html401-19991224'>Html 4.01</a>].
        * @param index The row number where to insert a new row. This index 
        *   starts from 0 and is relative to the logical order (not document 
        *   order) of all the rows contained inside the table.
        * @return The newly created row.
        * @exception DOMException
        *   INDEX_SIZE_ERR: Raised if the specified index is greater than the 
        *   number of rows or if the index is a negative number other than -1.
        * @version DOM Level 2
        */
        IHTMLElement InsertRow(int index);

        /**
         * Returns a collection of all the rows in the table, including all in 
         * <code>THEAD</code>, <code>TFOOT</code>, all <code>TBODY</code> 
         * elements. 
         */
        HTMLCollection Rows { get; set; }

        /**
         * Specifies which internal table borders to render. See the rules 
         * attribute definition in Html 4.01.
         */
        string Rules { get; set; }

        /**
         * Description about the purpose or structure of a table. See the summary 
         * attribute definition in Html 4.01.
         */
        string Summary { get; set; }

        /**
         * Returns a collection of the table bodies (including implicit ones).
         */
        HTMLCollection TBodies { get; }

        /**
         * Returns the table's <code>TFOOT</code>, or <code>null</code> if none 
         * exists. 
         * @version DOM Level 2
         */
        HTMLTableSectionElement TFoot { get; set; }

        /**
         * Returns the table's <code>THEAD</code>, or <code>null</code> if none 
         * exists. 
         * @version DOM Level 2
         */
        HTMLTableSectionElement THead { get; set; }

        /**
         * Specifies the desired table width. See the width attribute definition 
         * in Html 4.01.
         */
        string Width { get; set; }
    }
}
