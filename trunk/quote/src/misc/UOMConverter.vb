Imports System.ComponentModel

Public Class UOMConverter : Inherits StringConverter

    Public Overloads Overrides Function _
        GetStandardValues(ByVal context As  _
        System.ComponentModel.ITypeDescriptorContext) _
        As TypeConverter.StandardValuesCollection

        Dim l As New List(Of String)

        Dim table As QuoteDataBase._UnitOfMeasureDataTable
        table = New QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter().GetData()
        For Each row As QuoteDataBase._UnitOfMeasureRow In table.Rows
            l.AddRange(New String() {row.Name })
        Next
        l.Sort()

        Return New StandardValuesCollection(l)
    End Function

    Public Overloads Overrides Function _
        GetStandardValuesExclusive(ByVal context _
        As System.ComponentModel.ITypeDescriptorContext) _
        As Boolean

        Return False
    End Function

    Public Overrides Function _
        GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean

        Return True
    End Function

End Class
