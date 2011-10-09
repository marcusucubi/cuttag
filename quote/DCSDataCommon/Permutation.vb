Public Class Permutation
  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  'Example:
  'Dim pp As New DCS.Permutation(3)
  '  Do
  '    MsgBox(pp.ToString)
  '  Loop While pp.MoveNext
  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
  Private m_iAr() As Integer
  Private m_sAr() As String 'Not Used
  Private m_bSwap() As Boolean 'Not Used
  Private m_bSwappable As Boolean = False 'Not Used
  Private m_iSwapIndex(1) As Integer 'NotUsed
  Public Sub New(ByVal iElementCount As Integer)
    m_iAr = Array.CreateInstance(System.Type.GetType("System.Int32"), iElementCount)
    Reset()
  End Sub
  Public Sub NewNotUsed(ByVal sArray() As String, Optional ByVal bSwap() As Boolean = Nothing)
    m_iAr = Array.CreateInstance(System.Type.GetType("System.Int32"), sArray.Length)
    m_sAr = sArray.Clone
    If bSwap Is Nothing Then
      m_bSwap = Nothing
    Else
      If CheckSwapArray(bSwap) Then
        m_bSwap = bSwap
        m_bSwappable = True
      End If
    End If
    Reset()
  End Sub
  Default Public ReadOnly Property Item(ByVal Index As Integer) As Integer
    Get
      Return m_iAr(Index)
    End Get
  End Property
  Public Property InnerArray() As Integer()
    Get
      Return m_iAr.Clone
    End Get
    Set(ByVal Value As Integer())
      m_iAr = Value.Clone
    End Set
  End Property
  Public ReadOnly Property sItemNotUsed(ByVal Index As Integer) As String ''''''
    Get
      Return m_sAr(Index)
    End Get
  End Property
  Public ReadOnly Property SwapItemNotUsed(ByVal Index As Integer) As String
    Get
      Dim retValue As String = ""
      If m_bSwap(Index) Then
        Dim i As Integer
        For i = 0 To m_sAr.Length - 1
          If Not i = Index Then retValue = m_sAr(i)
        Next
      Else
        retValue = m_sAr(Index)
      End If
      If retValue = "" Then
        MsgBox("Problem finding a swap terminal in the permutaion for the " + m_sAr(Index) + " attribute")
      End If
      Return retValue
    End Get
  End Property
  Public ReadOnly Property sItemsNotUsed() As String()
    Get
      Return m_sAr
    End Get
  End Property
  Public ReadOnly Property Items() As Integer()
    Get
      Return m_iAr
    End Get
  End Property
  Public ReadOnly Property SwapItemsNotUsed() As String()
    Get
      Dim retVal() As String = m_sAr.Clone
      Dim sTemp As String = retVal(m_iSwapIndex(0))
      retVal(m_iSwapIndex(0)) = retVal(m_iSwapIndex(1))
      retVal(m_iSwapIndex(1)) = sTemp
      Return retVal
    End Get
  End Property
  Public ReadOnly Property UBound() As Integer
    Get
      Return m_iAr.Length - 1
    End Get
  End Property
  Public ReadOnly Property SwappableNotUsed() As Boolean '''''''
    Get
      Return m_bSwappable
    End Get
  End Property
  Private Function CheckSwapArray(ByVal bSwap() As Boolean) As Boolean  ''''''''
    Dim RetValue As Boolean = False
    If bSwap.Length = m_sAr.Length Then
      Dim iCount As Integer = 0
      Dim iSwapIndex As Integer = 0
      Dim iTrueCount As Integer = 0
      Dim bValue As Boolean
      For Each bValue In bSwap
        If bValue Then
          iTrueCount += 1
          m_iSwapIndex(iSwapIndex) = iCount
          iSwapIndex += 1
        End If
        iCount += 1
      Next
      If iTrueCount = 2 Then
        RetValue = True
      Else
        MsgBox("Permutation swap array must contain only two true values")
      End If
    Else
      MsgBox("Permutation swap array must be same size as element array")
    End If
    Return RetValue
  End Function
  Public Sub Reset()
    Dim i As Integer
    For i = 0 To m_iAr.Length - 1
      m_iAr(i) = i
    Next
  End Sub
  Public Function GetTerminalSwap() As String  '''''''''
    Dim retValue As String
    If m_bSwap Is Nothing Then
      retValue = Me.ToString
    Else
      Dim sTermTemp As String = ""
      Dim sTerm2 As String = ""
      Dim iIndex, iTerm1 As Integer
      Dim sArCopy() As String = m_sAr.Clone
      For iIndex = 0 To m_sAr.Length - 1
        If m_bSwap(iIndex) Then
          If sTermTemp = "" Then
            iTerm1 = iIndex
            sTermTemp = m_sAr(iIndex)
          Else
            m_sAr(iTerm1) = m_sAr(iIndex)
            m_sAr(iIndex) = sTermTemp
          End If
        End If
      Next
      retValue = Me.ToString
      m_sAr = sArCopy.Clone
    End If
    Return retValue
  End Function
  Public Overrides Function ToString() As String
    Dim sb As New System.Text.StringBuilder
    Dim i As Integer
    For i = 0 To m_iAr.Length - 1
      sb.Append(m_iAr(m_iAr(i)) + ",")
    Next
    sb.Remove(sb.Length - 1, 1)
    Return sb.ToString
  End Function
  Public Function ToStringNotUsed() As String
    Dim sb As New System.Text.StringBuilder
    Dim i As Integer
    For i = 0 To m_iAr.Length - 1
      sb.Append(m_sAr(m_iAr(i)) + ",")
    Next
    sb.Remove(sb.Length - 1, 1)
    Return sb.ToString
  End Function
  Public Function MoveNext() As Boolean
    Dim retValue As Boolean
    Dim iArNew() As Integer
    iArNew = m_iAr.Clone
    Dim p1, p2, i, j As Integer
    p1 = iArNew.Length - 2
    Do While (iArNew(p1) > iArNew(p1 + 1)) And (p1 >= 1)
      p1 -= 1
    Loop
    If ((p1 = 0) And (m_iAr(p1) > m_iAr(p1 + 1))) Then
      retValue = False
    Else
      p2 = iArNew.Length - 1
      Do While (iArNew(p1) > iArNew(p2))
        p2 -= 1
      Loop
      Dim temp As Integer = iArNew(p1)
      iArNew(p1) = iArNew(p2)
      iArNew(p2) = temp
      i = p1 + 1
      j = iArNew.Length - 1
      Do While (i < j)
        temp = iArNew(i)
        iArNew(i) = iArNew(j)
        i += 1
        iArNew(j) = temp
        j -= 1
      Loop
      m_iAr = iArNew
      retValue = True
    End If
    Return retValue
  End Function
End Class
