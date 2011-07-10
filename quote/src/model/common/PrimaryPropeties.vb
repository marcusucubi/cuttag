Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Common

    Public MustInherit Class PrimaryPropeties
        Inherits SaveableProperties

        <Browsable(False)> _
        Public Property CommonCustomerName As String = ""

        <Browsable(False)> _
        Public Property CommonRequestForQuoteNumber As String = ""

        <Browsable(False)> _
        Public Property CommonPartNumber As String = ""

        <Browsable(False)> _
        Public Property CommonQuoteNumber As Integer

        Public Overridable Sub SetID(ByVal id As Integer)

        End Sub

    End Class

End Namespace
