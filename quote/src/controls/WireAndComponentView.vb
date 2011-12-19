Imports DCS.Quote.QuoteDataBase

Public Class WireAndComponentView
	Private WithEvents _DetailCollection As Common.DetailCollection(Of Common.Detail)
	Private _Sorter As New ListViewColumnSorter
	'dd_Added 10/4/11
	Private _PartNumberSave As String
	Private _PartLookupDataSource As QuoteDataBase
	Private _PartLookupDataMember As String
	'dd_Added end

	Public Property DetailCollection As Common.DetailCollection(Of Common.Detail)
		Get
			Return _DetailCollection
		End Get
		Set(ByVal value As Common.DetailCollection(Of Common.Detail))
			_DetailCollection = value


            Me.dgvQuoteDetail.DataSource = Me._DetailCollection
            With dgvQuoteDetail_Lookup
                .SearchGrid.DataSource = _PartLookupDataSource
                .SearchGrid.DataMember = _PartLookupDataMember
            End With


            'Sync()
		End Set
	End Property

	'dd_Added 10/4/11
	Public Property PartLookupDataSource As DataSet
		Get
			Return _PartLookupDataSource
		End Get
		Set(ByVal value As DataSet)
			_PartLookupDataSource = value
		End Set
	End Property
	Public Property PartLookupDataMember As String
		Get
			Return _PartLookupDataMember
		End Get
		Set(ByVal value As String)
			_PartLookupDataMember = value
		End Set
	End Property
	'dd_Added end

	Public Sub New()
		InitializeComponent()
		InitComponent()
	End Sub
	Private Sub InitComponent()
		'dd_Added 9/29/2011
		Me.dgvQuoteDetail.AutoGenerateColumns = False
        dgvQuoteDetail_Lookup.SearchGrid = New WireAndComponentViewSearchGrid
		'dd_Added end
	End Sub

	Private Sub ListView1_ColumnClick(ByVal sender As Object, _
	 ByVal e As System.Windows.Forms.ColumnClickEventArgs) _
	 Handles ListView1.ColumnClick
		Dim c As ColumnHeader = Me.ListView1.Columns(e.Column)

		If (e.Column = _Sorter.SortColumn) Then
			If (_Sorter.Order = SortOrder.Ascending) Then
				_Sorter.Order = SortOrder.Descending
			Else
				_Sorter.Order = SortOrder.Ascending
			End If
		Else
			_Sorter.SortColumn = e.Column
			_Sorter.Order = SortOrder.Ascending
		End If
		ListView1.ListViewItemSorter = _Sorter
		ListView1.Sort()

	End Sub

	Private Sub ListView1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.GotFocus
        '		SelectDetail()
	End Sub
    Private Sub dgvQuoteDetail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvQuoteDetail.GotFocus
        SelectDetail()

    End Sub
    Private Sub ListView1_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.ItemActivate
        SelectDetail()
    End Sub
	Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
		SelectDetail()
	End Sub
	Private Sub _DetailCollection_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _DetailCollection.ListChanged
        '		Sync()
    End Sub

    Private Sub WireAndComponentView_Resize(ByVal sender As Object, _
   ByVal e As System.EventArgs) _
   Handles Me.Resize
        Dim size As Integer
        For Each col As ColumnHeader In Me.ListView1.Columns
            If col.DisplayIndex > 0 Then
                size = size + col.Width
            End If
        Next
        Dim left As ColumnHeader = Me.ListView1.Columns(0)
        left.Width = Me.ListView1.Width - (size + 25)
    End Sub

    Public Sub SelectDetail()
        'dd_Added 9/26/2011
        If dgvQuoteDetail.RowCount > 0 AndAlso Not dgvQuoteDetail.CurrentRow Is Nothing Then
            Dim i As Common.Detail
            i = dgvQuoteDetail.CurrentRow.DataBoundItem
            ActiveDetail.ActiveDetail.Detail = i
        Else
            ActiveDetail.ActiveDetail.Detail = Nothing
        End If

        'ddRemmed
        'If ListView1.SelectedItems.Count > 0 Then
        '	ActiveDetail.ActiveDetail.Detail = ListView1.Tag
        'Else
        '	ActiveDetail.ActiveDetail.Detail = Nothing
        'End If

        'dd_Added end

    End Sub
	Private Sub Sync()
		If Me._DetailCollection Is Nothing Then
			Return
		End If
		'Dim addList As New List(Of Common.Detail)
		'Dim removeList As New List(Of WireAndComponentItem)
		'For Each o As Common.Detail In Me._DetailCollection
		'	Dim test As Common.Detail = Nothing
		'	For Each item As ListViewItem In Me.ListView1.Items
		'		If item.Tag Is o Then
		'			test = item.Tag
		'		End If
		'	Next
		'	If test Is Nothing Then
		'		addList.Add(o)
		'	End If
		'Next
		'For Each o As Common.Detail In addList
		'	Dim i As New WireAndComponentItem(o)
		'	ListView1.BeginUpdate()
		'	ListView1.Items.Add(i)
		'	ListView1.SelectedItems.Clear()
		'	ListView1.SelectedIndices.Add(ListView1.Items.IndexOf(i))
		'	ListView1.EndUpdate()
		'Next
		'For Each item As ListViewItem In Me.ListView1.Items
		'	Dim test As Common.Detail = Nothing
		'	For Each o As Common.Detail In Me._DetailCollection
		'		If item.Tag Is o Then
		'			test = item.Tag
		'		End If
		'	Next
		'	If test Is Nothing Then
		'		removeList.Add(item)
		'	End If
		'Next
		'For Each o As WireAndComponentItem In removeList
		'	ListView1.BeginUpdate()
		'	ListView1.Items.Remove(o)
		'	ListView1.EndUpdate()
		'Next
		'ListView1.Refresh()

		'dd_Added 9/1/11
        ''dd_Added end

	End Sub
	Private Sub dgvQuoteDetail_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvQuoteDetail.CellBeginEdit
		Debug.WriteLine("Cell Begin Edit")
		'dd_Added 10/4/11
		If e.ColumnIndex = dgvQuoteDetail_Lookup.Index Then
			_PartNumberSave = dgvQuoteDetail.CurrentCell.Value
		End If
	End Sub

	Private Sub dgvQuoteDetail_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvQuoteDetail.CellEndEdit
		Debug.WriteLine("Cell End Edit-Lookup col indext" + dgvQuoteDetail_Lookup.Index.ToString)
		'dd_Added 10/4/11
		If e.ColumnIndex = dgvQuoteDetail_Lookup.Index Then
			If Not _PartNumberSave = dgvQuoteDetail.CurrentCell.Value Then
				'Dim sPartNumber As String = dgvQuoteDetail.CurrentCell.Value
				Dim drLookup As QuoteDataBase.ItemSourceLookupListRow = CType(dgvQuoteDetail_Lookup.SearchGrid.GetCurrentRow, DCS.Quote.QuoteDataBase.ItemSourceLookupListRow)
				Dim sPartNumber As String = drLookup.PartNumber
				Dim gSourceID As Guid = drLookup.SourceID
				Dim oDetail As DCS.Quote.Model.BOM.Detail = CType(dgvQuoteDetail.CurrentRow.DataBoundItem, DCS.Quote.Model.BOM.Detail)
				oDetail.IsWire = drLookup.IsWire
				oDetail.SourceID = drLookup.SourceID
				Dim dCost As Decimal
				Dim pProduct As Model.Product
                Dim sUOM As String = ""
                Dim drUOM As QuoteDataBase._UnitOfMeasureRow
                If drLookup.IsWire Then
                    Dim drSource As QuoteDataBase.WireSourceRow = _PartLookupDataSource.WireSource.FindByWireSourceID(gSourceID)
                    Dim sGage As String = ""
                    Dim drGage As QuoteDataBase.GageRow
                    drGage = _PartLookupDataSource.Gage.FindByOrganizationIDGageID(10, drSource.GageID) 'ddFix organizationid
                    If drGage IsNot Nothing Then sGage = drGage.Gage
                    dCost = 0
                    If Not drSource.IsQuotePriceNull Then
                        dCost = drSource.QuotePrice
                    End If
                    sUOM = "Decimeter" 'ddddd implement and handle wiresource.unitofmeasureid
                    pProduct = New DCS.Quote.Model.Product( _
                    sPartNumber, dCost, sGage, True, _
                    drSource, Nothing, sUOM)
                Else
                    Dim drSource As QuoteDataBase.WireComponentSourceRow = _PartLookupDataSource.WireComponentSource.FindByWireComponentSourceID(gSourceID)
                    drUOM = _PartLookupDataSource._UnitOfMeasure.FindByID(drSource.UnitOfMeasureID)
                    If drUOM IsNot Nothing Then sUOM = drUOM.Name
                    dCost = drSource.QuotePrice
                    pProduct = New DCS.Quote.Model.Product( _
                     sPartNumber, dCost, "", False, _
                     Nothing, drSource, sUOM)
                End If
				With oDetail
					.UpdateComponentProperties(pProduct)
					.MakeDirty()
					.SendEvents()
				End With
			ElseIf _PartNumberSave = "" Then 'must be new row with no search item chosen 
				'	dgvQuoteDetail.CurrentCell = Nothing
                'Dim oDetail As DCS.Quote.Model.BOM.Detail = CType(dgvQuoteDetail.CurrentRow.DataBoundItem, DCS.Quote.Model.BOM.Detail)
                'dgvQuoteDetail.CurrentCell = Nothing
                'oDetail.Header.Details.Remove(oDetail)
			End If
		End If
	End Sub

	Private Sub dgvQuoteDetail_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvQuoteDetail.EditingControlShowing
		Debug.WriteLine("Edit Control Showing")
	End Sub
    'Private Sub dgvQuoteDetail_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvQuoteDetail.RowEnter
    '       Debug.WriteLine("dgvTest_RowEnter")
    '       'With dgvQuoteDetail
    '       '    If Not IsNothing(.CurrentCell) AndAlso .Rows(e.RowIndex).IsNewRow Then
    '       '        '  .CurrentCell = dgvQuoteDetail(.Columns("dgvQuoteDetail_Lookup").Index, e.RowIndex)
    '       '        '        .BeginEdit(False)
    '       '        '    End If
    '       '        'If Not IsNothing(.CurrentRow) AndAlso .CurrentRow.IsNewRow Then
    '       '        '    .CurrentCell = dgvQuoteDetail(.Columns("dgvQuoteDetail_Lookup").Index, .RowCount - 1)
    '       '        '    .BeginEdit(False)
    '       '    End If
    '       'End With
    '   End Sub
    Private Sub dgvQuoteDetail_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvQuoteDetail.SelectionChanged
        Debug.WriteLine("dgvTest_SelectionChanged RowCount = : " + dgvQuoteDetail.RowCount.ToString)
        If CType(sender, LookupDataGridView).Focused Then
            '            Me.Sync()
            SelectDetail()
        End If

    End Sub
    Private Sub dgvQuoteDetail_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvQuoteDetail.UserDeletedRow
        '       Sync()
        SelectDetail()
    End Sub
    Private Sub dgvQuoteDetail_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvQuoteDetail.UserDeletingRow
        dgvQuoteDetail.CurrentCell = Nothing
    End Sub
    Private Sub dgvQuoteDetail_DataError(ByVal sender As System.Object, _
                                         ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) _
                                     Handles dgvQuoteDetail.DataError
        MsgBox(e.Exception.Message)
    End Sub
    Private Sub dgvQuoteDetail_NewRowEnteredByUser(ByVal iRowIndex As Integer) Handles dgvQuoteDetail.NewRowEnteredByUser
        Debug.WriteLine("--------------------------" + iRowIndex.ToString + "--------------------------")
        With dgvQuoteDetail
            .CurrentCell = dgvQuoteDetail(.Columns("dgvQuoteDetail_Lookup").Index, iRowIndex)
            .BeginEdit(False)
        End With
    End Sub

    Private Sub dgvQuoteDetail_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvQuoteDetail.CurrentCellChanged
        Dim s As String = ""
        If Me.dgvQuoteDetail.CurrentCell IsNot Nothing Then s = Me.dgvQuoteDetail.CurrentCell.RowIndex.ToString + " - " + Me.dgvQuoteDetail.CurrentCell.ColumnIndex.ToString
        Debug.WriteLine("dgv CurrentCellChanged " + s + " <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<")
    End Sub

    
End Class
