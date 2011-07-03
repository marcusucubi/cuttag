﻿Imports DCS.Quote.Model
Imports WeifenLuo.WinFormsUI.Docking

Public Class frmNonComputationProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveQuote
    Private WithEvents _NonQuoteProperties As NonComputationProperties

    Private Sub _QuoteProperties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _NonQuoteProperties.PropertyChanged
        Me.PropertyGrid1.Refresh()
    End Sub

    Private Sub frmProperties_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me._ActiveQuote = Nothing
        Me.PropertyGrid1.SelectedObject = Nothing
        _NonQuoteProperties = Nothing
    End Sub

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

    Private Sub UpdateProperties()
        If ActiveQuote.ActiveQuote.QuoteHeader IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = ActiveQuote.ActiveQuote.QuoteHeader.NonComputationProperties
            _NonQuoteProperties = ActiveQuote.ActiveQuote.QuoteHeader.NonComputationProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
            _NonQuoteProperties = Nothing
        End If
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveQuote.ActiveQuote
        UpdateProperties()
    End Sub

End Class