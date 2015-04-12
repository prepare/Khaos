using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Globalization;
using System.Windows;

namespace RenderEngine
{
    public class TextSectionManager : SectionManager
    {
        // Variables

        protected string nodeText;
        protected List<int> wordBeginIndex;
        protected List<double> wordSizes;

        // Properties

        public override CascadingStyleSheets.Style CssStyle
        {
            get
            {
                if (parent == null)
                    return base.CssStyle;
                return parent.CssStyle;
            }
        }

        public string NodeText 
        {
            set 
            {
                nodeText = value;// +" ";
                CalculateSplits();
            }
            get 
            { 
                return nodeText; 
            }
        }

        // Constructor

        public TextSectionManager() : base ()
        {
            nodeText = "";
            CalculateSplits();
        }

        /// <summary>
        /// Calculate the sizes and indices of tokens
        /// </summary>
        private void CalculateSplits()
        {
            if (nodeText == null || nodeText == "") nodeText = "  ";

            int startCap = (nodeText.Length / 10);
            wordBeginIndex = new List<int>(startCap);
            wordSizes = new List<double>(startCap);

            // Start at index [1] end at [n-1]
            wordBeginIndex.Add(0);
            bool hitWhitespace = false;
            for (int characterIndex = 1; characterIndex < (nodeText.Length - 1); characterIndex++)
            {
                // Find the start of the next token and add it to the list
                if (char.IsWhiteSpace(nodeText[characterIndex]))
                {
                    hitWhitespace = true;
                }
                else if (hitWhitespace == true)
                {
                    hitWhitespace = false;
                    wordBeginIndex.Add(characterIndex);
                    wordSizes.Add(GetFormattedSubString(wordBeginIndex.Count - 2, wordBeginIndex.Count - 1).WidthIncludingTrailingWhitespace);
                }
            }
            wordBeginIndex.Add(nodeText.Length);
            wordSizes.Add(GetFormattedSubString(wordBeginIndex.Count - 2, wordBeginIndex.Count - 1).WidthIncludingTrailingWhitespace);

            // Trim
            wordSizes.TrimExcess();
            wordBeginIndex.TrimExcess();
        }

        public override int Count
        {
            get { return wordBeginIndex.Count; }
        }
        public new int this[int index]
        {
            get { return wordBeginIndex[index]; }
        }

        public double TokenSize(int index)
        {
            return wordSizes[index];
        }

        public string GetSubString(int begin, int end)
        {
            int start = wordBeginIndex[begin];
            return nodeText.Substring(start, wordBeginIndex[end] - start);
        }

        public FormattedText GetFormattedSubString(int begin)
        {
            return FormatString(GetSubString(begin));
        }

        public FormattedText GetFormattedSubString(int begin, int end)
        {
            return FormatString(GetSubString(begin, end));
        }

        /// <summary>
        /// Format String
        /// </summary>
        protected FormattedText FormatString(string str)
        {
            SolidColorBrush color = this.CssStyle.WebColor.RealValue;
            if (color == null) color = new SolidColorBrush(Colors.DarkOrange);

            FormattedText result = new FormattedText(str,
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            new Typeface(this.CssStyle.WebFontFamily.RealValue,
                this.CssStyle.WebFontStyle.RealValue,
                this.CssStyle.WebFontWeight.RealValue,
                this.CssStyle.WebFontStretch.RealValue),
            this.CssStyle.WebFontSize.Value,
            color);

            //result.SetTextDecorations(

            return result;
        }

        public string GetSubString(int begin)
        {
            return nodeText.Substring(wordBeginIndex[begin]);
        }

        public override void AddSection(VisualNode section, VisualNode splitMaker)
        {
            // Add the new section to the node 
            // above so it can be rendered
            sections.AddLast(section);
            parent.AddAfter(section, splitMaker);
        }    
    }
}
