Imports System.CodeDom
Imports System.IO
Imports System.CodeDom.Compiler
Imports Microsoft.CSharp
Imports System.Reflection
Imports System.Collections.Specialized

Public Class PropertyLoader

    Public Class Node
        Public Property Name As String
        Public Property TypeName As String
        Public Property Value As Object
        Public Property Category As String
        Public Property Description As String
    End Class

    Private _PropertyNames As New List(Of Node)
    Private _UniqueList As New List(Of String)

    Public Property ClassName As String = "Class1"
    Public Property BaseTypeName As String

    Public Sub Add(ByVal node As Node)
        If Not _UniqueList.Contains(node.Name) Then
            _PropertyNames.Add(node)
            _UniqueList.Add(node.Name)
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

        For Each node As Node In Me._PropertyNames
            Me.AddProperty(class1, node.Name, _
               node.TypeName, node.Value, node.Category, node.Description)
        Next

        Dim code As String = GenerateCode(compileUnit)
        Return CompileCode(code)
    End Function

    Private Function GenerateCode(ByVal compileunit As CodeCompileUnit) _
        As String

        Dim provider As New VBCodeProvider()
        Dim sourceFile As String

        Using sw As New StringWriter()
            Dim tw As New IndentedTextWriter(sw, "    ")
            provider.GenerateCodeFromCompileUnit(compileunit, tw, _
                New CodeGeneratorOptions())
            tw.Close()
            Console.WriteLine(sw.ToString)
            sourceFile = sw.ToString
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
            'writer.WriteLine("Errors building {0} into {1}", _
            '    sourceFile, cr.PathToAssembly)
            For Each ce As CompilerError In cr.Errors
                writer.WriteLine("  {0}", ce.ToString())
                writer.WriteLine()
            Next ce
            MsgBox(writer.ToString)
        Else
            Console.WriteLine("Source built successfully.")
        End If

        Dim ca As Assembly = cr.CompiledAssembly
        Dim classes As Type() = ca.GetTypes()
        For Each t As Type In classes
            If t.Name = ClassName Then
                result = t.MakeByRefType()
                result = Activator.CreateInstance(t)
            End If
        Next

        If cr.Errors.Count > 0 Then
            Throw New DatabaseException()
        End If

        Return result
    End Function

    Private Sub AddProperty(ByVal class1 As CodeTypeDeclaration, _
                            ByVal name As String, _
                            ByVal typeName As String, _
                            ByVal value As Object, _
                            ByVal category As String, _
                            ByVal desc As String)

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

        property1.GetStatements.Add( _
            New CodeMethodReturnStatement( _
                New CodePrimitiveExpression(value)))

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
