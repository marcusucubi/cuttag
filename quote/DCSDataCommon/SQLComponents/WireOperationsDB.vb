Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class WireOperationsDB
  Public Function HasSolderOperation(ByVal WireID As Guid, ByVal WireEnd As String) As Boolean
    Dim cmd As New SqlCommand
    Dim rValue As Boolean
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT os.IsSolder FROM WireOperation wo " _
      + "LEFT JOIN OperationSource os ON " _
      + "wo.OperationSourceID = os.OperationSourceID " _
      + "WHERE wo.WireEnd = '" + WireEnd + "' AND wo.WireID = '" + WireID.ToString + "'"

    g_cn.Open()
    rValue = cmd.ExecuteScalar()
    g_cn.Close()
    Return rValue
  End Function
End Class
