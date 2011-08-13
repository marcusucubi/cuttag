Imports System.Reflection

Public Class PropertyProcessor

    Private _Target As Object
    Private _Info() As PropertyInfo

    Public Delegate Sub NextProperty(ByRef Prop As PropertyProxy)
    Public Event NextPropertyEvent As NextProperty

    Public Property Target As Object
        Get
            Return _Target
        End Get
        Set(ByVal value As Object)
            _Target = value
            _Info = value.GetType.GetProperties()
        End Set
    End Property

    Public Sub Process()

        Dim index As Integer
        Do
            If index >= _Info.Length Then
                Exit Do
            End If

            GetNextProperty(index)
            index += 1
        Loop

    End Sub

    Private Function GetNextProperty(ByVal index As Integer) As Boolean
        If index >= _Info.Length Then
            Return False
        End If
        Dim p As PropertyInfo = _Info(index)
        RaiseEvent NextPropertyEvent(New PropertyProxy(p, _Target))
        Return True
    End Function

End Class
