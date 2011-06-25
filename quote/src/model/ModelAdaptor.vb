Imports System.ComponentModel

Namespace Model

    ''' <summary>
    ''' Allows display in a DataGridView
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditableQuoteHeader
        Inherits QuoteHeader
        Implements IEditableObject

        Protected Overrides Function FactoryMethod() As QuoteDetail
            Return New EditableQuoteDetail(Me)
        End Function

        Public Sub BeginEdit() Implements System.ComponentModel.IEditableObject.BeginEdit
        End Sub

        Public Sub CancelEdit() Implements System.ComponentModel.IEditableObject.CancelEdit
        End Sub

        Public Sub EndEdit() Implements System.ComponentModel.IEditableObject.EndEdit
        End Sub

    End Class

    ''' <summary>
    ''' Allows display in a DataGridView
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EditableQuoteDetail
        Inherits QuoteDetail
        Implements IEditableObject

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

