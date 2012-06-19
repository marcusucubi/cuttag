Public Class frmImportPartsList
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ' Me.DialogResult = System.Windows.Forms.DialogResult.OK
        'If Me.cboPartLookup.SelectedIndex = -1 Then
        '    If MsgBox("You have not selected a valid part. Are you sure you want to close part import window?", vbYesNo) = MsgBoxResult.Yes Then
        '        Me.Close()
        '    End If
        'End If
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        '        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        '        Me.Close()
    End Sub

    Private Sub frmImportPartsList_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.OK And Me.cboPartLookup.SelectedIndex = -1 Then
            If MsgBox("You have not selected a valid part. Are you sure you want to close part import window?", vbYesNo) = MsgBoxResult.No Then
                e.Cancel = True
            End If
        End If


    End Sub

    Private Sub frmImportPartsList_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'ImportDataSet.HQ_GetParts4Lookup' table. You can move, or remove it, as needed.
        Me.HQ_GetParts4LookupTableAdapter.Fill(Me.ImportDataSet.HQ_GetParts4Lookup)

    End Sub


    Private Sub cboPartLookup_TextChanged(sender As Object, e As System.EventArgs) Handles cboPartLookup.TextChanged
        Debug.WriteLine("Selected Index" + Me.cboPartLookup.SelectedIndex.ToString)
        Debug.WriteLine("Text" + Me.cboPartLookup.Text)
        '        Debug.WriteLine("Selected Item" + Me.cboPartLookup.SelectedItem.ToString)


    End Sub

    Private Sub cboPartLookup_Validated(sender As Object, e As System.EventArgs) Handles cboPartLookup.Validated
        Debug.WriteLine("Validated" + Me.cboPartLookup.SelectedIndex.ToString)
        ' Debug.WriteLine("Selected Item" + Me.cboPartLookup.SelectedItem.ToString)

    End Sub

    Private Sub cboPartLookup_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboPartLookup.Validating
        Debug.WriteLine("Validating" + Me.cboPartLookup.SelectedIndex.ToString)
        If Me.cboPartLookup.SelectedIndex = -1 Then
            '         e.Cancel = True
        End If
    End Sub

    Private Sub cboPartLookup_ValueMemberChanged(sender As Object, e As System.EventArgs) Handles cboPartLookup.ValueMemberChanged
        Debug.WriteLine("Value Member Changed")

    End Sub
End Class