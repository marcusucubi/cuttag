Imports System
Imports System.Windows.Forms

Imports PluginHost
Imports PluginOutputView

<PluginMenuItem(Text:="Import (Legacy)", Parent:="Template", MenuSeporatorNumber:=5)>
Public Class ImportMenuItem
    Implements IPluginMenuAction

    Public Sub Execute() Implements IPluginMenuAction.Execute

        Dim import As New QuoteImport
        import.DoImport()

    End Sub

End Class
