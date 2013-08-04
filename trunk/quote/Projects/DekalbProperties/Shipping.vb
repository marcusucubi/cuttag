Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Public NotInheritable Class Shipping
    
    Private Shared m_dictionary As New SortedDictionary(Of String, Decimal)()

    Private Shared m_descriptions As New ReadOnlyCollection(Of String)(New String() {String.Empty})

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Dictionary() As SortedDictionary(Of String, Decimal)
        Get
            Return m_dictionary
        End Get
    End Property

    Public Shared ReadOnly Property Descriptions() As ReadOnlyCollection(Of String)
        Get
            Return m_descriptions
        End Get
    End Property

    Public Shared Sub SetupDescriptions(descriptions As ReadOnlyCollection(Of String))
        Shipping.m_descriptions = descriptions
    End Sub

    Public Shared Function Lookup(description As String) As Decimal
    
        If m_dictionary.ContainsKey(description) Then
            Return m_dictionary(description)
        End If

        Return 0
    End Function
    
End Class
