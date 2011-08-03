
Namespace Model

    ''' <summary>
    ''' Represent a product
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Product

        Private m_code As String
        Private m_gage As String
        Private m_unitCost As Decimal
        Private m_unitOfMeasure As UnitOfMeasure
        Private m_Description As String
        Private m_LeadTime As Integer
        Private m_Vendor As String

        Public Sub New( _
                       ByVal Code As String, _
                       ByVal UnitCost As Decimal, _
                       ByVal Gage As String, _
                       ByVal UnitOfMeasure As UnitOfMeasure, _
                       ByVal WireRow As QuoteDataBase.WireSourceRow, _
                       ByVal PartRow As QuoteDataBase._PartsRow
                       )
            Me.m_code = Code
            Me.m_unitCost = UnitCost
            Me.m_gage = Gage
            Me.m_unitOfMeasure = UnitOfMeasure
            If PartRow IsNot Nothing Then
                If Not PartRow.IsDescriptionNull Then
                    Me.m_Description = PartRow.Description
                End If
                If Not PartRow.IsLeadTimeNull Then
                    Me.m_LeadTime = PartRow.LeadTime
                End If
                If Not PartRow.IsVendorNull Then
                    Me.m_Vendor = PartRow.Vendor
                End If
            End If
        End Sub

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

        Property UnitCost As Decimal
            Get
                Return m_unitCost
            End Get
            Set(ByVal value As Decimal)
                m_unitCost = Math.Round(value, 2)
            End Set
        End Property

        ReadOnly Property UnitOfMeasure As UnitOfMeasure
            Get
                Return m_unitOfMeasure
            End Get
        End Property

        Public Overloads ReadOnly Property Description() As String
            Get
                Return m_Description
            End Get
        End Property

        Public Overloads ReadOnly Property LeadTime() As Integer
            Get
                Return m_LeadTime
            End Get
        End Property

        Public Overloads ReadOnly Property Vendor() As String
            Get
                Return m_Vendor
            End Get
        End Property

    End Class

End Namespace

