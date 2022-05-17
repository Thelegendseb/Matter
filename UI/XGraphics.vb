Imports System.Windows.Forms
Imports System.Drawing
Public Class XGraphics

    Private WithEvents Window As XWindow
    Private Toolkit As XDrawing

    Sub New(RunOnCreate As Boolean)
        Me.Window = New XWindow(RunOnCreate)
        Me.Toolkit = New XDrawing(Me.Window.CreateGraphics,
                                  Me.Window.ClientSize.Width,
                                  Me.Window.ClientSize.Height)
        InitiateEventMethods()
    End Sub
    Sub New()
        Me.Window = New XWindow
        Me.Toolkit = New XDrawing(Me.Window.CreateGraphics,
                                  Me.Window.ClientSize.Width,
                                  Me.Window.ClientSize.Height)
        InitiateEventMethods()
    End Sub
    Public Sub Draw()
        Me.Toolkit.Draw()
        Me.Window.DoEvents()
    End Sub
    Private Sub InitiateEventMethods()
        AddHandler Me.Window.MouseDown, AddressOf Me.MouseDown
    End Sub

    Private Sub MouseDown(sender As Object, e As MouseEventArgs)
        Me.Toolkit.Draw()
    End Sub


    '====GETTERS/SETTERS====
    Public Function GetClientBounds() As Rectangle
        Return Me.Window.ClientRectangle
    End Function
    Public Sub SetTitle(arg As String)
        Me.Window.Text = arg
    End Sub
    Public Sub RunWindow()
        Me.Window.Run()
    End Sub

End Class
