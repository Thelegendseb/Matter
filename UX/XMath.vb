Public Class XMath

    'compatibility with XVector,XEntity Class
    Public Class SUVATs
        Public Shared Function Velocity(Initial1 As XVector, Initial2 As XVector, Mass1 As Double, Mass2 As Double) As Double
            Return Initial1.X * (Mass1 - Mass2) / (Mass1 + Mass2) + Initial2.X * 2 * Mass2 / (Mass1 + Mass2)
        End Function
    End Class
    Public Shared Function Distance(v1 As XVector, v2 As XVector)
        Return Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2))
    End Function

    Public Shared Function EECollisionCheck(e1 As XEntity, e2 As XEntity) As Boolean
        Dim Dist As Double = Distance(e1.GetPosition, e2.GetPosition)
        If Dist < e1.GetRadius + e2.GetRadius Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function Map(Value As Double,
                     Min As Double,
                     Max As Double,
                     Min2 As Double,
                     Max2 As Double) As Double
        Return (((Value - Min) / (Max - Min)) * (Max2 - Min2)) + Min2
    End Function

End Class

