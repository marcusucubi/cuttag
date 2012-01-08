Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM

    Public Class OtherProperties
        Inherits Common.OtherProperties
        Implements ICustomTypeDescriptor
        Private _QuoteHeader As Header
        Private _LeadTimeInitial As Integer
        Private _LeadTimeStandard As Integer
        Private _EstimatedAnnualUnits As Integer
        Private _Tooling As Decimal
        Private _FormBoardCost As Decimal
        Private _DueDate As DateTime
        Private _QuoteDate As DateTime
        Private _QuoteType As String = "Production"
        Private _ImportedUnitCost As Decimal
        Private _ImportedCuWeight As Decimal
        Private _ImportedLaborMinutes As Decimal
        Private _IsNew As Boolean = True

        Public Sub New(ByVal QuoteHeader As Header)
            _QuoteHeader = QuoteHeader
        End Sub
        <FilterAttribute(True), _
        CategoryAttribute(SortedSpaces1 + "Date"), _
        DisplayName("Due Date"), _
        DescriptionAttribute("Date the quote is to be given to the customer")> _
        Public Property DueDate As DateTime
            Get
                Return _DueDate
            End Get
            Set(ByVal value As DateTime)
                _DueDate = value
                Me.SendEvents()
            End Set
        End Property
        <CategoryAttribute(SortedSpaces1 + "Date"), _
        DisplayName("Quote Date"), _
        DescriptionAttribute("Quote Date to be displayed to the customer")> _
        Public Property QuoteDate As DateTime
            Get
                Return _QuoteDate
            End Get
            Set(ByVal value As DateTime)
                _QuoteDate = value
                Me.SendEvents()
            End Set
        End Property
        Public Sub SetImportedUnitCost(ByVal value As Decimal)
            _ImportedUnitCost = value
        End Sub

        <CategoryAttribute(SortedSpaces2 + "Import"), _
        DisplayName("ImportedCuWeight"), _
        DescriptionAttribute("Imported CuWeight")> _
        Public ReadOnly Property ImportedCuWeight As Decimal
            Get
                Return _ImportedCuWeight
            End Get
        End Property

        Public Sub SetImportedCuWeight(ByVal value As Decimal)
            _ImportedCuWeight = value
        End Sub

        <CategoryAttribute(SortedSpaces2 + "Import"), _
        DisplayName("ImportedLaborMinutes"), _
        DescriptionAttribute("Imported Labor Minutes")> _
        Public ReadOnly Property ImportedLaborMinutes As Decimal
            Get
                Return _ImportedLaborMinutes
            End Get
        End Property

        <CategoryAttribute(SortedSpaces2 + "Import"), _
        DisplayName("ImportedUnitCost"), _
        DescriptionAttribute("Imported Unit Cost")> _
        Public ReadOnly Property ImportedUnitCost As Decimal
            Get
                Return _ImportedUnitCost
            End Get
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Estimated Annual Units"), _
        DescriptionAttribute("Estimated Annual Units")> _
        Public Property EstimatedAnnualUnits As Integer
            Get
                Return _EstimatedAnnualUnits
            End Get
            Set(ByVal value As Integer)
                _EstimatedAnnualUnits = value
                Me.SendEvents()
            End Set
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Initial Lead Time"), _
        DescriptionAttribute("Minimum number of days between the first purchase order and delivery")> _
        Public Property LeadTimeInitial As Integer
            Get
                Return _LeadTimeInitial
            End Get
            Set(ByVal value As Integer)
                _LeadTimeInitial = value
                Me.SendEvents()
            End Set
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Standard Lead Time"), _
        DescriptionAttribute("Minimum number of days between the purchase order and delivery")> _
        Public Property LeadTimeStandard As Integer
            Get
                Return _LeadTimeStandard
            End Get
            Set(ByVal value As Integer)
                _LeadTimeStandard = value
                Me.SendEvents()
            End Set
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Tooling"), _
        DescriptionAttribute("Tooling Cost")> _
        Public Property Tooling As Decimal
            Get
                Return _Tooling
            End Get
            Set(ByVal value As Decimal)
                _Tooling = value
                Me.SendEvents()
            End Set
        End Property
        <FilterAttribute(False), _
        CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Form Board Cost"), _
        DescriptionAttribute("Form Board Cost")> _
        Public Property FormBoardCost As Decimal
            Get
                Return _FormBoardCost
            End Get
            Set(ByVal value As Decimal)
                _FormBoardCost = value
                Me.SendEvents()
            End Set
        End Property
        <FilterAttribute(False), _
        CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Is New"), _
        DescriptionAttribute("Is New")> _
        Public Property IsNew As Boolean
            Get
                Return _IsNew
            End Get
            Set(ByVal value As Boolean)
                _IsNew = value
                Me.SendEvents()
            End Set
        End Property
        Public Sub SetImportedLaborMinutes(ByVal value As Decimal)
            _ImportedLaborMinutes = value
        End Sub
        'dd_Added 11/21/11
#Region "TypeDescriptor Implementation"
        Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
            Return TypeDescriptor.GetClassName(Me, True)
        End Function
        Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
            Return TypeDescriptor.GetAttributes(Me, True)
        End Function
        Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
            Return TypeDescriptor.GetComponentName(Me, True)
        End Function
        Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
            Return TypeDescriptor.GetConverter(Me, True)
        End Function
        Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
            Return TypeDescriptor.GetDefaultEvent(Me, True)
        End Function
        Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
            Return TypeDescriptor.GetDefaultProperty(Me, True)
        End Function
        Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
            Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
        End Function
        Public Function GetEvents(ByVal attributes As Attribute()) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, attributes, True)
        End Function
        Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
            Return TypeDescriptor.GetEvents(Me, True)
        End Function
        Public Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            Return FilterProperties(TypeDescriptor.GetProperties(Me, attributes, True))
        End Function
        Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            Return TypeDescriptor.GetProperties(Me, True)
        End Function
        Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
            Return Me
        End Function
#End Region
        'dd_Added End

    End Class

End Namespace
