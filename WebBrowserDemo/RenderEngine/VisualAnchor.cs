using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CascadingStyleSheets;
using System.Windows.Input;
using System.Diagnostics;

namespace RenderEngine
{
    public class VisualAnchor : VisualNode
    {
        public enum LinkState
        {
            None,
            Hover,
            Visited,
            Link,
            Active
        }

        // None
        // Hover
        // Visited
        // Link
        // Active

        protected LinkState currentLinkState;

        private bool HasElement
        {
            get { return Element is DOMCore.IHTMLAnchorElement; }
        }

        private DOMCore.IHTMLAnchorElement AnchorElement
        {
            get { return Element as DOMCore.IHTMLAnchorElement; }
        }

        public override DOMCore.IHTMLElement Element
        {
            get
            {
                return base.Element;
            }
            set
            {
                base.Element = value;
                if (HasElement)
                {
                    this.Href = AnchorElement.Href;
                    try
                    {
                        if (AnchorElement.Target != null && AnchorElement.Target != "")
                            this.Target = (TargetEnum)Enum.Parse(typeof(TargetEnum), AnchorElement.Target);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private string href = "";
        public string Href
        {
            get
            {
                return href;
            }
            set
            {
                href = value;
                if (currentLinkState == LinkState.None)
                    currentLinkState = LinkState.Link;
            }
        }

        private TargetEnum target = TargetEnum._self;
        public TargetEnum Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }


        public VisualAnchor(VisualNode parentNode)
            : base(parentNode)
        {
            currentLinkState = LinkState.None;
            CssStyle.WebDisplay.RealValue = CascadingStyleSheets.DisplayEnum.Inline;
            CssStyle.WebColor = new WebColor() { RealValue = new System.Windows.Media.SolidColorBrush(Colors.Blue) };
            CssStyle.WebCursor = new WebCursor() { ForText = true, RealValue = CursorEnum.Hand };
        }

        protected override System.Windows.Size MeasureVisual(System.Windows.Size constraint)
        {
            if (currentLinkState != LinkState.None)
            {
            }

            return base.MeasureVisual(constraint);
        }

        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {

            CssStyle.WebTextDecoration = new WebEnumInheriting<TextDecorationEnum>(TextDecorationEnum.Inherit) { RealValue = TextDecorationEnum.Underline };

            InvalidateChildrenVisuals();

            if (currentLinkState != LinkState.None)
            {
                currentLinkState = LinkState.Hover;

            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {

            CssStyle.WebTextDecoration = new WebEnumInheriting<TextDecorationEnum>(TextDecorationEnum.Inherit) { RealValue = TextDecorationEnum.None };

            InvalidateChildrenVisuals();

            if (currentLinkState != LinkState.None)
            {
                currentLinkState = LinkState.Link;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (!href.StartsWith("#"))
            {
                pageHost.Navigate(href, target);
            }
        }
    }
}
