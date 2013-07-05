Imports Microsoft.VisualBasic.ApplicationServices
Imports DCS.Quote.Common

Namespace My

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, _
                                          ByVal e As StartupEventArgs) _
                                        Handles Me.Startup

            Dim s As String = DB.CuttagDatabaseConnection.ConnectionString
            DB.My.MySettings.Default.SetConnectionString(s)

        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, _
                                                     ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) _
                                                 Handles Me.UnhandledException

            Dim s As String
            s = e.Exception.ToString() + vbCrLf + vbCrLf + vbCrLf
            s += e.Exception.StackTrace
            MsgBox(s)

        End Sub

    End Class


End Namespace

