namespace CostAnalysisWindow.Decompile
{
    using System;
    using System.Threading;
    using ICSharpCode.Decompiler;

    public class DisassembleOptions
    {
        /// <summary>
        /// Gets whether a full decompilation (all members recursively) is desired.
        /// If this option is false, language bindings are allowed to show the only headers of the decompiled element's children.
        /// </summary>
        public bool FullDisassemble { get; set; }
        
        /// <summary>
        /// Gets/Sets the directory into which the project is saved.
        /// </summary>
        public string SaveAsProjectDirectory { get; set; }
        
        /// <summary>
        /// Gets the cancellation token that is used to abort the decompiler.
        /// </summary>
        /// <remarks>
        /// Decompilers should regularly call <c>options.CancellationToken.ThrowIfCancellationRequested();</c>
        /// to allow for cooperative cancellation of the decompilation task.
        /// </remarks>
        public CancellationToken CancellationToken { get; set; }
        
        /// <summary>
        /// Gets the settings for the decompiler.
        /// </summary>
        public DecompilerSettings DisassembleSettings { get; set; }

    }
}
