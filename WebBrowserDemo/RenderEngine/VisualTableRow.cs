using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Collections;
using CascadingStyleSheets;

namespace RenderEngine
{
    public class VisualTableRow : VisualNode
    {
        private int cellCount = 0; //number of cells in  this row

        private double totalMinWidth; //total width of minimum width
        private double finalWidth; //finalized width of the row
        private double maxRowHeight = 0; //maximum row height

        bool secondLoop = false; //check on whether it is in second loop
        bool useMinWidth = false; //check whether minimum width are use to calculate
        bool finalLoop = false; //check whether it is in final loop
        bool tableWidthAuto; //check whether table width is auto
        bool tableHeightAuto; //check whether table height is auto
        bool helpOn = false; //check whether help is needed

        private double[] colMAX; //max column size
        private double[] widthList; //list of each cells width 
        private double[] widthMinList; //list of each cells minimum width       
        
        double tempWidth; //temporally width
        double totalWidth; //total width of cells = width of the row
        
        Size finalSize = new Size(); //the returning size of the row
        VisualTable parentTable; //table this row belongs
        VisualTableCell tempCell; //temporal cell
        private List<VisualTableCell> tempRow = new List<VisualTableCell>(); //temporal cell

        //getter and setter of variables
        public bool setHelpOn
        {
            set
            {
                helpOn = value;
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
        public bool setFinalLoop
        {
            set
            {
                finalLoop = value;
            }
        }

        public double setFinalWidth
        {
            set
            {
                finalWidth = value;
            }
        }
        public double getTotalMinWidth
        {
            get
            {
                return totalMinWidth;
            }
        }
        public double getSetMaxRowHeight
        {
            get
            {
                return maxRowHeight;
            }
            set
            {
                maxRowHeight = value;
            }
        }
        public double getTotalWidth
        {
            get
            {
                return totalWidth;
            }
        }

        public int getCellCount
        {
            get
            {
                return cellCount;
            }
        }

        public List<VisualTableCell> getSetTempRow
        {
            get
            {
                return tempRow;
            }
            set
            {
                tempRow = value;
            }
        }

        public double[] getSetColMAX
        {
            get
            {
                return colMAX;
            }
            set
            {
                colMAX = value;
            }
        }
        public double[] getSetWidthList
        {
            get
            {
                return widthList;
            }

            set
            {
                widthList = value;
            }
        }
        public double[] getSetWidthMinList
        {
            get
            {
                return widthMinList;
            }
            set
            {
                widthMinList = value;
            }
        }

        //constructor
        public VisualTableRow(VisualTable parentNode)
            : base(parentNode)
        {
            parentNode.addChild(this);
            parentTable = parentNode;
        }

        //add cell to the row done automatically
        public void addChild(VisualTableCell cell)
        {
            cellCount++;
            tempRow.Add(cell);
        }

        //check size of content compared with size of row
        public void checkRowWidth()
        {
            totalWidth = 0;
            tempWidth = 0;

            if (useMinWidth)
            {
                double temptotalWidth = 0;
                for (int i = 0; i < tempRow.Count; i++)
                {
                    temptotalWidth += tempRow[i].getRealMinWidth;
                }
                this.totalWidth = temptotalWidth;
            }
            else if (secondLoop)
            {
                double temptotalWidth = 0;
                for (int i = 0; i < tempRow.Count; i++)
                {
                    temptotalWidth += widthList[i];
                }
                this.totalWidth = temptotalWidth + LeftEdge + RightEdge;
            }
            else
            {
                for (int i = 0; i < tempRow.Count; i++)
                {
                    this.widthList[i] = 0;
                }

                for (int i = 0; i < tempRow.Count; i++)
                {
                    tempCell = tempRow[i];

                    if (tempCell.DesiredSize != null)
                    {
                        tempWidth = tempCell.getTotWidth + LeftEdge + RightEdge;
                        totalWidth += tempWidth;
                        this.widthList[i] = tempWidth;
                    }
                }
            }
        }

        //calculate the maximum row height
        public void calcMaxRowHeight()
        {
            maxRowHeight = 0;
            for (int i = 0; i < cellCount; i++)
            {
                double tempHeight = 0;
                if (tempRow[i].CssStyle.WebHeight.IsAuto || tempRow[i].CssStyle.WebHeight.RealValue.IsRelative)
                {
                    if (tempRow[i].DesiredSize.Height < tempRow[i].getRealMinHeight)
                    {
                        tempHeight = tempRow[i].getRealMinHeight;
                    }
                    else
                    {
                        tempHeight = tempRow[i].DesiredSize.Height;
                    }
                }
                else
                {
                    if (tempHeight < tempRow[i].getTotHeight)
                    {
                        tempHeight = tempRow[i].getTotHeight;
                    }
                }

                if (tempHeight > maxRowHeight)
                {
                    maxRowHeight = tempHeight;
                }
            }
            maxRowHeight += TopEdge + BottomEdge;
        }

        //calculate the total minimum width
        public void CalcTotalMinWidth()
        {
            totalMinWidth = 0;
            for (int i = 0; i < cellCount; i++)
            {
                totalMinWidth += tempRow[i].getRealMinWidth;
            }
        }

        /// <summary>
        /// calculate the size of the rows and call the cells measure
        /// on the second loop, it sets each columns size according to size specified by
        /// table
        /// on final loop, it sets it self to size specified by table
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureVisual(Size constraint) //measuring the size
        {
            if (finalLoop)
            {
                finalSize.Width = finalWidth;
                finalSize.Height = maxRowHeight;
            }
            if (secondLoop)//use the measurement given
            {
                for (int i = 0; i < cellCount; i++)
                {
                    VisualTableCell tableCell = tempRow[i];
                    tableCell.setColWidth = widthList[i];
                    tableCell.setRowHeight = maxRowHeight;
                    tableCell.setSecondLoop = true;
                    tableCell.InvalidateMeasure();
                    tableCell.MeasureNoClip(constraint, windowSize, wasCalledByMeasure);
                }
            }
            else
            {
                widthList = new double[cellCount];
                colMAX = new double[cellCount];
                widthMinList = new double[cellCount];

                for (int i = 0; i < cellCount; i++)
                {
                    VisualTableCell tableCell = (VisualTableCell)tempRow[i];
                    Size cellSize = new Size();
                    tableCell.MeasureNoClip(constraint, windowSize, wasCalledByMeasure);
                    cellSize = tableCell.RealDesiredSize;
                    widthList[i] = cellSize.Width;
                }
            }


            if (!tableWidthAuto)
            {
                CalcTotalMinWidth();
                if (totalMinWidth > constraint.Width - LeftEdge - RightEdge)
                {
                    useMinWidth = true;
                    secondLoop = false;
                    for (int i = 0; i < cellCount; i++)
                    {
                        tempRow[i].setUseMinWidth = true;
                    }
                }

                for (int i = 0; i < cellCount; i++)
                {
                    tempRow[i].InvalidateMeasure();
                    tempRow[i].MeasureNoClip(finalSize, windowSize, wasCalledByMeasure);
                }

                for (int i = 0; i < cellCount; i++)
                {
                    widthMinList[i] = tempRow[i].getRealMinWidth;
                }
            }

            this.checkRowWidth();

            finalSize.Height = this.maxRowHeight + TopEdge + BottomEdge;

            if (!finalLoop)
            {
                finalSize.Width = this.totalWidth;
            }

            RealDesiredSize = finalSize;
            visualChildrenCount = cellCount;
            secondLoop = true;
            return finalSize;
        }

        //arrange the cells to be on the same line
        protected override Size ArrangeOverride(Size finalSize)
        {
            Point position = new Point();
            double initWidth = LeftEdge;
            double initHeight = TopEdge;
            position.X = initWidth;
            position.Y = initHeight;

            for (int i = 0; i < cellCount; i++)
            {
                Size tempSize = tempRow[i].DesiredSize;
                tempRow[i].Arrange(new Rect(position, tempSize));
                position.X += tempSize.Width;
            }
            return finalSize;
        }
    }
}
