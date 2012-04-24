Imports System.Reflection
Imports DCS.Quote.Common
Imports System.IO

Public Class TextDetailGenerator

    Private _TextGenerator As TextGenerator

    Public Class Node
        Public Property ProductCode As String
        Public Property Qty As Double
    End Class

    Public Sub New(generator As TextGenerator)
        _TextGenerator = generator
    End Sub

    Public Function UpdateContent() As String

        Dim s As String = ""

        s += UpdateWireContent()
        s += vbCrLf
        s += UpdatePartContent()

        Return s

    End Function

    Public Function UpdatePartContent() As String

        Dim s As String = ""
        s = GenerateReport("Components", Function(d As Detail)
                                             Return Not d.IsWire
                                         End Function)
        Return s

    End Function


    Public Function UpdateWireContent() As String

        Dim s As String = ""
        s = GenerateReport("Wires", Function(d As Detail)
                                        Return d.IsWire
                                    End Function)
        Return s

    End Function

    Function GenerateReport(title As String, test As Predicate(Of Detail)) As String

        Dim s As String = ""
        Dim indent As String = _TextGenerator.Indent

        Dim list As New List(Of Node)
        Dim map As New Dictionary(Of String, Detail)
        For Each d As Detail In _TextGenerator.Header.Details
            If test(d) Then

                Dim n As New Node
                n.ProductCode = d.ProductCode
                n.Qty = d.Qty
                list.Add(n)

                If Not map.ContainsKey(d.ProductCode) Then
                    map.Add(d.ProductCode, d)
                End If
            End If
        Next

        If Not _TextGenerator.HeaderToCompare Is Nothing Then
            For Each d As Detail In _TextGenerator.HeaderToCompare.Details

                If map.ContainsKey(d.ProductCode) Then
                    Continue For
                End If

                If test(d) Then
                    Dim n As New Node
                    n.ProductCode = d.ProductCode
                    n.Qty = 0
                    list.Add(n)
                End If
            Next
        End If

        s += vbCrLf + "-------------------------------------------------------------------------"
        s += vbCrLf & title
        s += vbCrLf & "      " & title & " Count = " & list.Count & ""
        s += vbCrLf
        s += vbCrLf + "   Part Number                    Qty"

        list.Sort(Function(d1 As Node, d2 As Node)
                      Dim r As Integer
                      r = d1.ProductCode.CompareTo(d2.ProductCode)
                      If r = 0 Then
                          r = d1.Qty.CompareTo(d2.Qty)
                      End If
                      Return r
                  End Function)

        For Each d As Node In list
            s += ConvertToString2(False, indent, indent + indent, d.ProductCode, d)
        Next

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

        Dim line As String = ConvertToString2(displayCategory, indent2, list)
        If line.Length > 0 Then
            s += vbCrLf
            s += indent1 + title
            For i As Integer = 0 To 30 - title.Length
                s += " "
            Next
            s += line
        End If

        If Not Me._TextGenerator.SyncDictionary Is Nothing Then

            Dim key As String

            If Me._TextGenerator.SyncDictionary.ContainsKey(title) Then

                key = Me._TextGenerator.SyncDictionary(title)

                s += vbCrLf & "Sync" & key
                s += vbCrLf & "Sync22" & key
            Else
                Dim g As System.Guid

                g = System.Guid.NewGuid()
                key = g.ToString()
                Me._TextGenerator.SyncDictionary(title) = key

                s += vbCrLf & "Sync" & key
                s += vbCrLf & "Sync22" & key
            End If
        End If

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

    Function ConvertToString2(displayCategory As Boolean, _
                              indent As String, _
                              list As List(Of PropertyProxy)) _
                         As String

        Dim s As String = ""
        For Each n As PropertyProxy In list

            If Not n.Browsable Then
                Continue For
            End If

            If Not n.Name = "Qty" Then
                Continue For
            End If

            If Not n.Value Is Nothing Then

                If n.Value.ToString() <> "0" Then
                    s += n.Value.ToString()
                End If
            End If

        Next

        Return s
    End Function

End Class

