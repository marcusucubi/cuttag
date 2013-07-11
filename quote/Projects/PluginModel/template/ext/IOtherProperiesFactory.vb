Imports Model.Template

Namespace Template.Ext

    Public Interface IOtherProperiesFactory

        Function CreateOtherProperties(header As Header, _
                                       id As Integer) _
                                       As Common.SaveableProperties

    End Interface

End Namespace

