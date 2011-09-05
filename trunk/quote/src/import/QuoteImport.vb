Imports System.Data.SqlClient
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase

Public Class QuoteImport

    Public Function Import() As Integer

        Console.WriteLine("----- Importing -----")

        Dim row As ImportDataSet.QuoteHeaderRow = GetHeader()
        Dim header As New Model.BOM.Header
        TransferHeader(row, header)

        GetDetails(header, row.QuoteID)

        Dim BOMSaver As New BOMSaver
        Dim id As Integer = BOMSaver.Save(header)

        Console.WriteLine("QuoteID: " & id)

        Return id
    End Function

    Public Function GetHeader() As ImportDataSet.QuoteHeaderRow

        Dim adaptor As New ImportDataSetTableAdapters.QuoteHeaderTableAdapter
        Dim table As ImportDataSet.QuoteHeaderDataTable

        table = adaptor.GetDataByQuoteNumber(17616)
        Dim row As ImportDataSet.QuoteHeaderRow = table.Rows.Item(0)

        Console.WriteLine("QuoteNumber: " & row.QuoteNumber)
        Console.WriteLine("PartNumber: " & row.PartNumber)

        Return row
    End Function

    Public Sub TransferHeader(ByVal row As ImportDataSet.QuoteHeaderRow, _
                              ByVal header As Model.BOM.Header)

        Dim customer As String = row.ContactName

        Dim primary As Model.BOM.PrimaryPropeties = header.PrimaryProperties
        primary.PartNumber = row.PartNumber
        primary.RequestForQuoteNumber = row.RFQ
        primary.CommonInitials = "Import: " & row.CreatedBy

        Dim other As Model.BOM.OtherProperties = header.OtherProperties
        other.EstimatedAnnualUnits = row.EAU
        other.FormBoardCost = row.FormBoardCost
        other.LeadTimeInitial = row.LeadTimeInitial
        other.LeadTimeStandard = row.LeadTimeStandard
        other.Tooling = row.Tooling
        other.ImportedUnitCost = row.UnitPrice

        Dim comp As Model.BOM.ComputationProperties = header.ComputationProperties
        comp.CopperPrice = row.CuPrice
        comp.LaborRate = row.LaborRate
        comp.NumberOfCuts = row.Cuts
        comp.MinimumOrderQuantity = row.Minimum
    End Sub

    Public Function GetDetails(ByVal header As Model.BOM.Header, _
                               ByVal quotID As System.Guid) _
                           As Integer

        Dim adaptor As New ImportDataSetTableAdapters.QuoteDetailTableAdapter
        Dim table As ImportDataSet.QuoteDetailDataTable

        table = adaptor.GetDataByQuoteID(quotID)
        For i As Integer = 0 To table.Rows.Count - 1

            Dim detailRow As ImportDataSet.QuoteDetailRow = table.Rows.Item(i)

            Dim time As Integer = 0
            If Not detailRow.IsTimeNull Then
                time = detailRow.Time
            End If

            Dim product As New Model.Product( _
                detailRow.PartNumber, _
                "Imported", _
                detailRow.UnitCost, _
                time, _
                Model.UnitOfMeasure.BY_EACH, _
                "Imported", _
                0,
                "Imported", _
                0, _
                0)

            Dim detail As New Model.BOM.Detail(header, product)
            detail.Qty = detailRow.Qty
            header.Details.Add(detail)
        Next

    End Function

End Class
