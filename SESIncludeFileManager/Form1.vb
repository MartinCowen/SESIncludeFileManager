Imports System.Collections
Public Class Form1
    Private Class FilesInFolder
        Public filename As String
        Public folder As String
    End Class

    Private lstFilesInFolders As New List(Of FilesInFolder)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSDKFolder.Text = My.Settings.SDKFolder
        txtProjectFolder.Text = My.Settings.SESProjectFolder
        txtProjectFile.Text = My.Settings.SESProjectFile


    End Sub

    Private Sub btnReadProject_Click(sender As Object, e As EventArgs) Handles btnReadProject.Click
        ReadAndParseProjectFile(IO.Path.Combine(txtProjectFolder.Text, txtProjectFile.Text))
        RefreshlbFiles()
    End Sub

    Private Sub ReadAndParseProjectFile(filename As String)

        Dim xmlDoc As XmlDocument = New XmlDocument()

        Try
            xmlDoc.Load(filename)
        Catch ex As Exception
            MessageBox.Show("error loading file " & filename & " as XML " & ex.ToString)
        End Try

        Dim sIncludes As String

        Try
            sIncludes = xmlDoc.SelectSingleNode("solution/project/configuration[@c_user_include_directories]").Attributes("c_user_include_directories").Value
        Catch ex As Exception
            MessageBox.Show("Format of project file is unexpected")
            Exit Sub
        End Try

        ' Dim teststr As String = "../debug;../;../../../../../../components/device;../../../../../../components/toolchain/cmsis/include"
        Dim lst As Array = sIncludes.Split(";")

        UpdatePathList(lst)

    End Sub

    Private Sub UpdatePathList(lst As Array)

        lvPaths.BeginUpdate()
        lvPaths.Items.Clear()

        For Each p As String In lst
            Dim lvi As New ListViewItem

            lvi.Text = p

            If Not DoesDirectoryExist(p) Then
                lvi.SubItems.Add("!")
            End If

            lvPaths.Items.Add(lvi)
        Next p

        lvPaths.EndUpdate()
    End Sub

    ''' <summary>
    ''' Checks for folder existing relative to the reference path
    ''' </summary>
    ''' <param name="p">additional path</param>
    ''' <returns></returns>
    Private Function DoesDirectoryExist(p As String) As Boolean
        Return My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CombinePath(txtProjectFolder.Text, p))
    End Function



    Private Sub lvPaths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvPaths.SelectedIndexChanged

        If lvPaths.SelectedItems.Count < 1 Then Exit Sub

        RefreshlbFiles()
    End Sub

    Private Sub RefreshlbFiles()


        lbFiles.BeginUpdate()
        lbFiles.Items.Clear()
        lstFilesInFolders.Clear()

        If chkOnlyShowFilesInSelectedFolder.Checked AndAlso lvPaths.SelectedItems.Count > 0 Then
            'file list is cleared and only shows files from selected folder
            Dim d As String = lvPaths.SelectedItems(0).Text

            For Each s As String In GetListOfFilesInFolder(d)
                Dim fif As New FilesInFolder
                fif.filename = s
                fif.folder = d
                lstFilesInFolders.Add(fif)
                lbFiles.Items.Add(fif.filename)
            Next s

        Else 'unchecked
            'get list of all files in all folders

            For Each lvi As ListViewItem In lvPaths.Items
                Dim d As String = lvi.Text
                If DoesDirectoryExist(d) Then
                    For Each s As String In GetListOfFilesInFolder(d)
                        Dim fif As New FilesInFolder
                        fif.filename = s
                        fif.folder = d
                        lstFilesInFolders.Add(fif)
                        lbFiles.Items.Add(fif.filename)
                    Next s
                End If
            Next lvi

        End If

        lbFiles.EndUpdate()
    End Sub

    Private Function GetListOfFilesInFolder(folder As String) As List(Of String)
        Dim res As Array = Nothing
        Dim lstFiles As New List(Of String)
        If DoesDirectoryExist(folder) Then
            res = My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CombinePath(txtProjectFolder.Text, folder), FileIO.SearchOption.SearchTopLevelOnly, "*.h").ToArray
            For Each n As String In res
                lstFiles.Add(My.Computer.FileSystem.GetName(n))
            Next n
        End If
        Return lstFiles
    End Function

    Private Sub chkOnlyShowFilesInSelectedFolder_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyShowFilesInSelectedFolder.CheckedChanged

        RefreshlbFiles()

    End Sub

    Private Sub txtSearchFiles_TextChanged(sender As Object, e As EventArgs) Handles txtSearchFiles.TextChanged
        If txtSearchFiles.Text <> "" Then

            lbFiles.Items.Clear()
            Dim lstFileNames As New List(Of String)
            For Each fif As FilesInFolder In lstFilesInFolders
                lstFileNames.Add(fif.filename)
            Next fif
            lbFiles.Items.AddRange(FindFileNamesMatchingString(txtSearchFiles.Text, lstFileNames).ToArray)

        End If
    End Sub
    Private Function FindFileNamesMatchingString(s As String, lstOriginal As List(Of String)) As List(Of String)
        Dim lstFiles As New List(Of String)

        For Each n As String In lstOriginal
            If n.Contains(s) Then
                lstFiles.Add(n)
            End If
        Next n
        Return lstFiles
    End Function


    Private Sub lstFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbFiles.SelectedIndexChanged
        If chkOnlyShowFilesInSelectedFolder.Checked = False Then
            Dim d As String = FindFolderForFile(lbFiles.Items(lbFiles.SelectedIndex).ToString)
            If d <> "" Then
                'highlight folder
                For Each lvi As ListViewItem In lvPaths.Items
                    If lvi.Text = d Then
                        lvi.Selected = True
                    Else
                        lvi.Selected = False
                    End If
                Next lvi
            Else
                'not found
            End If
        End If
    End Sub
    Private Function FindFolderForFile(f As String) As String

        'reverse lookup, find the folder for this file
        For Each fif As FilesInFolder In lstFilesInFolders
            If fif.filename = f Then Return fif.folder
        Next fif
        Return "" 'not found
    End Function

    Private Sub btnBrowseSDK_Click(sender As Object, e As EventArgs) Handles btnBrowseSDK.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        'select a folder not a file, see https://stackoverflow.com/q/31059

        Try
            openFileDialog.Title = "SDK Root Folder"
            openFileDialog.Filter = "folder|*.folders"
            openFileDialog.FileName = "Folder Selection"
            openFileDialog.ValidateNames = False
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = False
            openFileDialog.InitialDirectory = txtSDKFolder.Text
            Dim dialogResult As DialogResult = openFileDialog.ShowDialog
            If dialogResult = DialogResult.OK Then
                txtSDKFolder.Text = My.Computer.FileSystem.GetParentPath(openFileDialog.FileName)
                My.Settings.SDKFolder = txtSDKFolder.Text
            End If

        Catch ex As Exception
            MessageBox.Show("File error: " & ex.ToString)

        End Try
    End Sub

    Private Sub btnBrowseProject_Click(sender As Object, e As EventArgs) Handles btnBrowseProject.Click
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        'selects folder and file for different text boxes

        Try
            openFileDialog.Title = "SES Project Folder"
            openFileDialog.Filter = "SES Project|*.emProject"
            openFileDialog.FileName = txtProjectFile.Text
            openFileDialog.ValidateNames = False
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = False
            openFileDialog.InitialDirectory = txtProjectFolder.Text
            Dim dialogResult As DialogResult = openFileDialog.ShowDialog
            If dialogResult = DialogResult.OK Then
                txtProjectFolder.Text = My.Computer.FileSystem.GetParentPath(openFileDialog.FileName)
                txtProjectFile.Text = My.Computer.FileSystem.GetName(openFileDialog.FileName)

                My.Settings.SESProjectFolder = txtProjectFolder.Text
                My.Settings.SESProjectFile = txtProjectFile.Text
            End If

        Catch ex As Exception
            MessageBox.Show("File error: " & ex.ToString)

        End Try
    End Sub



End Class
