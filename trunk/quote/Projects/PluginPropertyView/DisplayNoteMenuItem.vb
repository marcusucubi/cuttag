Imports System.Drawing

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Notes", _
    Parent:="View", _
    MenuAnchor:="ViewSep1", _
    MenuPosition:=MenuPosition.Above
    )>
Public Class DisplayNoteMenuItem
    Implements IPluginMenuAction, HasIcon

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.NoteProperties
    End Sub

    Public Function GetImage() As Image Implements HasIcon.GetImage
        Return My.Resources.notes
    End Function

End Class

