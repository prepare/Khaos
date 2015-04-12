
namespace DOMCore
{
    public interface IHTMLTableColElement : IHTMLElement
    {

        /**
         * Horizontal alignment of cell data in column. See the align attribute 
         * definition in Html 4.01.
         */
        string Align { get; set; }

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
         * Indicates the number of columns in a group or affected by a grouping. 
         * See the span attribute definition in Html 4.01.
         */
        int Span { get; set; }

        /**
         * Vertical alignment of cell data in column. See the valign attribute 
         * definition in Html 4.01.
         */
        string VAlign { get; set; }

        /**
         * Default column width. See the width attribute definition in Html 4.01.
         */
        string Width { get; set; }
    }
}
