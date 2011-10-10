Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class MachineSetupInfoDB
  Public Function GetSetupInfo(ByVal iMachineID As Integer) As dsMachine.MachineSetupInfoDataTable
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' MachineID = 0 -> attribute applies to any machine
    ' If MachineID = 0 and X (X<>0) for same attribute, use only X
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim cmd As New SqlCommand
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    'Get attribute with MachineID = 0 last
    cmd.CommandText = "SELECT Attribute, IsTerminal, SetupHours, ReverseSort " _
       + "FROM MachineSetupInfo WHERE OrganizationID = " + g_iOrganizationID.ToString _
       + " AND (MachineID = " + iMachineID.ToString + " OR MachineID = 0) " _
       + " ORDER BY MachineID DESC"
    Dim dtInfo As New dsMachine.MachineSetupInfoDataTable
    Try
      g_cn.Open()
      Dim alAttribute As New ArrayList
      Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
      If rdr.HasRows Then
        While rdr.Read
          If Not alAttribute.Contains(rdr("Attribute")) Then 'Discard if already added
            alAttribute.Add(rdr("Attribute"))
            dtInfo.AddMachineSetupInfoRow(rdr("Attribute"), rdr("IsTerminal"), rdr("SetUpHours"), rdr("ReverseSort"))
          End If
        End While
      Else
        MsgBox("The MachineSetupInfo is empty for your organization for Machine " + iMachineID.ToString)
      End If
    Catch ex As Exception
      MsgBox("Problem loading MachineSetupInfo from the database" + ControlChars.CrLf + ex.Message)
    Finally
      If g_cn.State = ConnectionState.Open Then g_cn.Close()
    End Try
    Return dtInfo
  End Function
End Class
