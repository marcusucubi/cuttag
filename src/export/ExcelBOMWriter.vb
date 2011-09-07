Imports NPOI.SS.UserModel
Imports System.IO
Imports NPOI.HSSF.UserModel

Public Class ExcelBOMWriter

    Private Const XLS_FILE_NAME As String = "Export.xls"

    Private WithEvents _Processor As PropertyProcessor
    Private _Workbook As New HSSFWorkbook
    Private _Sheet As Sheet
    Private _Index As Integer
    Private _Indent As Integer
    Private _CursorX As Integer

    Public Path As String
    Public TemplatePath As String

    Public UpdateWithValue As Boolean = True
    Public UpdateWithName As Boolean = True
    Public MoveRight As Boolean
    Public MoveDown As Boolean = True
    Public SaveUpdateWithValue As Boolean
    Public SaveUpdateWithName As Boolean
    Public SaveMoveRight As Boolean
    Public SaveMoveDown As Boolean

    Public Sub New(ByVal sheetName As String)
        _Workbook = New HSSFWorkbook()
        SetupPath()
        _Sheet = _Workbook.CreateSheet(sheetName)
        SizeColumns()
    End Sub

    Public Sub New(ByVal Path As String, ByVal workbook As HSSFWorkbook, ByVal sheetName As String)
        _Workbook = workbook
        Me.Path = Path
        _Sheet = _Workbook.CreateSheet(sheetName)
        SizeColumns()
    End Sub

    Public Sub SizeColumns()
        For i As Integer = 0 To 10
            _Sheet.SetColumnWidth(i, 1000 * 5)
        Next
    End Sub

    Public ReadOnly Property Workbook As HSSFWorkbook
        Get
            Return _Workbook
        End Get
    End Property

    Public Sub SaveSettings()
        SaveUpdateWithValue = UpdateWithValue
        SaveUpdateWithName = UpdateWithName
        SaveMoveRight = MoveRight
        SaveMoveDown = MoveDown
    End Sub

    Public Sub RestoreSettings()
        UpdateWithValue = SaveUpdateWithValue
        UpdateWithName = SaveUpdateWithName
        MoveRight = SaveMoveRight
        MoveDown = SaveMoveDown
    End Sub

    Public Sub StartIndent(ByVal s As String)
        WriteValue(s, "")
        _Indent = _Indent + 1
    End Sub

    Public Sub EndIndent()
        _Indent = _Indent - 1
    End Sub

    Public Sub IncrementIndex()
        _CursorX = 0
        _Index = _Index + 1
    End Sub

    Public Property Processor As PropertyProcessor
        Get
            Return _Processor
        End Get
        Set(ByVal value As PropertyProcessor)
            _Processor = value
        End Set
    End Property

    Public Sub Open()
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
        Path = "exportBOM\" + t + ".xls"

        If Not Directory.Exists("exportBOM") Then
            Directory.CreateDirectory("exportBOM")
        End If

    End Sub

    Private Sub _Processor_NextPropertyEvent(ByRef Prop As PropertyProxy) _
        Handles _Processor.NextPropertyEvent

        If Not Prop.Browsable Then
            Return
        End If

        WriteValue(Prop.DisplayName, Prop.Value)
    End Sub

    Private Sub WriteValue(ByVal name As String, ByVal value As String)

        Dim row As Row = _Sheet.GetRow(_Index)
        If row Is Nothing Then
            row = _Sheet.CreateRow(_Index)
        End If
        Dim cell As Cell = row.GetCell(_Indent + _CursorX)
        If cell Is Nothing Then
            cell = row.CreateCell(_Indent + _CursorX)
        End If

        If UpdateWithValue And UpdateWithName Then
            cell.SetCellValue(name)
            cell = row.GetCell(_Indent + _CursorX + 1)
            If cell Is Nothing Then
                cell = row.CreateCell(_Indent + _CursorX + 1)
            End If
            If IsNumeric(value) Then
                cell.SetCellValue(CDec(value))
                cell.SetCellType(CellType.NUMERIC)
            Else
                cell.SetCellValue(value)
            End If
        Else
            If UpdateWithValue Then
                If IsNumeric(value) Then
                    Dim d As Decimal = 0
                    value = value.TrimEnd("0")
                    value = value.TrimStart("0")
                    value = value.TrimEnd(".")
                    If (value.Length > 0) Then
                        d = CDbl(value)
                    End If
                    '                    If Decimal.TryParse(value.Trim(), d) Then
                    ' cell.SetCellValue(value)
                    'Else
                    cell.SetCellValue(d)
                    cell.SetCellType(CellType.NUMERIC)
                    'End If
                Else
                    cell.SetCellValue(value)
                End If
            End If
            If UpdateWithName Then
                cell.SetCellValue(name)
            End If
        End If

        If MoveRight Then
            _CursorX = _CursorX + 1
        End If
        If MoveDown Then
            _Index = _Index + 1
        End If

    End Sub

End Class
