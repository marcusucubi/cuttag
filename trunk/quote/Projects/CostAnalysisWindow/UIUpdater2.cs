using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using CostAnalysisWindow.Elements;

using Model.Template;

namespace CostAnalysisWindow
{
    class UIUpdater2
    {
        private readonly PropertyCollection m_Nodes = new PropertyCollection();

        public UIUpdater2(PropertyCollection nodes)
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
        
        public void DisplayValues(TreeView treeView1)
        {
            if (treeView1.IsDisposed)
            {
                return;
            }
            
            Model.Template.Header header =
                Model.ActiveHeader.ActiveHeader.Header as Model.Template.Header;
            
            if (header != null)
            {
                List<TreeNode> nodes = GetAllNodes(treeView1);
                treeView1.SuspendLayout();
            
                IComputationWrapper wrapper = header.ComputationProperties as IComputationWrapper;
                if (wrapper != null)
                {
                    UpdateNodes(nodes, wrapper);
                }
                
                treeView1.ResumeLayout();
            }
            
        }

        void UpdateNodes(List<TreeNode> nodes, IComputationWrapper wrapper)
        {
            ComputationProperties comp = wrapper.GetComputationProperties();
            Type type = comp.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props) {
                foreach (TreeNode treeNode in nodes) {
                    UpdateNode(comp, prop, treeNode);
                }
            }
        }

        void UpdateNode(ComputationProperties comp, PropertyInfo prop, TreeNode treeNode)
        {
            PropertyElement element = treeNode.Tag as PropertyElement;
            if (element != null) {
                if (element.Property.Name == prop.Name) {
                    MethodInfo m = prop.GetGetMethod();
                    object value = m.Invoke(comp, new object[] {
                                                
                                            });
                    if (value is decimal) {
                        decimal d = (decimal)value;
                        treeNode.Text = d.ToString("#,##0.0000") + " " + prop.Name;
                    }
                    if (value is int) {
                        int i = (int)value;
                        treeNode.Text = i.ToString("#,##0.0000") + " " + prop.Name;
                    }
                }
            }
        }
        
        List<TreeNode> GetAllNodes(TreeView treeView1)
        {
            List<TreeNode> result = new List<TreeNode>();
            
            foreach(TreeNode child in treeView1.Nodes)
            {
                result.Add(child);
                
                List<TreeNode> grandChildren = GetChildNodes(child);
                
                result.AddRange(grandChildren);
            }
            
            return result;
        }

        List<TreeNode> GetChildNodes(TreeNode node)
        {
            List<TreeNode> result = new List<TreeNode>();
            
            foreach(TreeNode child in node.Nodes)
            {
                result.Add(child);
                
                List<TreeNode> grandChildren = GetChildNodes(child);
                
                result.AddRange(grandChildren);
            }
            
            return result;
        }
        
    }
}
