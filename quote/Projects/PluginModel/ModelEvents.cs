namespace Model
{
    using System;
    using System.Linq;
    
    /// <summary>
    /// Used to signal changes to the model.  Like when a quote is loaded.
    /// This is a singleton class.
    /// </summary>
    public sealed class ModelEvents
    {
        /// <summary>
        /// The only instance.
        /// </summary>
        private static ModelEvents instance = new ModelEvents();
        
        /// <summary>
        /// Prevents a default instance of the <see cref="ModelEvents"/> class from being created.
        /// </summary>
        private ModelEvents()
        {
            // Singleton class
        }

        /// <summary>
        /// Signals when a template is created.
        /// </summary>
        public event EventHandler<ModelEventArgs> TemplateCreated;

        /// <summary>
        /// Signals when a template is viewed.
        /// </summary>
        public event EventHandler TemplateViewed;

        /// <summary>
        /// Signals when a quote is viewed.
        /// </summary>
        public event EventHandler QuoteViewed;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static ModelEvents Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Signal that a template has been created.
        /// </summary>
        /// <param name="id">The id of the model object.</param>
        public void NotifyTemplateCreated(int id)
        {
            ModelEventArgs args = new ModelEventArgs();
            args.Id = id;
            if (this.TemplateCreated != null) 
            {
                this.TemplateCreated(null, args);
            }
        }

        /// <summary>
        /// Signal that a template has been viewed.
        /// </summary>
        public void NotifyTemplateViewed()
        {
            if (this.TemplateViewed != null) 
            {
                this.TemplateViewed(null, new EventArgs());
            }
        }

        /// <summary>
        /// Signal that a quote has been viewed.
        /// </summary>
        public void NotifyQuoteViewed()
        {
            if (this.QuoteViewed != null) 
            {
                this.QuoteViewed(null, new EventArgs());
            }
        }
    }
}