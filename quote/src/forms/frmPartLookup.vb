Imports System.Windows.Forms
Imports DCS.Quote.Model

Public Class frmComponentLookup

    Public Property Product As Product

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SelectProduct()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectProduct()

        Dim View As System.Data.DataRowView = Me.ListBox1.SelectedItem
        Dim row As QuoteDataBase.WireComponentSourceRow = View.Row

        Dim num As String = row.PartNumber
        Dim cost As Decimal = 0
        If Not row.IsQuotePriceNull Then
            cost = row.QuotePrice
        End If

        Product = New Product( _
            num, cost, "", UnitOfMeasure.BY_EACH, _
            Nothing, View.Row)
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPartLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim table As QuoteDataBase.WireComponentSourceDataTable
            table = New QuoteDataBaseTableAdapters.WireComponentSourceTableAdapter().GetData()
            Me.ListBox1.DataSource = table
            Me.ListBox1.DisplayMember = "PartNumber"
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox(ex.Message)
        End Try

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

End Class
