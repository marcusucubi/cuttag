Imports System.Windows.Forms
Imports DCS.Quote.Model

Public Class frmPartLookup

    Public Property Product As Product

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim View As System.Data.DataRowView = Me.ListBox1.SelectedItem
        Dim num As String = View.Row.ItemArray(1)
        Dim cost As Decimal = View.Row.ItemArray(2)

        Product = New Product(num, cost, ProductClass.WIRE)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPartLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me._PartsTableAdapter.Fill(Me.DevDataSet._Parts)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
