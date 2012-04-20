Imports System.Reflection
Imports DCS.Quote.Common
Imports System.IO

Public Class TextGenerator

    Private _Header As Common.Header
    Private _Data As String
    Private _List As New List(Of String)

    Public Sub New(ByVal q1 As Common.Header)
        _Header = q1
        UpdateContent()
    End Sub

    Public ReadOnly Property Data As String
        Get
            Return _Data
        End Get
    End Property

    Public ReadOnly Property List As List(Of String)
        Get
            Return _List
        End Get
    End Property

    Private Sub UpdateContent()

        Dim indent As String = "    "
        Dim s As String = ""
        s += ConvertToString(False, "", indent, "Primary", Me._Header.PrimaryProperties)
        s += ConvertToString(False, "", indent, "Computation", Me._Header.ComputationProperties)
        s += ConvertToString(False, "", indent, "Other", Me._Header.OtherProperties)
        s += ConvertToString(False, "", indent, "Notes", Me._Header.NoteProperties)

        ' -------------

        Dim wires As New List(Of Detail)
        For Each d As Detail In Me._Header.Details
            If d.IsWire Then
                wires.Add(d)
            End If
        Next

        s += vbCrLf + "-------------------------------------------------------------------------"
        s += vbCrLf + "Wires (" & wires.Count & ")" + vbCrLf + vbCrLf

        wires.Sort(Function(d1 As Detail, d2 As Detail)
                       Return d1.ProductCode.CompareTo(d2.ProductCode)
                   End Function)

        For Each d As Detail In wires
            s += ConvertToString2(False, indent, indent + indent, d.ProductCode, d)
        Next

        ' -------------

        Dim parts As New List(Of Detail)
        For Each d As Detail In Me._Header.Details
            If Not d.IsWire Then
                parts.Add(d)
            End If
        Next

        s += vbCrLf + "-------------------------------------------------------------------------"
        s += vbCrLf + "Componenets (" & parts.Count & ")" + vbCrLf + vbCrLf

        parts.Sort(Function(d1 As Detail, d2 As Detail)
                       Return d1.ProductCode.CompareTo(d2.ProductCode)
                   End Function)

        For Each d As Detail In parts
            s += ConvertToString2(False, indent, indent + indent, d.ProductCode, d)
        Next

        ' -------------


        _Data = s

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

    Function ConvertToString2(displayCategory As Boolean, _
                              indent1 As String, _
                              indent2 As String, _
                              title As String, _
                              obj As Object) _
                          As String
        Dim list As New List(Of PropertyProxy)
        list = ConvertToList(obj)
        Dim s As String = ""
        s += vbCrLf

        s += indent1 + title
        For i As Integer = 0 To 30 - title.Length
            s += " "
        Next

        s += vbCrLf + indent1 + "                               "
        s += "{"
        s += ConvertToString2(displayCategory, indent2, list)
        s += "}"
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

    Function ConvertToString2(displayCategory As Boolean, _
                             indent As String, _
                             list As List(Of PropertyProxy)) _
                         As String

        Dim s As String = ""
        For Each n As PropertyProxy In list

            If Not n.Browsable Then
                Continue For
            End If

            s += n.DisplayName
            s += "="
            If Not n.Value Is Nothing Then
                s += n.Value.ToString()
            End If
            s += "|"

        Next

        Return s
    End Function

End Class
