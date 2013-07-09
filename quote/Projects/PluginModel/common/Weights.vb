Imports System.ComponentModel
Imports System.Reflection

Imports Model.Quote
Imports Model.Template
Imports Model

Namespace Common
    Public Class Weights

        Public Sub New(ByVal Header As Common.Header)
            _Header = Header
        End Sub

        Private WithEvents _Header As Header

        <CategoryAttribute("Total"), _
        DescriptionAttribute("Total Weight" + Chr(10) + "(Pounds)")> _
        Public ReadOnly Property Weight As Decimal
            Get
                Return Math.Round(CalcWeight(), 4)
            End Get
        End Property

        Private Function CalcWeight() As Decimal
            Dim retValue As Decimal = 0
            For Each q As Detail In _Header.Details

                If q.IsWire Then
                    Dim p As DisplayableWireProperties = CType(q.QuoteDetailProperties, DisplayableWireProperties)
                    Dim w As Decimal = p.Subject.PoundsPer1000Feet
                    'dd_ToDo ' Handle non-Decimetor UOM for wires 
                    retValue += q.Qty / 3.048 * w / 1000
                End If
            Next
            Return retValue
        End Function

    End Class
End Namespace
