
Namespace Model

    ''' <summary>
    ''' Represent a product
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Product

        ReadOnly Property Code As String
            Get
                Return m_code
            End Get
        End Property

        ReadOnly Property Gage As String
            Get
                Return m_gage
            End Get
        End Property

        ReadOnly Property UnitCost As Decimal
            Get
                Return m_unitCost
            End Get
        End Property

        ReadOnly Property UnitOfMeasure As UnitOfMeasure
            Get
                Return m_unitOfMeasure
            End Get
        End Property

        Private m_code As String
        Private m_gage As String
        Private m_unitCost As Decimal
        Private m_unitOfMeasure As UnitOfMeasure

        Public Sub New( _
                       ByVal Code As String, _
                       ByVal UnitCost As Decimal, _
                       ByVal Gage As String, _
                       ByVal UnitOfMeasure As UnitOfMeasure _
                       )
            Me.m_code = Code
            Me.m_unitCost = UnitCost
            Me.m_gage = Gage
            Me.m_unitOfMeasure = UnitOfMeasure
        End Sub

    End Class

End Namespace

