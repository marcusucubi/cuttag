Imports System.Collections.ObjectModel

''' <summary>
''' Loads similar quotes or BOMs
''' </summary>
''' <remarks></remarks>
Public Class SimilarQuoteLoader

    Public Shared Function Load(targetId As Integer) As SimilarQuotes

        Dim quoteMap As Dictionary(Of Int32, QuoteDataBase._QuoteRow) = LoadQuoteMap()

        Dim adaptor1 As New QuoteDataBaseTableAdapters._QuoteDetailTableAdapter
        Dim table1 As New QuoteDataBase._QuoteDetailDataTable
        Dim row1 As QuoteDataBase._QuoteDetailRow
        adaptor1.FillByQuoteID(table1, targetId)
        Dim target As SimilarQuote = Nothing
        For Each row1 In table1.Rows
            If target Is Nothing Then
                target = New SimilarQuote(row1, quoteMap(row1.QuoteID), Nothing)
            Else
                target.AddPartOrWire(row1, target)
            End If
        Next

        Dim result = New SimilarQuotes(target)

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteDetailTableAdapter
        Dim table As New QuoteDataBase._QuoteDetailDataTable
        Dim row As QuoteDataBase._QuoteDetailRow

        adaptor.Fill(table)
        'adaptor.FillByQuoteID(table, 10843)

        For Each row In table.Rows

            If targetId = row.QuoteID Then
                Continue For
            End If
            If 0 = row.QuoteID Then
                Continue For
            End If

            If result.ContainsQuote(row.QuoteID) Then
                Dim q As SimilarQuote = result.GetQuote(row.QuoteID)
                q.AddPartOrWire(row, target)
            Else
                Dim quoteRow As QuoteDataBase._QuoteRow = quoteMap(row.QuoteID)
                result.AddQuote(row, quoteRow)
            End If
        Next

        Return result

    End Function

    Shared Function LoadQuoteMap() As Dictionary(Of Int32, QuoteDataBase._QuoteRow)

        Dim result As New Dictionary(Of Int32, QuoteDataBase._QuoteRow)

        Dim adaptor1 As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        Dim table1 As New QuoteDataBase._QuoteDataTable
        adaptor1.Fill(table1)

        Dim row1 As QuoteDataBase._QuoteRow
        For Each row1 In table1.Rows
            result.Add(row1.id, row1)
        Next

        Return result

    End Function

End Class

''' <summary>
''' A collcetion of SimilarQuotes
''' </summary>
''' <remarks></remarks>
Public Class SimilarQuotes
    Inherits ObservableCollection(Of SimilarQuote)

    Private ReadOnly _Target As SimilarQuote

    Public Sub New(target As SimilarQuote)
        _Target = target
    End Sub

    Private ReadOnly _Map As New Dictionary(Of Integer, SimilarQuote)

    Public Sub AddQuote(row As QuoteDataBase._QuoteDetailRow, quoteRow As QuoteDataBase._QuoteRow)
        Dim d As New SimilarQuote(row, quoteRow, _Target)
        Add(d)
        _Map.Add(d.id, d)
    End Sub

    Public Function ContainsQuote(id As Integer) As Boolean
        Return _Map.ContainsKey(id)
    End Function

    Public Function GetQuote(id As Integer) As SimilarQuote
        Return _Map(id)
    End Function

End Class

''' <summary>
''' A quote and how close it matches the target quote
''' </summary>
''' <remarks></remarks>
Public Class SimilarQuote

    Private ReadOnly quoteRow As QuoteDataBase._QuoteRow
    Public wires As New Dictionary(Of String, SimilarWire)
    Public parts As New Dictionary(Of String, SimilarPart)
    Property id As Integer
    Property matchWires As Integer
    Property matchParts As Integer
    Property matchWiresAndQty As Integer
    Property matchPartsAndQty As Integer

    Public Sub New(row As QuoteDataBase._QuoteDetailRow, _
                   quoteRow As QuoteDataBase._QuoteRow, _
                   target As SimilarQuote)
        Me.quoteRow = quoteRow
        Init(row, target)
    End Sub

    Public ReadOnly Property AsIsQuote As Boolean
        Get
            Return quoteRow.IsQuote
        End Get
    End Property

    Public ReadOnly Property MatchPercent As Integer
        Get
            Dim total As Decimal
            total += parts.Count
            total += wires.Count
            total *= 2

            Dim totalMatch As Decimal
            totalMatch += matchParts
            totalMatch += matchPartsAndQty
            totalMatch += matchWires
            totalMatch += matchWiresAndQty

            Return CInt((totalMatch / total) * 100)
        End Get
    End Property

    Public Sub AddPartOrWire(row As QuoteDataBase._QuoteDetailRow, _
                             target As SimilarQuote)
        If row.IsWire Then
            Dim wire As New SimilarWire
            wire.partNumber = row.ProductCode
            wire.Qty = row.Qty
            If Not wires.ContainsKey(wire.partNumber) Then
                wires.Add(wire.partNumber, wire)

                If target.wires.ContainsKey(wire.partNumber) Then
                    matchWires += 1

                    Dim o As SimilarWire = target.wires(wire.partNumber)
                    If o.Qty = row.Qty Then
                        matchWiresAndQty += 1
                    End If

                End If
            End If

        Else
            Dim part As New SimilarPart
            part.partNumber = row.ProductCode
            part.Qty = row.Qty
            If Not parts.ContainsKey(part.partNumber) Then
                parts.Add(part.partNumber, part)

                If target.parts.ContainsKey(part.partNumber) Then
                    matchParts += 1

                    Dim o As SimilarPart = target.parts(part.partNumber)
                    If o.Qty = row.Qty Then
                        matchPartsAndQty += 1
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub Init(row As QuoteDataBase._QuoteDetailRow, _
                     target As SimilarQuote)
        id = row.QuoteID
        If row.IsWire Then
            Dim wire As New SimilarWire
            wire.partNumber = row.ProductCode
            wire.Qty = row.Qty
            wires.Add(wire.partNumber, wire)
            If Not target Is Nothing Then
                If target.wires.ContainsKey(wire.partNumber) Then

                    Dim o As SimilarWire = target.wires(wire.partNumber)
                    If o.Qty = row.Qty Then
                        matchWiresAndQty += 1
                    End If

                End If
            End If
        Else
            Dim part As New SimilarPart
            part.partNumber = row.ProductCode
            part.Qty = row.Qty
            parts.Add(part.partNumber, part)

            If Not target Is Nothing Then
                If target.parts.ContainsKey(part.partNumber) Then
                    matchParts += 1

                    Dim o As SimilarPart = target.parts(part.partNumber)
                    If o.Qty = row.Qty Then
                        matchPartsAndQty += 1
                    End If

                End If
            End If
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return "" & id & " Wires(" & wires.Count & ") Parts(" & parts.Count & ")"
    End Function

End Class

''' <summary>
''' Holds the number of similar wires
''' </summary>
''' <remarks></remarks>
Public Structure SimilarWire
    Property partNumber As String
    Property Qty As Decimal

    Public Overrides Function ToString() As String
        Return "" + partNumber + ":" + Qty
    End Function
End Structure

''' <summary>
''' Holds the number of similar components
''' </summary>
''' <remarks></remarks>
Public Structure SimilarPart
    Property partNumber As String
    Property Qty As Decimal

    Public Overrides Function ToString() As String
        Return "" + partNumber + ":" + Qty
    End Function
End Structure

