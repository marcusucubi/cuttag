Imports System.ComponentModel
Imports System.Drawing.Design
Imports DCS.Quote.ObjectGenerator
Imports System.ComponentModel.Design
Imports DCS.Quote.Common.CustomPropertiesFactory

Namespace Common

    Public Class CustomPropertiesFactory
        Inherits SaveableProperties

        Private WithEvents _PropInfos As New ListWithEvents2
        Private _Parent As Common.Header

        Public Class PropInfo
            Public Property Name As String = "Property"
            Public Property Expression As String
            Private Shared _Count As Integer

            Public Sub New()
                _Count = _Count + 1
                Name = Name & _Count
            End Sub

        End Class

        Public Sub New(ByVal parent As Common.Header)
            Me._Parent = parent
        End Sub

        <EditorAttribute(GetType(PropCollectionEditor), GetType(UITypeEditor))> _
        Public ReadOnly Property Properties As ListWithEvents2
            Get
                Return _PropInfos
            End Get
        End Property

        Public Function Generate() As SaveableProperties

            Dim g As New ObjectGenerator

            For Each o As PropInfo In Me._PropInfos
                Dim info As New PropertyInfo
                info.Name = o.Name
                info.CodeSnippet = o.Expression
                info.TypeName = "System.String"
                g.Add(info)
            Next

            g.BaseTypeName = "Common.CustomProperties"
            g.InitObject = _Parent.ComputationProperties

            Return g.Generate
        End Function

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
