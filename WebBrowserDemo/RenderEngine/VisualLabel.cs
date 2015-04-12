using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using CascadingStyleSheets;
using System.Diagnostics;
using DOMCore;

namespace RenderEngine
{

    public class VisualLabel : VisualNode
    {

        public VisualLabel(VisualNode parentNode)
            : base(parentNode)
        {

            CssStyle.WebCursor = new WebCursor() { ForText = true, RealValue = CursorEnum.Arrow };
        }

        private IHTMLLabelElement LableElement
        {
            get { return splitManager.Element as IHTMLLabelElement; }
        }

        public string For
        {
            get
            {
                return LableElement.HtmlFor;
            }
            set
            {
                LableElement.HtmlFor = value;
            }
        }

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            VisualNode node = visualRoot.FindNode(For);

            if (node is VisualInput)
            {
                VisualInput inputNode = (node as VisualInput);
                if(inputNode.Type == VisualInput.InputType.button || 
                    inputNode.Type == VisualInput.InputType.text)
                {
                    inputNode.Focus();
                }
            }
        }

    }
}
