
Namespace Model

    ''' <summary>
    ''' Represent a product
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Product

        Private m_code As String
        Private m_gage As String
        Private m_unitCost As Decimal
        Private m_machineTime As Decimal
        Private m_unitOfMeasure As UnitOfMeasure
        Private m_Description As String
        Private m_LeadTime As Integer
        Private m_Vendor As String
        Private m_MinimumQty As Decimal
        Private m_MinimumDollar As Decimal

        Public Sub New( _
                     ByVal m_code As String,
                     ByVal m_gage As String,
                     ByVal m_unitCost As Decimal,
                     ByVal m_machineTime As Decimal,
                     ByVal m_unitOfMeasure As UnitOfMeasure,
                     ByVal m_Description As String,
                     ByVal m_LeadTime As Integer,
                     ByVal m_Vendor As String,
                     ByVal m_MinimumQty As Decimal,
                     ByVal m_MinimumDollar As Decimal
                       )
            Me.m_code = m_code
            Me.m_gage = m_gage
            Me.m_unitCost = m_unitCost
            Me.m_machineTime = m_machineTime
            Me.m_unitOfMeasure = m_unitOfMeasure
            Me.m_Description = m_Description
            Me.m_LeadTime = m_LeadTime
            Me.m_Vendor = m_Vendor
            Me.m_MinimumQty = m_MinimumQty
            Me.m_MinimumDollar = m_MinimumDollar
        End Sub

        Public Sub New( _
                       ByVal Code As String, _
                       ByVal UnitCost As Decimal, _
                       ByVal Gage As String, _
                       ByVal UnitOfMeasure As UnitOfMeasure, _
                       ByVal WireRow As QuoteDataBase.WireSourceRow, _
                       ByVal PartRow As QuoteDataBase.WireComponentSourceRow
                       )
            Me.m_code = Code
            Me.m_unitCost = UnitCost
            Me.m_gage = Gage
            Me.m_unitOfMeasure = UnitOfMeasure
            If PartRow IsNot Nothing Then
                Me.m_Description = PartRow.Description
                If Not PartRow.IsLeadTimeNull Then
                    Me.m_LeadTime = PartRow.LeadTime
                End If
                If Not PartRow.IsVendorNull Then
                    Me.m_Vendor = PartRow.Vendor
                End If
                If Not PartRow.IsMachineTimeNull Then
                    Me.m_machineTime = PartRow.MachineTime
                End If
                If Not PartRow.IsMinimumQtyNull Then
                    Me.m_MinimumQty = PartRow.MinimumQty
                End If
                If Not PartRow.IsMinimumDollarNull Then
                    Me.m_MinimumDollar = PartRow.MinimumDollar
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
                m_unitCost = value
            End Set
        End Property

        ReadOnly Property UnitOfMeasure As UnitOfMeasure
            Get
                Return m_unitOfMeasure
            End Get
        End Property

        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property

        Public Property LeadTime() As Integer
            Get
                Return m_LeadTime
            End Get
            Set(ByVal value As Integer)
                m_LeadTime = value
            End Set
        End Property

        Public Property Vendor() As String
            Get
                Return m_Vendor
            End Get
            Set(ByVal value As String)
                m_Vendor = value
            End Set
        End Property

        Public Property MachineTime() As Decimal
            Get
                Return m_machineTime
            End Get
            Set(ByVal value As Decimal)
                m_machineTime = value
            End Set
        End Property

        Public Property MinimumQty() As Decimal
            Get
                Return m_MinimumQty
            End Get
            Set(ByVal value As Decimal)
                m_MinimumQty = value
            End Set
        End Property

        Public Property MinimumDollar() As Decimal
            Get
                Return m_MinimumDollar
            End Get
            Set(ByVal value As Decimal)
                m_MinimumDollar = value
            End Set
        End Property

    End Class

End Namespace

