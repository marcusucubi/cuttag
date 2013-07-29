Imports Host
Imports Model.Template.Ext

<Register(Key:=GetType(Model.Template.Ext.IWirePropertiesFactory))>
Public Class WirePropertiesFactory
    Implements IWirePropertiesFactory

    Public Function CreateWireProperties(detail As Model.Template.Detail) As Model.Common.SaveableProperties _ 
        Implements IWirePropertiesFactory.CreateWireProperties
        
        Return New DisplayableWireProperties(New DekalbWireProperties(detail))
    End Function
End Class
