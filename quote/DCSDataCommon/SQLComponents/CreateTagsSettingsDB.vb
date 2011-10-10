Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class CreateTagsSettingsDB
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	'Purpose: To read/write data used in frmBatchSetup
	'  currently called from BreakLevels.UpdateDatabase
	'12/12/2010-created
	'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
	Public Function GetBatchType(ByRef ds As dsTags) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetBatchType"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		'temp eliminate after all on 4.1.5 and HC_GetBatchType changed to always supply IsDefault
		cmd.AddParameter("@NewVersion", True)


		bRetValue = cmd.Execute(CType(ds, System.Data.DataSet), "BatchType", False)
		Return bRetValue
	End Function
	Public Function UpdateBatchType(ByRef ds As dsTags) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_UpdateBatchType"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@BatchTypeID", DCSCommandParameterType.prmInt, , ds.BatchType.BatchTypeIDColumn.ColumnName)
		cmd.AddParameter("@Enabled", DCSCommandParameterType.prmBoolean, , ds.BatchType.EnabledColumn.ColumnName)
		bRetValue = cmd.Execute(CType(ds, System.Data.DataSet), "BatchType", False, True)
		Return bRetValue
	End Function
	Public Function GetTagsSortOrder(ByRef ds As dsTags) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetTagsSortOrder"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		bRetValue = cmd.Execute(CType(ds, System.Data.DataSet), "TagsSortOrder", False)
		Return bRetValue
	End Function
	Public Function UpdateTagsSortOrder(ByRef ds As dsTags) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_UpdateTagsSortOrder"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@BatchTypeID", DCSCommandParameterType.prmInt, , ds.TagsSortOrder.BatchTypeIDColumn.ColumnName)
		cmd.AddParameter("@Attribute", DCSCommandParameterType.prmString, , ds.TagsSortOrder.AttributeColumn.ColumnName)
		cmd.AddParameter("@SortOrder", DCSCommandParameterType.prmInt, , ds.TagsSortOrder.SortOrderColumn.ColumnName)
		bRetValue = cmd.Execute(CType(ds, System.Data.DataSet), "TagsSortOrder", False, True)
		Return bRetValue
	End Function
	Public Function GetSuppressBatchTags() As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_GetSuppressBatchTags"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		bRetValue = cmd.ExecuteScaler()
		Return bRetValue
	End Function
	Public Function SetSuppressBatchTags(ByVal bSuppressBatchTags As Boolean) As Boolean
		Dim sql As String
		Dim bRetValue As Boolean = False
		sql = "HC_UpdateSuppressBatchTags"
		Dim cmd As New GenericCommand(sql, True)
		cmd.AddParameter("@OrganizationID", g_iOrganizationID)
		cmd.AddParameter("@SuppressBatchTags", bSuppressBatchTags)
		bRetValue = cmd.Execute(False)
		Return bRetValue
	End Function
End Class
