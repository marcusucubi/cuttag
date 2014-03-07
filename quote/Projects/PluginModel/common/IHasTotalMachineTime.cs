namespace Model.Common
{
    using System;

    /// <summary>
    /// Used by the component properties object.
    /// </summary>
    public interface IHasTotalMachineTime
    {
        /// <summary>
        /// Gets the total machine time for the component.
        /// </summary>
        /// <value>The machine time for the component.</value>
        decimal TotalMachineTime
        {
            get;
        }
    }
}
