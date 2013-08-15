namespace PluginModelUnitTests.Common
{
    using System;

    public class TestCommonHeader : Model.Common.Header
    {
        private bool quote;
        
        public TestCommonHeader(bool quote)
        {
            this.quote = quote;
        }
        
        public override Model.Common.Detail NewDetail(Model.Product product)
        {
            return new TestCommonDetail(product);
        }
        
        public override bool IsQuote { get { return this.quote; } }
        
        public override string DisplayName { get { return "Test"; } }
    }
}
