
Namespace Model.BOM

    Public Class Customer

        Private _Name As String
        Private _ID As Integer

        Public ReadOnly Property Name As String
            Get
                Return _Name
            End Get
        End Property

        Public Sub SetName(name As String)
            _Name = name
        End Sub

        Public ReadOnly Property ID As String
            Get
                Return _ID
            End Get
        End Property

        Public Sub SetID(id As Integer)
            _ID = id
        End Sub

        Public Overrides Function GetHashCode() As Integer
            Return _ID.GetHashCode()
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim other As Customer = obj
            Return (_ID = other._ID)
        End Function

        Public Overrides Function ToString() As String
            If _ID = 0 Then
                Return _Name
            End If
            Return _ID & "@" & _Name
        End Function

        Public Shared Function GetByName(name As String) As Customer

            Dim customer As New Customer
            Dim adaptor As New QuoteDataBaseTableAdapters.CustomerTableAdapter
            Dim table As QuoteDataBase.CustomerDataTable
            table = adaptor.GetData()
            For Each row As QuoteDataBase.CustomerRow In table.Rows
                If row.CustomerName.ToLowerInvariant() = name Then
                    customer.SetName(row.CustomerName)
                    customer.SetID(row.CustomerID)
                    Return customer
                End If
            Next

            Return Nothing
        End Function

    End Class
End Namespace
