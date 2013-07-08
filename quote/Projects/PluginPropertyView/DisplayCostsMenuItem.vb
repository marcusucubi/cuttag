Imports System.Drawing
Imports System.Windows.Forms

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Costs", _
    Parent:="View", _
    MenuAnchor:="ViewSep1", _
    MenuPosition:=MenuPosition.Above
    )>
Public Class DisplayCostsMenuItem
    Implements IPluginMenuAction, HasIcon

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.ComputationProperties
    End Sub

    Public Function GetImage() As Image Implements HasIcon.GetImage
        Return My.Resources.dollar
    End Function

End Class

