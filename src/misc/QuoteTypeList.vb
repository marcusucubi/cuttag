Imports System.ComponentModel

Public Class QuoteTypeList : Inherits System.ComponentModel.StringConverter

    Public Shared PRODUCTION As String = "Production"
    Public Shared PILOT As String = "Pilot"
    Public Shared PROVE As String = "Prove"
    Public Shared SINGLE_DEFINATE As String = "Single Definate"

    Public Overloads Overrides Function _
        GetStandardValues(ByVal context As  _
        System.ComponentModel.ITypeDescriptorContext) _
        As TypeConverter.StandardValuesCollection

        Dim l As New List(Of String)
        l.AddRange(New String() {
            PRODUCTION, _
            PILOT, _
            PROVE, _
            SINGLE_DEFINATE
        })
        Return New StandardValuesCollection(l)
    End Function

    Public Overloads Overrides Function _
        GetStandardValuesExclusive(ByVal context _
        As System.ComponentModel.ITypeDescriptorContext) _
        As Boolean

        Return True
    End Function

    Public Overrides Function _
        GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean

        Return True
    End Function

End Class
