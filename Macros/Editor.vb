'Toggle file tracking in solution explorer
Public Sub ToggleFileTracker()
        DTE.ExecuteCommand("View.TrackActivityinSolutionExplorer", "")
End Sub