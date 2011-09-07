
Namespace Model
    Public Class UnitOfMeasure

        Public Shared ReadOnly BY_EACH As UnitOfMeasure = New UnitOfMeasure("Each")
        Public Shared ReadOnly BY_LENGTH As UnitOfMeasure = New UnitOfMeasure("Length")

        Public ReadOnly value As String
        Private Sub New(ByVal value As String)
            Me.value = value
        End Sub

        Public Overrides Function ToString() As String
            Return value
        End Function

        Public Shared Operator =(ByVal left As UnitOfMeasure, _
                                 ByVal right As UnitOfMeasure) As Boolean
            Return left.value = right.value
        End Operator

        Public Shared Operator <>(ByVal left As UnitOfMeasure, _
                                 ByVal right As UnitOfMeasure) As Boolean
            Return left.value <> right.value
        End Operator

    End Class
End Namespace