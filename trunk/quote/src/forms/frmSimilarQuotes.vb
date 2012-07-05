Imports System.Windows.Forms
Imports System.Collections.ObjectModel

Public Class frmSimilarQuotes

    Private _Quotes As SimilarQuotes
    Private _Target As SimilarQuote
    Private Shared _Debug As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(targetId As Integer)

        InitializeComponent()

        Cursor = Cursors.WaitCursor
        My.Application.DoEvents()

        Init(targetId)

        Cursor = Cursors.Default
        Sort(2)
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OK_Button.Click
        DialogResult = Windows.Forms.DialogResult.OK
        OpenAndClose()
    End Sub

    Private Sub OpenAndClose()
        Dim i As ListViewItem = ListView1.SelectedItems(0)
        Dim id As Integer = CInt(i.Text)
        Cursor = Cursors.WaitCursor
        My.Application.DoEvents()
        frmMain.frmMain.LoadTemplate(id)
        Cursor = Cursors.Default
        Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub ListView1_ColumnClick(sender As System.Object, e As Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick
        Sort(e.Column)
    End Sub

    Private Sub Sort(column As Integer)
        Dim sorter As New ListViewColumnSorter
        sorter.SortColumn = column
        sorter.Order = SortOrder.Descending
        ListView1.ListViewItemSorter = sorter
    End Sub

    Public Sub Init(targetId As Integer)

        Dim adaptor1 As New QuoteDataBaseTableAdapters._QuoteDetailTableAdapter
        Dim table1 As New QuoteDataBase._QuoteDetailDataTable
        Dim row1 As QuoteDataBase._QuoteDetailRow
        adaptor1.FillByQuoteID(table1, targetId)
        Dim target As SimilarQuote = Nothing
        _Debug = True
        For Each row1 In table1.Rows
            If target Is Nothing Then
                target = New SimilarQuote(row1, Nothing)
            Else
                target.AddPartOrWire(row1, target)
            End If
        Next
        _Debug = False
        _Target = target

        _Quotes = New SimilarQuotes(target)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteDetailTableAdapter
        Dim table As New QuoteDataBase._QuoteDetailDataTable
        Dim row As QuoteDataBase._QuoteDetailRow

        adaptor.Fill(table)

        For Each row In table.Rows

            If targetId = row.QuoteID Then
                Continue For
            End If

            If _Quotes.ContainsQuote(row.QuoteID) Then
                Dim q As SimilarQuote = _Quotes.GetQuote(row.QuoteID)
                q.AddPartOrWire(row, target)
            Else
                _Quotes.AddQuote(row)
            End If
        Next

        For Each q As SimilarQuote In _Quotes
            Dim i As New ListViewItem()
            i.Name = q.id
            i.Text = "" & q.id
            ListView1.Items.Add(i)
            Dim s1 As New ListViewItem.ListViewSubItem
            s1.Text = q.matchWires
            i.SubItems.Add(s1)
            Dim s2 As New ListViewItem.ListViewSubItem
            s2.Text = q.matchParts
            i.SubItems.Add(s2)
        Next

    End Sub

    Public Class SimilarQuotes
        Inherits ObservableCollection(Of SimilarQuote)

        Private ReadOnly _Target As SimilarQuote

        Public Sub New(target As SimilarQuote)
            _Target = target
        End Sub

        Private ReadOnly _Map As New Dictionary(Of Integer, SimilarQuote)

        Public Sub AddQuote(row As QuoteDataBase._QuoteDetailRow)
            Dim d As New SimilarQuote(row, _Target)
            Add(d)
            _Map.Add(d.id, d)
        End Sub

        Public Function ContainsQuote(id As Integer) As Boolean
            Return _Map.ContainsKey(id)
        End Function

        Public Function GetQuote(id As Integer) As SimilarQuote
            Return _Map(id)
        End Function

    End Class

    Public Class SimilarQuote

        Public wires As New Dictionary(Of String, SimilarWire)
        Public parts As New Dictionary(Of String, SimilarPart)
        Property id As Integer
        Property matchWires As Integer
        Property matchParts As Integer

        Public Sub New(row As QuoteDataBase._QuoteDetailRow, _
                       target As SimilarQuote)
            Init(row, target)
        End Sub

        Public Sub AddPartOrWire(row As QuoteDataBase._QuoteDetailRow, _
                                 target As SimilarQuote)
            If row.IsWire Then
                Dim wire As New SimilarWire
                wire.partNumber = row.ProductCode
                If Not wires.ContainsKey(wire.partNumber) Then
                    wires.Add(wire.partNumber, wire)

                    If target.wires.ContainsKey(wire.partNumber) Then
                        matchWires += 1
                    End If
                End If

            Else
                Dim part As New SimilarPart
                part.partNumber = row.ProductCode
                If Not parts.ContainsKey(part.partNumber) Then
                    parts.Add(part.partNumber, part)

                    If target.parts.ContainsKey(part.partNumber) Then
                        matchParts += 1
                    End If
                End If
            End If
        End Sub

        Private Sub Init(row As QuoteDataBase._QuoteDetailRow, _
                         target As SimilarQuote)
            id = row.QuoteID
            If row.IsWire Then
                Dim wire As New SimilarWire
                wire.partNumber = row.ProductCode
                wires.Add(wire.partNumber, wire)
                If Not target Is Nothing Then
                    If target.wires.ContainsKey(wire.partNumber) Then
                        matchWires += 1
                    End If
                End If
            Else
                Dim part As New SimilarPart
                part.partNumber = row.ProductCode
                parts.Add(part.partNumber, part)

                If Not target Is Nothing Then
                    If target.parts.ContainsKey(part.partNumber) Then
                        matchParts += 1
                    End If
                End If
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return "" & id & " Wires(" & wires.Count & ") Parts(" & parts.Count & ")"
        End Function

    End Class

    Public Structure SimilarWire
        Property partNumber As String

        Public Overrides Function ToString() As String
            Return "" + partNumber
        End Function
    End Structure

    Public Structure SimilarPart
        Property partNumber As String

        Public Overrides Function ToString() As String
            Return "" + partNumber
        End Function
    End Structure

    Private Sub ListView1_ItemSelectionChanged(sender As System.Object, _
                                               e As Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        EnableButtons()
    End Sub

    Private Sub EnableButtons()
        If ListView1.SelectedItems.Count > 0 Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ListView1.DoubleClick
        OpenAndClose()
    End Sub

End Class
