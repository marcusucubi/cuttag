Imports System.Reflection
Imports DCS.Quote.Common
Imports System.IO

Public Class TextGenerator

    Private _Header As Common.Header
    Private _HeaderToCompare As Common.Header
    Private _Data As String
    Private _List As New List(Of String)
    Private _SyncDictionary As Dictionary(Of String, String)
    Private _GenerateGuid As Boolean

    Public Class Node
        Public Property ProductCode As String
        Public Property Qty As Double
    End Class

    Public Sub New(q1 As Common.Header, q2 As Common.Header)
        Me.New(q1, q2, New Dictionary(Of String, String), True)
    End Sub

    Public Sub New(q1 As Common.Header, _
                   q2 As Common.Header, _
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

    Public ReadOnly Property Header As Common.Header
        Get
            Return _Header
        End Get
    End Property

    Public ReadOnly Property HeaderToCompare As Common.Header
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

        Dim indent As String = "    "
        Dim s As String = ""
        s += ConvertToString(False, "", indent, "Primary", Me._Header.PrimaryProperties)
        s += ConvertToString(False, "", indent, "Computation", Me._Header.ComputationProperties)
        s += ConvertToString(False, "", indent, "Other", Me._Header.OtherProperties)
        s += ConvertToString(False, "", indent, "Notes", Me._Header.NoteProperties)


        Dim g As New TextDetailGenerator(Me)
        s += g.UpdateContent()


        Dim strReader As StringReader = New StringReader(s)
        Dim line As String = ""
        Do While True
            line = strReader.ReadLine()

            If line Is Nothing Then
                Exit Do
            End If

            Debug.WriteLine(line)

            _List.Add(line)
        Loop

    End Sub

    Function ConvertToString(displayCategory As Boolean, _
                             indent1 As String, _
                             indent2 As String, _
                             title As String, _
                             obj As Object) _
                         As String
        Dim list As New List(Of PropertyProxy)
        list = ConvertToList(obj)
        Dim s As String = ""
        s += vbCrLf
        s += indent1 + title + vbCrLf
        s += vbCrLf
        s += ConvertToString(displayCategory, indent2, list)
        Return s
    End Function

    Function ConvertToList(obj) As List(Of PropertyProxy)

        Dim list As New List(Of PropertyProxy)

        Dim props As PropertyInfo()
        props = obj.GetType.GetProperties()
        For Each prop As PropertyInfo In props
            Dim proxy As New PropertyProxy(prop, obj)
            list.Add(proxy)
        Next
        Return list
    End Function

    Function ConvertToString(displayCategory As Boolean, _
                             indent As String, _
                             list As List(Of PropertyProxy)) _
                         As String

        Dim s As String = ""
        For Each n As PropertyProxy In list

            If Not n.Browsable Then
                Continue For
            End If

            s += indent

            If displayCategory Then
                s += n.Category
                For i As Integer = 0 To 20 - n.Category.Length
                    s += " "
                Next
            End If

            s += n.DisplayName
            For i As Integer = 0 To 30 - n.DisplayName.Length
                s += " "
            Next

            If Not n.Value Is Nothing Then
                s += "" + n.Value.ToString()
            End If
            s += vbCrLf

        Next

        Return s
    End Function

End Class
