Imports System.ComponentModel
Imports System.Drawing.Design
Imports DCS.Quote.ObjectGenerator
Imports System.ComponentModel.Design
Imports DCS.Quote.Common.CustomPropertiesGenerator

Namespace Common

    Public MustInherit Class CustomPropertiesGenerator
        Inherits SaveableProperties

        Private WithEvents _PropInfos As New List(Of PropInfo)
        Private _Parent As Common.Header

        Public Class PropInfo
            Implements ICloneable

            Private Shared _Count As Integer
            Private _Name As String = "Property"

            Public Property Name As String
                Get
                    Return _Name
                End Get
                Set(ByVal value As String)
                    _Name = value
                    _Name = _Name.Replace(" ", "")
                    _Name = _Name.Replace(".", "")
                End Set
            End Property

            Public Property Expression As String

            Public Sub New()
                _Count = _Count + 1
                _Name = _Name & _Count
            End Sub

            Public Function Clone() As Object Implements System.ICloneable.Clone
                Return MemberwiseClone()
            End Function

        End Class

        Public Sub New(ByVal parent As Common.Header)
            Me._Parent = parent
        End Sub

        <EditorAttribute(GetType(PropCollectionEditor), GetType(UITypeEditor))> _
        Public ReadOnly Property Properties As List(Of PropInfo)
            Get
                Return _PropInfos
            End Get
        End Property

        <Browsable(False)> _
        Public ReadOnly Property Parent As Common.Header
            Get
                Return _Parent
            End Get
        End Property

        Public MustOverride Function Generate() As SaveableProperties

    End Class

    Public Class PropCollectionEditor
        Inherits CollectionEditor

        Private Shared UseCopy As Boolean

        Public Sub New(ByVal newType As Type)
            MyBase.new(newType)
        End Sub

        Protected Overrides Function CanSelectMultipleInstances() _
        As Boolean
            Return False
        End Function

        Protected Overrides Function CreateCollectionItemType() As Type
            Return GetType(PropInfo)
        End Function

        Protected Overrides Sub CancelChanges()
            MyBase.CancelChanges()
            UseCopy = True
        End Sub

        Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, _
                                            ByVal provider As IServiceProvider, _
                                            ByVal value As Object) As Object
            Dim _Copy As New List(Of PropInfo)
            Dim _WorkingObject As List(Of PropInfo)

            _WorkingObject = DirectCast(value, List(Of PropInfo))
            Copy(_Copy, _WorkingObject)

            Do
                UseCopy = False
                _WorkingObject = MyBase.EditValue(context, provider, _WorkingObject)
                If UseCopy Then
                    Copy(_WorkingObject, _Copy)
                End If

                Dim h As Common.Header = ActiveHeader.ActiveHeader.Header
                Try
                    h.GenerateCustomProperties()
                    h.SendEvents()
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Continue Do
                End Try

                Exit Do
            Loop

            Return (_WorkingObject)
        End Function

        Private Sub Copy(ByVal destination As List(Of PropInfo), _
                         ByVal source As List(Of PropInfo))
            destination.Clear()
            For Each o As PropInfo In source
                destination.Add(o.Clone)
            Next
        End Sub

    End Class

End Namespace
