Imports System.Windows.Forms

Imports WeifenLuo.WinFormsUI.Docking

Imports Host
Imports Model
Imports Doc

<MenuItem( _
    Text:="Open Similar", _
    Parent:="Template" _
    )>
Public Class TemplateFindSimilarMenuItem
    Implements IMenuAction, IMenuInit

    Private WithEvents m_Watch As Model.ActiveHeader
    Private WithEvents m_Menu As ToolStripMenuItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IMenuAction.Execute
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim frm As frmSimilarQuotes
        frm = New frmSimilarQuotes(ActiveHeader.ActiveHeader.Header.PrimaryProperties.CommonId)
        frm.ShowDialog(Host.App.MainForm)

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_Menu = menu
        menu.Enabled = False

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IMenuInit.InitButton

        m_Button = button
        button.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.ActiveHeader.Header Is Model.Template.Header) Then
            m_Menu.Enabled = True
        Else
            m_Menu.Enabled = False
        End If

    End Sub

End Class

