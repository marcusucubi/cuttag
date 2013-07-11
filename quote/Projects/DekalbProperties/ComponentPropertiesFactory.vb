Imports PluginHost

<Register(Key:=GetType(Model.Template.Ext.IComponentPropertiesFactory))>
Public Class ComponentPropertiesFactory
    Implements Model.Template.Ext.IComponentPropertiesFactory

    Public Function CreateComponentProperties(detail As Model.Template.Detail) As Model.Common.SaveableProperties Implements Model.Template.Ext.IComponentPropertiesFactory.CreateComponentProperties
        Return New DisplayableComponentProperties(New DekalbComponentProperties(detail))
    End Function

End Class
