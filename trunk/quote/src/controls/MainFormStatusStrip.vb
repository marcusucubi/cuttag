Public Class MainFormStatusStrip

    Private WithEvents _ActiveHeader As ActiveHeader
    Private WithEvents _Propeties As Common.PrimaryPropeties

    Public Sub New()
        InitializeComponent()
        Me._ActiveHeader = ActiveHeader.ActiveHeader
    End Sub

    Private Sub _ActiveHeader_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _ActiveHeader.PropertyChanged
        If (Me._Propeties IsNot ActiveHeader.ActiveHeader.Header) Then
            If ActiveHeader.ActiveHeader.Header Is Nothing Then
                Me._Propeties = Nothing
            Else
                Me._Propeties = ActiveHeader.ActiveHeader.Header.PrimaryProperties
            End If
            UpdateStatusBar()
        End If
    End Sub

    Private Sub _Propeties_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _Propeties.PropertyChanged
        Me.UpdateStatusBar()
    End Sub

    Public Sub UpdateStatusBar()
        Dim sNoneText As String = "None"
        If Me._ActiveHeader.Header Is Nothing Then
            Me.sslblPartNumber.Text = sNoneText
            Me.sslblRFQ.Text = sNoneText
            Me.sslblQuoteDate.Text = sNoneText
        Else
            Dim part As String = Me._ActiveHeader.Header.PrimaryProperties.CommonPartNumber
            If (part = "") Then
                part = sNoneText
            End If
            Me.sslblPartNumber.Text = part

            Dim rfc As String = Me._ActiveHeader.Header.PrimaryProperties.CommonRequestForQuoteNumber
            If (rfc = "") Then
                rfc = sNoneText
            End If
            Me.sslblRFQ.Text = rfc

            Dim creaded As String = Me._ActiveHeader.Header.PrimaryProperties.CommonCreatedDate.ToShortDateString
            Me.sslblQuoteDate.Text = creaded
        End If
    End Sub

End Class
