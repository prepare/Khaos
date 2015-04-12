using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WinStyle = System.Windows.Style;
using CssStyle = CascadingStyleSheets.Style;
using System.Diagnostics;

namespace RenderEngine
{
    //public partial class VisualNode
    //{
    //protected
        public class SectionManager : ChildManager
        {
            // Static Variables

            protected static int splitCount = 0;
            public static int SplitCount { get { return splitCount; } }

            // Variables

            protected string name;
            //protected CssStyle cssStyle;
            protected LinkedList<VisualNode> sections;

            // Properties

            public string Name
            {
                set { name = value; }
                get { return name; }
            }

            public int SectionCount
            {
                get { return sections.Count; }
            }

            // Construtors

            public SectionManager() //CssStyle defaultStyle)
                : base(null)
            {
                this.name = "Unnamed";
                this.parent = null;
                this.sections = new LinkedList<VisualNode>();
            }

            // Methods

            public virtual void AddSection(VisualNode section, VisualNode splitMaker)
            {
                // Add a visual count for the parent

                //// SANITY CHECKS
                // Sections after add |GREATER THAN| Elements (-1 for the initial)
                if ((sections.Count + 1) > (elements.Count -1 /* - 1*/))
                {
                    Debug.WriteLine("There are more sections that elemets! Total sections: " + splitCount);
                    return;
                }
                else if (parent == null)
                {
                    Debug.WriteLine("Parent cannot be split error!");
                    return;
                }
                //// END SANITY CHECKS

                // Add the new section to the node 
                // above so it can be rendered
                sections.AddLast(section);
                parent.AddAfter(section, splitMaker);

                splitCount++;
            }

            public virtual void ResetSplitting()
            {
                // This method is recursive
                // It will reset all splitting

                if (sections.Count > 0)
                {
                    var current = sections.First; //Last;
                    //for (int sIndex = 0; sIndex < sections.Count; sIndex++)
                    while (current != null)
                    {
                        var splitItem = current.Value;

                        // Debug
                        splitCount--;

                        // Relink a spit visualNodes children to original node
                        for (int cIndex = 0; cIndex < splitItem.Count; cIndex++)
                        {
                            VisualNode visualNode = splitItem[cIndex];
                            splitItem.splitMaker.AddLocalChild(visualNode);
                        }

                        // Remove
                        parent.RemoveChild(splitItem);

                        //current = current.Previous;
                        current = current.Next;
                    }
                    sections.Clear();
                }

                for (int index = 0; index < elements.Count; index++)
                {
                    elements[index].ResetSplitting();
                }
            }

            //public virtual void ResetSplitting()
            //{
            //     //This method is recursive
            //     //It will reset all splitting
            //    if (parent != null)
            //        parent.RemoveChild(vNode);

            //    if (sections.Count > 0)
            //        sections.Clear();

            //    for (int index = 0; index < elements.Count; index++)
            //    {
            //        VisualNode element = elements[index];
            //        element.ResetSplitting();
            //        vNode.AddChild(element);

            //        Debug.WriteLine(vNode.DebugID + ": Add Refresh : " + index);
            //    }

            //}

            public override string ToString()
            {
                return name + " split: " + sections.Count;
            }

        } // End of Section Manager
    //}
}
