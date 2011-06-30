Imports DCS.Quote.Model

Public Class ctrWireSummery

    Dim WithEvents _TotalLengthBinding As Binding
    Dim WithEvents _TotalLengthFeetBinding As Binding
    Dim WithEvents _WireCountBinding As Binding
    Dim WithEvents _WireTimeBinding As Binding
    Dim WithEvents _CutTimeBinding As Binding
    Dim WithEvents _NumberOfCutsBinding As Binding
    Private _QuoteHeader As QuoteHeader

    Public Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
        Set(ByVal value As QuoteHeader)
            _QuoteHeader = value
            If value IsNot Nothing Then
                _TotalLengthBinding = New Binding("Text", QuoteHeader, "TotalLength")
                Me.txtTotalLengthDm.DataBindings.Add(_TotalLengthBinding)

                _TotalLengthFeetBinding = New Binding("Text", QuoteHeader, "TotalLengthFeet")
                Me.txtTotalLengthFeet.DataBindings.Add(_TotalLengthFeetBinding)

                _WireCountBinding = New Binding("Text", QuoteHeader, "WireCount")
                Me.txtWireCount.DataBindings.Add(_WireCountBinding)

                _WireTimeBinding = New Binding("Text", QuoteHeader.QuoteComputer, "WireTime")
                Me.txtWireTime.DataBindings.Add(_WireTimeBinding)

                _CutTimeBinding = New Binding("Text", QuoteHeader.QuoteComputer, "CutTime")
                Me.txtCutTime.DataBindings.Add(_CutTimeBinding)

                _NumberOfCutsBinding = New Binding("Text", QuoteHeader.QuoteComputer, "NumberOfCuts")
                Me.txtNumberOfCuts.DataBindings.Add(__NumberOfCutsBinding)
            End If
        End Set
    End Property

End Class
