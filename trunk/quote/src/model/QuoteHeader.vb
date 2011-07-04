﻿Imports System.ComponentModel
Imports System.Reflection

Namespace Model

    ''' <summary>
    ''' Represents the quote header
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Class QuoteHeader
        Implements INotifyPropertyChanged

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#Region "Variables"
        Private WithEvents _QuoteDetails As New QuoteDetailCollection
#End Region

#Region "Properties"

        Public Property ComputationProperties As New ComputationProperties(Me)
        Public Property NonComputationProperties As New OtherProperties(Me)
        Public Property GageProperties As New Weights(Me)

        Public ReadOnly Property QuoteDetails As QuoteDetailCollection
            Get
                Return _QuoteDetails
            End Get
        End Property

#End Region

#Region "Methods"

        Public Function NewQuoteDetail(ByVal product As Product) As QuoteDetail

            Dim oo As QuoteDetail = FactoryMethod(product)

            AddHandler oo.PropertyChanged, AddressOf ForwardEvent
            Me.QuoteDetails.Add(oo)
            SendEvents()

            Return oo
        End Function

        Public Sub Remove(ByVal detail As QuoteDetail)
            If detail IsNot Nothing Then
                Me.QuoteDetails.Remove(detail)

                RemoveHandler detail.PropertyChanged, AddressOf ForwardEvent
                SendEvents()
            End If
        End Sub

        Protected Overridable Function FactoryMethod(ByVal product As Product) As QuoteDetail
            Return New QuoteDetail(Me, product)
        End Function

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteHeader).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
            Me.ComputationProperties.SendEvents()
        End Sub

        Private Sub ForwardEvent(ByVal sender, ByVal e)
            RaiseEvent PropertyChanged(sender, e)
        End Sub

        Private Sub _col_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _QuoteDetails.ListChanged
            SendEvents()
        End Sub

#End Region

    End Class
End Namespace