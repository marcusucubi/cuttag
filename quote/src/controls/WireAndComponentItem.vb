Class WireAndComponentItem
    Inherits ListViewItem

    Private WithEvents _Detail As Common.Detail
    Private _ItemType As New ListViewItem.ListViewSubItem
    Private _ItemQuantity As New ListViewItem.ListViewSubItem
    Private _ItemUnitCost As New ListViewItem.ListViewSubItem
    Private _ItemTotalCost As New ListViewItem.ListViewSubItem

    Public Sub New(ByVal o As Common.Detail)
        Me._Detail = o
        Init(o)
    End Sub

    Private Sub _Detail_SavableChange(ByVal subject As Common.SaveableProperties) Handles _Detail.SavableChange
        Init(_Detail)
    End Sub

    Private Sub Init(ByVal o As Common.Detail)

        Dim i As New ListViewItem
        i = Me
        i.Name = o.ProductCode
        i.Text = o.ProductCode
        i.Tag = o

        _ItemType.Text = o.DisplayableProductClass
        _ItemType.Name = "Type"
        i.SubItems.Add(_ItemType)

        _ItemQuantity.Text = Math.Round(o.Qty)
        _ItemQuantity.Name = "Quantity"
        i.SubItems.Add(_ItemQuantity)

        _ItemUnitCost.Text = Math.Round(o.UnitCost, 6)
        _ItemUnitCost.Name = "UnitCost"
        i.SubItems.Add(_ItemUnitCost)

        _ItemTotalCost.Text = Math.Round(o.TotalCost, 4)
        _ItemTotalCost.Name = "TotalCost"
        i.SubItems.Add(_ItemTotalCost)

    End Sub

End Class

