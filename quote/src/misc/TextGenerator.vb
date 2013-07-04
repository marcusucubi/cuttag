Imports System.Globalization
Imports System.Reflection
Imports System.IO

Imports Model.Common

Public Class TextGenerator

    Private _Header As Model.Common.Header
    Private _HeaderToCompare As Model.Common.Header
    Private _Data As String
    Private _List As New List(Of String)
    Private _SyncDictionary As Dictionary(Of String, String)
    Private _GenerateGuid As Boolean

    Public Sub New(q1 As Model.Common.Header, q2 As Model.Common.Header)
        Me.New(q1, q2, New Dictionary(Of String, String), True)
    End Sub

    Public Sub New(q1 As Model.Common.Header, _
                   q2 As Model.Common.Header, _
                   syncMap As Dictionary(Of String, String), _
                   generateGuid As Boolean)
        _Header = q1
        _HeaderToCompare = q2
        _SyncDictionary = syncMap
        _GenerateGuid = generateGuid
        UpdateContent()
    End Sub

    Public Property Data As String
        Get
            Return _Data
        End Get
        Set(value As String)
            _Data = value
        End Set
    End Property

    Public ReadOnly Property SyncDictionary As Dictionary(Of String, String)
        Get
            Return _SyncDictionary
        End Get
    End Property

    Public ReadOnly Property List As List(Of String)
        Get
            Return _List
        End Get
    End Property

    Public ReadOnly Property Header As Model.Common.Header
        Get
            Return _Header
        End Get
    End Property

    Public ReadOnly Property HeaderToCompare As Model.Common.Header
        Get
            Return _HeaderToCompare
        End Get
    End Property

    Public ReadOnly Property Indent As String
        Get
            Return "   "
        End Get
    End Property

    Private Sub UpdateContent()

        Dim s As String = ""
        s += ConvertToString("Primary", Me._Header.PrimaryProperties, Me.HeaderToCompare.PrimaryProperties)
        s += ConvertToString("Computation", Me._Header.ComputationProperties, Me.HeaderToCompare.ComputationProperties)
        s += ConvertToString("Other", Me._Header.OtherProperties, Me.HeaderToCompare.OtherProperties)
        s += ConvertToString("Notes", Me._Header.NoteProperties, Me.HeaderToCompare.NoteProperties)

        Dim g As New TextDetailGenerator(Me)
        s += g.UpdateContent()

        Dim strReader As StringReader = New StringReader(s)
        Dim line As String = ""
        Do While True
            line = strReader.ReadLine()

            If line Is Nothing Then
                Exit Do
            End If

            _List.Add(line)
        Loop

    End Sub

    Function ConvertToString(title As String, _
                             obj As Object, _
                             obj2 As Object) _
                         As String
        Dim list As New List(Of Node)
        list = ConvertToList(obj, obj2)
        Dim s As String = ""
        s += vbCrLf
        s += title + vbCrLf
        s += vbCrLf
        s += ConvertToString(Me.Indent, list)
        Return s
    End Function

    Public Class Node
        Public Property DisplayName As String
        Public Property Value As Object
        Public Property Browsable As Boolean
        Public Property Type As Type
    End Class

    Function ConvertToList(obj, obj2) As List(Of Node)

        Dim map As New Dictionary(Of String, Node)
        Dim list As New List(Of Node)

        Dim props As PropertyInfo()
        props = obj.GetType.GetProperties()
        For Each prop As PropertyInfo In props

            Dim proxy As New PropertyProxy(prop, obj)
            Dim n As New Node()
            n.DisplayName = proxy.DisplayName
            n.Value = proxy.Value
            n.Browsable = proxy.Browsable
            n.Type = proxy.Type
            list.Add(n)

            If Not map.ContainsKey(proxy.DisplayName) Then
                map.Add(proxy.DisplayName, n)
            End If
        Next

        If Not _HeaderToCompare Is Nothing Then

            Dim props2 As PropertyInfo()
            props2 = obj2.GetType.GetProperties()
            For Each prop As PropertyInfo In props2

                Dim proxy As New PropertyProxy(prop, obj2)
                If map.ContainsKey(proxy.DisplayName) Then
                    Continue For
                End If

                Dim n As New Node()
                n.DisplayName = proxy.DisplayName
                n.Browsable = proxy.Browsable
                list.Add(n)
            Next
        End If

        list.Sort(Function(n1 As Node, n2 As Node)
                      Return n1.DisplayName.CompareTo(n2.DisplayName)
                  End Function)

        Return list
    End Function

    Function ConvertToString(indent As String, _
                             list As List(Of Node)) _
                         As String

        Dim s As String = ""
        For Each n As Node In list

            If Not n.Browsable Then
                Continue For
            End If

            If Not n.Value Is Nothing Then

                s += indent

                s += n.DisplayName
                For i As Integer = 0 To 30 - n.DisplayName.Length
                    s += " "
                Next

                If n.Type = GetType(Decimal) Then
                    Dim d As Double = CType(n.Value, Double)
                    s += "" + d.ToString("#,#,0.0000")
                Else
                    s += "" + n.Value.ToString()
                End If

                s += vbCrLf
            End If

            Dim title As String = n.DisplayName
            If Not Me._SyncDictionary Is Nothing Then

                Dim key As String

                If Me._SyncDictionary.ContainsKey(title) Then

                    key = Me._SyncDictionary(title)

                    s += "Sync" & key
                    s += vbCrLf & "Sync22" & key
                Else
                    Dim g As System.Guid

                    g = System.Guid.NewGuid()
                    key = g.ToString()
                    Me._SyncDictionary(title) = key

                    s += "Sync" & key
                    s += vbCrLf & "Sync22" & key
                End If

                s += vbCrLf
            End If

        Next

        Return s
    End Function

End Class
