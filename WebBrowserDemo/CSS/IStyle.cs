using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoenixOI;

namespace CascadingStyleSheets
{
    public interface IStyle
    {
        ///// <summary>
        ///// For creation of root style
        ///// </summary>
        //IStyle(PhoenixOI.Request request);

        ///// <summary>
        ///// For creation of child style
        ///// </summary>
        //IStyle(IStyle parent);

        Request Request { get; }
        string this[string propName] { get; set; }

        // Property Layout //

        // Property
        // InternalProperty
        // ResolvedProperty

        // General Properties //

        string WebTitle { get; set; }
        string InternalTitle { get; set; }
        string ResolveTitle { get; }

        // Background Properties //

        string WebBackground { set; }

        string WebBackgroundColor { get; set; }
        WebColor InternalBackgroundColor { get; set; }
        WebColor ResolveBackgroundColor { get; }

        string WebBackgroundImage { get; set; }
        WebImage InternalBackgroundImage { get; set; }
        WebImage ResolveBackgroundImage { get; }

        string WebBackgroundRepeat { get; set; }
        WebBackgroundRepeat InternalBackgroundRepeat { get; set; }
        WebBackgroundRepeat ResolveBackgroundRepeat { get; }

        string WebBackgroundAttachment { get; set; }
        WebBackgroundAttachment InternalBackgroundAttachment { get; set; }
        WebBackgroundAttachment ResolveBackgroundAttachment { get; }

        string WebBackgroundPosition { get; set; }
        WebBackgroundPosition InternalBackgroundPosition { get; set; }
        WebBackgroundPosition ResolveBackgroundPosition { get; }

        // Border Properties //

        string WebBorderTop { set; }

        string WebBorderBottom { set; }

        string WebBorderLeft { set; }

        string WebBorderRight { set; }

        string WebBorder { set; }

        string WebBorderColor { set; }


        string WebBorderLeftColor { get; set; }
        WebColor InternalBorderLeftColor { get; set; }
        WebColor ResolveBorderLeftColor { get; }

        string WebBorderTopColor { get; set; }
        WebColor InternalBorderTopColor { get; set; }
        WebColor ResolveBorderTopColor{ get; }

        string WebBorderBottomColor { get; set; }
        WebColor InternalBorderBottomColor { get; set; }
        WebColor ResolveBorderBottomColor { get; }

        string WebBorderRightColor { get; set; }
        WebColor InternalBorderRightColor { get; set; }
        WebColor ResolveBorderRightColor { get; }


        string WebBorderLeftStyle { get; set; }
        WebBorderStyle InternalBorderLeftStyle { get; set; }
        WebBorderStyle ResolveBorderLeftStyle { get; }

        string WebBorderTopStyle { get; set; }
        WebBorderStyle InternalBorderTopStyle { get; set; }
        WebBorderStyle ResolveBorderTopStyle { get; }

        string WebBorderBottomStyle{ get; set; }
        WebBorderStyle InternalBorderBottomStyle { get; set; }
        WebBorderStyle ResolveBorderBottomStyle { get; }

        string WebBorderRightStyle { get; set; }
        WebBorderStyle InternalBorderRightStyle { get; set; }
        WebBorderStyle ResolveBorderRightStyle { get; }


        string WebBorderLeftWidth { get; set; }
        WebBorderWidth InternalBorderLeftWidth { get; set; }
        WebBorderWidth ResolveBorderLeftWidth { get; }

        string WebBorderTopWidth { get; set; }
        WebBorderWidth InternalBorderTopWidth { get; set; }
        WebBorderWidth ResolveBorderTopWidth { get; }

        string WebBorderBottomWidth { get; set; }
        WebBorderWidth InternalBorderBottomWidth { get; set; }
        WebBorderWidth ResolveBorderBottomWidth { get; }

        string WebBorderRightWidth { get; set; }
        WebBorderWidth InternalBorderRightWidth { get; set; }
        WebBorderWidth ResolveBorderRightWidth { get; }

        // Margin and Padding //

        string Padding { set; }

        string WebPaddingTop { get; set; }
        WebLength InternalPaddingTop { get; set; }
        WebLength ResolvePaddingTop { get; }

        string WebPaddingBottom { get; set; }
        WebLength InternalPaddingBottom { get; set; }
        WebLength ResolvePaddingBottom { get; }

        string WebPaddingLeft { get; set; }
        WebLength InternalPaddingLeft { get; set; }
        WebLength ResolvePaddingLeft { get; }

        string WebPaddingRight { get; set; }
        WebLength InternalPaddingRight { get; set; }
        WebLength ResolvePaddingRight { get; }


        string Margin { set; }

        string WebMarginTop { get; set; }
        WebLength InternalMarginTop { get; set; }
        WebLength ResolveMarginTop { get; }

        string WebMarginBottom { get; set; }
        WebLength InternalMarginBottom { get; set; }
        WebLength ResolveMarginBottom { get; }

        string WebMarginLeft { get; set; }
        WebLength InternalMarginLeft { get; set; }
        WebLength ResolveMarginLeft { get; }

        string WebMarginRight { get; set; }
        WebLength InternalMarginRight { get; set; }
        WebLength ResolveMarginRight { get; }

        // Dimension Properties //

        string WebWidth { get; set; }
        WebLength InternalWidth { get; set; }
        WebLength ResolveWidth { get; }

        string WebHeight { get; set; }
        WebLength InternalHeight { get; set; }
        WebLength ResolveHeight { get; }


        string WebminWidth { get; set; }
        WebLength InternalminWidth { get; set; }
        WebLength ResolveminWidth { get; }

        string WebmaxWidth { get; set; }
        WebLength InternalmaxWidth { get; set; }
        WebLength ResolvemaxWidth { get; }

        string WebminHeight { get; set; }
        WebLength InternalminHeight { get; set; }
        WebLength ResolveminHeight { get; }

        string WebmaxHeight { get; set; }
        WebLength InternalmaxHeight { get; set; }
        WebLength ResolvemaxHeight { get; }


        string WebPixelHeight { get; set; }
        WebLength InternalWebPixelHeight { get; set; }
        WebLength ResolveWebPixelHeight { get; }

        string WebPixelWidth { get; set; }
        WebLength InternalPixelWidth { get; set; }
        WebLength ResolvePixelWidth { get; }

        string WebLineHeight { get; set; }
        WebLength InternalLineHeight { get; set; }
        WebLength ResolveLineHeight { get; }

        // Classification Properties // 

        string WebPosition { get; set; }
        WebPosition InternalPosition { get; set; }
        WebPosition ResolvePosition { get; }

        string WebVAlign { get; set; }
        WebVAlign InternalVAlign { get; set; }
        WebVAlign ResolveVAlign { get; }

        string WebFloat { get; set; }
        WebFloat InternalFloat { get; set; }
        WebFloat ResolveFloat { get; }

        string WebClear { get; set; }
        WebClear InternalClear { get; set; }
        WebClear ResolveClear { get; }

        string WebCursor { get; set; }
        WebCursor InternalCursor { get; set; }
        WebCursor ResolveCursor { get; }

        string WebDisplay { get; set; }
        WebDisplay InternalDisplay { get; set; }
        WebDisplay ResolveDisplay { get; }

        string WebVisibility { get; set; }
        WebVisibility InternalVisibility { get; set; }
        WebVisibility ResolveVisibility { get; }

        // Position Properties //

        string WebBottom { get; set; }
        WebLength InternalBottom { get; set; }
        WebLength ResolveBottom { get; }

        string WebLeft { get; set; }
        WebLength InternalLeft { get; set; }
        WebLength ResolveLeft { get; }

        string WebRight { get; set; }
        WebLength InternalRight { get; set; }
        WebLength ResolveRight { get; }

        string WebTop { get; set; }
        WebLength InternalTop { get; set; }
        WebLength ResolveTop { get; }

        string WebZIndex { get; set; }
        WebIndex InternalZIndex { get; set; }
        WebIndex ResolveZIndex { get; }

        string WebOverflow { get; set; }
        WebOverflow InternalOverflow { get; set; }
        WebOverflow ResolveOverflow { get; }

        // Special Properties //

        string WebOpacity { get; set; }
        WebLength InternalOpacity { get; set; }
        WebLength ResolveOpacity { get; }

        // Text Properties //

        string WebColor { get; set; }
        WebColor InternalColor { get; set; }
        WebColor ResolveColor { get; }

        string WebTextDecoration { get; set; }
        WebTextDecoration InternalTextDecoration { get; set; }
        WebTextDecoration ResolveTextDecoration { get; }

        string WebTextAlign { get; set; }
        WebTextAlign InternalTextAlign { get; set; }
        WebTextAlign ResolveTextAlign { get; }

        // Font Properties //

        string Font { get; set; }

        string WebFontFamily { get; set; }
        WebFontFamily InternalFontFamily { get; set; }
        WebFontFamily ResolveFontFamily { get; }

        string WebFontSty { get; set; }
        WebFontStyle InternalFontSty { get; set; }
        WebFontStyle ResolveFontSty { get; }

        string WebFontWeight { get; set; }
        WebFontWeight InternalFontWeight { get; set; }
        WebFontWeight ResolveFontWeight { get; }

        string WebFontStretch { get; set; }
        WebFontStretch InternalFontStretch { get; set; }
        WebFontStretch ResolveFontStretch { get; }

        string WebFontSize { get; set; }
        WebFontUnit InternalFontSize { get; set; }
        WebFontUnit ResolveFontSize { get; }

        // Khaos Custom Properties //

        string WebLineBreak { get; set; }
        WebLineBreak InternalLineBreak { get; set; }
        WebLineBreak ResolveLineBreak { get; }

    }
}
