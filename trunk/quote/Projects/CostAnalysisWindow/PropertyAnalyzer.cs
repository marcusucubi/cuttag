using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

using Dile.Disassemble;
using Dile.Disassemble.ILCodes;
using Dile.Metadata;

using SampleWindow;
using System.Reflection.Emit;

namespace SampleProperties
{
    class PropertyAnalyzer
    {
        private PropertyNodeCollection m_Nodes = new PropertyNodeCollection();

        public PropertyNodeCollection Nodes
        {
            get { return m_Nodes; }
        }

        public void Init()
        {
            Model.Common.SaveableProperties computationProperties = 
                BuildComputationProperties();

            OpCodeGroups.Initialize();
            Type computationPropertiesType = computationProperties.GetType();

            Assembly assembly = LoadAssembly(computationPropertiesType);
            TypeDefinition typeDefinition = 
                LoadTypeDef(computationPropertiesType, assembly);
            List<Property> props = LoadProperties(typeDefinition);

            Dictionary<uint, MethodDefinition> methodDefinitions = 
                BuildMethodDefs(typeDefinition, props);
            m_Nodes.Sort();

            foreach (Property p in props)
            {
                PropertyNode cursor = m_Nodes.Find(p);
                p.Initialize();

                ProcessPropteryDef(methodDefinitions, p, cursor);
                ProcessPropteryDef2(methodDefinitions, p, cursor);
                ProcessPropteryDef3(methodDefinitions, p, cursor);
            }

            foreach (PropertyNode propNode in m_Nodes)
            {
                PropertyNodeCollection col =
                    m_Nodes.FindNodeWithDependent(propNode.Property.Name);

                foreach (PropertyNode childNode in col)
                {
                    propNode.DependingProperties.Add(childNode);
                }
            }

        }

        private static Assembly LoadAssembly(Type computationPropertiesType)
        {
            Assembly assembly = new Assembly(false, false);
            assembly.FullPath = computationPropertiesType.Assembly.Location;
            assembly.LoadAssembly();
            assembly.Initialize();
            return assembly;
        }

        private void ProcessPropteryDef(
            Dictionary<uint, MethodDefinition> methodDefs,
            Property property,
            PropertyNode propertyNode)
        {
            MethodDefinition def = methodDefs[property.GetterMethodToken];
            def.Initialize();

            foreach (CodeLine line in def.CodeLines)
            {
                BaseILCode il = line as BaseILCode;
                propertyNode.CodeLines.Add(line);

                if (il == null)
                {
                    continue;
                }

                if (il.OpCode.FlowControl != System.Reflection.Emit.FlowControl.Call)
                {
                    continue;
                }

                MethodILCode methodCode = il as MethodILCode;
                if (methodCode == null)
                {
                    continue;
                }

                TokenBase param = methodCode.DecodedParameter;
                ProcessMethodDef(propertyNode, param);
            }
        }

        private void ProcessPropteryDef2(
            Dictionary<uint, MethodDefinition> methodDefs,
            Property property,
            PropertyNode propertyNode)
        {
            MethodDefinition def = methodDefs[property.GetterMethodToken];
            def.Initialize();

            foreach (CodeLine line in def.CodeLines)
            {
                BaseILCode il = line as BaseILCode;

                if (il == null)
                {
                    continue;
                }

                FieldILCode fieldCode = il as FieldILCode;
                if (fieldCode == null)
                {
                    continue;
                }

                FieldDefinition fieldDef = fieldCode.DecodedParameter as FieldDefinition;
                if (fieldDef == null)
                {
                    continue;
                }

                bool isSystem = (fieldDef.FieldTypeName.IndexOf("System") != -1);
                bool isInt = (fieldDef.FieldTypeName.IndexOf("int") != -1);
                bool isString = (fieldDef.FieldTypeName.IndexOf("string") != -1);
                if (!isSystem && !isInt && !isString)
                {
                    continue;
                }

                if (!propertyNode.FieldDefs.Contains(fieldDef))
                {
                    propertyNode.FieldDefs.Add(fieldDef);
                }
            }
        }

        private void ProcessPropteryDef3(
            Dictionary<uint, MethodDefinition> methodDefs,
            Property property,
            PropertyNode propertyNode)
        {
            MethodDefinition def = methodDefs[property.GetterMethodToken];
            def.Initialize();

            foreach (CodeLine line in def.CodeLines)
            {
                BaseILCode il = line as BaseILCode;

                if (il == null)
                {
                    continue;
                }

                FieldILCode fieldCode = il as FieldILCode;
                if (fieldCode == null)
                {
                    continue;
                }

                FieldDefinition fieldDef = fieldCode.DecodedParameter as FieldDefinition;
                if (fieldDef == null)
                {
                    continue;
                }

                if (propertyNode.Property.Name == "SummaryCostAdjustment")
                {
                    Console.WriteLine(propertyNode.Property.Name);
                }

                PropertyNodeCollection primaryNodeCollection = 
                    m_Nodes.FindNodePrimaryProperty(fieldDef.Name);
                foreach (PropertyNode primaryNode in primaryNodeCollection)
                {
                    propertyNode.DependentProperties.Add(primaryNode);
                }
            }
        }

        private void ProcessMethodDef(
            PropertyNode propertyNode, 
            TokenBase param)
        {
            MethodDefinition methodRef = param as MethodDefinition;

            if (methodRef == null)
            {
                return;
            }

            PropertyNode other = m_Nodes.Find(methodRef.Name);

            if (other != null && 
                propertyNode.Property.Name != other.Property.Name)
            {
                if (!propertyNode.DependentProperties.Contains(other))
                {
                    propertyNode.DependentProperties.Add(other);
                }
            }
        }

        private Dictionary<uint, MethodDefinition> BuildMethodDefs(
            TypeDefinition typeDefinition, 
            List<Property> props)
        {
            Dictionary<uint, MethodDefinition> methodDefinitions = null;
            {
                methodDefinitions = new Dictionary<uint, MethodDefinition>(
                    typeDefinition.MethodDefinitions);

                foreach (Property p in props)
                {
                    PropertyNode cursor = m_Nodes.Find(p);
                    p.Initialize();

                    MethodDefinition def = methodDefinitions[p.GetterMethodToken];
                    def.Initialize();
                }

                foreach (Property p in props)
                {
                    MethodDefinition def = methodDefinitions[p.GetterMethodToken];
                    bool isReadonly = ( !methodDefinitions.ContainsKey(p.SetterMethodToken) );

                    PropertyNode node = new PropertyNode(p, def.Name, isReadonly);
                    m_Nodes.Add(node);
                }
            }

            return methodDefinitions;
        }

        private static List<Property> LoadProperties(
            TypeDefinition typeDef)
        {
            List<Property> props = new List<Property>();
            foreach (Property p in typeDef.Properties.Values)
            {
                props.Add(p);
            }

            return props;
        }

        private static TypeDefinition LoadTypeDef(
            Type t,
            Assembly assembly)
        {
            TypeDefinition typeDefinition = null;

            foreach (TypeDefinition typeDef in assembly.ModuleScope.TypeDefinitions.Values)
            {
                if (typeDef.FullName != t.FullName)
                {
                    continue;
                }

                typeDefinition = typeDef;
            }

            return typeDefinition;
        }

        private static Model.Common.SaveableProperties BuildComputationProperties()
        {
            Model.Template.Header header = new Model.Template.Header();
            Model.Common.SaveableProperties computationProperties =
                Model.Template.PropertyFactory.Instance.CreateComputationProperties(header, 1);
            Model.Template.IComputationWrapper wrapper =
                computationProperties as Model.Template.IComputationWrapper;

            if (wrapper != null)
            {
                computationProperties = wrapper.GetComputationProperties();
            }

            return computationProperties;
        }

    }
}
