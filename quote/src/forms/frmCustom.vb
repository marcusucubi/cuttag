Imports WeifenLuo.WinFormsUI.Docking
Imports DCS.Quote.Common.CustomProperties
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Imports System.ComponentModel.Design

Public Class frmCustom
    Inherits DockContent

    Private WithEvents _Header As Common.Header
    Private WithEvents _ComputationProperties As Common.ComputationProperties

    Private Sub frmCustom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim o As Object = ActiveHeader.ActiveHeader.Header.CustomPropertiesFactory
        Me.PropertyGrid1.SelectedObject = o
        Me.UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Dim o As Object = ActiveHeader.ActiveHeader.Header.CustomProperties
            Me.PropertyGrid2.SelectedObject = o
            Me._Header = ActiveHeader.ActiveHeader.Header
            Me._ComputationProperties = Me._Header.ComputationProperties
        Else
            Me._Header = Nothing
            Me.PropertyGrid2.SelectedObject = Nothing
            Me._ComputationProperties = Nothing
        End If
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

End Class