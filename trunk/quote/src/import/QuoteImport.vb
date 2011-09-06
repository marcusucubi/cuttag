﻿Imports System.Data.SqlClient
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase

Public Class QuoteImport

    Private _OldUnitCost As Decimal
    Private _NewUnitCost As Decimal

    Public Function Import(ByVal QuoteNumber As Integer) As Integer

        Console.WriteLine("----- Importing " & QuoteNumber)

        Dim row As ImportDataSet.QuoteHeaderRow = GetHeader(QuoteNumber)
        Dim header As New Model.BOM.Header
        TransferHeader(row, header)

        GetDetails(header, row.QuoteID)

        Dim comp As Model.BOM.ComputationProperties = header.ComputationProperties
        _NewUnitCost = comp.TotalUnitCost

        Dim BOMSaver As New BOMSaver
        Dim id As Integer = BOMSaver.Save(header)

        Console.WriteLine("    New Quote Number: " & id)

        Console.WriteLine("    Old: " & Math.Round(_OldUnitCost, 2))
        Console.WriteLine("    New: " & Math.Round(_NewUnitCost, 2))
        Dim percent As Decimal = Math.Round((Math.Abs(_OldUnitCost - _NewUnitCost) / _OldUnitCost) * 100)
        Console.WriteLine("    Difference: " & percent & "%")
        If (percent > 2) Then
            Console.WriteLine("    *** Warning ***")
        End If
        Console.WriteLine("----- Finished")

        Return id
    End Function

    Private Function GetHeader(ByVal QuoteNumber As Integer) As ImportDataSet.QuoteHeaderRow

        Dim adaptor As New ImportDataSetTableAdapters.QuoteHeaderTableAdapter
        Dim table As ImportDataSet.QuoteHeaderDataTable

        table = adaptor.GetDataByQuoteNumber(QuoteNumber)
        Dim row As ImportDataSet.QuoteHeaderRow = table.Rows.Item(0)

        Console.WriteLine("    QuoteNumber: " & row.QuoteNumber)
        Console.WriteLine("    PartNumber: " & row.PartNumber)

        Return row
    End Function

    Private Sub TransferHeader(ByVal row As ImportDataSet.QuoteHeaderRow, _
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
        _OldUnitCost = row.UnitPrice

        Dim comp As Model.BOM.ComputationProperties = header.ComputationProperties
        comp.CopperPrice = row.CuPrice
        comp.LaborRate = row.LaborRate
        comp.NumberOfCuts = row.Cuts
        comp.MinimumOrderQuantity = row.Minimum
        comp.ShippingContainer = row.BoxSize
        comp.TimeMultiplier = row.TimeMultiplier
        comp.PercentCopperScrap = (row.CuWeightMultiplier - 1) * 100

        Dim note As Model.BOM.NoteProperties = header.NoteProperties
        note.Note = "Imported from " & row.QuoteNumber

    End Sub

    Private Function GetDetails(ByVal header As Model.BOM.Header, _
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

            Dim unit As Model.UnitOfMeasure
            If (detailRow.IsWire) Then
                unit = Model.UnitOfMeasure.BY_LENGTH
            Else
                unit = Model.UnitOfMeasure.BY_EACH
            End If
            Dim gage As String = ""
            If (Not detailRow.IsGageNull) Then
                gage = detailRow.Gage.Trim()
            End If

            Dim product As New Model.Product( _
                detailRow.PartNumber, _
                gage, _
                detailRow.UnitCost, _
                time, _
                unit, _
                "", _
                0,
                "", _
                0, _
                0)

            Dim detail As New Model.BOM.Detail(header, product)
            detail.Qty = detailRow.Qty
            header.Details.Add(detail)
        Next

    End Function

End Class