Imports DCS.Quote.ObjectGenerator
Imports System.ComponentModel
Imports DCS.Quote.Common
Imports System.Drawing.Design

Namespace Model.Template

    Public Class CustomPropertiesGenerator
        Inherits Common.CustomPropertiesGenerator

        Public Overrides Function Generate() As SaveableProperties

            If ActiveHeader.ActiveHeader.Header Is Nothing Then
                Return New SaveableProperties
            End If

            Dim g As New ObjectGenerator

            For Each o As PropInfo In Me.Properties
                Dim info As New PropertyInfo
                info.Name = o.Name
                info.CodeSnippet = o.Expression
                info.TypeName = "System.String"
                info.Description = o.Expression
                g.Add(info)
            Next

            g.BaseTypeName = GetType(SaveableProperties).Name
            g.InitObject = ActiveHeader.ActiveHeader.Header.ComputationProperties

            CommonSaver.SaveCustomPropertiesGenerator(Me)

            Return g.Generate
        End Function

    End Class

End Namespace