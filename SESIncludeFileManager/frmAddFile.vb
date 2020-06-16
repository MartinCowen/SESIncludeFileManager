Imports System.Collections.ObjectModel
Imports System.IO

Public Class frmAddFile
    Private WithEvents tmrStartup As New Timer
    Private dirs As ReadOnlyCollection(Of String)
    Private files As ReadOnlyCollection(Of String)
    Private lstAllFilesInFolders As New List(Of Form1.FilesInFolder)

    Private Sub frmAddFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'allow form to be painted before starting CPU intensive task
        tmrStartup.Interval = 1
        tmrStartup.Start()


    End Sub

    Private Sub tmrStartup_Tick(sender As Object, e As EventArgs) Handles tmrStartup.Tick
        tmrStartup.Stop()

        Me.Cursor = Cursors.WaitCursor
        lblItemProgress.Text = "Finding Folders in " & Form1.SDKFolder
        lblItemProgress.Refresh()
        dirs = My.Computer.FileSystem.GetDirectories(Form1.SDKFolder, FileIO.SearchOption.SearchAllSubDirectories)
        GetAllFiles()

        txtSearchFor.Text = Form1.FileSearchFor

        Me.Cursor = Cursors.Default
    End Sub
    Private Sub UpdateCmbFilesMatch(s As String)
        Me.Cursor = Cursors.WaitCursor
        cmbFiles.BeginUpdate()
        cmbFiles.Items.Clear()
        For Each fif As Form1.FilesInFolder In lstAllFilesInFolders
            If fif.filename.Contains(txtSearchFor.Text) Then
                cmbFiles.Items.Add(fif.filename)
            End If
        Next fif
        cmbFiles.EndUpdate()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetAllFiles()
        ProgressBar1.Maximum = dirs.Count - 1
        ProgressBar1.Value = 0
        lstAllFilesInFolders.Clear()

        For i As Integer = 0 To dirs.Count - 1

            If dirs(i).Contains("\examples\") Then Continue For 'skip the examples

            Dim filesInThisFolder As ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetFiles(dirs(i), FileIO.SearchOption.SearchTopLevelOnly, "*.h")

            For Each f As String In filesInThisFolder

                Dim fif As New Form1.FilesInFolder
                fif.folder = dirs(i).ToString
                fif.filename = My.Computer.FileSystem.GetFileInfo(f).Name
                lstAllFilesInFolders.Add(fif)

            Next f
            ProgressBar1.Value = i
            lblItemProgress.Text = "Listing " & dirs(i).ToString
            lblItemProgress.Refresh()
        Next i

    End Sub

    Private Sub txtSearchFor_TextChanged(sender As Object, e As EventArgs) Handles txtSearchFor.TextChanged
        UpdateCmbFilesMatch(txtSearchFor.Text)
    End Sub
End Class