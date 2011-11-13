Imports System.IO

Public Class ExportPath

    Public Shared Function Path()

        Dim myDocs As String = Environment.GetFolderPath( _
            Environment.SpecialFolder.MyDocuments)
        Dim root As String = myDocs + "\WHQ"
        Directory.CreateDirectory(root)
        Return root

    End Function

    Public Shared Function DateFileName()

        Dim t As String
        t = Date.Now.Year.ToString & "-"
        t += Date.Now.Month.ToString & "-"
        t += Date.Now.Day.ToString & "-"
        t += Date.Now.ToShortTimeString
        t = t.Replace(".", "")
        t = t.Replace("/", "")
        t = t.Replace(":", "")
        t = t.Replace(" ", "")
        Return t

    End Function

End Class
