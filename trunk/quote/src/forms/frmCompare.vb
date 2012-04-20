Imports DifferenceEngine

Public Class frmCompare

    Private _Header1 As Common.Header
    Private _Header2 As Common.Header
    Private _IgnoreSelect As Boolean

    Public Sub New(ByVal q1 As Common.Header, ByVal q2 As Common.Header)

        _Header1 = q1
        _Header2 = q2

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim g1 As TextGenerator = New TextGenerator(_Header1)
        Dim g2 As TextGenerator = New TextGenerator(_Header2)
        TextDiff(g1.List, g2.List)

        UpdateText()
    End Sub

    Public Sub TextDiff(sFile As List(Of String), dFile As List(Of String))

        Dim level As DiffEngineLevel = DiffEngineLevel.SlowPerfect

        Dim sLF As DiffList_TextFile = Nothing
        Dim dLF As DiffList_TextFile = Nothing
        Try
            sLF = New DiffList_TextFile(sFile)
            dLF = New DiffList_TextFile(dFile)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "File Error")
            Return
        End Try

        Try
            Dim time As Double = 0
            Dim de As DiffEngine = New DiffEngine()
            time = de.ProcessDiff(sLF, dLF, level)

            Dim rep As ArrayList = de.DiffReport()
            ShowResults(sLF, dLF, rep, time)
        Catch ex As Exception
            Dim tmp As String = String.Format("{0}{1}{1}***STACK***{1}{2}",
             ex.Message,
             Environment.NewLine,
             ex.StackTrace)
            MessageBox.Show(tmp, "Compare Error")
        End Try
    End Sub

    Public Sub ShowResults(source As DiffList_TextFile, _
                           destination As DiffList_TextFile, _
                           DiffLines As ArrayList, _
                           seconds As Double)

        Dim lviS As ListViewItem
        Dim lviD As ListViewItem
        Dim cnt As Integer = 1
        Dim i As Integer = 1

        For Each drs As DiffResultSpan In DiffLines

            Select Case drs.Status
                Case DiffResultSpanStatus.DeleteSource
                    For i = 0 To drs.Length - 1

                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.LightGreen
                        Dim v = source.GetByIndex(drs.SourceIndex + i)
                        lviS.SubItems.Add(v.Line)
                        lviD.BackColor = Drawing.Color.LightGray
                        lviD.SubItems.Add("")

                        ListViewSource.Items.Add(lviS)
                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next
                    Exit Select

                Case DiffResultSpanStatus.NoChange

                    For i = 0 To drs.Length - 1
                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.White
                        Dim v = source.GetByIndex(drs.SourceIndex + i)
                        lviS.SubItems.Add(v.Line)
                        lviD.BackColor = Drawing.Color.White
                        Dim v2 = destination.GetByIndex(drs.DestIndex + i)
                        lviD.SubItems.Add(v2.Line)

                        ListViewSource.Items.Add(lviS)
                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next

                    Exit Select

                Case DiffResultSpanStatus.AddDestination
                    For i = 0 To drs.Length - 1

                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.LightGray
                        lviS.SubItems.Add("")
                        lviD.BackColor = Drawing.Color.LightGreen
                        Dim v = destination.GetByIndex(drs.DestIndex + i)
                        lviD.SubItems.Add(v.Line)

                        ListViewSource.Items.Add(lviS)
                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next
                    Exit Select

                Case DiffResultSpanStatus.Replace
                    For i = 0 To drs.Length - 1

                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.LightGreen
                        Dim v = source.GetByIndex(drs.SourceIndex + i)
                        lviS.SubItems.Add(v.Line)
                        lviD.BackColor = Drawing.Color.LightGreen
                        Dim v2 = destination.GetByIndex(drs.DestIndex + i)
                        lviD.SubItems.Add(v2.Line)

                        ListViewSource.Items.Add(lviS)
                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next
                    Exit Select

            End Select
        Next

    End Sub

    Private Sub UpdateText()

        Dim s As String = ""
        If _Header1.IsQuote Then
            s += "Quote " & _Header1.PrimaryProperties.CommonID
        Else
            s += "BOM " & _Header1.PrimaryProperties.CommonID
        End If
        s += " to "
        If _Header2.IsQuote Then
            s += "Quote " & _Header2.PrimaryProperties.CommonID
        Else
            s += "BOM " & _Header2.PrimaryProperties.CommonID
        End If

        Me.Text = s
    End Sub

    Private Sub ListViewSource_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListViewSource.SelectedIndexChanged

        If _IgnoreSelect Then
            Return
        End If

        If (ListViewSource.SelectedItems.Count > 0) Then
            _IgnoreSelect = True
            Dim lvi As ListViewItem
            lvi = ListViewDestination.Items(ListViewSource.SelectedItems(0).Index)
            lvi.Selected = True
            lvi.EnsureVisible()
        End If
        _IgnoreSelect = False

    End Sub

    Private Sub ListViewDestination_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListViewDestination.SelectedIndexChanged

        If _IgnoreSelect Then
            Return
        End If

        If (ListViewDestination.SelectedItems.Count > 0) Then
            _IgnoreSelect = True
            Dim lvi As ListViewItem
            lvi = ListViewSource.Items(ListViewDestination.SelectedItems(0).Index)
            lvi.Selected = True
            lvi.EnsureVisible()
        End If
        _IgnoreSelect = False

    End Sub

End Class