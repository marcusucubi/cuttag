Public Class Export

    Private _Excel As ExcellWriter
    Private _ExcelBOMRwiter As ExcelBOMWriter

    Public Function Export(ByVal header As Common.Header, _
                           ByVal templatePath As String) As String

        _Excel = New ExcellWriter
        _Excel.Init(templatePath)

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)

        _Excel.Term()
        Return _Excel.Path
    End Function

    Public Function ExportBOM(ByVal header As Common.Header) As String

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
        If (_Excel IsNot Nothing) Then
            _Excel.Processor = processor
        End If
        If (_ExcelBOMRwiter IsNot Nothing) Then
            _ExcelBOMRwiter.Processor = processor
        End If

        processor.Process()

    End Sub

End Class
