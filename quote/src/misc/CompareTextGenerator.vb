Imports System.Reflection

Public Class CompareTextGenerator

    Public Function Generate(header As Common.Header) As List(Of String)

        Dim list As New List(Of String)

        Dim p = header.ComputationProperties
        Dim props As PropertyInfo()
        props = p.GetType.GetProperties()
        For Each prop As PropertyInfo In props
            list.Add(prop.Name)
        Next

        Return list
    End Function

End Class
