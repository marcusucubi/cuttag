Imports System.Drawing

Imports Host

Imports WeifenLuo.WinFormsUI.Docking

<MenuItem( _
    Text:="Notes", _
    Parent:="View" _
    )>
Public Class DisplayNoteMenuItem
    Implements IMenuAction, IHasIcon

    Public Overridable Sub Execute() Implements IMenuAction.Execute
        Dim t = ViewController.Instance.NoteProperties
    End Sub

    Public ReadOnly Property Image As Image Implements IHasIcon.Image
        Get
            Return My.Resources.notes
        End Get
    End property

End Class

