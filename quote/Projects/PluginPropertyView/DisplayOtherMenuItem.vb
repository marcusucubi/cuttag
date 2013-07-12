Imports System.Drawing

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Other", _
    Parent:="View" _
    )>
Public Class DisplayOtherMenuItem
    Implements IPluginMenuAction

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.OtherProperties
    End Sub

End Class

