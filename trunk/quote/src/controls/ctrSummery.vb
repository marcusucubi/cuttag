Imports DCS.Quote.Model

Public Class ctrSummery

    Dim WithEvents _CountBinding As Binding
    Private _QuoteHeader As QuoteHeader

    Public Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
        Set(ByVal value As QuoteHeader)
            _QuoteHeader = value
            If value IsNot Nothing Then
                Me.PropertyGrid1.SelectedObject = value
            End If
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim OptionsPropertyGrid As PropertyGrid = New PropertyGrid()
        OptionsPropertyGrid.Size = New Drawing.Size(300, 250)

        Me.Controls.Add(OptionsPropertyGrid)
        Me.Text = "Options Dialog"
    End Sub
End Class
