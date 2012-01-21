Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM
    ''' <summary>
    ''' Adds display attributes and rounding to ComponentProperties.
    ''' </summary>
    ''' <remarks>
    ''' This class should contain display releated code,
    ''' and ComponentProperties should contain computation
    ''' related code.
    ''' </remarks>
    Public Class DisplayableComponentProperties
        Implements ICustomTypeDescriptor, INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

        Private WithEvents _Options As Common.GlobalOptions = Common.GlobalOptions.Instance

        Private Sub _Options_Changed() Handles _Options.Changed
            Me.SendEvents()
        End Sub

        Private _Subject As ComponentProperties

        Public Sub New(subject As ComponentProperties)
            _Subject = subject
        End Sub

        <FilterAttribute(True), _
        DisplayName("Total Machine Time"), _
        Browsable(False)>
        Public Overloads ReadOnly Property TotalMachineTime As Decimal
            Get
                Return Math.Round(_Subject.TotalMachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
        End Property

        <DisplayName("Machine Time")>
        Public Overloads Property MachineTime As Decimal
            Get
                Return Math.Round(_Subject.MachineTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MachineTime = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Minimum Qty"), _
        CategoryAttribute("Vendor")> _
        Public Overloads Property MinimumQty As Decimal
            Get
                Return Math.Round(_Subject.MinimumQty, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MinimumQty = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Minimum Dollar"), _
        CategoryAttribute("Vendor")> _
        Public Overloads Property MinimumDollar As Decimal
            Get
                Return Math.Round(_Subject.MinimumDollar, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.MinimumDollar = value
                SendEvents()
            End Set
        End Property

        Public Overloads Property Quantity As Decimal
            Get
                Return Math.Round(_Subject.Quantity, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.Quantity = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Description"), _
        CategoryAttribute("Vendor")> _
        Public Property Description As String
            Get
                Return _Subject.Description
            End Get
            Set(ByVal value As String)
                _Subject.Description = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Lead Time"), _
        CategoryAttribute("Vendor")> _
        Public Property LeadTime As Integer
            Get
                Return Math.Round(_Subject.LeadTime, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Integer)
                _Subject.LeadTime = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Vendor"), _
        CategoryAttribute("Vendor")> _
        Public Property Vendor As String
            Get
                Return _Subject.Vendor
            End Get
            Set(ByVal value As String)
                _Subject.Vendor = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Unit Of Measure"), _
        TypeConverter(GetType(UOMConverter)), _
        CategoryAttribute("Vendor")> _
        Public Property UnitOfMeasure As String
            Get
                Return _Subject.UnitOfMeasure
            End Get
            Set(ByVal value As String)
                _Subject.UnitOfMeasure = value
                SendEvents()
            End Set
        End Property

        <FilterAttribute(False), _
        DisplayName("Unit Cost")> _
        Public Property UnitCost As Decimal
            Get
                Return Math.Round(_Subject.UnitCost, Common.GlobalOptions.DecimalPointsToDisplay)
            End Get
            Set(ByVal value As Decimal)
                _Subject.UnitCost = value
                SendEvents()
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

        Friend Sub SendEvents()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(""))
        End Sub

    End Class
End Namespace
