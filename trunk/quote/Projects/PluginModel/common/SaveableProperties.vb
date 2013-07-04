Imports System.ComponentModel
Imports System.Reflection

Namespace Common
    Public Class SaveableProperties
        Implements INotifyPropertyChanged, ICloneable

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged

#Region "SortedSpaces Consts for alpha sort of property categories"
        Protected Const SortedSpaces1 = "          "
        Protected Const SortedSpaces2 = "          "
        Protected Const SortedSpaces3 = "          "
        Protected Const SortedSpaces4 = "          "
        Protected Const SortedSpaces5 = "          "
        Protected Const SortedSpaces6 = "          "
        Protected Const SortedSpaces7 = "          "
        Protected Const SortedSpaces8 = "          "
        Protected Const SortedSpaces9 = "          "
        Protected Const SortedSpaces10 = "          "
        Protected Const SortedSpaces11 = "          "
#End Region

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

        Public Sub SendEvents()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("sp"))
            MakeDirty()
        End Sub

        Public Function Clone() As Object Implements System.ICloneable.Clone
            Return Me.MemberwiseClone
        End Function

        Public Function FilterProperties(ByVal Props2Filter As PropertyDescriptorCollection) As PropertyDescriptorCollection
            'If HideReadOnlyProperties then eliminate readonly properties starting with a property containing FilterAttribute(True)
            ' and ending with FilterAttribute(False)
            ' allowing all other properties to be included
            '            Dim props As New PropertyDescriptorCollection(Nothing)
            '            Dim i As Integer = 0
            '            Dim bDoFilter As Boolean = False
            '            If ActiveHeader.HideReadOnlyProperties Then
            ' For Each prop As PropertyDescriptor In Props2Filter
            ' If prop.Attributes.Contains(New DCS.Quote.Model.FilterAttribute(True)) And Not bDoFilter Then 'Begin filtering with this property
            'bDoFilter = True
            'End If
            'If Not bDoFilter Then
            ' props.Add(prop) 'Not filtering, so include property
            ' ElseIf Not prop.IsReadOnly Then
            ' props.Add(prop) 'Now filtering readonly properties, but this property is not read only so include property
            ' End If
            ' If prop.Attributes.Contains(New DCS.Quote.Model.FilterAttribute(False)) Then 'Stop filtering with this property
            ' bDoFilter = False
            ' End If
            ' Next
            ' Else
            ' props = Nothing 'All properites will show
            ' End If
            'Return props
            Return Nothing
        End Function
    End Class

End Namespace