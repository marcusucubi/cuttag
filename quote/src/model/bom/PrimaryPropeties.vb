Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties
        Implements ICustomTypeDescriptor
        'dd_Changed 11/13/11 sorting spaced to CategoryAttributes
        '  rearranged sub items 
        '  turn off alph sub item sorting in grid properties
        'added ICustomTypeDescriptor for filtering out read-only properties on user command

        Private _QuoteHeader As Header

        Public Sub New(ByVal QuoteHeader As Header, ByVal id As Long)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
            CustomerName = "Caterpillar Inc."
        End Sub
        <FilterAttribute(True), CategoryAttribute(SortedSpaces1 + "Date"), _
        DisplayName("CreatedDate"), _
        DescriptionAttribute("Created Date")> _
        Public ReadOnly Property CreatedDate As DateTime
            Get
                Return Me.CommonCreatedDate
            End Get
        End Property

        <CategoryAttribute(SortedSpaces1 + "Date"), _
        DisplayName("LastModified"), _
        DescriptionAttribute("Last Modified Date")> _
        Public ReadOnly Property LastModified As DateTime
            Get
                Return Me.CommonLastModified
            End Get
        End Property
        <CategoryAttribute(SortedSpaces2 + "Misc"), _
        DisplayName("Quote Number"), _
        DescriptionAttribute("Quote Number")> _
        Public Overloads ReadOnly Property QuoteNumber As Integer
            Get
                Return MyBase.CommonID
            End Get
        End Property
        <CategoryAttribute(SortedSpaces2 + "Misc"), _
        DisplayName("Initials"), _
        DescriptionAttribute("Initials of creator")> _
        Public ReadOnly Property Initials As String
            Get
                Return Me.CommonInitials
            End Get
        End Property

        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Customer"), _
        DescriptionAttribute("The customer name")> _
        Public Property CustomerName As String
            Get
                Return Me.CommonCustomerName
            End Get
            Set(ByVal value As String)
                Me.CommonCustomerName = value
                Me.SendEvents()
            End Set
        End Property
        <CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("Part Number"), _
        DescriptionAttribute("Part Number")> _
        Public Property PartNumber As String
            Get
                Return Me.CommonPartNumber
            End Get
            Set(ByVal value As String)
                Me.CommonPartNumber = value
                Me.SendEvents()
                'dd_Added 11/19/11
                Me.SendStatusBarEvent()
            End Set
        End Property
        <FilterAttribute(False), CategoryAttribute(SortedSpaces3 + "Quote"), _
        DisplayName("RFQ"), _
        DescriptionAttribute("Request For Quote")> _
        Public Property RequestForQuoteNumber As String
            Get
                Return Me.CommonRequestForQuoteNumber
            End Get
            Set(ByVal value As String)
                Me.CommonRequestForQuoteNumber = value
                Me.SendEvents()
                'dd_Added 11/19/11
                Me.SendStatusBarEvent()
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
            Return FilterProperties(TypeDescriptor.GetProperties(Me, attributes, True))
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
