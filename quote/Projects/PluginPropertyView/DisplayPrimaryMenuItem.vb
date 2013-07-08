Imports System.Drawing

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Primary", _
    Parent:="View", _
    MenuAnchor:="ViewSep1", _
    MenuPosition:=MenuPosition.Above
    )>
Public Class DisplayPrimaryMenuItem
    Implements IPluginMenuAction

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.PrimaryProperties
    End Sub

End Class

