''' <summary>
''' Used to display drop down menu for quote compare
''' </summary>
''' <remarks></remarks>
Public Class CompareQuoteMenuItem
    Inherits ToolStripMenuItem

    Private _Header As Model.Common.Header

    Public Sub New(name As String, header As Model.Common.Header)
        MyBase.New(name)
        _Header = header
    End Sub

    Public ReadOnly Property Header
        Get
            Return _Header
        End Get
    End Property

End Class

