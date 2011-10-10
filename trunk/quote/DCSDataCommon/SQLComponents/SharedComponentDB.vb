Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports DCS.DCSShared
Public Class SharedComponentDB
  Public Function GetSharedComponents(ByVal gPartID As Guid, ByRef dt As DCSDataTable) As String
    Dim retValue As String = ""
    Do 'Single Pass Loop
      Try
        dt.Clear()
      Catch ex As Exception
        MessageBox.Show("Problem clearing MultiComponentDetail in WireCoTermCheck" _
         + ControlChars.CrLf + ControlChars.CrLf + ex.message)
      End Try
      '*******************************************************
      'xxxxxxxxxxx Populate MultiComponentDetail with coterminated wires 
      '   exclude current wire
      '   populated table will be used for calulating
      '     striplength in dgPart_DataSourceChanged event
      '*******************************************************
			'Dim s As String
      Try
        Dim sql As String = _
          "SELECT t.WireComponentSourceID, wcs.PartNumber, t.UserName, t.Position, t.PositionText, t.SharedCount, cc.Description, " _
          + " cc.IsTerminal, cc.IsMold, cc.IsConnector FROM " _
          + " (SELECT wc.WireComponentSourceID, wc.UserName, wc.Position,MAX(wc.PositionText) AS PositionText, " _
          + "  COUNT(wc.Position) AS SharedCount FROM Wire AS w  " _
          + "  LEFT JOIN WireComponent AS wc ON wc.WireID = w.WireID  " _
          + "  LEFT JOIN WireComponentSource AS wcs ON wc.WireComponentSourceId = wcs.WireComponentSourceID  " _
          + "  LEFT JOIN ComponentClass AS cc ON wcs.ClassID = cc.ClassID " _
          + "  WHERE w.PartID = '" + gPartID.ToString + "' " _
          + "   AND cc.IsTerminal = 1 " _
          + "   AND NOT UserName IS NULL " _
          + "  GROUP BY wc.WireComponentSourceID, wc.UserName, wc.Position " _
          + " UNION   " _
          + " SELECT wc.WireComponentSourceID, wc.UserName, -1 AS Position, '' AS PositionText, COUNT(wc.UserName)  AS SharedCount FROM Wire AS w  " _
          + "  LEFT JOIN WireComponent AS wc ON wc.WireID = w.WireID  " _
          + "  LEFT JOIN WireComponentSource AS wcs ON wc.WireComponentSourceId = wcs.WireComponentSourceID  " _
          + "  LEFT JOIN ComponentClass AS cc ON wcs.ClassID = cc.ClassID " _
          + "  WHERE w.PartID = '" + gPartID.ToString + "' " _
          + "   AND cc.IsTerminal = 0 " _
          + "   AND NOT UserName IS NULL " _
          + "  GROUP BY wc.WireComponentSourceID, wc.UserName/*, wc.Position*/ " _
          + " ) AS t " _
          + " LEFT JOIN WireComponentSource AS wcs ON t.WireComponentSourceId = wcs.WireComponentSourceID  " _
          + " LEFT JOIN ComponentClass AS cc ON wcs.ClassID = cc.ClassID "

        '+ " UNION " _
        '+ " SELECT  wc.WireComponentSourceID, wc.UserName, wc.Position, 1 AS SharedCount FROM Wire AS w   " _
        '+ "  LEFT JOIN WireComponent AS wc ON wc.WireID = w.WireID  " _
        '+ "  WHERE w.PartID = '" + gPartID.ToString + "' " _
        '+ "   AND UserName IS NULL " _


        Dim da As New SqlDataAdapter(sql, g_cn)
        da.MissingSchemaAction = MissingSchemaAction.AddWithKey
        da.MissingMappingAction = MissingMappingAction.Passthrough
        da.Fill(dt)
        's = " Wire: " + drParent("WireNumber") + drParent("LNode") _
        ' + drParent("RNode") + " End: " + sWireEnd _
        ' + " will join " + ControlChars.CrLf
      Catch ex As Exception
        MessageBox.Show("Problem in GetSharedComponents." + ControlChars.CrLf + ex.Message.ToString)
        Exit Do
      End Try
      Exit Do
    Loop 'Single Pass Loop
    Return retValue
  End Function
End Class
