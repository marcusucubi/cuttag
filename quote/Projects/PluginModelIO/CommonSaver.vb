﻿Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Transactions
Imports System.ComponentModel

Imports Model.Common

Imports DB.QuoteDataBase
Imports DB.QuoteDataBaseTableAdapters

Public Class CommonSaver

    Public Shared ReadOnly COMPUTATION_PROPERTIES_ID = -1
    Public Shared ReadOnly OTHER_PROPERTIES_ID = -2
    Public Shared ReadOnly CUSTOM_PROPERTIES_ID = -3
    Public Shared ReadOnly NOTE_PROPERTIES_ID = -4

    Public Shared Sub SaveNoteProperties(ByVal id As Integer, _
                       ByVal obj As Object)
        SaveProperties(id, NOTE_PROPERTIES_ID, obj, True)
    End Sub

    Public Shared Sub SaveCustomProperties(ByVal id As Integer, _
                        ByVal obj As Object)
        SaveProperties(id, CUSTOM_PROPERTIES_ID, obj, True)
    End Sub

    Public Shared Sub SaveOtherProperties(ByVal id As Integer, _
                       ByVal obj As Object, _
                       ByVal SaveAll As Boolean)
        SaveProperties(id, OTHER_PROPERTIES_ID, obj, SaveAll)
    End Sub

    Public Shared Sub SaveComputationProperties(ByVal id As Integer, _
                     ByVal obj As Object, _
                     ByVal SaveAll As Boolean)
        SaveProperties(id, COMPUTATION_PROPERTIES_ID, obj, SaveAll)
    End Sub

    Private Shared Sub SaveProperties(ByVal id As Integer, _
                    ByVal childId As Integer, _
                    ByVal obj As Object, _
                    ByVal SaveAll As Boolean)
        Dim props As PropertyInfo() = obj.GetType.GetProperties
        Dim adaptor As New DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter
        Dim pNonDisplayable As PropertyInfo = Nothing
        For Each p As PropertyInfo In props
            'dd_added 2/8/2012
            If Not IsNothing(obj.GetType().GetProperty("Subject").GetValue(obj, Nothing)) Then
                pNonDisplayable = obj.Subject.GetType.GetProperty(p.Name)
            End If
            'dd_added end
            If SaveAll = False Then
                If Not p.CanWrite Then
                    Continue For
                End If
            End If

            Dim cat As String = "Misc"
            With cat
                Dim oa As CategoryAttribute() = p.GetCustomAttributes(GetType(CategoryAttribute), False)
                If oa.Length > 0 Then
                    cat = oa(0).Category
                End If
            End With

            Dim desc As String = ""
            With desc
                Dim oa As DescriptionAttribute() = p.GetCustomAttributes(GetType(DescriptionAttribute), False)
                If oa.Length > 0 Then
                    desc = oa(0).Description
                End If
            End With

            Dim browsable As Boolean = True
            With browsable
                Dim oa As BrowsableAttribute() = p.GetCustomAttributes(GetType(BrowsableAttribute), False)
                If oa.Length > 0 Then
                    browsable = oa(0).Browsable
                End If
            End With
            If Not browsable Then
                Continue For
            End If

            Dim s As String = Nothing
            Dim i As Integer = Nothing
            Dim d As Decimal = Nothing
            Dim b As Boolean = Nothing

            Dim o As Object
            'dd_changed 2/8/2012
            If IsNothing(pNonDisplayable) Then
                o = p.GetValue(obj, Nothing)
            Else
                o = pNonDisplayable.GetValue(obj.Subject, Nothing)
            End If
            'dd_changed end
            If TypeOf o Is Integer Then
                i = CInt(o)
                adaptor.Insert(id, childId, p.Name, Nothing, Nothing, i, cat, desc, Nothing)
            End If
            If TypeOf o Is String Then
                Dim t As New DB.QuoteDataBase._QuotePropertiesDataTable
                Dim max = t.PropertyStringValueColumn.MaxLength
                s = CStr(o)
                If s.Length > max Then
                    s = s.Substring(0, max - 1)
                End If
                adaptor.Insert(id, childId, p.Name, s, Nothing, Nothing, cat, desc, Nothing)
            End If
            If TypeOf o Is Decimal Then
                d = CDec(o)
                adaptor.Insert(id, childId, p.Name, Nothing, d, Nothing, cat, desc, Nothing)
            End If
            If TypeOf o Is DateTime Then
                Dim dt As DateTime = CDate(o)
                If dt.Year > 1 Then
                    adaptor.Insert(id, childId, p.Name, Nothing, Nothing, Nothing, cat, desc, dt)
                End If
            End If
            If TypeOf o Is Boolean Then
                b = CBool(o)
                If b Then
                    adaptor.Insert(id, childId, p.Name, "True", Nothing, Nothing, cat, desc, Nothing)
                Else
                    adaptor.Insert(id, childId, p.Name, "False", Nothing, Nothing, cat, desc, Nothing)
                End If
            End If
        Next
    End Sub

    Public Shared Sub DeleteComponents(ByVal id As Integer)
        Dim adaptor As New _QuoteDetailTableAdapter
        Dim table As DB.QuoteDataBase._QuoteDetailDataTable
        table = adaptor.GetDataByQuoteID(id)
        For Each row As _QuoteDetailRow In table.Rows
            adaptor.Delete(row.id)
        Next
    End Sub

    Private Shared Sub DeleteCustomProperties()
        DeleteProperties(CUSTOM_PROPERTIES_ID)
    End Sub

    Public Shared Sub DeleteProperties(ByVal QuoteID As Integer)

        Dim adaptor As New DB.QuoteDataBaseTableAdapters._QuotePropertiesTableAdapter
        Dim table As _QuotePropertiesDataTable = _
          adaptor.GetDataByQuoteID(QuoteID)
        For Each row As _QuotePropertiesRow In table.Rows
            adaptor.Delete(row.id)
        Next
    End Sub

    Public Shared Sub SaveComponents(ByVal q As Model.Template.Header, _
                     ByVal quoteId As Integer, _
                     ByVal SaveAll As Boolean)

        Dim adaptor As New _QuoteDetailTableAdapter
        Dim oldId As Integer = q.PrimaryProperties.CommonId
        Dim table As _QuoteDetailDataTable = adaptor.GetDataByQuoteID(oldId)
        For Each detail As Model.Common.Detail In q.Details
            adaptor.Connection.Open()
            Dim proxy As New DB.DetailTableProxy(adaptor)
            proxy.Transaction = adaptor.Connection.BeginTransaction
            'dd_Added sourceID, IsWire 10/3/11, SequenceNumber 10/7/11
            adaptor.Insert(quoteId, detail.SequenceNumber, _
                           detail.Qty, _
                           detail.Product.Code, _
                           detail.SourceID, _
                           detail.IsWire, detail.UnitOfMeasure)
            Dim cmd As SqlCommand = New SqlCommand( _
             "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = proxy.Transaction
            Dim id As Integer = CInt(cmd.ExecuteScalar())
            proxy.Transaction.Commit()
            adaptor.Connection.Close()

            SaveProperties(quoteId, id, _
              detail.QuoteDetailProperties, SaveAll)
            detail.ClearDirty()
        Next

    End Sub


End Class
