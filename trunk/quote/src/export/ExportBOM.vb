Public Class ExportBOM

    Private _ExcelBOMRwiter As ExcelBOMWriter

    Public Function Export(ByVal header As Common.Header) As String

        _ExcelBOMRwiter = New ExcelBOMWriter
        _ExcelBOMRwiter.Init()

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

        Dim printer As New PropertyPrinter
        printer.Processor = processor
        If (_ExcelBOMRwiter IsNot Nothing) Then
            _ExcelBOMRwiter.Processor = processor
        End If

        processor.Process()

    End Sub

End Class
