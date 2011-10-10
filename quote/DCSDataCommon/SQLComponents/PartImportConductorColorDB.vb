Imports System.Data
Imports DCS.DCSShared
Imports System.Data.SqlClient
Public Class PartImportConductorColorDB
	''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'Use to store user choices for conductor color found in the import file
	' but not in the Color table
	' Prevents forcing user to repeatedly choose the same colors 
	''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	Public Sub SaveColor(ByVal sColor As String, ByVal ConductorColor As String)
		Try
			g_cn.Open()
			If IsNothing(GetConductorColor(sColor)) Then
				Add(sColor, ConductorColor)
			Else
				Update(sColor, ConductorColor)
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
		Finally
			If Not IsNothing(g_cn) AndAlso g_cn.State = ConnectionState.Open Then
				g_cn.Close()
			End If
		End Try
	End Sub
	Public Function GetConductorColor(ByVal PartImportColor As String) As String
		Dim cmd As New SqlCommand
		Dim retValue As String
		cmd.Connection = g_cn
		Dim cnnState As System.Data.ConnectionState = g_cn.State
		If Not cnnState = ConnectionState.Open Then g_cn.Open()
		cmd.CommandType = CommandType.Text
		cmd.CommandText = "SELECT ConductorColor FROM PartImportConductorColor " _
		 + "WHERE OrganizationID = " + g_iOrganizationID.ToString + " AND PartImportColor = '" + PartImportColor + "'"
		retValue = cmd.ExecuteScalar()
		If cnnState = ConnectionState.Closed Then g_cn.Close()
		Return retValue
	End Function
	Private Sub Update(ByVal sPartImportColor As String, ByVal sConductorColor As String)
		Dim cmd As New SqlCommand
		cmd.Connection = g_cn
		cmd.CommandType = CommandType.Text
		cmd.CommandText = "UPDATE PartImportConductorColor SET ConductorColor = '" + sConductorColor _
		+ "' WHERE OrganizationID = " + g_iOrganizationID.ToString _
		+ " AND PartImportColor = '" + sPartImportColor + "'"
		cmd.ExecuteNonQuery()
	End Sub
	Private Sub Add(ByVal sPartImportColor As String, ByVal sConductorColor As String)
		Dim cmd As New SqlCommand
		cmd.Connection = g_cn
		cmd.CommandType = CommandType.Text
		cmd.CommandText = "INSERT PartImportConductorColor VALUES (" + g_iOrganizationID.ToString + " , '" _
		+ sPartImportColor + "', '" + sConductorColor + "')"
		cmd.ExecuteNonQuery()
	End Sub
End Class
