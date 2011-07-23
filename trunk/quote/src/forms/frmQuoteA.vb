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
        Me.HeaderSource.Add(_Header)
        'Me.gridDetail.DataSource = _Header.Details
        _DetailCollection = _Header.Details
        UpdateText()
    End Sub

    Private Sub frmQuoteA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me._Header.IsQuote Then
            Me.panelButtons.Visible = False
            'Me.gridDetail.ReadOnly = True
        End If
        'Me.ListView1.Groups.Add(_ComponentsGroup)
        'Me.ListView1.Groups.Add(_WiresGroup)
        Me.WireAndComponentView1.DetailCollection = _Header.Details
        Sync()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If ListView1.SelectedItems.Count > 0 Then
        'Dim i As Common.Detail
        'i = ListView1.SelectedItems(0).Tag
        'ActiveDetail.ActiveDetail.Detail = i
        'Else
        'ActiveDetail.ActiveDetail.Detail = Nothing
        'End If
    End Sub

    Private Sub gridDetail_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Dim view As DataGridViewRow = gridDetail.Rows(e.RowIndex)
        'Dim detail As Detail = view.DataBoundItem
        'ActiveDetail.ActiveDetail.Detail = detail
    End Sub

    Private Sub gridDetail_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        'Dim c As DataGridViewColumn = Me.gridDetail.Columns(e.ColumnIndex)
        'Dim name As String = c.DataPropertyName
        'Me._Header.Details.Sort = name
    End Sub

    Private Sub btnAddComponent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddComponent.Click
        Dim result As DialogResult = frmComponentLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As Detail
            detail = _Header.NewDetail(frmComponentLookup.Product)
            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

    Private Sub bntAddWire_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAddWire.Click
        Dim result As DialogResult = frmWireLookup.ShowDialog(Me)
        If result = DialogResult.OK Then
            Dim detail As Detail
            detail = _Header.NewDetail(frmWireLookup.Product)
            Me.DetailSource.Add(detail)
            Me.DetailSource.MoveNext()
        End If
    End Sub

    Private Sub frmQuoteA_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ActiveHeader.ActiveHeader.Header = Me._Header
    End Sub

    Private Sub frmQuoteA_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        ActiveHeader.ActiveHeader.Header = Nothing
        ActiveDetail.ActiveDetail.Detail = Nothing
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

    Private Sub _DetailCollection_ListChanged(ByVal sender As Object, _
                                              ByVal e As System.ComponentModel.ListChangedEventArgs) _
                                          Handles _DetailCollection.ListChanged
        Sync()
    End Sub

    Public Sub UpdateText()
        If Me._PrimaryProperties.CommonID > 0 Then
            If _Header.IsQuote Then
                Me.Text = "Quote " & Me._PrimaryProperties.CommonID
            Else
                Me.Text = "Template " & Me._PrimaryProperties.CommonID
            End If
        Else
            Me.Text = "New Template"
        End If
        If Me._Header.Dirty Then
            Me.Text = Me.Text + " *"
        End If
    End Sub

    Private Sub Sync()

        Dim addList As New List(Of Common.Detail)

        'For Each o As Common.Detail In Me._DetailCollection
        'Dim test As Common.Detail = Nothing
        'For Each item As ListViewItem In Me.ListView1.Items
        'If item.Tag Is o Then
        'test = item.Tag
        'End If
        'Next
        'If test Is Nothing Then
        'addList.Add(o)
        'End If
        'Next

        'For Each o As Common.Detail In addList
        'Dim i As New ListViewItem
        'i.Name = o.ProductCode
        'i.Text = o.ProductCode
        'i.Tag = o

        'Dim su As New ListViewItem.ListViewSubItem
        'su.Text = o.DisplayableProductClass
        'su.Name = "Type"
        'i.SubItems.Add(su)

        'su = New ListViewItem.ListViewSubItem
        'su.Text = Math.Round(o.Qty)
        'su.Name = "Quantity"
        'i.SubItems.Add(su)

        'su = New ListViewItem.ListViewSubItem
        'su.Text = Math.Round(o.UnitCost, 2)
        'su.Name = "UnitCost"
        'i.SubItems.Add(su)

        'su = New ListViewItem.ListViewSubItem
        'su.Text = Math.Round(o.TotalCost, 2)
        'su.Name = "TotalCost"
        'i.SubItems.Add(su)

        'If o.Product.UnitOfMeasure = UnitOfMeasure.BY_EACH Then
        '    i.Group = _ComponentsGroup
        'Else
        '    i.Group = _WiresGroup
        'End If
        ''ListView1.BeginUpdate()
        ''ListView1.Items.Add(i)
        ''ListView1.EndUpdate()
        ''ListView1.Refresh()
        'Next

    End Sub

End Class
