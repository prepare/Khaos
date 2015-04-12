using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using CascadingStyleSheets;
using DOMCore;
using System.Diagnostics;

namespace RenderEngine
{
    public class VisualInput : VisualNode
    {
        public enum InputType
        {
            text,
            password,
            checkbox,
            radio,
            submit,
            image,
            reset,
            button,
            hidden,
            file
        }

        private InputType type;
        private Control internalControl;

        public InputType Type
        {
            get { return type; }
            set 
            {
                //if (type != value)
                //{
                type = value;
                UpdateVisualType();
                //}
            }
        }

        public VisualInput(VisualNode parentNode)
            : base(parentNode)
        {
            type = InputType.hidden;
            CssStyle.WebDisplay.RealValue = CascadingStyleSheets.DisplayEnum.Inline;
        }

        protected virtual void UpdateVisualType()
        {
            //internalControl
            if (internalControl != null)
            {
                this.RemoveLogicalChild(internalControl);
                this.RemoveVisualChild(internalControl);
            }

            switch (type)
            {
                case InputType.text:
                    
                    TextBox textBox = new TextBox();
                    textBox.Width = 120;
                    textBox.Text = value;
                    textBox.IsReadOnly = readOnly;

                    if (maxLength > 0)
                        textBox.MaxLength = maxLength;
                    else
                        maxLength = textBox.MaxLength;

                    CssStyle.WebCursor = new WebCursor() { RealValue = CursorEnum.IBeam };
                    internalControl = textBox;
                    break;
                case InputType.password:

                    PasswordBox passBox = new PasswordBox();
                    //passBox.Password

                    if (maxLength > 0)
                        passBox.MaxLength = maxLength;
                    else
                        maxLength = passBox.MaxLength;

                    CssStyle.WebCursor = new WebCursor() { RealValue = CursorEnum.IBeam };
                    internalControl = passBox;
                    break;
                case InputType.submit:

                    Button button = new Button();
                    button.Content = value;

                    CssStyle.WebCursor = new WebCursor() { RealValue = CursorEnum.Arrow };
                    internalControl = button;
                    break;
                default:
                    break;
            }

            if (internalControl != null)
            {
                base.AddLogicalChild(internalControl);
                base.AddVisualChild(internalControl);
            }

        }


        public IHTMLInputElement InputElement
        {
            get { return Element as IHTMLInputElement; }
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

                try
                {
                    if (InputElement.Type == null || InputElement.Type == "")
                        InputElement.Type = "text";

                    this.Type = (InputType)Enum.Parse(typeof(InputType), InputElement.Type.ToLower());
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                    this.Type = InputType.text;
                }
                catch (ArgumentException ex)
                {
                    Debug.WriteLine(ex.Message);
                    this.Type = InputType.text;
                }

                this.Maxlength = InputElement.MaxLength;
                this.ReadOnly = InputElement.ReadOnly;
                this.Disabled = InputElement.Disabled;
                this.Value = InputElement.Value;
            }
        }


        //accept
        //alt
        //checked

        public bool Disabled
        {
            get 
            { 
                if (internalControl != null)
                return !internalControl.IsEnabled;
                return true;
            }
            set 
            {
                if (internalControl != null)
                internalControl.IsEnabled = !value;
            }
        }

        int maxLength = 0;
        public int Maxlength
        {
            get 
            {
                return maxLength;
            }
            set 
            {
                if (maxLength > 0)
                {
                    maxLength = value;

                    if (internalControl is TextBox)
                    {
                        (internalControl as TextBox).MaxLength = maxLength;
                    }
                }
            }
        }

        private bool readOnly = false;
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                if (internalControl is TextBox)
                {
                    (internalControl as TextBox).IsReadOnly = readOnly;
                }
            }
        }

        //size
        //src

        private string value;
        public string Value
        {
            get 
            {
                return value; 
            }
            set 
            {
                this.value = value;
                if (internalControl is TextBox)
                {
                    (internalControl as TextBox).Text = value;
                }
                else if (internalControl is Button)
                {
                    (internalControl as Button).Content = value;
                }
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return (internalControl == null) ? 0 : 1;
            }
        }

        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
            if (index == 0)

                return internalControl;
            
            else
            
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

        protected override double GetMinWidth()
        {
            if (internalControl != null)
                return internalControl.MinWidth;
            else
                return 10;
        }

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                yield return internalControl;
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
            throw new IndexOutOfRangeException("Input visuals don't have sections");
        }

        protected override Size MeasureVisual(Size constraint)
        {
            if (internalControl != null)
            {
                if (!this.widthAuto) internalControl.Width = this.TotalWidth;
                else if (type == InputType.text || type == InputType.password) internalControl.Width = 130;

                if (!this.heightAuto) internalControl.Height = this.TotalHeight;

                internalControl.Measure(constraint);

                this.RealDesiredSize = internalControl.DesiredSize;
                return internalControl.DesiredSize;
            }
            else
            {
                return new Size();
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (internalControl != null)
            {
                internalControl.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
                return internalControl.RenderSize;
            }
            else
            {
                return finalSize;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // Do nothing..
        }

        public override string ToString()
        {
            return base.ToString() + " <" + this.Type.ToString() + ">";
        }
    }
}
