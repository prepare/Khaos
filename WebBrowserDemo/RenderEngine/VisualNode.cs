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
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using System.Collections;
using DOMCore;
using PhoenixOI;


namespace RenderEngine
{

    /// <summary>
    /// Visual Node Represents the box model logic
    /// </summary>
    public partial class VisualNode : FrameworkElement, IScrollInfo//Panel
    {
        // Debug Variables
        private static int currentID = -1;

        // Shared variables for pages
        protected VisualNode visualRoot;
        protected IPageHost pageHost;
        protected Request request;
 
        // Variables
        protected SectionManager splitManager = null;
        internal VisualNode splitMaker = null;
        protected bool isFirst = true;
        protected bool isLast = true;

        protected int logicalChildCount = 0;
        protected int visualChildrenCount = 0;
        protected int lastVisualChildrenCount = 0;
        protected int logicalBeginIndex = 0;
        //protected bool isRoot = false;

        protected bool wasCalledByMeasure;
        private Size availableSize;
        protected Size windowSize;

        private ScrollBar scrolly;
        private ScrollBar scrollx;
        private double scrollBarWidth = 17;

        // Properties

        /// <summary>
        /// Html Element
        /// </summary>
        public virtual IHTMLElement Element
        {
            get
            {
                return splitManager.Element;
            }
            set 
            {
                splitManager.Element = value;
                //this.Name = value.getAttribute("name");
                //this.Uid = value.Id
            }
        }

        /// <summary>
        /// Background Color
        /// </summary>
        public SolidColorBrush Background
        {
            get { return splitManager.CssStyle.WebBackgroundColor.RealValue; }
            set { splitManager.CssStyle.WebBackgroundColor.RealValue = value; }
        }

        /// <summary>
        /// Css Style
        /// </summary>
        public CssStyle CssStyle
        {
            get 
            { 
                return splitManager.CssStyle; 
            }
        }

        /// <summary>
        ///  Node name
        /// </summary>
        public virtual string ItemName
        {
            get;
            set;
        }

        /// <summary>
        /// The starting index for the node child array (for splitting)
        /// </summary>
        public int LogicalBeginIndex
        { 
            get { return logicalBeginIndex; } 
        }

        /// <summary>
        /// 
        /// </summary>
        public Size RealDesiredSize 
        { 
            get;
            protected set; 
        }

        /// <summary>
        /// Unique Node Number
        /// </summary>
        public int DebugID { get; set; }

        #region // :::: Real Sizes ::::

        /// <summary>
        /// Checks is the css width is auto
        /// </summary>
        protected bool widthAuto
        {
            get { return splitManager.CssStyle.WebWidth.IsAuto; }
        }

        /// <summary>
        /// Checks if the css hight is auto
        /// </summary>
        protected bool heightAuto
        {
            get { return splitManager.CssStyle.WebHeight.IsAuto; }
        }

        /// <summary>
        /// Calculates the minimum width of the node
        /// </summary>
        protected double minWidth
        {
            get
            {
                if (splitManager.CssStyle.WebMinWidth.IsAuto)
                    return 0d;
                // Min Width
                else if (splitManager.CssStyle.WebMinWidth.RealValue.Type == UnitType.Percentage)
                    return (splitManager.CssStyle.WebMinWidth.RealValue.Value * availableSize.Width);
                else
                    return splitManager.CssStyle.WebMinWidth.RealValue.Value;
            }
        }

        /// <summary>
        /// Calculates the css width of the node
        /// </summary>
        private void calculateWidth(out double widthLeft, out double totalPercent)
        {
            widthLeft = availableSize.Width;
            totalPercent = 0;

            // Width
            if (splitManager.CssStyle.WebWidth.IsAuto)
            {
                totalPercent += 1;
            }
            else if (splitManager.CssStyle.WebWidth.RealValue.IsRelative)
            {
                totalPercent += splitManager.CssStyle.WebWidth.RealValue.Value;
            }
            else
            {
                widthLeft -= splitManager.CssStyle.WebWidth.RealValue.Value;
            }

            if (isFirst)
            {
                // Margin
                if (splitManager.CssStyle.WebMarginLeft.IsAuto)
                {
                    totalPercent += 1;
                }
                else if (splitManager.CssStyle.WebMarginLeft.RealValue.IsRelative)
                {
                    totalPercent += splitManager.CssStyle.WebMarginLeft.RealValue.Value;
                }
                else
                {
                    widthLeft -= splitManager.CssStyle.WebMarginLeft.RealValue.Value;
                }

                // Border
                widthLeft -= splitManager.CssStyle.WebBorderLeftWidth.RealValue;

                // Padding
                if (splitManager.CssStyle.WebPaddingLeft.IsAuto)
                {
                    totalPercent += 1;
                }
                else if (splitManager.CssStyle.WebPaddingLeft.RealValue.IsRelative)
                {
                    totalPercent += splitManager.CssStyle.WebPaddingLeft.RealValue.Value;
                }
                else
                {
                    widthLeft -= splitManager.CssStyle.WebPaddingLeft.RealValue.Value;
                }
            }

            if (isLast)
            {
                // Margin
                if (splitManager.CssStyle.WebMarginRight.IsAuto)
                {
                    totalPercent += 1;
                }
                else if (splitManager.CssStyle.WebMarginRight.RealValue.IsRelative)
                {
                    totalPercent += splitManager.CssStyle.WebMarginRight.RealValue.Value;
                }
                else
                {
                    widthLeft -= splitManager.CssStyle.WebMarginRight.RealValue.Value;
                }

                // Border
                widthLeft -= splitManager.CssStyle.WebBorderRightWidth.RealValue;

                // Padding
                if (splitManager.CssStyle.WebPaddingRight.IsAuto)
                {
                    totalPercent += 1;
                }
                else if (splitManager.CssStyle.WebPaddingRight.RealValue.IsRelative)
                {
                    totalPercent += splitManager.CssStyle.WebPaddingRight.RealValue.Value;
                }
                else
                {
                    widthLeft -= splitManager.CssStyle.WebPaddingRight.RealValue.Value;
                }
            }
        }

        /// <summary>
        /// Calculates the desired content width of the node
        /// </summary>
        protected double desiredWidth
        {
            get
            {
                if (splitManager.CssStyle.WebWidth.IsAuto && this.CanShareLine)
                {
                    return 0d;
                }
                // Desired Width
                else if (splitManager.CssStyle.WebWidth.IsAuto || splitManager.CssStyle.WebWidth.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebWidth.IsAuto) ? 1 : splitManager.CssStyle.WebWidth.RealValue.Value;

                    double widthLeft, totalPercent;
                    calculateWidth(out widthLeft, out totalPercent);
                    totalPercent += selfPercent;

                    // if two parts have 100% width they both really have 50% each
                    return ((selfPercent * totalPercent) * widthLeft);   //(selfPercent / totalPercent)
                }
                else
                    return splitManager.CssStyle.WebWidth.RealValue.Value;
            }
        }

        /// <summary>
        /// alculates the minimum height of the node
        /// </summary>
        protected double minHeight
        {
            get
            {
                if (splitManager.CssStyle.WebMinHeight.IsAuto)
                    return 0d;
                // Min Width
                else if (splitManager.CssStyle.WebMinHeight.RealValue.Type == UnitType.Percentage)
                    return (splitManager.CssStyle.WebMinHeight.RealValue.Value * availableSize.Width);
                else
                    return splitManager.CssStyle.WebMinHeight.RealValue.Value;
            }
        }

        /// <summary>
        /// Calculates the css height of the node
        /// </summary>
        private void calculateHeight(out double heightLeft, out double totalPercent)
        {

            heightLeft = availableSize.Height;
            totalPercent = 0;

            // Height
            if (splitManager.CssStyle.WebHeight.IsAuto)
            {
                totalPercent += 1;
            }
            else if (splitManager.CssStyle.WebHeight.RealValue.IsRelative)
            {
                totalPercent += splitManager.CssStyle.WebHeight.RealValue.Value;
            }
            else
            {
                heightLeft -= splitManager.CssStyle.WebHeight.RealValue.Value;
            }

            // Margin
            if (splitManager.CssStyle.WebMarginTop.IsAuto)
            {
                totalPercent += 1;
            }
            else if (splitManager.CssStyle.WebMarginTop.RealValue.IsRelative)
            {
                totalPercent += splitManager.CssStyle.WebMarginTop.RealValue.Value;
            }
            else
            {
                heightLeft -= splitManager.CssStyle.WebMarginTop.RealValue.Value;
            }

            // Border
            heightLeft -= splitManager.CssStyle.WebBorderTopWidth.RealValue;

            // Padding
            if (splitManager.CssStyle.WebPaddingTop.IsAuto)
            {
                totalPercent += 1;
            }
            else if (splitManager.CssStyle.WebPaddingTop.RealValue.IsRelative)
            {
                totalPercent += splitManager.CssStyle.WebPaddingTop.RealValue.Value;
            }
            else
            {
                heightLeft -= splitManager.CssStyle.WebPaddingTop.RealValue.Value;
            }

            // Margin
            if (splitManager.CssStyle.WebMarginBottom.IsAuto)
            {
                totalPercent += 1;
            }
            else if (splitManager.CssStyle.WebMarginBottom.RealValue.IsRelative)
            {
                totalPercent += splitManager.CssStyle.WebMarginBottom.RealValue.Value;
            }
            else
            {
                heightLeft -= splitManager.CssStyle.WebMarginBottom.RealValue.Value;
            }

            // Border
            heightLeft -= splitManager.CssStyle.WebBorderBottomWidth.RealValue;

            // Padding
            if (splitManager.CssStyle.WebPaddingBottom.IsAuto)
            {
                totalPercent += 1;
            }
            else if (splitManager.CssStyle.WebPaddingBottom.RealValue.IsRelative)
            {
                totalPercent += splitManager.CssStyle.WebPaddingBottom.RealValue.Value;
            }
            else
            {
                heightLeft -= splitManager.CssStyle.WebPaddingBottom.RealValue.Value;
            }
        }

        /// <summary>
        /// Calculates the desired content height of the node
        /// </summary>
        protected double desiredHeight
        {
            get
            {

                // Even blocks take smallest height
                if (splitManager.CssStyle.WebHeight.IsAuto)
                {
                    //return splitManager.CssStyle.WebLineHeight.RealValue.Value..
                    return 10d; // 10 will be the smallest height.. should be line height
                }
                // Desired Width
                else if (splitManager.CssStyle.WebHeight.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebHeight.IsAuto) ? 1 : splitManager.CssStyle.WebHeight.RealValue.Value;

                    double heightLeft, totalPercent;
                    calculateHeight(out heightLeft, out totalPercent);
                    totalPercent += selfPercent;

                    //return ((selfPercent / totalPercent) * heightLeft);
                    return ((selfPercent * totalPercent) * heightLeft);
                }
                else
                    return splitManager.CssStyle.WebHeight.RealValue.Value;


                //if (splitManager.CssStyle.WebHeight.IsAuto)
                //{ 
                //    return (availableSize.Width - (this.TopEdge + this.BottomEdge));
                //}
                //// Desired Width
                //if (splitManager.CssStyle.WebHeight.RealValue.Type == UnitType.Percentage)
                //    return (splitManager.CssStyle.WebHeight.RealValue.Value * availableSize.Width);
                //else
                //    return splitManager.CssStyle.WebHeight.RealValue.Value;
            }
        }


        /// <summary>
        /// Calculates the desired content width of the node with min width
        /// </summary>
        protected double ContentWidth
        {
            get
            {
                double minW = minWidth;
                double desW = desiredWidth;
                return (minW > desW) ? minW : desW;
            }
        }

        /// <summary>
        /// Calculates the desired content height of the node with min height
        /// </summary>
        protected double ContentHeight
        {
            get
            {
                double minH = minHeight;
                double desH = desiredHeight;
                return (minH > desH) ? minH : desH;
            }
        }

        protected double PaddingLeft
        {
            get
            {
                if (splitManager.CssStyle.WebPaddingLeft.IsAuto || splitManager.CssStyle.WebPaddingLeft.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebPaddingLeft.IsAuto) ? 1 : splitManager.CssStyle.WebPaddingLeft.RealValue.Value;

                    double widthLeft, totalPercent;
                    calculateWidth(out widthLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * widthLeft);
                }
                else if (splitManager.CssStyle.WebPaddingLeft.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebPaddingLeft.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double PaddingRight
        {
            get
            {
                if (splitManager.CssStyle.WebPaddingRight.IsAuto || splitManager.CssStyle.WebPaddingRight.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebPaddingRight.IsAuto) ? 1 : splitManager.CssStyle.WebPaddingRight.RealValue.Value;

                    double widthLeft, totalPercent;
                    calculateWidth(out widthLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * widthLeft);
                }
                else if (splitManager.CssStyle.WebPaddingRight.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebPaddingRight.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double PaddingTop
        {
            get
            {
                if (splitManager.CssStyle.WebPaddingTop.IsAuto || splitManager.CssStyle.WebPaddingTop.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebPaddingTop.IsAuto) ? 1 : splitManager.CssStyle.WebPaddingTop.RealValue.Value;

                    double heightLeft, totalPercent;
                    calculateHeight(out heightLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * heightLeft);
                }
                else if (splitManager.CssStyle.WebPaddingTop.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebPaddingTop.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double PaddingBottom
        {
            get
            {
                if (splitManager.CssStyle.WebPaddingBottom.IsAuto || splitManager.CssStyle.WebPaddingBottom.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebPaddingBottom.IsAuto) ? 1 : splitManager.CssStyle.WebPaddingBottom.RealValue.Value;

                    double heightLeft, totalPercent;
                    calculateHeight(out heightLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * heightLeft);
                }
                else if (splitManager.CssStyle.WebPaddingBottom.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebPaddingBottom.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double MarginLeft
        {
            get
            {
                if (splitManager.CssStyle.WebMarginLeft.IsAuto || splitManager.CssStyle.WebMarginLeft.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebMarginLeft.IsAuto) ? 1 : splitManager.CssStyle.WebMarginLeft.RealValue.Value;

                    double widthLeft, totalPercent;
                    calculateWidth(out widthLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * widthLeft);
                }
                else if (splitManager.CssStyle.WebMarginLeft.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebMarginLeft.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double MarginRight
        {
            get
            {
                if (splitManager.CssStyle.WebMarginRight.IsAuto || splitManager.CssStyle.WebMarginRight.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebMarginRight.IsAuto) ? 1 : splitManager.CssStyle.WebMarginRight.RealValue.Value;

                    double widthLeft, totalPercent;
                    calculateWidth(out widthLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * widthLeft);
                }
                else if (splitManager.CssStyle.WebMarginRight.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebMarginRight.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double MarginTop
        {
            get
            {
                if (splitManager.CssStyle.WebMarginTop.IsAuto || splitManager.CssStyle.WebMarginTop.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebMarginTop.IsAuto) ? 1 : splitManager.CssStyle.WebMarginTop.RealValue.Value;

                    double heightLeft, totalPercent;
                    calculateHeight(out heightLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * heightLeft);
                }
                else if (splitManager.CssStyle.WebMarginTop.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebMarginTop.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double MarginBottom
        {
            get
            {
                if (splitManager.CssStyle.WebMarginBottom.IsAuto || splitManager.CssStyle.WebMarginBottom.RealValue.Type == UnitType.Percentage)
                {
                    double selfPercent = (splitManager.CssStyle.WebMarginBottom.IsAuto) ? 1 : splitManager.CssStyle.WebMarginBottom.RealValue.Value;

                    double heightLeft, totalPercent;
                    calculateHeight(out heightLeft, out totalPercent);

                    return ((selfPercent / totalPercent) * heightLeft);
                }
                else if (splitManager.CssStyle.WebMarginBottom.RealValue.Type == UnitType.Pixel)
                {
                    return splitManager.CssStyle.WebMarginBottom.RealValue.Value;
                }
                return 0d;
            }
        }

        protected double LeftEdge
        {
            get
            {
                return this.PaddingLeft +
                        splitManager.CssStyle.WebBorderLeftWidth.RealValue +
                        this.MarginLeft;
            }
        }

        protected double RightEdge
        {
            get
            {
                return this.PaddingRight +
                    splitManager.CssStyle.WebBorderRightWidth.RealValue +
                    this.MarginRight;
            }
        }

        protected double TopEdge
        {
            get
            {
                return this.PaddingTop +
                    splitManager.CssStyle.WebBorderTopWidth.RealValue +
                    this.MarginTop;
            }
        }

        protected double BottomEdge
        {
            get
            {
                return this.PaddingBottom +
                    splitManager.CssStyle.WebBorderBottomWidth.RealValue +
                    this.MarginBottom;
            }
        }

        protected double LeftContentEdge
        {
            get
            {
                return this.PaddingLeft +
                        splitManager.CssStyle.WebBorderLeftWidth.RealValue;
            }
        }

        protected double RightContentEdge
        {
            get
            {
                return this.PaddingRight +
                    splitManager.CssStyle.WebBorderRightWidth.RealValue;
            }
        }

        protected double TopContentEdge
        {
            get
            {
                return this.PaddingTop +
                    splitManager.CssStyle.WebBorderTopWidth.RealValue;
            }
        }

        protected double BottomContentEdge
        {
            get
            {
                return this.PaddingBottom +
                    splitManager.CssStyle.WebBorderBottomWidth.RealValue;
            }
        }

        /// <summary>
        /// Calculates the total css node width
        /// </summary>
        protected double TotalWidth
        {
            get
            {
                double result = this.ContentWidth;
                if (this.isFirst)
                {
                    result += this.PaddingLeft +
                        splitManager.CssStyle.WebBorderLeftWidth.RealValue +
                        this.MarginLeft;
                }
                if (this.isLast)
                {
                    result += this.PaddingRight +
                        splitManager.CssStyle.WebBorderRightWidth.RealValue +
                        this.MarginRight;
                }
                return result;
            }
        }

        /// <summary>
        /// Calculates the total css node height
        /// </summary>
        protected double TotalHeight
        {
            get
            {
                return this.ContentHeight +
                         this.PaddingTop +
                         this.PaddingBottom +
                         splitManager.CssStyle.WebBorderTopWidth.RealValue +
                         splitManager.CssStyle.WebBorderBottomWidth.RealValue +
                         this.MarginTop +
                         this.MarginBottom;
            }
        }

        #endregion

        // Constructors

        /// <summary>
        /// Creates a visual node that will be the child of a node
        /// </summary>
        public VisualNode(VisualNode parentNode)
            : this()
        { // Made from external code
            if (parentNode != null)
            {
                parentNode.AddChild(this);
                this.visualRoot = parentNode.visualRoot;
                this.request = parentNode.request;
            }
        }

        /// <summary>
        /// Creates a visual node that will inherit from a given css style
        /// </summary>
        /// <param name="ancestor"></param>
        protected VisualNode()
        {
            CreateSectionManager();

            isFirst = true;
            isLast = true;

            // Add Scrolling X

            scrollx = new ScrollBar();
            scrollx.Orientation = Orientation.Horizontal;
            scrollx.ViewportSize = 100;
            scrollx.Opacity = 0.75;
            scrollx.Scroll += new ScrollEventHandler(scrollx_Scroll);

            this.AddVisualChild(scrollx);
            this.AddLogicalChild(scrollx);

            // Add Scrolling Y

            scrolly = new ScrollBar();
            scrolly.Orientation = Orientation.Vertical;
            scrolly.ViewportSize = 100;
            scrolly.Opacity = 0.75;
            scrolly.Scroll += new ScrollEventHandler(scrolly_Scroll);

            this.AddVisualChild(scrolly);
            this.AddLogicalChild(scrolly);
        }
        
        /// <summary>
        /// Update Scroll Y
        /// </summary>
        void scrolly_Scroll(object sender, ScrollEventArgs e)
        {
            SetVerticalOffset(e.NewValue);
        }

        /// <summary>
        /// Update Scroll X
        /// </summary>
        void scrollx_Scroll(object sender, ScrollEventArgs e)
        {
            SetHorizontalOffset(e.NewValue);
        }
        
        /// <summary>
        /// Create section manager to handle children and splitting
        /// </summary>
        protected virtual void CreateSectionManager()
        {
            this.splitManager = new SectionManager();
        }

        /// <summary>
        /// Called to construct a Visual Node created by the splitting of an existing node
        /// </summary>
        protected VisualNode(VisualNode splitMaker, int beginIndex)
        { // Made from split
            this.splitManager = splitMaker.splitManager;
            this.logicalBeginIndex = beginIndex;

            isFirst = false;
            isLast = true;

            this.splitMaker = splitMaker;
        }

        /// <summary>
        /// Creates a split or continued section from the current node
        /// </summary>
        /// <param name="splitBegin">The index the split will begin at</param>
        /// <returns>The new split section</returns>
        private VisualNode CreateSplit(int splitBegin)
        {
            // Create Split
            VisualNode newSplit = new VisualNode((this.splitMaker == null) ? this : splitMaker, splitBegin);

            newSplit.visualRoot = this.visualRoot;
            newSplit.pageHost = this.pageHost;
            newSplit.request = this.request;

            // *Sanity Check* \\
            // It is impossible to contain more visuals than there are children
            if (splitBegin >= splitManager.Count)
                throw new Exception("It is impossible to contain more visuals than there are children");

            // Add to Visual Node
            splitManager.AddSection(newSplit, this);

            return newSplit;
        }

        /// <summary>
        /// Creates the root visual node
        /// </summary>
        public static VisualNode CreateVisualRoot(IPageHost host, Request request)
        {
            VisualNode node = new VisualNode();
            node.visualRoot = node;
            node.pageHost = host;
            node.request = request;
            return node;
        }

        // Methods

        /// <summary>
        /// Adds a new child
        /// </summary>
        public void AddChild(VisualNode node)
        {
            if (node == null) throw new ArgumentNullException();

            node.pageHost = this.pageHost;
            node.visualRoot = this.visualRoot;
            node.request = this.request;

            node.splitManager.Parent = this;
            splitManager.AddChild(node);

            this.AddLocalChild(node);
            //base.Children.Add(node);
        }

        /// <summary>
        /// Removes a child
        /// </summary>
        public void RemoveChild(VisualNode node)
        {
            if (node == null) throw new ArgumentNullException();

            //this.Children.Remove(node);
            node.Disconnect();
            splitManager.RemoveChild(node);
        }

        /// <summary>
        /// Adds a new child
        /// </summary>
        internal void AddLocalChild(VisualNode node)
        {
            node.Disconnect();

            this.logicalChildCount++;
            this.AddLogicalChild(node);
            this.AddVisualChild(node);
        }

        /// <summary>
        /// Removes a child
        /// </summary>
        internal void RemoveLocalChild(VisualNode node)
        {
            node.Disconnect();
            //this.Children.Remove(node);
        }

        /// <summary>
        /// Adds a node after an existing node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeBefore"></param>
        internal void AddAfter(VisualNode node, VisualNode nodeBefore)
        {
            if (node == null || nodeBefore == null) throw new ArgumentNullException();

            splitManager.AddChildAfter(node, nodeBefore);
            AddLocalChild(node);
        }

        /// <summary>
        /// Disconnect Node
        /// </summary>
        protected void Disconnect()
        {
            if (this.Parent != null)
            {
                VisualNode parent = (this.Parent as VisualNode);

                parent.RemoveLogicalChild(this);
                parent.RemoveVisualChild(this);
                parent.logicalChildCount--;
            }
        }

        /// <summary>
        /// Links event system
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (Parent != null)
                {
                    for (int cIndex = 0; cIndex < logicalChildCount; cIndex++)
                    {
                        yield return splitManager.GetChild(this, cIndex);
                    }
                }
                yield break;
            }
        }

        /// <summary>
        /// The visual child of a given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            bool scroll = (CssStyle.WebOverflow.RealValue == OverflowEnum.Scroll);

            if (index > -1 && index < (scroll? (logicalChildCount + 2) : logicalChildCount))
            {
                if (index < logicalChildCount)
                    return splitManager.GetChild(this, index);
                    //return splitManager.GetChild(this, this.Count - index - 1);
                else if (index == logicalChildCount)
                    return scrollx;
                else
                    return scrolly;
            }
            else
                throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// The number of visual children that the node currently renders
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                if (this.Parent == null)
                    return 0;
                else if (CssStyle.WebOverflow.RealValue == OverflowEnum.Scroll)
                    return logicalChildCount + 2;
                else
                    return logicalChildCount;
            }
        }

        /// <summary>
        /// The visual child of a given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual VisualNode this[int index]
        {
            get
            {
                return splitManager.GetChild(this, index);
            }
        }

        /// <summary>
        /// The number of visual children that the node currently renders
        /// </summary>
        public virtual int Count
        {
            get { return logicalChildCount; }
        }

        /// <summary>
        /// Checks if the current node is managed by a parent node and
        /// that the node behaves like an inline element
        /// </summary>
        public virtual bool CanShareLine
        {
            get
            {
                bool noParent = splitManager.Parent == null;

                bool inline = CssStyle.WebDisplay.RealValue == DisplayEnum.Compact ||
                CssStyle.WebDisplay.RealValue == DisplayEnum.Inline ||
                CssStyle.WebDisplay.RealValue == DisplayEnum.InlineBlock ||
                CssStyle.WebDisplay.RealValue == DisplayEnum.RunIn;

                return noParent ? false : inline;
            }
        }

        /// <summary>
        /// Resets inline splitting
        /// </summary>
        internal virtual void ResetSplitting()
        {
            this.splitManager.ResetSplitting();
            this.DebugID = -1;
            this.visualChildrenCount = 0;
            this.InvalidateMeasure();
        }

        /// <summary>
        /// Refreshes Visual Calculations
        /// </summary>
        protected void InvalidateChildrenVisuals()
        {
            for (int index = 0; index < this.Count; index++)
            {
                VisualNode node = splitManager.GetChild(this, index);
                node.InvalidateVisual();
                node.InvalidateChildrenVisuals();
            }
        }

        /// <summary>
        /// Returns true if the current section or any part of it can fit in the
        /// width that is left availible for the current line.
        /// </summary>
        /// <param name="lineWidthLeft"></param>
        /// <returns></returns>
        protected virtual bool CanSectionFit(double lineWidthLeft)
        {   // This method only works for inline elements

            double wBorderl, wBorderr;
            wBorderl = splitManager.CssStyle.WebBorderRightWidth.RealValue;
            wBorderr = splitManager.CssStyle.WebBorderLeftWidth.RealValue;

            // If it fits check children
            if (lineWidthLeft >= this.TotalWidth)
            {

                if (CanShareLine)
                { // inline 
                    // false, check next layer

                    bool hasChild = 
                        (visualChildrenCount > 0) &&
                        (splitManager.GetChild(this, 0) != null);

                    if (hasChild)
                    {
                        // nesting subtracts from size left
                        VisualNode node = splitManager.GetChild(this, 0);

                        if (isFirst) lineWidthLeft -= this.PaddingLeft + this.MarginLeft + wBorderl;
                        if (isLast) lineWidthLeft -= this.PaddingRight + this.MarginRight + wBorderr;
                        return node.CanSectionFit(lineWidthLeft);
                    }
                    else
                    {
                        return (lineWidthLeft >= this.TotalWidth); // 22 March
                        // return true;
                    }
                }
                else
                { // block
                    // cannot be split
                    return false;
                }
            }
            else
            { // Does not fit
                return false;
            }
        }

        /// <summary>
        /// Get the size of the largest child
        /// </summary>
        protected virtual double GetMinWidth()
        {   
            // Recursive method ish
            // if node has fixed size return else get max child width

            if (this.widthAuto || CssStyle.WebWidth.RealValue.IsRelative)
            {
                double minimumWidth = 0;

                // For all children
                for (int cIndex = logicalBeginIndex; cIndex < logicalChildCount; cIndex++)
                {
                    VisualNode child = this[cIndex];

                    // Recursive call and get max
                    double result = child.GetMinWidth();
                    if (result > minimumWidth) 
                        minimumWidth = result;
                }

                minimumWidth = Math.Max(minWidth, minimumWidth);
                minimumWidth += this.LeftEdge + this.RightEdge;

                return minimumWidth;
            }
            else
            {
                return TotalWidth;
            }
        }
      
        /// <summary>
        /// Force clip to bounds correct clipping
        /// </summary>
        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            return ClipToBounds ? base.GetLayoutClip(layoutSlotSize) : null;
        }


        /// <summary>
        /// Bypasses WPF Size clipping
        /// </summary>
        /// <param name="constraint">Constraint Parent Imposes</param>
        /// <param name="windowSize">Largest Size a node can occupy</param>
        public void MeasureNoClip(Size constraint, Size windowSize, bool byMeasure)
        {
            this.calledByMeasureCheck = true;
            this.wasCalledByMeasure = byMeasure;
            this.availableSize = constraint;
            this.Measure(windowSize);
            this.calledByMeasureCheck = false;
        }

        bool calledByMeasureCheck = false;

        ///// <summary>
        ///// Measures the child elements and determines the size of the Node. 
        ///// Do not override this method, use Measure visual
        ///// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            windowSize = constraint;

            // Reset splitting
            if (this == visualRoot)
            {
                availableSize = windowSize;
                this.ResetSplitting();

                this.calledByMeasureCheck = true;
                this.wasCalledByMeasure = true;

                currentID = 0;
            }
            DebugID = currentID;
            currentID++;

            // Measure the node
            Size result = MeasureVisual(availableSize);

            // Refresh Scrollbars
            if (!CanShareLine && 
                CssStyle.WebOverflow.RealValue == OverflowEnum.Scroll && 
                scrollx != null)
            {
                scrolly.Width = scrollBarWidth;
                scrolly.Height = availableSize.Height;
                scrolly.ViewportSize = Math.Max(0, availableSize.Height); //availableSize.Height;

                scrollx.Height = scrollBarWidth;
                scrollx.Width = Math.Max(0, availableSize.Width - scrolly.Width);
                scrollx.ViewportSize = availableSize.Width;

                scrollx.Measure(availableSize);
                scrolly.Measure(availableSize);

                scrollx.Maximum = result.Width - scrollx.ViewportSize;
                scrolly.Maximum = result.Height - scrolly.ViewportSize;
            }

            wasCalledByMeasure = false;
            return result;
        }

        /// <summary>
        /// Measures the child elements and determines the size of the Node. 
        /// </summary>
        protected virtual Size MeasureVisual(Size constraint)
        {
            // Reset the child count
            visualChildrenCount = 0;

            // If the result is less then one pixel, don't bother
            if (constraint.Width < 1) return new Size();

            // Variables
            double maxLineWidth = 0;
            double currentLineHeight = 0;
            Point currentLine = new Point();
            bool isLineClear = true;
            bool forceNewline = false;
            bool childWasSplit = false;

            // Edges
            double leftEdge = (isFirst) ? this.LeftEdge : 0;
            
            // Blocks don't split, always incluse zeroed edge in inline renders
            double rightEdge = (!this.CanShareLine) ? this.RightEdge : 0;

            double startWitdth = leftEdge + rightEdge;

            // Inital Line Position
            currentLine.X = startWitdth;
            currentLine.Y = this.TopEdge;

            // SizeLeft
            double widthLeft = double.PositiveInfinity;

            // Use non auto sizes
            bool hasExplicitWidth = !CssStyle.WebWidth.IsAuto;
            bool hasExplicitHeight = !CssStyle.WebHeight.IsAuto;

            bool thisCanSplit = this.CanShareLine;
            bool fullMeasure = calledByMeasureCheck && this.wasCalledByMeasure;

            // Calculate the real size of the node
            Size realNodeSize = new Size();
            
            if (CssStyle.WebPosition.RealValue == PositionEnum.Static)
            { // Use Standard Sizing

                realNodeSize.Width = hasExplicitWidth ?
                    (CssStyle.WebWidth.RealValue.IsRelative ? 
                    (constraint.Width * CssStyle.WebWidth.RealValue.Value) : 
                    this.TotalWidth) : constraint.Width; //constraint

                realNodeSize.Height = hasExplicitHeight ?
                    (CssStyle.WebHeight.RealValue.IsRelative ? 
                    (constraint.Height * CssStyle.WebHeight.RealValue.Value) : 
                    this.TotalHeight) : constraint.Height; //constraint
            }
            else
            { // Use Relative Sizing
                // Calculate X position
                if (!CssStyle.WebLeft.IsAuto & !CssStyle.WebRight.IsAuto)
                {
                    realNodeSize.Width = viewport.Width //constraint
                        - CssStyle.WebLeft.RealValue.Value
                        - CssStyle.WebRight.RealValue.Value;
                }
                else
                { // Use Standard Sizing
                    realNodeSize.Width = hasExplicitWidth ?
                        (CssStyle.WebWidth.RealValue.IsRelative ?
                        (constraint.Width * CssStyle.WebWidth.RealValue.Value) :
                        this.TotalWidth) : constraint.Width; //constraint
                }

                // Calculate Y position
                if (CssStyle.WebTop.IsAuto && !CssStyle.WebBottom.IsAuto)
                {
                    realNodeSize.Height = viewport.Height //constraint
                        - CssStyle.WebTop.RealValue.Value
                        - CssStyle.WebBottom.RealValue.Value;
                }
                else
                { // Use Standard Sizing
                    realNodeSize.Height = hasExplicitHeight ?
                        (CssStyle.WebHeight.RealValue.IsRelative ?
                        (constraint.Height * CssStyle.WebHeight.RealValue.Value) :
                        this.TotalHeight) : constraint.Height; //constraint
                }
            }

            double minWidth = this.GetMinWidth();
            realNodeSize.Width = Math.Max(realNodeSize.Width, minWidth);

            // Current loop child
            VisualNode child = null;
            bool isFirstChild = true;

            // Enumerate and measure all child nodes
            // Loops ends either at a split or the processing of all children
            int preLength = this.logicalChildCount + this.logicalBeginIndex; // Scroll
            for (int cIndex = this.logicalBeginIndex; cIndex < (fullMeasure ? this.splitManager.Count : preLength); cIndex++)
            {
                // Get Child Reference
                child = splitManager[cIndex];

                // A new line is being forced
                if (forceNewline ||
                    // A double line break is set on the child (and line is clear)
                    (child.CssStyle.WebLineBreak.RealValue == LineBreakEnum.Double ||
                    // Child is split inside a block and is not on a new line
                    (child.splitMaker != null && !this.CanShareLine)) &&
                    !isLineClear)
                {

                    // Reset the line position and increment the line height
                    currentLine.X = startWitdth;
                    currentLine.Y += currentLineHeight;
                    currentLineHeight = 0;

                    // Update status
                    forceNewline = false;
                    isLineClear = true;

                    //splitOnCurrentLine = false;
                }
                else if (this.CanShareLine && child.splitMaker != null) // this.CanShareLine
                {
                    childWasSplit = true;
                }

                // CALCULATE THE SIZE LEFT
                widthLeft = realNodeSize.Width - currentLine.X;

                // Check if the child is inline
                if (child.CanShareLine) // ::::::::Child is INLINE::::::::
                {
                    double widthCons = Math.Max(0, (childWasSplit) ? (realNodeSize.Width - startWitdth) : widthLeft);

                    //
                    //// [Problem Fixer : First element 'broken' on new line] 

                    bool lastChildIsSplit = !isFirstChild && // If there is an element before
                        splitManager[cIndex - 1].splitMaker != null && // and that node is a split
                        !isLineClear && // and the line is not clear
                        !this.CanShareLine; // and this is a block element

                    if (lastChildIsSplit && !child.CanSectionFit(widthCons))
                    {
                        // Reset the line position and increment the line height
                        currentLine.X = startWitdth;
                        currentLine.Y += currentLineHeight;
                        currentLineHeight = 0;

                        // Udate Status
                        isLineClear = true;

                        //splitOnCurrentLine = false;

                        widthLeft = realNodeSize.Width - currentLine.X;
                        widthCons = Math.Max(0, (childWasSplit) ? (realNodeSize.Width - startWitdth) : widthLeft);
                    }

                    //// [/Problem Fixer]

                    // Measure Child
                    child.MeasureNoClip(new Size(widthCons, realNodeSize.Height), windowSize, fullMeasure);

                    //canAdd && 
                    // If the child fits, just add it to the current line.
                    // If it's a new line and the child doesn't fit at all then force fit
                    if (!(!isFirstChild && !isLineClear && widthLeft < child.RealDesiredSize.Width) || !fullMeasure) // Scroll
                    //
                    {

                        // Add the child to this node
                        if (fullMeasure) this.AddLocalChild(child);

                        //Normal Add, Increment sizes with regards to added child
                        currentLine.X += child.RealDesiredSize.Width;
                        if (currentLineHeight < child.RealDesiredSize.Height)
                            currentLineHeight = child.RealDesiredSize.Height;

                        // Update Status
                        isLineClear = false;

                        // Handle Line Breaks
                        if (child.CssStyle.WebLineBreak.RealValue != LineBreakEnum.None)
                        {
                            // Force a new line
                            forceNewline = true;
                        }
                    }
                    // If the child doesn't fit at all then split
                    else
                    {
                        ///////// Debug - Sanity Check ////////
                        if ((splitManager.SectionCount - this.logicalBeginIndex) > visualChildrenCount)
                        {
                            throw new Exception("There are more splits than child elements for " + this.ToString());
                            
                            //forceNewline = true;
                            //continue;
                        }

                        // If this Node Can be split
                        if (this.CanShareLine)
                        {
                            break; // A split is needed, split logic is below
                        }
                        else
                        {
                            // Just force the child onto a new line
                            forceNewline = true;
                            cIndex--;
                            continue;
                        }
                    }

                }
                else // ::::::::Child is Block::::::::
                {
                    // If the line is not clear
                    // clear the line as blocks do not share
                    if (!isLineClear)
                    {
                        // Reset the line position and increment the line height
                        currentLine.X = startWitdth;
                        currentLine.Y += currentLineHeight;
                        currentLineHeight = 0;

                        childWasSplit = false;

                        // Determine how much is left : refresh
                        widthLeft = realNodeSize.Width - currentLine.X;
                    }

                    child.MeasureNoClip(new Size(widthLeft, realNodeSize.Height), windowSize, wasCalledByMeasure);

                    // Force Expantion
                    // <This is the actual measure action> \\
                    currentLine.X += child.RealDesiredSize.Width;
                    currentLineHeight = child.RealDesiredSize.Height;

                    isLineClear = false;
                    forceNewline = true;
                }

                // Add visual
                visualChildrenCount++;
                isFirstChild = false;

                // Store max width
                if (maxLineWidth < currentLine.X)
                    maxLineWidth = currentLine.X;

            } // End of child loop

            // Store max width
            if (maxLineWidth < currentLine.X)
                maxLineWidth = currentLine.X;

            // Record the final line height
            currentLine.Y += (currentLineHeight + this.BottomEdge);

            // Move the children to their new WPF parent
            if (fullMeasure) // Scroll
            {

                VisualNode newSplit = null;
                int splitBegin = this.logicalBeginIndex + this.visualChildrenCount; 
                for (int cIndex = splitBegin; cIndex < splitManager.Count; cIndex++)
                {
                    child = splitManager[cIndex];

                    // First loop iteration, 
                    if (cIndex == splitBegin)
                        newSplit = this.CreateSplit(splitBegin);

                    // Add to the newly created split
                    newSplit.AddLocalChild(child);  //<---------- This line is needed for WPF coz it's multi threaded
                }

                if (newSplit == null)
                    this.isLast = true;
                else
                    this.isLast = false;
            }

            // Record the number of children currently being rendered
            lastVisualChildrenCount = visualChildrenCount;

            ////////////////////////
            // Return actual size //
            Size resultSize = new Size();

            if (CanShareLine)
            {
                if (hasExplicitWidth) resultSize.Width = realNodeSize.Width;
                else resultSize.Width = (isLast ? (maxLineWidth + this.RightEdge) : maxLineWidth);
            }
            else
            {
                resultSize.Width = realNodeSize.Width;
            }

            // Even blocks get height from children
            resultSize.Height = hasExplicitHeight ? realNodeSize.Height : currentLine.Y;

            // Scroll Logic
            // changes avalible size to constraint
            VerifyScrollData(constraint, resultSize);

            RealDesiredSize = resultSize;
            return resultSize;
        }

        /// <summary>
        /// Determines the final rendering position of the Node
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (visualChildrenCount == 0) return finalSize;

            // Variables
            double maxLineWidth = 0;
            double currentLineHeight = 0;

            bool isLineClear = true;
            bool forceNewline = false;

            Point currentLine = new Point();
            //TranslateTransform trans = null;

            double leftEdge = (isFirst) ? this.LeftEdge : 0;
            double rightEdge = (isLast) ? this.RightEdge : 0;

            // Inital Line Position
            currentLine.X = leftEdge;
            currentLine.Y = this.TopEdge;

            // SizeLeft
            double widthLeft = double.PositiveInfinity;

            OnArrangeStart(currentLine.Y, finalSize);

            VisualNode child = null;
            for (int cIndex = 0; cIndex < logicalChildCount; cIndex++)
            {
                // Select Next
                child = splitManager.GetChild(this, cIndex);
                //if (child == null) break;

                // LineBreaks
                if (forceNewline ||
                    child.CssStyle.WebLineBreak.RealValue == LineBreakEnum.Double && !isLineClear)
                {
                    currentLine.X = leftEdge;
                    currentLine.Y += currentLineHeight;
                    currentLineHeight = 0;

                    widthLeft = finalSize.Width - currentLine.X - rightEdge;

                    OnArrangeNewLine(currentLine.Y);

                    forceNewline = false;
                    isLineClear = true;
                }

                if (child.CanShareLine) // ::::::::INLINE::::::::
                {

                    // Line width left
                    widthLeft = finalSize.Width - currentLine.X;

                    if (!isLineClear && (widthLeft < child.RealDesiredSize.Width))
                    {
                        currentLine.X = leftEdge;
                        currentLine.Y += currentLineHeight;
                        currentLineHeight = 0;

                        widthLeft = finalSize.Width - currentLine.X - rightEdge;

                        OnArrangeNewLine(currentLine.Y);
                    }

                    isLineClear = false;

                    if (!isLineClear && child.CssStyle.WebLineBreak.RealValue != LineBreakEnum.None)
                    {
                        currentLine.X = leftEdge;
                        currentLine.Y += currentLineHeight;
                        currentLineHeight = 0;

                        widthLeft = finalSize.Width - currentLine.X - rightEdge;

                        OnArrangeNewLine(currentLine.Y);

                        isLineClear = true;
                    }
                }
                else // ::::::::Block::::::::
                {
                    // If the line is not clear
                    // clear the line as blocks do not share
                    if (!isLineClear)
                    {
                        currentLine.X = leftEdge;
                        currentLine.Y += currentLineHeight;
                        currentLineHeight = 0;

                        OnArrangeNewLine(currentLine.Y);
                    }

                    isLineClear = false;
                    forceNewline = true;
                }

                if (currentLineHeight < child.RealDesiredSize.Height)
                    currentLineHeight = child.RealDesiredSize.Height;

                // Store max width
                if (maxLineWidth < currentLine.X)
                    maxLineWidth = currentLine.X;


                ArrageChild(child, currentLine, currentLineHeight, finalSize);

                // Force Expantion
                currentLine.X += child.RealDesiredSize.Width;

            } // End of child loop


            // Record the final line height
            currentLine.Y += currentLineHeight;

            // Scroll Logic
            //VerifyScrollData(finalSize, new Size(maxLineWidth, currentLine.Y));

            OnArrangeDone();

            if (!CanShareLine && 
                CssStyle.WebOverflow.RealValue == OverflowEnum.Scroll && 
                scrollx != null)
            {
                scrollx.Arrange(new Rect(new Point(0, scrolly.DesiredSize.Height - scrolly.DesiredSize.Width), scrollx.DesiredSize));
                scrolly.Arrange(new Rect(new Point(scrollx.DesiredSize.Width, 0), scrolly.DesiredSize)); // - scrollx.DesiredSize.Height
            } 

            //base.ClipToBounds = false;
            //base.Clip = new RectangleGeometry(new Rect(finalSize));
            return finalSize;
        }

        /// <summary>
        /// Allows extra functionality to be added at the arrangement start
        /// </summary>
        public virtual void OnArrangeStart(double currentY, Size finalSize)
        {
        }


        /// <summary>
        /// Allows extra functionality to be added when a new line is created
        /// </summary>
        protected virtual void OnArrangeNewLine(double currentY)
        { 
        }

        /// <summary>
        /// Allows the arrangement logic of a child to be redefined
        /// </summary>
        protected virtual void ArrageChild(VisualNode child, Point currentLine, double currentLineHeight, Size finalSize)
        {
            // Calculate Position
            Point relativePosition = new Point();

            if (child.CssStyle.WebPosition.RealValue == PositionEnum.Static)
            {
                relativePosition.X = currentLine.X;
                //(Because if one element is taller than the other they all sit on the bottom)
                relativePosition.Y = currentLine.Y + (currentLineHeight - child.RealDesiredSize.Height);
            }
            else
            {
                // Calculate X position
                if (child.CssStyle.WebLeft.IsAuto)
                    if (child.CssStyle.WebRight.IsAuto)
                        relativePosition.X = currentLine.X;
                    else
                        relativePosition.X = finalSize.Width - child.CssStyle.WebRight.RealValue.Value;
                else
                    relativePosition.X = child.CssStyle.WebLeft.RealValue.Value;

                // Calculate Y position
                if (child.CssStyle.WebTop.IsAuto)
                    if (child.CssStyle.WebBottom.IsAuto)
                        relativePosition.Y = currentLine.Y + (currentLineHeight - child.RealDesiredSize.Height);
                    else
                        relativePosition.Y = finalSize.Height - child.CssStyle.WebBottom.RealValue.Value;
                else
                    relativePosition.Y = child.CssStyle.WebTop.RealValue.Value;
            }


            // Arrange 
            Point newPoint = new Point
                (
                relativePosition.X - HorizontalOffset, // Scroll Logic
                relativePosition.Y - VerticalOffset // Scroll Logic
                );

            child.Arrange(new Rect(newPoint, child.RealDesiredSize));

        }

        /// <summary>
        /// Allows extra functionality to be added at the arrangement end
        /// </summary>
        public virtual void OnArrangeDone()
        { 
        }

        /// <summary>
        /// Refresh properties on size change
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.ClipToBounds = CssStyle.WebOverflow.RealValue == OverflowEnum.Hidden;

            base.OnRenderSizeChanged(sizeInfo);
        }

        /// <summary>
        /// Performs the actual rendering of the visual element
        /// </summary>
        /// <param name="drawingContext">WPF visual content discriptor</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            CssStyle style = splitManager.CssStyle;

            // Color Brushes
            SolidColorBrush bColorTop, bColorBottom, bColorLeft, bColorRight;
            bColorTop = style.WebBorderTopColor.RealValue;
            bColorBottom = style.WebBorderBottomColor.RealValue;
            bColorLeft = style.WebBorderLeftColor.RealValue;
            bColorRight = style.WebBorderRightColor.RealValue;

            // Border Thickness
            double bWidthLeft, bWidthRight, bWidthTop, bWidthBottom, 
                bWidthLeftOriginal, bWidthRightOriginal;

            bWidthRightOriginal = style.WebBorderRightWidth.RealValue;
            bWidthLeftOriginal = style.WebBorderLeftWidth.RealValue;

            bWidthLeft = isFirst? bWidthLeftOriginal : 0;
            bWidthRight = isLast? bWidthRightOriginal : 0;
            bWidthTop = style.WebBorderTopWidth.RealValue;
            bWidthBottom = style.WebBorderBottomWidth.RealValue;

            // Border Styles
            WebEnum<BorderStyleEnum> bStyleTop, bStyleBottom, bStyleLeft, bStyleRight;
            bStyleTop = style.WebBorderTopStyle;
            bStyleBottom = style.WebBorderBottomStyle;
            bStyleLeft = style.WebBorderLeftStyle;
            bStyleRight = style.WebBorderRightStyle;
            // Top Pen
            Pen pen = new Pen();
            pen.Brush = bColorTop;
            pen.Thickness = bWidthTop;
            pen.EndLineCap = PenLineCap.Square;
            if (pen.CanFreeze) pen.Freeze();

            // Border: Calculate X and Y Positions
            double bLeftPos = this.PaddingLeft + (bWidthLeftOriginal * .5);
            double bTopPos = this.PaddingTop + (bWidthTop * .5);
            double bRightPos = this.RenderSize.Width - this.PaddingRight - (bWidthRightOriginal * .5);
            double bBottomPos = this.RenderSize.Height - this.PaddingBottom - (bWidthBottom * .5);

            // Contents: Calculate X and Y Positions
            double cLeftPos = this.PaddingLeft;
            double cTopPos = this.PaddingTop;
            double cRightPos = this.RenderSize.Width - this.PaddingRight;
            double cBottomPos = this.RenderSize.Height - this.PaddingBottom;

            //// Construct the four corner points
            Point topLeft = new Point(bLeftPos, bTopPos);
            Point bottomRight = new Point(bRightPos, bBottomPos);
            Point topRight = new Point(bRightPos, bTopPos);
            Point bottomLeft = new Point(bLeftPos, bBottomPos);

            Rect contentRect = new Rect(new Point(cLeftPos, cTopPos), new Point(cRightPos, cBottomPos));

            // Back Color or Back Image
            if (!style.WebBackgroundImage.IsNone)
            {
                // Draw Background Image
                //drawingContext.DrawImage(  , new Rect(topLeft, bottomRight));
            }
            else if (!style.WebBackgroundColor.IsTransparent)
            {
                // Draw Color
                drawingContext.DrawRectangle(style.WebBackgroundColor.RealValue, null, contentRect);
            }
            else
            {
                // Draw Transparent
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.Transparent), null, contentRect);
            }

            // Borders
            bool renderSingleEdges = true;
            if ((bWidthTop > 0d) && (bStyleTop.RealValue != BorderStyleEnum.None))
            {
                // If all the borders are the same, just draw a rectangle
                if (MultiCompare.Compare(
                        bColorLeft.Color.GetHashCode(), 
                        bColorRight.Color.GetHashCode(), 
                        bColorTop.Color.GetHashCode(), 
                        bColorBottom.Color.GetHashCode()) &&
                    MultiCompare.Compare(
                        bWidthLeft, 
                        bWidthRight, 
                        bWidthTop, 
                        bWidthBottom) &&
                    MultiCompare.Compare(
                        (int)bStyleTop.RealValue, 
                        (int)bStyleBottom.RealValue, 
                        (int)bStyleLeft.RealValue, 
                        (int)bStyleRight.RealValue))
                {
                    Rect borderRect = new Rect(topLeft, bottomRight);
                    drawingContext.DrawRectangle(null, pen, borderRect);

                    renderSingleEdges = false;
                }
                else
                {
                    // Top Render
                    drawingContext.DrawLine(pen, topLeft, topRight);
                }
            }
            if (renderSingleEdges)
            {
                // Bottom
                if ((bStyleBottom.RealValue != BorderStyleEnum.None) && (bWidthBottom > 0d))
                {
                    pen = new Pen();
                    pen.Brush = bColorBottom;
                    pen.Thickness = bWidthBottom;
                    pen.EndLineCap = PenLineCap.Square;
                    if (pen.CanFreeze) pen.Freeze();

                    drawingContext.DrawLine(pen, bottomLeft, bottomRight);
                }
                // Left
                if ((bStyleLeft.RealValue != BorderStyleEnum.None) && (bWidthLeft > 0d))
                {
                    pen = new Pen();
                    pen.Brush = bColorLeft;
                    pen.Thickness = bWidthLeft;
                    pen.EndLineCap = PenLineCap.Square;
                    if (pen.CanFreeze) pen.Freeze();

                    drawingContext.DrawLine(pen, topLeft, bottomLeft);
                }
                // Right
                if ((bStyleRight.RealValue != BorderStyleEnum.None) && (bWidthRight > 0d))
                {
                    pen = new Pen();
                    pen.Brush = bColorRight;
                    pen.Thickness = bWidthRight;
                    pen.EndLineCap = PenLineCap.Square;
                    if (pen.CanFreeze) pen.Freeze();

                    drawingContext.DrawLine(pen, topRight, bottomRight);
                }
            }
        }

        /// <summary>
        /// Gets the element name
        /// </summary>
        public override string ToString()
        {
            string tag = (this.Element != null)? this.Element.TagName : "";
            return  tag + " (" + logicalChildCount + ") {Start = " + logicalBeginIndex + ", ID = " + DebugID + ", Split = " + (splitMaker != null).ToString() + " ]";
        }

        /// <summary>
        /// Mouse enter for cursor override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            Mouse.OverrideCursor = CascadingStyleSheets.Helper.CursorConvert(CssStyle.WebCursor.RealValue);
        }

        /// <summary>
        /// Mouse leave for cursor override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            //if (e.MouseDevice.DirectlyOver == this)
            //{
            if (splitManager.Parent != null)
            {
                Mouse.OverrideCursor = CascadingStyleSheets.Helper.CursorConvert(splitManager.Parent.CssStyle.WebCursor.RealValue);
            }
            //}
        }

        /// <summary>
        /// Finds a node where node.Name equals the name
        /// </summary>
        public VisualNode FindNode(string name)
        {
            if (this.Name == name)
                return this;

            for (int cIndex = 0; cIndex < Count; cIndex++)
            {
                VisualNode result = this[cIndex].FindNode(name);
                if (result != null) 
                    return result;
            }
            return null;
        }

        // IScroll info interface implementation //

        private const double scrollLineSize = 16;
        private const double wheelSize = 3 * scrollLineSize;

        private bool canHorizontallyScroll = true;
        private bool canVerticallyScroll = true;
        private ScrollViewer scrollOwner;
        private Vector scrollOffset;
        private Size extent;
        private Size viewport;


        public void LineDown()
        { SetVerticalOffset(VerticalOffset + scrollLineSize); }

        public void LineUp()
        { SetVerticalOffset(VerticalOffset - scrollLineSize); }

        public void LineLeft()
        { SetHorizontalOffset(HorizontalOffset - scrollLineSize); }

        public void LineRight()
        { SetHorizontalOffset(HorizontalOffset + scrollLineSize); }

        public void MouseWheelDown()
        { SetVerticalOffset(VerticalOffset + wheelSize); }

        public void MouseWheelUp()
        { SetVerticalOffset(VerticalOffset - wheelSize); }

        public void MouseWheelLeft()
        { SetHorizontalOffset(HorizontalOffset - wheelSize); }

        public void MouseWheelRight()
        { SetHorizontalOffset(HorizontalOffset + wheelSize); }

        public void PageDown()
        { SetVerticalOffset(VerticalOffset + ViewportHeight); }

        public void PageUp()
        { SetVerticalOffset(VerticalOffset - ViewportHeight); }

        public void PageLeft()
        { SetHorizontalOffset(HorizontalOffset - ViewportWidth); }

        public void PageRight()
        { SetHorizontalOffset(HorizontalOffset + ViewportWidth); }

        public ScrollViewer ScrollOwner
        {
            get { return scrollOwner; }
            set { scrollOwner = value; }
        }

        public bool CanHorizontallyScroll
        {
            get { return canHorizontallyScroll; }
            set { canHorizontallyScroll = value; }
        }

        public bool CanVerticallyScroll
        {
            get { return canVerticallyScroll; }
            set { canVerticallyScroll = value; }
        }

        public double ExtentHeight
        { 
            get
            { 
                return extent.Height;
            }
        }

        public double ExtentWidth
        { 
            get 
            { 
                return extent.Width;
            } 
        }

        public double HorizontalOffset
        { get { return scrollOffset.X; } }

        public double VerticalOffset
        { get { return scrollOffset.Y; } }

        public double ViewportHeight
        {
            get
            {
                return viewport.Height;
            }
        }

        public double ViewportWidth
        { 
            get 
            {
                return viewport.Width; 
            } 
        }

        protected void VerifyScrollData(Size viewport, Size extent)
        {
            if (double.IsInfinity(viewport.Width))
            { viewport.Width = extent.Width; }

            if (double.IsInfinity(viewport.Height))
            { viewport.Height = extent.Height; }

            this.extent = extent;
            this.viewport = viewport;

            scrollOffset.X = Math.Max(0,
              Math.Min(scrollOffset.X, ExtentWidth - ViewportWidth));
            scrollOffset.Y = Math.Max(0,
              Math.Min(scrollOffset.Y, ExtentHeight - ViewportHeight));

            if (ScrollOwner != null)
            { ScrollOwner.InvalidateScrollInfo(); }
        }

        public void SetHorizontalOffset(double offset)
        {
            offset = Math.Max(0, Math.Min(offset, ExtentWidth - ViewportWidth));
            if (offset != scrollOffset.Y)
            {
                scrollOffset.X = offset;
                InvalidateArrange();
            }
        }

        public void SetVerticalOffset(double offset)
        {
            offset = Math.Max(0, Math.Min(offset, ExtentHeight - ViewportHeight));
            if (offset != scrollOffset.Y)
            {
                scrollOffset.Y = offset;
                InvalidateArrange();
            }
        }


        #region IScrollInfo Members


        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            for (int i = 0; i < this.Count; i++)
            {

                if ((Visual)this[i] == visual)
                {

                    // we found the visual! Let's scroll it into view. First we need to know how big
                    // each child is.
                    Size finalSize = this.RenderSize;

                    Size childSize = new Size(
                        finalSize.Width,
                        (finalSize.Height * 2) / this.Count);

                    // now we can calculate the vertical offset that we need and set it
                    SetVerticalOffset(childSize.Height * i);

                    // child size is always smaller than viewport, because that is what makes the Panel
                    // an AnnoyingPanel.
                    return rectangle;
                }
            }
            throw new ArgumentException("Given visual is not in this Panel");
        }

        #endregion
    }

}
