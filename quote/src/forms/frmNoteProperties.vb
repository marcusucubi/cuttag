Imports WeifenLuo.WinFormsUI.Docking

Public Class frmNoteProperties
    Inherits DockContent

    Private WithEvents _ActiveQuote As ActiveHeader
    Private WithEvents _Notes As Common.NoteProperties

    Private Sub _frmForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ActiveQuote = ActiveHeader.ActiveHeader
        If (_ActiveQuote.Header IsNot Nothing) Then
            _Notes = _ActiveQuote.Header.NoteProperties
        End If
        UpdateProperties()

        Dim t As New QuoteDataBase._QuotePropertiesDataTable
        Dim max = t.PropertyStringValueColumn.MaxLength
        Me.TextBox1.MaxLength = max
        Me.txtNote2Customer.MaxLength = max

        Me.Panel1.Height = Me.Height / 2
    End Sub

    Private Sub _ActiveQuote_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveQuote.PropertyChanged
        _ActiveQuote = ActiveHeader.ActiveHeader
        If (_ActiveQuote.Header IsNot Nothing) Then
            _Notes = _ActiveQuote.Header.NoteProperties
        Else
            _Notes = Nothing
        End If
        UpdateProperties()
    End Sub

    Private Sub _Notes_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Notes.PropertyChanged
        UpdateProperties()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If _Notes IsNot Nothing Then
            Dim o As Object = _Notes
            If o.Note <> Me.TextBox1.Text Then
                o.Note = Me.TextBox1.Text
            End If
        End If
    End Sub
    Private Sub txtNote2Cust_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNote2Customer.KeyPress
        If _Notes IsNot Nothing Then
            Dim o As Object = _Notes
            If o.Note2Customer <> Me.txtNote2Customer.Text Then
                o.Note2Customer = Me.txtNote2Customer.Text
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If _Notes IsNot Nothing Then
            Dim o As Object = _Notes
            If o.Note <> Me.TextBox1.Text Then
                o.Note = Me.TextBox1.Text
            End If
        End If
    End Sub
    Private Sub txtNote2Customer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNote2Customer.TextChanged
        If _Notes IsNot Nothing Then
            Dim o As Object = _Notes
            If o.Note2Customer <> Me.txtNote2Customer.Text Then
                o.Note2Customer = Me.txtNote2Customer.Text
            End If
        End If
    End Sub

    Private Sub UpdateProperties()
        'dd_Changed 11/23//11 added Me.visible
        If _Notes IsNot Nothing AndAlso Me.Visible Then
            Dim o As Object = _Notes

            If o.GetType().GetProperty("Note") IsNot Nothing Then
                Me.TextBox1.Text = o.Note
                If o.GetType().GetProperty("Note").CanWrite Then
                    Me.TextBox1.ReadOnly = False
                Else
                    Me.TextBox1.ReadOnly = True
                End If
            End If

            If o.GetType().GetProperty("Note2Customer") IsNot Nothing Then
                Me.txtNote2Customer.Text = o.Note2Customer
                If o.GetType().GetProperty("Note2Customer").CanWrite Then
                    Me.txtNote2Customer.ReadOnly = False
                Else
                    Me.txtNote2Customer.ReadOnly = True
                End If
            End If
        Else

            If (Me.TextBox1.Text.Length > 0) Then
                Me.TextBox1.Text = ""
            End If
            Me.TextBox1.ReadOnly = True
            If (Me.txtNote2Customer.Text.Length > 0) Then
                Me.txtNote2Customer.Text = ""
            End If
            Me.txtNote2Customer.ReadOnly = True
        End If
    End Sub


End Class