namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template.Ext;

    public class Detail : Common.Detail
    {
        private Common.WireProperties wireProperties;

        private Common.ComponentProperties componentProperties;
        
        public Detail(Header header, Product product) : base(product, product.UnitOfMeasure, 1)
        {
            this.Header = header;
            this.IsWire = product.IsWire;
            this.componentProperties = PropertyFactory.CreateComponentProperties(this);
            this.wireProperties = PropertyFactory.CreateWireProperties(this);
            this.SequenceNumber = this.Header.NextSequenceNumber;
        }

        [BrowsableAttribute(false)]
        public Header Header { get; set; }

        [BrowsableAttribute(false)]
        public override Common.SavableProperties QuoteDetailProperties 
        {
            get 
            {
                if (this.IsWire) 
                {
                    return this.wireProperties;
                } 
                else 
                {
                    return this.componentProperties;
                }
            }
        }

        public void UpdateComponentProperties(Product product)
        {
            this.SetProduct(product);
            this.SetPrivateUnitOfMeasure(product.UnitOfMeasure);

            this.componentProperties = PropertyFactory.CreateComponentProperties(this);

            if (this.wireProperties is DefaultWireProperties) 
            {
                DefaultWireProperties w = this.wireProperties as DefaultWireProperties;
                w.PoundsPer1000Feet = product.CopperWeightPer1000Feet;
            }
        }
    }
}
