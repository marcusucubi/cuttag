Imports System.ComponentModel
Imports System.Reflection
Imports DCS.Quote.Model.Quote

Namespace Model.BOM

    Public Class WireProperties
        Inherits Common.WireProperties
        Implements ICustomTypeDescriptor

        Private WithEvents _QuoteDetail As Detail
        'dd_Changed 11/13/11 sorting spaced to CategoryAttributes
        '  rearranged sub items 
        '  turn off alph sub item sorting in grid properties
        'added ICustomTypeDescriptor for filtering out read-only properties on user command
        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
        End Sub

        Protected Overrides Sub Finalize()
            _QuoteDetail = Nothing
        End Sub
        <FilterAttribute(True)>
        Public ReadOnly Property Gage As String
            Get
                Return _QuoteDetail.Product.Gage.Trim
            End Get
        End Property

        <DescriptionAttribute("Length in Decimeters")> _
        Public ReadOnly Property Length As Decimal
            Get
                Return _QuoteDetail.Qty
            End Get
        End Property

        <DescriptionAttribute("Length / 3.048")> _
        Public ReadOnly Property LengthFeet As Decimal
            Get
                Return Math.Round(Me._LengthFeet, 4)
            End Get
        End Property
        'dd_Changed 12/30/11 added new property and remmed old
        <DescriptionAttribute("Pounds per 1000 feet"), _
        CategoryAttribute("Copper Weight")> _
        Public Property PoundsPer1000Feet As Decimal
            Get
                Return Me._CopperWeightPer1000Ft
            End Get
            Set(ByVal value As Decimal)
                Me._CopperWeightPer1000Ft = value
                SendEvents()
            End Set
        End Property

        '<DescriptionAttribute("Pounds per foot"), _
        'CategoryAttribute("Weight")> _
        'Public ReadOnly Property WeightPerFoot As Decimal
        '    Get
        '        Return Common.Weights.FindWeight(Me.Gage)
        '    End Get
        'End Property

        <DescriptionAttribute("WeightPerFoot * Length" + Chr(10) + "(Pounds)"), _
        CategoryAttribute("Copper Weight")> _
        Public ReadOnly Property TotalWeight As Decimal
            Get
                Return Math.Round(Me._TotalWeight, 4)
            End Get
        End Property

        Private Sub _QuoteDetail_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteDetail.PropertyChanged
            SendEvents()
        End Sub

        <DescriptionAttribute("Number of Decimeters")> _
        Public Property Quantity() As Decimal
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteDetail.Qty = value
                Me.SendEvents()
            End Set
        End Property

        <DisplayName("Unit Cost"), _
        DescriptionAttribute("Dollars per Decimeter")> _
        Public Property UnitCost() As Decimal
            Get
                Return _QuoteDetail.UnitCost
            End Get
            Set(ByVal value As Decimal)
                _QuoteDetail.UnitCost = value
            End Set
        End Property

        <FilterAttribute(False), _
        DisplayName("Unit Of Measure"), _
        TypeConverter(GetType(UOMConverter))> _
        Public Property UnitOfMeasure() As String
            Get
                Return _QuoteDetail.UOM
            End Get
            Set(ByVal value As String)
                _QuoteDetail.UOM = value
            End Set
        End Property

        Private Overloads Sub SendEvents()
            Me._LengthFeet = _QuoteDetail.Qty / 3.048
            Me._TotalWeight = PoundsPer1000Feet / 1000 * Me._LengthFeet
            MyBase.SendEvents()
            Me._QuoteDetail.Header.ComputationProperties.SendEvents()
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

