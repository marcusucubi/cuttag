Imports System.Windows.Forms

Public Class frmQuoteLookup

    Public Shared Property QuoteID As Long

    Private Sub frmQuoteOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim table As QuoteDataBase._QuoteDataTable
        table = New QuoteDataBaseTableAdapters._QuoteTableAdapter().GetDataByWithQuotes
        Me.ComboBox1.DataSource = table
        Me.ComboBox1.DisplayMember = "ID"
        GetQuoteID()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        GetQuoteID()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        GetQuoteID()
    End Sub

    Private Sub frmTemplateLookup_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        GetQuoteID()
    End Sub

    Private Sub GetQuoteID()
        Try
            If IsNumeric(Me.ComboBox1.Text) Then
                QuoteID = CLng(Me.ComboBox1.Text)
            End If
        Catch ex As Exception
            QuoteID = 0
        End Try
        If QuoteID = 0 Then
            Me.OK_Button.Enabled = False
        Else
            Me.OK_Button.Enabled = True
        End If
    End Sub

End Class
