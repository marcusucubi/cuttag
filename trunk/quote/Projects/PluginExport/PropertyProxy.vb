Imports System.Reflection
Imports System.ComponentModel

Public Class PropertyProxy

    Private _Info As PropertyInfo

    Public Property Name As String
    Public Property Value As Object
    Public Property Type As Type

    Public Sub New(ByRef Info As PropertyInfo, _
                   ByRef Target As Object)
        Name = Info.Name
        Value = Info.GetValue(Target, Nothing)
        Type = Info.PropertyType
        _Info = Info
    End Sub

    Public ReadOnly Property Category As String
        Get
            Dim cat As String = "Misc"
            With cat
                Dim oa As CategoryAttribute() = _
                        _Info.GetCustomAttributes(GetType(CategoryAttribute), False)
                If oa.Length > 0 Then
                    cat = oa(0).Category
                End If
            End With

            Dim s As String = ""
            s = Replace(cat, " ", "")
            s = Replace(s, Chr(160), "")
            s = Replace(s, vbTab, "")

            Return s
        End Get
    End Property

    Public ReadOnly Property Browsable As Boolean
        Get
            Dim result As Boolean = True
            Dim oa As BrowsableAttribute() = _
                    _Info.GetCustomAttributes(GetType(BrowsableAttribute), False)
            If oa.Length > 0 Then
                result = oa(0).Browsable
            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property DisplayName As String
        Get
            Return _Info.Name
        End Get
    End Property

End Class
