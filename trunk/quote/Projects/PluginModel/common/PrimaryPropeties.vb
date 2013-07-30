Imports System.ComponentModel
Imports System.Reflection

Imports Model
Imports Model.Template

Namespace Common

    Public Class PrimaryProperties
        Inherits SaveableProperties

        Private _ID As Integer

        <Browsable(False)> _
        Public Property CommonCustomer As New Customer

        <Browsable(False)> _
        Public Property CommonRequestForQuoteNumber As String = ""

        <Browsable(False)> _
        Public Property CommonPartNumber As String = ""

        <Browsable(False)> _
        Public Property CommonCreatedDate As DateTime = Date.Now

        <Browsable(False)> _
        Public Property CommonLastModified As DateTime = Date.Now

        <Browsable(False)> _
        Public Property CommonInitials As String

        <Browsable(False)> _
        Public ReadOnly Property CommonId As Integer
            Get
                Return _ID
            End Get
        End Property

        Public Sub SetId(ByVal id As Integer)
            Me._ID = id
            SendEvents()
        End Sub

    End Class

End Namespace
