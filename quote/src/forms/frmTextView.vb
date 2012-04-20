Imports DCS.Quote.Common
Imports System.Reflection

Public Class frmTextView

    Private WithEvents _Header As Header
    Private WithEvents _PrimaryProperties As PrimaryPropeties

    Public Sub New(ByVal q As Common.Header)
        InitializeComponent()
        If q IsNot Nothing Then
            Me._Header = q
            Me._PrimaryProperties = q.PrimaryProperties
        Else
            Me._Header = New Model.BOM.Header
            Me._PrimaryProperties = _Header.PrimaryProperties
        End If
        UpdateText()

        Dim g As TextGenerator = New TextGenerator(_Header)
        Me.TextBox1.Text = g.Data
    End Sub

    Private Sub UpdateText()
        If Me._PrimaryProperties.CommonID > 0 Then
            If _Header.IsQuote Then
                Me.Text = "Quote " & Me._PrimaryProperties.CommonID
            Else
                Me.Text = "BOM " & Me._PrimaryProperties.CommonID
            End If
        Else
            Me.Text = "New BOM"
        End If
        If Me._Header.Dirty Then
            Me.Text = Me.Text + " *"
        End If
    End Sub


End Class