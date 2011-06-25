
Namespace Model

    ''' <summary>
    ''' Represent a product
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure Product

        Property Code As String
        Property UnitPrice As Decimal

        Public Sub New(ByVal Code As String, ByVal UnitPrice As Decimal)
            Me.Code = Code
            Me.UnitPrice = UnitPrice
        End Sub

    End Structure

End Namespace

