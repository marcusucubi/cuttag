﻿Imports System.ComponentModel
Imports DCS.Quote.Model
Imports System.Reflection

Namespace Model
    Public Class GageProperties
        Implements INotifyPropertyChanged

        Class Weight
            Sub New(ByVal Gage As String, ByVal Value As Decimal)
                Me.Gage = Gage
                Me.Value = Value
            End Sub
            Property Gage As String
            Property Value As Decimal
        End Class

        Private Weights() = New Weight() { _
            New Weight("18", 0.00159),
            New Weight("16", 0.00242),
            New Weight("14", 0.00387),
            New Weight("12", 0.00602),
            New Weight("10", 0.00959),
            New Weight("8", 0.01541),
            New Weight("6", 0.02657),
            New Weight("4", 0.04206),
            New Weight("2", 0.39),
            New Weight("1", 0.198),
            New Weight("1/0", 0.244),
            New Weight("2/0", 0.311),
            New Weight("3/0", 0.492),
            New Weight("4/0", 0.673),
            New Weight("373 MCM", 1.176),
            New Weight("444 MCM", 1.413),
            New Weight("777 MCM", 2.435)
        }

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            _QuoteHeader = QuoteHeader
        End Sub

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private WithEvents _QuoteHeader As QuoteHeader

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage18 As Decimal
            Get
                Dim gage As Integer = CalcQty("18")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage16 As Decimal
            Get
                Dim gage As Integer = CalcQty("16")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage14 As Decimal
            Get
                Dim gage As Integer = CalcQty("14")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage12 As Decimal
            Get
                Dim gage As Integer = CalcQty("12")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage10 As Decimal
            Get
                Dim gage As Integer = CalcQty("10")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage08 As Decimal
            Get
                Dim gage As Integer = CalcQty("8")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage06 As Decimal
            Get
                Dim gage As Integer = CalcQty("6")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage04 As Decimal
            Get
                Dim gage As Integer = CalcQty("4")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage02 As Decimal
            Get
                Dim gage As Integer = CalcQty("2")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Gage"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage01 As Decimal
            Get
                Dim gage As Integer = CalcQty("1")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Big"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage00_1 As Decimal
            Get
                Dim gage As Integer = CalcQty("1/0")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Big"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage00_2 As Decimal
            Get
                Dim gage As Integer = CalcQty("2/0")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Big"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage00_3 As Decimal
            Get
                Dim gage As Integer = CalcQty("3/0")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Big"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage00_4 As Decimal
            Get
                Dim gage As Integer = CalcQty("4/0")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Large"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage373MCM As Decimal
            Get
                Dim gage As Integer = CalcQty("373 MCM")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Large"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage444MCM As Decimal
            Get
                Dim gage As Integer = CalcQty("444 MCM")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Large"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property Gage777MCM As Decimal
            Get
                Dim gage As Integer = CalcQty("777 MCM")
                Return gage
            End Get
        End Property

        <CategoryAttribute("Total"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property TotalLength As Decimal
            Get
                Return CalcQty(Nothing)
            End Get
        End Property

        <CategoryAttribute("Total"), _
        DescriptionAttribute("")> _
        Public ReadOnly Property TotalWeight As Decimal
            Get
                Return CalcWeight()
            End Get
        End Property

        Private Function CalcQty(ByVal gage As String) As Integer
            Dim qty As Integer
            For Each q As QuoteDetail In _QuoteHeader.QuoteDetails
                If q.Gage = gage Then
                    qty += q.Qty
                ElseIf q.Gage.Contains("-" & gage) Then
                    qty += q.Qty
                ElseIf gage Is Nothing Then
                    qty += q.Qty
                End If
            Next
            Return qty
        End Function

        Private Function CalcWeight() As Decimal
            Dim r As Decimal
            For Each w As Weight In Me.Weights
                Dim x As Decimal
                x += FindWeight(w.Gage) + CalcQty(w.Gage)
            Next
            Return r
        End Function

        Private Function FindWeight(ByVal gage As String) As Integer
            Dim r As Integer
            For Each w As Weight In Me.Weights
                If w.Gage = gage Then
                    r = w.Value
                ElseIf w.Gage.Contains("-" & gage) Then
                    r = w.Value
                ElseIf gage Is Nothing Then
                    r = w.Value
                End If
            Next
            Return r
        End Function

        Private Sub _QuoteHeader_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _QuoteHeader.PropertyChanged
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(""))
        End Sub

    End Class
End Namespace