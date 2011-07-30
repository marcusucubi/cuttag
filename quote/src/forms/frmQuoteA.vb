Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Common
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel
Imports DCS.Quote.Common
Imports System.Windows

Public Class frmQuoteA
    Inherits DockContent

    Private WithEvents _Header As Header
    Private WithEvents _PrimaryProperties As PrimaryPropeties
    Private WithEvents _DetailCollection As Common.DetailCollection(Of Common.Detail)
    Private WithEvents _ActiveDetail As ActiveDetail

    Private _ComponentsGroup As Forms.ListViewGroup = New Forms.ListViewGroup("Components", HorizontalAlignment.Left)
    Private _WiresGroup As Forms.ListViewGroup = New Forms.ListViewGroup("Wires", HorizontalAlignment.Left)

    Public ReadOnly Property QuoteHeader As Header
        Get
            Return _Header
        End Get
    End Property

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(ByVal Initials As String, ByVal d As String)
        Me.New(Nothing)
        Me._PrimaryProperties.CommonInitials = Initials
    End Sub

    Public Sub New(ByVal q As Common.Header)
        InitializeComponent()
        If q IsNot Nothing Then
            Me._Header = q
            Me._PrimaryProperties = q.PrimaryProperties
        Else
            Me._Header = New Model.Template.Header
            Me._PrimaryProperties = _Header.PrimaryProperties
        End If
        _DetailCollection = _Header.Details
        Me._ActiveDetail = ActiveDetail.ActiveDetail
        UpdateText()
    End Sub

    Private Sub frmQuoteA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me._Header.IsQuote Then
            Me.panelButtons.Visible = False
        End If
        Me.WireAndComponentView1.DetailCollection = _Header.Details
        EnableButtons()
    End Sub

    Private Sub btnAddComponent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComponent.Click
        Dim result As DialogResult = frmComponentLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As Detail
            detail = _Header.NewDetail(frmComponentLookup.Product)
        End If
    End Sub

    Private Sub bntAddWire_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAddWire.Click
        Dim result As DialogResult = frmWireLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As Detail
            detail = _Header.NewDetail(frmWireLookup.Product)
        End If
    End Sub

    Private Sub frmQuoteA_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ActiveHeader.ActiveHeader.Header = Me._Header
        EnableButtons()
    End Sub

    Private Sub frmQuoteA_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        ActiveHeader.ActiveHeader.Header = Nothing
        ActiveDetail.ActiveDetail.Detail = Nothing
        EnableButtons()
    End Sub

    Private Sub frmQuoteA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If Me.QuoteHeader.Dirty Then
            Dim msg As String
            msg = "Save changes from " + Me.QuoteHeader.DisplayName + "?"
            Dim r As MsgBoxResult = MsgBox(msg, MsgBoxStyle.YesNoCancel)
            If r = MsgBoxResult.Cancel Then
                e.Cancel = True
            ElseIf r = MsgBoxResult.Yes Then
                Dim saver As New TemplateSaver
                saver.Save(QuoteHeader)
            End If
        End If
    End Sub

    Private Sub frmQuoteA_MdiChildActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MdiChildActivate
        ActiveHeader.ActiveHeader.Header = Me._Header
    End Sub

    Private Sub _Header_SavableChange(ByVal subject As SaveableProperties) Handles _Header.SavableChange
        UpdateText()
    End Sub

    Private Sub _PrimaryProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _PrimaryProperties.PropertyChanged
        UpdateText()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim details As DetailCollection(Of Common.Detail) = ActiveHeader.ActiveHeader.Header.Details
        details.Remove(ActiveDetail.ActiveDetail.Detail)
    End Sub

    Private Sub _ActiveDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveDetail.PropertyChanged
        EnableButtons()
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

    Private Sub EnableButtons()
        If ActiveDetail.ActiveDetail.Detail Is Nothing Then
            Me.btnDelete.Enabled = False
        Else
            Me.btnDelete.Enabled = True
        End If
    End Sub

End Class
