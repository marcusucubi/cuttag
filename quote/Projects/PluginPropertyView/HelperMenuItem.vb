Imports System.Windows.Forms
Imports System.Drawing

Imports WeifenLuo.WinFormsUI.Docking

Imports PluginHost
Imports Model

Public MustInherit Class HelperMenuItem
    Implements IPluginMenuAction, IPluginMenuInit, HasIcon

    Private m_ToolStripItem As ToolStripItem
    Private m_Button As ToolStripButton
    Private m_Window As DockContent

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute

        If (m_Window Is Nothing) Then
            m_Window = CreateForm()
            InitChild(m_Window)
        End If

        If (m_Window.IsHidden Or m_Window.IsDisposed) Then
            m_Window = CreateForm()
            InitChild(m_Window)
        End If

    End Sub

    Protected MustOverride Function CreateForm() As DockContent

    Public MustOverride Function GetImage() As Image Implements HasIcon.GetImage

    Public Property Window As DockContent
        Get
            Return m_Window
        End Get
        Set(value As DockContent)
            m_Window = value
        End Set
    End Property

    Private Sub InitChild(ByVal frm As DockContent)
        PluginHost.App.DockPanel.SuspendLayout(True)
        frm.Show(PluginHost.App.DockPanel, DockState.DockRight)
        PluginHost.App.DockPanel.ResumeLayout(True, True)
    End Sub

    Public Overridable Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu
        m_ToolStripItem = menu
    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton
        m_Button = button
        button.Enabled = False
    End Sub

End Class

