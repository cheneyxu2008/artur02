Imports System
Imports EnvDTE80
Imports System.Diagnostics


Public Module AttachToWebServer

    'Attach to IIS web server
    Public Sub AttachToWebServer()

        Dim AspNetWp As String = "aspnet_wp.exe"
        Dim W3WP As String = "w3wp.exe"

        If Not (AttachToProcess(AspNetWp)) Then
            If Not AttachToProcess(W3WP) Then
                System.Windows.Forms.MessageBox.Show(String.Format("Process {0} or {1} Cannot Be Found", AspNetWp, W3WP), "Attach To Web Server Macro")
            End If
        End If
    End Sub

    'Attach to Internet Explorer for script debugging
    Public Sub AttachToIE()
        Dim IE As String = "iexplore.exe"

        If Not AttachToProcess(IE, "Script") Then
            System.Windows.Forms.MessageBox.Show(String.Format("Process {0} Cannot Be Found", IE), "Attach To IE Macro")
        End If
    End Sub

    'Attach to a process by name
    Public Function AttachToProcess(ByVal ProcessName As String) As Boolean
        AttachToProcess(ProcessName, "Managed")
    End Function

    'Attach to process by name, engine
    Public Function AttachToProcess(ByVal ProcessName As String, ByVal EngineName As String) As Boolean

        Dim dbg2 As EnvDTE80.Debugger2 = DTE.Debugger
        Dim trans As EnvDTE80.Transport = dbg2.Transports.Item("Default")


        Dim Processes As EnvDTE.Processes = dbg2.GetProcesses(trans, "")
        Dim Process As EnvDTE80.Process2
        Dim dbgeng(1) As EnvDTE80.Engine
        dbgeng(0) = trans.Engines.Item(EngineName)
        Dim ProcessFound As Boolean = False


        For Each Process In Processes
            If (Process.Name.Substring(Process.Name.LastIndexOf("\") + 1) = ProcessName) Then
                Process.Attach2(dbgeng)
                ProcessFound = True
            End If
        Next

        AttachToProcess = ProcessFound
    End Function

End Module

