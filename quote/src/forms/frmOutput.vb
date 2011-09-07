Imports WeifenLuo.WinFormsUI.Docking
Imports System.IO

Public Class frmOutput
    Inherits DockContent

    Public Class ConsoleWriter
        Inherits TextWriter

        Private _Writer As TextWriter
        Private _TextBox As TextBox

        Public Sub New(ByVal writer As TextWriter, ByVal text As TextBox)
            _Writer = writer
            _TextBox = text
        End Sub

        Public Overrides Sub Write(ByVal value As Char)
            MyBase.Write(value)
            _TextBox.SuspendLayout()
            _TextBox.Text = _TextBox.Text + value
            If value = Chr(10) Then
                _TextBox.SelectionStart = _TextBox.TextLength
                _TextBox.ScrollToCaret()
            End If
            _TextBox.ResumeLayout()
        End Sub

        Public Overrides ReadOnly Property Encoding As System.Text.Encoding
            Get
                Return _Writer.Encoding
            End Get
        End Property
    End Class

    Public Sub New()

        InitializeComponent()

        Dim writer As ConsoleWriter = New ConsoleWriter(System.Console.Out, _TextBox1)
        System.Console.SetOut(writer)

    End Sub

End Class