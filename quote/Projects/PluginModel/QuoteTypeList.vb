Imports System.ComponentModel

Public Class QuoteTypeList : Inherits System.ComponentModel.StringConverter

    Public Const Production As String = "Production"
    Public Const Pilot As String = "Pilot"
    Public Const Prove As String = "Prove"
    Public Const SingleDefinate As String = "Single Definate"

    Public Overloads Overrides Function _
        GetStandardValues(ByVal context As  _
        System.ComponentModel.ITypeDescriptorContext) _
        As TypeConverter.StandardValuesCollection

        Dim l As New List(Of String)
        l.AddRange(New String() {
            Production, _
            Pilot, _
            Prove, _
            SingleDefinate
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
