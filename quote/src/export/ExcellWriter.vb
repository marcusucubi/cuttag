Imports NPOI.SS.UserModel
Imports System.IO
Imports NPOI.HSSF.UserModel

Public Class ExcellWriter

    Private Const SHEET_NAME As String = "Names"
    Private Const XLS_FILE_NAME As String = "Export.xls"

    Private _Workbook As New HSSFWorkbook
    Private _Sheet As Sheet
    Private _Index As Integer
    Private WithEvents _Processor As PropertyProcessor

    Public Path As String
    Public TemplatePath As String

    Public Property Processor As PropertyProcessor
        Get
            Return _Processor
        End Get
        Set(ByVal value As PropertyProcessor)
            _Processor = value
        End Set
    End Property

    Public Sub Init(ByVal templatePath As String)

        SetupPath()

        If templatePath IsNot Nothing Then
            Dim s As FileStream = File.OpenRead(templatePath)
            _Workbook = New HSSFWorkbook(s)
        End If

        _Sheet = _Workbook.GetSheet(SHEET_NAME)
        If _Sheet IsNot Nothing Then
            Dim index As Integer = _Workbook.GetSheetIndex(_Sheet)
            _Workbook.RemoveSheetAt(index)
        End If
        _Sheet = _Workbook.CreateSheet(SHEET_NAME)

    End Sub

    Public Sub Term()
        Dim s As New FileStream(Me.Path, FileMode.OpenOrCreate)
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
        Path = "export\" + t + ".xls"

        If Not Directory.Exists("export") Then
            Directory.CreateDirectory("export")
        End If

    End Sub

    Private Sub _Processor_NextPropertyEvent(ByRef Prop As PropertyProxy) Handles _Processor.NextPropertyEvent

        If Not Prop.Browsable Then
            Return
        End If

        Dim cell2 As Cell

        Dim existingName As Name = _Workbook.GetName(Prop.DisplayName)
        If existingName IsNot Nothing Then
            _Workbook.RemoveName(Prop.DisplayName)
        End If

        Dim name As Name = _Workbook.CreateName()
        name.NameName = Prop.DisplayName
        name.RefersToFormula = SHEET_NAME & "!$B$" & (_Index + 1)
        Dim row As Row = _Sheet.CreateRow(_Index)
        Dim cell As Cell = row.CreateCell(0)
        cell.SetCellValue(Prop.DisplayName)
        cell2 = row.CreateCell(1)

        cell2.SetCellValue(Prop.Value.ToString)
        _Index = _Index + 1

    End Sub

End Class
