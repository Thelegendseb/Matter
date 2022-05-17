Imports System.Windows.Forms
Public Class XWindow
    Inherits Form
    Public Sub New(RunOnCreate As Boolean)
        Application.EnableVisualStyles()
        If RunOnCreate Then Application.Run(Me)
    End Sub
    Public Sub New()
        Application.EnableVisualStyles()
    End Sub
    Public Sub Run()
        Application.Run(Me)
    End Sub
    Public Sub DoEvents()
        Application.DoEvents()
    End Sub

End Class
