Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class StripLength
  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  'Purpose: To set striplength for one part or all parts
  '  for one component or all components
  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	Function SetStripLength(ByVal gPartID As Guid, ByVal gSourceID As Guid) As Boolean
		Dim retValue As Boolean = False
		If Not gPartID.Equals(Guid.Empty) Then
		End If
		Dim sql As String
		Try
			g_cn.Open()
      sql = GetSQL("L", False, gPartID, gSourceID)
			Process(sql)
      sql = GetSQL("R", False, gPartID, gSourceID)
			Process(sql)
			sql = GetSQL("L", True, gPartID, gSourceID)
			Process(sql)
			sql = GetSQL("R", True, gPartID, gSourceID)
			Process(sql)
			retValue = True
			g_cn.Close()
		Catch ex As Exception
			MsgBox("Problem Updating Striplength: " + ex.Message)
		Finally
			If Not g_cn.State = ConnectionState.Closed Then
				g_cn.Close()
			End If
		End Try
		Return retValue
	End Function
	Private Function Process(ByVal sql As String) As Integer
		Dim cmd As New SqlCommand(sql, g_cn)
		cmd.CommandTimeout = 6000
		Return cmd.ExecuteNonQuery()
	End Function
	Private Function GetSQLColumnName(ByVal sTableName As String, ByVal iColumnID As Integer) As String
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		'Temp function to be used with sql database column names are in transition
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		Dim retValue As String = ""
		Dim st As ConnectionState = g_cn.State
		Try
			If Not st = ConnectionState.Open Then g_cn.Open()
			Dim cmd As New SqlCommand("SELECT Col_Name(Object_ID('" + sTableName + "')," + iColumnID.ToString + ")", g_cn)
			retValue = cmd.ExecuteScalar
		Catch ex As Exception
			System.Windows.Forms.MessageBox.Show(("Problem getting XStripLength columns from database" _
		+ vbCrLf + "Full Error Message:" + vbCrLf + ex.ToString()))
		Finally
			If st = ConnectionState.Closed And g_cn.State = ConnectionState.Open Then g_cn.Close()
		End Try
		Return retValue
	End Function
	Private Function GetSQL(ByVal sWireEnd As String, ByVal bNullUserName As Boolean, _
	 ByVal gPartID As Guid, ByVal gSourceID As Guid) As String
		Dim sAttribute As String
		'Next line remmed 5/1/10
		'GetSQLColumnName("Wire", 15)
		'If sWireEnd = "L" Then
		'	sAttribute = "EndAStripLength"
		'Else
		'	sAttribute = "EndBStripLength"
		'End If
		' changed 5/1/10
		'If sWireEnd = "L" Then
		'	sAttribute = GetSQLColumnName("Wire", 15)			'Can change to LStripLength - SLDBChange
		'Else
		'	sAttribute = GetSQLColumnName("Wire", 16)			'Can change to RStripLength - SLDBChange
		'End If
		If sWireEnd = "L" Then
			sAttribute = "LStripLength"
		Else
			sAttribute = "RStripLength"
		End If

		Dim sql As String
		Dim sqlPart As String = ""
		Dim sqlSource As String = ""
		If Not gPartID.Equals(Guid.Empty) Then
			sqlPart = "partID = '" + gPartID.ToString + "' "
		End If
		If Not gSourceID.Equals(Guid.Empty) Then
			sqlSource += " AND wire.wireID in " _
			 + "(SELECT w.wireID FROM wire w LEFT JOIN wirecomponent wc ON " _
			 + "w.wireid = wc.wireid " _
			 + "WHERE wc.wirecomponentsourceid = '" + gSourceID.ToString + "')"
		End If
		If bNullUserName Then
			sql = "UPDATE wire SET " + sAttribute + " = " _
			 + "t.striplength FROM " _
			 + "(SELECT w.wireid, sl.striplength FROM part p LEFT JOIN wire w ON p.partid = w.partid LEFT JOIN wirecomponent wc " _
			 + "  ON w.wireid = wc.wireid " _
			 + "  LEFT JOIN wirecomponentsource cs " _
			 + "  ON cs.wirecomponentsourceid = wc.wirecomponentsourceid " _
			 + "  LEFT JOIN striplength sl " _
			 + "  ON sl.wirecomponentsourceid = wc.wirecomponentsourceid " _
			 + "  WHERE wc.username Is null " _
			 + "  AND wc.wireend = '" + sWireEnd + "' " _
			 + "  AND sl.conductorcount = 1 AND p.organizationid = " + g_iOrganizationID.ToString + " " _
			 + IIf(gSourceID.Equals(Guid.Empty), "", " AND wc.wirecomponentsourceid = '" + gSourceID.ToString + "' ") _
			 + ") AS t " _
			 + "WHERE t.wireid = wire.wireid " _
			 + IIf(sqlPart = "", "", "AND wire." + sqlPart)
		Else		'For Co-terminations
			sql = "UPDATE wire SET " + sAttribute + " = tt.striplength FROM " _
			 + "(SELECT w.wireid, sl.striplength FROM " _
			 + "part p LEFT JOIN wire w ON w.partid = p.partid " _
			 + "LEFT JOIN wirecomponent wc ON wc.wireid = w.wireid " _
			 + "Left Join " _
			 + "(select w.partid, wc.wirecomponentsourceid, wc.username, wc.position, " _
			 + "COUNT(wc.position) AS conductorcount FROM wire w " _
			 + "LEFT JOIN wirecomponent wc ON wc.wireid = w.wireid " _
			 + "LEFT JOIN wirecomponentsource cs ON cs.wirecomponentsourceid = wc.wirecomponentsourceid " _
			 + IIf(sqlPart = "", "", "WHERE w." + sqlPart) _
			 + "GROUP BY w.partid, wc.username, wc.wirecomponentsourceid, wc.position " _
			 + ") AS t " _
			 + "ON t.wirecomponentsourceid = wc.wirecomponentsourceid " _
			 + "AND " _
			 + "t.username = wc.username " _
			 + "AND " _
			 + "t.position = wc.position " _
			 + "AND " _
			 + "t.partID = w.partID " _
			 + "LEFT JOIN striplength sl " _
			 + "ON  sl.wirecomponentsourceid = t.wirecomponentsourceid " _
			 + "AND " _
			 + "sl.conductorcount = t.conductorcount " _
			 + "WHERE p.organizationid = " + g_iOrganizationID.ToString + " AND " _
			 + "sl.striplength is not null " _
			 + "AND wc.wireend = '" + sWireEnd + "' " _
			 + IIf(sqlPart = "", "", "AND p." + sqlPart) _
			 + IIf(gSourceID.Equals(Guid.Empty), "", " AND wc.wirecomponentsourceid = '" + gSourceID.ToString + "' ") _
			 + ") AS tt WHERE tt.wireid = wire.wireid " _
			 + " AND ((" + sAttribute + " IS NULL) OR (NOT tt.striplength = " + sAttribute + ")) " _
			 + IIf(sqlPart = "", "", "AND wire." + sqlPart)
		End If
		Return sql
	End Function

End Class
