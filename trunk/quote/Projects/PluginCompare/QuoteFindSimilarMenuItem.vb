Imports System.Windows.Forms

Imports WeifenLuo.WinFormsUI.Docking

Imports Host.UI
Imports Model
Imports Doc

<MenuItem( _
    Text:="Open Similar", _
    Parent:="Quote" _
    )>
Public Class QuoteFindSimilarMenuItem
    Implements IMenuAction, IMenuInit

    Private WithEvents m_Watch As Model.ActiveHeader
    Private WithEvents m_Menu As ToolStripMenuItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IMenuAction.Execute
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim frm As frmSimilarQuotes
        frm = New frmSimilarQuotes(ActiveHeader.Instance.Header.PrimaryProperties.CommonId)
        frm.ShowDialog(Host.UI.UIApp.MainForm)

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.Instance
        m_Menu = menu
        menu.Enabled = False

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IMenuInit.InitButton

        m_Button = button
        button.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.Instance.Header Is Model.Quote.Header) Then
            m_Menu.Enabled = True
        Else
            m_Menu.Enabled = False
        End If

    End Sub

End Class

