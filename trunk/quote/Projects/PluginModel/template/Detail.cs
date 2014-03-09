namespace Model.Template
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    
    using Model.Template.Ext;

    /// <summary>
    /// The detail for a template.
    /// </summary>
    public class Detail : Common.Detail
    {
        /// <summary>
        /// Contains properties if the detail has a wire.
        /// </summary>
        private Common.WireProperties wireProperties;

        /// <summary>
        /// Contains properties if the detail has a component.
        /// </summary>
        private Common.ComponentProperties componentProperties;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Detail" /> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="product">The product.</param>
        public Detail(Header header, Product product) : base(product, product.UnitOfMeasure, 1)
        {
            this.Header = header;
            this.componentProperties = PropertyFactory.CreateComponentProperties(this);
            this.wireProperties = PropertyFactory.CreateWireProperties(this);
            this.SequenceNumber = this.Header.NextSequenceNumber;
            
            this.AddChildProperty(this.componentProperties);
            this.AddChildProperty(this.wireProperties);
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        [BrowsableAttribute(false)]
        public Header Header { get; set; }

        /// <summary>
        /// Gets the properties for the product. 
        /// </summary>
        /// <value>The wire or component properties.</value>
        [BrowsableAttribute(false)]
        public override Common.ISavableProperties QuoteDetailProperties 
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

        /// <summary>
        /// Sets the product for this detail.
        /// </summary>
        /// <param name="product">The new product.</param>
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
