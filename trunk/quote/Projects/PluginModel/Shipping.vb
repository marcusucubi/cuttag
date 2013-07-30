Imports System.Collections.ObjectModel

Public NotInheritable Class Shipping
    
    Private Shared _Dictionary As New SortedDictionary(Of String, Decimal)
    
    Private Shared _Descriptions As New ReadOnlyCollection(Of String)( {""} )
        
    Private Sub New()
        
    End Sub
    
    Public Shared ReadOnly Property Dictionary As SortedDictionary(Of String, Decimal)
        Get
            Return _Dictionary
        End Get
    End Property

    Public Shared ReadOnly Property Descriptions As ReadOnlyCollection(Of String)
        Get
            Return _Descriptions
        End Get
    End Property
    
    Public Shared Sub SetupDescriptions(descriptions As ReadOnlyCollection(Of String))
        _Descriptions = descriptions
    End Sub

    Public Shared Function Lookup(ByVal description As String) As Decimal
    
        If (_Dictionary.ContainsKey(description)) Then
            Return _Dictionary(description)
        End If
        
        Return 0
        
    End Function

End Class
