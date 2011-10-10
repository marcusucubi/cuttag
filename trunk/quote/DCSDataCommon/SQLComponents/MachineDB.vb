Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class MachineDB
  Public Function IsActive(ByVal iMachineID As Integer) As Boolean
    Dim retValue As Boolean = False
    Dim cmd As New SqlCommand
    Try
      cmd.Connection = g_cn
      cmd.CommandType = CommandType.Text
      cmd.CommandText = "SELECT Active FROM Machine WHERE OrganizationID = " + g_iOrganizationID.ToString _
         + " AND MachineID = " + iMachineID.ToString
      g_cn.Open()
      retValue = cmd.ExecuteScalar
    Catch ex As Exception
      MsgBox("Problem getting machine with ID of " + iMachineID.ToString + ". " _
        + ControlChars.CrLf + ex.Message)
    Finally
      If g_cn.State = ConnectionState.Open Then g_cn.Close()
    End Try
    Return (retValue)
  End Function

  Public Function GetMachineSpecs(ByVal iMachineID As Integer) As SqlDataReader
    Dim cmd As New SqlCommand
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT MachineName, MachineDescription, SetupHoursPerTag, FillFraction, Active, FixedUse " _
       + "FROM Machine WHERE OrganizationID = " + g_iOrganizationID.ToString _
       + " AND MachineID = " + iMachineID.ToString
    g_cn.Open()
    Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
    Return rdr
  End Function
End Class
