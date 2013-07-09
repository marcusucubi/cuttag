Imports Model

Public Class MainFormStatusStrip

    Private WithEvents _ActiveHeader As ActiveHeader
    Private WithEvents _PrimaryPropeties As Model.Common.PrimaryPropeties
    Private WithEvents _OtherPropeties As Model.Common.OtherProperties

    Public Sub New()
        InitializeComponent()
        Me._ActiveHeader = ActiveHeader.ActiveHeader
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
            Me._PartNumber.Text = sNoneText
            Me._RFQ.Text = sNoneText
            Me._QuoteDate.Text = sNoneText
            Me._IsNew.Text = sNoneText
        Else
            Dim part As String = Me._ActiveHeader.Header.PrimaryProperties.CommonPartNumber
            If (part = "") Then
                part = sNoneText
            End If
            Me._PartNumber.Text = part

            Dim rfc As String = Me._ActiveHeader.Header.PrimaryProperties.CommonRequestForQuoteNumber
            If (rfc = "") Then
                rfc = sNoneText
            End If
            Me._RFQ.Text = rfc

            Dim creaded As String = Me._ActiveHeader.Header.PrimaryProperties.CommonCreatedDate.ToShortDateString
            Me._QuoteDate.Text = creaded

            If (TypeOf Me._ActiveHeader.Header Is Model.Template.Header) Then
                Dim other As Model.Template.OtherProperties = _ActiveHeader.Header.OtherProperties
                If (other.IsNew) Then
                    Me._IsNew.Text = "New"
                Else
                    Me._IsNew.Text = "Old"
                End If
            Else
                Me._IsNew.Text = "Quote"
            End If
        End If
    End Sub

End Class
