Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class BomDB
	Private m_dsPart As DCS.dsBOM
	Public Sub New(ByRef ds As DCS.dsBOM)
		m_dsPart = ds
	End Sub
	Public Function Fill_dsBOM(ByVal gPartID As Guid) As Boolean
		Dim retvalue As Boolean = False
		GetPartComponents(gPartID)
		GetSingleConductors(gPartID)
		GetWireComponents(gPartID)
		GetMultiConductorCable(gPartID)
		Return retvalue
	End Function
	Public Function GetSingleConductors(ByVal gPartID As Guid) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetSingleConductors4BOM"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@PartID", gPartID)
		bRetValue = cmd.Execute(CType(m_dsPart, System.Data.DataSet), "SingleConductors", False)
		Return bRetValue
	End Function

	Public Function GetMultiConductorCable(ByVal gPartID As Guid) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetMultiConductorCable4BOM"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@PartID", gPartID)
		bRetValue = cmd.Execute(CType(m_dsPart, System.Data.DataSet), "MultiConductorCable", False)
		Return bRetValue
	End Function
	Public Function GetWireComponents(ByVal gPartID As Guid) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetWireComponents4BOM"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@PartID", gPartID)
		bRetValue = cmd.Execute(CType(m_dsPart, System.Data.DataSet), "WireComponents", False)
		Return bRetValue
	End Function
	Public Function GetPartComponents(ByVal gPartID As Guid) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetPartComponents4BOM"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@PartID", gPartID)
		bRetValue = cmd.Execute(CType(m_dsPart, System.Data.DataSet), "PartComponents", False)
		Return bRetValue
	End Function
End Class
