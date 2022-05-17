Imports System.Drawing
Public Class XSession

    Private Timer As Stopwatch
    Const FPS As Integer = 60

    Protected EntityList As List(Of XEntity)
    Protected Running As Boolean
    Protected Bounds As Rectangle
    Sub New() 'has to be a form for key down etc.
        Init()
    End Sub
    Private Sub Init() 'doesnt need to be a form


        Me.Timer = New Stopwatch

        Me.EntityList = New List(Of XEntity)

        AddTests()
    End Sub

    Private Sub AddTests()

        Me.EntityList.Add(New XEntity(1))
        Me.EntityList(0).SetPosition(New XVector(50, 100))
        Me.EntityList(0).SetVelocity(New XVector(5, 3))


        Me.EntityList.Add(New XEntity(1.5))
        Me.EntityList(1).SetPosition(New XVector(300, 100))
        Me.EntityList(1).SetVelocity(New XVector(2, 2))


        Me.EntityList.Add(New XEntity(2))
        Me.EntityList(2).SetPosition(New XVector(550, 100))
        Me.EntityList(2).SetVelocity(New XVector(20, 1))


        Me.EntityList.Add(New XEntity(0.5))
        Me.EntityList(3).SetPosition(New XVector(50, 300))
        Me.EntityList(3).SetVelocity(New XVector(10, 4))


        Me.EntityList.Add(New XEntity(0.75))
        Me.EntityList(4).SetPosition(New XVector(300, 300))
        Me.EntityList(4).SetVelocity(New XVector(5, -3))


        Me.EntityList.Add(New XEntity(0.5))
        Me.EntityList(5).SetPosition(New XVector(550, 300))
        Me.EntityList(5).SetVelocity(New XVector(-2, -2))


        Me.EntityList.Add(New XEntity(0.25))
        Me.EntityList(6).SetPosition(New XVector(50, 500))
        Me.EntityList(6).SetVelocity(New XVector(8, -4))
    End Sub
    Public Sub Start()
        Me.Running = True

        While Me.Running
            Me.Timer.Restart()

            Me.Update()

            Me.TimerEnd()
        End While
    End Sub
    Private Sub Update()
        For Each Entity As XEntity In Me.EntityList
            Entity.Update(Me)
        Next
    End Sub
    Private Sub TimerEnd()
        Dim frametime As Double = 1000 / FPS
        Dim elapsed As Double = Me.Timer.ElapsedMilliseconds / 1000
        If frametime < elapsed Then Throw New Exception("Time taken to execute exceeded Frame Rate")
        System.Threading.Thread.Sleep(Int(frametime - elapsed))
    End Sub
    'ALL ENTITY OPS
    Private Sub GravityToggle(G As Boolean)
        For Each Entity As XEntity In Me.EntityList
            Entity.SetAcceleration(0, G)
        Next
    End Sub
    Private Sub AccelerationToggleX(val As Double)
        For Each Entity As XEntity In Me.EntityList
            Entity.SetAcceleration(New XVector(val, Entity.GetAcceleration.Y))
        Next
    End Sub
    Private Sub AccelerationToggleY(val As Double)
        For Each Entity As XEntity In Me.EntityList
            Entity.SetAcceleration(New XVector(Entity.GetAcceleration.X, val))
        Next
    End Sub


    '===GETTERS/SETTERS/OTHERS====
    Public Function GetEntityList() As List(Of XEntity)
        Return Me.EntityList
    End Function
    Public Sub SetBounds(val As Rectangle)
        Me.Bounds = val
    End Sub
    Public Function GetBounds() As Rectangle
        Return Me.Bounds
    End Function
    Public Sub Halt()
        Me.Running = False
    End Sub
End Class
