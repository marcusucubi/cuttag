
Namespace Model

    ''' <summary>
    ''' Represent a product
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure Product

        Property Code As String
        Property UnitCost As Decimal
        Property ProductClass As ProductClass

        ReadOnly Property UnitOfMeasure As UnitOfMeasure
            Get
                Return IIf(ProductClass = Model.ProductClass.WIRE, UnitOfMeasure.LENGTH, UnitOfMeasure.PIECES)
            End Get
        End Property

        Public Sub New(ByVal Code As String, _
                       ByVal UnitCost As Decimal, _
                       ByVal ProductClass As ProductClass)
            Me.Code = Code
            Me.UnitCost = UnitCost
            Me.ProductClass = ProductClass
        End Sub

    End Structure

End Namespace

