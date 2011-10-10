Imports System.Data.SqlClient
Imports DCS.DCSShared

Public Class ComputerHistoryDB
  Private m_ComputerName As String
  Private m_UserName As String
  Private m_OperatingSystem As String
  Private m_HCVersion As String

  Public Sub UpdateHistory(ByVal ComputerName As String, ByVal UserName As String, ByVal OperatingSystem As String, ByVal HCVersion As String)
    Try
      g_cn.Open()
      If RecordExists(ComputerName, UserName) Then
        Update(ComputerName, UserName, OperatingSystem, HCVersion)
      Else
        Add(ComputerName, UserName, OperatingSystem, HCVersion)
      End If
    Catch ex As Exception
      MsgBox(ex.Message)
    Finally
      If Not IsNothing(g_cn) AndAlso g_cn.State = ConnectionState.Open Then
        g_cn.Close()
      End If
    End Try

  End Sub
  Private Function RecordExists(ByVal ComputerName As String, ByVal UserName As String) As Boolean
    Dim cmd As New SqlCommand
    Dim rValue As Boolean
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT UserName FROM ComputerHistory " _
      + "WHERE ComputerName = '" + ComputerName + "' AND UserName = '" + UserName + "'"
    If IsNothing(cmd.ExecuteScalar()) Then
      rValue = False
    Else
      rValue = True
    End If
    Return rValue
  End Function
  Private Sub Update(ByVal ComputerName As String, ByVal UserName As String, ByVal OperatingSystem As String, ByVal HCVersion As String)
    Dim cmd As New SqlCommand
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "UPDATE ComputerHistory SET OperatingSystem = '" + OperatingSystem _
      + "', HCVersion = '" + HCVersion + "', DateLastUsed = '" + _
      Now.ToString(System.Globalization.CultureInfo.InvariantCulture) + "'" _
      + " WHERE ComputerName = '" + ComputerName + "' AND UserName = '" + UserName + "'"
    cmd.ExecuteNonQuery()
  End Sub
  Private Sub Add(ByVal ComputerName As String, ByVal UserName As String, ByVal OperatingSystem As String, ByVal HCVersion As String)
    Dim cmd As New SqlCommand
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "INSERT ComputerHistory VALUES('" + ComputerName + "', '" _
      + UserName + "', '" + OperatingSystem + "', '" + HCVersion + "', '" + Now.ToString + "')"
    cmd.ExecuteNonQuery()
  End Sub
End Class
