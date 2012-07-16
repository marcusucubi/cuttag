Imports System.Windows.Forms

''' <summary>
''' Dislays a list of quotes
''' </summary>
''' <remarks></remarks>
Public Class frmSimilarQuotes

    Private _Quotes As SimilarQuotes

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

        _Quotes = SimilarQuoteLoader.Load(targetId)

        For Each q As SimilarQuote In _Quotes
            Dim i As New ListViewItem()
            i.Name = q.id
            i.Text = "" & q.id
            ListView1.Items.Add(i)

            Dim sType As New ListViewItem.ListViewSubItem
            If q.AsIsQuote Then
                sType.Text = "Quote"
            Else
                sType.Text = "BOM"
            End If
            i.SubItems.Add(sType)
            
            Dim s1 As New ListViewItem.ListViewSubItem
            s1.Text = q.matchWires
            i.SubItems.Add(s1)

            Dim s2 As New ListViewItem.ListViewSubItem
            s2.Text = q.matchParts
            i.SubItems.Add(s2)
        Next

    End Sub

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

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        OpenAndClose()
    End Sub

End Class
