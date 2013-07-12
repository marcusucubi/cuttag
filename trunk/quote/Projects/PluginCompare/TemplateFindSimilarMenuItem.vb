Imports System.Windows.Forms

Imports WeifenLuo.WinFormsUI.Docking

Imports PluginHost
Imports Model
Imports Doc

<PluginMenuItem( _
    Text:="Open Similar", _
    Parent:="Template" _
    )>
Public Class TemplateFindSimilarMenuItem
    Implements IPluginMenuAction, IPluginMenuInit

    Private WithEvents m_Watch As Model.ActiveHeader
    Private WithEvents m_Menu As ToolStripMenuItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IPluginMenuAction.Execute
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim frm As frmSimilarQuotes
        frm = New frmSimilarQuotes(ActiveHeader.ActiveHeader.Header.PrimaryProperties.CommonID)
        frm.ShowDialog(PluginHost.App.MainForm)

        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_Menu = menu
        menu.Enabled = False

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton

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

