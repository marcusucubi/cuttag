namespace PluginOutputView
{
    partial class FrmOutputView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._TextBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _TextBox1
            // 
            this._TextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TextBox1.Location = new System.Drawing.Point(0, 0);
            this._TextBox1.Multiline = true;
            this._TextBox1.Name = "_TextBox1";
            this._TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._TextBox1.Size = new System.Drawing.Size(284, 262);
            this._TextBox1.TabIndex = 1;
            // 
            // FrmOutputView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._TextBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmOutputView";
            this.Text = "Output";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox _TextBox1;
    }
}