Namespace My

    Partial Friend NotInheritable Class MySettings

        Public Sub SetConnectionString(ByVal strConnection As String)
            My.Settings.Item("cuttagSKEConnectionString") = strConnection
        End Sub

    End Class

End Namespace

