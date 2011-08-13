Imports System.Data.SqlClient
Imports DCS.Quote
Imports DCS.Quote.Model.BOM
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase
Imports DCS.Quote.Model

Public Class BOMLoader

    Public Function Load(ByVal id As Long) As Header

        frmMain.frmMain.UseWaitCursor = True
        My.Application.DoEvents()

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Header()

        adaptor.FillByByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Header(row.ID)
            Dim customer As String = row.CustomerName
            Dim rfq As String = ""
            If Not row.IsRequestForQuoteNumberNull Then
                rfq = row.RequestForQuoteNumber
            End If
            Dim part As String = ""
            If Not row.IsPartNumberNull Then
                part = row.PartNumber
            End If
            Dim Initials As String = ""
            If Not row.IsInitialsNull Then
                Initials = row.Initials
            End If
            Dim createdDate As DateTime
            If Not row.IsCreatedDateNull Then
                createdDate = row.CreatedDate
            End If
            Dim lastModDate As DateTime
            If Not row.IsLastModifedDateNull Then
                lastModDate = row.LastModifedDate
            End If
            q.PrimaryProperties.CommonCustomerName = customer
            q.PrimaryProperties.CommonPartNumber = part
            q.PrimaryProperties.CommonRequestForQuoteNumber = rfq
            q.PrimaryProperties.CommonCreatedDate = createdDate
            q.PrimaryProperties.CommonLastModified = lastModDate
            q.PrimaryProperties.CommonInitials = Initials

            CommonLoader.LoadComputationProperties(id, q.ComputationProperties)
            CommonLoader.LoadOtherProperties(id, q.OtherProperties)
            CommonLoader.LoadNoteProperties(id, q.NoteProperties)
            CommonLoader.LoadComponents(q)
        End If

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.NoteProperties.ClearDirty()
        q.ClearDirty()

        frmMain.frmMain.UseWaitCursor = False

        Return q
    End Function

End Class
