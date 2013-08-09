namespace Model.Common
{
    using System;
    using System.Linq;

    /// <summary>
    /// Holds the data displayed in the options form.
    /// </summary>
    /// <remarks></remarks>
    public class GlobalOptions
    {
        private static GlobalOptions instance = new GlobalOptions();

        private static int decimalPointsToDisplay = 4;
        
        public event EventHandler Changed;

        public static GlobalOptions Instance 
        {
            get { return instance; }
        }

        public int DecimalPointsToDisplay 
        {
            get 
            { 
                return decimalPointsToDisplay; 
            }
            
            set 
            {
                if (decimalPointsToDisplay != value)
                {
                    decimalPointsToDisplay = value;
                    instance.FireChanged();
                }
            }
        }

        private void FireChanged()
        {
            if (this.Changed != null) 
            {
                this.Changed(this, new EventArgs());
            }
        }
    }
}
