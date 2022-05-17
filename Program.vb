Module Program
    Private UX As XSession

    Private UI As XGraphics
    Sub Main()
        UX = New XSession
        UI = New XGraphics
        Start()
    End Sub
    Sub Start()
        UX.SetBounds(UI.GetClientBounds)
        UI.SetTitle("XGraphics Test")
        UI.RunWindow()
        UX.Start()
    End Sub

End Module
