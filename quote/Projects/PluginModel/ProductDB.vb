Public NotInheritable Class ProductDB
    
    Private Sub New()
        
    End Sub
    
    Public Shared Function Load( _
            ByVal code As String, _
            ByVal unitCost As Decimal, _
            ByVal gage As String, _
            ByVal isWire As Boolean, _
            ByVal wireRow As DB.QuoteDataBase.WireSourceRow, _
            ByVal partRow As DB.QuoteDataBase.WireComponentSourceRow, _
            ByVal unitOfMeasure As String, _
            ByVal copperWeightPer1000Feet As Decimal
            ) As ProductBuildData
        
        Dim result as new ProductBuildData
        
        result.Code = code
        result.UnitCost = unitCost
        result.Gage = gage
        result.IsWire = isWire
        result.UnitOfMeasure = unitOfMeasure
        
        If WireRow IsNot Nothing Then
            result.CopperWeightPer1000Feet = copperWeightPer1000Feet
        End If
        
        If PartRow IsNot Nothing Then
            
            result.Description = PartRow.Description
            
            If Not PartRow.IsLeadTimeNull Then
                result.LeadTime = PartRow.LeadTime
            End If
            
            If Not PartRow.IsVendorNull Then
                result.Vendor = PartRow.Vendor
            End If
            
            If Not PartRow.IsMachineTimeNull Then
                result.MachineTime = PartRow.MachineTime
            End If
            
            If Not PartRow.IsMinimumQtyNull Then
                result.MinimumQty = PartRow.MinimumQty
            End If
            
            If Not PartRow.IsMinimumDollarNull Then
                result.MinimumDollar = PartRow.MinimumDollar
            End If
            
        End If
        
        Return result
    End Function
    
End Class
