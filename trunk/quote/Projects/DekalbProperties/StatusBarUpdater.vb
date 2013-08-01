Imports Model

Imports System.Windows.Forms

Public Class StatusBarUpdater

    Private WithEvents _ActiveHeader As ActiveHeader
    Private WithEvents _PrimaryPropeties As Model.Common.PrimaryProperties
    Private WithEvents _OtherPropeties As Model.Common.OtherProperties

    Friend _IsNew As System.Windows.Forms.Label
    Friend ToolTip1 As System.Windows.Forms.ToolTip

    Public Sub Init()

        Me._ActiveHeader = ActiveHeader.Instance
        CreateLabel()
    End Sub

    Private Sub CreateLabel()
        Me._IsNew = New System.Windows.Forms.Label()
        Me._IsNew.AutoSize = True
        Me._IsNew.BackColor = System.Drawing.SystemColors.Info
        Me._IsNew.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._IsNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._IsNew.Location = New System.Drawing.Point(138, 5)
        Me._IsNew.MinimumSize = New System.Drawing.Size(0, 15)
        Me._IsNew.Name = "_IsNew"
        Me._IsNew.Size = New System.Drawing.Size(39, 15)
        Me._IsNew.TabIndex = 3
        Me._IsNew.Text = "None"
        Me._IsNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Host.App.StatusStripPanel.SuspendLayout()
        Host.App.StatusStripPanel.Controls.Add(Me._IsNew)
        Host.App.StatusStripToolTip.SetToolTip(Me._IsNew, "Is Quote New")
        Host.App.StatusStripPanel.ResumeLayout()

    End Sub


    Private Sub _ActiveHeader_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveHeader.PropertyChanged
        If (Me._PrimaryPropeties IsNot ActiveHeader.Instance.Header) Then
            If ActiveHeader.Instance.Header Is Nothing Then
                Me._PrimaryPropeties = Nothing
                Me._OtherPropeties = Nothing
            Else
                Me._PrimaryPropeties = ActiveHeader.Instance.Header.PrimaryProperties
                Me._OtherPropeties = ActiveHeader.Instance.Header.OtherProperties
            End If
            UpdateStatusBar()
        End If
    End Sub

    Private Sub _OtherPropeties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _OtherPropeties.PropertyChanged
        Me.UpdateStatusBar()
    End Sub

    Private Sub _Propeties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _PrimaryPropeties.PropertyChanged
        Me.UpdateStatusBar()
    End Sub

    Public Sub UpdateStatusBar()

        Dim sNoneText As String = "None"

        If Me._ActiveHeader.Header Is Nothing Then

            Me._IsNew.Text = sNoneText
        Else

            If (TypeOf Me._ActiveHeader.Header Is Model.Template.Header) Then

                If (TypeOf _ActiveHeader.Header.OtherProperties Is DekalbOtherProperties) Then

                    Dim other As DekalbOtherProperties = _
                        _ActiveHeader.Header.OtherProperties

                    If (other.IsNew) Then
                        Me._IsNew.Text = "New"
                    Else
                        Me._IsNew.Text = "Old"
                    End If

                Else
                    Me._IsNew.Text = "Quote"
                End If
            End If
        End If
    End Sub

End Class
