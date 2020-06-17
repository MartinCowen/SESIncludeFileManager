Imports System.Collections.ObjectModel


Public Class frmAddFolder
    Private WithEvents tmrStartup As New Timer
    Private dirs As ReadOnlyCollection(Of String)
    Private files As ReadOnlyCollection(Of String)
    Private lstAllFilesInFolders As New List(Of Form1.FilesInFolder)
    Private lstFilteredFilesInFolders As New List(Of Form1.FilesInFolder)


    Private Sub frmAddFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'allow form to be painted before starting CPU intensive task
        tmrStartup.Interval = 20
        tmrStartup.Start()
        chkAsRelPath.Checked = My.Settings.AddFolderAsRelativePath

    End Sub

    Private Sub StartUp()

        lblItemProgress.Text = "Wait... Finding Folders in " & Form1.SDKFolder
        lblItemProgress.Refresh()
        ProgressBar1.Visible = True

        Cursor = Cursors.WaitCursor
        'this line take several seconds
        dirs = My.Computer.FileSystem.GetDirectories(Form1.SDKFolder, FileIO.SearchOption.SearchAllSubDirectories)
        Cursor = Cursors.Default

        GetAllFiles()

        txtSearchFor.Text = Form1.FileSearchFor

    End Sub

    Private Sub tmrStartup_Tick(sender As Object, e As EventArgs) Handles tmrStartup.Tick
        tmrStartup.Stop()
        StartUp()
    End Sub
    Private Sub UpdateCmbFilteredFiles()

        cmbFiles.BeginUpdate()
        cmbFiles.Items.Clear()
        For Each fif As Form1.FilesInFolder In lstFilteredFilesInFolders
            cmbFiles.Items.Add(fif.filename)
        Next fif
        cmbFiles.EndUpdate()

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
        lblItemProgress.Text = "Files Matching : " & lstFilteredFilesInFolders.Count
    End Sub

    Private Sub cmbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFiles.SelectedIndexChanged
        If cmbFiles.SelectedIndex < 0 Then
            btnAddFolder.Enabled = False
            Exit Sub
        End If

        lblFileFolder.Text = lstFilteredFilesInFolders(cmbFiles.SelectedIndex).folder.Replace(Form1.SDKFolder, "")
        btnAddFolder.Enabled = True
    End Sub

    Private Sub btnAddFolder_Click(sender As Object, e As EventArgs) Handles btnAddFolder.Click
        Form1.AddFolder(lstFilteredFilesInFolders(cmbFiles.SelectedIndex).folder, chkAsRelPath.Checked) 'absolute path but with option to convert to relative
    End Sub

    Private Sub chkAsRelPath_CheckedChanged(sender As Object, e As EventArgs) Handles chkAsRelPath.CheckedChanged
        My.Settings.AddFolderAsRelativePath = chkAsRelPath.Checked
    End Sub
End Class