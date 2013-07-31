Imports Model.Template

Namespace Template.Ext

    Public Interface IComponentPropertiesFactory

        Function CreateComponentProperties(detail As Template.Detail) _
                                           As Common.SavableProperties

    End Interface

End Namespace

