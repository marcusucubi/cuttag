Imports System.Data
Imports DCS.DCSShared
Imports System.Data.SqlClient
Public Class PartImportPartInfo
	Public Function AddPartInfoData(ByVal gPartID As Guid, ByVal gLayoutID As Guid) As Boolean
		Dim cmd As New SqlCommand
		Dim rValue As Integer
		Try
			cmd.Connection = g_cn
			cmd.CommandType = CommandType.Text
			cmd.CommandText = "INSERT INTO PartInfo SELECT '" + gPartID.ToString + "',  SequenceNumber, PartInfoCodeId, " _
			 + "InfoText FROM PartImportPartInfo WHERE LayoutID = '" + gLayoutID.ToString + "'"
			g_cn.Open()
			rValue = cmd.ExecuteNonQuery()
			If rValue < 1 Then
				MsgBox("No PartImportPartInfo data exits for this layout.  The circuit name used for reports will be the full " _
				 + " Circuit + LNode + RNode")
			End If
			g_cn.Close()

		Catch ex As Exception
			MsgBox("A problem occured while setting Part Info for the selected layout.  " + ex.Message)
		End Try
	End Function
End Class
