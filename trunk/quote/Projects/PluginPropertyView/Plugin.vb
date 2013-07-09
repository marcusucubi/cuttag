Public Class Plugin
    Implements PluginHost.IPluginInit

    Private WithEvents m_Events As Model.ModelEvents

    Public Sub Init() Implements PluginHost.IPluginInit.Init
        m_Events = Model.ModelEvents.ActiveEvents
    End Sub

    Private Sub m_Events_QuoteViewed(sender As Object, e As System.EventArgs) Handles m_Events.QuoteViewed
        ViewController.Instance.OpenAll()
    End Sub

    Private Sub m_Events_TemplateViewed(sender As Object, e As System.EventArgs) Handles m_Events.TemplateViewed
        ViewController.Instance.OpenAll()
    End Sub

    Private Sub m_Events_TemplateCreated(source As Object, args As Model.ModelEventArgs) Handles m_Events.TemplateCreated
        ViewController.Instance.OpenAll()
    End Sub

End Class
