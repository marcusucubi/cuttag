Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class WireLength
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'Purpose: To set Wirelength for one part or all parts
	'  for one component or all components
	'7/1/2010-added single wire calculation
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	Public Function SetSingleWireLength(ByVal gWireID As Guid, ByVal dLength As Double) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_UpdateWireLength"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@WireID", gWireID)
		cmd.AddParameter("@Length", dLength)
		bRetValue = cmd.Execute(False)
		Return bRetValue
	End Function
	Function SetWireLength(ByVal gPartID As Guid, ByVal gSourceID As Guid) As Integer
		Dim sql As String
		Dim iRetValue As Integer = 0
		sql = "UPDATE wire SET length = wire.reflength + t.length FROM " _
		 + "(SELECT w.WireID, sum(cs.WireLengthAdjust) AS length " _
		 + "FROM wire w " _
		 + "LEFT JOIN wirecomponent wc ON wc.wireid = w.wireid " _
		 + "LEFT JOIN wirecomponentsource cs ON cs.wirecomponentsourceid = wc.wirecomponentsourceid " _
		 + "LEFT JOIN ComponentClass cc ON cs.ClassID = cc.ClassID " _
		 + "LEFT JOIN Part p ON p.PartID = w.PartID " _
		 + "WHERE cc.IsTerminal = 1 AND p.OrganizationID = " + g_iOrganizationID.ToString + " " _
		 + "GROUP BY w.WireID " _
		 + ") AS t " _
		 + "WHERE t.WireID = Wire.WireID AND NOT t.length = wire.length " _
		 + "AND wire.reflength > 0 "
		If Not gPartID.Equals(Guid.Empty) Then
			sql += "AND wire.partID = '" + gPartID.ToString + "'"
		End If
		If Not gSourceID.Equals(Guid.Empty) Then
			sql += " AND wire.wireID in " _
			 + "(SELECT w.wireID FROM wire w LEFT JOIN wirecomponent wc ON " _
			 + "w.wireid = wc.wireid " _
			 + "WHERE wc.wirecomponentsourceid = '" + gSourceID.ToString + "')"
		End If
		Try
			g_cn.Open()
			Dim cmd As New SqlCommand(sql, g_cn)
			cmd.CommandTimeout = 6000
			iRetValue = cmd.ExecuteNonQuery()
			g_cn.Close()
		Catch ex As Exception
			MsgBox("Problem Updating Wire Length: " + ex.Message)
		Finally
			If Not g_cn.State = ConnectionState.Closed Then
				g_cn.Close()
			End If
		End Try
		Return iRetValue
	End Function
End Class
