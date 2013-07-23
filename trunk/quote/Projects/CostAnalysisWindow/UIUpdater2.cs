﻿using System;
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
            CustomTreeNode root = new CustomTreeNode("Costs");
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
            CustomTreeNode parent,
            PropertyElement node)
        {
            CustomTreeNode propNode = new CustomTreeNode(node.Property.Name);
            propNode.CodeElement = node;
            
            AddFieldsToTree(propNode, node);

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
            CustomTreeNode parent,
            PropertyElement node)
        {
            foreach(FieldElement element in node.OrphanedFieldDefs)
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
            
            Model.Template.Header header =
                Model.ActiveHeader.ActiveHeader.Header as Model.Template.Header;
            
            List<CustomTreeNode> nodes = GetAllNodes(treeView1);
            treeView1.SuspendLayout();
            
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
                ClearNodes(treeView1);
            }
            
            treeView1.ResumeLayout();
            
        }

        void ClearNodes(TreeView treeView1)
        {
            List<CustomTreeNode> nodes = GetAllNodes(treeView1);
            foreach(CustomTreeNode node in nodes)
            {
                node.ResetText();
            }
        }

        void UpdateNodes(
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

        void UpdateNode(
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
                    
                    object value = m.Invoke(comp, new object[] {});
                    
                    if (value is decimal) 
                    {
                        decimal d = (decimal)value;
                        treeNode.Text = d.ToString("#,##0.0000") + " " + prop.Name;
                    }
                    
                    if (value is int) 
                    {
                        int i = (int)value;
                        treeNode.Text = i.ToString("#,##0.0000") + " " + prop.Name;
                    }
                }
            }
        }
        
        List<CustomTreeNode> GetAllNodes(TreeView treeView1)
        {
            List<CustomTreeNode> result = new List<CustomTreeNode>();
            
            foreach(TreeNode child in treeView1.Nodes)
            {
                result.Add(child as CustomTreeNode);
                
                List<CustomTreeNode> grandChildren = GetChildNodes(child as CustomTreeNode);
                
                result.AddRange(grandChildren);
            }
            
            return result;
        }

        List<CustomTreeNode> GetChildNodes(CustomTreeNode node)
        {
            List<CustomTreeNode> result = new List<CustomTreeNode>();
            
            foreach(TreeNode child in node.Nodes)
            {
                result.Add(child as CustomTreeNode);
                
                List<CustomTreeNode> grandChildren = GetChildNodes(child as CustomTreeNode);
                
                result.AddRange(grandChildren);
            }
            
            return result;
        }
        
    }
}
