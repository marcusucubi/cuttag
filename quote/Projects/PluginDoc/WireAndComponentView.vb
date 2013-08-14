Imports DB.QuoteDataBase
Imports Model
Imports Model.IO.Misc

Public Class WireAndComponentView
    Private WithEvents _DetailCollection As Model.Common.DetailCollection(Of Model.Common.Detail)
    Private _Sorter As New ListViewColumnSorter
    Private _PartNumberSave As String
    Private _PartLookupDataSource As DB.QuoteDataBase
    Private _PartLookupDataMember As String
    Public Property DetailCollection As Model.Common.DetailCollection(Of Model.Common.Detail)
        Get
            Return _DetailCollection
        End Get
        
        Set(ByVal value As Model.Common.DetailCollection(Of Model.Common.Detail))
            
            _DetailCollection = value
            
            If value IsNot Nothing Then
                
                Dim list As New DetailBindingList(value)
                Me.dgvQuoteDetail.DataSource = list
                
                With dgvQuoteDetail_Lookup
                    .SearchGrid.DataSource = _PartLookupDataSource
                    .SearchGrid.DataMember = _PartLookupDataMember
                End With
                
            End If
        End Set
    End Property
    
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
    
    Public Sub New()
        InitializeComponent()
        InitComponent()
    End Sub
    Private Sub InitComponent()
        Me.dgvQuoteDetail.AutoGenerateColumns = False
        dgvQuoteDetail_Lookup.SearchGrid = New WireAndComponentViewSearchGrid
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
    Private Sub dgvQuoteDetail_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvQuoteDetail.GotFocus
        SelectDetail()
    End Sub
    Private Sub ListView1_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.ItemActivate
        SelectDetail()
    End Sub
    Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        SelectDetail()
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
        If dgvQuoteDetail.RowCount > 0 AndAlso Not dgvQuoteDetail.CurrentRow Is Nothing Then
            Dim i As Model.Common.Detail
            i = dgvQuoteDetail.CurrentRow.DataBoundItem
            Model.ActiveDetail.Instance.Detail = i
        Else
            Model.ActiveDetail.Instance.Detail = Nothing
        End If

    End Sub
    Private Sub Sync()
        If Me._DetailCollection Is Nothing Then
            Return
        End If
    End Sub
    Private Sub dgvQuoteDetail_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvQuoteDetail.CellBeginEdit
        Debug.WriteLine("Cell Begin Edit")
        If e.ColumnIndex = dgvQuoteDetail_Lookup.Index Then
            _PartNumberSave = dgvQuoteDetail.CurrentCell.Value
        End If
    End Sub
    Private Sub dgvQuoteDetail_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvQuoteDetail.CellEndEdit
       Debug.WriteLine("Cell End Edit-Lookup col indext" + dgvQuoteDetail_Lookup.Index.ToString)
       If e.ColumnIndex = dgvQuoteDetail_Lookup.Index Then
            If Not _PartNumberSave = dgvQuoteDetail.CurrentCell.Value Then
                Dim drLookup As DB.QuoteDataBase.ItemSourceLookupListRow = CType(dgvQuoteDetail_Lookup.SearchGrid.GetCurrentRow, DB.QuoteDataBase.ItemSourceLookupListRow)
                Dim sPartNumber As String = drLookup.PartNumber
                Dim gSourceID As Guid = drLookup.SourceID
                Dim oDetail As Model.Template.Detail = CType(dgvQuoteDetail.CurrentRow.DataBoundItem, Model.Template.Detail)
                oDetail.SourceID = drLookup.SourceID
                
                Dim pProduct As Model.Product = _
                    ProductDB.Load(gSourceID, drLookup.IsWire, _PartLookupDataSource)
                
                oDetail.UpdateComponentProperties(pProduct)
                oDetail.IsDirty = True
                
            ElseIf _PartNumberSave = "" Then 'must be new row with no search item chosen 
            End If
        End If
    End Sub
    Private Sub dgvQuoteDetail_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvQuoteDetail.EditingControlShowing
        Debug.WriteLine("Edit Control Showing")
    End Sub
    Private Sub dgvQuoteDetail_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvQuoteDetail.SelectionChanged
        Debug.WriteLine("dgvTest_SelectionChanged RowCount = : " + dgvQuoteDetail.RowCount.ToString)
        If CType(sender, LookupDataGridView).Focused Then
            SelectDetail()
        End If
    End Sub
    Private Sub dgvQuoteDetail_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvQuoteDetail.UserDeletedRow
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
