Public Class Export

    Dim _Processor As New PropertyProcessor()

    Public Sub Export(ByVal header As Common.Header)

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)

    End Sub


    Public Sub ExportObject(ByVal Target As Object)

        Dim o As Object = Target
        _Processor.Target = o

        Dim printer As New PropertyPrinter
        printer.Processor = _Processor

        _Processor.Process()

    End Sub

End Class
