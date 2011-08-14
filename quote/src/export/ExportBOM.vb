Public Class ExportBOM

    Private _Writer As ExcelBOMWriter

    Public Function Export(ByVal header As Common.Header) As String

        _Writer = New ExcelBOMWriter("Summary")

        _Writer.StartIndent("PrimaryProperties")
        ExportObject(header.PrimaryProperties)
        _Writer.EndIndent()

        _Writer.StartIndent("OtherProperties")
        ExportObject(header.OtherProperties)
        _Writer.EndIndent()

        _Writer.StartIndent("CustomProperties")
        ExportObject(ActiveCustomProperties.ActiveCustomProperties.Properties)
        _Writer.EndIndent()

        _Writer.StartIndent("ComputationProperties")
        ExportObject(header.ComputationProperties)
        _Writer.EndIndent()

        _Writer = New ExcelBOMWriter(_Writer.Path, _Writer.Workbook, "Wires")
        Dim index As Integer
        For Each detail As Common.Detail In header.Details
            If (detail.Product.UnitOfMeasure = Model.UnitOfMeasure.BY_LENGTH) Then
                If index = 0 Then
                    PrintWireColumns(detail)
                    PrintWireColumns(detail.QuoteDetailProperties)
                    _Writer.IncrementIndex()
                End If
                PrintWireValue(detail)
                PrintWireValue(detail.QuoteDetailProperties)
                _Writer.IncrementIndex()
                index = index + 1
            End If
        Next

        index = 0
        _Writer = New ExcelBOMWriter(_Writer.Path, _Writer.Workbook, "Components")
        For Each detail As Common.Detail In header.Details
            If (detail.Product.UnitOfMeasure = Model.UnitOfMeasure.BY_EACH) Then
                If index = 0 Then
                    PrintWireColumns(detail)
                    PrintWireColumns(detail.QuoteDetailProperties)
                    _Writer.IncrementIndex()
                End If
                PrintWireValue(detail)
                PrintWireValue(detail.QuoteDetailProperties)
                _Writer.IncrementIndex()
                index = index + 1
            End If
        Next

        Dim w As NPOI.SS.UserModel.Workbook = _Writer.Workbook
        Dim sheet As NPOI.SS.UserModel.Sheet = w.CreateSheet("Notes")
        Dim cell As NPOI.SS.UserModel.Cell = sheet.CreateRow(0).CreateCell(0)
        Dim notes As Model.BOM.NoteProperties = header.NoteProperties
        cell.SetCellValue(notes.Note)
        cell.Sheet.SetColumnWidth(0, 1000 * 1000)
        cell.Row.Height = 1000 * 5
        cell.CellStyle.WrapText = True
        cell.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.TOP

        _Writer.Open()
        Return _Writer.Path
    End Function

    Private Sub PrintWireColumns(ByVal Detail As Object)
        _Writer.SaveSettings()
        _Writer.MoveRight = True
        _Writer.MoveDown = False
        _Writer.UpdateWithName = True
        _Writer.UpdateWithValue = False
        ExportObject(Detail)
        _Writer.RestoreSettings()
    End Sub

    Private Sub PrintWireValue(ByVal Detail As Object)
        _Writer.SaveSettings()
        _Writer.MoveRight = True
        _Writer.MoveDown = False
        _Writer.UpdateWithName = False
        _Writer.UpdateWithValue = True
        ExportObject(Detail)
        _Writer.RestoreSettings()
    End Sub

    Private Sub ExportObject(ByVal Target As Object)

        Dim o As Object = Target
        Dim processor As New PropertyProcessor()
        processor.Target = o
        _Writer.Processor = processor

        processor.Process()

    End Sub

End Class
