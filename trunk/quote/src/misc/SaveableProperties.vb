﻿Imports System.ComponentModel

Public Class SaveableProperties

    Private _IsDirty As Boolean

    Public Delegate Sub SavableChangeHandler(ByVal subject As SaveableProperties)

    Public Event SavableChange As SavableChangeHandler

    Public ReadOnly Property Dirty As Boolean
        Get
            Return _IsDirty
        End Get
    End Property

    Protected Overridable Sub MakeDirty()
        Me._IsDirty = True
        RaiseEvent SavableChange(Me)
    End Sub

    Protected Overridable Sub ClearDirty()
        Me._IsDirty = False
        RaiseEvent SavableChange(Me)
    End Sub

    Protected Sub AddDependent(ByVal subject As SaveableProperties)
        AddHandler subject.SavableChange, AddressOf OnSavableChanged
    End Sub

    Protected Sub OnSavableChanged(ByVal subject As SaveableProperties) _
                                Handles Me.SavableChange
        If subject.Dirty <> Me.Dirty Then
            Me._IsDirty = True
            RaiseEvent SavableChange(Me)
        End If
    End Sub

End Class
