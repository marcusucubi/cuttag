Imports System.Windows.Forms
Imports DB.QuoteDataBase
Imports DB.QuoteDataBaseTableAdapters
Imports Model
Imports Model.IO.Misc

Public Class frmWireLookup

    Public Property Product As Product

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SelectProduct()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectProduct()
        Dim View As System.Data.DataRowView = Me.ListBox1.SelectedItem
        Dim row As DB.QuoteDataBase.WireSourceRow = View.Row
        Dim num As String = row.PartNumber
        Dim cost As Decimal = 0
        If Not row.IsQuotePriceNull Then
            cost = row.QuotePrice
        End If
        Dim gage As String = ""
        Dim gageTable As GageDataTable
        Dim gageAdaptor As New GageTableAdapter
        gageTable = gageAdaptor.GetDataByGageID(row.GageID)
        If gageTable IsNot Nothing Then
            Dim gageRow As GageRow = gageTable.Rows(0)
            gage = gageRow.Gage
        End If
        
        Product = ProductDB.Load( _
            num, cost, gage, True, _
            row, Nothing, "", 0)
        
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPartLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DoFilter()
        ListBox1.Focus()
    End Sub

    Protected Overrides Sub OnActivated(ByVal e As System.EventArgs)
        MyBase.OnActivated(e)
        Me.DoFilter()
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        SelectProduct()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ListBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedValueChanged
        If ListBox1.SelectedItems.Count > 0 Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        DoFilter()
        EnableButtons()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        EnableButtons()
    End Sub

    Private Sub DoFilter()

        Dim table As WireSourceDataTable

        If (Me.TextBox1.Text.Length > 0) Then
            Dim filter As String = "%" & Me.TextBox1.Text.Trim.ToUpper & "%"
            table = New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter().GetDataLikePartNumber(filter)
        Else
            table = New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter().GetData()
        End If
        Me.ListBox1.DataSource = table
        Me.ListBox1.DisplayMember = "PartNumber"
        EnableButtons()
    End Sub

    Private Sub EnableButtons()
        If ListBox1.SelectedItems.Count > 0 Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

End Class
