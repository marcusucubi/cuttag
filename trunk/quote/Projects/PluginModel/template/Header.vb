﻿Imports System.ComponentModel
Imports System.Reflection

Imports Model
Imports Model.Common

Namespace Template

    Public Class Header
        Inherits Common.Header

        Private WithEvents _Details As DetailCollection(Of Common.Detail)

        Public Sub New()
            Me.New(0)
            _Details = MyBase.Details
            Me.SendEvents()
        End Sub

        Public Sub New(ByVal id As Long)
    
            _Details = MyBase.Details

            MyBase.SetPrimaryProperties(New PrimaryProperties(id))

            MyBase.SetOtherProperties(PropertyFactory.CreateOtherProperties(Me, id))
            MyBase.SetComputationProperties(PropertyFactory.CreateComputationProperties(Me, id))
            MyBase.SetNoteProperties(New NoteProperties())
            
            MyBase.AddDependent(MyBase.ComputationProperties)
            MyBase.AddDependent(MyBase.OtherProperties)
            MyBase.AddDependent(MyBase.PrimaryProperties)
            MyBase.AddDependent(MyBase.NoteProperties)
        End Sub

        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")> _
        Public Overloads ReadOnly Property IsQuote As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overloads ReadOnly Property ID As Integer
            Get
                Return PrimaryProperties.CommonId
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

        Private Sub _col_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs) _
            Handles _Details.ListChanged
            SendEvents()
        End Sub

        Public Overloads Sub SendEvents()
            Me.ComputationProperties.SendEvents()
        End Sub

    End Class
End Namespace