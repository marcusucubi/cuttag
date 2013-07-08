Imports System.ComponentModel

Public Class ObjectWrapper
    Implements ICustomTypeDescriptor

    Private m_Target As Object

    Public Sub New(target As Object)
        m_Target = target
    End Sub

    Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
        Return TypeDescriptor.GetClassName(m_Target, True)
    End Function

    Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
        Return TypeDescriptor.GetAttributes(m_Target, True)
    End Function

    Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
        Return TypeDescriptor.GetComponentName(m_Target, True)
    End Function

    Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
        Return TypeDescriptor.GetConverter(m_Target, True)
    End Function

    Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
        Return TypeDescriptor.GetDefaultEvent(m_Target, True)
    End Function

    Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
        Return TypeDescriptor.GetDefaultProperty(m_Target, True)
    End Function

    Public Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
        Return TypeDescriptor.GetEditor(m_Target, editorBaseType, True)
    End Function

    Public Function GetEvents(ByVal attributes As Attribute()) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(m_Target, attributes, True)
    End Function

    Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(m_Target, True)
    End Function

    Public Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Return FilterProperties2(TypeDescriptor.GetProperties(m_Target, attributes, True))
    End Function

    Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Return TypeDescriptor.GetProperties(m_Target, True)
    End Function

    Public Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
        Return m_Target
    End Function

    Public Function FilterProperties2(ByVal Props2Filter As PropertyDescriptorCollection) As PropertyDescriptorCollection

        ' If HideReadOnlyProperties then eliminate readonly properties starting 
        ' with a property containing FilterAttribute(True)
        ' and ending with FilterAttribute(False)
        ' allowing all other properties to be included
        Dim props As New PropertyDescriptorCollection(Nothing)
        Dim i As Integer = 0
        Dim bDoFilter As Boolean = False

        If DisplaySettings.Instance.HideReadOnlyProperties Then

            For Each prop As PropertyDescriptor In Props2Filter

                If prop.Attributes.Contains(New Model.FilterAttribute(True)) And Not bDoFilter Then 'Begin filtering with this property
                    bDoFilter = True
                End If

                If Not bDoFilter Then
                    props.Add(prop) 'Not filtering, so include property
                ElseIf Not prop.IsReadOnly Then
                    props.Add(prop) 'Now filtering readonly properties, but this property is not read only so include property
                End If

                If prop.Attributes.Contains(New Model.FilterAttribute(False)) Then 'Stop filtering with this property
                    bDoFilter = False
                End If

            Next
        Else
            props = Props2Filter 'All properites will show
        End If

        Return props
    End Function

End Class
