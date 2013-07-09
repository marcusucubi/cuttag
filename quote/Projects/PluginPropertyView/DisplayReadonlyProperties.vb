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
    Private WithEvents _Settings As DisplaySettings = DisplaySettings.Instance

    Public Sub Execute() Implements IPluginMenuAction.Execute

        If Not DisplaySettings.Instance.HideReadOnlyProperties Then
            DisplaySettings.Instance.HideReadOnlyProperties = True
        Else
            DisplaySettings.Instance.HideReadOnlyProperties = False
        End If
        UpdateMenu()

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
        UpdateMenu()
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

    Private Sub UpdateMenu()

        If Not DisplaySettings.Instance.HideReadOnlyProperties Then
            m_ToolStripItem.Text = "Hide Detail"
            m_ToolStripItem.ToolTipText = "Hide read-only values"
        Else
            m_ToolStripItem.Text = "Show Detail"
            m_ToolStripItem.ToolTipText = "Show read-only values"
        End If

    End Sub

    Private Sub _Settings_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles _Settings.PropertyChanged
        UpdateMenu()
    End Sub

End Class
