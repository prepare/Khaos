
namespace DOMCore
{
    /**
     * The object used to represent the <code>TH</code> and <code>TD</code> 
     * elements. See the TD element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public interface IHTMLTableCellElement : IHTMLElement
    {
        /**
         * Abbreviation for header cells. See the abbr attribute definition in 
         * Html 4.01.
         */
        string Abbr { get; set; }

        /**
         * Horizontal alignment of data in cell. See the align attribute definition
         *  in Html 4.01.
         */
        string Align { get; set; }

        /**
         * Names group of related headers. See the axis attribute definition in 
         * Html 4.01.
         */
        string Axis { get; set; }

        /**
         * Cell background color. See the bgcolor attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        string BgColor { get; set; }

        /**
         * The index of this cell in the row, starting from 0. This index is in 
         * document tree order and not display order.
         */
        int CellIndex { get; }

        /**
         * Alignment character for cells in a column. See the char attribute 
         * definition in Html 4.01.
         */
        string Ch { get; set; }

        /**
         * Offset of alignment character. See the charoff attribute definition in 
         * Html 4.01.
         */
        string ChOff { get; set; }

        /**
         * Number of columns spanned by cell. See the colspan attribute definition 
         * in Html 4.01.
         */
        int ColSpan { get; set; }

        /**
         * List of <code>id</code> attribute values for header cells. See the 
         * headers attribute definition in Html 4.01.
         */
        string Headers { get; set; }

        /**
         * Cell height. See the height attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        string Height { get; set; }

        /**
         * Suppress word wrapping. See the nowrap attribute definition in Html 
         * 4.01. This attribute is deprecated in Html 4.01.
         */
        string NoWrap { get; set; }

        /**
         * Number of rows spanned by cell. See the rowspan attribute definition in 
         * Html 4.01.
         */
        int RowSpan { get; set; }

        /**
         * Scope covered by header cells. See the scope attribute definition in 
         * Html 4.01.
         */
        string Scope { get; set; }

        /**
         * Vertical alignment of data in cell. See the valign attribute definition 
         * in Html 4.01.
         */
        string VAlign { get; set; }

        /**
         * Cell width. See the width attribute definition in Html 4.01. This 
         * attribute is deprecated in Html 4.01.
         */
        string Width { get; set; }
    }
}
