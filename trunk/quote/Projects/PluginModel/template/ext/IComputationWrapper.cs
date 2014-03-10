namespace Model.Template.Ext
{
    using System;
    using System.Linq;

    /// <summary>
    /// Implemented by the display decorator classes.
    /// This interface is used by the CostAnalysisWindow
    /// project.  It can probably be removed somehow.
    /// </summary>
    public interface IComputationWrapper
    {
        /// <summary>
        /// Gets the decorated computation properties object.
        /// </summary>
        /// <value>The decorated computation properties object.</value>
        ComputationProperties ComputationProperties { get; }
    }
}
