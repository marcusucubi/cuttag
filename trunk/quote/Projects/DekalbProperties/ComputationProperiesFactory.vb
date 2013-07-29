Imports Host

<Register(Key:=GetType(Model.Template.Ext.IComputationPropertiesFactory))>
Public Class ComputationPropertiesFactory
    Implements Model.Template.Ext.IComputationPropertiesFactory

    Public Function CreateComputationProperties(header As Model.Template.Header, _
                                                id As Integer) _
                                            As Model.Common.SaveableProperties _
                                            Implements Model.Template.Ext.IComputationPropertiesFactory.CreateComputationProperties
        Return New DisplayableComputationProperties(New DekalbComputationProperties(header))
    End Function

End Class
