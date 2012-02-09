
Namespace Model
    ''' <summary>
    ''' Custom Property Attribute used to filter properties for display in property grid.
    ''' </summary>
    ''' <remarks>
    ''' Including FilterAttribute(True) -> Begin filtering with this property
    ''' Including FilterAttribute(False) -> Stop filtering with this property
    ''' Currently used by SaveableProperties.FilterAttributes
    ''' </remarks>

    NotInheritable Class FilterAttribute
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
End Namespace
