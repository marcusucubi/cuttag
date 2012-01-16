Imports System.ComponentModel

Public Class CustomerConverter
    Inherits ExpandableObjectConverter

    Private values As ArrayList

    Public Sub New()
        Dim adaptor As New QuoteDataBaseTableAdapters.CustomerTableAdapter()
        Dim table As QuoteDataBase.CustomerDataTable
        table = adaptor.GetData()
        values = New ArrayList()
        For Each row As QuoteDataBase.CustomerRow In table.Rows
            If row.CustomerID > 0 Then
                Dim c As New Customer
                c.SetID(row.CustomerID)
                c.SetName(row.CustomerName.Trim())
                values.Add(c)
            End If
        Next
    End Sub

    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function GetStandardValuesExclusive(context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Dim svc As New StandardValuesCollection(values)
        Return svc
    End Function

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If sourceType Is GetType(String) Then
            Return True
        Else
            Return MyBase.CanConvertFrom(context, sourceType)
        End If
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If value.GetType() Is GetType(String) Then
            Dim parts As String()
            parts = value.ToString().Split(New Char() {"@"c})

            Dim customer As New Customer
            If parts.Length > 1 Then
                Dim id As Integer
                Integer.TryParse(parts(0), id)
                Dim name As String = parts(1)

                customer.SetID(id)
                customer.SetName(name)
            Else
                customer.SetName(value.ToString())
            End If

            Return customer
        Else
            Return MyBase.ConvertFrom(context, culture, value)
        End If
    End Function

End Class
