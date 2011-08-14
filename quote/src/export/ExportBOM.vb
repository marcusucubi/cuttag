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

        _Writer.StartIndent("ComputationProperties")
        ExportObject(header.ComputationProperties)
        _Writer.EndIndent()

        _Writer.StartIndent("NoteProperties")
        ExportObject(header.NoteProperties)
        _Writer.EndIndent()

        _Writer = New ExcelBOMWriter(_Writer.Path, _Writer.Workbook, "Wires")
        Dim index As Integer
        For Each detail As Common.Detail In header.Details
            If (detail.Product.UnitOfMeasure = Model.UnitOfMeasure.BY_LENGTH) Then
                If index = 0 Then
                    PrintWireColumns(detail.QuoteDetailProperties)
                    _Writer.IncrementIndex()
                End If
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
                    PrintWireColumns(detail.QuoteDetailProperties)
                    _Writer.IncrementIndex()
                End If
                PrintWireValue(detail.QuoteDetailProperties)
                _Writer.IncrementIndex()
                index = index + 1
            End If
        Next


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
