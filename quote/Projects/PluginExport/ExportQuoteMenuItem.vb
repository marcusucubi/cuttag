Imports System.Windows.Forms

Imports PluginHost
Imports Model

<PluginMenuItem( _
    Text:="Export", _
    Parent:="Quote", _
    MenuAnchor:="QuoteSep4", _
    ButtonAnchor:="QuoteSeparator1", _
    MenuPosition:=MenuPosition.Above
    )>
Public Class ExportQuoteMenuItem
    Implements IPluginMenuAction, IPluginMenuInit, HasIcon

    Private WithEvents m_Watch As Model.ActiveHeader
    Private m_ToolStripItem As ToolStripItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim frm As New frmExport
        frm.ShowDialog()

    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IPluginMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_ToolStripItem = menu
        menu.Enabled = False

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IPluginMenuInit.InitButton

        m_Button = button
        button.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.ActiveHeader.Header Is Model.Quote.Header) Then
            m_ToolStripItem.Enabled = True
            m_Button.Enabled = True
        Else
            m_ToolStripItem.Enabled = False
            m_Button.Enabled = False
        End If

    End Sub

    Public Function GetImage() As System.Drawing.Image Implements HasIcon.GetImage
        Return My.Resources.excel
    End Function

End Class
