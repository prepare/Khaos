using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{

    public enum TextAlignEnum
    {
        Left,
        Right,
        Center,
        Justify 

    }

    public enum LineBreakEnum
    { 
        None,
        Normal,
        Double
    }

    public enum TextDecorationEnum
    {
        Inherit,
        None,
        Underline,
        Overline,
        LineThrough,
    }

    public enum FloatEnum
    {
        Inherit,
        Left,
        Right,
        None
    }

    public enum ClearingEnum
    {
        Inherit,
        Left,
        Right,
        None,
        Both

    }

    public enum VisibilityEnum
    {
        Inherit,
        Visiblie,
        Hidden,
        Collapse
    }

    public enum OverflowEnum
    {
        Inherit,
        Visible,
        Hidden,
        Scroll,
        Auto
    }

    public enum DisplayEnum
    {
        None,
        Inline,
        Block,
        ListItem,
        InlineBlock,
        RunIn,
        Compact,
        Marker,
        Table,
        InlineTable,
        TableRowGroup,
        TableHeaderGroup,
        TableFooterGroup,
        TableRow,
        TableColumnGroup,
        TableColumn,
        TableCell,
        TableCaption
    }

    public enum PositionEnum
    {
        /// <summary>Normal Flow (Default)</summary>
        Static,
        /// <summary>Relative Positioning</summary>
        Relative,
        /// <summary>Page Positioning</summary>
        Absolute,
        /// <summary>Browser Positioning</summary>
        Fixed
    }

    public enum VAlignEnum
    {
        Baseline,
        Sub,
        Super,
        Top,
        TextTop,
        Middle,
        Bottom,
        TextBottom
    }

    public enum BackgroundAttachmentEnum
    {
        Inherit = 0,
        Scroll,
        Fixed
    }

    public enum BackgroundPositionEnum
    {
        Inherit = 0,
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        CenterCenter,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public enum BackgroundRepeatEnum
    {
        Inherit = 0,
        Repeat,
        RepeatX,
        RepeatY,
        NoRepeat
    }

    public enum BorderStyleEnum
    {
        None,
        Solid,
        Double,
        Groove,
        Ridge,
        Inset,
        Outset
    }

    // Summary:
    //     Specifies the built in cursor types.
    public enum CursorEnum
    {
        Inherit,
        // Summary:
        //     A value indicating that no cursor should be displayed.
        None = 0,
        //
        // Summary:
        //     No cursor.
        No = 1,
        //
        // Summary:
        //     A standard arrow cursor.
        Arrow = 2,
        //
        // Summary:
        //     A standard arrow with small hourglass cursor.
        AppStarting = 3,
        //
        // Summary:
        //     A crosshair cursor.
        Cross = 4,
        //
        // Summary:
        //     A help cursor.
        Help = 5,
        //
        // Summary:
        //     A text I-Beam cursor.
        IBeam = 6,
        //
        // Summary:
        //     A cursor with arrows pointing north, south, east, and west.
        SizeAll = 7,
        //
        // Summary:
        //     A cursor with arrows pointing northeast and southwest.
        SizeNESW = 8,
        //
        // Summary:
        //     A cursor with arrows pointing north and south.
        SizeNS = 9,
        //
        // Summary:
        //     A cursor with arrows pointing northwest and southeast.
        SizeNWSE = 10,
        //
        // Summary:
        //     A cursor with arrows pointing west and east.
        SizeWE = 11,
        //
        // Summary:
        //     A vertical arrow cursor.
        UpArrow = 12,
        //
        // Summary:
        //     An hourglass cursor.
        Wait = 13,
        //
        // Summary:
        //     A hand cursor.
        Hand = 14,
        //
        // Summary:
        //     A pen cursor.
        Pen = 15,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing north and south.
        ScrollNS = 16,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing west and east.
        ScrollWE = 17,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing north, south, east, and west.
        ScrollAll = 18,
        //
        // Summary:
        //     A scrolling cursor with an arrow pointing north.
        ScrollN = 19,
        //
        // Summary:
        //     A scrolling cursor with an arrow pointing south.
        ScrollS = 20,
        //
        // Summary:
        //     A scrolling cursor with an arrow pointing west.
        ScrollW = 21,
        //
        // Summary:
        //     A scrolling cursor with an arrow pointing east.
        ScrollE = 22,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing north and west.
        ScrollNW = 23,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing north and east.
        ScrollNE = 24,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing south and west.
        ScrollSW = 25,
        //
        // Summary:
        //     A scrolling cursor with arrows pointing south and east.
        ScrollSE = 26,
        //
        // Summary:
        //     An arrow cd cursor.
        ArrowCD = 27,
    }
}
