Public Class Plugin
    Implements Host.IInit
    
    Public Sub Init() Implements Host.IInit.Init 
        
        Model.ShippingDB.InitializeShipping()
        
    End Sub
    
End Class
