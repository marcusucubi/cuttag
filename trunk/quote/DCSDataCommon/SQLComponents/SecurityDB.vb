Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class SecurityDB
  Public Function GetRolesByTask(ByVal TaskCode As String) As SqlDataReader
    Dim cmd As New SqlCommand
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT DomainName, GroupName FROM SecurityTaskRole WHERE TaskCode = '" + TaskCode + "'"
    g_cn.Open()
    Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    Return rdr
  End Function
  Public Function GetAllTasks() As SqlDataReader
    Dim cmd As New SqlCommand
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT Sequence, TaskCode, Description FROM SecurityTask"
    g_cn.Open()
    Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    Return rdr
  End Function
End Class
