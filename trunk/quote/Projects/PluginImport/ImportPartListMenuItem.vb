Imports System
Imports System.Windows.Forms

Imports Host.UI
Imports PluginOutputView

<MenuItem( _
    Text:="Import Parts List", _
    Parent:="Template" _
    )>
Public Class ImportPartListMenuItem
    Implements IMenuAction

    Public Sub Execute() Implements IMenuAction.Execute

        Dim import As New QuoteImport
        import.DoImportFromPartsList()

    End Sub

End Class
