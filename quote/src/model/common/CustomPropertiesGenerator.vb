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

        Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, _
                                            ByVal provider As IServiceProvider, _
                                            ByVal value As Object) As Object
            Dim o As Object
            o = MyBase.EditValue(context, provider, value)

            Dim h As Common.Header = ActiveHeader.ActiveHeader.Header
            h.GenerateCustomProperties()
            h.SendEvents()
            Return (o)
        End Function

    End Class

End Namespace
