﻿Imports System.Windows.Forms

Imports PluginHost
Imports Model
Imports WeifenLuo.WinFormsUI.Docking
Imports Doc

<PluginMenuItem( _
    Text:="Compare With", _
    Parent:="Template", _
    MenuAnchor:="TemplateSep2", _
    MenuPosition:=MenuPosition.Below
    )>
Public Class TemplateCompareWithMenuItem
    Implements IPluginMenuAction, IPluginMenuInit

    Private WithEvents m_Watch As Model.ActiveHeader
    Private WithEvents m_Menu As ToolStripMenuItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IPluginMenuAction.Execute

    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_Menu = menu
        menu.Enabled = False
        menu.Image = My.Resources.Scales
        menu.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

        Dim dropDown As ToolStripMenuItem
        dropDown = menu
        Dim holder As New ToolStripMenuItem("Holder")
        dropDown.DropDownItems.Add(holder)

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton

        m_Button = button
        button.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.ActiveHeader.Header Is Model.Template.Header) Then
            m_Menu.Enabled = True
        Else
            m_Menu.Enabled = False
        End If

    End Sub

    Private Sub m_Menu_DropDownOpening(sender As Object, e As System.EventArgs) Handles m_Menu.DropDownOpening
        Dim menu As ToolStripMenuItem = DirectCast(sender,  _
            ToolStripMenuItem)

        AddMenuItemsForOpenQuotes(menu)
    End Sub

    Private Sub AddMenuItemsForOpenQuotes(menu As ToolStripMenuItem)
        menu.DropDownItems.Clear()
        For Each d As DockContent In PluginHost.App.DockPanel.Documents

            If Not (TypeOf d Is frmDocumentA) Then
                Continue For
            End If

            Dim doc As frmDocumentA = d

            If Model.ActiveHeader.ActiveHeader.Header Is doc.QuoteHeader Then
                Continue For
            End If

            Dim name As String = doc.QuoteHeader.DisplayName

            Dim new_item As New CompareTemplateMenuItem(name, doc.QuoteHeader)
            menu.DropDownItems.Add(new_item)
            AddHandler new_item.Click, AddressOf CompareWithToolStripMenuItem_Click
        Next
    End Sub

    Private Sub CompareWithToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

        Dim menu As CompareTemplateMenuItem = DirectCast(sender, CompareTemplateMenuItem)

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim frmCompare As frmCompare
        frmCompare = New frmCompare(Model.ActiveHeader.ActiveHeader.Header, menu.Header)
        frmCompare.MdiParent = PluginHost.App.MainForm
        frmCompare.Show(PluginHost.App.DockPanel)

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub


End Class

