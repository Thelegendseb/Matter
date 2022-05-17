Imports System.Drawing
Public Class XVector
    Public Property X As Double
    Public Property Y As Double

    Sub New()
        Me.X = 0
        Me.Y = 0
    End Sub
    Sub New(x As Double, y As Double)
        Me.X = x
        Me.Y = y
    End Sub

    Public Sub SetMag(newmag As Double)
        Dim firstmag As Double = Me.Magnitude
        Me.X = Me.X * newmag / firstmag
        Me.Y = Me.Y * newmag / firstmag
    End Sub

    Public Function Magnitude() As Double
        Return Math.Sqrt(Me.X * Me.X + Me.Y * Me.Y)
    End Function

    Public Function Normalize() As XVector
        Dim len As Double = Me.Magnitude
        Return New XVector(Me.X / len, Me.Y / len)
    End Function
    Public Function Distance(v1 As XVector) As Double
        Dim dx As Double = v1.X - Me.X
        Dim dy As Double = v1.Y - Me.Y
        Return Math.Sqrt(dx * dx + dy * dy)
    End Function
    Public Shared Function Convert(v1 As Double) As XVector
        Return New XVector(v1, v1)
    End Function
    Public Sub Dissapate(Coefficient As Double)
        Me.X *= Coefficient
        Me.Y *= Coefficient
    End Sub
    Public Function Rotate(theta As Double) As XVector
        Dim Old As XVector = Me
        Dim Mid As XVector = Midpoint()
        Dim NewX As Double = Math.Cos(theta) * (Old.X - Mid.X) - Math.Sin(theta) * (Old.Y - Mid.Y) + Mid.X
        Dim NewY As Double = Math.Sin(theta) * (Old.X - Mid.X) + Math.Cos(theta) * (Old.Y - Mid.Y) + Mid.Y
        Return New XVector(NewX, NewY)
    End Function
    Public Function Midpoint() As XVector
        Return New XVector(Me.X / 2, Me.Y / 2)
    End Function
    Public Function Dot() As Double
        Return Me.X * Me.X + Me.Y * Me.Y
    End Function
    Public Function Unit() As XVector
        Dim len As Double = Me.Magnitude
        Return New XVector(Me.X / len, Me.Y / len)
    End Function
    Public ReadOnly Property ToPointF() As PointF
        Get
            Return New PointF(CSng(X), CSng(Y))
        End Get
    End Property

    Public Shared Operator +(ByVal v1 As XVector, ByVal v2 As XVector) As XVector
        Return New XVector(v1.X + v2.X, v1.Y + v2.Y)
    End Operator

    Public Shared Operator -(ByVal v1 As XVector, ByVal v2 As XVector) As XVector
        Return New XVector(v1.X - v2.X, v1.Y - v2.Y)
    End Operator

    Public Shared Operator *(ByVal v1 As XVector, ByVal v2 As XVector) As XVector
        Return New XVector(v1.X * v2.X, v1.Y * v2.Y)
    End Operator

    Public Shared Operator /(ByVal v1 As XVector, ByVal v2 As Double) As XVector
        Return New XVector(v1.X / v2, v1.Y / v2)
    End Operator

    Public Shared ReadOnly Property Zero As XVector
        Get
            Return New XVector(0, 0)
        End Get
    End Property
    Public Shared ReadOnly Property InverseMultiplier As XVector
        Get
            Return New XVector(-1, -1)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("[{0},{1}]", Me.X, Me.Y)
    End Function
End Class

