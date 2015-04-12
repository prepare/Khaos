using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RenderEngine;
using DOMCore;
using CssStyle = CascadingStyleSheets.Style;
using System.Diagnostics;

namespace RenderEngine
{
    //public partial class VisualNode
    //{
    //    protected 
        public class ChildManager
        {

            // Variables

            protected VisualNode parent;
            //protected VisualNode vNode;
            protected List<VisualNode> elements;
            protected IHTMLElement domElement; 

            // Properties

            public virtual int Count
            {
                get { return elements.Count; }
            }
            public virtual VisualNode this[int index]
            {
                get { return elements[index]; }
            }
            public VisualNode Parent
            {
                get { return parent; }
                set { parent = value; }
            }
            public IHTMLElement Element
            {
                get 
                { 
                    return domElement; 
                }
                set 
                { 
                    domElement = value;
                }
            }

            public virtual CssStyle CssStyle
            {
                get 
                { 
                    if (domElement != null)
                        return domElement.CSSStyle;

                    if (backupStyle != null)
                    {
                        return backupStyle;
                    }
                    else if (parent != null)
                    {
                        if (parent.CssStyle != null)
                        {
                            backupStyle = new CascadingStyleSheets.Style();
                            backupStyle.InheritFrom(parent.CssStyle);
                            return backupStyle;
                        }
                        else
                        {
                            Debug.WriteLine("Parent has no css in visual doc.");

                            backupStyle = new CascadingStyleSheets.Style();
                            return backupStyle;
                        }
                    }
                    else
                    {
                        backupStyle = new CascadingStyleSheets.Style();
                        return backupStyle;
                    }
                }
            }

            private CssStyle backupStyle = null;

            // Construtors

            public ChildManager(VisualNode parentNode)
            {
                this.parent = parentNode;
                this.elements = new List<VisualNode>();
            }

            // Methods

            public void AddChild(VisualNode child)
            {
                this.elements.Add(child);
            }

            public void AddChildAfter(VisualNode child, VisualNode nodeBefore)
            {
                this.elements.Insert(this.elements.IndexOf(nodeBefore) + 1, child);
                //this.elements.Add(child);
            }

            public void RemoveChild(VisualNode child)
            {
                this.elements.Remove(child);
            }

            public VisualNode GetChild(VisualNode childOwner, int index)
            {
                int realIndex = childOwner.LogicalBeginIndex + index;

                if ( (realIndex < 0) || (realIndex > (elements.Count - 1)))
                    throw new IndexOutOfRangeException(index + " : " + realIndex);

                return elements[realIndex];
            }
        }
    //}
}