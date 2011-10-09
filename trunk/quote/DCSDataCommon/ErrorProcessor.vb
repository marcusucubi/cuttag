Imports System.io
Imports System.Windows.Forms

Public Class ErrorProcessor
	'-----------------------------------------------------
	' Allows error strings to be accumulated and persisted
	'-----------------------------------------------------
	Inherits System.Data.DataTable
	Private m_sFilePath As String
	Private m_sFileName As String
	Public Property FilePath() As String
		Get
			Return m_sFilePath
		End Get
		Set(ByVal Value As String)
			m_sFilePath = Value
		End Set
	End Property
	Public Property FileName() As String
		Get
			Return m_sFileName
		End Get
		Set(ByVal Value As String)
			m_sFileName = Value
		End Set
	End Property
	Public ReadOnly Property ErrorsExist() As Boolean
		Get
			If Me.Rows.Count = 0 Then
				Return False
			Else
				Return True
			End If
		End Get
	End Property
	Public Sub New()
		Me.Columns.Add("Error", System.Type.GetType("System.String"))
	End Sub
	Public Sub Add(ByVal sError As String, Optional ByVal bAdd2Beginning As Boolean = False)
		Dim arValues() As String = New String() {sError}
		Dim row As DataRow = Me.NewRow
		row(0) = sError
		Try
			If bAdd2Beginning Then
				Me.Rows.InsertAt(row, 0)
			Else
				Me.Rows.Add(row)
			End If
		Catch ex As Exception
			Throw New Exception("Error Adding sring to ErrorProcessor: " + ex.Message)
		End Try
	End Sub
	Public Sub Add(ByVal arErrors() As String, Optional ByVal bAdd2Beginning As Boolean = False)
		Dim sError As String
		Dim row As DataRow
		Dim iPosition As Integer = 0
		Try
			For Each sError In arErrors
				row = Me.NewRow
				row(0) = sError
				If bAdd2Beginning Then
					Me.Rows.InsertAt(row, iPosition)
					iPosition += 1
				Else
					Me.Rows.Add(row)
				End If
			Next
		Catch ex As Exception
			Throw New Exception("Error Adding sring array to ErrorProcessor: " + ex.Message)
		End Try
	End Sub
	Public Sub Write2File()
		Dim sw As StreamWriter = Nothing
		Try
			sw = New StreamWriter(Me.FilePath + "\" + Me.FileName)
			Dim dr As DataRow
			For Each dr In Me.Rows
				sw.WriteLine(dr("Error"))
			Next
		Catch ex As Exception
			MsgBox("Could not create and write to error file at " + Me.FilePath + "\" + Me.FileName)
		Finally
			sw.Close()
		End Try
	End Sub
End Class
