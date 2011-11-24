Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM
    Public Class ComponentProperties
        Inherits Common.ComponentProperties
        Implements ICustomTypeDescriptor

        Private _QuoteDetail As Detail
        'dd_Remmed 11/13/11
        'Private _MachineTime As Decimal
        Private _MinimumQty As Decimal
        Private _MinimumDollar As Decimal
        'dd_Changed 11/13/11 sorting spaced to CategoryAttributes
        '  rearranged sub items 
        '  turn off alph sub item sorting in grid properties
        'added ICustomTypeDescriptor for filtering out read-only properties on user command
        Public Sub New(ByVal QuoteDetail As Detail)
            _QuoteDetail = QuoteDetail
            If _QuoteDetail.Product IsNot Nothing Then
                'dd_Remmed 11/13/11
                'Me._MachineTime = _QuoteDetail.Product.MachineTime
                Me._MinimumQty = _QuoteDetail.Product.MinimumQty
                Me._MinimumDollar = _QuoteDetail.Product.MinimumDollar
            End If
        End Sub

        <FilterAttribute(True), _
        DisplayName("Total Machine Time"), _
        Browsable(False)>
        Public Overloads ReadOnly Property TotalMachineTime() As Decimal
            Get
                Return (_QuoteDetail.MachineTime * _QuoteDetail.Qty)
                'Return (_MachineTime * _QuoteDetail.Qty)
            End Get
        End Property

        <DisplayName("Machine Time")>
        Public Overloads Property MachineTime() As Decimal
            Get
                ' Return Me._MachineTime
                Return _QuoteDetail.MachineTime 'Me._MachineTime
            End Get
            Set(ByVal value As Decimal)
                'dd_Changed 11/13/11
                'If Not (value = _MachineTime) Then
                If Not (value = _QuoteDetail.MachineTime) Then
                    _QuoteDetail.MachineTime = value
                    'dd_Remmed 11/13/11
                    'Me._MachineTime = value
                    SendEvents()
                End If
            End Set
        End Property

        <DisplayName("Minimum Qty"), _
        CategoryAttribute("Vendor")> _
        Public Overloads Property MinimumQty() As Decimal
            Get
                Return Me._MinimumQty
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _MinimumQty) Then
                    Me._MinimumQty = value
                    SendEvents()
                End If
            End Set
        End Property

        <DisplayName("Minimum Dollar"), _
        CategoryAttribute("Vendor")> _
        Public Overloads Property MinimumDollar() As Decimal
            Get
                Return Me._MinimumDollar
            End Get
            Set(ByVal value As Decimal)
                If Not (value = _MinimumDollar) Then
                    Me._MinimumDollar = value
                    SendEvents()
                End If
            End Set
        End Property

        Public Overloads Property Quantity() As Decimal
            Get
                Return Me._QuoteDetail.Qty
            End Get
            Set(ByVal value As Decimal)
                Me._QuoteDetail.Qty = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Description"), _
        CategoryAttribute("Vendor")> _
        Public Property Description() As String
            Get
                Return _QuoteDetail.Product.Description
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Description = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Lead Time"), _
        CategoryAttribute("Vendor")> _
        Public Property LeadTime() As Integer
            Get
                Return _QuoteDetail.Product.LeadTime
            End Get
            Set(ByVal value As Integer)
                _QuoteDetail.Product.LeadTime = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Vendor"), _
        CategoryAttribute("Vendor")> _
        Public Property Vendor() As String
            Get
                Return _QuoteDetail.Product.Vendor
            End Get
            Set(ByVal value As String)
                _QuoteDetail.Product.Vendor = value
                SendEvents()
            End Set
        End Property

        <DisplayName("Unit Of Measure"), _
        TypeConverter(GetType(UOMConverter)), _
        CategoryAttribute("Vendor")> _
        Public Property UnitOfMeasure() As String
            Get
                Return _QuoteDetail.UOM
            End Get
            Set(ByVal value As String)
                _QuoteDetail.UOM = value
            End Set
        End Property

        <FilterAttribute(False), _
        DisplayName("Unit Cost")> _
        Public Property UnitCost() As Decimal
            Get
                Return _QuoteDetail.UnitCost
            End Get
            Set(ByVal value As Decimal)
                _QuoteDetail.UnitCost = value
                SendEvents()
            End Set
        End Property

        Private Overloads Sub SendEvents()
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
