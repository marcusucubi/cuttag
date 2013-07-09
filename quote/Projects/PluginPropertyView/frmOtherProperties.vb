Imports WeifenLuo.WinFormsUI.Docking

Imports Model.Common
Imports Model

Public Class frmOtherProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _NonQuoteProperties As OtherProperties
    Private WithEvents _Settings As DisplaySettings = DisplaySettings.Instance

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _NonQuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        _NonQuoteProperties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
        UpdateButton()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = _
                ActiveHeader.ActiveHeader.Header.OtherProperties
            _NonQuoteProperties = _
                ActiveHeader.ActiveHeader.Header.OtherProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _NonQuoteProperties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        DisplaySettings.Instance.HideReadOnlyProperties = Not DisplaySettings.Instance.HideReadOnlyProperties
    End Sub

    Private Sub _Settings_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _Settings.PropertyChanged
        UpdateButton()
    End Sub

    Private Sub UpdateButton()
        Me.ToolStripButton1.Checked = DisplaySettings.Instance.HideReadOnlyProperties
        If Not DisplaySettings.Instance.HideReadOnlyProperties Then
            ToolStripButton1.Text = "Hide Detail"
            ToolStripButton1.ToolTipText = "Hide read-only values"
        Else
            ToolStripButton1.Text = "Show Detail"
            ToolStripButton1.ToolTipText = "Show read-only values"
        End If
    End Sub

End Class