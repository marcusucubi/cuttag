Imports System.Drawing

Imports PluginHost

Imports WeifenLuo.WinFormsUI.Docking

<PluginMenuItem( _
    Text:="Notes", _
    Parent:="View" _
    )>
Public Class DisplayNoteMenuItem
    Implements IPluginMenuAction, IHasIcon

    Public Overridable Sub Execute() Implements IPluginMenuAction.Execute
        Dim t = ViewController.Instance.NoteProperties
    End Sub

    Public ReadOnly Property Image As Image Implements IHasIcon.Image
        Get
            Return My.Resources.notes
        End Get
    End property

End Class

