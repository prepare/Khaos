using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Input;
using PhoenixOI;

namespace RenderEngine
{
    public class VisualDocument : VisualNode
    {
        private VisualDocument(IPageHost pageHost)
            : base()
        {
            this.pageHost = pageHost;

        }

        public void SetDefaultStyle()
        {
            CssStyle.WebColor.RealValue = new SolidColorBrush(Colors.Black);
            CssStyle.WebBackgroundColor.IsTransparent = true;
            CssStyle.WebBackgroundImage.RealValue = null;
            CssStyle.WebBackgroundPosition.RealValue = CascadingStyleSheets.BackgroundPositionEnum.TopLeft;
            CssStyle.WebBackgroundRepeat.RealValue = CascadingStyleSheets.BackgroundRepeatEnum.Repeat;
            CssStyle.WebBackgroundAttachment.RealValue = CascadingStyleSheets.BackgroundAttachmentEnum.Scroll;

            CssStyle.WebBorderLeftColor.IsTransparent = true;
            CssStyle.WebBorderRightColor.IsTransparent = true;
            CssStyle.WebBorderTopColor.IsTransparent = true;
            CssStyle.WebBorderBottomColor.IsTransparent = true;

            CssStyle.WebFloat.RealValue = CascadingStyleSheets.FloatEnum.None;
            CssStyle.WebClear.RealValue = CascadingStyleSheets.ClearingEnum.None;
            CssStyle.WebCursor.RealValue = CascadingStyleSheets.CursorEnum.Arrow;

            CssStyle.WebTextDecoration.RealValue = CascadingStyleSheets.TextDecorationEnum.None;

            CssStyle.WebOverflow.RealValue = CascadingStyleSheets.OverflowEnum.Scroll;
        }

        public new static VisualDocument CreateVisualRoot(IPageHost pageHost, Request request)
        {
            VisualDocument doc = new VisualDocument(pageHost);
            doc.visualRoot = doc;
            doc.pageHost = pageHost;
            doc.request = request;
            return doc;
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            base.OnMouseLeave(e);
        }

        public override bool CanShareLine
        {
            get
            {
                return false;
            }
        }
    }
}
