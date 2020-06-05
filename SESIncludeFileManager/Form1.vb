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
    End Sub

    Private Sub ReadAndParseProjectFile(f As String)
        'simple string search version
        'Dim s As String = My.Computer.FileSystem.ReadAllText(f)

        Dim teststr As String = "../debug;../;../../../../../../components/device;../../../../../../components/toolchain/cmsis/include"
        Dim lst As Array = teststr.Split(";")

        UpdatePathList(lst)

    End Sub

    Private Sub UpdatePathList(lst As Array)

        lvPaths.BeginUpdate()
        lvPaths.Items.Clear()

        For Each p As String In lst
            Dim lvi As New ListViewItem

            lvi.Text = p

            If Not My.Computer.FileSystem.DirectoryExists(p) Then
                lvi.SubItems.Add("!")
            End If

            lvPaths.Items.Add(lvi)
        Next p

        lvPaths.EndUpdate()
    End Sub

    Private Sub lvPaths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvPaths.SelectedIndexChanged

        If lvPaths.SelectedItems.Count < 1 Then Exit Sub


        RefreshlbFiles()
    End Sub

    Private Sub RefreshlbFiles()
        If lvPaths.SelectedItems.Count < 1 Then Exit Sub
        Dim d As String = lvPaths.SelectedItems(0).Text

        If chkOnlyShowFilesInSelectedFolder.Checked Then
            'file list is cleared and only shows files from selected folder

            lbFiles.BeginUpdate()
            lbFiles.Items.Clear()
            lstFilesInFolders.Clear()

            For Each s As String In GetListOfFilesInFolder(d)
                Dim fif As New FilesInFolder
                fif.filename = s
                fif.folder = d
                lstFilesInFolders.Add(fif)
                lbFiles.Items.Add(fif.filename)
            Next s

            Me.lbFiles.EndUpdate()
        End If
    End Sub

    Private Function GetListOfFilesInFolder(folder As String) As List(Of String)
        Dim res As Array = Nothing
        Dim lstFiles As New List(Of String)
        If My.Computer.FileSystem.DirectoryExists(folder) Then
            res = My.Computer.FileSystem.GetFiles(folder, FileIO.SearchOption.SearchTopLevelOnly, "*.h").ToArray
            For Each n As String In res
                lstFiles.Add(My.Computer.FileSystem.GetName(n))
            Next n
        End If
        Return lstFiles
    End Function

    Private Sub chkOnlyShowFilesInSelectedFolder_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyShowFilesInSelectedFolder.CheckedChanged
        If chkOnlyShowFilesInSelectedFolder.Checked = False Then
            'get list of all files in all folders
            lstFilesInFolders.Clear()
            lbFiles.BeginUpdate()
            lbFiles.Items.Clear()

            For Each lvi As ListViewItem In lvPaths.Items
                Dim d As String = lvi.Text
                If My.Computer.FileSystem.DirectoryExists(d) Then
                    For Each s As String In GetListOfFilesInFolder(d)
                        Dim fif As New FilesInFolder
                        fif.filename = s
                        fif.folder = d
                        lstFilesInFolders.Add(fif)
                        lbFiles.Items.Add(fif.filename)
                    Next s
                End If
            Next lvi

            lbFiles.EndUpdate()
        Else 'chk is true
            'refresh showing only files in folder
            RefreshlbFiles()

        End If
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

End Class
