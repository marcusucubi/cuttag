Public Class Plugin
    Implements Host.IStartup, Host.UI.IStartup2

    Public Sub Initialize() Implements Host.IStartup.Initialize

        ShippingDB.Initialize()

    End Sub

    Public Sub InitializeUI() Implements Host.UI.IStartup2.InitializeUI

        Dim s As New StatusBarUpdater
        s.Init()

    End Sub
    
End Class
