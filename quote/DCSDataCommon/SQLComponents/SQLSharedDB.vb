Imports DCS.DCSShared
Imports System.Data
Imports System.Data.SqlClient
' Probably should be moved to BaseDB.vb
Public Class SQLSharedDB
	Public Enum paramType As Integer 'this is duplicated in Shared.vb
		BooleanPrm = 1
		GUIDPrm = 2
		DatePrm = 3
		StringPrm = 4
		NTextPrm = 5
		IntPrm = 6
		MoneyPrm = 7
		DoublePrm = 8
	End Enum
	Public Structure paramStruct
		Public Value As Object
		Public Type As paramType
	End Structure

	Public Shared Function FillDataTable(ByRef dt As DataTable, ByVal sql As String) As System.Data.DataTable
		Dim da As New SqlDataAdapter(sql, g_cn)
		da.MissingMappingAction = MissingMappingAction.Passthrough
		da.MissingSchemaAction = MissingSchemaAction.Error
		da.Fill(dt)
		Return dt
	End Function
	Public Shared Function GetReader(ByVal sql As String) As SqlDataReader
		Dim cmd As New SqlCommand
		cmd.Connection = g_cn
		cmd.CommandType = CommandType.Text
		cmd.CommandText = sql
		g_cn.Open()
		Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
		Return rdr
	End Function
	Public Shared Function GetIntegerScaler(ByVal sql As String) As Integer
		Dim retValue As Integer = 0
		Dim cmd As New SqlCommand
		cmd.Connection = g_cn
		cmd.CommandType = CommandType.Text
		cmd.CommandText = sql
		g_cn.Open()
		Dim ob As Object
		Try
			ob = cmd.ExecuteScalar()
			If ob.GetType Is System.Type.GetType("System.Int32") Then
				retValue = CType(ob, Integer)
			End If
		Catch ex As Exception
			Throw New Exception("Problem with GetIntegerScaler in SQLSharedDB. " + ex.Message)
		Finally
			If Not g_cn.State = ConnectionState.Closed Then
				g_cn.Close()
			End If
		End Try
		Return retValue
	End Function
	Public Shared Function GetStringScaler(ByVal sql As String, Optional ByVal bIsStoredProc As Boolean = False) As String
		Dim retValue As String = ""
		Dim cmd As New SqlCommand
		cmd.Connection = g_cn
		If bIsStoredProc Then
			cmd.CommandType = CommandType.StoredProcedure
		Else
			cmd.CommandType = CommandType.Text
		End If
		cmd.CommandText = sql
		g_cn.Open()
		Dim ob As Object
		Try
			ob = cmd.ExecuteScalar()
			If ob.GetType Is System.Type.GetType("System.String") Then
				retValue = CType(ob, String)
			End If
		Catch ex As Exception
			Throw New Exception("Problem with GetStringScaler in SQLSharedDB. " + ex.Message)
		Finally
			If Not g_cn.State = ConnectionState.Closed Then
				g_cn.Close()
			End If
		End Try
		Return retValue
	End Function
	Public Shared Function UpdateStringColumn(ByVal sql As String, Optional ByVal bIsStoredProc As Boolean = False, Optional ByVal sValue As String = "") As Boolean
		Dim retValue As Boolean = False
		Dim cmd As New SqlCommand
		cmd.Connection = g_cn
		If bIsStoredProc Then
			cmd.CommandType = CommandType.StoredProcedure
			cmd.Parameters.Add("@Param1", SqlDbType.NVarChar, 50)
			cmd.Parameters(0).Value = sValue
		Else
			cmd.CommandType = CommandType.Text
		End If
		cmd.CommandText = sql
		g_cn.Open()
		Dim iResult As Integer
		Try
			iResult = cmd.ExecuteNonQuery
			If iResult = 1 Then
				retValue = True
			End If
		Catch ex As Exception
			Throw New Exception("Problem with UpdateStringColumn in SQLSharedDB. " + ex.Message)
		Finally
			If Not g_cn.State = ConnectionState.Closed Then
				g_cn.Close()
			End If
		End Try
		Return retValue
	End Function

End Class
