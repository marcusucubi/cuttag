Imports NPOI.SS.UserModel
Imports System.IO
Imports NPOI.HSSF.UserModel

Public Class ExcellWriter

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

    Public Sub New()
        Init()
    End Sub

    Private Sub Init()

        Dim wb As New HSSFWorkbook
        Dim sheet As Sheet = wb.CreateSheet("hi there")
        Dim s As New System.IO.FileStream("Test1.xls", FileMode.OpenOrCreate)
        wb.Write(s)
        s.Close()

    End Sub

    Private Sub Term()

    End Sub

    Private Sub _Processor_NextPropertyEvent(ByRef Prop As PropertyProxy) Handles _Processor.NextPropertyEvent

    End Sub

End Class
