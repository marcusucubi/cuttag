Imports Host

<Register(Key:=GetType(Model.Template.Ext.IWireProperiesFactory))>
Public Class WirePropertiesFactory
    Implements Model.Template.Ext.IWireProperiesFactory

    Public Function CreateWireProperties(detail As Model.Template.Detail) As Model.Common.SaveableProperties Implements Model.Template.Ext.IWireProperiesFactory.CreateWireProperties
        Return New DisplayableWireProperties(New DekalbWireProperties(detail))
    End Function
End Class
