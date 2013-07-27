using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;

using CostAnalysisWindow.Elements;

using Model.Template;

namespace CostAnalysisWindow
{
    class UIUpdater2
    {
        private readonly PropertyCollection m_Nodes = new PropertyCollection();

        public UIUpdater2(ReadOnlyCollection<PropertyElement> nodes)
        {
            this.m_Nodes = new PropertyCollection(nodes);
        }

        public void UpdateTree(TreeView treeView1)
        {
            treeView1.Nodes.Clear();
            CustomTreeNode root = new CustomTreeNode("Costs");
            treeView1.Nodes.Add(root);
            
            IEnumerable<PropertyElement> query = this.m_Nodes.OrderBy(element => element.Name);

            foreach (PropertyElement n in query)
            {
                if (n.NodesAbove.Count > 0)
                {
                }
                else if (n.NodesBelow.Count > 0)
                {
                    this.AddNodeToTree(root, n);
                }
                else
                {
                }
            }
            
        }

        public void AddNodeToTree(
            CustomTreeNode parent,
            PropertyElement node)
        {
            CustomTreeNode propNode = new CustomTreeNode(node.Property.Name);
            propNode.CodeElement = node;
            
            AddFieldsToTree(propNode, node);

            parent.Nodes.Add(propNode);

            IEnumerable<CodeElement> query = 
                node.NodesBelow.OrderBy(element => element.Name);
            
            foreach (PropertyElement n in query)
            {
                if (n == node)
                {
                    continue;
                }

                this.AddNodeToTree(propNode, n);
            }
        }

        public static void AddFieldsToTree(
            CustomTreeNode parent,
            PropertyElement node)
        {
            foreach (FieldElement element in node.OrphanedFields) 
            {
                CustomTreeNode propNode = new CustomTreeNode(element.Name);
                propNode.CodeElement = element;
                
                parent.Nodes.Add(propNode);
            }

        }
        
        public void DisplayValues(TreeView treeView1)
        {
            if (treeView1.IsDisposed)
            {
                return;
            }
            
            List<CustomTreeNode> nodes = this.GetAllNodes(treeView1);
            
            Model.Template.Header header =
                Model.ActiveHeader.ActiveHeader.Header as Model.Template.Header;
            
            if (header != null)
            {
                IComputationWrapper wrapper = header.ComputationProperties as IComputationWrapper;
                if (wrapper != null)
                {
                    UpdateNodes(nodes, wrapper);
                }
            }
            else
            {
                ClearNodes(nodes);
            }
            
            treeView1.Invalidate();
        }

        static void ClearNodes(
            List<CustomTreeNode> nodes)
        {
            foreach (CustomTreeNode treeNode in nodes) 
            {
                treeNode.PropertyValue = 0;
                treeNode.PropertyAttached = false;
            }
        }
        
        static void UpdateNodes(
            List<CustomTreeNode> nodes, 
            IComputationWrapper wrapper)
        {
            ComputationProperties comp = wrapper.GetComputationProperties();
            Type type = comp.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props) 
            {
                foreach (CustomTreeNode treeNode in nodes) 
                {
                    UpdateNode(comp, prop, treeNode);
                }
            }
        }

        static void UpdateNode(
            ComputationProperties comp, 
            PropertyInfo prop, 
            CustomTreeNode treeNode)
        {
            PropertyElement element = treeNode.CodeElement as PropertyElement;
            if (element != null) 
            {
                if (element.Property.Name == prop.Name) 
                {
                    MethodInfo m = prop.GetGetMethod();
                    
                    object value = m.Invoke(comp, new object[] { });
                    
                    if (value is decimal) 
                    {
                        decimal d = (decimal)value;
                        treeNode.PropertyValue = d;
                        treeNode.PropertyAttached = true;
                    }
                    
                    if (value is int) 
                    {
                        int i = (int)value;
                        treeNode.PropertyValue = new decimal(i);
                        treeNode.PropertyAttached = true;
                    }
                }
            }
        }
        
        List<CustomTreeNode> GetAllNodes(TreeView treeView1)
        {
            List<CustomTreeNode> result = new List<CustomTreeNode>();
            
            foreach (TreeNode child in treeView1.Nodes)
            {
                CustomTreeNode treeNode = child as CustomTreeNode;
                
                result.Add(treeNode);
                
                List<CustomTreeNode> grandChildren = this.GetChildNodes(treeNode);
                
                result.AddRange(grandChildren);
            }
            
            return result;
        }

        List<CustomTreeNode> GetChildNodes(CustomTreeNode node)
        {
            List<CustomTreeNode> result = new List<CustomTreeNode>();
            
            foreach (TreeNode child in node.Nodes)
            {
                CustomTreeNode treeNode = child as CustomTreeNode;
                
                result.Add(treeNode);
                
                List<CustomTreeNode> grandChildren = this.GetChildNodes(treeNode);
                
                result.AddRange(grandChildren);
            }
            
            return result;
        }
        
    }
}
