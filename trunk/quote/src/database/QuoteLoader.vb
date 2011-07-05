Imports System.Data.SqlClient
Imports DCS.Quote.Model
Imports DCS.Quote.QuoteDataBase
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports DCS.Quote.QuoteDataBaseTableAdapters

Public Class QuoteLoader

    Public Sub Save(ByVal q As Model.QuoteHeader)

        ' Ensure the properies are updated
        frmMain.frmMain.Focus()

        Dim id As Integer

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim o As PrimaryPropeties = q.PrimaryProperties
        If q.PrimaryProperties.QuoteNumnber > 0 Then
            adaptor.Update( _
                o.CustomerName, o.RequestForQuoteNumber, _
                o.PartNumber, o.QuoteNumnber)
            id = o.QuoteNumnber
        Else
            adaptor.Connection.Open()
            adaptor.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.CustomerName, o.PartNumber, o.RequestForQuoteNumber)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = adaptor.Transaction
            id = CInt(cmd.ExecuteScalar())
            adaptor.Transaction.Commit()
            adaptor.Connection.Close()
            o.SetID(id)
        End If

        adaptor.Connection.Open()
        Me.DeleteProperties(id)
        Me.SaveProperties(id, q.NonComputationProperties, Nothing)
        Me.SaveProperties(id, q.ComputationProperties, Nothing)
        Me.SaveComponents(q)
        adaptor.Connection.Close()

    End Sub

    Private Sub SaveComponents(ByVal q As QuoteHeader)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim id As Integer = q.PrimaryProperties.QuoteNumnber
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows
            adaptor.Delete(row.ID)
        Next
        For Each detail As QuoteDetail In q.QuoteDetails
            adaptor.Insert(id, detail.Qty, detail.ComponentTime, detail.ProductCode)
        Next
    End Sub

    Private Sub LoadComponents(ByVal q As QuoteHeader)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim partAdaptor As New _PartsTableAdapter
        Dim wireAdaptor As New _WiresTableAdapter
        Dim id As Integer = q.PrimaryProperties.QuoteNumnber
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows

            Dim detail As QuoteDetail = Nothing

            Dim parts As _PartsDataTable
            parts = partAdaptor.GetDataByProductCode(row.ProductCode)
            If (parts.Count > 0) Then
                Dim part As _PartsRow
                part = parts(0)
                Dim partObj As New Product( _
                    part.PartNumber, part.UnitCost, _
                    0, UnitOfMeasure.BY_EACH)

                detail = New QuoteDetail(q, partObj)
            End If

            Dim wires As _WiresDataTable
            wires = wireAdaptor.GetDataByProductCode(row.ProductCode)
            If (wires.Count > 0) Then
                Dim wire As _WiresRow
                wire = wires(0)
                Dim wireObj As New Product( _
                    wire.PartNumber, wire.Price, _
                    wire.Gage, UnitOfMeasure.BY_LENGTH)

                detail = New QuoteDetail(q, wireObj)
            End If

            If (detail IsNot Nothing) Then
                q.QuoteDetails.Add(detail)
            End If
        Next
    End Sub

    Public Function Load(ByVal id As Long) As Model.QuoteHeader

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table As New QuoteDataBase._QuoteDataTable
        Dim q As New Model.QuoteHeader()

        adaptor.FillByQuoteID(table, id)
        If table.Rows.Count > 0 Then
            Dim row As QuoteDataBase._QuoteRow = table.Rows(0)
            q = New Model.QuoteHeader(row.ID)
            q.PrimaryProperties.CustomerName = row.CustomerName
            q.PrimaryProperties.PartNumber = row.PartNumber
            q.PrimaryProperties.RequestForQuoteNumber = row.RequestForQuoteNumber

            LoadProperties(id, q.ComputationProperties)
            LoadProperties(id, q.NonComputationProperties)
        End If

        Me.LoadComponents(q)

        Return q
    End Function

    Private Sub SaveProperties(ByVal id As Integer, ByVal obj As Object, ByRef Transaction As OleDbTransaction)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            If Not p.CanWrite Then
                Continue For
            End If

            Dim s As String = Nothing
            Dim i As Integer = Nothing
            Dim d As Decimal = Nothing
            Dim o As Object = p.GetValue(obj, Nothing)

            If TypeOf o Is Integer Then
                i = CInt(o)
            End If
            If TypeOf o Is String Then
                s = CStr(o)
            End If
            If TypeOf o Is Decimal Then
                d = CDec(o)
            End If

            adaptor.Insert(id, p.Name, s, d, i)
        Next
    End Sub

    Private Sub LoadProperties(ByVal id As Integer, ByVal obj As Object)

        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter

        For Each p As PropertyInfo In props

            If Not p.CanWrite Then
                Continue For
            End If

            Dim table As _QuotePropertiesDataTable = adaptor.GetDataByIDAndName(id, p.Name)

            If table.Rows.Count > 0 Then
                Dim row As _QuotePropertiesRow = table.Rows(0)
                If row("PropertyStringValue") IsNot DBNull.Value Then
                    If p.PropertyType.Name = "String" Then
                        p.SetValue(obj, row.PropertyStringValue, Nothing)
                    End If
                End If
                If row("PropertyDecimalValue") IsNot DBNull.Value Then
                    If p.PropertyType.Name = "Decimal" Then
                        p.SetValue(obj, row.PropertyDecimalValue, Nothing)
                    End If
                End If
                If row("PropertyIntegerValue") IsNot DBNull.Value Then
                    If p.PropertyType.Name = "Int32" Then
                        p.SetValue(obj, row.PropertyIntegerValue, Nothing)
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub DeleteProperties(ByVal QuoteID As Integer)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter
        Dim table As _QuotePropertiesDataTable = adaptor.GetDataByQuoteID(QuoteID)
        For Each row As _QuotePropertiesRow In table.Rows
            adaptor.Delete(row.ID)
        Next
    End Sub

End Class
