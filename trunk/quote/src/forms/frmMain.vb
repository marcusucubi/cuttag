Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Model
Imports System.ComponentModel

Public Class frmMain
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Private _Summary As frmProperties
    Private _QuoteProperties As QuoteProperties

    Public Shared Property frmMain As frmMain

    Public Property QuoteProperties As QuoteProperties
        Get
            Return _QuoteProperties
        End Get
        Set(ByVal value As QuoteProperties)
            _QuoteProperties = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("QuoteProperties"))
        End Set
    End Property

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim ChildForm As New frmQuoteA
        ChildForm.MdiParent = Me
        ChildForm.Show(Me.DockPanel1)
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        frmMain = Me
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If (_Summary Is Nothing) Then
            _Summary = New frmProperties
            InitChild(_Summary)
        End If
        If (_Summary.IsHidden Or _Summary.IsDisposed) Then
            _Summary = New frmProperties
            InitChild(_Summary)
        End If
    End Sub

    Private Sub InitChild(ByVal frm As DockContent)
        frm.MdiParent = frmMain.frmMain
        frm.Show(frmMain.frmMain.DockPanel1)
        frm.DockState = DockState.DockRight
    End Sub

End Class

