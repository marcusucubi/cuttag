Imports Model.Template

Namespace Template.Ext

    Public Interface IOtherPropertiesFactory

        Function CreateOtherProperties(header As Header, _
                                       id As Integer) _
                                       As Common.SavableProperties

    End Interface

End Namespace

