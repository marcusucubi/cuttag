using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.Decompiler;

using Mono.Cecil;

namespace CostAnalysisWindow
{
    sealed class DebuggerTextOutput : ITextOutput
    {
        readonly ITextOutput output;
        
        readonly Dictionary<string, StringWriter> _Dictionary = 
            new Dictionary<string, StringWriter>();
        
        StringWriter activeWriter;
        int _Intent;
        
        public DebuggerTextOutput(ITextOutput output)
        {
            this.output = output;
        }
        
        public Dictionary<string, StringWriter> Dictionary
        {
            get { return _Dictionary; }
        }
            
        public ICSharpCode.NRefactory.TextLocation Location 
        {
            get { return output.Location; }
        }
        
        public void Indent()
        {
            output.Indent();
            _Intent++;
        }
        
        public void Unindent()
        {
            output.Unindent();
            _Intent--;
        }
        
        public void Write(char ch)
        {
            output.Write(ch);
            
            if (activeWriter != null && _Intent > 0)
            {
                activeWriter.Write(ch);
                System.Diagnostics.Debug.WriteLine(ch);
            }
        }
        
        public void Write(string text)
        {
            output.Write(text);
            
            if (activeWriter != null && _Intent > 0)
            {
                activeWriter.Write(text);
                System.Diagnostics.Debug.WriteLine(text);
            }
        }
        
        public void WriteLine()
        {
            output.WriteLine();
            
            if (activeWriter != null && _Intent > 0)
            {
                activeWriter.WriteLine();
            }
        }
        
        public void WriteDefinition(string text, object definition, bool isLocal)
        {
            System.Diagnostics.Debug.WriteLine(definition.GetType().Name + " - " + definition.ToString());
            
            MethodDefinition m = (definition as MethodDefinition);
            if (m != null)
            {
                activeWriter = new StringWriter();
                _Dictionary.Add(definition.ToString(), activeWriter);
                
                _Intent = 0;
            }
        }
        
        public void WriteReference(string text, object reference, bool isLocal)
        {
            output.WriteReference(text, reference, isLocal);
            if (activeWriter != null && _Intent > 0)
            {
                activeWriter.Write(text);
                System.Diagnostics.Debug.WriteLine(text);
            }
        }
        
        public void AddDebuggerMemberMapping(MemberMapping memberMapping)
        {
        }
        
        public void MarkFoldStart(string collapsedText, bool defaultCollapsed)
        {
            output.MarkFoldStart(collapsedText, defaultCollapsed);
        }
        
        public void MarkFoldEnd()
        {
            output.MarkFoldEnd();
        }
    }
}
