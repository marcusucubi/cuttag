Imports System.ComponentModel
Imports System.Windows
Imports VB = Microsoft.VisualBasic

Imports WeifenLuo.WinFormsUI.Docking

Imports Model.Common
Imports Model

Imports DB.QuoteDataBase
Imports DB.QuoteDataBaseTableAdapters

Imports DCS.DataGrid
Imports DCS.SharedMethods

Public Class frmDocumentA
    Inherits DockContent
    Private WithEvents _Header As Header
    Private WithEvents _PrimaryProperties As PrimaryProperties
    Private WithEvents _DetailCollection As Model.Common.DetailCollection(Of Model.Common.Detail)
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

    Public Sub New(ByVal q As Model.Common.Header)
        InitializeComponent()
        If q IsNot Nothing Then
            Me._Header = q
            Me._PrimaryProperties = q.PrimaryProperties
        Else
            Me._Header = New Model.Template.Header
            Me._PrimaryProperties = _Header.PrimaryProperties
        End If
        _DetailCollection = _Header.Details
        Me._ActiveDetail = ActiveDetail.Instance
        UpdateText()
    End Sub

    Private Sub frmQuoteA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me._Header.IsQuote Then
            Me.panelButtons.Visible = False
            'dd_Added 9/15/11
            Me.WireAndComponentView1.dgvQuoteDetail.ReadOnly = True
            Me.WireAndComponentView1.dgvQuoteDetail.AllowUserToAddRows = False
            Me.WireAndComponentView1.dgvQuoteDetail.AllowUserToDeleteRows = False
        Else
            'dd_Added 10/7/11
            Me.panelButtons.Visible = False
            Dim ds As New DB.QuoteDataBase
            Dim daWire As New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter
            Dim daComp As New DB.QuoteDataBaseTableAdapters.WireComponentSourceTableAdapter
            Dim daSource As New DB.QuoteDataBaseTableAdapters.ItemSourceLookupListTableAdapter
            Dim daGage As New DB.QuoteDataBaseTableAdapters.GageTableAdapter
            Dim daUOM As New DB.QuoteDataBaseTableAdapters._UnitOfMeasureTableAdapter
            daWire.Fill(ds.WireSource)
            '            ds.EnforceConstraints = False
            daComp.Fill(ds.WireComponentSource)
            daSource.Fill(ds.ItemSourceLookupList)
            daUOM.Fill(ds._UnitOfMeasure)
            daGage.Fill(ds.Gage)
            Me.WireAndComponentView1.PartLookupDataSource = ds
            Me.WireAndComponentView1.PartLookupDataMember = "ItemSourceLookupList"
            'end dd_Added
        End If
        With Me.WireAndComponentView1
            .DetailCollection = _Header.Details
            .dgvQuoteDetail.Focus()

            WireAndComponentView1.SelectDetail()
        End With
        EnableButtons()
    End Sub

    Private Sub btnAddComponent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComponent.Click
        Dim frm As New frmComponentLookup()
        Dim result As DialogResult = frm.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As Detail
            detail = _Header.NewDetail(frm.Product)
        End If
    End Sub

    Private Sub bntAddWire_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAddWire.Click
        Dim frm As New frmWireLookup()
        Dim result As DialogResult = frm.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As Detail
            detail = _Header.NewDetail(frm.Product)
        End If
    End Sub

    Private Sub Me_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ActiveHeader.Instance.Header = Me._Header
        EnableButtons()
    End Sub

    Private Sub Me_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        ActiveHeader.Instance.Header = Nothing
        ActiveDetail.Instance.Detail = Nothing
        EnableButtons()
    End Sub

    Private Sub frmQuoteA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.QuoteHeader.IsDirty Then
            Dim msg As String
            msg = "Save changes from " + Me.QuoteHeader.DisplayName + "?"
            Dim r As MsgBoxResult = MsgBox(msg, MsgBoxStyle.YesNoCancel)
            If r = MsgBoxResult.Cancel Then
                e.Cancel = True
            ElseIf r = MsgBoxResult.Yes Then
                Model.IO.TemplateSaver.Save(QuoteHeader)
            End If
        End If
    End Sub

    Private Sub frmQuoteA_MdiChildActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MdiChildActivate
        ActiveHeader.Instance.Header = Me._Header
    End Sub

    Private Sub _Header_SavableChange(ByVal subject As SavableProperties, _
                                      args As EventArgs) _ 
                                      Handles _Header.Dirty
        UpdateText()
    End Sub

    Private Sub _Header_SavableClean(ByVal subject As SavableProperties, _
                                      args As EventArgs) _ 
                                      Handles _Header.Clean
        UpdateText()
    End Sub
    
    Private Sub _PrimaryProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _PrimaryProperties.PropertyChanged
        UpdateText()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim details As DetailCollection(Of Model.Common.Detail)
        details = ActiveHeader.Instance.Header.Details
        details.Remove(ActiveDetail.Instance.Detail)
    End Sub

    Private Sub _ActiveDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveDetail.PropertyChanged
        EnableButtons()
    End Sub

    Private Sub UpdateText()
        If Me._PrimaryProperties.CommonId > 0 Then
            If _Header.IsQuote Then
                Me.Text = "Quote " & Me._PrimaryProperties.CommonId
            Else
                Me.Text = "Template " & Me._PrimaryProperties.CommonId
            End If
        Else
            Me.Text = "New Template"
        End If
        If Me._Header.IsDirty Then
            Me.Text = Me.Text + " *"
        End If
    End Sub

    Private Sub EnableButtons()
        If ActiveDetail.Instance.Detail Is Nothing Then
            Me.btnDelete.Enabled = False
        Else
            Me.btnDelete.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddItem()
    End Sub
    Public Sub DeleteItem()
        Dim details As DetailCollection(Of Model.Common.Detail)
        details = ActiveHeader.Instance.Header.Details
        details.Remove(ActiveDetail.Instance.Detail)

    End Sub
    Public Sub AddItem()
        Dim gv As LookupDataGridView = Me.WireAndComponentView1.dgvQuoteDetail
        '      Dim pProduct As New Product( _
        '      "", 0, "", False, _
        '      Nothing, Nothing)
        'Dim dDetail As DCS.Quote.BOM.Detail = _Header.NewDetail(pProduct)
        gv.Focus()
        gv.CurrentCell = gv(gv.Columns("dgvQuoteDetail_Lookup").Index, gv.RowCount - 1)
        gv.BeginEdit(False)

    End Sub
End Class

