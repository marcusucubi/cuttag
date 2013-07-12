Imports Model

Imports System.Windows.Forms

Public Class StatusBarUpdater

    Private WithEvents _ActiveHeader As ActiveHeader
    Private WithEvents _PrimaryPropeties As Model.Common.PrimaryPropeties
    Private WithEvents _OtherPropeties As Model.Common.OtherProperties

    Friend _IsNew As System.Windows.Forms.Label

    Public Sub Init()

        Me._ActiveHeader = ActiveHeader.ActiveHeader

        Dim controls As Control()
        controls = PluginHost.App.StatusStrip.Controls.Find("_IsNew", True)
        If (controls.Length > 0) Then
            _IsNew = controls(0)
        End If

        Debug.Assert(Not _IsNew Is Nothing)

    End Sub

    Private Sub _ActiveHeader_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveHeader.PropertyChanged
        If (Me._PrimaryPropeties IsNot ActiveHeader.ActiveHeader.Header) Then
            If ActiveHeader.ActiveHeader.Header Is Nothing Then
                Me._PrimaryPropeties = Nothing
                Me._OtherPropeties = Nothing
            Else
                Me._PrimaryPropeties = ActiveHeader.ActiveHeader.Header.PrimaryProperties
                Me._OtherPropeties = ActiveHeader.ActiveHeader.Header.OtherProperties
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
