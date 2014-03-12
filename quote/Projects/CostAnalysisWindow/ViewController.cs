namespace CostAnalysisWindow
{
    using System;
    
    using WeifenLuo.WinFormsUI.Docking;
    
    public class ViewController : IDisposable
    {
        private static ViewController instance = new ViewController();

        private FormTree tree;

        private ViewController() 
        { 
        }

        public static ViewController Instance
        {
            get { return instance; }
        }

        public void ShowTree()
        {
            if (this.tree == null)
            {
                this.tree = new FormTree();
                InitChild(this.tree);
            }

            if (this.tree.IsHidden || this.tree.IsDisposed) 
            {
                this.tree = new FormTree();
                InitChild(this.tree);
            }
        }
        
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool native)
        {
            this.tree.Close();
            this.tree = null;
        }
        
        static void InitChild(DockContent frm)
        {
            Host.UI.UIApp.DockPanel.SuspendLayout(true);
            frm.Show(Host.UI.UIApp.DockPanel, DockState.DockLeft);
            Host.UI.UIApp.DockPanel.ResumeLayout(true, true);
        }
    }
}
