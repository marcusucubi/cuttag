Imports PluginHost
Imports Model

Imports System.Windows.Forms

<PluginMenuItem( _
    Text:="Export", _
    Parent:="Quote", _
    Anchor:="QuoteSep3", _
    MenuPosition:=MenuPosition.Below
    )>
Public Class ExportQuoteMenuItem
    Implements IPluginMenuAction, IPluginMenuInit, HasIcon

    Private WithEvents m_Watch As Model.ActiveHeader
    Private m_ToolStripItem As ToolStripItem

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim frm As New frmExport
        frm.ShowDialog()

    End Sub

    Public Sub Init(item As ToolStripItem) Implements IPluginMenuInit.Init

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_ToolStripItem = item
        item.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.ActiveHeader.Header Is Model.Quote.Header) Then
            m_ToolStripItem.Enabled = True
        Else
            m_ToolStripItem.Enabled = False
        End If

    End Sub

    Public Function GetImage() As System.Drawing.Image Implements HasIcon.GetImage
        Return My.Resources.excel
    End Function

End Class
