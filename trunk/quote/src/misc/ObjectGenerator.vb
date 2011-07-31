Imports System.CodeDom
Imports System.IO
Imports System.CodeDom.Compiler
Imports Microsoft.CSharp
Imports System.Reflection
Imports System.Collections.Specialized

Public Class ObjectGenerator

    Public Class PropertyInfo
        Public Property Name As String
        Public Property TypeName As String
        Public Property Value As Object
        Public Property CodeSnippet As String
        Public Property Category As String
        Public Property Description As String
    End Class

    Private _InfoList As New List(Of PropertyInfo)
    Private _NameList As New List(Of String)

    Public Property ClassName As String = "GeneratedProperties"
    Public Property BaseTypeName As String
    Public Property InitObject As Object

    Public Sub Add(ByVal node As PropertyInfo)
        If Not _NameList.Contains(node.Name) Then
            _InfoList.Add(node)
            _NameList.Add(node.Name)
        End If
    End Sub

    Public Function Generate() As Object

        Dim compileUnit As New CodeCompileUnit()
        Dim samples As New CodeNamespace("DCS.Quote.Prperties")

        samples.Imports.Add(New CodeNamespaceImport("System"))
        samples.Imports.Add(New CodeNamespaceImport("DCS.Quote.Common"))
        samples.Imports.Add(New CodeNamespaceImport("System.ComponentModel"))
        compileUnit.Namespaces.Add(samples)
        Dim class1 As New CodeTypeDeclaration(ClassName)
        samples.Types.Add(class1)
        If Me.BaseTypeName.Length > 0 Then
            class1.BaseTypes.Add(Me.BaseTypeName)
        End If

        For Each node As PropertyInfo In Me._InfoList
            If node.Value Is Nothing And node.CodeSnippet Is Nothing Then
                Me.AddProperty(class1, node.Name)
            Else
                Me.AddProperty(class1, node.Name, _
                   node.TypeName, node.Value, node.CodeSnippet, _
                   node.Category, node.Description)
            End If
        Next

        'OnGenerateParentProperty(class1)

        If Me.InitObject IsNot Nothing Then
            'Dim ctor As New CodeConstructor
            'ctor.Parameters.Add(New CodeParameterDeclarationExpression( _
            '                    "System.Object", "Parent"))
            'Dim as1 As New CodeAssignStatement( _
            '    New CodeVariableReferenceExpression("_Parent"), _
            '    New CodeVariableReferenceExpression("Parent"))
            'ctor.Statements.Add(as1)
            'ctor.Attributes = MemberAttributes.Public
            'class1.Members.Add(ctor)
        End If

        Dim code As String = GenerateCode(compileUnit)
        Return CompileCode(code)
    End Function

    Private Sub OnGenerateParentProperty(ByVal class1 As CodeTypeDeclaration)
        Dim parent As New CodeMemberField()
        parent.Name = "Parent"
        parent.Type = New CodeTypeReference("System.Object")
        parent.Attributes = MemberAttributes.Public
        class1.Members.Add(parent)
    End Sub

    Private Function GenerateCode(ByVal compileunit As CodeCompileUnit) _
        As String

        Dim provider As New VBCodeProvider()
        Dim sourceFile As String

        Using sw As New StringWriter()
            Dim tw As New IndentedTextWriter(sw, "    ")
            provider.GenerateCodeFromCompileUnit(compileunit, tw, _
                New CodeGeneratorOptions())
            tw.Close()
            sourceFile = sw.ToString
            Console.WriteLine(sourceFile)
        End Using

        Return sourceFile
    End Function

    Private Function CompileCode(ByVal sourceFile As String) As Object

        Dim result As Object = Nothing
        Dim provider As New VBCodeProvider()
        Dim cp As New CompilerParameters()

        cp.ReferencedAssemblies.Add("System.dll")

        Dim dpath As String = My.Application.Info.DirectoryPath
        Dim aname As String = My.Application.Info.AssemblyName + ".exe"
        cp.ReferencedAssemblies.Add(dpath + "\" + aname)

        cp.GenerateExecutable = False
        cp.GenerateInMemory = True
        Dim cr As CompilerResults = provider.CompileAssemblyFromSource(cp, sourceFile)

        If cr.Errors.Count > 0 Then
            Dim writer As New StringWriter
            For Each ce As CompilerError In cr.Errors
                writer.WriteLine("{0}", ce.ToString())
                writer.WriteLine()
            Next ce
            Throw New ObjectGeneratorException(writer.ToString)
        End If

        Dim ca As Assembly = cr.CompiledAssembly
        Dim classes As Type() = ca.GetTypes()
        For Each t As Type In classes
            If t.Name = ClassName Then
                result = t.MakeByRefType()
                result = Activator.CreateInstance(t)
                If Me.InitObject IsNot Nothing Then
                    result.Subject = Me.InitObject
                End If
            End If
        Next

        If cr.Errors.Count > 0 Then
            Throw New DatabaseException()
        End If

        Return result
    End Function

    Private Sub AddProperty(ByVal class1 As CodeTypeDeclaration, _
                            ByVal name As String, _
                            Optional ByVal typeName As String = "System.String", _
                            Optional ByVal value As Object = Nothing, _
                            Optional ByVal snippet As String = Nothing, _
                            Optional ByVal category As String = "", _
                            Optional ByVal desc As String = "")

        Dim property1 As New CodeMemberProperty()
        property1.Name = name
        property1.Type = New CodeTypeReference(typeName)
        property1.Attributes = MemberAttributes.Public
        property1.HasSet = False
        If value Is Nothing Then
            If typeName = "System.Decimal" Then
                value = 0
            End If
            If typeName = "System.Int32" Then
                value = 0
            End If
            If typeName = "System.String" Then
                value = ""
            End If
        End If

        If snippet Is Nothing Then
            property1.GetStatements.Add( _
                New CodeMethodReturnStatement( _
                    New CodePrimitiveExpression(value)))
        Else
            If Me.InitObject IsNot Nothing Then
                For Each node As System.Reflection.PropertyInfo In Me.InitObject.GetType.GetProperties
                    Dim variableRef1 As New CodePropertyReferenceExpression( _
                        New CodeThisReferenceExpression(), "Subject")
                    Dim propertyRef As New CodePropertyReferenceExpression( _
                        variableRef1, node.Name)
                    Dim variableDeclaration As New CodeVariableDeclarationStatement( _
                        node.PropertyType, node.Name, propertyRef)
                    property1.GetStatements.Add(variableDeclaration)
                Next
            End If

            property1.GetStatements.Add( _
                New CodeMethodReturnStatement( _
                    New CodeSnippetExpression(snippet)))
        End If

        Dim arg = New CodeAttributeArgument( _
            New CodePrimitiveExpression(category))
        property1.CustomAttributes.Add( _
            New CodeAttributeDeclaration( _
                   "CategoryAttribute", arg))
        Dim arg2 = New CodeAttributeArgument( _
            New CodePrimitiveExpression(desc))
        property1.CustomAttributes.Add( _
            New CodeAttributeDeclaration( _
                   "DescriptionAttribute", arg2))
        class1.Members.Add(property1)

    End Sub

End Class
