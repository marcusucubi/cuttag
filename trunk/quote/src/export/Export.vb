Public Class Export

    Private _Excel As New ExcellWriter

    Public Function Export(ByVal header As Common.Header, _
                           ByVal templatePath As String) As String

        _Excel.Init(templatePath)

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)
        ExportObject(header.CustomProperties)

        _Excel.Term()
        Return _Excel.Path
    End Function

    Private Sub ExportObject(ByVal Target As Object)

        Dim o As Object = Target
        Dim processor As New PropertyProcessor()
        processor.Target = o

        Dim printer As New PropertyPrinter
        printer.Processor = processor
        _Excel.Processor = processor

        processor.Process()

    End Sub

End Class
