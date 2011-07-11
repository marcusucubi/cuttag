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
    End Class

    Public Property ClassName As String = "Class1"
    Public Property PropertyNames As New List(Of Node)
    Public Property BaseTypeName As String


    Public Function Generate() As Object

        Dim compileUnit As New CodeCompileUnit()
        Dim samples As New CodeNamespace("DCS.Quote.Prperties")

        samples.Imports.Add(New CodeNamespaceImport("System"))
        samples.Imports.Add(New CodeNamespaceImport("DCS.Quote.Common"))
        compileUnit.Namespaces.Add(samples)
        Dim class1 As New CodeTypeDeclaration(ClassName)
        samples.Types.Add(class1)
        If Me.BaseTypeName.Length > 0 Then
            class1.BaseTypes.Add(Me.BaseTypeName)
        End If

        'Dim Start As New CodeEntryPointMethod()
        'Dim cs1 As New CodeMethodInvokeExpression( _
        '    New CodeTypeReferenceExpression("System.Console"), "WriteLine", _
        '    New CodePrimitiveExpression("Starting"))
        'Start.Statements.Add(New CodeExpressionStatement(cs1))
        'class1.Members.Add(Start)

        For Each node As Node In Me.PropertyNames
            Me.AddProperty(class1, node.Name, "System.String")
        Next

        Dim code As String = GenerateCode(compileUnit)
        Return CompileCode(code)
    End Function

    Private Function GenerateCode(ByVal compileunit As CodeCompileUnit) _
        As String

        Dim provider As New VBCodeProvider()
        Dim sourceFile As String
        If provider.FileExtension(0) = "." Then
            sourceFile = "HelloWorld" + provider.FileExtension
        Else
            sourceFile = "HelloWorld." + provider.FileExtension
        End If

        Using sw As New StreamWriter(sourceFile, False)
            Dim tw As New IndentedTextWriter(sw, "    ")
            provider.GenerateCodeFromCompileUnit(compileunit, tw, _
                New CodeGeneratorOptions())
            tw.Close()
        End Using

        Dim result As String
        Using sw As New StringWriter()
            Dim tw As New IndentedTextWriter(sw, "    ")
            provider.GenerateCodeFromCompileUnit(compileunit, tw, _
                New CodeGeneratorOptions())
            tw.Close()
            result = sw.ToString
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
            Console.WriteLine("Errors building {0} into {1}", _
                sourceFile, cr.PathToAssembly)
            For Each ce As CompilerError In cr.Errors
                Console.WriteLine("  {0}", ce.ToString())
                Console.WriteLine()
            Next ce
        Else
            Console.WriteLine("Source built successfully.")
        End If

        Dim ca As Assembly = cr.CompiledAssembly
        Dim classes As Type() = ca.GetTypes()
        For Each t As Type In classes
            If t.Name = ClassName Then
                result = t.MakeByRefType()
                result = Activator.CreateInstance(t)
                Console.WriteLine(result)
            End If
            Console.WriteLine(t.Name)
        Next

        If cr.Errors.Count > 0 Then
            Throw New PropertyException()
        End If

        Return result
    End Function

    Private Sub AddProperty(ByVal class1 As CodeTypeDeclaration, _
                           ByVal name As String, _
                           ByVal typeName As String)

        Dim fieldName As String = name + "Field"
        Dim field1 As New CodeMemberField("System.String", fieldName)
        class1.Members.Add(field1)

        Dim property1 As New CodeMemberProperty()
        property1.Name = name
        property1.Type = New CodeTypeReference("System.String")
        property1.Attributes = MemberAttributes.Public
        property1.GetStatements.Add( _
            New CodeMethodReturnStatement( _
                New CodeFieldReferenceExpression( _
                    New CodeThisReferenceExpression(), fieldName)))
        property1.SetStatements.Add( _
            New CodeAssignStatement( _
                New CodeFieldReferenceExpression( _
                    New CodeThisReferenceExpression(), fieldName), _
                New CodePropertySetValueReferenceExpression()))
        class1.Members.Add(property1)

    End Sub

End Class
