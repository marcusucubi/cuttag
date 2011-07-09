Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Common
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel

Public Class frmQuoteA
    Inherits DockContent

    Private WithEvents _QuoteHeader As New QuoteHeader
    Private WithEvents _PrimaryProperties As PrimaryPropeties

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(ByVal q As Model.QuoteHeader)
        InitializeComponent()
        If q IsNot Nothing Then
            Me._QuoteHeader = q
            Me._PrimaryProperties = q.PrimaryProperties
        Else
            Me._PrimaryProperties = _QuoteHeader.PrimaryProperties
        End If
        Me.HeaderSource.Add(_QuoteHeader)
        Me.gridDetail.DataSource = _QuoteHeader.QuoteDetails
        UpdateText()
    End Sub

    Public ReadOnly Property QuoteHeader As QuoteHeader
        Get
            Return _QuoteHeader
        End Get
    End Property

    Private Sub gridDetail_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridDetail.RowEnter
        Dim view As DataGridViewRow = gridDetail.Rows(e.RowIndex)
        Dim detail As QuoteDetail = view.DataBoundItem
        ActiveTemplateDetail.ActiveTemplateDetail.QuoteDetail = detail
    End Sub

    Private Sub gridDetail_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        Dim c As DataGridViewColumn = Me.gridDetail.Columns(e.ColumnIndex)
        Dim name As String = c.DataPropertyName
        Me._QuoteHeader.QuoteDetails.Sort = name
    End Sub

    Private Sub btnAddComponent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComponent.Click
        Dim result As DialogResult = frmComponentLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As QuoteDetail
            detail = _QuoteHeader.NewQuoteDetail(frmComponentLookup.Product)
            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

    Private Sub bntAddWire_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAddWire.Click
        Dim result As DialogResult = frmWireLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As QuoteDetail
            detail = _QuoteHeader.NewQuoteDetail(frmWireLookup.Product)
            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

    Private Sub frmQuoteA_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ActiveTemplate.ActiveTemplate.QuoteHeader = Me._QuoteHeader
    End Sub

    Private Sub frmQuoteA_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        ActiveTemplate.ActiveTemplate.QuoteHeader = Nothing
        ActiveTemplateDetail.ActiveTemplateDetail.QuoteDetail = Nothing
    End Sub

    Private Sub frmQuoteA_MdiChildActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MdiChildActivate
        ActiveTemplate.ActiveTemplate.QuoteHeader = Me._QuoteHeader
    End Sub

    Private Sub _QuoteHeader_SavableChange(ByVal subject As SaveableProperties) Handles _QuoteHeader.SavableChange
        UpdateText()
    End Sub

    Private Sub _PrimaryProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _PrimaryProperties.PropertyChanged
        UpdateText()
    End Sub

    Public Sub UpdateText()
        If Me._PrimaryProperties.QuoteNumnber > 0 Then
            If _QuoteHeader.IsQuote Then
                Me.Text = "Quote " & Me._PrimaryProperties.QuoteNumnber
            Else
                Me.Text = "Template " & Me._PrimaryProperties.QuoteNumnber
            End If
        Else
            Me.Text = "New Template"
        End If
        If Me._QuoteHeader.Dirty Then
            Me.Text = Me.Text + " *"
        End If
    End Sub

End Class
