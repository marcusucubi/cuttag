Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions

Imports DB.QuoteDataBaseTableAdapters
Imports DB.QuoteDataBase
Imports DB

Imports Model.Template
Imports Model
Imports System.Windows.Forms

Public Class TemplateLoader

    Public Function Load(ByVal id As Long) As Header

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Header()

        adaptor.FillByByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Header(row.id)

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

            Dim customerObj As Customer = LookupCustomer(row)

            q.PrimaryProperties.CommonCustomer = customerObj
            q.PrimaryProperties.CommonPartNumber = part
            q.PrimaryProperties.CommonRequestForQuoteNumber = rfq
            q.PrimaryProperties.CommonCreatedDate = createdDate
            q.PrimaryProperties.CommonLastModified = lastModDate
            q.PrimaryProperties.CommonInitials = Initials

            CommonLoader.LoadComputationProperties(id, q.ComputationProperties)
            CommonLoader.LoadOtherProperties(id, q.OtherProperties)
            CommonLoader.LoadNoteProperties(id, q.NoteProperties)
            LoadComponents(q)
        End If

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.NoteProperties.ClearDirty()
        q.ClearDirty()

        System.Windows.Forms.Cursor.Current = Cursors.Default

        Return q
    End Function

    Public Function LookupCustomer(row As QuoteDataBase._QuoteRow) As Customer

        Dim customer As String = ""
        If Not row.IsCustomerNameNull Then
            customer = row.CustomerName
        End If
        Dim customerID As Integer

        If row.IsCustomerIDNull Then
            If Not row.IsCustomerNameNull Then
                Dim temp As Customer
                temp = Model.Template.Customer.GetByName(row.CustomerName)
                If (Not temp Is Nothing) Then
                    customerID = temp.ID
                End If
            End If
        Else
            customerID = row.CustomerID
        End If

        Dim customerObj As New Customer
        customerObj.SetName(customer)
        customerObj.SetID(customerID)

        Return customerObj
    End Function


    Public Shared Sub LoadComponents(ByVal q As Model.Common.Header)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim id As Integer = q.PrimaryProperties.CommonID
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows

            Dim temp As New TempObj
            CommonLoader.LoadProperties(id, row.id, temp)

            Dim product As New Model.Product( _
              row.ProductCode, _
              temp.Gage, _
              0, _
              0, _
              row.IsWire, _
              "", _
              0,
              "", _
              0, _
              0)
            Dim detail As Model.Common.Detail = q.NewDetail(product)
            detail.Qty = row.Qty
            With detail
                .IsWire = row.IsWire
                If Not row.IsSourceIDNull Then .SourceID = row.SourceID
                .SequenceNumber = row.SequenceNumber
            End With
            CommonLoader.LoadProperties(id, row.id, detail.QuoteDetailProperties)
        Next
    End Sub

    Public Class TempObj
        Public Property UnitOfMeasure As String
        Public Property Gage As String = ""
    End Class

End Class
