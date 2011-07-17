Imports System.Reflection

Public Class PropertyProxy

    Public Property Name As String
    Public Property Value As Object
    Public Property Type As Type

    Public Sub New(ByRef Info As PropertyInfo, _
                   ByRef Target As Object)
        Name = Info.Name
        Value = Info.GetValue(Target, Nothing)
        Type = Info.PropertyType
    End Sub

End Class
