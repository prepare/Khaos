using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RenderEngine;

namespace VisualNodeVisualiser
{
    [Serializable]
    public class visualTreeNode //: TreeNode
    {
        public LinkedList<visualTreeNode> Children;
        public string Name;

        public visualTreeNode(VisualNode vNode)// : base()
        {
            //this.Text = vNode.GetType().Name + ": " + vNode.ToString();
            this.Name = vNode.GetType().Name + ": " + vNode.ToString();
            Children = new LinkedList<visualTreeNode>();
        }

        public static visualTreeNode Createtree(VisualNode vNode)
        {
            visualTreeNode node = new visualTreeNode(vNode);
            CreateTreeReq(node, vNode);
            return node;
        }

        public static void CreateTreeReq(visualTreeNode node, VisualNode vNode)
        {
            visualTreeNode node2;
            VisualNode vNode2;

            for (int index = 0; index < vNode.Count; index++)
            {
                vNode2 = vNode[index];
                node2 = new visualTreeNode(vNode2);

                //node.Nodes.Add(node2);
                node.Children.AddLast(node2);

                CreateTreeReq(node2, vNode2);
            }
        }
    }
}
