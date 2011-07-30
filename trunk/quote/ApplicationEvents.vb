Imports Microsoft.VisualBasic.ApplicationServices
Imports DCS.Quote.Common

Namespace My

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, _
                                          ByVal e As StartupEventArgs) _
                                        Handles Me.Startup

            Dim s As String = CuttagDatabaseConnection.ConnectionString
            My.MySettings.Default.SetConnectionString(s)

            ActiveCustomProperties.ActiveCustomProperties.Load()

        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, _
                                                     ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) _
                                                 Handles Me.UnhandledException

            MsgBox(e.Exception.Message)

        End Sub

    End Class


End Namespace

