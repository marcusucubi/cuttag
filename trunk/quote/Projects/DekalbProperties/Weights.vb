Imports System.ComponentModel
Imports System.Reflection

Imports Model.Quote
Imports Model.Template
Imports Model

Public Class Weights

    Public Shared Function CalcWeight(_Header As Common.Header) As Decimal

        Dim retValue As Decimal = 0
        For Each q As Common.Detail In _Header.Details

            If q.IsWire Then
                Dim p As DisplayableWireProperties = CType(q.QuoteDetailProperties, DisplayableWireProperties)

                Dim w As Decimal = p.PoundsPer1000Feet
                retValue += q.Qty / 3.048 * w / 1000
            End If
        Next

        Return Math.Round(retValue, 4)
    End Function

End Class
