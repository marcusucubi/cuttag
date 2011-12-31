Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Common

    Public MustInherit Class WireProperties
        Inherits SaveableProperties
        Protected _CopperWeightPer1000Ft As Decimal
        Protected _TotalWeight As Decimal
        Protected _LengthFeet As Decimal
    End Class

End Namespace

