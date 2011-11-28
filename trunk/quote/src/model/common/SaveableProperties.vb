Imports System.ComponentModel
Imports System.Reflection

Namespace Common
    Public Class SaveableProperties
        Implements INotifyPropertyChanged, ICloneable

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged
        'dd_Added 11/20/11
        Public Event StatusBarPropertyChanged()
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
        'dd_Added End

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
        Protected Function FilterProperties(ByVal Props2Filter As PropertyDescriptorCollection) As PropertyDescriptorCollection
            Dim props As New PropertyDescriptorCollection(Nothing)
            Dim i As Integer = 0
            Dim aFilterTrue As New Model.FilterAttribute(True)
            Dim aFilterFalse As New Model.FilterAttribute(False)
            Dim bDoFilter As Boolean = False
            If ActiveHeader.HideReadOnlyProperties Then
                For Each prop As PropertyDescriptor In Props2Filter
                    '    i += 1
                    'Dim b As Boolean
                    If prop.Attributes.Contains(aFilterTrue) And Not bDoFilter Then
                        bDoFilter = True
                    End If
                    'b = False
                    If Not bDoFilter Then
                        props.Add(prop)
                        '    b = True
                    ElseIf Not prop.IsReadOnly Then
                        '    b = True
                        props.Add(prop)
                    End If
                    If prop.Attributes.Contains(aFilterFalse) Then
                        bDoFilter = False
                    End If
                    ' Debug.WriteLine(i.ToString + ":(" + b.ToString + ")" _
                    '                 + prop.Category + "|" + prop.DisplayName + "|" + prop.Description)
                Next
            Else
                props = Nothing 'All properites will show
            End If
            Return props
        End Function

        'dd_Problem - Temp workaround - could not bubble event to frmMain
        Private Sub SaveableProperties_StatusBarPropertyChanged() Handles Me.StatusBarPropertyChanged
            CType(Application.OpenForms("frmMain"), frmMain).UpdateStatusBar()
        End Sub
    End Class

End Namespace