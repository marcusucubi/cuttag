Public Class ExportBOM

    Private _ExcelBOMRwiter As ExcelBOMWriter

    Public Function Export(ByVal header As Common.Header) As String

        _ExcelBOMRwiter = New ExcelBOMWriter
        _ExcelBOMRwiter.Init()

        _ExcelBOMRwiter.StartIndent("Wires")
        For Each detail As Common.Detail In header.Details
            If (detail.Product.UnitOfMeasure = Model.UnitOfMeasure.BY_LENGTH) Then
                _ExcelBOMRwiter.StartIndent(detail.ProductCode)
                ExportObject(detail.QuoteDetailProperties)
                _ExcelBOMRwiter.EndIndent()
            End If
        Next
        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.StartIndent("Components")
        For Each detail As Common.Detail In header.Details
            If (detail.Product.UnitOfMeasure = Model.UnitOfMeasure.BY_EACH) Then
                _ExcelBOMRwiter.StartIndent(detail.ProductCode)
                ExportObject(detail.QuoteDetailProperties)
                _ExcelBOMRwiter.EndIndent()
            End If
        Next
        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.StartIndent("Header")

        _ExcelBOMRwiter.StartIndent("PrimaryProperties")
        ExportObject(header.PrimaryProperties)
        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.StartIndent("OtherProperties")
        ExportObject(header.OtherProperties)
        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.StartIndent("ComputationProperties")
        ExportObject(header.ComputationProperties)
        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.StartIndent("NoteProperties")
        ExportObject(header.NoteProperties)
        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.EndIndent()

        _ExcelBOMRwiter.Term()
        Return _ExcelBOMRwiter.Path
    End Function

    Private Sub ExportObject(ByVal Target As Object)

        Dim o As Object = Target
        Dim processor As New PropertyProcessor()
        processor.Target = o
        _ExcelBOMRwiter.Processor = processor

        processor.Process()

    End Sub

End Class
