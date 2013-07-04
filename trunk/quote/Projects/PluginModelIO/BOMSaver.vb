Imports System.Data.SqlClient
Imports System.Transactions

Imports Model.BOM

Imports DB
Imports DB.QuoteDataBaseTableAdapters
Imports System.Windows.Forms

Public Class BOMSaver

    Public Function Save(ByVal q As Header) _
                        As Integer

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        ' Ensure the properies are updated
        'frmMain.frmMain.Focus()

        Dim o As PrimaryPropeties = q.PrimaryProperties

        Dim newId As Integer
        Dim id As Integer = o.CommonID

        Dim adaptor As New QuoteDataBaseTableAdapters._QuoteTableAdapter
        If id > 0 Then
            adaptor.Connection.Open()
            o.CommonLastModified = Date.Now
            Dim test As Integer = adaptor.Update( _
                o.Customer.Name, o.RequestForQuoteNumber, _
                o.PartNumber, False, Nothing, _
                o.CommonInitials, o.CommonCreatedDate, _
                o.LastModified, o.Customer.ID, _
                o.CommonID, o.CommonID)
            adaptor.Connection.Close()
            newId = id
        Else
            adaptor.Connection.Open()
            Dim proxy As New QuoteTableProxy(adaptor)
            proxy.Transaction = adaptor.Connection.BeginTransaction
            adaptor.Insert(o.Customer.Name, _
                o.RequestForQuoteNumber, o.PartNumber, False, 0, _
                o.CommonInitials, Date.Now, Date.Now, o.Customer.ID)
            Dim cmd As SqlCommand = New SqlCommand( _
                "SELECT @@IDENTITY", adaptor.Connection)
            cmd.Transaction = proxy.Transaction
            newId = CInt(cmd.ExecuteScalar())
            proxy.Transaction.Commit()
            adaptor.Connection.Close()
            If id = 0 Then
                q.PrimaryProperties.SetID(newId)
            End If
        End If

        adaptor.Connection.Open()
        CommonSaver.DeleteProperties(newId)
        CommonSaver.SaveNoteProperties(newId, q.NoteProperties)
        CommonSaver.SaveOtherProperties(newId, q.OtherProperties, True)
        CommonSaver.SaveComputationProperties(newId, q.ComputationProperties, True)
        CommonSaver.DeleteComponents(newId)
        CommonSaver.SaveComponents(q, newId, True)
        adaptor.Connection.Close()

        q.PrimaryProperties.SendEvents()

        q.ComputationProperties.ClearDirty()
        q.OtherProperties.ClearDirty()
        q.PrimaryProperties.ClearDirty()
        q.ClearDirty()

        System.Windows.Forms.Cursor.Current = Cursors.Default

        Return newId
    End Function

End Class
