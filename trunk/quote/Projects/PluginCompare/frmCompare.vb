Imports System.Drawing
Imports System.Drawing.Drawing2D

Imports PluginExport
Imports DifferenceEngine

Public Class frmCompare

    Private _Header1 As Model.Common.Header
    Private _Header2 As Model.Common.Header
    Private _IgnoreSelect As Boolean

    Private _S1 As String
    Private _S2 As String

    Public Sub New(ByVal q1 As Model.Common.Header, ByVal q2 As Model.Common.Header)

        _Header1 = q1
        _Header2 = q2

        InitializeComponent()

        Me.SameButton.Checked = False
        Me.NewLeftButton.Checked = False
        Me.NewRightButton.Checked = False
        Me.ChangedButton.Checked = False

        Me.NewLeftButton.Text = "Only in " & q2.DisplayName
        Me.NewRightButton.Text = "Only in " & q1.DisplayName
        Me.SameButton.Text = "Same and in Both"
        Me.ChangedButton.Text = "Different in Both"

        FillListbox()
        UpdateText()
    End Sub

    Sub FillListbox()

        Dim g1 As TextGenerator
        Dim g2 As TextGenerator
        g1 = New TextGenerator(_Header1, _Header2)
        g2 = New TextGenerator(_Header2, _Header1, g1.SyncDictionary, False)
        TextDiff(g1.List, g2.List)

        _S1 = g1.Data
        _S2 = g2.Data

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

        Dim showSame As Boolean = Not Me.SameButton.Checked
        Dim newRight As Boolean = Not Me.NewLeftButton.Checked
        Dim newLeft As Boolean = Not Me.NewRightButton.Checked
        Dim changed As Boolean = Not Me.ChangedButton.Checked

        ListViewDestination.Items.Clear()

        For Each drs As DiffResultSpan In DiffLines

            Select Case drs.Status
                Case DiffResultSpanStatus.DeleteSource

                    If Not newLeft Then
                        Exit Select
                    End If

                    For i = 0 To drs.Length - 1

                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
                        Dim v = source.GetByIndex(drs.SourceIndex + i)
                        lviD.BackColor = Drawing.Color.LightGray
                        lviD.SubItems.Add("")

                        If v.Line.ToString().StartsWith("Sync") Then
                            Continue For
                        End If

                        lviS.SubItems.Add(v.Line)
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add(v.Line)
                        o.Tag = lviS.BackColor

                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next
                    Exit Select

                Case DiffResultSpanStatus.NoChange

                    If Not showSame Then
                        Exit Select
                    End If

                    For i = 0 To drs.Length - 1
                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.White

                        Dim v = source.GetByIndex(drs.SourceIndex + i)
                        Dim v2 = destination.GetByIndex(drs.DestIndex + i)

                        If v.Line.ToString().StartsWith("Sync") Then
                            Continue For
                        End If

                        If v2.Line.ToString().StartsWith("Sync") Then
                            Continue For
                        End If

                        lviS.ToolTipText = "No change"

                        lviS.SubItems.Add(v.Line)
                        lviD.BackColor = Drawing.Color.White
                        lviD.SubItems.Add(v2.Line)
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add(v.Line)
                        o.Tag = Drawing.Color.White

                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next

                    Exit Select

                Case DiffResultSpanStatus.AddDestination

                    If Not newRight Then
                        Exit Select
                    End If

                    For i = 0 To drs.Length - 1

                        lviS = New ListViewItem(cnt.ToString("00000"))
                        lviD = New ListViewItem(cnt.ToString("00000"))
                        lviS.BackColor = Drawing.Color.LightGray
                        lviD.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
                        Dim v = destination.GetByIndex(drs.DestIndex + i)

                        If v.Line.ToString().StartsWith("Sync") Then
                            Continue For
                        End If

                        lviS.SubItems.Add("")
                        lviD.SubItems.Add(v.Line)
                        Dim o As New ListViewItem.ListViewSubItem
                        o = lviD.SubItems.Add("")
                        o.Tag = Drawing.Color.LightGray

                        ListViewDestination.Items.Add(lviD)
                        cnt = cnt + 1
                    Next
                    Exit Select

                Case DiffResultSpanStatus.Replace

                    If Not changed Then
                        Exit Select
                    End If

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
        If _Header2.IsQuote Then
            s += "Quote " & _Header2.PrimaryProperties.CommonId
        Else
            s += "Template " & _Header2.PrimaryProperties.CommonId
        End If
        s += " to "
        If _Header1.IsQuote Then
            s += "Quote " & _Header1.PrimaryProperties.CommonId
        Else
            s += "Template " & _Header1.PrimaryProperties.CommonId
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
            isSelected = True
        End If
        leftBackColor = e.Item.BackColor
        rightBackColor = e.Item.SubItems(2).Tag

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

        If isSelected Then
            Dim pen As New Pen(Drawing.Brushes.Yellow, 2)
            left.Inflate(-2, -2)
            right.Inflate(-2, -2)
            e.Graphics.DrawRectangle(pen, left)
            e.Graphics.DrawRectangle(pen, right)
        End If

    End Sub

    Private Sub ListViewDestination_Resize(sender As System.Object, e As System.EventArgs) Handles ListViewDestination.Resize
        Me.ListViewDestination.Refresh()
    End Sub

    Private Sub SameButton_Click(sender As System.Object, e As System.EventArgs) Handles SameButton.Click
        FillListbox()
    End Sub

    Private Sub NewLeftButton_Click(sender As System.Object, e As System.EventArgs) Handles NewLeftButton.Click
        FillListbox()
    End Sub

    Private Sub NewRightButton_Click(sender As System.Object, e As System.EventArgs) Handles NewRightButton.Click
        FillListbox()
    End Sub

    Private Sub ChangedButton_Click(sender As System.Object, e As System.EventArgs) Handles ChangedButton.Click
        FillListbox()
    End Sub

    Private Sub ListViewDestination_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ListViewDestination.MouseMove

        Dim test As ListViewHitTestInfo = Me.ListViewDestination.HitTest(e.Location)

        If Not test.Item Is Nothing Then
            'test.Item.Selected = True
        End If

    End Sub

    '    Private Sub ToolTip1_Popup(sender As System.Object, e As System.Windows.Forms.PopupEventArgs) Handles ToolTip1.Popup

    ' If Me.ListViewDestination.SelectedItems.Count = 0 Then
    '     Return
    ' End If

    'Dim item As ListViewItem = Me.ListViewDestination.SelectedItems(0)
    'Me.ToolTip1.SetToolTip(Me.ListViewDestination, item.ToolTipText)

    'End Sub

End Class