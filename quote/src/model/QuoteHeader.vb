Imports System.ComponentModel
Imports System.Reflection

Namespace Model

    ''' <summary>
    ''' Represents the quote header
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Class QuoteHeader
        Inherits SaveableProperties
        Implements INotifyPropertyChanged
        Implements IEditableObject

        Private _IsQuote As Boolean

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Public Sub New()
            Me.New(0, False)
        End Sub

        Public Sub New(ByVal id As Long, ByVal IsQuote As Boolean)
            Me.PrimaryProperties = New PrimaryPropeties(Me, id)
            Me._IsQuote = IsQuote

            MyBase.AddDependent(ComputationProperties)
            MyBase.AddDependent(NonComputationProperties)
            MyBase.AddDependent(PrimaryProperties)
        End Sub

#Region "Variables"
        Private WithEvents _QuoteDetails As New QuoteDetailCollection
#End Region

#Region "Properties"

        Public Property ComputationProperties As new ComputationProperties(Me)
        Public Property NonComputationProperties As New OtherProperties(Me)
        Public Property WeightProperties As New Weights(Me)
        Public Property PrimaryProperties As PrimaryPropeties

        Public ReadOnly Property QuoteDetails As QuoteDetailCollection
            Get
                Return _QuoteDetails
            End Get
        End Property

        Public ReadOnly Property IsQuote As Boolean
            Get
                Return _IsQuote
            End Get
        End Property

#End Region

#Region "Methods"

        Public Function NewQuoteDetail(ByVal product As Product) As QuoteDetail

            Dim oo As QuoteDetail = New QuoteDetail(Me, product)

            AddHandler oo.PropertyChanged, AddressOf ForwardEvent
            Me.QuoteDetails.Add(oo)
            MyBase.AddDependent(oo)
            SendEvents()

            Return oo
        End Function

        Public Sub Remove(ByVal detail As QuoteDetail)
            If detail IsNot Nothing Then
                Me.QuoteDetails.Remove(detail)

                RemoveHandler detail.PropertyChanged, AddressOf ForwardEvent
                MyBase.RemoveDependent(detail)
                SendEvents()
            End If
        End Sub

        Private Sub ForwardEvent(ByVal sender, ByVal e)
            RaiseEvent PropertyChanged(sender, e)
        End Sub

        Private Sub _col_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _QuoteDetails.ListChanged
            SendEvents()
        End Sub

        Public Sub BeginEdit() Implements System.ComponentModel.IEditableObject.BeginEdit
        End Sub

        Public Sub CancelEdit() Implements System.ComponentModel.IEditableObject.CancelEdit
        End Sub

        Public Sub EndEdit() Implements System.ComponentModel.IEditableObject.EndEdit
        End Sub

        Private Sub SendEvents()
            Dim info() As PropertyInfo
            info = GetType(QuoteHeader).GetProperties()
            For Each i As PropertyInfo In info
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(i.Name))
            Next
            Me.ComputationProperties.SendEvents()
            MyBase.MakeDirty()
        End Sub

#End Region


    End Class
End Namespace
