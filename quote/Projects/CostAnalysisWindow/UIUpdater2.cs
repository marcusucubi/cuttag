using System;
using System.Collections.Generic;
using System.Windows.Forms;

using CostAnalysisWindow.Elements;

namespace CostAnalysisWindow
{
    class UIUpdater2
    {
        private readonly ElementCollection m_Nodes = new ElementCollection();

        public UIUpdater2(ElementCollection nodes)
        {
            m_Nodes = nodes;
        }

        public void UpdateTree(TreeView treeView1)
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("Costs");
            treeView1.Nodes.Add(root);

            m_Nodes.Sort();
            foreach (PropertyElement n in m_Nodes)
            {
                if (n.NodesAbove.Count > 0)
                {
                }
                else if (n.NodesBelow.Count > 0)
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
            PropertyElement node)
        {
            TreeNode propNode = new TreeNode(node.Property.Name);
            propNode.Tag = node;
            
            AddFieldsToTree(propNode, node);

            if (!node.ReadonlyProperty)
            {
                propNode.ImageIndex = 7;
            }
            else if (node.NodesBelow.Count == 0)
            {
                propNode.ImageIndex = 6;
            }
            else
            {
                propNode.ImageIndex = 2;
            }

            parent.Nodes.Add(propNode);

            foreach (PropertyElement n in node.NodesBelow)
            {
                if (n == node)
                {
                    continue;
                }

                AddNodeToTree(propNode, n);
            }
        }

        public void AddFieldsToTree(
            TreeNode parent,
            PropertyElement node)
        {
            foreach(FieldElement element in node.OrphanedFieldDefs)
            {
                TreeNode propNode = new TreeNode(element.Name);
                propNode.Tag = element;
                propNode.ImageIndex = 5;
                
                parent.Nodes.Add(propNode);
            }

        }
        
    }
}
