Public Class Export


    Public Sub Export(ByVal header As Common.Header)

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)

    End Sub

    Public Sub ExportObject(ByVal Target As Object)

        Dim o As Object = Target
        Dim processor As New PropertyProcessor()
        processor.Target = o

        Dim printer As New PropertyPrinter
        printer.Processor = processor
        Dim excel As New ExcellWriter
        excel.Processor = processor

        processor.Process()

    End Sub

End Class
