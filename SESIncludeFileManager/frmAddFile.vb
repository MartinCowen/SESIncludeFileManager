Imports System.Collections.ObjectModel
Imports System.IO

Public Class frmAddFile
    Private WithEvents tmrStartup As New Timer
    Private dirs As ReadOnlyCollection(Of String)
    Private files As ReadOnlyCollection(Of String)
    Private lstAllFilesInFolders As New List(Of Form1.FilesInFolder)
    Private lstFilteredFilesInFolders As New List(Of Form1.FilesInFolder)


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
        ProgressBar1.Visible = True
        dirs = My.Computer.FileSystem.GetDirectories(Form1.SDKFolder, FileIO.SearchOption.SearchAllSubDirectories)

        GetAllFiles()

        txtSearchFor.Text = Form1.FileSearchFor

        Me.Cursor = Cursors.Default
    End Sub
    Private Sub UpdateCmbFilteredFiles()
        Me.Cursor = Cursors.WaitCursor
        cmbFiles.BeginUpdate()
        cmbFiles.Items.Clear()
        For Each fif As Form1.FilesInFolder In lstFilteredFilesInFolders
            cmbFiles.Items.Add(fif.filename)
        Next fif
        cmbFiles.EndUpdate()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetAllFiles()
        ProgressBar1.Maximum = dirs.Count - 1
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
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
            lblItemProgress.Text = "Listing " & dirs(i).Replace(Form1.SDKFolder, "")
            lblItemProgress.Refresh()
        Next i

        lblItemProgress.Text = "Listing Complete"
        lblItemProgress.Refresh()
        ProgressBar1.Visible = False

    End Sub

    Private Sub txtSearchFor_TextChanged(sender As Object, e As EventArgs) Handles txtSearchFor.TextChanged

        'make a filtered fif list
        lstFilteredFilesInFolders.Clear()
        For Each fif As Form1.FilesInFolder In lstAllFilesInFolders
            If fif.filename.Contains(txtSearchFor.Text) Then
                lstFilteredFilesInFolders.Add(fif)
            End If
        Next fif

        UpdateCmbFilteredFiles()
        cmbFiles.Text = ""
    End Sub

    Private Sub cmbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFiles.SelectedIndexChanged
        If cmbFiles.SelectedIndex < 0 Then
            btnAddFile.Enabled = False
            Exit Sub
        End If

        lblFileFolder.Text = lstFilteredFilesInFolders(cmbFiles.SelectedIndex).folder.Replace(Form1.SDKFolder, "")
        btnAddFile.Enabled = True
    End Sub
End Class