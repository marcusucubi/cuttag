Imports Host

<Register(Key:=GetType(Model.Template.Ext.IComputationProperiesFactory))>
Public Class ComputationProperiesFactory
    Implements Model.Template.Ext.IComputationProperiesFactory

    Public Function CreateComputationProperties(header As Model.Template.Header, _
                                                id As Integer) _
                                            As Model.Common.SaveableProperties _
                                            Implements Model.Template.Ext.IComputationProperiesFactory.CreateComputationProperties
        Return New DisplayableComputationProperties(New DekalbComputationProperties(header))
    End Function

End Class
