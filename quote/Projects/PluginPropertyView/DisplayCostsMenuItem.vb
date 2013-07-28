Imports System.Drawing
Imports System.Windows.Forms

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Costs", _
    Parent:="View" _
    )>
Public Class DisplayCostsMenuItem
    Implements IPluginMenuAction, IPluginMenuInit, IHasIcon

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.ComputationProperties
    End Sub

    Public ReadOnly Property Image As Image Implements IHasIcon.Image
        Get
            Return My.Resources.dollar
        End Get
    End Property

    Public Overridable Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu
        Dim menuItem As ToolStripMenuItem = menu
        menuItem.ShortcutKeys = Keys.F12
    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton
        button.Enabled = False
    End Sub

End Class

