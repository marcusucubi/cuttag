Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Transactions
Imports System.Windows.Forms

Imports Model.Quote
Imports Model

Imports DB.QuoteDataBase
Imports DB.QuoteDataBaseTableAdapters

Public Class QuoteSaver

    Public Class QuoteInfoClass
        Public Property PartNumber As String
        Public Property RFQ As String
        Public Property Initials As String
    End Class

    Public Function Save(ByVal q As Model.BOM.Header, _
                         ByVal info As QuoteInfoClass) As Integer
        Return Save(q, info, False)
    End Function

    Public Function Save(ByVal q As Model.BOM.Header, _
                         ByVal info As QuoteInfoClass, _
                         ByVal IsQuote As Boolean) _
                        As Integer

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ' Ensure the properies are updated
        PluginHost.App.MainForm.Focus()

        Dim o As Model.BOM.PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer
        Dim id As Integer = o.CommonID
        If IsQuote Then
            id = 0
        End If

        Dim adaptor As New DB.QuoteDataBaseTableAdapters._QuoteTableAdapter
        If id > 0 Then
            adaptor.Update( _
                o.Customer.Name, info.RFQ, info.PartNumber, _
                False, q.ID, o.Initials, q.PrimaryProperties.CommonCreatedDate, _
                Date.Now, o.Customer.ID, o.CommonID, o.CommonID)
            newId = id
        Else
            adaptor.Connection.Open()
            Dim proxy As New DB.QuoteTableProxy(adaptor)
            proxy.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.Customer.Name, _
                info.RFQ, info.PartNumber, IsQuote, _
                q.ID, info.Initials, Date.Now, Date.Now, _
                o.Customer.ID)
            Dim cmd As SqlCommand = New SqlCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = proxy.Transaction
            newId = CInt(cmd.ExecuteScalar())
            proxy.Transaction.Commit()
            adaptor.Connection.Close()
            If id = 0 And Not IsQuote Then
                q.PrimaryProperties.SetID(newId)
            End If
        End If

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, True)
        CommonSaver.SaveComputationProperties(newId, _
            q.ComputationProperties, True)
        CommonSaver.SaveNoteProperties(newId, q.NoteProperties)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, True)
        adaptor.Connection.Close()

        System.Windows.Forms.Cursor.Current = Cursors.Default

        Return newId
    End Function

End Class
