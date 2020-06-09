﻿Imports System.Collections
Public Class Form1
    Private Class FilesInFolder
        Public filename As String
        Public folder As String
    End Class

    Private lstFilesInFolders As New List(Of FilesInFolder)
    Private bManualSelectFile As Boolean

    Private xmlDoc As XmlDocument = New XmlDocument()

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

        Try
            'preserveWhitespace has to be true before load, 'https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument.preservewhitespace?view=netcore-3.1

            xmlDoc.PreserveWhitespace = True
            xmlDoc.XmlResolver = Nothing
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


        Me.Cursor = Cursors.WaitCursor

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
                lvi.SubItems.Add("!")
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
        Return My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.CombinePath(txtProjectFolder.Text, p))
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
            If lvi.SubItems(1).Text = "!" Then notfoundcount += 1
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

        'update the xml node value
        xmlDoc.SelectSingleNode("solution/project/configuration[@c_user_include_directories]").Attributes("c_user_include_directories").Value = sIncludes

        'write to file
        Dim xs As XmlWriterSettings = New XmlWriterSettings()
        xs.Indent = True
        xs.NewLineOnAttributes = True
        xs.OmitXmlDeclaration = True

        Using w As XmlWriter = XmlWriter.Create(filename, xs)

            Dim n As XmlDocument = New XmlDocument()
            n = xmlDoc.Clone()

            Dim parent As XmlNode = n.DocumentType.ParentNode
            '4th param in CreateDocumentType has to be Nothing to avoid getting [] in DOCTYPE, which is what you get by default and with empty string - see refs:
            'https://stackoverflow.com/questions/284394/net-xmldocument-why-doctype-changes-after-save
            'https://www.vistax64.com/threads/xmldocument-save-with-null-xmlresolver-modifies-doctype-tag.215921/
            'https://stackoverflow.com/questions/12358061/c-sharp-linq-to-xml-remove-characters-from-the-dtd-header/16451790#16451790
            parent.ReplaceChild(n.CreateDocumentType("CrossStudio_Project_File", Nothing, Nothing, Nothing), n.DocumentType)

            Try
                n.Save(w)
            Catch ex As Exception
                MessageBox.Show("Error while writing, " & ex.ToString)
            End Try

            w.Flush()
            w.Close()

        End Using

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
End Class
