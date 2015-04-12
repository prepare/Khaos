using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Diagnostics;
using CssStyle = CascadingStyleSheets.Style;
using CascadingStyleSheets;
using System.Windows.Input;


namespace RenderEngine
{
    public class VisualText : VisualNode
    {

        protected TextSectionManager SplitManager 
        { 
            get 
            { 
                return this.splitManager as TextSectionManager; 
            } 
        }

        private TextDecorationCollection textDecoration;
        private FormattedText renderText;

        public VisualText(VisualNode parentNode)
            : base(parentNode)
        {
            this.Text = "";
            this.textDecoration = new TextDecorationCollection();

            CssStyle.WebDisplay.RealValue = CascadingStyleSheets.DisplayEnum.Inline;
        }

        protected VisualText(VisualText splitMaker, int beginIndex)
            : base(splitMaker, beginIndex)
        {
            this.textDecoration = new TextDecorationCollection();
        }

        public override string ItemName
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            if (renderText != null)
                return renderText.Text; //SplitManager.GetSubString(logicalBeginIndex, logicalBeginIndex + visualChildrenCount);
            else
                return SplitManager.NodeText;
        }

        protected override void CreateSectionManager()
        {
            this.splitManager = new TextSectionManager(); // CssStyle style style);
        }

        public string Text
        {
            get { return SplitManager.NodeText; }
            set 
            {
                SplitManager.NodeText = value;
                visualChildrenCount = (SplitManager.Count - 2);
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 0;
            }
        }

        public override int Count
        {
            get
            {
                return 0;
            }
        }

        protected override bool CanSectionFit(double lineWidthLeft)
        {
            bool result = true;

            // If there is text to measure
            if (SplitManager.Count - logicalBeginIndex > 0) 
            {
                // Get the minimum possible contents
                FormattedText formattedText = SplitManager.GetFormattedSubString(logicalBeginIndex, logicalBeginIndex + 1);
                
                // if width left is less than desired width return false
                result = !(lineWidthLeft < formattedText.WidthIncludingTrailingWhitespace);
                
            }
            return result;
        }

        protected override double GetMinWidth()
        {
            double minimumWidth = 0;

            for (int tIndex = logicalBeginIndex; tIndex < (logicalBeginIndex + visualChildrenCount); tIndex++)
            {
                double result = SplitManager.TokenSize(tIndex);
                if (result > minimumWidth)
                    minimumWidth = result;
            }

            return Math.Max(minWidth, minimumWidth);
        }

        protected override Size MeasureVisual(Size constraint)
        {
            //Debug.WriteLine(constraint);

            Size resultSize = new Size();

            FormattedText formattedText = SplitManager.GetFormattedSubString(logicalBeginIndex);
            FormattedText minFormattedText = SplitManager.GetFormattedSubString(logicalBeginIndex, logicalBeginIndex + 1);

            // Test constraint against min width
            if (constraint.Width < minFormattedText.WidthIncludingTrailingWhitespace)
            {
                // Case never fits
                formattedText = minFormattedText;
                visualChildrenCount = (SplitManager.Count - logicalBeginIndex - 1);
                CreateSplit(logicalBeginIndex + 1);
            }
            else if (constraint.Width < formattedText.WidthIncludingTrailingWhitespace)
            {
                // Case will fit

                //// Binary Search
                ///////////////////

                int first = logicalBeginIndex;
                int last = (SplitManager.Count); // -1

                while (first <= last)
                {
                    int mid = (int)Math.Ceiling((double)(first + last) / 2);  // compute mid point.
                    //if ( mid <= first ) 

                    formattedText = SplitManager.GetFormattedSubString(logicalBeginIndex, mid);
                    int result = IsBestFit(constraint.Width, formattedText.WidthIncludingTrailingWhitespace, mid);

                    if (result > 0)
                        first = mid + 1;  // repeat search in top half.
                    else if (result < 0)
                        last = mid - 1; // repeat search in bottom half.
                    else
                    {
                        // found it
                        visualChildrenCount = (mid - logicalBeginIndex - 1);
                        this.CreateSplit(mid); // + 1
                        break;
                    }
                }
            }

            //if (formattedText.Text.Length < 1)
                //throw new Exception("Visual Text cannot have a length of zero.");

            renderText = formattedText;

            resultSize.Width = renderText.WidthIncludingTrailingWhitespace;
            resultSize.Height = Math.Max(CssStyle.WebLineHeight.RealValue.Value, renderText.Height + 1);

            if (resultSize.Width <= 0)
            {
                resultSize.Width = 1;
                Debug.WriteLine("Changed text result size: file: VisualText.cs, line: 180");
            }

            this.RealDesiredSize = resultSize;
            return resultSize;
        }

        protected int IsBestFit(double sizeLeft, double desiredSize, int end)
        {
            int result = 0;

            // It must fit
            if (sizeLeft < desiredSize)
            {
                // If the size left is less than the size needed
                result = -1;
            }
            else
            {
                // does fit

                // compare with one token over
                if (end < (SplitManager.Count - 1))
                {
                    FormattedText formattedText = SplitManager.GetFormattedSubString(end, end + 1);

                    // If the addition of a single token makes the string too large then
                    // the text is best fitting: return true.

                    double oneSizeUp = desiredSize + formattedText.WidthIncludingTrailingWhitespace;

                    //Debug.WriteLine("oneSizeUp(" + oneSizeUp + ") < sizeLeft(" + sizeLeft + ") == " + (oneSizeUp < sizeLeft));
                    if (oneSizeUp < sizeLeft) 
                        result = 1;
                }
                // else
                // This is the last word
            }

            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }

        /// <summary>
        /// Creates a split or continued section from the current node
        /// </summary>
        /// <param name="splitBegin">The index the split will begin at</param>
        private void CreateSplit(int splitBeginIndex)
        {
            if (splitBeginIndex < (SplitManager.Count - 1))
            {
                // Create Split
                VisualText newSplit = new VisualText(((splitMaker == null) ? this : (splitMaker as VisualText)), splitBeginIndex);

                // Add to Visual Node
                splitManager.AddSection(newSplit, this);
            }

        }

        protected void RefreshTextDecoration(CascadingStyleSheets.TextDecorationEnum value)
        {
            textDecoration.Clear();

            switch (value)
            {

                case CascadingStyleSheets.TextDecorationEnum.None:
                    break;
                case CascadingStyleSheets.TextDecorationEnum.Underline:

                    textDecoration.Add(new TextDecoration( TextDecorationLocation.Underline,
                        new Pen(CssStyle.WebColor.RealValue, 2), 0, 
                        TextDecorationUnit.FontRecommended, TextDecorationUnit.FontRecommended));

                    break;
                case CascadingStyleSheets.TextDecorationEnum.Overline:

                    textDecoration.Add(new TextDecoration( TextDecorationLocation.OverLine,
                        new Pen(CssStyle.WebColor.RealValue, 2), 0,
                        TextDecorationUnit.FontRecommended, TextDecorationUnit.FontRecommended));


                    break;
                case CascadingStyleSheets.TextDecorationEnum.LineThrough:

                    textDecoration.Add(new TextDecoration(TextDecorationLocation.Strikethrough,
                        new Pen(CssStyle.WebColor.RealValue, 2), 0,
                        TextDecorationUnit.FontRecommended, TextDecorationUnit.FontRecommended));

                    break;
            }

            renderText.SetTextDecorations(textDecoration);

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            RefreshTextDecoration(CssStyle.WebTextDecoration.RealValue);

            Point startingPosition = new Point();
            drawingContext.DrawText(renderText, startingPosition);

            //Debuging
            //Pen pen = new Pen();
            //pen.Brush = new SolidColorBrush(Colors.Black);
            //pen.Thickness = 1;
            //pen.EndLineCap = PenLineCap.Square;
            //if (pen.CanFreeze) pen.Freeze();
            //drawingContext.DrawLine(pen, new Point(RenderSize.Width, 0), new Point(RenderSize.Width, RenderSize.Height));
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            // default I
            // lable Arrow
            // Anq Hand

            if (!CssStyle.WebCursor.ForText)
            {
                Mouse.OverrideCursor = Cursors.IBeam;
            }
            else
            {
                base.OnMouseEnter(e);
            }
        }

        internal override void ResetSplitting()
        {
            this.SplitManager.ResetSplitting();
            this.DebugID = -1;
            this.visualChildrenCount = (SplitManager.Count - 1); //Math.Max
            this.InvalidateMeasure();
        }
    }
}
