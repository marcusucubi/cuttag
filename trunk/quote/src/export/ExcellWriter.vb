Imports NPOI.SS.UserModel
Imports System.IO
Imports NPOI.HSSF.UserModel

Public Class ExcellWriter

    Private _Workbook As New HSSFWorkbook
    Private _Sheet As Sheet
    Private WithEvents _Processor As PropertyProcessor
    Private _Index As Integer

    Public Property Processor As PropertyProcessor
        Get
            Return _Processor
        End Get
        Set(ByVal value As PropertyProcessor)
            _Processor = value
        End Set
    End Property

    Public Sub Init()
        _Sheet = _Workbook.CreateSheet("raw")
    End Sub

    Public Sub Term()
        System.IO.File.Delete("Export.xls")
        Dim s As New System.IO.FileStream("Export.xls", FileMode.OpenOrCreate)
        _Workbook.Write(s)
        s.Close()
    End Sub

    Private Sub _Processor_NextPropertyEvent(ByRef Prop As PropertyProxy) Handles _Processor.NextPropertyEvent

        If Not Prop.Browsable Then
            Return
        End If

        Dim row As Row = _Sheet.CreateRow(_Index)
        Dim cell As Cell = row.CreateCell(0)
        cell.SetCellValue(Prop.DisplayName)
        Dim cell2 As Cell = row.CreateCell(1)
        cell2.SetCellValue(Prop.Value.ToString)
        _Index = _Index + 1
    End Sub

End Class
