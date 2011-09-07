
Public Class ShippingList : Inherits System.ComponentModel.StringConverter

    Public Overloads Overrides Function _
        GetStandardValues(ByVal context As  _
        System.ComponentModel.ITypeDescriptorContext) _
        As System.ComponentModel.TypeConverter.StandardValuesCollection

        Return New StandardValuesCollection(Shipping.Shipping.Descriptions)
    End Function

    Public Overloads Overrides Function _
        GetStandardValuesExclusive(ByVal context _
        As System.ComponentModel.ITypeDescriptorContext) _
        As Boolean

        Return True
    End Function

    Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

End Class
