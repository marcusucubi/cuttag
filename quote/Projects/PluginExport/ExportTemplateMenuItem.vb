﻿Imports System.Drawing
Imports System.Windows.Forms
Imports Model
Imports PluginHost

<PluginMenuItem( _
    Text:="Export Template", _
    Parent:="Template", _
    ShowInToolbar:=True
    )>
Public Class ExportTemplateMenuItem
    Implements IPluginMenuAction, IPluginMenuInit, IHasIcon

    Private WithEvents m_Watch As Model.ActiveHeader
    Private m_ToolStripItem As ToolStripItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim export As New ExportBOM
        export.Export(ActiveHeader.ActiveHeader.Header)

    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_ToolStripItem = menu
        menu.Enabled = False

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton

        m_Button = button
        m_Button.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.ActiveHeader.Header Is Model.Template.Header) Then
            m_ToolStripItem.Enabled = True
            m_Button.Enabled = True
        Else
            m_ToolStripItem.Enabled = False
            m_Button.Enabled = False
        End If

    End Sub
    
    Public ReadOnly property Image As Image Implements IHasIcon.Image
        Get
            Return My.Resources.excel
        End Get
    End property

End Class
