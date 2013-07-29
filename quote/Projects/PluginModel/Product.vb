''' <summary>
''' Represent a product
''' </summary>
''' <remarks></remarks>
Public Class Product

    Private m_code As String
    Private m_gage As String
    Private m_CopperWeightPer1000Ft As Decimal
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
           ByVal Description As String,
           ByVal LeadTime As Integer,
           ByVal Vendor As String,
           ByVal MinimumQty As Decimal,
           ByVal MinimumDollar As Decimal
            )
        Me.m_code = code
        Me.m_gage = gage
        Me.m_unitCost = unitCost
        Me.m_machineTime = machineTime
        Me.m_isWire = isWire
        Me.m_Description = Description
        Me.m_LeadTime = LeadTime
        Me.m_Vendor = Vendor
        Me.m_MinimumQty = MinimumQty
        Me.m_MinimumDollar = MinimumDollar
    End Sub

    Public Sub New(data As ProductBuildData)
        m_code = data.Code
        m_gage = data.Gage
        m_CopperWeightPer1000Ft = data.CopperWeightPer1000Feet
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
            ByVal Code As String, _
            ByVal UnitCost As Decimal, _
            ByVal Gage As String, _
            ByVal IsWire As Boolean, _
            ByVal WireRow As DB.QuoteDataBase.WireSourceRow, _
            ByVal PartRow As DB.QuoteDataBase.WireComponentSourceRow, _
            Optional ByVal UnitOfMeasure As String = "", _
            Optional ByVal CopperWeightPer1000Ft As Decimal = 0
            )
        Me.m_code = Code
        Me.m_unitCost = UnitCost
        Me.m_gage = Gage
        Me.m_isWire = IsWire
        Me.m_UnitOfMeasure = UnitOfMeasure
        If WireRow IsNot Nothing Then
            Me.m_CopperWeightPer1000Ft = CopperWeightPer1000Ft
        End If
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
    Public Sub New( _
             ByVal SourceID As Guid, _
             ByVal IsWire As Boolean, _
             ByVal PartLookupDataSource As DB.QuoteDataBase
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
                Me.m_CopperWeightPer1000Ft = qWeight.GetWirePoundsPer1000Ft(.WireSourceID, sMessage)
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

    Property CopperWeightPer1000Ft As String
        Get
            Return m_CopperWeightPer1000Ft
        End Get
        Set(ByVal Value As String)
            m_CopperWeightPer1000Ft = Value
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

