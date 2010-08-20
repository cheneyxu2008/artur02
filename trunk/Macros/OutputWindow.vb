'Write to output window
Private Sub Write(ByVal name As String, ByVal message As String)
    Dim output As Window = DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput)
    Dim window As OutputWindow = output.Object
    Dim pane As OutputWindowPane = window.OutputWindowPanes.Item(name)
    pane.Activate()
    pane.OutputString(message)
    pane.OutputString(Environment.NewLine)
End Sub

'Write to Debug window
Private Sub Log(ByVal message As String, ByVal ParamArray args() As Object)
    Write("Debug", String.Format(message, args))
End Sub

'Write to Debug window
Private Sub Log(ByVal message As String)
    Write("Debug", message)
End Sub