Imports System.Configuration
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class BaseDB
	Protected Sub New()
	End Sub
	Protected Sub CloseConnection(ByVal cnn As SqlConnection)
		If Not cnn Is Nothing AndAlso cnn.State = ConnectionState.Open Then
			cnn.Close()
		End If
	End Sub
	Protected Function Fill(ByVal da As SqlDataAdapter, ByVal dt As DataTable, Optional ByRef sError As String = "") As Integer
		Return DaAction("Fill", da, dt, sError)
	End Function
	Protected Function Update(ByVal da As SqlDataAdapter, ByVal dt As DataTable, Optional ByRef sError As String = "") As Integer
		Return DaAction("Update", da, dt, sError)
	End Function
	Private Function DaAction(ByVal sTask As String, ByVal da As SqlDataAdapter, ByVal dt As DataTable, ByRef sError As String) As Integer
		Dim retValue As Integer = 0
		Select Case sTask
			Case "Fill"
				retValue = da.Fill(dt)
			Case "Update"
				retValue = da.Update(dt)
		End Select
		Return retValue
	End Function
	Protected Function SQLCommandExecute(ByVal cnn As SqlConnection, _
	 Optional ByRef ds As DataSet = Nothing, Optional ByVal sql As String = "", _
	 Optional ByVal sTableName As String = "", Optional ByVal bNonQuery As Boolean = False, _
	 Optional ByVal cmd As SqlCommand = Nothing, Optional ByVal da As SqlDataAdapter = Nothing, _
	 Optional ByVal sMessage As String = "", Optional ByVal bOpenCloseCnn As Boolean = False, _
	 Optional ByVal bIsStoredProcedure As Boolean = False, Optional ByVal bIsDatasetUpdate As Boolean = False, _
	 Optional ByRef oScalar As Object = Nothing) As Boolean
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		'Fills/Updates dataset (ds) or executes nonquery command or retrieves scalar
		' ds required for QueryCommands
		' sql required if cmd or da not supplied
		' sTableName required for QueryCommands and used in Exception Text
		' bNonQuery = 0 true -> cmd.ExecuteNonQuery or ds update
		' smessage - custom exception text
		'To use stored procedure
		' build cmd or da along with parmameters before this function call
		' sql not used
		' sTableName only used for exception text
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		Dim retValue As Boolean = False
		Try
			If bOpenCloseCnn Then
				cnn.Open()
			End If
			If bNonQuery Then
				If cmd Is Nothing Then
					cmd = New SqlClient.SqlCommand(sql, cnn)
				End If
				If bIsStoredProcedure Then
					cmd.CommandType = CommandType.StoredProcedure
				Else
					cmd.CommandType = CommandType.Text
				End If
				cmd.ExecuteNonQuery()
			Else
				If oScalar Is Nothing Then
					If da Is Nothing Then
						da = New SqlDataAdapter(sql, cnn)
						If cmd Is Nothing Then
							Throw New Exception("Problem accessing data in SQLCommandExecute: cmd and sql both not allowed - please report this proplem")
						Else
							If bIsDatasetUpdate Then
								da.UpdateCommand = cmd
							Else
								da.SelectCommand = cmd
							End If
						End If
					End If
					da.MissingSchemaAction = MissingSchemaAction.Error
					da.MissingMappingAction = MissingMappingAction.Passthrough
					If bIsDatasetUpdate Then
						Update(da, ds.Tables(sTableName))
					Else
						Fill(da, ds.Tables(sTableName))
					End If
				Else
					oScalar = cmd.ExecuteScalar()
				End If
			End If
			retValue = True
		Catch ex As Exception
			Throw New Exception("Problem accessing data in SQLCommandExecute: " _
			+ sMessage + ControlChars.CrLf + ControlChars.CrLf + ex.ToString)
		Finally
			If bOpenCloseCnn Then
				cnn.Close()
			End If
		End Try
		Return retValue
	End Function
	Public Function CopyParameters(ByVal cmdSource As SqlCommand, ByVal cmdDestination As SqlCommand, Optional ByRef sError As String = "") As Boolean
		Dim retValue As Boolean = True
		Dim pSource, pDestination As SqlParameter
		Try
			For Each pSource In cmdSource.Parameters
				pDestination = cmdDestination.Parameters.Add(pSource.ParameterName, pSource.SqlDbType) ', p.Value, p.SourceColumn)
				pDestination.Value = pSource.Value
				pDestination.SourceColumn = pSource.SourceColumn
				pDestination.Size = pSource.Size
			Next
		Catch ex As Exception
			sError += " Could not copy parameters. Error: " + ex.Message
			retValue = False
		End Try
		Return retValue
	End Function
	Protected Function AddParameterBoolean(ByRef cmd As SqlCommand, ByVal pname As String, _
 ByVal booleanValue As Boolean, Optional ByVal IsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.Bit
		If sColumnName = "" Then
			prm.Value = booleanValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterGUID(ByRef cmd As SqlCommand, ByVal pname As String, _
	ByVal guidValue As Guid, Optional ByVal IsOutPut As Boolean = False, _
	Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.UniqueIdentifier
		If sColumnName = "" Then
			prm.Value = guidValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterDate(ByRef cmd As SqlCommand, ByVal pname As String, _
 ByVal dateValue As DateTime, Optional ByVal IsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.DateTime
		If sColumnName = "" Then
			prm.Value = dateValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterString(ByRef cmd As SqlCommand, ByVal pname As String, _
 ByVal stringValue As String, ByVal iLength As Integer, Optional ByVal IsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.NVarChar
		If iLength > 0 Then
			prm.Size = iLength
		End If
		If sColumnName = "" Then
			prm.Value = stringValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterNText(ByRef cmd As SqlCommand, ByVal pname As String, _
	ByVal textValue As String, Optional ByVal IsOutPut As Boolean = False, _
	Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.NText
		If sColumnName = "" Then
			prm.Value = textValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterInt(ByRef cmd As SqlCommand, ByVal pname As String, _
 ByRef integerValue As Integer, Optional ByVal IsOutPut As Boolean = False, _
 Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.Int
		If sColumnName = "" Then
			prm.Value = integerValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterMoney(ByRef cmd As SqlCommand, ByVal pname As String, _
	ByVal moneyValue As Decimal, Optional ByVal IsOutPut As Boolean = False, _
	Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.Money
		If sColumnName = "" Then
			prm.Value = moneyValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Protected Function AddParameterDouble(ByRef cmd As SqlCommand, ByVal pname As String, _
	ByVal doubleValue As Double, Optional ByVal IsOutPut As Boolean = False, _
	Optional ByVal sColumnName As String = "") As SqlParameter
		Dim prm As SqlParameter = ConstructParameter(cmd, pname, IsOutPut)
		prm.SqlDbType = SqlDbType.Float
		If sColumnName = "" Then
			prm.Value = doubleValue
		Else
			prm.SourceColumn = sColumnName
		End If
		Return prm
	End Function
	Private Function ConstructParameter(ByRef cmd As SqlCommand, _
	ByVal pname As String, Optional ByVal IsOutPut As Boolean = False) As SqlParameter
		Dim prm As SqlParameter = New SqlParameter
		prm.ParameterName = pname
		If IsOutPut Then
			prm.Direction = ParameterDirection.Output
		End If
		cmd.Parameters.Add(prm)
		Return prm
	End Function
End Class
