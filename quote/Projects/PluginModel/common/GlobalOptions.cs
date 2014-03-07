namespace Model.Common
{
    using System;
    using System.Linq;

    /// <summary>
    /// Holds the data displayed in the options form.
    /// </summary>
    public class GlobalOptions
    {
        /// <summary>
        /// The static instance of options.
        /// </summary>
        private static GlobalOptions instance = new GlobalOptions();

        /// <summary>
        /// The number of decimal digits to round.
        /// </summary>
        private static int decimalPointsToDisplay = 4;
        
        /// <summary>
        /// Notifies clients when the options are changed.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Gets the options object.
        /// </summary>
        /// <value>The global options data object.</value>
        public static GlobalOptions Instance 
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets or sets the number of digits to round decimal values to.
        /// </summary>
        /// <value>The number of digits to round to.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Ignore")]
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

        /// <summary>
        /// Triggers the changed event.
        /// </summary>
        private void FireChanged()
        {
            if (this.Changed != null) 
            {
                this.Changed(this, new EventArgs());
            }
        }
    }
}
