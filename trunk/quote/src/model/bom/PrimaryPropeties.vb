Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM

    Public Class PrimaryPropeties
        Inherits Common.PrimaryPropeties

        Private _QuoteHeader As Header

        Public Sub New(ByVal QuoteHeader As Header, ByVal id As Long)
            _QuoteHeader = QuoteHeader
            Me.SetID(id)
            CustomerName = "Caterpillar Inc."
        End Sub

        <CategoryAttribute("Date"), _
        DisplayName("CreatedDate"), _
        DescriptionAttribute("Created Date")> _
        Public ReadOnly Property CreatedDate As DateTime
            Get
                Return Me.CommonCreatedDate
            End Get
        End Property

        <CategoryAttribute("Date"), _
        DisplayName("LastModified"), _
        DescriptionAttribute("Last Modified Date")> _
        Public ReadOnly Property LastModified As DateTime
            Get
                Return Me.CommonLastModified
            End Get
        End Property

        <CategoryAttribute("Misc"), _
        DisplayName("Initials"), _
        DescriptionAttribute("Initials of creator")> _
        Public ReadOnly Property Initials As String
            Get
                Return Me.CommonInitials
            End Get
        End Property

        <CategoryAttribute("Quote"), _
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

        <CategoryAttribute("Quote"), _
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

        <CategoryAttribute("Quote"), _
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

        <CategoryAttribute("Misc"), _
        DisplayName("Quote Number"), _
        DescriptionAttribute("Quote Number")> _
        Public Overloads ReadOnly Property QuoteNumber As Integer
            Get
                Return MyBase.CommonID
            End Get
        End Property

    End Class
End Namespace
