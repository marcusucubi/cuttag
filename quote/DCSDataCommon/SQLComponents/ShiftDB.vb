Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class ShiftDB
  Public Function GetAvailableHours() As Double
    Dim cmd As New SqlCommand
    Dim retValue As Double
    Try
      cmd.Connection = g_cn
      cmd.CommandType = CommandType.Text
      cmd.CommandText = "SELECT Sum(ShiftHours) FROM Shift WHERE " _
        + " OrganizationID = " + g_iOrganizationID.ToString + " AND " _
        + " Active = 1 " 
      g_cn.Open()
      retValue = cmd.ExecuteScalar()
    Catch ex As Exception
      MsgBox("Problem getting available hours for active shifts. Will use 8 hours. Error: " + _
        ControlChars.CrLf + ControlChars.CrLf + ex.Message)
      retValue = 8
    Finally
      If g_cn.State = ConnectionState.Open Then g_cn.Close()
    End Try
    Return retValue
  End Function
End Class
