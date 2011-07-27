Imports WeifenLuo.WinFormsUI.Docking

Public Class frmCustom
    Inherits DockContent

    Private Sub frmCustom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.UpdateProperties()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                         Handles btnNew.Click
        frmCustomNew.ShowDialog()
        If frmCustomNew.DialogResult = DialogResult.OK Then
            Dim d As Common.Header = ActiveHeader.ActiveHeader.Header
            d.CustomProperties.Add(frmCustomNew.Name)
        End If
    End Sub

    Private Sub UpdateProperties()
        If ActiveHeader.ActiveHeader.Header IsNot Nothing Then
            Me.PropertyGrid1.SelectedObject = _
                ActiveHeader.ActiveHeader.Header.CustomProperties
        Else
            Me.PropertyGrid1.SelectedObject = Nothing
        End If
    End Sub

End Class