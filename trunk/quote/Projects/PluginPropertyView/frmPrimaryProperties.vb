Imports WeifenLuo.WinFormsUI.Docking

Imports Model
Imports Model.Common

Public Class frmPrimaryProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _Properties As PrimaryPropeties
    Private WithEvents _Settings As DisplaySettings = DisplaySettings.Instance

    Private Sub _PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Properties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub _Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        _Properties = Nothing
    End Sub

    Private Sub _Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
        UpdateButton()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveHeader.ActiveHeader.Header.PrimaryProperties
            _Properties = ActiveHeader.ActiveHeader.Header.PrimaryProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _Properties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        DisplaySettings.Instance.HideReadOnlyProperties = Not DisplaySettings.Instance.HideReadOnlyProperties
    End Sub

    Private Sub UpdateButton()
        Me.ToolStripButton1.Checked = DisplaySettings.Instance.HideReadOnlyProperties
    End Sub

    Private Sub _Settings_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _Settings.PropertyChanged
        UpdateButton()
    End Sub

End Class