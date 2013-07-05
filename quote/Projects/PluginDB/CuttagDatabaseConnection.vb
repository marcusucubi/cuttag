Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class CuttagDatabaseConnection

    Public Shared Property DataSource As String
    Public Shared Property DataBase As String
    Public Shared Property ConnectionString As String
    Public Shared Property ShowErrors As Boolean = True
    Public Shared Property Connection As New SqlConnection

    Shared Sub New()
        Init()
    End Sub

    Private Shared Sub Init()
        DataSource = GetSetting("Cuttag", "DataLocation", "Server")
        DataBase = GetSetting("Cuttag", "DataLocation", "DataBase")

        If DataSource.Length > 0 Then
            Console.WriteLine("DataSource: {0}", DataSource)
            Console.WriteLine("DataBase: {0}", DataBase)
            Dim sSecurity As String = "SSPI"
            ConnectionString = "data source= " + DataSource _
                + ";initial catalog= " + DataBase _
                + ";integrated security=" + sSecurity _
                + ";persist security info=False;" _
                + ";packet size=4096;connect timeout=5"
            If Not Open() Then
                ConnectionString = DB.My.MySettings.Default.cuttagSKEConnectionString
            End If
        Else
            ConnectionString = DB.My.MySettings.Default.cuttagSKEConnectionString
        End If
        Console.WriteLine("ConnectionString: {0}", ConnectionString)

    End Sub

    Private Shared Function Open() As Boolean

        Dim result As Boolean = True
        Try
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If

            Connection = New SqlConnection(ConnectionString)
            Connection.Open()
            Connection.Close()

        Catch ex As Exception
            If ShowErrors Then
                MessageBox.Show(("Problem opening database connection!" + vbCrLf _
                        + "Connection: " + vbCrLf + ConnectionString _
                        + vbCrLf + "Full Error Message:" + vbCrLf + ex.ToString()))
            End If
            result = False
        End Try
        Return result
    End Function

End Class
