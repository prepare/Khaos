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
using System.Windows.Shapes;
using RenderEngine;
using DOMCore;

namespace PhoenixEngine
{
    /// <summary>
    /// Interaction logic for DeveloperTools.xaml
    /// </summary>
    public partial class DeveloperTools : Window
    {
        private string source;
        private IHTMLDocument dom;
        private VisualDocument vdm;

        public DeveloperTools(string source, DOMCore.IHTMLDocument dom, VisualDocument vdm)
        {
            InitializeComponent();

            this.source = source;
            this.dom = dom;
            this.vdm = vdm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSource();
            if (dom != null) UpdateDOMVisualiser();
            if (vdm != null) UpdateVDMVisualiser();
        }

        private void UpdateSource()
        {
            sourceTextBox.Text = source;
        }

        private void UpdateDOMVisualiser()
        {
            if (dom != null && dom.DocumentElement != null)
            {

                TreeViewItem treeRoot = new TreeViewItem() { Header = (dom.DocumentElement.TagName + ": " + dom.DocumentElement.NodeName) };
                domTreeViewer.Items.Add(treeRoot);
                treeRoot.IsExpanded = true;

                // The stack for the current dom and render elements
                Stack<IElement> domStack = new Stack<IElement>();
                Stack<TreeViewItem> treeStack = new Stack<TreeViewItem>();

                // Set the initial stack elements
                domStack.Push(dom.DocumentElement);
                treeStack.Push(treeRoot);

                // While there are still elements to evaluate
                while (domStack.Count > 0)
                {
                    // get the next element to evaluate
                    IElement item = domStack.Pop();
                    TreeViewItem parent = treeStack.Pop();

                    // For each child the element has
                    for (int cIndex = 0; cIndex < item.ChildNodes.Length; cIndex++)
                    {
                        object unidentifiedObject = item.ChildNodes[cIndex];

                        if (unidentifiedObject is IElement) //IText
                        {
                            var child = unidentifiedObject as IElement;

                            // Add
                            TreeViewItem newItem = new TreeViewItem() { Header = ("<" + child.TagName + ">") };
                            newItem.ToolTip = child.getElementInfo();
                            parent.Items.Add(newItem);
                            treeStack.Push(newItem);
                            domStack.Push(child);
                        }
                        else if (unidentifiedObject is IText)
                        {
                            //Add
                            TreeViewItem newItem = new TreeViewItem() { Header = ("#Text: " + (unidentifiedObject as IText).Data) };
                            parent.Items.Add(newItem);
                        }
                        else
                        {
                            throw new Exception("Sanity Check");
                        }
                    }
                }
            }
        }

        private void UpdateVDMVisualiser()
        {
            if (vdm != null)
            {
                TreeViewItem treeRoot = new TreeViewItem() { Header = (vdm.GetType().Name + ": " + vdm.ItemName) };
                visTreeViewer.Items.Add(treeRoot);
                treeRoot.IsExpanded = true;

                // The stack for the current dom and render elements
                Stack<VisualNode> visStack = new Stack<VisualNode>();
                Stack<TreeViewItem> treeStack = new Stack<TreeViewItem>();

                // Set the initial stack elements
                visStack.Push(vdm);
                treeStack.Push(treeRoot);

                // While there are still elements to evaluate
                while (visStack.Count > 0)
                {
                    // get the next element to evaluate
                    VisualNode item = visStack.Pop();
                    TreeViewItem parent = treeStack.Pop();

                    // For each child the element has
                    for (int cIndex = 0; cIndex < item.Count; cIndex++)
                    {
                        VisualNode child = item[cIndex];

                        // Add
                        TreeViewItem newItem = new TreeViewItem() { Header = (child.GetType().Name + ": " + child.ToString()) };

                        //
                        //if (child.Element != null && child.Element.CSSStyle != null)
                        //{
                        //    StringBuilder builder = new StringBuilder();
                        //    for (int propIndex = 0; propIndex < child.Element.CSSStyle.WebPropertyCount; propIndex++)
                        //    {
                        //        builder.AppendLine(child.Element.CSSStyle[propIndex]);
                        //    }
                        //    newItem.ToolTip = builder.ToString();
                        //}
                        //

                        parent.Items.Add(newItem);
                        treeStack.Push(newItem);
                        visStack.Push(child);
                    }
                }
                //visTreeViewer.Vie
            }
        }
    }
}
