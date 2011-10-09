Public Class LookupTextBox
	Inherits System.Windows.Forms.TextBox
	Public arIndex() As Integer
	Public arLookup() As String
	Public Count As Integer	'9/21/2010 added as integer
	Public IsDirty As Boolean	'9/21/2010 add as boolean
	Public arL As System.Collections.ArrayList
	Event KeyPreviewDCS(ByRef Handled As Boolean, ByVal keyData As System.Windows.Forms.Keys)
	Public Sub New()
		MyBase.New()
		arL = New System.Collections.ArrayList()
	End Sub
	Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
		Dim bHandled As Boolean = False
		RaiseEvent KeyPreviewDCS(bHandled, keyData)
		If bHandled Then Return True
		Return MyBase.ProcessCmdKey(msg, keyData)
	End Function
End Class
Public Class LookupObject
	Implements IComparable
	Private m_Index As Int32
	Private m_Lookup As String
	Public Sub New(ByVal iIndex As Int32, ByVal sLookup As String)
		MyBase.New()
		Me.m_Index = iIndex
		Me.m_Lookup = sLookup
	End Sub
	Public Function CompareTo(ByVal obj As Object) _
		As Integer Implements IComparable.CompareTo
		Try
			Return Me.m_Lookup.CompareTo(obj.Lookup)
		Catch ex As System.Exception
			Throw (ex)
			Exit Function
		End Try
	End Function
	Public ReadOnly Property Index() As Int32
		Get
			Return m_Index
		End Get
	End Property
	Public ReadOnly Property Lookup() As String
		Get
			Return m_Lookup
		End Get
	End Property
	Public Overrides Function ToString() As String
		Return Me.m_Index.ToString & " - " & Me.m_Lookup
	End Function
End Class
