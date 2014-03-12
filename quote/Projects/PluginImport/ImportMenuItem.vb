Imports System
Imports System.Windows.Forms

Imports Host.UI
Imports PluginOutputView

<MenuItem( _
    Text:="Import (Legacy)", _
    Parent:="Template" _
    )>
Public Class ImportMenuItem
    Implements IMenuAction

    Public Sub Execute() Implements IMenuAction.Execute

        Dim import As New QuoteImport
        import.DoImport()

    End Sub

End Class
