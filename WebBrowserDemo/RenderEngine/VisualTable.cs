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
    public class VisualTable : VisualNode
    {
        int rowCount = 0; //number of rows

        double rowMaxWidth; //row's maximum width        
        double rowFinalWidth; //finalized row width
        double finalWidth; //finalized table width
        double totalTallHeight; //total of each row height 
        double rowTotMinHeight; //total of each row's minimum height

        double[] tempCol; //array of column size
        double[] colMAX; //array of max column size
        double[] finalCol; //finalized column size

        bool useMinWidth = false; //check whether table have to use minimum width of row
        bool fit = true; //check whether content fits in table
        bool sameColNum = true;

        VisualTableRow tempRow; //temporary row
        List<VisualTableRow> tempTable = new List<VisualTableRow>(); //array of rows
        Size returnSize = new Size(); //final returning size

        List<double> temColSize; //array of temporal column sizes
        List<double> rowWidth; //list of row width
        List<double> rowHeight; //list of row height
        List<double> rowMinWidth; //list of row's minimum width

        //constructor
        public VisualTable(VisualNode parentNode)
                : base(parentNode)
            {
            }

        //add row to table done automatically
        public void addChild(VisualTableRow row)
        {
            rowCount++;
            tempTable.Add(row);
        }

        /// <summary>
        /// measures the size of table, rows, cells, contents and columns
        /// first determine whether if table is auto, table is fixed and content can fit in there, 
        /// table is fixed but content is greater than specified size, table size is much greater than
        /// contents/ rows/ cells
        /// secondary call the measure in rows
        /// thirdly make the column
        /// then lastly call the measure in rows again to set to calculated columns
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureVisual(Size constraint)
        {
            Size thisSize = new Size();

            if (this.CssStyle.WebHeight.IsAuto || this.CssStyle.WebHeight.RealValue.IsRelative)
            {
                for (int i = 0; i < tempTable.Count; i++)
                {
                    VisualTableRow rowMeasure = (VisualTableRow)tempTable[i];
                    rowMeasure.setTableHeightAuto = true;
                }
            }
            else
            {
                for (int i = 0; i < tempTable.Count; i++)
                {
                    VisualTableRow rowMeasure = tempTable[i];
                    rowMeasure.setTableHeightAuto = false;
                    rowMeasure.CalcTotalMinWidth();
                }
            }

            if (this.CssStyle.WebWidth.IsAuto || this.CssStyle.WebWidth.RealValue.IsRelative)
            {
                for (int i = 0; i < tempTable.Count; i++)
                {
                    VisualTableRow rowMeasure = (VisualTableRow)tempTable[i];
                    rowMeasure.setTableWidthAuto = true;
                    rowMeasure.MeasureNoClip(constraint, windowSize, wasCalledByMeasure);
                }
            }
            else
            {
                thisSize.Width = this.CssStyle.WebWidth.RealValue.Value - LeftEdge - RightEdge;

                if (!this.CssStyle.WebHeight.IsAuto && !this.CssStyle.WebHeight.RealValue.IsRelative)
                {
                    thisSize.Height = this.CssStyle.WebHeight.RealValue.Value - TopEdge - BottomEdge;
                }

                for (int i = 0; i < tempTable.Count; i++)
                {
                    VisualTableRow rowMeasure = tempTable[i];
                    rowMeasure.setTableWidthAuto = false;
                    rowMeasure.MeasureNoClip(thisSize, windowSize, wasCalledByMeasure);
                    rowMeasure.CalcTotalMinWidth();
                    if (rowMeasure.getTotalMinWidth > thisSize.Width)
                    {
                        useMinWidth = true;
                    }
                }
            }

            getRowHeights();

            rowTotMinHeight = 0;
            totalTallHeight = 0;

            for (int i = 0; i < tempTable.Count; i++)
            {
                VisualTableRow temRow = tempTable[i];
                rowTotMinHeight += temRow.getSetMaxRowHeight + TopEdge + BottomEdge;
            }
            rowTotMinHeight += TopEdge + BottomEdge;

            int colNumber = 0;

            for (int i = 0; i < tempTable.Count; i++)
            {
                if (i == 0)
                {
                    colNumber = tempTable[i].getCellCount;
                }
                else
                {
                    if (!(colNumber == tempTable[i].getCellCount))
                    {
                        sameColNum = false;
                    }
                }
            }

            if (sameColNum)
            {
                if (!useMinWidth)
                {
                    makeColumn();
                }
                else
                {
                    rowFinalWidth = 0;
                    makeColumnMin();
                    fit = false;

                    for (int i = 0; i < finalCol.Length; i++)
                    {
                        rowFinalWidth += finalCol[i];
                    }

                    for (int i = 0; i < tempTable.Count; i++)
                    {
                        VisualTableRow rowMeasure = tempTable[i];
                        finalWidth = rowFinalWidth + LeftEdge + RightEdge;
                        rowMeasure.setFinalWidth = finalWidth;
                        rowMeasure.setFinalLoop = true;
                        returnSize.Width = finalWidth + LeftEdge + RightEdge;
                    }
                }

                if (!this.CssStyle.WebWidth.IsAuto && !this.CssStyle.WebWidth.RealValue.IsRelative)
                {
                    if (!useMinWidth && fit)
                    {
                        makeFatColumn();
                    }
                }

                if (!this.CssStyle.WebHeight.IsAuto && !this.CssStyle.WebHeight.RealValue.IsRelative)
                {
                    checkRowHeight();
                }

                for (int i = 0; i < tempTable.Count; i++)
                {
                    VisualTableRow rowMeasure = (VisualTableRow)tempTable[i];
                    rowMeasure.InvalidateMeasure();
                    rowMeasure.getSetWidthList = finalCol;
                    thisSize.Height = rowMeasure.getSetMaxRowHeight - TopEdge - BottomEdge;
                    rowMeasure.MeasureNoClip(thisSize, windowSize, wasCalledByMeasure);
                }
            }
            else
            {
                measureNoCol();
            }

            MeasureTable(); //return size
            RealDesiredSize = returnSize;

            visualChildrenCount = rowCount;

            return returnSize;
        }

        //make column for when minimum size have to be used to calculate the column width
        public void makeColumnMin()
        {
            rowMinWidth = new List<double>();
            rowHeight = new List<double>();
            temColSize = new List<double>();

            int x = 0;
            tempRow = (VisualTableRow)tempTable[x];
            

            int cellCount = tempRow.getCellCount;
            int totalCell = cellCount * tempTable.Count;

            for (int j = 0; j < tempTable.Count; j++) //row
            {
                VisualTableRow tempWidthRow = (VisualTableRow)tempTable[j];
                rowMinWidth.Add(tempWidthRow.getTotalMinWidth);
                rowHeight.Add(tempWidthRow.getSetMaxRowHeight);
            }

            colMAX = new double[cellCount];

            for (int i = 0; i < cellCount; i++) //cell
            {
                tempCol = new double[tempTable.Count];
                for (int j = 0; j < tempTable.Count; j++)//row
                {
                    VisualTableRow tempWidthRow = (VisualTableRow)tempTable[j];
                    tempCol[j] = tempWidthRow.getSetWidthMinList[i];

                    if (tempCol[j] > colMAX[i])
                    {
                        colMAX[i] = tempCol[j];
                    }
                }
                temColSize.Add(colMAX[i]);
            }

            double totColWidth = 0;
            finalCol = new double[temColSize.Count];

            for (int i = 0; i < temColSize.Count; i++)
            {
                totColWidth += (double)temColSize[i];
                finalCol[i] = (double)temColSize[i];
            }
        }

        //measure the size of the table
        public void MeasureTable()
        {
            WebLength widthReal = this.CssStyle.WebWidth;
            WebLength heightReal = this.CssStyle.WebHeight;

            if (widthReal.IsAuto || widthReal.RealValue.IsRelative)
            {
                rowMaxWidth = 0;
                for (int i = 0; i < rowWidth.Count; i++)
                {
                    VisualTableRow temRow = tempTable[i];
                    if (temRow.DesiredSize.Width > rowMaxWidth)
                    {
                        rowMaxWidth = temRow.DesiredSize.Width;
                    }
                }

                returnSize.Width = rowMaxWidth + LeftEdge + RightEdge;
            }
            else
            {
                if (useMinWidth && fit)
                {
                    double currentTotWidth = this.TotalWidth;

                    for (int i = 0; i < rowMinWidth.Count; i++)
                    {
                        if (rowMinWidth[i] > currentTotWidth)
                        {
                            currentTotWidth = rowMinWidth[i];
                        }
                    }
                    returnSize.Width = currentTotWidth + LeftEdge + RightEdge;
                }
                else if (!useMinWidth)
                {
                    double currentTotWidth = this.TotalWidth;
                    returnSize.Width = currentTotWidth + LeftEdge + RightEdge;
                }
            }

            if (heightReal.IsAuto || heightReal.RealValue.IsRelative)
            {
                double rowTotHeight = 0;
                for (int i = 0; i < rowHeight.Count; i++)
                {
                    VisualTableRow temRow = tempTable[i];
                    rowTotHeight += temRow.DesiredSize.Height;
                }

                returnSize.Height = rowTotHeight + TopEdge + BottomEdge;
            }
            else if (!this.CssStyle.WebHeight.IsAuto && !this.CssStyle.WebHeight.RealValue.IsRelative)
            {
                if (rowTotMinHeight > this.CssStyle.WebHeight.RealValue.Value)
                {
                    returnSize.Height = rowTotMinHeight;
                }
                else if (rowTotMinHeight < this.CssStyle.WebHeight.RealValue.Value)
                {
                    returnSize.Height = totalTallHeight + TopEdge + BottomEdge;
                }
            }
        }

        //make column for when table is set to auto or content fits in specified table size
        public void makeColumn()
        {
            rowWidth = new List<double>();
            rowHeight = new List<double>();
            temColSize = new List<double>();

            int x = 0;
            if (tempTable != null && tempTable.Count > 0)
            {
                tempRow = tempTable[x];
                int cellCount = tempRow.getCellCount;
                int totalCell = cellCount * tempTable.Count;

                for (int j = 0; j < tempTable.Count; j++) //row
                {
                    VisualTableRow tempWidthRow = (VisualTableRow)tempTable[j];
                    rowWidth.Add(tempWidthRow.getTotalWidth);
                    rowHeight.Add(tempWidthRow.getSetMaxRowHeight);
                }

                colMAX = new double[cellCount];

                for (int i = 0; i < cellCount; i++) //cell
                {
                    tempCol = new double[tempTable.Count];
                    for (int j = 0; j < tempTable.Count; j++)//row
                    {
                        VisualTableRow tempWidthRow = (VisualTableRow)tempTable[j];
                        tempCol[j] = tempWidthRow.getSetWidthList[i];

                        if (tempCol[j] > colMAX[i])
                        {
                            colMAX[i] = tempCol[j];
                        }
                    }
                    temColSize.Add(colMAX[i]);
                }

                double totColWidth = 0;
                finalCol = new double[temColSize.Count];

                for (int i = 0; i < temColSize.Count; i++)
                {
                    totColWidth += (double)temColSize[i];
                    finalCol[i] = (double)temColSize[i];
                }
            }
            
        }

        //make column for when table size is oversized compare to actual contents
        public void makeFatColumn()
        {
            double[] fatCol = new double[finalCol.Length];
            double totalNormal = 0;

            for (int i = 0; i < finalCol.Length; i++)
            {
                fatCol[i] = finalCol[i];
                totalNormal += fatCol[i];
            }
           
            for (int i = 0; i < fatCol.Length; i++)
            {
                double tempFatCol = fatCol[i];
                double normFat = (this.TotalWidth - LeftEdge - RightEdge) / totalNormal;
                tempFatCol = tempFatCol * normFat;
                fatCol[i] = tempFatCol;
            }


            for (int i = 0; i < fatCol.Length; i++)
            {
                finalCol[i] = fatCol[i];
            }
        }

        //get each rows height
        public void getRowHeights()
        {
            for (int i = 0; i < rowCount; i++)
            {
                tempTable[i].calcMaxRowHeight();
            }
        }

        //get each rows minimum, maximum height 
        public void checkRowHeight()
        {
            if (rowTotMinHeight < this.CssStyle.WebHeight.RealValue.Value)
            {
                double tempTallHeight = this.TotalHeight;
                double tempMaxHeight = 0;
                for (int i = 0; i < tempTable.Count; i++)
                {
                    tempMaxHeight += tempTable[i].getSetMaxRowHeight;
                }

                for (int i = 0; i < tempTable.Count; i++)
                {
                    VisualTableRow temRow = tempTable[i];
                    double tempRat = tempTallHeight / tempMaxHeight;
                    double tempRowHeight = temRow.getSetMaxRowHeight * tempRat;
                    temRow.getSetMaxRowHeight = tempRowHeight;
                    totalTallHeight += tempRowHeight + TopEdge + BottomEdge;
                }
            }
        }

        public void measureNoCol()
        {
            rowWidth = new List<double>();
            rowHeight = new List<double>();

            for (int j = 0; j < tempTable.Count; j++)
            {
                VisualTableRow tempWidthRow = (VisualTableRow)tempTable[j];
                rowWidth.Add(tempWidthRow.getTotalWidth);
                rowHeight.Add(tempWidthRow.getSetMaxRowHeight);
            }
        }
    }
}
