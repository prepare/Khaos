using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using CascadingStyleSheets;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace RenderEngine
{

    public class VisualImage : VisualNode
    {

        protected static ImageSource linkBroken;
        private ImageSource finalImage = null;
        private WebImageLoader image;

        // Properties

        private bool HasElement
        {
            get { return Element is DOMCore.IHTMLImageElement; }
        }

        private DOMCore.IHTMLImageElement ImageElement
        {
            get { return Element as DOMCore.IHTMLImageElement; }
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
                    this.Src = ImageElement.Src;
            }
        }

        public string Src
        {
            get 
            { 
                return image.StyleValue; 
            }
            set 
            { 
                image.StyleValue = value;
                //if (HasElement)
                //    ImageElement.Src = value;
            }
        }

        public WebImageLoader Image
        {
            get
            {
                return image;
            }
        }

        // Constructors

        static VisualImage()
        {
            linkBroken = ConvertGDI_To_WPF(RenderEngine.Properties.Resources.LinkBroken);
        }

        public VisualImage(VisualNode parentNode)
            : base(parentNode)
        {
            image = new WebImageLoader(this.request);
            image.ImageChanged += new EventHandler(image_Changed);
            finalImage = linkBroken;

        }

        public void OverrideImage(System.Drawing.Bitmap image)
        {

            finalImage = ConvertGDI_To_WPF(image);
            this.InvalidateVisual();
        }

        public void OverrideImage(ImageSource image)
        {
            finalImage = image;
            this.InvalidateVisual();
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 0;
            }
        }

        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
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
                yield return null;
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

            Size resultSize = new Size(0, 0);

            resultSize.Width = this.widthAuto ? finalImage.Width : this.TotalWidth;
            resultSize.Height = this.widthAuto ? finalImage.Height : this.TotalHeight;

            this.RealDesiredSize = resultSize;
            return resultSize;
        }

        void image_Changed(object sender, EventArgs e)
        {
            //this.ParentLayoutInvalidated(this);

            finalImage = image.Image;

            VisualNode currentNode = this;
            while (currentNode != null)
            {
                currentNode.InvalidateMeasure();
                currentNode.InvalidateVisual();
                currentNode = (currentNode.Parent as VisualNode);
            }

            //FrameworkElement host = (visualRoot.Parent as FrameworkElement);
            //if (host != null)
            //{
            //    host.InvalidateMeasure();
            //    host.InvalidateVisual();
            //    host.UpdateLayout();
            //}

            if (visualRoot != null)
            {
                visualRoot.InvalidateMeasure();
                visualRoot.InvalidateVisual();
                visualRoot.UpdateLayout();
            }

        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawImage(finalImage, new Rect(this.RenderSize));
        }

        private static BitmapSource ConvertGDI_To_WPF(System.Drawing.Bitmap bm)
        {
            BitmapSource bms = null;
            if (bm != null)
            {
                IntPtr h_bm = bm.GetHbitmap();
                bms = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(h_bm, IntPtr.Zero, 
                    Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            return bms;
        }

    }
}
