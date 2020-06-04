Imports System.Collections
Public Class Form1


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

        Dim teststr As String = "../debug;../../../../../../components/device;../../../../../../components/toolchain/cmsis/include"
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

        Dim f As String = lvPaths.SelectedItems(0).Text

        If chkOnlyShowFilesInSelectedFolder.Checked Then
            'file list is cleared and only shows files from selected folder

            Me.lstFiles.BeginUpdate()
            Me.lstFiles.Items.Clear()

            Dim lstFiles As List(Of String) = GetListOfFilesInFolder(f)

            If lstFiles IsNot Nothing Then
                Me.lstFiles.Items.AddRange(lstFiles.ToArray)
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
End Class
