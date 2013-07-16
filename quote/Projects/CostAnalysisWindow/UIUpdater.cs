using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SampleWindow
{
    class UIUpdater
    {
        private readonly PropertyNodeCollection m_Nodes = new PropertyNodeCollection();

        public UIUpdater(PropertyNodeCollection nodes)
        {
            m_Nodes = nodes;
        }

        public void UpdateTree(TreeView treeView1)
        {
            TreeNode root = new TreeNode("Costs");
            treeView1.Nodes.Add(root);

            //TreeNode orphanNode = new TreeNode("Other Properties");
            //root.Nodes.Add(orphanNode);

            m_Nodes.Sort();
            foreach (PropertyNode n in m_Nodes)
            {
                if (n.DependingProperties.Count > 0)
                {
                    //TreeNode propNode = new TreeNode(n.Property.Name);
                    //orphanNode.Nodes.Add(propNode);
                }
                else if (n.DependentProperties.Count > 0)
                {
                    AddNodeToTree(root, n);
                }
                else
                {
                    //TreeNode propNode = new TreeNode(n.Property.Name);
                    //orphanNode.Nodes.Add(propNode);
                }
            }
        }

        public void AddNodeToTree(
            TreeNode parent,
            PropertyNode node)
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

            foreach (PropertyNode n in node.DependentProperties)
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
