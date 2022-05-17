Imports System.Drawing
Public Class XDrawing
    Private BackColor As Color
    'used by an XCanvas object

    Private g As Graphics
    Private buffer As BufferedGraphics
    Private context As BufferedGraphicsContext

    Sub New(ByRef gin As Graphics, width As Integer, height As Integer)
        Me.g = gin
        Me.context = BufferedGraphicsManager.Current
        Me.context.MaximumBuffer = New Size(width + 1, height + 1)
        Me.buffer = context.Allocate(g, New Rectangle(0, 0, width, height))
    End Sub
    Private Sub Clear(C As Color)
        buffer.Graphics.Clear(C)
    End Sub
    Private Sub Clear()
        buffer.Graphics.Clear(Me.BackColor)
    End Sub

    Private Sub Render()
        buffer.Render(Me.g)
    End Sub
    Public Sub Draw()
        Clear(Color.White)



        Render()
    End Sub

End Class
