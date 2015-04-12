using System;
using System.Collections.Generic;
using System.Windows;
using RenderEngine;
using System.Windows.Media;
using DOMCore;
using System.Windows.Controls;
using PhoenixOI;
using System.IO;
using System.Text;
using CascadingStyleSheets;
using System.Net;
using System.Threading;
using System.Windows.Threading;

namespace PhoenixEngine
{
    public class BrowserControl : FrameworkElement, IPageHost //ContentControl
    {
        // Private Variables

        private VisualNode visualRoot; // This visual
        private RenderEngine.Transformers.TransformManager transformer; // DOM to visual Model Transformer
        private string status = "Ready"; // Page status
        private string source; // Page Source Code
        private Uri uri; // Internal address in uri formatt
        private string internalAddress = "about:blank"; // Internal address

        private const string KHome = "khaos:home"; // KHome address
        private const string Blank = "about:blank"; // Blank Page

        protected PhoenixOI.Request request;
        protected IHTMLDocument innerDocument;

        // History
        private List<string> controlHistory = new List<string>();
        private int listLocale = 0;
        private bool goingBackOrForward = false;


        // Events

        public event EventHandler Navigated;
        public event EventHandler DocumentChanged;
        public event EventHandler TitleChanged;
        public event EventHandler StatusChanged;

        // Properties

        public string DocumentTitle
        {
            get
            {
                if (innerDocument != null)
                {
                    return innerDocument.Title;
                }
                else
                {
                    return "Khaos";
                }
            }
        }

        public string Status
        {
            get { return status; }
        }

        public string Uri
        {
            get
            {
                if (uri != null)
                    return uri.ToString();
                else
                    return internalAddress;
            }
        }

        // Constructor

        public BrowserControl()
        {
            innerDocument = null;

            //scrollView = new ScrollViewer();

            request = new PhoenixOI.Request();
            transformer = new RenderEngine.Transformers.TransformManager();

            //this.AddLogicalChild(scrollView);
            //this.AddVisualChild(scrollView);
        }

        public override void BeginInit()
        {
            base.BeginInit();

            ShowHome();
        }


        // Properties

        public IHTMLDocument Document
        {
            get
            {
                return innerDocument;
            }
            set
            {
                this.status = "Loading";
                if (StatusChanged != null)
                    StatusChanged(this, new EventArgs());

                this.innerDocument = value;
                this.OnDocumentChanged(this, new EventArgs());
            }
        }

        // Public Methods

        public virtual void OnDocumentChanged(object sender, EventArgs args)
        {
            if (DocumentChanged != null)
                DocumentChanged(sender, args);

            transformer.HtmlDocument = innerDocument;
            //transformer.Transform(request, this, this.Dispatcher); //this

            // Threading Sync
            if (!this.CheckAccess())
            {
                // Invoke the transformer on the main thread
                this.Dispatcher.Invoke( (Action)delegate
                {
                    transformer.Transform(request, this, this.Dispatcher);
                    this.Content = transformer.VisDocument;
                });
            }
            else
            {
                // Already on the main thread
                transformer.Transform(request, this, this.Dispatcher);
                this.Content = transformer.VisDocument;
            }

            if (Navigated != null)
                Navigated(sender, args);

            if (TitleChanged != null)
                TitleChanged(sender, args);

            this.status = "Ready";

            if (StatusChanged != null)
                StatusChanged(sender, args);
        }

        /// <summary>
        /// Navigate to the given address
        /// </summary>
        public void Navigate(string navigateLocation)
        {
            navigateLocation = navigateLocation.ToLower();

            if (navigateLocation == KHome)
            {
                ShowHome();
                return;
            }
            else if (navigateLocation == Blank)
            {
                ShowBlank();
                return;
            }

            if (!goingBackOrForward)
            {
                controlHistory.Add(navigateLocation);
                listLocale++;
            }

            if (navigateLocation != null && navigateLocation != "")
            {
                this.status = "Loading";
                if (StatusChanged != null)
                    StatusChanged(this, new EventArgs());

                // Start a new thread to get the html and parse it
                ThreadPool.QueueUserWorkItem((obj) =>
                    {
                        request.CreateWebRequest(navigateLocation);

                        // Parse Html
                        DOMCore.HtmlParser htmlParser = new HtmlParser();

                        request.GetResponse();
                        WebResponse responceObject = request.Response;


                        if (responceObject != null)
                        {
                            uri = responceObject.ResponseUri;

                            StreamReader sRead = new StreamReader(responceObject.GetResponseStream());

                            var parserOut = htmlParser.ParseHTML(sRead);
                            this.source = htmlParser.Source;

                            // Generate DOM
                            DOMCore.DOMParser domParser = new DOMParser(parserOut, request);
                            this.Document = domParser.ParseDom();
                        }
                        else
                        {
                            this.Dispatcher.Invoke((Action)delegate { ShowError("Could not find page"); } );
                        }
                    });
            }
        }

        // Methods

        /// <summary>
        /// Set the visual node the represents a page
        /// </summary>
        private VisualNode Content
        {
            set
            {
                // Remove the od page
                if (visualRoot != null)
                {
                    this.RemoveLogicalChild(visualRoot);
                    this.RemoveVisualChild(visualRoot);
                }

                // Add the new page
                this.visualRoot = value;
                this.AddLogicalChild(visualRoot);
                this.AddVisualChild(visualRoot);

                // Refresh page layout
                this.InvalidateMeasure();
                this.UpdateLayout();

            }
        }

        /// <summary>
        /// Gets the number of children
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                // Return the number of children
                return (visualRoot == null) ? 0 : 1; ;
            }
        }

        /// <summary>
        /// Gets the actual visual element
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            if (index == 0)
                return visualRoot;
            else
                throw new IndexOutOfRangeException("BrowserControl asked for invalid Child");
        }

        /// <summary>
        /// Measure content
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            // If there is a page measure it
            if (visualRoot != null)
            {
                visualRoot.Measure(availableSize);
                return visualRoot.DesiredSize;
            }
            else
            {
                return base.MeasureOverride(availableSize);
            }
        }

        /// <summary>
        /// Arrange Conent
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            // If there is a page arrange it
            if (visualRoot != null)
            {
                visualRoot.Arrange(new Rect(finalSize));
                return visualRoot.RenderSize;
            }
            else
            {
                return base.ArrangeOverride(finalSize);
            }
        }

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                yield return visualRoot;
            }
        }

        /// <summary>
        /// Dislay the tools window
        /// </summary>
        public void ShowTools()
        {
            DeveloperTools tools = new DeveloperTools(source, Document, visualRoot as VisualDocument);
            tools.Show();
        }

        /// <summary>
        /// Show the default homepage
        /// </summary>
        public void ShowHome()
        {
            if (!goingBackOrForward)
            {
                controlHistory.Add(KHome);
                listLocale++;
            }

            internalAddress = KHome;
            this.source = "Home has no source";
            this.innerDocument = null;

            VisualDocument vdm = VisualDocument.CreateVisualRoot(this, null); //this
            vdm.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;
            vdm.SetDefaultStyle();

            VisualCenter center = new VisualCenter(vdm);
            center.ItemName = "center";
            center.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;

            VisualBreak br = new VisualBreak(center);
            br.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;
            br.CssStyle.WebLineBreak.RealValue = LineBreakEnum.Normal;

            VisualText heading = new VisualText(center);
            heading.CssStyle.WebFontSize = new Unit(20, UnitType.Pixel);
            heading.Text = "Khaos";

            VisualImage image = new VisualImage(center);
            image.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;
            image.OverrideImage(PhoenixEngine.Properties.Resources.logo);

            this.Content = vdm;
        }

        /// <summary>
        /// Show a blank page
        /// </summary>
        public void ShowBlank()
        {
            if (!goingBackOrForward)
            {
                controlHistory.Add(Blank);
                listLocale++;
            }

            internalAddress = Blank;
            this.source = "Blank has no source";
            this.innerDocument = null;

            VisualDocument vdm = VisualDocument.CreateVisualRoot(this, null); //this

            this.Content = vdm;
        }

        /// <summary>
        /// Show a blank page with a message
        /// </summary>
        public void ShowError(string message)
        {
            if (!goingBackOrForward)
            {
                controlHistory.Add(KHome);
                listLocale++;
            }

            internalAddress = KHome;
            this.source = "Page has no source";
            this.innerDocument = null;

            VisualDocument vdm = VisualDocument.CreateVisualRoot(this, null); //this
            vdm.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;
            vdm.SetDefaultStyle();

            VisualCenter center = new VisualCenter(vdm);
            center.ItemName = "center";
            center.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;

            VisualBreak br = new VisualBreak(center);
            br.CssStyle.WebDisplay.RealValue = DisplayEnum.Block;
            br.CssStyle.WebLineBreak.RealValue = LineBreakEnum.Normal;

            VisualText heading = new VisualText(center);
            heading.CssStyle.WebFontSize = new Unit(20, UnitType.Pixel);
            heading.Text = message;

            this.Content = vdm;
        }

        /// <summary>
        /// Navigate Back
        /// </summary>
        public void NavigateBack()
        {
            if (listLocale < 2)
            {
                return;
            }
            else
            {
                listLocale = listLocale - 2;
                goingBackOrForward = true;
                Navigate(controlHistory[listLocale]);
                goingBackOrForward = false;
            }
        }

        /// <summary>
        /// Navigate Forward
        /// </summary>
        public void NavigateForward()
        {
            if (listLocale >= controlHistory.Count)
            {
                return;
            }
            else
            {
                listLocale++;
                goingBackOrForward = true;
                Navigate(controlHistory[listLocale]);
                goingBackOrForward = false;
            }
        }

        /// <summary>
        /// Clear History
        /// </summary>
        public void ClearForwardControlHistory()
        {
            int i = listLocale;
            while (i < controlHistory.Count)
            {
                controlHistory.RemoveAt(i);
            }
        }

        #region IPageHost Members

        // New Window, Tab

        public event NewPage NewWindow;
        public event NewPage NewTab;

        /// <summary>
        /// Navigate interface for links
        /// </summary>
        public void Navigate(string uri, TargetEnum target)
        {
            switch (target)
            {
                case TargetEnum._tab:
                    if (NewTab != null) NewTab(this, target);
                    break;
                case TargetEnum._blank:
                    if (NewWindow != null) NewWindow(this, target);
                    break;
                case TargetEnum._self:
                    Navigate(uri);
                    break;
                default:
                    Navigate(uri);
                    break;
            }
        }

        #endregion


    }

    /// <summary>
    /// New page method pointer
    /// </summary>
    public delegate void NewPage(object source, TargetEnum target);
}
