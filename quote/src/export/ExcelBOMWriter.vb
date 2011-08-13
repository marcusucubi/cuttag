Imports NPOI.SS.UserModel
Imports System.IO
Imports NPOI.HSSF.UserModel

Public Class ExcelBOMWriter

    Private Const SHEET_NAME As String = "BOM"
    Private Const XLS_FILE_NAME As String = "Export.xls"

    Private _Workbook As New HSSFWorkbook
    Private _Sheet As Sheet
    Private _Index As Integer
    Private WithEvents _Processor As PropertyProcessor
    Private _Indent As Integer

    Public Path As String
    Public TemplatePath As String

    Public Sub StartIndent(ByVal s As String)
        WriteValue(s, "")
        _Indent = _Indent + 1
    End Sub

    Public Sub EndIndent()
        _Indent = _Indent - 1
    End Sub

    Public Property Processor As PropertyProcessor
        Get
            Return _Processor
        End Get
        Set(ByVal value As PropertyProcessor)
            _Processor = value
        End Set
    End Property

    Public Sub Init()

        SetupPath()

        _Workbook = New HSSFWorkbook()
        _Sheet = _Workbook.CreateSheet(SHEET_NAME)
        _Sheet.SetColumnWidth(0, 1000 * 5)
        _Sheet.SetColumnWidth(1, 1000 * 5)
        _Sheet.SetColumnWidth(2, 1000 * 6)
        _Sheet.SetColumnWidth(3, 1000 * 5)
    End Sub

    Public Sub Term()
        Dim s As New FileStream(Me.Path, FileMode.OpenOrCreate)
        _Workbook.GetSheetAt(0).ForceFormulaRecalculation = True
        _Workbook.Write(s)
        s.Close()

        Dim parent As DirectoryInfo = Directory.GetParent(Me.Path)
        Process.Start("explorer.exe", parent.FullName)
    End Sub

    Private Sub SetupPath()

        Dim t As String
        t = Date.Now.Year.ToString & "-"
        t += Date.Now.Month.ToString & "-"
        t += Date.Now.Day.ToString & "-"
        t += Date.Now.ToShortTimeString
        t = t.Replace(".", "")
        t = t.Replace("/", "")
        t = t.Replace(":", "")
        t = t.Replace(" ", "")
        Path = "exportBOM\" + t + ".xls"

        If Not Directory.Exists("exportBOM") Then
            Directory.CreateDirectory("exportBOM")
        End If

    End Sub

    Private Sub _Processor_NextPropertyEvent(ByRef Prop As PropertyProxy) Handles _Processor.NextPropertyEvent

        If Not Prop.Browsable Then
            Return
        End If

        WriteValue(Prop.DisplayName, Prop.Value.ToString)
    End Sub

    Private Sub WriteValue(ByVal name As String, ByVal value As String)

        Dim row As Row = _Sheet.CreateRow(_Index)
        Dim cell As Cell = row.CreateCell(_Indent)
        cell.SetCellValue(name)
        Dim cell2 As Cell = row.CreateCell(_Indent + 1)

        cell2.SetCellValue(value)
        _Index = _Index + 1

    End Sub

End Class
