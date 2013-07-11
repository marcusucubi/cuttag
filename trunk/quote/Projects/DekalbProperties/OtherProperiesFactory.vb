Imports PluginHost

<Register(Key:=GetType(Model.Template.Ext.IOtherProperiesFactory))>
Public Class OtherProperiesFactory
    Implements Model.Template.Ext.IOtherProperiesFactory

    Public Function CreateOtherProperties(header As Model.Template.Header, _
                                          id As Integer) As  _
                                      Model.Common.SaveableProperties _
                                      Implements Model.Template.Ext.IOtherProperiesFactory.CreateOtherProperties
        Return New DekalbOtherProperties(header)
    End Function

End Class
