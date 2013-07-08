Imports WeifenLuo.WinFormsUI.Docking

Public Class ViewController

    Private Shared s_ViewController As New ViewController

    Private m_Events As Model.ModelEvents
    Private m_ComputationProperties As frmComputationProperties
    Private m_DetailProperties As frmDetailProperties
    Private m_NoteProperties As frmNoteProperties
    Private m_OtherProperties As frmOtherProperties
    Private m_PrimaryProperties As frmPrimaryProperties

    Public Shared ReadOnly Property Instance As ViewController
        Get
            Return s_ViewController
        End Get
    End Property

    Public Sub OpenAll()
        Dim t
        t = Me.DetailProperties

        t = Me.PrimaryProperties
        t = Me.NoteProperties
        t = Me.OtherProperties
        t = Me.ComputationProperties
    End Sub

    Public ReadOnly Property ComputationProperties() As frmComputationProperties
        Get
            If (m_ComputationProperties Is Nothing) Then
                m_ComputationProperties = New frmComputationProperties
                InitChild(m_ComputationProperties)
            End If

            If (m_ComputationProperties.IsHidden Or m_ComputationProperties.IsDisposed) Then
                m_ComputationProperties = New frmComputationProperties
                InitChild(m_ComputationProperties)
            End If

            Return m_ComputationProperties
        End Get
    End Property

    Public ReadOnly Property DetailProperties() As frmDetailProperties
        Get
            If (m_DetailProperties Is Nothing) Then

                m_DetailProperties = New frmDetailProperties

                PluginHost.App.DockPanel.SuspendLayout(True)
                InitChild(m_DetailProperties, DockState.DockBottom)
                PluginHost.App.DockPanel.ResumeLayout(True, True)

            End If

            If (m_DetailProperties.IsHidden Or m_DetailProperties.IsDisposed) Then
                m_DetailProperties = New frmDetailProperties
                InitChild(m_DetailProperties, DockState.DockBottom)
            End If

            Return m_DetailProperties
        End Get
    End Property

    Public ReadOnly Property NoteProperties() As frmNoteProperties
        Get
            If (m_NoteProperties Is Nothing) Then
                m_NoteProperties = New frmNoteProperties
                InitChild(m_NoteProperties)
            End If

            If (m_NoteProperties.IsHidden Or m_NoteProperties.IsDisposed) Then
                m_NoteProperties = New frmNoteProperties
                InitChild(m_NoteProperties)
            End If

            Return m_NoteProperties
        End Get
    End Property

    Public ReadOnly Property OtherProperties() As frmOtherProperties
        Get
            If (m_OtherProperties Is Nothing) Then
                m_OtherProperties = New frmOtherProperties
                InitChild(m_OtherProperties)
            End If

            If (m_OtherProperties.IsHidden Or m_OtherProperties.IsDisposed) Then
                m_OtherProperties = New frmOtherProperties
                InitChild(m_OtherProperties)
            End If

            Return m_OtherProperties
        End Get
    End Property

    Public ReadOnly Property PrimaryProperties() As frmPrimaryProperties
        Get
            If (m_PrimaryProperties Is Nothing) Then
                m_PrimaryProperties = New frmPrimaryProperties
                InitChild(m_PrimaryProperties)
            End If

            If (m_PrimaryProperties.IsHidden Or m_PrimaryProperties.IsDisposed) Then
                m_PrimaryProperties = New frmPrimaryProperties
                InitChild(m_PrimaryProperties)
            End If

            Return m_PrimaryProperties
        End Get
    End Property

    Private Sub InitChild(ByVal frm As DockContent)
        PluginHost.App.DockPanel.SuspendLayout(True)
        frm.Show(PluginHost.App.DockPanel, DockState.DockRight)
        PluginHost.App.DockPanel.ResumeLayout(True, True)
    End Sub

    Private Sub InitChild(ByVal frm As DockContent, state As DockState)
        PluginHost.App.DockPanel.SuspendLayout(True)
        frm.Show(PluginHost.App.DockPanel, state)
        PluginHost.App.DockPanel.ResumeLayout(True, True)
    End Sub

End Class
