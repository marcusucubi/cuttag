namespace Model.IO
{
    using System;
    
    public class ObjectGeneratorPropertyInfo
    {
        public string Name { get; set; }
        
        public string TypeName { get; set; }
        
        public object Value { get; set; }
        
        public string CodeSnippet { get; set; }
        
        public string Category { get; set; }
        
        public string Description { get; set; }
    }
}
