Imports System.String
Public Class frmQuoteMain
	Private Sub frmQuoteMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Me.tabQuote.Visible = False
		With Me.dgvSearchResults
			.AllowUserToAddRows = False
			.AllowUserToDeleteRows = False
			.SelectionMode = DataGridViewSelectionMode.FullRowSelect
			.EditMode = DataGridViewEditMode.EditProgrammatically
		End With
		Me.tabQuote.TabPages.Remove(Me.tpgShow)
		Me.lblSelectAndShow.Visible = False
	End Sub
	Private Sub frmQuoteMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
		Me.tabQuote.Width = Me.Width - 14
		Me.tabQuote.Height = Me.Height - 52
	End Sub
	Private Sub btnSeach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeach.Click
		Dim oQuoteDB As New QuoteDB
		Me.dgvSearchResults.DataSource = oQuoteDB.GetSearchResults(Me.txtQuoteNumberLookup.Text, Me.txtRFQLookup.Text, Me.txtPartNumberLookup.Text)
		Me.dgvSearchResults.Select()
		If Me.dgvSearchResults.Rows.Count > 0 Then
			Me.dgvSearchResults.Columns(0).Visible = False
			Me.dgvSearchResults.Rows(0).Selected = True
			ShowTab(Me.tpgShow)
			Me.lblSelectAndShow.Visible = True
		Else
			Me.tabQuote.TabPages.Remove(Me.tpgShow)
			Me.lblSelectAndShow.Visible = False
		End If
	End Sub
	Private Sub ShowTab(ByVal tab As System.Windows.Forms.TabPage)
		If Not Me.tabQuote.TabPages.Contains(tab) Then
			Me.tabQuote.TabPages.Add(tab)
		End If

	End Sub
	Private Sub dgvSearchResults_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSearchResults.SelectionChanged
		If dgvSearchResults.SelectedRows.Count > 0 Then
			OpenQuote(dgvSearchResults.SelectedRows(0).Cells(0).Value)
		End If
	End Sub
	Private Sub OpenQuote(ByVal gQuoteID As Guid)
		Dim oQuoteDB As New QuoteDB
		Dim drHeader As dsQuote.QuoteHeaderRow
		drHeader = oQuoteDB.GetHeaderRow(gQuoteID)
		Dim txt As Control
		For Each txt In Me.tpgShow.Controls
			If txt.Name.Substring(0, 3) = "txt" Then
				If drHeader.Table.Columns.Contains(txt.Name.Substring(3)) Then
					txt.Text = drHeader(txt.Name.Substring(3)).ToString
				End If
			End If
		Next
		Me.txtComputedPrice.Text = oQuoteDB.GetUnitPrice(gQuoteID)
	End Sub
	Private Sub tpgShow_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpgShow.Enter
		'	OpenQuote(dgvSearchResults.SelectedRows(0).Cells(0).Value)
	End Sub
	Private Sub mniImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniImport.Click
		Dim xl As New DCS.LoadXL
		Dim gQuoteID As Guid
		gQuoteID = xl.Connect2XL()
		If Not gQuoteID.Equals(Guid.Empty) Then
			OpenQuote(gQuoteID)
			Me.tabQuote.Visible = True
			ShowTab(Me.tpgShow)
			Me.tabQuote.SelectTab(Me.tpgShow)
		End If
	End Sub
	Private Sub mniOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mniOpen.Click
		Me.tabQuote.Visible = True
	End Sub
End Class