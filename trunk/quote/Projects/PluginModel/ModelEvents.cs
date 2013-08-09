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

        public static ModelEvents Instance
        {
            get { return instance; }
        }

        public event EventHandler<ModelEventArgs> TemplateCreated;

        public event EventHandler TemplateViewed;

        public event EventHandler QuoteViewed;

        public void NotifyTemplateCreated(int id)
        {
            ModelEventArgs args = new ModelEventArgs();
            args.Id = id;
            if (TemplateCreated != null) 
            {
                TemplateCreated(null, args);
            }
        }

        public void NotifyTemplateViewed()
        {
            if (TemplateViewed != null) 
            {
                TemplateViewed(null, new EventArgs());
            }
        }

        public void NotifyQuoteViewed()
        {
            if (QuoteViewed != null) 
            {
                QuoteViewed(null, new EventArgs());
            }
        }
    }
}