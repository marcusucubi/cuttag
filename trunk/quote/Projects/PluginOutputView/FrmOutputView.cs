using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace PluginOutputView
{
    public partial class FrmOutputView : DockContent
    {
        public FrmOutputView()
        {
            InitializeComponent();

            ConsoleWriter writer = new ConsoleWriter(Console.Out, _TextBox1);
            Console.SetOut(writer);
        }

        public class ConsoleWriter : TextWriter
        {
            private TextWriter _Writer;
            private TextBox _TextBox;

            public ConsoleWriter(TextWriter writer, TextBox text)
            {
                _Writer = writer;
                _TextBox = text;
            }

            public override void Write(Char value)
            {
                base.Write(value);
                _TextBox.SuspendLayout();
                _TextBox.Text = _TextBox.Text + value;
                if (value == '\r') 
                {
                    _TextBox.SelectionStart = _TextBox.TextLength;
                    _TextBox.ScrollToCaret();
                }
                _TextBox.ResumeLayout();
            }

            public override System.Text.Encoding Encoding 
            {
                get { return _Writer.Encoding; }
            }
        }
        
    }
}
