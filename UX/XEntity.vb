Imports System.Drawing
Public Class XEntity

    Protected Position As XVector
    Protected Velocity As XVector
    Protected Acceleration As XVector
    Protected Mass As Double
    Protected Radius As Double
    Sub New(Mass As Double)
        Me.Mass = Mass
        Me.Radius = Mass * 25
        Me.Position = XVector.Zero
        Me.Velocity = XVector.Zero
        Me.Acceleration = XVector.Zero
    End Sub

    Public Sub Update(ByRef Session As XSession)

        BorderCollisionChecks(Session.GetBounds)
        EntityCollisionChecks(Session.GetEntityList)

        VectorSums()

    End Sub

    Private Sub VectorSums()
        Me.Velocity += Me.Acceleration
        Me.Position += Me.Velocity
    End Sub
    Private Sub EntityCollisionChecks(EList As List(Of XEntity))
        For Each Entity As XEntity In EList
            If Not Me.Equals(Entity) Then
                EntityCollisionCheck(Entity)
            End If
        Next
    End Sub
    Private Sub BorderCollisionChecks(FormBounds As Rectangle)
        Dim CollisionOccured As Boolean = False
        If Me.Position.X + Me.Velocity.X > FormBounds.X + FormBounds.Width Then
            Me.Position.X = FormBounds.X + FormBounds.Width
            Me.Velocity.X *= -1
            CollisionOccured = True
        End If
        If Me.Position.X + Me.Velocity.X < FormBounds.X Then
            Me.Position.X = FormBounds.X
            Me.Velocity.X *= -1
            CollisionOccured = True
        End If
        If Me.Position.Y + Me.Velocity.Y > FormBounds.Y + FormBounds.Height Then
            Me.Position.Y = FormBounds.Y + FormBounds.Height
            Me.Velocity.Y *= -1
            CollisionOccured = True
        End If
        If Me.Position.Y + Me.Velocity.Y < FormBounds.Y Then
            Me.Position.Y = FormBounds.Y
            Me.Velocity.Y *= -1
            CollisionOccured = True
        End If

        If CollisionOccured Then
            Me.Velocity.Dissapate(0.9) 'Friction/ Loss of Ek?
        End If
    End Sub
    Private Sub EntityCollisionCheck(ByRef Entity As XEntity)
        If XMath.EECollisionCheck(Me, Entity) = True Then
            Dim Dist As XVector = Me.Position - Entity.Position
            Dim PenDepth As Double = Me.GetRadius + Entity.GetRadius - Dist.Magnitude
            Dim PenRes As XVector = Dist.Unit * XVector.Convert(PenDepth / 2)
            Me.Position += PenRes * XVector.Convert(Me.Mass)
            Entity.SetPosition(Entity.GetPosition + PenRes * XVector.InverseMultiplier * XVector.Convert(Entity.GetMass))

            ResolveCollision(Entity)
        End If
    End Sub
    Private Sub ResolveCollision(ByRef Entity As XEntity)

        Dim theta As Single = -Math.Atan2(Entity.GetPosition.Y - Me.Position.Y, Entity.GetPosition.X - Me.Position.X)

        'Variable Dec for easy reading
        Dim mass1 As Double = Me.Mass
        Dim mass2 As Double = Entity.GetMass
        Dim initial1 As XVector = Me.Velocity.Rotate(theta)
        Dim initial2 As XVector = Entity.Velocity.Rotate(theta)

        Dim final1 As New XVector(XMath.SUVATs.Velocity(initial1, initial2, mass1, mass2), initial1.Y)
        Dim final2 As New XVector(XMath.SUVATs.Velocity(initial2, initial1, mass1, mass2), initial2.Y)

        Dim vFinal1 As XVector = final1.Rotate(-theta)
        Dim vFinal2 As XVector = final2.Rotate(-theta)

        Me.SetVelocity(vFinal1)
        Entity.SetVelocity(vFinal2)

        Me.Velocity.Dissapate(0.9)  'replace all 0.9s with a constant friction coefficient
        Entity.Velocity.Dissapate(0.9)

    End Sub

    '======GETTERS/SETTERS/MATHS OPS========
    Public Function GetPosition() As XVector
        Return Me.Position
    End Function
    Public Sub SetPosition(ByVal Position As XVector)
        Me.Position = Position
    End Sub
    Public Function GetVelocity() As XVector
        Return Me.Velocity
    End Function
    Public Sub SetVelocity(ByVal Velocity As XVector)
        Me.Velocity = Velocity
    End Sub
    Public Function GetAcceleration() As XVector
        Return Me.Acceleration
    End Function
    Public Sub SetAcceleration(ByVal Acceleration As XVector)
        Me.Acceleration = Acceleration
    End Sub
    Public Sub SetAcceleration(XComponent As Double, EffectedByGravity As Boolean)
        Dim YComponent As Double = 0.0
        Dim Gravity As Double = 1.25 'Stored in Session
        If EffectedByGravity Then
            YComponent = Me.Mass * Gravity
        Else
            YComponent = 0
        End If
        Me.Acceleration = New XVector(XComponent, YComponent)
    End Sub
    Public Function GetRadius() As Double
        Return Me.Radius
    End Function
    Public Sub SetRadius(val As Double)
        Me.Radius = val
    End Sub
    Public Function GetMass() As Double
        Return Me.Mass
    End Function
    Public Sub SetMass(val As Double)
        Me.Mass = val
    End Sub
    Public Function GetBounds() As Rectangle
        Return New Rectangle(Me.Position.X, Me.Position.Y, Me.Radius * 2, Me.Radius * 2)
    End Function
    '=============================
End Class
