Imports System.Collections.ObjectModel

Public Class Shipping

    Public Shared Property Dictionary As New SortedDictionary(Of String, Decimal)

    Public Shared Property Descriptions As New ReadOnlyCollection(Of String)( {""} )

    Public Shared Function Lookup(ByVal Description As String) As Decimal
    
        If (_Dictionary.ContainsKey(Description)) Then
            Return _Dictionary(Description)
        End If
        
        Return 0
        
    End Function

End Class
