
Namespace Common

    ''' <summary>
    ''' Holds the data displayed in the options form.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GlobalOptions

        Public Event Changed As EventHandler

        Private Shared _Instance As New GlobalOptions
        Private Shared _DecimalPointsToDisplay As Integer = 4

        Public Shared ReadOnly Property Instance As GlobalOptions
            Get
                Return _Instance
            End Get
        End Property

        Public Shared Property DecimalPointsToDisplay As Integer
            Get
                Return _DecimalPointsToDisplay
            End Get
            Set(value As Integer)
                If (_DecimalPointsToDisplay <> value) Then
                    _DecimalPointsToDisplay = value
                    _Instance.FireChanged()
                End If
            End Set
        End Property

        Private Sub FireChanged()
            RaiseEvent Changed(Me, New EventArgs())
        End Sub

    End Class

End Namespace
