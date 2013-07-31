Imports System.ComponentModel
Imports System.Reflection

Namespace Common
    
    Public Class SavableProperties
        Implements INotifyPropertyChanged, ICloneable

        Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

        Private _IsDirty As Boolean

        Public Event SavableChange As EventHandler

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
            RaiseEvent SavableChange(Me, New EventArgs())
        End Sub

        Public Overridable Sub ClearDirty()
            Me._IsDirty = False
            RaiseEvent SavableChange(Me, New EventArgs())
        End Sub

        Protected Sub AddDependent(ByVal subject As SavableProperties)
            AddHandler subject.SavableChange, AddressOf OnSavableChanged
        End Sub

        Protected Sub RemoveDependent(ByVal subject As SavableProperties)
            Dim address As EventHandler = AddressOf OnSavableChanged
            RemoveHandler subject.SavableChange, address
        End Sub

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId := "args")> _
        Protected Sub OnSavableChanged(ByVal subject As SavableProperties, _
                                       args As EventArgs) _
                                       Handles Me.SavableChange
            If subject.Dirty <> Me.Dirty Then
                Me._IsDirty = True
                RaiseEvent SavableChange(Me, New EventArgs())
            End If
        End Sub

        Public Sub SendEvents()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("sp"))
            MakeDirty()
        End Sub

        Public Function Clone() As Object Implements System.ICloneable.Clone
            Return Me.MemberwiseClone
        End Function

    End Class

End Namespace