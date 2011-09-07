Imports System.Reflection

Public Class PropertyProcessor

    Private _Target As Object
    Private _Info() As PropertyInfo
    Private _InfoList As New List(Of PropertyInfo)

    Public Delegate Sub NextProperty(ByRef Prop As PropertyProxy)
    Public Event NextPropertyEvent As NextProperty

    Public Class NameComparitor
        Implements System.Collections.Generic.IComparer(Of PropertyInfo)

        Public Function Compare(ByVal x As System.Reflection.PropertyInfo, _
                                ByVal y As System.Reflection.PropertyInfo) As Integer _
                            Implements System.Collections.Generic.IComparer(Of System.Reflection.PropertyInfo).Compare
            Return x.Name.CompareTo(y.Name)
        End Function
    End Class

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

        _InfoList.Clear()
        For Each p As PropertyInfo In _Info
            _InfoList.Add(p)
        Next
        _InfoList.Sort(New NameComparitor())

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
        Dim p As PropertyInfo = _InfoList(index)
        RaiseEvent NextPropertyEvent(New PropertyProxy(p, _Target))
        Return True
    End Function

End Class
