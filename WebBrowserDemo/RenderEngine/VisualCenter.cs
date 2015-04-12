using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CascadingStyleSheets;

namespace RenderEngine
{
    public class VisualCenter : VisualNode
    {
        // Variables

        private LinkedList<Line> lines = new LinkedList<Line>();
        private Line currentCenteredLine;
        private Size finalSize;

        public VisualCenter(VisualNode parentNode)
            : base(parentNode)
        { }

        /// <summary>
        /// A line that can calculate centering
        /// </summary>
        private class Line 
        {
            public Line(double currentLineHeight, Size finalSize, VisualNode arranger)
            {
                this.arranger = arranger;
                this.finalSize = finalSize;

                lineY = currentLineHeight;
                childrenOnLine = new LinkedList<VisualNode>();
            }

            // Variables

            private VisualNode arranger;
            private Size finalSize;
            private LinkedList<VisualNode> childrenOnLine;
            private double lineY;
            private double lineHeight = 0;
            private double lineWidth = 0;

            public double LineHeight { get { return lineHeight; } }
            public double LineWidth { get { return lineWidth; } }

            // Properties

            public double LineY 
            {
                set { lineY = value; }
            }

            /// <summary>
            /// Add a child to the line
            /// </summary>
            public void AddChild(VisualNode node)
            {
                if (lineHeight < node.RealDesiredSize.Height)
                    lineHeight = node.RealDesiredSize.Height;

                lineWidth += node.RealDesiredSize.Width;
                childrenOnLine.AddLast(node);
            }

            /// <summary>
            /// Calculates the positions to center children
            /// </summary>
            public void CenterChildren()
            {
                double lineCenter = finalSize.Width / 2;
                double halfWidth = LineWidth / 2;

                double currectX = lineCenter - halfWidth;

                double collectedWidth = 0;
                foreach (var item in childrenOnLine)
                {
                    // Calculate Position
                    Point relativePosition = new Point();


                    // Calculate positioning
                    if (item.CssStyle.WebPosition.RealValue == PositionEnum.Static)
                    {
                        relativePosition.X = currectX + collectedWidth;
                        relativePosition.Y = lineY + (lineHeight - item.RealDesiredSize.Height);
                    }
                    else
                    {
                        // Calculate X position
                        if (item.CssStyle.WebLeft.IsAuto)
                            if (item.CssStyle.WebRight.IsAuto)
                                relativePosition.X = currectX + collectedWidth;
                            else
                                relativePosition.X = finalSize.Width - item.CssStyle.WebRight.RealValue.Value;
                        else
                            relativePosition.X = item.CssStyle.WebLeft.RealValue.Value;

                        // Calculate Y position
                        if (item.CssStyle.WebTop.IsAuto)
                            if (item.CssStyle.WebBottom.IsAuto)
                                relativePosition.Y = lineY + (lineHeight - item.RealDesiredSize.Height);
                            else
                                relativePosition.Y = finalSize.Height - item.CssStyle.WebBottom.RealValue.Value;
                        else
                            relativePosition.Y = item.CssStyle.WebTop.RealValue.Value;
                    }

                    // Arrange 
                    Point newPoint = new Point
                        (
                        relativePosition.X - arranger.HorizontalOffset, // Scroll Logic
                        relativePosition.Y - arranger.VerticalOffset // Scroll Logic
                        );

                    item.Arrange(new Rect(newPoint, item.RealDesiredSize));


                    collectedWidth += item.RealDesiredSize.Width;
                }
            }

        }

        /// <summary>
        /// Override Arrange Start
        /// </summary>
        public override void OnArrangeStart(double currentY, Size finalSize)
        {
            this.finalSize = finalSize;

            lines.Clear();
            currentCenteredLine = new Line(currentY, finalSize, this);
            lines.AddLast(currentCenteredLine);
        }

        /// <summary>
        /// Create new line
        /// </summary>
        protected override void OnArrangeNewLine(double currentY)
        {
            currentCenteredLine = new Line(currentY, finalSize, this);
            lines.AddLast(currentCenteredLine);

        }

        /// <summary>
        /// Calculate Centering
        /// </summary>
        public override void OnArrangeDone()
        {
            foreach (var item in lines)
                item.CenterChildren();
        }

        /// <summary>
        /// Arrange Children
        /// </summary>
        protected override void ArrageChild(VisualNode child, Point currentLine, double currentLineHeight, Size finalSize)
        {
            // Arrange
            currentCenteredLine.AddChild(child);
        }

    }
}
