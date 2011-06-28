
Namespace Model

    ''' <summary>
    ''' Represent a product
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Product

        Property Code As String
        Property UnitCost As Decimal
        Property UnitOfMeasure As UnitOfMeasure

        Public Sub New(ByVal Code As String, _
                       ByVal UnitCost As Decimal, _
                       ByVal UnitOfMeasure As UnitOfMeasure)
            Me.Code = Code
            Me.UnitCost = UnitCost
            Me.UnitOfMeasure = UnitOfMeasure
        End Sub

    End Class

End Namespace

