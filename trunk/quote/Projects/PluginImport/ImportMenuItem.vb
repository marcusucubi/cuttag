Imports System
Imports System.Windows.Forms

Imports PluginHost
Imports PluginOutputView

<PluginMenuItem( _
    Text:="Import (Legacy)", _
    Parent:="Template", _
    Anchor:="Import", _
    MenuPosition:=MenuPosition.Below
    )>
Public Class ImportMenuItem
    Implements IPluginMenuAction

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim import As New QuoteImport
        import.DoImport()

    End Sub

End Class
