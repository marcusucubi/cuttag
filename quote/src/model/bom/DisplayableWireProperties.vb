Imports System.ComponentModel
Imports System.Reflection
Imports DCS.Quote.Model.Quote

Namespace Model.BOM

    Public Class DisplayableWireProperties
        Implements ICustomTypeDescriptor

        Private _Subject As Model.BOM.WireProperties
        Private Const SIZE As Integer = 4

        Public Sub New(subject As Model.BOM.WireProperties)
            _Subject = subject
        End Sub

        <Browsable(False)>
        Public ReadOnly Property Subject As WireProperties
            Get
                Return _Subject
            End Get
        End Property

        <FilterAttribute(True)>
        Public ReadOnly Property Gage As String
            Get
                Return _Subject.Gage
            End Get
        End Property

        <DescriptionAttribute("Length in Decimeters")> _
        Public ReadOnly Property Length As Decimal
            Get
                Return Math.Round(_Subject.Length, SIZE)
            End Get
        End Property

        <DescriptionAttribute("Length / 3.048")> _
        Public ReadOnly Property LengthFeet As Decimal
            Get
                Return Math.Round(_Subject.LengthFeet, SIZE)
            End Get
        End Property

        <DescriptionAttribute("Pounds per 1000 feet"), _
        CategoryAttribute("Copper Weight")> _
        Public Property PoundsPer1000Feet As Decimal
            Get
                Return Math.Round(_Subject.PoundsPer1000Feet, SIZE)
            End Get
            Set(ByVal value As Decimal)
                _Subject.PoundsPer1000Feet = value
            End Set
        End Property

        <DescriptionAttribute("WeightPerFoot * Length" + Chr(10) + "(Pounds)"), _
        CategoryAttribute("Copper Weight")> _
        Public ReadOnly Property TotalWeight As Decimal
            Get
                Return Math.Round(_Subject.TotalWeight, SIZE)
            End Get
        End Property

        <DescriptionAttribute("Number of Decimeters")> _
        Public Property Quantity As Decimal
            Get
                Return Math.Round(_Subject.Quantity, SIZE)
            End Get
            Set(ByVal value As Decimal)
                _Subject.Quantity = value
            End Set
        End Property

        <DisplayName("Unit Cost"), _
        DescriptionAttribute("Dollars per Decimeter")> _
        Public Property UnitCost As Decimal
            Get
                Return Math.Round(_Subject.UnitCost, SIZE)
            End Get
            Set(ByVal value As Decimal)
                _Subject.UnitCost = value
            End Set
        End Property

        <FilterAttribute(False), _
        DisplayName("Unit Of Measure"), _
        TypeConverter(GetType(UOMConverter))> _
        Public Property UnitOfMeasure As String
            Get
                Return _Subject.UnitOfMeasure
            End Get
            Set(ByVal value As String)
                _Subject.UnitOfMeasure = value
            End Set
        End Property

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
            Return _Subject.FilterProperties(TypeDescriptor.GetProperties(Me, attributes, True))
        End Function
        Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
            Return TypeDescriptor.GetProperties(Me, True)
        End Function
        Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
            Return Me
        End Function
#End Region

    End Class
End Namespace

