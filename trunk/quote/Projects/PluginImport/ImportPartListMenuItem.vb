Imports System
Imports System.Windows.Forms

Imports PluginHost
Imports PluginOutputView

<PluginMenuItem( _
    Text:="Import Parts List", _
    Parent:="Template", _
    MenuSeporatorNumber:=5, _
    Position:=PluginHost.MenuPosition.Bottom)>
Public Class ImportPartListMenuItem
    Implements IPluginMenuAction

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim import As New QuoteImport
        import.DoImportFromPartsList()

    End Sub

End Class
