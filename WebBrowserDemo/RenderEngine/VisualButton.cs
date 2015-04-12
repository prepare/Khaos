using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using CascadingStyleSheets;
using System.Diagnostics;

namespace RenderEngine
{

    public class VisualButton : VisualNode
    {
        private Button internalButton;

        public VisualButton(VisualNode parentNode)
            : base(parentNode)
        {
            this.Init();
        }

        private void Init()
        {

            internalButton = new Button();

            base.AddLogicalChild(internalButton);
            base.AddVisualChild(internalButton);
        }

        public string Text
        {
            get { return internalButton.Content.ToString(); }
            set { internalButton.Content = value; }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return (internalButton == null)? 0 : 1; ;
            }
        }

        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
            if (index == 0)
            
                return internalButton;
            
            else
            
                throw new IndexOutOfRangeException("Visual Button asked for invalid Visual Child");
            
        }

        public override VisualNode this[int index]
        {
            get
            {
                throw new IndexOutOfRangeException("Visual Button asked for Visual Node");
            }
        }

        public override int Count
        {
            get { return 0; }
        }

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                yield return internalButton;
            }
        }

        /// <summary>
        /// Checks if the current node is managed by a parent node and
        /// that the node behaves like an inline element
        /// </summary>
        public override bool CanShareLine
        {
            get
            {
                bool inline = CssStyle.WebDisplay.RealValue == DisplayEnum.Compact ||
                CssStyle.WebDisplay.RealValue == DisplayEnum.Inline ||
                CssStyle.WebDisplay.RealValue == DisplayEnum.RunIn;

                return inline;
            }
        }

        
        /// <summary>
        /// Returns true if the current section or any part of it can fit in the
        /// width that is left availible for the current line.
        /// </summary>
        protected override bool CanSectionFit(double lineWidthLeft)
        {
            throw new IndexOutOfRangeException("Buttons don't have sections");
        }

        protected override Size MeasureVisual(Size constraint)
        {
            if (!this.widthAuto) internalButton.Width = this.TotalWidth;
            if (!this.heightAuto) internalButton.Height = this.TotalHeight;

            internalButton.Measure(constraint);

            RealDesiredSize = internalButton.DesiredSize;
            return internalButton.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            internalButton.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            return internalButton.RenderSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // Do nothing..
        }

    }
}
