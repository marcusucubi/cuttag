Imports DifferenceEngine
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class frmCompare

    Private _Header1 As Common.Header
    Private _Header2 As Common.Header
    Private _IgnoreSelect As Boolean

    Public Sub New(ByVal q1 As Common.Header, ByVal q2 As Common.Header)

        _Header1 = q1
        _Header2 = q2

        InitializeComponent()

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
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add(v.Line)
                        o.Tag = Drawing.Color.LightGreen

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
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add(v.Line)
                        o.Tag = Drawing.Color.White

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
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add("")
                        o.Tag = Drawing.Color.LightGray

                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next
                    Exit Select

                Case DiffResultSpanStatus.Replace
                    For i = 0 To drs.Length - 1

                        Dim v = source.GetByIndex(drs.SourceIndex + i)
                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.LightGreen
                        lviS.SubItems.Add(v.Line)
                        lviD.BackColor = Drawing.Color.LightGreen
                        Dim v2 = destination.GetByIndex(drs.DestIndex + i)
                        lviD.SubItems.Add(v2.Line)
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add(v.Line)
                        o.Tag = Drawing.Color.LightGreen

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

    Private Sub ListView1_DrawItem(sender As System.Object, e As System.Windows.Forms.DrawListViewItemEventArgs) Handles ListViewDestination.DrawItem
        Draw(e)
    End Sub

    Private Sub Draw(e As System.Windows.Forms.DrawListViewItemEventArgs)

        Dim width As Integer = (Me.ListViewDestination.Width - 25) / 2

        Dim left As New Rectangle(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height)
        Dim right As New Rectangle(e.Bounds.X + (width), e.Bounds.Y, width, e.Bounds.Height)

        Dim leftBackColor As Color = Color.White
        Dim rightBackColor As Color = Color.White

        Dim isSelected As Boolean = False

        If Not (e.State And ListViewItemStates.Selected) = 0 Then
            leftBackColor = Color.LightBlue
            rightBackColor = Color.LightBlue
        Else
            leftBackColor = e.Item.BackColor
            rightBackColor = e.Item.SubItems(2).Tag
        End If

        Dim font As New Font(FontFamily.GenericMonospace, 8)

        Dim text1 As String = e.Item.SubItems(1).Text
        Dim text2 As String = e.Item.SubItems(2).Text

        Dim point1 As New Point(e.Bounds.X, e.Bounds.Y)
        Dim point2 As New Point(e.Bounds.X + (width), e.Bounds.Y)

        e.Graphics.FillRectangle(New SolidBrush(leftBackColor), left)
        TextRenderer.DrawText( _
            e.Graphics, text1, font, left, _
            Color.Black, leftBackColor, TextFormatFlags.TextBoxControl)
        e.Graphics.DrawRectangle(Drawing.Pens.Black, left)

        e.Graphics.FillRectangle(New SolidBrush(rightBackColor), right)
        TextRenderer.DrawText( _
            e.Graphics, text2, font, right, _
            Color.Black, rightBackColor, TextFormatFlags.Left)
        e.Graphics.DrawRectangle(Drawing.Pens.Black, right)

    End Sub

    Private Sub ListViewDestination_Resize(sender As System.Object, e As System.EventArgs) Handles ListViewDestination.Resize
        Me.ListViewDestination.Refresh()
    End Sub

End Class