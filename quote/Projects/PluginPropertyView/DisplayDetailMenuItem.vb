Imports System.Drawing

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Properties", _
    Parent:="View" _
   )>
Public Class DisplayDetailMenuItem
    Implements IPluginMenuAction

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.DetailProperties
    End Sub

End Class

