using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CostAnalysisWindow
{
    class UIUpdater2
    {
        private readonly PropertyNodeCollection2 m_Nodes = new PropertyNodeCollection2();

        public UIUpdater2(PropertyNodeCollection2 nodes)
        {
            m_Nodes = nodes;
        }

        public void UpdateTree(TreeView treeView1)
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("Costs");
            treeView1.Nodes.Add(root);

            m_Nodes.Sort();
            foreach (PropertyNode2 n in m_Nodes)
            {
                if (n.DependingProperties.Count > 0)
                {
                }
                else if (n.DependentProperties.Count > 0)
                {
                    AddNodeToTree(root, n);
                }
                else
                {
                }
            }
        }

        public void AddNodeToTree(
            TreeNode parent,
            PropertyNode2 node)
        {
            TreeNode propNode = new TreeNode(node.Property.Name);
            propNode.Tag = node;

            if (!node.ReadonlyProperty)
            {
                propNode.ImageIndex = 3;
            }
            else if (node.DependentProperties.Count == 0)
            {
                propNode.ImageIndex = 1;
            }
            else
            {
                propNode.ImageIndex = 2;
            }

            parent.Nodes.Add(propNode);

            foreach (PropertyNode2 n in node.DependentProperties)
            {
                if (n == node)
                {
                    continue;
                }

                AddNodeToTree(propNode, n);
            }
        }

    }
}
