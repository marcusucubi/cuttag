Imports Model.Common
Imports Model.Template.Ext

Imports Host

Namespace Template

    Public NotInheritable Class PropertyFactory
    
        Private Sub New()
        
        End Sub

        Public Shared Function CreateOtherProperties(header As Header, _
                                              id As Integer) _
                                              As SavableProperties

            Dim result As SavableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IOtherPropertiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IOtherPropertiesFactory))
                If (Not v Is Nothing) Then

                    Dim o As IOtherPropertiesFactory = Activator.CreateInstance(v)
                    result = o.CreateOtherProperties(header, id)
                End If

            End If

            If result Is Nothing Then
                result = New DefaultOtherProperties(header)
            End If

            Return result
        End Function

        Public Shared Function CreateComputationProperties(header As Header, _
                                                    id As Integer) _
                                                    As SavableProperties

            Dim result As SavableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IComputationPropertiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IComputationPropertiesFactory))

                If (Not v Is Nothing) Then

                    Dim o As IComputationPropertiesFactory = Activator.CreateInstance(v)

                    result = o.CreateComputationProperties(header, id)
                End If

            End If

            If result Is Nothing Then
                result = New DefaultComputationProperties(header)
            End If

            Return result
        End Function

        Public Shared Function CreateComponentProperties(detail As Template.Detail) _
                                                  As SavableProperties

            Dim result As SavableProperties = Nothing

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

        Public Shared Function CreateWireProperties(detail As Template.Detail) _
                                             As SavableProperties

            Dim result As SavableProperties = Nothing

            If App.RegisteredClasses.ContainsKey(GetType(IWirePropertiesFactory)) Then

                Dim v = App.RegisteredClasses(GetType(IWirePropertiesFactory))

                If (Not v Is Nothing) Then

                    Dim o As IWirePropertiesFactory = Activator.CreateInstance(v)

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
