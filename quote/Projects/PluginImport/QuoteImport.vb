Imports System.Data.SqlClient
Imports System.Windows.Forms

Imports DB.QuoteDataBaseTableAdapters

Public Class QuoteImport
    Private _OldUnitCost As Decimal
    Private _NewUnitCost As Decimal
    Private _NumberOfWarnings As Integer

    Public Sub DoImport()

        Dim frm As New frmImport
        Dim result As DialogResult = frm.ShowDialog()
        If result = DialogResult.OK Then

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PluginOutputView.OutputPlugin.ShowOutputView()

            If frm.ImportTest Then
                ImportTest()
            ElseIf frm.ImportAll Then
                ImportAll()
            Else
                Dim id As Integer = Import(frm.QuoteNumber)
                Model.ModelEvents.NotifyTemplateCreated(id)
            End If

            System.Windows.Forms.Cursor.Current = Cursors.Default

        End If

    End Sub
    Private Sub ImportTest()

        Console.WriteLine("----- Testing ")

        Dim table As ImportDataSet.QuoteHeaderDataTable
        table = New ImportDataSetTableAdapters.QuoteHeaderTableAdapter().GetData()
        For Each row As ImportDataSet.QuoteHeaderRow In table
            ImportTest(row.QuoteNumber)
            'Application.DoEvents()
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

                Dim header As New Model.Template.Header
                header = BuildHeader(row.QuoteNumber)
                Save(header)

                'Application.DoEvents()

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

        Dim header As New Model.Template.Header
        header = BuildHeader(QuoteNumber)

    End Sub

    Private Function Import(ByVal QuoteNumber As Integer) As Integer

        Console.WriteLine("----- Importing " & QuoteNumber)

        Dim header As New Model.Template.Header
        header = BuildHeader(QuoteNumber)
        Dim id As Integer = Save(header)

        Console.WriteLine("----- Finished")

        Return id
    End Function
    Public Sub DoImportFromPartsList()
        Dim frm As New frmImportPartsList
        Dim result As DialogResult = frm.ShowDialog()
        Dim sInitials As String = "From WHC"
        If result = DialogResult.OK Then
            Dim frmInitials As New frmNewBOM
            frmInitials.pnlImportSource.Visible = True
            If frmInitials.ShowDialog = DialogResult.OK Then
                sInitials += " by " + frmInitials.Initials
            Else
                Exit Sub
            End If
            PluginOutputView.OutputPlugin.ShowOutputView()
            'frmMain.ShowOutput()
            Console.WriteLine("----- Importing " & frm.cboPartLookup.SelectedText)
            Try
                Dim dr As ImportDataSet.HQ_GetParts4LookupRow = CType(CType(frm.cboPartLookup.SelectedItem, DataRowView).Row, ImportDataSet.HQ_GetParts4LookupRow)
                Dim gPartID As Guid = frm.cboPartLookup.SelectedValue
                Dim header As Model.Template.Header = BuildHeader4PartsList(dr, sInitials, frmInitials.rbComputed.Checked)
                Dim id As Integer = Save(header)
                Console.WriteLine("----- Finished")
                Model.ModelEvents.NotifyTemplateCreated(id)
            Catch ex As Exception
                MsgBox("Problem getting Parts List from Wire Harness Control.  Please report the problem: " + ex.Message)
            End Try
        End If
    End Sub
    Private Function BuildHeader4PartsList(ByVal drPart As ImportDataSet.HQ_GetParts4LookupRow, _
            ByVal Initials As String, ByVal UseComputedParts As Boolean) As Model.Template.Header
        Dim header As New Model.Template.Header
        TransferHeader4PartsList(drPart, header, Initials)
        GetDetails4PartsList(header, drPart.PartID, UseComputedParts)
        Return header
    End Function

    Private Function BuildHeader(ByVal QuoteNumber As Integer) As Model.Template.Header

        Dim row As ImportDataSet.QuoteHeaderRow = GetHeader(QuoteNumber)
        Dim header As New Model.Template.Header
        TransferHeader(row, header)

        GetDetails(header, row.QuoteID)

        Dim comp As Model.Template.DisplayableComputationProperties = header.ComputationProperties
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

    Private Function Save(ByVal header As Model.Template.Header) As Integer

        Dim BOMSaver As New Model.IO.TemplateSaver
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
    Private Sub TransferHeader4PartsList(ByVal row As ImportDataSet.HQ_GetParts4LookupRow, _
                           ByVal header As Model.Template.Header, ByVal Initials As String)
        Dim primary As Model.Template.PrimaryPropeties = header.PrimaryProperties
        primary.Customer = Model.Template.Customer.GetByID(row.CustomerID)
        primary.PartNumber = row.Display
        primary.CommonInitials = Initials
    End Sub
    Private Sub TransferHeader(ByVal row As ImportDataSet.QuoteHeaderRow, _
                              ByVal header As Model.Template.Header)

        Dim customer As String = row.ContactName

        Dim primary As Model.Template.PrimaryPropeties = header.PrimaryProperties
        primary.PartNumber = row.PartNumber
        primary.RequestForQuoteNumber = row.RFQ
        primary.CommonInitials = "Import: " & row.CreatedBy

        Dim other As Model.Template.DefaultOtherProperties = header.OtherProperties
        other.EstimatedAnnualUnits = row.EAU
        other.FormBoardCost = row.FormBoardCost
        other.LeadTimeInitial = row.LeadTimeInitial
        other.LeadTimeStandard = row.LeadTimeStandard
        other.Tooling = row.Tooling
        other.SetImportedUnitCost(row.UnitPrice)
        other.SetImportedCuWeight(row.CuWeight)
        other.SetImportedLaborMinutes(row.LaborMinutes)
        _OldUnitCost = row.UnitPrice

        Dim comp As Model.Template.DisplayableComputationProperties = header.ComputationProperties
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

        Dim note As Model.Template.NoteProperties = header.NoteProperties
        note.Note = "Imported from " & row.QuoteNumber

    End Sub
    Private Function GetDetails4PartsList(ByVal header As Model.Template.Header, _
                               ByVal PartID As System.Guid, ByVal UseComputedParts As Boolean) _
                           As Integer
        Dim adaptor As New ImportDataSetTableAdapters.HQ_GetPartsListTableAdapter
        Dim errors As New List(Of String)
        Dim dsQuoteDataBase As New DB.QuoteDataBase
        Dim daWire As New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter
        Dim daComp As New DB.QuoteDataBaseTableAdapters.WireComponentSourceTableAdapter
        Dim daGage As New GageTableAdapter
        Dim daUOM As New DB.QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter
        With dsQuoteDataBase
            daWire.Fill(.WireSource)
            daComp.Fill(.WireComponentSource)
            daUOM.Fill(._UnitOfMeasure)
            daGage.Fill(.Gage)
        End With
        Dim ignore As String = ""
        Dim pProduct As Model.Product = Nothing
        For Each detailRow As ImportDataSet.HQ_GetPartsListRow In adaptor.GetData(PartID, UseComputedParts)
            If IsNothing(dsQuoteDataBase.WireSource.FindByWireSourceID(detailRow.SourceID)) Then
                If IsNothing(dsQuoteDataBase.WireComponentSource.FindByWireComponentSourceID(detailRow.SourceID)) _
                    OrElse dsQuoteDataBase.WireComponentSource.FindByWireComponentSourceID(detailRow.SourceID).PartNumber = "MISSING" Then
                    Console.WriteLine("   Failure finding component source for " + detailRow.PartNumber)
                    pProduct = New Model.Product(detailRow.PartNumber, 0, "", False, Nothing, Nothing)
                Else 'It is a component and wirecomponentsource.partnumber <> "MISSING"
                    pProduct = New Model.Product(detailRow.SourceID, False, dsQuoteDataBase)
                End If
            Else 'It is a wire
                If dsQuoteDataBase.WireSource.FindByWireSourceID(detailRow.SourceID).PartNumber = "MISSING" Then
                    Console.WriteLine("   Failure finding source for " + detailRow.PartNumber)
                    pProduct = New Model.Product(detailRow.PartNumber, 0, "", False, Nothing, Nothing)
                Else 'wiresource.partnumber <> "MISSING"
                    pProduct = New Model.Product(detailRow.SourceID, True, dsQuoteDataBase)
                End If
                pProduct.CopperWeightPer1000Ft = _
                    daWire.GetWirePoundsPer1000Ft(detailRow.SourceID, ignore)
                'detail.UpdateComponentProperties(pProduct)
                If pProduct.CopperWeightPer1000Ft = 0 Then
                    Console.WriteLine("   Zero weight for " + detailRow.PartNumber)
                End If
            End If
            Dim detail As New Model.Template.Detail(header, pProduct)
            detail.Qty = detailRow.Qty
            detail.SourceID = detailRow.SourceID
            detail.UpdateComponentProperties(pProduct)
            header.Details.Add(detail)
        Next
        Return 0
    End Function
    Private Function GetDetails(ByVal header As Model.Template.Header, _
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
            Dim detail As New Model.Template.Detail(header, product)
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
        Return 0
    End Function
    ''' <summary>
    ''' Looks up the wire component and assign UOM
    ''' </summary>
    Private Sub LookupWireComponentPart(ByVal detailRow As ImportDataSet.QuoteDetailRow, _
                                        ByVal detail As Model.Template.Detail, _
                                        ByVal product As Model.Product)
        Dim adaptor As New DB.QuoteDataBaseTableAdapters.WireComponentSourceTableAdapter
        Dim table As DB.QuoteDataBase.WireComponentSourceDataTable
        table = adaptor.GetDataByPartNumber(detailRow.PartNumber)
        If (table.Count = 0) Then
            Dim partNum As String = detailRow.PartNumber
            partNum = partNum.Replace("-", "").ToUpper()
            table = adaptor.GetDataByCleanedPartNumber(partNum)
            If (table.Count = 0) Then
                Dim adtKeyWord As New DB.QuoteDataBaseTableAdapters.WireComponentSourceKeyWordTableAdapter
                If adtKeyWord.GetSourceIDByCleanedKeyWord(partNum).HasValue Then
                    Dim gSourceID As Guid = adtKeyWord.GetSourceIDByCleanedKeyWord(partNum)
                    table = adaptor.GetDataBySourceID(gSourceID)
                End If
            End If
        End If
        If (table.Count > 0) Then
            Dim sUOM As String = ""
            Dim row As DB.QuoteDataBase.WireComponentSourceRow = table.Rows(0)
            detail.SourceID = row.WireComponentSourceID
            Dim lookup As New DB.QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter
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
                               ByVal detail As Model.Template.Detail, _
                               ByVal product As Model.Product, _
                               ByVal errors As List(Of String))
        Dim adaptor As New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter
        Dim table As DB.QuoteDataBase.WireSourceDataTable
        table = adaptor.GetDataByPartNumber(detailRow.PartNumber)
        If (table.Count = 0) Then
            Dim partNum As String = detailRow.PartNumber
            partNum = partNum.Replace("-", "").ToUpper()
            table = adaptor.GetDataByCleanedPartNumber(partNum)
            If (table.Count = 0) Then
                Dim adtKeyWord As New DB.QuoteDataBaseTableAdapters.WireSourceKeyWordTableAdapter
                If adtKeyWord.GetSourceIDByCleanedKeyWord(partNum).HasValue Then
                    Dim gSourceID As Guid = adtKeyWord.GetSourceIDByCleanedKeyWord(partNum)
                    table = adaptor.GetDataBySourceID(gSourceID)
                End If
            End If
        End If
        If (table.Count > 0) Then
            Dim row As DB.QuoteDataBase.WireSourceRow = table.Rows(0)
            detail.SourceID = row.WireSourceID
            Dim lookup As New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter
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
