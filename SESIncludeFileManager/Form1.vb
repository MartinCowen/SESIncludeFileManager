Imports System.Collections
Public Class Form1
    Private lstFileNames As New List(Of String)

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

        Dim teststr As String = "../debug;../../86;../../../../../../components/device;../../../../../../components/toolchain/cmsis/include"
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

        Dim d As String = lvPaths.SelectedItems(0).Text

        If chkOnlyShowFilesInSelectedFolder.Checked Then
            'file list is cleared and only shows files from selected folder

            lstFiles.BeginUpdate()
            lstFiles.Items.Clear()

            lstFileNames = GetListOfFilesInFolder(d)

            Dim lFiles As List(Of String) = lstFileNames

            If lFiles IsNot Nothing Then
                lstFiles.Items.AddRange(lFiles.ToArray)
            End If

            Me.lstFiles.EndUpdate()
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
            lstFileNames.Clear()
            lstFiles.BeginUpdate()
            lstFiles.Items.Clear()

            For Each lvi As ListViewItem In lvPaths.Items
                Dim d As String = lvi.Text
                If My.Computer.FileSystem.DirectoryExists(d) Then
                    Dim lfiles As List(Of String)
                    lfiles = GetListOfFilesInFolder(d)
                    If lfiles IsNot Nothing Then
                        lstFileNames.AddRange(lfiles.ToArray)
                    End If
                End If
            Next lvi

            lstFiles.Items.AddRange(lstFileNames.ToArray)


            lstFiles.EndUpdate()
        End If
    End Sub

    Private Sub txtSearchFiles_TextChanged(sender As Object, e As EventArgs) Handles txtSearchFiles.TextChanged
        If txtSearchFiles.Text <> "" Then

            lstFiles.Items.Clear()
            lstFiles.Items.AddRange(FindFileNamesMatchingString(txtSearchFiles.Text, lstFileNames).ToArray)

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
    Private Function colToArray(c As ListBox.ObjectCollection) As ArrayList
        Dim ar As New ArrayList
        ar.AddRange(c)

        'For Each s As String In c
        '    ar.Ad
        'Next c
        'lst.AddRange(c)
        Return ar
    End Function
    'Private Function colToListOfString(c As Collection) As List(Of String)
    '    Dim lst As New List(Of String)
    '    For Each s As String In c
    '        lst.Add()
    '    Next c
    '    lst.AddRange(c)
    'End Function
End Class
