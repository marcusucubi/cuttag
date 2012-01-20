
Namespace Model.BOM

    ''' <summary>
    ''' Represents the customer
    ''' </summary>
    ''' <remarks></remarks>
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
            Return _ID & " " & _Name
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

        Public Shared Function CreateFromString(value As String) As Customer

            Dim index As Integer
            index = value.IndexOf(" ")

            If index = -1 Then
                Dim c As New Customer
                c.SetName(value)
                Return c
            End If

            Dim left As String = value.Substring(0, index)
            Dim right As String = value.Substring(index)

            Dim customer As New Model.BOM.Customer
            If left.Length > 0 Then
                Dim id As Integer
                Integer.TryParse(left, id)
                Dim name As String = right

                customer.SetID(id)
                customer.SetName(name)
            Else
                customer.SetName(value.ToString())
            End If

            Return customer

        End Function

    End Class
End Namespace
