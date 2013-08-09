Imports System.ComponentModel
Imports System.Reflection

Imports Model

Public Class DisplayableComponentProperties
    Inherits Model.Template.DisplayableComponentProperties

    Private WithEvents _Options As Common.GlobalOptions = Common.GlobalOptions.Instance

    Private Sub _Options_Changed() Handles _Options.Changed
        Me.SendEvents()
    End Sub

    Private _Subject As DekalbComponentProperties

    Public Sub New(ByVal subject As DekalbComponentProperties)
        MyBase.New(subject)
        _Subject = subject
        MyBase.Subject = subject
    End Sub

    <DisplayName("Minimum Qty"), _
    CategoryAttribute("Vendor")> _
    Public Overloads Property MinimumQty As Decimal
        Get
            Return Math.Round(_Subject.MinimumQty, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
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
            Return Math.Round(_Subject.MinimumDollar, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
        Set(ByVal value As Decimal)
            _Subject.MinimumDollar = value
            SendEvents()
        End Set
    End Property

    <DisplayName("Lead Time"), _
    CategoryAttribute("Vendor")> _
    Public Property LeadTime As Integer
        Get
            Return Math.Round(_Subject.LeadTime, Common.GlobalOptions.Instance.DecimalPointsToDisplay)
        End Get
        Set(ByVal value As Integer)
            _Subject.LeadTime = value
            SendEvents()
        End Set
    End Property

End Class
