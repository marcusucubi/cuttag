Imports DCS.DCSShared
Public Class GenericCommand
	Inherits BaseDB
	Private m_cmd As System.Data.SqlClient.SqlCommand
	Private m_bIsStoredProcedure As Boolean
	Public Sub New(ByVal sql As String, Optional ByVal bIsStoredProcedure As Boolean = False)
		m_cmd = New SqlClient.SqlCommand(sql, g_cn)
		m_bIsStoredProcedure = bIsStoredProcedure
		If m_bIsStoredProcedure Then
			m_cmd.CommandType = CommandType.StoredProcedure
		Else
			m_cmd.CommandType = CommandType.Text
		End If
	End Sub
	Public Function ExecuteScaler(Optional ByVal bOpenCloseConnection As Boolean = False) As Object
		Dim retValue As New Object
		Dim bSuccess As Boolean = False
		bSuccess = Me.SQLCommandExecute(g_cn, , , , False, m_cmd, , , bOpenCloseConnection, Me.m_bIsStoredProcedure, False, retValue)
		Return retValue
	End Function
	Public Function Execute(Optional ByVal bOpenCloseConnection As Boolean = True) As Boolean
		Return Me.SQLCommandExecute(g_cn, , , , True, m_cmd, , , bOpenCloseConnection, Me.m_bIsStoredProcedure)
	End Function
	Public Function Execute(ByRef dsDataSet2Fill As DataSet, ByVal sTableName As String, Optional ByVal bOpenCloseConnection As Boolean = True, _
	 Optional ByVal bIsDataSetUpDate As Boolean = False) As Boolean
		Return Me.SQLCommandExecute(g_cn, dsDataSet2Fill, , sTableName, False, m_cmd, , , bOpenCloseConnection, , bIsDataSetUpDate)
	End Function
	Public Sub AddParameter(ByVal sParamName As String, ByVal bValue As Boolean, Optional ByVal bIsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "", Optional ByVal bIsLongString As Boolean = False)
		AddParameterBoolean(m_cmd, sParamName, bValue, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal sParamName As String, ByVal gValue As Guid, Optional ByVal bIsOutPut As Boolean = False, _
	 Optional ByVal sColumnName As String = "")
		AddParameterGUID(m_cmd, sParamName, gValue, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal sParamName As String, ByVal dValue As Date, Optional ByVal bIsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "")
		AddParameterDate(m_cmd, sParamName, dValue, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal sParamName As String, ByVal sValue As String, Optional ByVal bIsOutPut As Boolean = False, _
	Optional ByVal sColumnName As String = "")
		AddParameterString(m_cmd, sParamName, sValue, 0, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal sParamName As String, ByVal iValue As Integer, Optional ByVal bIsOutPut As Boolean = False, _
	 Optional ByVal sColumnName As String = "")
		AddParameterInt(m_cmd, sParamName, iValue, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal bIsMoney As Boolean, ByVal sParamName As String, ByVal dMoneyValue As Decimal, Optional ByVal bIsOutPut As Boolean = False, _
	Optional ByVal sColumnName As String = "")
		AddParameterMoney(m_cmd, sParamName, dMoneyValue, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal sParamName As String, ByVal dValue As Double, Optional ByVal bIsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "")
		AddParameterDouble(m_cmd, sParamName, dValue, bIsOutPut, sColumnName)
	End Sub
	Public Sub AddParameter(ByVal sParamName As String, ByVal prmType As DCSCommandParameterType, Optional ByVal bIsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "")
		Select Case prmType
			Case DCSCommandParameterType.prmBoolean
				AddParameterBoolean(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmGUID
				AddParameterGUID(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmDateTime
				AddParameterDate(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmString
				AddParameterString(m_cmd, sParamName, Nothing, 0, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmNText
				AddParameterNText(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmInt
				AddParameterInt(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmMoney
				AddParameterMoney(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
			Case DCSCommandParameterType.prmDouble
				AddParameterDouble(m_cmd, sParamName, Nothing, bIsOutPut, sColumnName)
		End Select
	End Sub
End Class
