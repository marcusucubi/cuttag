﻿Imports WeifenLuo.WinFormsUI.Docking
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Imports System.ComponentModel.Design

Public Class frmCustomProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _Header As Common.Header
    Private WithEvents _ComputationProperties As Common.ComputationProperties

    Private Sub frmCustom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        Me.UpdateProperties()
    End Sub

    Private Sub _Header_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Header.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub _Header_SavableChange(ByVal subject As Common.SaveableProperties) Handles _Header.SavableChange
        UpdateProperties()
    End Sub

    Private Sub _ComputationProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ComputationProperties.PropertyChanged
        Me.PropertyGrid2.Refresh()
    End Sub

    Private Sub _ComputationProperties_SavableChange(ByVal subject As Common.SaveableProperties) Handles _ComputationProperties.SavableChange
        Me.PropertyGrid2.Refresh()
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Dim o1 As Object = ActiveHeader.ActiveHeader.Header.CustomPropertiesGenerator
            Me.PropertyGrid1.SelectedObject = o1
            Dim o As Object = ActiveHeader.ActiveHeader.Header.CustomProperties
            Me.PropertyGrid2.SelectedObject = o
            Me._Header = ActiveHeader.ActiveHeader.Header
            Me._ComputationProperties = Me._Header.ComputationProperties
            If _Header.IsQuote Then
                Me.PropertyGrid1.Visible = False
            Else
                Me.PropertyGrid1.Visible = True
            End If
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            Me._Header = Nothing
            Me.PropertyGrid2.SelectedObject = Nothing
            Me._ComputationProperties = Nothing
        End If
    End Sub

End Class