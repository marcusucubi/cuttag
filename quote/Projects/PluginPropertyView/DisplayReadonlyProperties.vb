Imports System.Windows.Forms

Imports WeifenLuo.WinFormsUI.Docking

Imports PluginHost
Imports Model

<PluginMenuItem( _
    Text:="Hide Details", _
    Parent:="View", _
    MenuAnchor:="ViewSep1", _
    MenuPosition:=MenuPosition.Above
    )>
Public Class DisplayReadonlyProperties
    Implements IPluginMenuAction, IPluginMenuInit

    Private m_ToolStripItem As ToolStripItem
    Private m_Button As ToolStripButton
    Private m_Window As DockContent

    Public Sub Execute() Implements IPluginMenuAction.Execute

        If Not DisplaySettings.Instance.HideReadOnlyProperties Then

            m_ToolStripItem.Text = "Show Detail"
            m_ToolStripItem.ToolTipText = "Show read-only values"

            DisplaySettings.Instance.HideReadOnlyProperties = True
        Else
            m_ToolStripItem.Text = "Hide Detail"
            m_ToolStripItem.ToolTipText = "Hide read-only values"

            DisplaySettings.Instance.HideReadOnlyProperties = False
        End If

    End Sub

    Private Sub InitChild(ByVal frm As DockContent)
        PluginHost.App.DockPanel.SuspendLayout(True)
        frm.Show(PluginHost.App.DockPanel, DockState.DockRight)
        PluginHost.App.DockPanel.ResumeLayout(True, True)
    End Sub

    Public Overridable Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu
        m_ToolStripItem = menu
        Dim menuItem As ToolStripMenuItem = menu
        menuItem.ShortcutKeys = Keys.F11
    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton
        m_Button = button
        button.Enabled = False
    End Sub

    Private Sub InitChild(ByVal frm As DockContent, ByVal state As DockState)
        PluginHost.App.DockPanel.SuspendLayout(True)
        frm.Show(PluginHost.App.DockPanel, state)
        PluginHost.App.DockPanel.ResumeLayout(True, True)
    End Sub

End Class
