using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RenderEngine;

namespace VisualNodeVisualiser
{
    public partial class VisForm : Form
    {
        visualTreeNode node;

        public VisForm(visualTreeNode node)
        {
            this.node = node;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            TreeNode tNode = new TreeNode(node.Name);
            BuiltTree(tNode, node);

            //treeView.Nodes.Add(node);
            treeView.Nodes.Add(tNode);

            treeView.ExpandAll();
        }

        private void BuiltTree(TreeNode tNode, visualTreeNode vNode)
        {
            TreeNode tNodeChild;

            foreach (var item in vNode.Children)
            {
                tNodeChild = new TreeNode(item.Name);
                tNode.Nodes.Add(tNodeChild);

                BuiltTree(tNodeChild, item);
            }
        }
    }
}
