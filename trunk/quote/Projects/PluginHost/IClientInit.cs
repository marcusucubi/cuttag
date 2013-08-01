namespace Host
{
    using System;

    /// <summary> 
    /// This will be called on start.  Any class can implement.
    /// </summary> 
    public interface IInit
    {
        /// <summary> 
        /// Called on startup.
        /// </summary> 
        void Init();
    }
}
