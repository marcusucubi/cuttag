Public Class ExportBOM

    Private _ExcelBOMRwiter As ExcelBOMWriter

    Public Function Export(ByVal header As Common.Header) As String

        _ExcelBOMRwiter = New ExcelBOMWriter
        _ExcelBOMRwiter.Init()

        For Each detail As Common.Detail In header.Details
            ExportObject(detail.QuoteDetailProperties)
        Next

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)

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
