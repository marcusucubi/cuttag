﻿Imports System.Windows.Forms
Imports System.ComponentModel

Imports WeifenLuo.WinFormsUI.Docking

Imports Model
Imports Model.Template

Public Class frmComputationProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _Properties As Model.Common.ComputationProperties
    Private WithEvents _Settings As DisplaySettings = DisplaySettings.Instance

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, _
                                                 ByVal e As PropertyChangedEventArgs) _
                                                 Handles _Properties.PropertyChanged
        Me.PropertyGrid1.Refresh()
        UpdateButton()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        _Properties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.Instance
        UpdateProperties()
        UpdateButton()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.Instance.Header IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = _
                ActiveHeader.Instance.Header.ComputationProperties
            _Properties = _
                ActiveHeader.Instance.Header.ComputationProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing

            If Not IsNothing(_Properties) Then _Properties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.Instance
        UpdateProperties()
    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles FilterButton.Click
        DisplaySettings.Instance.HideReadOnlyProperties = Not DisplaySettings.Instance.HideReadOnlyProperties
    End Sub

    Private Sub UpdateButton()
        Me.FilterButton.Checked = DisplaySettings.Instance.HideReadOnlyProperties

        If Not DisplaySettings.Instance.HideReadOnlyProperties Then
            FilterButton.Text = "Hide Detail"
            FilterButton.ToolTipText = "Hide read-only values"
        Else
            FilterButton.Text = "Show Detail"
            FilterButton.ToolTipText = "Show read-only values"
        End If
    End Sub

    Private Sub _Settings_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _Settings.PropertyChanged
        UpdateButton()
    End Sub

End Class