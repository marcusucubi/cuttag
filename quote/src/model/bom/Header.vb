Imports System.ComponentModel
Imports System.Reflection

Namespace Model.BOM

    Public Class Header
        Inherits Common.Header

        Public Sub New()
            Me.New(0)
            Me.SendEvents()
        End Sub

        Public Sub New(ByVal id As Long)
            _PrimaryProperties = New PrimaryPropeties(Me, id)
            _ComputationProperties = New ComputationProperties(Me)
            _OtherProperties = New OtherProperties(Me)
            _NoteProperties = New NoteProperties(Me)
            MyBase.AddDependent(_ComputationProperties)
            MyBase.AddDependent(_OtherProperties)
            MyBase.AddDependent(_PrimaryProperties)
        End Sub

        Public Overloads ReadOnly Property IsQuote As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overloads ReadOnly Property ID As Integer
            Get
                Return PrimaryProperties.CommonID
            End Get
        End Property

        Public Overrides Function NewDetail(ByVal product As Product) As Common.Detail

            Dim oo As Detail = New Detail(Me, product)

            MyBase.Details.Add(oo)
            MyBase.AddDependent(oo)
            SendEvents()

            Return oo
        End Function

        Public Sub Remove(ByVal detail As Detail)
            If detail IsNot Nothing Then
                Me.Details.Remove(detail)

                MyBase.RemoveDependent(detail)
                SendEvents()
            End If
        End Sub

        Private Sub _col_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Details.ListChanged
            SendEvents()
        End Sub

        Private Overloads Sub SendEvents()
            Me.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace
