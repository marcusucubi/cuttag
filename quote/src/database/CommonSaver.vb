﻿Imports System.Data.SqlClient
Imports DCS.Quote.Common
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports System.ComponentModel

Public Class CommonSaver

    Public Shared Sub SaveProperties(ByVal id As Integer, _
                                     ByVal childId As Integer, _
                                     ByVal obj As Object, _
                                     ByVal SaveAll As Boolean)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            If SaveAll = False Then
                If Not p.CanWrite Then
                    Continue For
                End If
            End If

            Dim cat As String = "Misc"
            With cat
                Dim oa As CategoryAttribute() = p.GetCustomAttributes(GetType(CategoryAttribute), False)
                If oa.Length > 0 Then
                    cat = oa(0).Category
                End If
            End With

            Dim desc As String = ""
            With desc
                Dim oa As DescriptionAttribute() = p.GetCustomAttributes(GetType(DescriptionAttribute), False)
                If oa.Length > 0 Then
                    desc = oa(0).Description
                End If
            End With

            Dim browsable As Boolean = True
            With browsable
                Dim oa As BrowsableAttribute() = p.GetCustomAttributes(GetType(BrowsableAttribute), False)
                If oa.Length > 0 Then
                    browsable = oa(0).Browsable
                End If
            End With
            If Not browsable Then
                Continue For
            End If

            Dim s As String = Nothing
            Dim i As Integer = Nothing
            Dim d As Decimal = Nothing
            Dim o As Object = p.GetValue(obj, Nothing)

            If TypeOf o Is Integer Then
                i = CInt(o)
                adaptor.Insert(id, childId, p.Name, Nothing, Nothing, i, cat, desc)
            End If
            If TypeOf o Is String Then
                s = CStr(o)
                adaptor.Insert(id, childId, p.Name, s, Nothing, Nothing, cat, desc)
            End If
            If TypeOf o Is Decimal Then
                d = CDec(o)
                adaptor.Insert(id, childId, p.Name, Nothing, d, Nothing, cat, desc)
            End If
            If TypeOf o Is DateTime Then
                Dim dt As DateTime = CDate(o)
                adaptor.Insert(id, childId, p.Name, dt.ToShortDateString, Nothing, Nothing, cat, desc)
            End If
        Next
    End Sub

    Public Shared Sub DeleteComponents(ByVal id As Integer)
        Dim adaptor As New _QuoteDetailTableAdapter
        adaptor.Delete(id)
    End Sub

    Public Shared Sub DeleteProperties(ByVal QuoteID As Integer)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter
        Dim table As _QuotePropertiesDataTable = adaptor.GetDataByQuoteID(QuoteID)
        For Each row As _QuotePropertiesRow In table.Rows
            adaptor.Delete(row.ID)
        Next
    End Sub

    Public Shared Sub SaveComponents(ByVal q As Model.Template.Header, _
                                     ByVal quoteId As Integer, _
                                     ByVal SaveAll As Boolean)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim oldId As Integer = q.PrimaryProperties.CommonID
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(oldId)
        For Each detail As Common.Detail In q.Details
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(quoteId, detail.Qty, detail.Product.Code)
            Dim cmd As OleDbCommand = New OleDbCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            Dim id As Integer = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()

            SaveProperties(quoteId, id, _
               detail.QuoteDetailProperties, SaveAll)
            detail.ClearDirty()
        Next

    End Sub


End Class
