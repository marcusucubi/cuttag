Imports System.Data.SqlClient

Public Class CuttagDatabaseConnection

    Public Shared Property DataSource As String
    Public Shared Property DataBase As String
    Public Shared Property ConnectionString As String
    Public Shared Property ShowErrors As Boolean
    Public Shared Property Connection As New SqlConnection
    Public Shared Property DefaultConnectionString As String = _
        "Data Source=TKMAE45-PC\SQLEXPRESS08;" + _
        "Initial Catalog=cuttagSKE;Integrated Security=True"

    Shared Sub New()
        Init()
    End Sub

    Private Shared Sub Init()
        DataSource = GetSetting(Application.ProductName, "DataLocation", "Server")
        DataBase = GetSetting(Application.ProductName, "DataLocation", "DataBase")

        If DataSource.Length = 0 Then
            ConnectionString = DefaultConnectionString 
        Else
            Dim sSecurity As String = "SSPI"
            ConnectionString = "data source= " + DataSource _
                + ";initial catalog= " + DataBase _
                + ";integrated security=" + sSecurity _
                + ";persist security info=False; " _
                + ";packet size=4096;connect timeout=30"
        End If

    End Sub

    Private Shared Sub Open()

        Try
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If

            Connection = New SqlConnection(ConnectionString)
            Connection.Open()

        Catch ex As Exception
            If ShowErrors Then
                MessageBox.Show(("Problem opening database connection!" + vbCrLf _
                        + "Connection: " + vbCrLf + ConnectionString _
                        + vbCrLf + "Full Error Message:" + vbCrLf + ex.ToString()))
            End If
        End Try

    End Sub

End Class
