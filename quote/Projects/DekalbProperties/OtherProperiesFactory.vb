Imports Host
Imports Model.Template.Ext

<Register(Key:=GetType(Model.Template.Ext.IOtherPropertiesFactory))>
Public Class OtherProperiesFactory
    Implements IOtherPropertiesFactory

    Public Function CreateOtherProperties(header As Model.Template.Header, _
                                          id As Integer) As  _
                                      Model.Common.SavableProperties _
                                      Implements IOtherPropertiesFactory.CreateOtherProperties
        Return New DekalbOtherProperties(header)
    End Function

End Class
