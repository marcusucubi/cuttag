Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model.Quote

    Public Class ComponentProperties
        Inherits Common.ComponentProperties

        <DisplayName("Total Component Time")>
        Public Overloads Property TotalComponentTime() As Integer

        <DisplayName("Component Time")>
        Public Overloads Property ComponentTime() As Integer

        Public Overloads Property Quantity() As Integer

    End Class

End Namespace
