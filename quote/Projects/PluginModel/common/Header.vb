
Namespace Common

    Public MustInherit Class Header
        Inherits SaveableProperties

        Private _PrimaryProperties As Common.PrimaryProperties
        Private _OtherProperties As Common.OtherProperties
        Private _ComputationProperties As Common.ComputationProperties
        Private _CustomProperties As New Common.SaveableProperties
        Private _NoteProperties As New Common.NoteProperties

        Protected WithEvents _Details As DetailCollection(Of Common.Detail)

        Public Property Id As Integer
        Public Property IsQuote As Boolean

        Public Sub New()
            _Details = New DetailCollection(Of Common.Detail)(Me)
        End Sub

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

        Public ReadOnly Property PrimaryProperties As Common.PrimaryProperties
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

        Public ReadOnly Property DisplayName As String
            Get
                Dim s As String
                If IsQuote Then
                    s = "Quote"
                Else
                    s = "Template"
                End If
                If Me.PrimaryProperties.CommonId = 0 Then
                    Return "New " + s
                End If
                Return s & " " & Me.PrimaryProperties.CommonId
            End Get
        End Property

        Public MustOverride Function NewDetail(ByVal product As Model.Product) As Detail

        Protected Sub SetPrimaryProperties(ByVal value As Object)
            Me._PrimaryProperties = value
        End Sub
        
        Protected Sub SetComputationProperties(ByVal value As Object)
            Me._ComputationProperties = value
        End Sub

        Protected Sub SetOtherProperties(ByVal value As Object)
            Me._OtherProperties = value
        End Sub

        Protected Sub SetCustomProperties(ByVal value As Object)
            Me._CustomProperties = value
        End Sub

        Protected Sub SetNoteProperties(ByVal value As Object)
            Me._NoteProperties = value
        End Sub
        
    End Class

End Namespace