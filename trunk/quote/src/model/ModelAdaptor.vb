Imports System.ComponentModel

Namespace Model

    Public Class EditableQuoteHeader
        Inherits QuoteHeader
        Implements IEditableObject, INotifyPropertyChanged

        Public Function NewEditableQuoteDetail() As EditableQuoteDetail

            Dim oo As New EditableQuoteDetail(Me)

            RaiseEvent PropertyChanged(oo, New PropertyChangedEventArgs("Qty"))

            AddHandler oo.PropertyChanged,
                Sub(sender, e)
                    RaiseEvent PropertyChanged(sender, e)
                End Sub
            MyBase._col.Add(oo)

            Return oo
        End Function

        Public Sub BeginEdit() Implements System.ComponentModel.IEditableObject.BeginEdit
        End Sub

        Public Sub CancelEdit() Implements System.ComponentModel.IEditableObject.CancelEdit
        End Sub

        Public Sub EndEdit() Implements System.ComponentModel.IEditableObject.EndEdit
        End Sub

        Public Shadows Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    End Class


    Public Class EditableQuoteDetail
        Inherits QuoteDetail
        Implements IEditableObject, INotifyPropertyChanged

        Private m_backupData As QuoteDetail
        Private m_inTx As Boolean

        Public Sub New(ByVal QuoteHeader As QuoteHeader)
            MyBase.New(QuoteHeader)
            m_backupData = New QuoteDetail
        End Sub

        Public Sub BeginEdit() Implements System.ComponentModel.IEditableObject.BeginEdit
            If Not m_inTx Then
                m_backupData.Qty = Me.Qty
                m_inTx = True
            End If
        End Sub

        Public Sub CancelEdit() Implements System.ComponentModel.IEditableObject.CancelEdit
            If m_inTx Then
                Me.Qty = m_backupData.Qty
                m_inTx = False
            End If
        End Sub

        Public Sub EndEdit() Implements System.ComponentModel.IEditableObject.EndEdit
            If m_inTx Then
                m_inTx = False
            End If
        End Sub

    End Class
End Namespace

