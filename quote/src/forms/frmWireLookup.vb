Imports System.Windows.Forms
Imports DCS.Quote.Model

Public Class frmWireLookup

    Public Property Product As Product

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SelectProduct()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectProduct()

        Dim View As System.Data.DataRowView = Me.ListBox1.SelectedItem

        Dim num As String = View.Row.ItemArray(1)
        Dim cost As Decimal = View.Row.ItemArray(2)

        Product = New Product(num, cost, UnitOfMeasure.BY_LENGTH)
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPartLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me._WiresTableAdapter.Fill(Me.DevDataSet1._Wires)
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
