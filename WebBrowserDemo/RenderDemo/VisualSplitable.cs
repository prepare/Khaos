using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

using WinStyle = System.Windows.Style;
using CssStyle = CascadingStyleSheets.Style;

namespace RenderEngine
{
    public class VisualSplitable : FrameworkElement
    {



                // Constructors

        public VisualSplitable()
        { // This is the inital root section
            this.splitManager = new SectionManager(new CssStyle());

            isFirst = true;
            isLast = true;
        }

        public VisualSplitable(VisualSplitable inherit)
        { // Made from external code
            this.splitManager = new SectionManager(inherit.splitManager);


            isFirst = true;
            isLast = true;

            if (parentNode != null)
                parentNode.Add(this);
        }

        /// <summary>
        /// Called to construct a Visual Node created by the splitting of an existing node
        /// </summary>
        protected VisualSplitable(SectionManager container, int beginIndex, VisualNode splitMaker)
        { // Made from split
            this.splitManager = container;
            this.logicalbeginIndex = beginIndex;

            isFirst = false;
            isLast = true;

            this.splitMaker = splitMaker;
            this.Name = "split" + scount++;
        }

    }
}
