''' <summary>
''' Represent a product
''' </summary>
''' <remarks></remarks>
Public Class Product

    Private m_code As String
    Private m_gage As String
    Private m_CopperWeightPer1000Feet As Decimal
    Private m_unitCost As Decimal
    Private m_machineTime As Decimal
    Private m_isWire As Boolean
    Private m_Description As String
    Private m_LeadTime As Integer
    Private m_Vendor As String
    Private m_MinimumQty As Decimal
    Private m_MinimumDollar As Decimal
    Private m_UnitOfMeasure As String

    Public Sub New( _
           ByVal code As String,
           ByVal gage As String,
           ByVal unitCost As Decimal,
           ByVal machineTime As Decimal,
           ByVal isWire As Boolean,
           ByVal description As String,
           ByVal leadTime As Integer,
           ByVal vendor As String,
           ByVal minimumQty As Decimal,
           ByVal minimumDollar As Decimal
            )
        Me.m_code = code
        Me.m_gage = gage
        Me.m_unitCost = unitCost
        Me.m_machineTime = machineTime
        Me.m_isWire = isWire
        Me.m_Description = description
        Me.m_LeadTime = leadTime
        Me.m_Vendor = vendor
        Me.m_MinimumQty = minimumQty
        Me.m_MinimumDollar = minimumDollar
    End Sub

    Public Sub New()
        m_code = ""
        m_gage = ""
    End Sub
    
    Public Sub New(data As ProductBuildData)
        m_code = data.Code
        m_gage = data.Gage
        m_CopperWeightPer1000Feet = data.CopperWeightPer1000Feet
        m_unitCost = data.UnitCost
        m_machineTime = data.MachineTime
        m_isWire = data.IsWire
        m_Description = data.Description
        m_LeadTime = data.LeadTime
        m_Vendor = data.Vendor
        m_MinimumQty = data.MinimumQty
        m_MinimumDollar = data.MinimumDollar
        m_UnitOfMeasure = data.UnitOfMeasure
    End Sub

    Public Sub New( _
             ByVal sourceId As Guid, _
             ByVal isWire As Boolean, _
             ByVal partLookupDataSource As DB.QuoteDataBase
             )
        Dim sGage As String = ""
        Dim dCost As Decimal = 0
        Dim sUOM As String = ""
        Dim drUOM As DB.QuoteDataBase._UnitOfMeasureRow
        If IsWire Then
            Dim drSource As DB.QuoteDataBase.WireSourceRow = _
                PartLookupDataSource.WireSource.FindByWireSourceID(SourceID)
            With drSource
                Me.m_code = .PartNumber
                Me.m_Description = .Description
                sUOM = "Decimeter" 'dd_ToDo 12/31/11 implement and handle wiresource.unitofmeasureid
                Dim drGage As DB.QuoteDataBase.GageRow
                drGage = PartLookupDataSource.Gage.FindByOrganizationIDGageID(10, .GageID) 'ddFix organizationid
                If drGage IsNot Nothing Then sGage = drGage.Gage
                If Not drSource.IsQuotePriceNull Then
                    dCost = .QuotePrice
                End If
                Dim qWeight As New DB.QuoteDataBaseTableAdapters.WireSourceTableAdapter
                Dim sMessage As String = "" 'dd_ToDo 12/31/11 change sp GetWirePoundsPer1000Ft to return how wt computed and present to as wireproperty
                Me.m_CopperWeightPer1000Feet = qWeight.GetWirePoundsPer1000Ft(.WireSourceID, sMessage)
            End With
        Else
            Dim drSource As DB.QuoteDataBase.WireComponentSourceRow = _
                PartLookupDataSource.WireComponentSource.FindByWireComponentSourceID(SourceID)
            With drSource
                Me.m_code = .PartNumber
                Me.m_Description = .Description
                drUOM = PartLookupDataSource._UnitOfMeasure.FindByID(drSource.UnitOfMeasureID)
                If drUOM IsNot Nothing Then sUOM = drUOM.Name
                If Not drSource.IsQuotePriceNull Then
                    dCost = .QuotePrice
                End If
                sGage = ""
                If Not .IsMachineTimeNull Then
                    Me.m_machineTime = .MachineTime
                End If
                If Not .IsLeadTimeNull Then
                    Me.m_LeadTime = .LeadTime
                End If
                If Not .IsVendorNull Then
                    Me.m_Vendor = .Vendor
                End If
                If Not .IsMinimumQtyNull Then
                    Me.m_MinimumQty = .MinimumQty
                End If
                If Not .IsMinimumDollarNull Then
                    Me.m_MinimumDollar = .MinimumDollar
                End If
            End With
        End If
        Me.m_gage = sGage
        Me.m_unitCost = dCost
        Me.m_isWire = IsWire
        Me.m_UnitOfMeasure = sUOM
    End Sub

    Property Code As String
        Get
            Return m_code
        End Get
        Set(ByVal Value As String)
            m_code = Value
        End Set
    End Property

    Property CopperWeightPer1000Feet As String
        Get
            Return m_CopperWeightPer1000Feet
        End Get
        Set(ByVal Value As String)
            m_CopperWeightPer1000Feet = Value
        End Set
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

    ReadOnly Property IsWire As Boolean
        Get
            Return m_isWire
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
    Public Property UnitOfMeasure() As String
        Get
            Return m_UnitOfMeasure
        End Get
        Set(ByVal value As String)
            m_UnitOfMeasure = value
        End Set
    End Property
End Class

