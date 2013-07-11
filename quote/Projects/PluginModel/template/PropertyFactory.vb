Imports PluginHost
Imports Model.Common
Imports Model.Template.Ext

Namespace Template

    Public Class PropertyFactory

        Private Shared s_Instance As New PropertyFactory

        Public Shared ReadOnly Property Instance As PropertyFactory
            Get
                Return s_Instance
            End Get
        End Property

        Public Function CreateOtherProperties(header As Header, _
                                              id As Integer) _
                                              As SaveableProperties

            Dim result As SaveableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IOtherProperiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IOtherProperiesFactory))
                If (Not v Is Nothing) Then

                    Dim o As IOtherProperiesFactory = Activator.CreateInstance(v)
                    result = o.CreateOtherProperties(header, id)
                End If

            End If

            If result Is Nothing Then
                result = New DefaultOtherProperties(header)
            End If

            Return result
        End Function

        Public Function CreateComputationProperties(header As Header, _
                                                    id As Integer) _
                                                    As SaveableProperties

            Dim result As SaveableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IComputationProperiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IComputationProperiesFactory))

                If (Not v Is Nothing) Then

                    Dim o As IComputationProperiesFactory = Activator.CreateInstance(v)

                    result = o.CreateComputationProperties(header, id)
                End If

            End If

            If result Is Nothing Then
                result = New DefaultComputationProperties(header)
            End If

            Return result
        End Function

        Public Function CreateComponentProperties(detail As Template.Detail) _
                                                  As SaveableProperties

            Dim result As SaveableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IComponentPropertiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IComponentPropertiesFactory))

                If (Not v Is Nothing) Then

                    Dim o As IComponentPropertiesFactory = Activator.CreateInstance(v)

                    result = o.CreateComponentProperties(detail)
                End If

            End If

            If result Is Nothing Then
                result = New DefaultComponentProperties(detail)
            End If

            Return result
        End Function

        Public Function CreateWireProperties(detail As Template.Detail) _
                                             As SaveableProperties

            Dim result As SaveableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IWireProperiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IWireProperiesFactory))

                If (Not v Is Nothing) Then

                    Dim o As IWireProperiesFactory = Activator.CreateInstance(v)

                    result = o.CreateWireProperties(detail)
                End If

            End If

            If result Is Nothing Then
                result = New DefaultWireProperties(detail)
            End If

            Return result
        End Function

    End Class

End Namespace
