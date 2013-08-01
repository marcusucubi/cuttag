namespace Model
{
    using System;
    using System.Linq;
    
    public sealed class ModelEvents
    {
        private static ModelEvents activeEvents = new ModelEvents();
        
        private ModelEvents()
        {
            // Use static members only
        }

        public static event EventHandler<ModelEventArgs> TemplateCreated;

        public static event EventHandler TemplateViewed;

        public static event EventHandler QuoteViewed;

        public static ModelEvents ActiveEvents 
        {
            get { return activeEvents; }
        }

        public static void NotifyTemplateCreated(int id)
        {
            ModelEventArgs args = new ModelEventArgs();
            args.Id = id;
            if (TemplateCreated != null) 
            {
                TemplateCreated(null, args);
            }
        }

        public static void NotifyTemplateViewed()
        {
            if (TemplateViewed != null) 
            {
                TemplateViewed(null, new EventArgs());
            }
        }

        public static void NotifyQuoteViewed()
        {
            if (QuoteViewed != null) 
            {
                QuoteViewed(null, new EventArgs());
            }
        }
    }
}