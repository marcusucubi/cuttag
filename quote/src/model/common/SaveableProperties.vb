Imports System.ComponentModel
Imports System.Reflection

Namespace Common
    Public Class SaveableProperties
        Implements INotifyPropertyChanged, ICloneable

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged
        'dd_Added 11/20/11
        Public Event StatusBarPropertyChanged()

        Private _IsDirty As Boolean

        Public Delegate Sub SavableChangeHandler(ByVal subject As SaveableProperties)

        Public Event SavableChange As SavableChangeHandler

        <Browsable(False)> _
        Public Property Subject As Object

        <Browsable(False)>
        Public ReadOnly Property Dirty As Boolean
            Get
                Return _IsDirty
            End Get
        End Property

        Public Overridable Sub MakeDirty()
            Me._IsDirty = True
            RaiseEvent SavableChange(Me)
        End Sub

        Public Overridable Sub ClearDirty()
            Me._IsDirty = False
            RaiseEvent SavableChange(Me)
        End Sub

        Protected Sub AddDependent(ByVal subject As SaveableProperties)
            AddHandler subject.SavableChange, AddressOf OnSavableChanged
        End Sub

        Protected Sub RemoveDependent(ByVal subject As SaveableProperties)
            RemoveHandler subject.SavableChange, AddressOf OnSavableChanged
        End Sub

        Protected Sub OnSavableChanged(ByVal subject As SaveableProperties) _
                                    Handles Me.SavableChange
            If subject.Dirty <> Me.Dirty Then
                Me._IsDirty = True
                RaiseEvent SavableChange(Me)
            End If
        End Sub

        Friend Sub SendEvents()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("sp"))
            MakeDirty()
        End Sub
        'dd_Added 11/19/11
        Protected Sub SendStatusBarEvent()
            RaiseEvent StatusBarPropertyChanged()
        End Sub

        Public Function Clone() As Object Implements System.ICloneable.Clone
            Return Me.MemberwiseClone
        End Function
        'dd_Problem - Temp workaround - could not bubble event to frmMain
        Private Sub SaveableProperties_StatusBarPropertyChanged() Handles Me.StatusBarPropertyChanged
            CType(Application.OpenForms("frmMain"), frmMain).UpdateStatusBar()
        End Sub
    End Class

End Namespace