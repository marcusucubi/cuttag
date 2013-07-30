

Public Structure ProductBuildData
    
    Private _Code As String 
    Private _Gage As String 
    Private _CopperWeightPer1000Feet As Decimal
    Private _UnitCost As Decimal
    Private _MachineTime As Decimal
    Private _IsWire As Boolean
    Private _Description As String 
    Private _LeadTime As Integer
    Private _Vendor As String 
    Private _MinimumQty As Decimal
    Private _MinimumDollar As Decimal
    Private _UnitOfMeasure As String
    
    Public Property Code As String
        Get
            Return _Code
        End Get
        Set
            _Code = value
        End Set
    End Property
    
    Public Property Gage As String
        Get
            Return _Gage
        End Get
        Set
            _Gage = value
        End Set
    End Property
    
    Public Property CopperWeightPer1000Feet As Decimal
        Get
            Return _CopperWeightPer1000Feet
        End Get
        Set
            _CopperWeightPer1000Feet = value
        End Set
    End Property
    
    Public Property UnitCost As Decimal
        Get
            Return _UnitCost
        End Get
        Set
            _UnitCost = value
        End Set
    End Property
    
    Public Property MachineTime As Decimal
        Get
            Return _MachineTime
        End Get
        Set
            _MachineTime = value
        End Set
    End Property
    
    Public Property IsWire As Boolean
        Get
            Return _IsWire
        End Get
        Set
            _IsWire = value
        End Set
    End Property
    
    Public Property Description As String
        Get
            Return _Description
        End Get
        Set
            _Description = value
        End Set
    End Property
    
    Public Property LeadTime As Integer
        Get
            Return _LeadTime
        End Get
        Set
            _LeadTime = value
        End Set
    End Property
    
    Public Property Vendor As String
        Get
            Return _Vendor
        End Get
        Set
            _Vendor = value
        End Set
    End Property
    
    Public Property MinimumQty As Decimal
        Get
            Return _MinimumQty
        End Get
        Set
            _MinimumQty = value
        End Set
    End Property
    
    Public Property MinimumDollar As Decimal
        Get
            Return _MinimumDollar
        End Get
        Set
            _MinimumDollar = value
        End Set
    End Property
    
    Public Property UnitOfMeasure As String
        Get
            Return _UnitOfMeasure
        End Get
        Set
            _UnitOfMeasure = value
        End Set
    End Property
    
    Public Overrides Function GetHashCode() As Integer
        Return 0
    End Function
    
    Public Overrides Function Equals(obj As Object) As Boolean
        Return True
    End Function

    Public Shared Operator =(left As ProductBuildData, right As ProductBuildData) As Boolean
        Return Object.Equals(left, right)
    End Operator
    
    Public Shared Operator <>(left As ProductBuildData, right As ProductBuildData) As Boolean
        Return Not Object.Equals(left, right)
    End Operator
    
End Structure

