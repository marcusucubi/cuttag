Imports System.IO
Imports Model.Template

Public Class PropertyPrinter

    Private WithEvents _Processor As PropertyProcessor
    Private _Writer As TextWriter

    Public Property Processor As PropertyProcessor
        Get
            Return _Processor
        End Get
        Set(ByVal value As PropertyProcessor)
            _Processor = value
        End Set
    End Property

    Public Sub New()
        Me._Writer = Console.Out
    End Sub

    Private Sub _Next(ByRef Prop As PropertyProxy) _
        Handles _Processor.NextPropertyEvent

        _Writer.WriteLine("Property")
        If TypeOf Prop.Value Is Model.Customer Then
            Dim c As Model.Customer = Prop.Value
            _Writer.WriteLine("  Name : CustomerName")
            _Writer.WriteLine("  Type : String")
            _Writer.WriteLine("  Value : " & c.Name)
        Else
            _Writer.WriteLine("  Name : " & Prop.Name)
            _Writer.WriteLine("  Type : " & Prop.Type.Name)
            _Writer.WriteLine("  Value : " & Prop.Value)
        End If
        _Writer.WriteLine("End Property")
    End Sub

End Class
