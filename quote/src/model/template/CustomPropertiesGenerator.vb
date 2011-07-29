Imports DCS.Quote.ObjectGenerator
Imports System.ComponentModel
Imports DCS.Quote.Common
Imports System.Drawing.Design

Namespace Model.Template

    Public Class CustomPropertiesGenerator
        Inherits Common.CustomPropertiesGenerator

        Public Sub New(ByVal parent As Common.Header)
            MyBase.New(parent)
        End Sub

        Public Overrides Function Generate() As SaveableProperties

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
            g.InitObject = Me.Parent.ComputationProperties

            Return g.Generate
        End Function

    End Class

End Namespace