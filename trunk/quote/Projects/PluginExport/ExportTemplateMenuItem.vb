Imports System.Drawing
Imports System.Windows.Forms

Imports Model
Imports Host.UI

<MenuItem( _
    Text:="Export Template", _
    Parent:="Template", _
    ShowInToolbar:=True
    )>
Public Class ExportTemplateMenuItem
    Implements IMenuAction, IMenuInit, IHasIcon

    Private WithEvents m_Watch As Model.ActiveHeader
    Private m_ToolStripItem As ToolStripItem
    Private m_Button As ToolStripButton

    Public Sub Execute() Implements IMenuAction.Execute

        Dim export As New ExportBOM
        export.Export(ActiveHeader.Instance.Header)

    End Sub

    Public Sub InitMenu(menu As ToolStripItem) Implements IMenuInit.InitMenu

        m_Watch = Model.ActiveHeader.Instance
        m_ToolStripItem = menu
        menu.Enabled = False

    End Sub

    Public Sub InitButton(button As ToolStripButton) Implements IMenuInit.InitButton

        m_Button = button
        m_Button.Enabled = False

    End Sub

    Private Sub m_Watch_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles m_Watch.PropertyChanged

        If (TypeOf ActiveHeader.Instance.Header Is Model.Template.Header) Then
            m_ToolStripItem.Enabled = True
            m_Button.Enabled = True
        Else
            m_ToolStripItem.Enabled = False
            m_Button.Enabled = False
        End If

    End Sub
    
    Public ReadOnly property Image As Image Implements IHasIcon.Image
        Get
            Return My.Resources.excel
        End Get
    End property

End Class
