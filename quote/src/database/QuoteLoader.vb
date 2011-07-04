Imports System.Data.SqlClient
Imports DCS.Quote.Model

Public Class QuoteLoader

    Public Sub Save(ByVal q As Model.QuoteHeader)

        Dim o As PrimaryPropeties = q.PrimaryProperties

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim i As Integer
        adaptor.Connection.Open()
        i = adaptor.Insert(o.CustomerName, o.PartNumber, o.QuoteNumnber)
        adaptor.Connection.Close()
        Console.WriteLine(i)
    End Sub

    Public Function FetchDetail(ByVal QuoteID As Integer) As QuoteDataBase._QuoteDetailDataTable

        Dim sql = "SELECT ID, QuoteID, Qty, PartTime, ProductCode " + _
            "FROM _QuoteDetail where QuoteID = @QuoteID"

        Dim cmd As New SqlCommand(sql)

        Dim s As String = My.Settings.devConnectionString
        cmd.Connection = New SqlConnection(s)

        Dim params As New SqlParameter("@QuoteID", SqlDbType.Int)
        params.Value = QuoteID
        cmd.Parameters.Add(params)

        Dim adaptor As New SqlDataAdapter(cmd)
        Dim ds As New QuoteDataBase
        cmd.Connection.Open()
        adaptor.Fill(ds)
        cmd.Connection.Close()

        Return ds._QuoteDetail
    End Function

    Public Sub SaveDetail(ByVal table As QuoteDataBase._QuoteDetailDataTable)

        Dim sql = "SELECT ID, QuoteID, Qty, PartTime, ProductCode " + _
            "FROM _QuoteDetail where QuoteID = @QuoteID"

        Dim cmd As New SqlCommand(sql)

        Dim s As String = My.Settings.devConnectionString
        cmd.Connection = New SqlConnection(s)

        Dim adaptor As New SqlDataAdapter(cmd)
        Dim ds As New QuoteDataBase
        cmd.Connection.Open()
        adaptor.Update(table)
        cmd.Connection.Close()

    End Sub

End Class
