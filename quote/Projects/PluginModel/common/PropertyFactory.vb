
Namespace Common

    Public Class PropertyFactory

        Private Shared s_Instance As New PropertyFactory

        Public Shared ReadOnly Property Instance As PropertyFactory
            Get
                Return s_Instance
            End Get
        End Property

        Public Function Create(Of T As SaveableProperties) _
                              (header As Model.Template.Header, _
                               id As Integer) _
                               As T

            If Not PluginHost.App.RegisteredClasses.ContainsKey(GetType(T)) Then
                Return Nothing
            End If

            Dim result As SaveableProperties = Nothing
            Dim v = PluginHost.App.RegisteredClasses(GetType(T))
            If (Not v Is Nothing) Then
                Dim o As Object = Activator.CreateInstance(v, header)

                result = o
            End If

            Return CType(result, T)
        End Function

    End Class

End Namespace
