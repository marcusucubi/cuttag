﻿Imports System.Data.SqlClient

Imports DB.QuoteDataBaseTableAdapters

Public Class DetailTableProxy

    Private ReadOnly m_Adapter As _QuoteDetailTableAdapter

    Public Sub New(adapter As _QuoteDetailTableAdapter)
        m_Adapter = adapter
    End Sub

    Public Property Transaction As SqlTransaction
        Get
            Return m_Adapter.Transaction
        End Get
        Set(value As SqlTransaction)
            m_Adapter.Transaction = value
        End Set
    End Property

End Class
