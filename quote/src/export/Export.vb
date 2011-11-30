Public Class Export

    Private _Excel As ExcellWriter

    Public Function Export(ByVal header As Common.Header, _
                           ByVal templatePath As String) As String

        _Excel = New ExcellWriter
        _Excel.Init(templatePath)

        ExportObject(header.OtherProperties)
        ExportObject(header.PrimaryProperties)
        ExportObject(header.ComputationProperties)
        'dd_Added 11/28/11
        ExportObject(header.NoteProperties)

        _Excel.Term()
        Return _Excel.Path
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

        processor.Process()

    End Sub

End Class
