Imports DCS.Quote.Model

Public Class ctrWires3

    Dim WithEvents _TotalLengthBinding As Binding
    Dim WithEvents _TotalLengthFeetBinding As Binding
    Dim WithEvents _WireCountBinding As Binding

    Public Property QuoteHeader As QuoteHeader

    Private Sub Form2_Load(ByVal sender As Object, _
                           ByVal e As System.EventArgs) _
                Handles MyBase.Load

        _TotalLengthBinding = New Binding("Text", QuoteHeader, "TotalLength")
        Me.txtTotalLengthDm.DataBindings.Add(_TotalLengthBinding)

        _TotalLengthFeetBinding = New Binding("Text", QuoteHeader, "TotalLengthFeet")
        Me.txtTotalLengthFeet.DataBindings.Add(_TotalLengthFeetBinding)

        _WireCountBinding = New Binding("Text", QuoteHeader, "WireCount")
        Me.txtWireCount.DataBindings.Add(_WireCountBinding)

        'txt2.DataBindings.Add("Text", oDt, "GetDateTime")
    End Sub

    Private Sub _TotalLengthBinding_Format(ByVal sender As Object, _
                                           ByVal e As System.Windows.Forms.ConvertEventArgs) _
                                       Handles _TotalLengthBinding.Format
        'e.Value = FormatNumber(CInt(e.Value), , , , TriState.True)
        e.Value = "hi"
    End Sub

End Class
