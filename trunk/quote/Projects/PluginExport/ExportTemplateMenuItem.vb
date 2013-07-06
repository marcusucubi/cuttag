Imports Model
Imports PluginHost

Imports System.Windows.Forms

<PluginMenuItem( _
    Text:="Export", _
    Parent:="Template", _
    Anchor:="TemplateSep4", _
    MenuPosition:=MenuPosition.Below
    )>
Public Class ExportTemplateMenuItem
    Implements IPluginMenuAction, IPluginMenuInit, HasIcon

    Private WithEvents m_Watch As Model.ActiveHeader
    Private m_ToolStripItem As ToolStripItem

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim export As New ExportBOM
        export.Export(ActiveHeader.ActiveHeader.Header)

    End Sub

    Public Sub Init(item As ToolStripItem) Implements IPluginMenuInit.Init

        m_Watch = Model.ActiveHeader.ActiveHeader
        m_ToolStripItem = item
        item.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.ActiveHeader.Header Is Model.BOM.Header) Then
            m_ToolStripItem.Enabled = True
        Else
            m_ToolStripItem.Enabled = False
        End If

    End Sub

    Public Function GetImage() As System.Drawing.Image Implements HasIcon.GetImage
        Return My.Resources.excel
    End Function

End Class
