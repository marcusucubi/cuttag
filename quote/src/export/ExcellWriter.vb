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
        Dim i As Integer = _Workbook.GetSheetIndex(_Sheet)
        _Workbook.SetSheetHidden(i, True)
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

        Dim s As String = System.IO.Path.Combine( _
            ExportPath.Path, "quote")

        If Not Directory.Exists(s) Then
            Directory.CreateDirectory(s)
        End If

        Path = System.IO.Path.Combine(s, ExportPath.DateFileName + ".xls")

    End Sub

    Private Sub _Processor_NextPropertyEvent(ByRef Prop As PropertyProxy) Handles _Processor.NextPropertyEvent

        If Not Prop.Browsable Then
            Return
        End If

        Dim cell2 As Cell

        Dim name As Name = _Workbook.GetName(Prop.DisplayName)
        If name Is Nothing Then
            name = _Workbook.CreateName()
            name.NameName = Prop.DisplayName
        End If
        name.RefersToFormula = SHEET_NAME & "!$B$" & (_Index + 1)
        Dim row As Row = _Sheet.CreateRow(_Index)
        Dim cell As Cell = row.CreateCell(0)
        cell.SetCellValue(Prop.DisplayName)
        cell2 = row.CreateCell(1)

        cell2.SetCellValue(Prop.Value.ToString)
        _Index = _Index + 1

    End Sub

End Class
