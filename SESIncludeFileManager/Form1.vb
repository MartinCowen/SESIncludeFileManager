﻿Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.IO

Public Class Form1
    Private Class FilesInFolder
        Public filename As String
        Public folder As String
    End Class

    Private lstFilesInFolders As New List(Of FilesInFolder)
    Private bManualSelectFile As Boolean
    Private Const NotFoundMarker As String = "!"
    Private projFileLines() As String 'untrimmed, just each of the orignal lines in an array
    Private lineEndingChar As String = System.Environment.NewLine 'detect on read, use later on write back to file

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSDKFolder.Text = My.Settings.SDKFolder
        txtProjectFolder.Text = My.Settings.SESProjectFolder
        txtProjectFile.Text = My.Settings.SESProjectFile
        Debug.Print(CombinePathWithRelative("\1\2\3", "..\5"))
        Stop

    End Sub

    Private Sub btnReadProject_Click(sender As Object, e As EventArgs) Handles btnReadProject.Click
        ReadAndParseProjectFile(IO.Path.Combine(txtProjectFolder.Text, txtProjectFile.Text))
        RefreshlbFiles()
    End Sub

    Private Sub ReadAndParseProjectFile(filename As String)


        Dim wholeFile As String = ""

        Try
            wholeFile = My.Computer.FileSystem.ReadAllText(filename)
        Catch ex As Exception
            MessageBox.Show("error loading file " & filename & " - " & ex.ToString)
        End Try

        'auto detect line ending
        If wholeFile.Contains(vbCrLf) Then
            lineEndingChar = vbCrLf
        ElseIf wholeFile.Contains(vbLf) Then
            lineEndingChar = vbLf
        ElseIf wholeFile.Contains(vbCr) Then
            lineEndingChar = vbCr
        End If

        projFileLines = wholeFile.Split(lineEndingChar)

        'find the user includes line
        Dim sIncludes As String = ""
        Dim foundincludesline As Boolean = False
        For Each lineStr As String In projFileLines
            If lineStr.Contains("c_user_include_directories") Then
                'remove everything except the value of the attribute
                sIncludes = lineStr.Trim.Replace("c_user_include_directories=", "").Replace("""", "")
                foundincludesline = True
            End If
        Next lineStr

        If Not foundincludesline Then MessageBox.Show("Project file does not include a line with c_user_include_directories")

        '//>sIncludes = "../debug;../;../../../../../../components/device;../../../../../../components/toolchain/cmsis/include"
        Dim lst As Array = sIncludes.Split(";".ToCharArray, StringSplitOptions.RemoveEmptyEntries)

        UpdatePathList(lst)

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UpdatePathList(lst As Array)

        lvPaths.BeginUpdate()
        lvPaths.Items.Clear()

        For Each p As String In lst
            Dim lvi As New ListViewItem

            lvi.Text = p

            If Not DoesDirectoryExist(p) Then
                lvi.SubItems.Add(NotFoundMarker)
            Else
                lvi.SubItems.Add("")
            End If

            lvPaths.Items.Add(lvi)
        Next p

        lvPaths.EndUpdate()
        RefreshCountFolders()
        RefreshlbFiles() 'must also refresh lstFilesInFolders

    End Sub

    ''' <summary>
    ''' Checks for folder existing relative to the reference path
    ''' </summary>
    ''' <param name="p">additional path</param>
    ''' <returns></returns>
    Private Function DoesDirectoryExist(p As String) As Boolean
        Return My.Computer.FileSystem.DirectoryExists(Path.GetFullPath(Path.Combine(txtProjectFolder.Text, p)))
    End Function



    Private Sub lvPaths_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvPaths.SelectedIndexChanged

        If lvPaths.SelectedItems.Count < 1 Then Exit Sub
        If Not bManualSelectFile Then
            RefreshlbFiles()
        End If
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

        lblCountFiles.Text = "Total: " & lbFiles.Items.Count

    End Sub

    Private Sub RefreshCountFolders()
        Dim notfoundcount As Integer = 0
        For Each lvi As ListViewItem In lvPaths.Items
            If lvi.SubItems(1).Text = NotFoundMarker Then notfoundcount += 1
        Next lvi

        lblCountFolders.Text = "Total: " & lvPaths.Items.Count.ToString & " Not Found: " & notfoundcount.ToString
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
        UpdateLbFiles()
    End Sub
    Private Sub txtSearchFiles_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchFiles.KeyDown
        If e.KeyCode = Keys.Enter Then UpdateLbFiles()
    End Sub
    Private Sub UpdateLbFiles()
        If txtSearchFiles.Text <> "" Then

            lbFiles.Items.Clear()
            Dim lstFileNames As New List(Of String)
            For Each fif As FilesInFolder In lstFilesInFolders
                lstFileNames.Add(fif.filename)
            Next fif
            lbFiles.Items.AddRange(FindFileNamesMatchingString(txtSearchFiles.Text, lstFileNames).ToArray)

        End If

        lblCountFiles.Text = "Total: " & lbFiles.Items.Count

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
                bManualSelectFile = True
                For Each lvi As ListViewItem In lvPaths.Items
                    If lvi.Text = d Then

                        lvi.Selected = True

                        lvi.EnsureVisible()
                    Else
                        lvi.Selected = False
                    End If
                Next lvi
                bManualSelectFile = False
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

    Private Sub lvPaths_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvPaths.MouseDoubleClick
        Dim lv As ListView = DirectCast(sender, ListView)

        Dim lvi As ListViewItem = lv.HitTest(e.Location).Item
        If lvi IsNot Nothing Then
            Dim newname As String = InputBox("New Value ",, lvi.Text)
            If newname <> "" Then
                'update internal structures and lists
                lstFileInFolder_NewFolderName(lvi.Text, newname)
                lvi.Text = newname
            End If
        End If
    End Sub

    Private Function lstFileInFolder_NewFolderName(oName As String, nName As String) As Boolean

        For Each fif As FilesInFolder In lstFilesInFolders
            If fif.folder = oName Then
                fif.folder = nName

                UpdatePathList(FiF_FoldersToArray(lstFilesInFolders))
                Return True
            End If
        Next fif
        Return False 'no path found, maybe that path had no files in it.
    End Function

    Private Function FiF_FoldersToArray(lFiF As List(Of FilesInFolder)) As Array
        Dim ar(lFiF.Count) As String
        For i As Integer = 0 To lFiF.Count - 1
            ar(i) = lFiF.Item(i).folder
        Next i
        Return ar
    End Function

    Private Sub btnUpdateProject_Click(sender As Object, e As EventArgs) Handles btnUpdateProject.Click
        WriteProjectFile(IO.Path.Combine(txtProjectFolder.Text, txtProjectFile.Text))
    End Sub

    Private Sub WriteProjectFile(filename As String)

        'make up the includes string
        Dim sIncludes As String = MakeIncludesString()

        'update the line in the array of strings that is the project file

        Dim foundincludesline As Boolean = False
        For i As Integer = 0 To projFileLines.Count - 1
            Dim lineStr As String = projFileLines(i)
            If lineStr.Contains("c_user_include_directories") Then
                'replace the value of the attribute. preserve original text prior to =, add quotes
                projFileLines(i) = lineStr.Split("=")(0) & "=""" & sIncludes & """"
                foundincludesline = True
            End If
        Next i

        If Not foundincludesline Then
            MessageBox.Show("Error - did not find c_user_include_directories to replace!")
            Exit Sub
        End If

        'write to file

        Dim wholeFile As String = ""
        'For Each lineStr As String In projFileLines
        '    wholeFile &= lineStr & lineEndingChar
        'Next lineStr

        For i As Integer = 0 To projFileLines.Count - 2 'eliminate final blank line
            wholeFile &= projFileLines(i) & lineEndingChar
        Next i

        Try
            My.Computer.FileSystem.WriteAllText(filename, wholeFile, False)
        Catch ex As Exception
            MessageBox.Show("Error while writing, " & ex.ToString)
        End Try
    End Sub

    Private Function MakeIncludesString() As String
        Dim s As String = ""
        For Each lvi As ListViewItem In lvPaths.Items
            s &= lvi.Text & ";"
        Next lvi
        Return s
    End Function

    Private Sub lvPaths_MouseDown(sender As Object, e As MouseEventArgs) Handles lvPaths.MouseDown
        If e.Button = MouseButtons.Right Then
            cmsPathPopup.Show(CType(sender, Control), e.Location)
        End If
    End Sub

    Private Sub mnuRemoveFolder_Click(sender As Object, e As EventArgs) Handles mnuRemoveFolder.Click
        Dim lvi As ListViewItem
        If lvPaths.SelectedItems.Count <> 1 Then Exit Sub 'must be exactly one item selected
        lvi = lvPaths.SelectedItems(0)

        If lvi IsNot Nothing Then
            If DeleteFolder(lvi.Text) Then
                UpdatePathList(FiF_FoldersToArray(lstFilesInFolders)) 'problem for folders with no files!
            Else
                lvPaths.Items.Remove(lvi)
                RefreshCountFolders()
                RefreshlbFiles()
            End If
        End If
    End Sub

    Private Function DeleteFolder(s As String) As Boolean
        'first update the internal data structure
        Dim found As Boolean = False


        'delete all fif entries with this folder name
        For i As Integer = lstFilesInFolders.Count - 1 To 0 Step -1
            If lstFilesInFolders(i).folder = s Then
                lstFilesInFolders.RemoveAt(i)
                found = True
            End If
        Next i

        'does not update path list because would be inefficent for cases when called in a loop
        Return found
    End Function

    Private Sub mnuRemoveAllNotFoundFolders_Click(sender As Object, e As EventArgs) Handles mnuRemoveAllNotFoundFolders.Click
        Dim found As Boolean = False
        lvPaths.BeginUpdate()

        For i As Integer = lvPaths.Items.Count - 1 To 0 Step -1
            Dim lvi As ListViewItem = lvPaths.Items(i)

            If lvi.SubItems(1).Text = NotFoundMarker Then
                If DeleteFolder(lvi.Text) Then
                    found = True
                Else
                    lvPaths.Items.Remove(lvi)
                End If
            End If
        Next i
        lvPaths.EndUpdate()

        'TODO Test
        If found Then
            UpdatePathList(FiF_FoldersToArray(lstFilesInFolders)) 'problem for folders with no files!
        Else
            RefreshCountFolders()
            RefreshlbFiles()
        End If

    End Sub

    Private Sub mnuAutoFormatPaths_Click(sender As Object, e As EventArgs) Handles mnuAutoFormatPaths.Click
        Me.Cursor = Cursors.WaitCursor
        AutoFormatPaths(lvPaths.Items)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AutoFormatPaths(ByRef ar As ListView.ListViewItemCollection)
        'takes a few seconds to get all subdirectories
        'Dim dirs As ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetDirectories(txtSDKFolder.Text, FileIO.SearchOption.SearchAllSubDirectories)
        Dim dirs As ReadOnlyCollection(Of String) = My.Computer.FileSystem.GetDirectories(txtSDKFolder.Text, FileIO.SearchOption.SearchTopLevelOnly)

        'Debug.Print(dirs.ToString)

        For i As Integer = 0 To ar.Count - 1

            Dim pathInc As String = Path.GetFullPath(Path.Combine(txtProjectFolder.Text, ar(i).Text.Replace("/", "\"))) 'abs path
            Dim pathProj As String = txtProjectFolder.Text 'abs path

            Dim isProjectInsiderSDK As Boolean = pathInc.Contains(txtSDKFolder.Text)

            If isProjectInsiderSDK Then
                'project is inside sdk, can use rel path
                Dim relpath As String = GetRelativePath(pathProj, pathInc)

                If relpath <> String.Empty Then
                    'write back the corrected version
                    ar(i).Text = relpath.Replace("\", "/")
                End If


            Else
                'project is outside sdk, so have to use abs path

            End If


        Next i


    End Sub
    Private Function GetRelativePath(pathAbsProj As String, pathAbsWithRel As String) As String
        Dim relpath As String = ""
        Dim pathIncStrs() As String = pathAbsWithRel.Split(Path.DirectorySeparatorChar) 'copy that will be chopped up
        Dim pathProjStrs() As String = pathAbsProj.Split(Path.DirectorySeparatorChar)

        'remove dirs until base is common with include file
        For n As Integer = pathProjStrs.Length To 1 Step -1
            Dim commonPath As String = AssembleStringN(pathProjStrs, "\", n)
            Debug.Print(commonPath)
            If pathAbsWithRel.Contains(commonPath) Then
                relpath &= pathAbsWithRel.Replace(commonPath & "\", "")
                Exit For
            End If
            relpath &= "..\"
            'Debug.Print(relpath)
        Next n

        'Debug.Print(relpath)
        Return relpath

    End Function
    ''' <summary>
    ''' Combines paths when relPath can start with "..\" or "." or "\"
    ''' Does not cope with mixture of "..\" and "." in relPath
    ''' </summary>
    ''' <param name="basePath">Base Path</param>
    ''' <param name="relPath">Relative Path</param>
    ''' <returns>Combined Path</returns>
    Private Function CombinePathWithRelative(basePath As String, relPath As String) As String
        If relPath.StartsWith("..\") Then
            'make copies to work with
            Dim bp As String = basePath
            Dim rp As String = relPath
            Do
                bp = bp.Substring(0, bp.LastIndexOf("\"))
                rp = rp.Substring(rp.IndexOf("\") + 1)
            Loop While rp.StartsWith("..\") And bp IsNot String.Empty
            Return Path.Combine(bp, rp)
        ElseIf relPath.StartsWith(".") Then
            Return Path.Combine(Path.GetPathRoot(basePath), relPath.Substring(1))
        Else
            Return Path.Combine(basePath, relPath)
        End If

    End Function

    ''' <summary>
    ''' Assembles a string from an array and a seperator character up to n sections
    ''' </summary>
    ''' <param name="s">Array of sections</param>
    ''' <param name="sep">Seperator Character</param>
    ''' <param name="n">Number of sections you want</param>
    ''' <returns>Assembled string</returns>
    Private Function AssembleStringN(s() As String, sep As String, n As Integer) As String
        If n > s.Length Then Return ""

        Dim r As String = ""

        For i As Integer = 1 To n
            r &= s(i - 1) & sep
        Next i
        Return r.Remove(r.Length - 1) 'remove the final separator
    End Function
End Class
