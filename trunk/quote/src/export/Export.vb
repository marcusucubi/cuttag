Public Class Export

    Private _Excel As New ExcellWriter

    Public Sub Export(ByVal header As Common.Header)

        _Excel.Init()

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)

        _Excel.Term()

    End Sub

    Public Sub ExportObject(ByVal Target As Object)

        Dim o As Object = Target
        Dim processor As New PropertyProcessor()
        processor.Target = o

        Dim printer As New PropertyPrinter
        printer.Processor = processor
        _Excel.Processor = processor

        processor.Process()

    End Sub

End Class
