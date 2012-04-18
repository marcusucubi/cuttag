Imports DCS.Quote.Common
Imports System.Reflection

Public Class frmTextView

    Private WithEvents _Header As Header
    Private WithEvents _PrimaryProperties As PrimaryPropeties

    Public Sub New(ByVal q As Common.Header)
        InitializeComponent()
        If q IsNot Nothing Then
            Me._Header = q
            Me._PrimaryProperties = q.PrimaryProperties
        Else
            Me._Header = New Model.BOM.Header
            Me._PrimaryProperties = _Header.PrimaryProperties
        End If
        UpdateText()
        UpdateContent()
    End Sub

    Private Sub UpdateText()
        If Me._PrimaryProperties.CommonID > 0 Then
            If _Header.IsQuote Then
                Me.Text = "Quote " & Me._PrimaryProperties.CommonID
            Else
                Me.Text = "BOM " & Me._PrimaryProperties.CommonID
            End If
        Else
            Me.Text = "New BOM"
        End If
        If Me._Header.Dirty Then
            Me.Text = Me.Text + " *"
        End If
    End Sub

    Private Sub UpdateContent()

        Dim indent As String = "    "
        Dim s As String = ""
        s += ConvertToString(True, "", indent, "Primary", Me._Header.PrimaryProperties)
        s += ConvertToString(True, "", indent, "Computation", Me._Header.ComputationProperties)
        s += ConvertToString(True, "", indent, "Other", Me._Header.OtherProperties)
        s += ConvertToString(True, "", indent, "Notes", Me._Header.NoteProperties)

        s += vbCrLf + vbCrLf + "Detials" + vbCrLf + vbCrLf
        For Each d As Detail In Me._Header.Details
            s += ConvertToString(False, indent, indent + indent, d.ProductCode, d)
        Next

        Me.TextBox1.Text = s
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
                For i As Integer = 0 To 30 - n.Category.Length
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