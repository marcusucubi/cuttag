Imports System.Drawing

Imports Host

Imports WeifenLuo.WinFormsUI.Docking

<MenuItem( _
    Text:="Primary", _
    Parent:="View" _
    )>
Public Class DisplayPrimaryMenuItem
    Implements IMenuAction

    Public Overridable Sub Execute() Implements IMenuAction.Execute
        Dim t = ViewController.Instance.PrimaryProperties
    End Sub

End Class

