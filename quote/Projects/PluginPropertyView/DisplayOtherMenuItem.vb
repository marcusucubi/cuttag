Imports System.Drawing

Imports Host.UI

Imports WeifenLuo.WinFormsUI.Docking

<MenuItem( _
    Text:="Other", _
    Parent:="View" _
    )>
Public Class DisplayOtherMenuItem
    Implements IMenuAction

    Public Overridable Sub Execute() Implements IMenuAction.Execute
        Dim t = ViewController.Instance.OtherProperties
    End Sub

End Class

