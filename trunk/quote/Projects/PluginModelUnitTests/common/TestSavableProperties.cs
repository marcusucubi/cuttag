namespace PluginModelUnitTests.Common
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    
    using Model.Common;

    public class TestSavableProperties : SavableProperties
    {
        private bool sampleProperty;
       
        public TestSavableProperties()
        {
        }
        
        public bool SampleProperty
        {
            get
            {
                return sampleProperty;
            }
            set
            {
                sampleProperty = value;
                base.OnPropertyChanged();
            }
        }
        
        public void Add(SavableProperties subject)
        {
            base.AddDependent(subject);
        }
        
        public void Remove(SavableProperties subject)
        {
            base.RemoveDependent(subject);
        }
    }
}