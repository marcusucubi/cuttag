Imports System.Data.SqlClient
Imports DCS.Quote.QuoteDataBaseTableAdapters
Imports DCS.Quote.QuoteDataBase

Public Class QuoteImport

    Private _OldUnitCost As Decimal
    Private _NewUnitCost As Decimal
    Private _NumberOfWarnings As Integer

    Public Sub DoImport()

        Dim frm As New frmImport
        Dim result As DialogResult = frm.ShowDialog()
        If result = DialogResult.OK Then
            frmMain.ShowOutput()

            If frm.ImportTest Then
                ImportTest()
            ElseIf frm.ImportAll Then
                ImportAll()
            Else
                Dim id As Integer = Import(frm.QuoteNumber)
                frmMain.LoadTemplate(id)
            End If

        End If

    End Sub

    Private Sub ImportTest()

        Console.WriteLine("----- Testing ")

        Dim table As ImportDataSet.QuoteHeaderDataTable
        table = New ImportDataSetTableAdapters.QuoteHeaderTableAdapter().GetData()
        For Each row As ImportDataSet.QuoteHeaderRow In table
            ImportTest(row.QuoteNumber)
            Application.DoEvents()
        Next

        Console.WriteLine("----- Finished")
        Console.WriteLine("----- Warnings: " & _NumberOfWarnings)

    End Sub

    Private Sub ImportAll()

        Console.WriteLine("----- Import All ")

        Dim table As ImportDataSet.QuoteHeaderDataTable
        Dim adaptor As New ImportDataSetTableAdapters.QuoteHeaderTableAdapter()
        table = adaptor.GetData()
        For Each row As ImportDataSet.QuoteHeaderRow In table

            If row.Processed = 0 Then

                Dim header As New Model.BOM.Header
                header = BuildHeader(row.QuoteNumber)
                Save(header)

                Application.DoEvents()

                adaptor.Connection.Open()
                adaptor.Transaction = adaptor.Connection.BeginTransaction()
                row.Processed = row.Processed + 1
                adaptor.Update(row.Processed, row.QuoteID)
                adaptor.Transaction.Commit()
                adaptor.Connection.Close()
            End If

        Next

        Console.WriteLine("----- Finished")
        Console.WriteLine("----- Warnings: " & _NumberOfWarnings)

    End Sub

    Private Sub ImportTest(ByVal QuoteNumber As Integer)

        Dim header As New Model.BOM.Header
        header = BuildHeader(QuoteNumber)

    End Sub

    Private Function Import(ByVal QuoteNumber As Integer) As Integer

        Console.WriteLine("----- Importing " & QuoteNumber)

        Dim header As New Model.BOM.Header
        header = BuildHeader(QuoteNumber)
        Dim id As Integer = Save(header)

        Console.WriteLine("----- Finished")

        Return id
    End Function

    Private Function BuildHeader(ByVal QuoteNumber As Integer) As Model.BOM.Header

        Dim row As ImportDataSet.QuoteHeaderRow = GetHeader(QuoteNumber)
        Dim header As New Model.BOM.Header
        TransferHeader(row, header)

        GetDetails(header, row.QuoteID)

        Dim comp As Model.BOM.ComputationProperties = header.ComputationProperties
        _NewUnitCost = comp.AdjustedTotalUnitCost

        Console.WriteLine("    Old UnitCost: " & Math.Round(_OldUnitCost, 2))
        Console.WriteLine("    New UnitCost: " & Math.Round(_NewUnitCost, 2))

        Dim percent As Decimal = Math.Round((Math.Abs(_OldUnitCost - _NewUnitCost) / _OldUnitCost) * 100)
        If (percent > 2) Then
            _NumberOfWarnings = _NumberOfWarnings + 1
            Console.WriteLine("    *** Warning *** Difference: " & percent & "%")
        End If

        Return header
    End Function

    Private Function Save(ByVal header As Model.BOM.Header) As Integer

        Dim BOMSaver As New BOMSaver
        Dim id As Integer = BOMSaver.Save(header)

        Console.WriteLine("    New QuoteNumber: " & id)

        Return id
    End Function

    Private Function GetHeader(ByVal QuoteNumber As Integer) As ImportDataSet.QuoteHeaderRow

        Dim adaptor As New ImportDataSetTableAdapters.QuoteHeaderTableAdapter
        Dim table As ImportDataSet.QuoteHeaderDataTable

        table = adaptor.GetDataByQuoteNumber(QuoteNumber)
        Dim row As ImportDataSet.QuoteHeaderRow = table.Rows.Item(0)

        Console.WriteLine("    QuoteNumber: " & row.QuoteNumber)

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
        other.SetImportedUnitCost(row.UnitPrice)
        other.SetImportedCuWeight(row.CuWeight)
        other.SetImportedLaborMinutes(row.LaborMinutes)
        _OldUnitCost = row.UnitPrice

        Dim comp As Model.BOM.ComputationProperties = header.ComputationProperties
        comp.CopperPrice = row.CuPrice
        comp.LaborRate = row.LaborRate
        comp.NumberOfCuts = row.Cuts
        comp.MinimumOrderQuantity = row.Minimum
        comp.ShippingContainer = row.BoxSize
        comp.TimeMultiplier = row.TimeMultiplier
        comp.PercentCopperScrap = (row.CuWeightMultiplier - 1) * 100
        comp.WireSetupTime = row.CutTime
        comp.WireMachineTime = row.WireTime
        If Not row.IsComponentSetupTimeNull Then
            comp.ComponentSetupTime = row.ComponentSetupTime
        End If
        If Not row.IsShippingNull Then
            comp.ShippingCost = row.Shipping
        End If
        If Not row.IsNumberOfTwistsNull Then
            comp.NumberOfTwistedPairs = row.NumberOfTwists
        End If
        If row.IsFinalMarkupNull Then
            Console.WriteLine("    Warning: FinalMarkup is null")
        Else
            comp.ManufacturingMarkup = row.FinalMarkup
        End If
        If row.IsMaterialMarkupNull Then
            Console.WriteLine("    Warning: MaterialMarkUp is null")
        Else
            comp.MaterialMarkUp = row.MaterialMarkup
        End If

        Dim note As Model.BOM.NoteProperties = header.NoteProperties
        note.Note = "Imported from " & row.QuoteNumber

    End Sub
    Private Function GetDetails(ByVal header As Model.BOM.Header, _
                               ByVal quotID As System.Guid) _
                           As Integer
        Dim adaptor As New ImportDataSetTableAdapters.QuoteDetailTableAdapter
        Dim table As ImportDataSet.QuoteDetailDataTable
        Dim errors As New List(Of String)
        table = adaptor.GetDataByQuoteID(quotID)
        For i As Integer = 0 To table.Rows.Count - 1
            Dim detailRow As ImportDataSet.QuoteDetailRow = table.Rows.Item(i)
            Dim time As Decimal = 0
            If Not detailRow.IsTimeNull Then
                time = detailRow.Time
            End If
            'detailRow.IsWire
            Dim gage As String = ""
            If (Not detailRow.IsGageNull) Then
                gage = detailRow.Gage.Trim()
            End If
            Dim product As New Model.Product( _
                detailRow.PartNumber, _
                gage, _
                detailRow.UnitCost, _
                time, _
                detailRow.IsWire, _
                "", _
                0,
                "", _
                0, _
                0)
            Dim detail As New Model.BOM.Detail(header, product)
            If product.IsWire Then
                LookupWirePart(detailRow, detail, product, errors)
                detail.UOM = "Decimeter"
            Else
                LookupWireComponentPart(detailRow, detail, product)
            End If
            detail.Qty = detailRow.Qty
            header.Details.Add(detail)
        Next
        PrintLookUpErrors(errors)
    End Function
    ''' <summary>
    ''' Looks up the wire component and assign UOM
    ''' </summary>
    Private Sub LookupWireComponentPart(ByVal detailRow As ImportDataSet.QuoteDetailRow, _
                               ByVal detail As Model.BOM.Detail, _
                               ByVal product As Model.Product)
        Dim adaptor As New QuoteDataBaseTableAdapters.WireComponentSourceTableAdapter
        Dim table As QuoteDataBase.WireComponentSourceDataTable
        table = adaptor.GetDataByPartNumber(detailRow.PartNumber)
        If (table.Count = 0) Then
            Dim partNum As String = detailRow.PartNumber
            partNum = partNum.Replace("-", "").ToUpper()
            table = adaptor.GetDataByCleanedPartNumber(partNum)
            If (table.Count = 0) Then
                Dim adtKeyWord As New QuoteDataBaseTableAdapters.WireComponentSourceKeyWordTableAdapter
                If adtKeyWord.GetSourceIDByCleanedKeyWord(partNum).HasValue Then
                    Dim gSourceID As Guid = adtKeyWord.GetSourceIDByCleanedKeyWord(partNum)
                    table = adaptor.GetDataBySourceID(gSourceID)
                End If
            End If
        End If
        If (table.Count > 0) Then
            Dim sUOM As String = ""
            Dim row As QuoteDataBase.WireComponentSourceRow = table.Rows(0)
            detail.SourceID = row.WireComponentSourceID
            Dim lookup As New QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter
            sUOM = lookup.GetUOM_NameByID(row.UnitOfMeasureID)
            If IsNothing(sUOM) Then
                Console.WriteLine("   UOM is Null " + detailRow.PartNumber)
            Else
                detail.UOM = Trim(sUOM)
            End If
        Else
            Console.WriteLine("   Component not Found " + detailRow.PartNumber)
        End If
    End Sub
    ''' <summary>
    ''' Looks up the wire and assigns the weight
    ''' </summary>
    Private Sub LookupWirePart(ByVal detailRow As ImportDataSet.QuoteDetailRow, _
                               ByVal detail As Model.BOM.Detail, _
                               ByVal product As Model.Product, _
                               ByVal errors As List(Of String))
        Dim adaptor As New QuoteDataBaseTableAdapters.WireSourceTableAdapter
        Dim table As QuoteDataBase.WireSourceDataTable
        table = adaptor.GetDataByPartNumber(detailRow.PartNumber)
        If (table.Count = 0) Then
            Dim partNum As String = detailRow.PartNumber
            partNum = partNum.Replace("-", "").ToUpper()
            table = adaptor.GetDataByCleanedPartNumber(partNum)
            If (table.Count = 0) Then
                Dim adtKeyWord As New QuoteDataBaseTableAdapters.WireSourceKeyWordTableAdapter
                If adtKeyWord.GetSourceIDByCleanedKeyWord(partNum).HasValue Then
                    Dim gSourceID As Guid = adtKeyWord.GetSourceIDByCleanedKeyWord(partNum)
                    table = adaptor.GetDataBySourceID(gSourceID)
                End If
            End If
        End If
        If (table.Count > 0) Then
            Dim row As QuoteDataBase.WireSourceRow = table.Rows(0)
            detail.SourceID = row.WireSourceID
            Dim lookup As New QuoteDataBaseTableAdapters.WireSourceTableAdapter
            Dim ignore As String = ""
            product.CopperWeightPer1000Ft = _
                lookup.GetWirePoundsPer1000Ft(row.WireSourceID, ignore)
            detail.UpdateComponentProperties(product)
            If product.CopperWeightPer1000Ft = 0 Then
                Console.WriteLine("   Zero weight for " + detailRow.PartNumber)
            End If
        Else
            errors.Add(detailRow.PartNumber)
        End If
    End Sub
    Private Sub PrintLookUpErrors(ByVal errors As List(Of String))
        If errors.Count > 0 Then
            Dim msg As String = "    Warning: "
            msg += String.Format("{0}", errors.Count)
            msg += " parts not found "
            msg += " "
            For Each s As String In errors
                msg += "[" + s + "]  "
            Next
            Console.WriteLine(msg)
        End If
    End Sub
End Class
