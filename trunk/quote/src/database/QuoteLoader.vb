Imports System.Data.SqlClient

Public Class QuoteLoader

    Public Function Save() As DataSet

        Dim sql = "SELECT ID, QuoteID, Qty, PartTime, ProductCode " + _
            "FROM _QuoteDetail where QuoteID = @QuoteID"

        Dim cmd As New SqlCommand(sql)

        Dim s As String = My.Settings.devConnectionString
        cmd.Connection = New SqlConnection(s)

        Dim params As New SqlParameter("@QuoteID", SqlDbType.Int)
        cmd.Parameters.Add(params)

        Dim adaptor As New SqlDataAdapter(cmd)
        Dim rs As New DataSet()
        cmd.Connection.Open()
        adaptor.Fill(rs)
        cmd.Connection.Close()

        Return rs
    End Function

End Class
