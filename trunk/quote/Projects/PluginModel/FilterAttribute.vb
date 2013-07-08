
Public NotInheritable Class FilterAttribute
    Inherits Attribute

    Private _DoFilterAttribute As Boolean

    Sub New(ByVal DoFilter As Boolean)
        _DoFilterAttribute = DoFilter
    End Sub
    Public Property FilterAttribute As Boolean
        Get
            Return _DoFilterAttribute
        End Get
        Set(ByVal value As Boolean)
            _DoFilterAttribute = value
        End Set
    End Property
    Public Property FilterAttributeValue As Boolean
        Get
            Return _DoFilterAttribute
        End Get
        Set(ByVal value As Boolean)
            _DoFilterAttribute = value
        End Set
    End Property

End Class
