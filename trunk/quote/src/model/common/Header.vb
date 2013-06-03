Imports DCS.Quote.Common

Namespace Common

	Public MustInherit Class Header
		Inherits SaveableProperties

		Protected _PrimaryProperties As Common.PrimaryPropeties
		Protected _OtherProperties As Common.OtherProperties
		Protected _ComputationProperties As Common.ComputationProperties
		Protected _CustomProperties As New Common.SaveableProperties
		Protected _NoteProperties As New Common.NoteProperties
        Protected WithEvents _Details As DetailCollection(Of Common.Detail)
		Public Property ID As Integer
		Public Property WeightProperties As New Common.Weights(Me)
        Public Property IsQuote As Boolean
        Public Sub New()
            _Details = New DetailCollection(Of Common.Detail)(Me)
        End Sub
        'dd_Added 10/8/11
        Public ReadOnly Property NextSequenceNumber
            Get
                Dim iMax As Integer = 0
                For Each dDetail As Detail In _Details
                    If dDetail.SequenceNumber > iMax Then iMax = dDetail.SequenceNumber
                Next
                iMax += 1
                Return iMax
            End Get
        End Property
		'dd_Added end
		Public ReadOnly Property ComputationProperties As Common.ComputationProperties
			Get
				Return _ComputationProperties
			End Get
		End Property

		Public ReadOnly Property OtherProperties As Common.OtherProperties
			Get
				Return _OtherProperties
			End Get
		End Property

		Public ReadOnly Property PrimaryProperties As Common.PrimaryPropeties
			Get
				Return _PrimaryProperties
			End Get
		End Property

		Public ReadOnly Property CustomProperties As Common.SaveableProperties
			Get
				Return _CustomProperties
			End Get
		End Property

		Public ReadOnly Property NoteProperties As Common.NoteProperties
			Get
				Return _NoteProperties
			End Get
		End Property

		Public ReadOnly Property Details As DetailCollection(Of Common.Detail)
			Get
				Return _Details
			End Get
		End Property

		Public ReadOnly Property DisplayName As String
			Get
				Dim s As String
				If IsQuote Then
					s = "Quote"
				Else
                    s = "Template"
				End If
				If Me.PrimaryProperties.CommonID = 0 Then
					Return "New " + s
				End If
				Return s & " " & Me.PrimaryProperties.CommonID
			End Get
		End Property

		Public MustOverride Function NewDetail(ByVal product As Model.Product) As Detail

    End Class

End Namespace