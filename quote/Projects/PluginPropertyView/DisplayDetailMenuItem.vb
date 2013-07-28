Imports System.Drawing

Imports Host

Imports WeifenLuo.WinFormsUI.Docking

<MenuItem( _
    Text:="Properties", _
    Parent:="View" _
   )>
Public Class DisplayDetailMenuItem
    Implements IMenuAction

    Public Overridable Sub Execute() Implements IMenuAction.Execute
        Dim t = ViewController.Instance.DetailProperties
    End Sub

End Class

