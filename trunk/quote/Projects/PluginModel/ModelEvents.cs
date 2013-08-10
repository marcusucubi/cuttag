namespace Model
{
    using System;
    using System.Linq;
    
    public sealed class ModelEvents
    {
        private static ModelEvents instance = new ModelEvents();
        
        private ModelEvents()
        {
            // Singleton class
        }

        public event EventHandler<ModelEventArgs> TemplateCreated;

        public event EventHandler TemplateViewed;

        public event EventHandler QuoteViewed;

        public static ModelEvents Instance
        {
            get { return instance; }
        }

        public void NotifyTemplateCreated(int id)
        {
            ModelEventArgs args = new ModelEventArgs();
            args.Id = id;
            if (this.TemplateCreated != null) 
            {
                this.TemplateCreated(null, args);
            }
        }

        public void NotifyTemplateViewed()
        {
            if (this.TemplateViewed != null) 
            {
                this.TemplateViewed(null, new EventArgs());
            }
        }

        public void NotifyQuoteViewed()
        {
            if (this.QuoteViewed != null) 
            {
                this.QuoteViewed(null, new EventArgs());
            }
        }
    }
}