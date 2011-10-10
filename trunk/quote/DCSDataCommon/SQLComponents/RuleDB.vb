Imports System.Data.SqlClient
Imports DCS.DCSShared
Public Class RuleDB
  Private m_ds As dsRules
  Public Function GetRulesDs(ByVal Type As RuleType, ByVal Source As Integer, _
      Optional ByVal sSort As String = Nothing) As dsRules
    m_ds = New dsRules
    Dim sqlS As String = "SELECT "
    Dim sqlJ As String
    Dim sqlF As String = "FROM [Rule] r "
    Dim sqlW As String = "WHERE r.OrganizationID = " + g_iOrganizationID.ToString _
      + " AND r.Type = " + CType(Type, Integer).ToString _
      + " AND r.Source = " + Source.ToString
    m_ds.Tables("RuleTCP").Rows.Clear()
    m_ds.Tables("Rule").Rows.Clear()
    If sSort Is Nothing Then sSort = " r.Priority, r.Flexibility DESC"
    If Not sSort = "" Then sSort = " ORDER BY " + sSort
    FillTable("Rule", sqlS + "* " + sqlF + sqlW + sSort)
    If Type = RuleType.MachineLoading Then
      sqlJ = "INNER JOIN RuleTCP t ON r.RuleID = t.RuleID "
      FillTable("RuleTCP", sqlS + "t.RuleID, t.Sort, t.FillFraction, t.X, t.Y, t.TerminateX, t.TerminateY " + sqlF + sqlJ + sqlW + " ORDER BY t.Sort")
    End If
    Return m_ds
  End Function
  Private Sub FillTable(ByVal sTableName As String, ByVal sql As String)
    Dim da As New SqlDataAdapter(sql, g_cn)
    da.MissingMappingAction = MissingMappingAction.Passthrough
    da.MissingSchemaAction = MissingSchemaAction.Error
    da.Fill(m_ds.Tables(sTableName))
  End Sub
  Public Function GetHighestPriority() As Integer
    Dim cmd As New SqlCommand
    Dim retValue As Integer
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT MAX(TerminationClassID) FROM TerminationClass"
    g_cn.Open()
    retValue = cmd.ExecuteScalar() + 1
    g_cn.Close()
    Return retValue
  End Function
  Public Function GetTerminationClassPriority(ByVal iBatchType As Integer, ByVal iClass As Integer, ByVal gID As Guid) As Integer
    Dim cmd As New SqlCommand
    Dim retValue As Integer
    cmd.Connection = g_cn
    cmd.CommandType = CommandType.Text
    cmd.CommandText = "SELECT Priority FROM BatchTypeComponent WHERE " _
      + " OrganizationID = " + g_iOrganizationID.ToString + " AND " _
      + " BatchTypeID = " + iBatchType.ToString + " AND " _
      + " TerminationClassID = " + iClass.ToString + " AND " _
      + " WireComponentSourceID = '" + gID.ToString + "'"
    g_cn.Open()
    retValue = cmd.ExecuteScalar()
    g_cn.Close()
    Return retValue
  End Function
End Class
