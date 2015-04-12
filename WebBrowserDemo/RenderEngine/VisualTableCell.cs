using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using CascadingStyleSheets;
using WinStyle = System.Windows.Style;
using CssStyle = CascadingStyleSheets.Style;
using RenderDemo;
using System.Diagnostics;
namespace RenderEngine
{

    public class VisualTableCell : VisualNode
    {
        bool secondLoop = false; //determine whether it is in second loop/ setting size
        bool useMinWidth = false; //determine whether it should use the minimum size
        bool tableWidthAuto; //determine whether table width is automatic
        bool tableHeightAuto; //determine whether table height is automatic
        bool tooBig = false; //determine whether content is too big compare to table size

        int colSpan;

        double colWidth; //column/ cell width
        double rowHeight; //height of row
        double maxHeight; //height of maximum content within the cell
        double maxWidth; //width of maximum content within the cell
        double finalSize; //finalized size of the cell

        private double realMinWidth; //finalized minimum width
        private double realMinHeight;  //finalized minimum height
        private double totWidth; //total width of content
        private double totHeight; //total height of content

        Size returnSize = new Size(); //returning size of the cell
        VisualTableRow parentRow; //row it belongs to

        //getter and setter
        public bool setTooBig
        {
            set
            {
                tooBig = value;
            }
        }
        public bool setTableWidthAuto
        {
            set
            {
                tableWidthAuto = value;
            }
        }
        public bool setTableHeightAuto
        {
            set
            {
                tableHeightAuto = value;
            }
        }
        public bool setUseMinWidth
        {
            set
            {
                useMinWidth = value;
            }
        }
        public bool setSecondLoop
        {
            set
            {
                secondLoop = value;
            }
        }

        public double getSetFinalSize
        {
            get
            {
                return finalSize;
            }
            set
            {
                finalSize = value;
            }
        }
        public double getMaxHeight
        {
            get
            {
                return maxHeight;
            }
        }
        public double setColWidth
        {
            set
            {
                colWidth = value;
            }
        }
        public double setRowHeight
        {
            set
            {
                rowHeight = value;
            }
        }
        public double getRealMinWidth
        {
            get
            {
                return realMinWidth;
            }
        }
        public double getRealMinHeight
        {
            get
            {
                return realMinHeight;
            }
        }
        public double getTotWidth
        {
            get
            {
                return totWidth;
            }
        }
        public double getTotHeight
        {
            get
            {
                return totHeight;
            }
        }

        //constructor
        public VisualTableCell(VisualTableRow parentNode)
            : base(parentNode)
        {
            parentNode.addChild(this);
            parentRow = parentNode;
            
        }

        //count number of contents in cell
        protected override int VisualChildrenCount
        {
            get
            {
                return this.visualChildrenCount;
            }
        }

        //get visual node within the cell
        protected override Visual GetVisualChild(int index)
        {
            return base.GetVisualChild(index);
        }

        //find size of each contents
        public void ContentSizes(Size constraint)
        {
            totWidth = 0;
            totHeight = 0;

            int contentNum = this.splitManager.Count;

            if (contentNum > 0)
            {
                for (int i = 0; i < contentNum; i++)
                {
                    VisualNode content = this[i];
                    content.MeasureNoClip(constraint, windowSize, wasCalledByMeasure);
                    double thisHeight = content.DesiredSize.Height;
                    double thisWidth = content.DesiredSize.Width;

                    totWidth += thisWidth;
                    totHeight += thisHeight;

                    if (thisHeight > maxHeight)
                    {
                        maxHeight = thisHeight;
                    }

                    if (thisWidth > maxWidth)
                    {
                        maxWidth = thisWidth;
                    }
                }
                this.realMinWidth = maxWidth + LeftEdge + RightEdge;
                this.realMinHeight = maxHeight + TopEdge + BottomEdge;
            }
        }

        /// <summary>
        /// check each condition whether it is too big, second loop, using minimum width,
        /// and measure the content within the cells then measure it self
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureVisual(Size constraint)
        {
            if (tooBig)
            {
                returnSize = constraint;
            }

            if (secondLoop)
            {
                returnSize.Width = colWidth;
                returnSize.Height = rowHeight;
            }
            else if (useMinWidth)
            {
                returnSize.Width = realMinWidth;
                returnSize.Height = rowHeight;
            }
            else if (!secondLoop && !useMinWidth)
            {
                ContentSizes(constraint);
                MeasureCells(constraint); //in case cells are auto
                if (totWidth > returnSize.Width)
                {
                    returnSize.Width = totWidth + LeftEdge + RightEdge;
                }
                else if (returnSize.Width < realMinWidth)
                {
                    returnSize.Width = realMinWidth;
                }

                if (totHeight > returnSize.Height)
                {
                    returnSize.Height = totHeight + TopEdge + BottomEdge;
                }
                else if (returnSize.Height < realMinHeight)
                {
                    returnSize.Height = realMinHeight + TopEdge + BottomEdge;
                }
            }
            else
            {
                ContentSizes(constraint);
                MeasureCells(constraint); //in case cells are auto
                if (totWidth > returnSize.Width)
                {
                    returnSize.Width = totWidth + LeftEdge + RightEdge;
                }
                
                if (totHeight > returnSize.Height)
                {
                    returnSize.Height = totHeight + TopEdge + BottomEdge;
                }
            }

            RealDesiredSize = returnSize;
            visualChildrenCount = this.Count;

            return returnSize;
        }

        //measure the size of cell
        public void MeasureCells(Size constraint)

        {
            WebLength widthReal = this.CssStyle.WebWidth;
            WebLength heightReal = this.CssStyle.WebHeight;

            if (widthReal.IsAuto)
            {
                returnSize.Width = this.TotalWidth;
            }
            else if (widthReal.RealValue.IsRelative)
            {
                returnSize.Width = constraint.Width *  (widthReal.RealValue.Value / 100);
            }
            else
            {
                returnSize.Width = this.CssStyle.WebWidth.RealValue.Value + RightEdge + LeftEdge;
            }

            if (heightReal.IsAuto || heightReal.RealValue.IsRelative)
            {
                returnSize.Height = this.TotalHeight;
            }
            else
            {
                returnSize.Height = this.CssStyle.WebHeight.RealValue.Value + TopEdge + BottomEdge;
            }
        }
    }
}
