﻿Imports System.Data.SqlClient
Imports DCS.Quote.Model.Quote
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class QuoteSaver

    Public Function Save(ByVal q As Model.Template.Header) As Integer
        Return Save(q, False)
    End Function

    Public Function Save(ByVal q As Model.Template.Header, _
                         ByVal IsQuote As Boolean) _
                        As Integer

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim o As Model.Template.PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer
        Dim id As Integer = o.CommonID
        If IsQuote Then
            id = 0
        End If

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        If id > 0 Then
            adaptor.Update( _
                o.CustomerName, o.RequestForQuoteNumber, _
                o.PartNumber, o.CommonID, False)
            newId = id
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, _
                o.PartNumber, o.RequestForQuoteNumber, IsQuote)
            Dim cmd As OleDbCommand = New OleDbCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            newId = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
            If id = 0 And Not IsQuote Then
                q.PrimaryProperties.SetID(newId)
            End If
        End If

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveProperties(newId, 0, q.OtherProperties, True)
        CommonSaver.SaveProperties(newId, 0, q.ComputationProperties, True)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId)
        adaptor.Connection.Close()

        My.Settings.LastTamplate1 = _
            ActiveHeader.ActiveHeader.Header.ID

        Return newId
    End Function

End Class
