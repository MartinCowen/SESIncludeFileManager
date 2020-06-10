<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSDKFolder = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProjectFolder = New System.Windows.Forms.TextBox()
        Me.btnBrowseProject = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbFiles = New System.Windows.Forms.ListBox()
        Me.txtSearchFiles = New System.Windows.Forms.TextBox()
        Me.btnReadProject = New System.Windows.Forms.Button()
        Me.btnUpdateProject = New System.Windows.Forms.Button()
        Me.txtProjectFile = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lvPaths = New System.Windows.Forms.ListView()
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFound = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chkOnlyShowFilesInSelectedFolder = New System.Windows.Forms.CheckBox()
        Me.btnBrowseSDK = New System.Windows.Forms.Button()
        Me.lblCountFolders = New System.Windows.Forms.Label()
        Me.lblCountFiles = New System.Windows.Forms.Label()
        Me.cmsPathPopup = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRemoveFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveAllNotFoundFolders = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoFormatPaths = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsPathPopup.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(34, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SDK Folder"
        '
        'txtSDKFolder
        '
        Me.txtSDKFolder.Location = New System.Drawing.Point(107, 6)
        Me.txtSDKFolder.Name = "txtSDKFolder"
        Me.txtSDKFolder.Size = New System.Drawing.Size(391, 20)
        Me.txtSDKFolder.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "SES Project Folder"
        '
        'txtProjectFolder
        '
        Me.txtProjectFolder.Location = New System.Drawing.Point(107, 36)
        Me.txtProjectFolder.Name = "txtProjectFolder"
        Me.txtProjectFolder.Size = New System.Drawing.Size(664, 20)
        Me.txtProjectFolder.TabIndex = 3
        '
        'btnBrowseProject
        '
        Me.btnBrowseProject.Location = New System.Drawing.Point(696, 64)
        Me.btnBrowseProject.Name = "btnBrowseProject"
        Me.btnBrowseProject.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseProject.TabIndex = 4
        Me.btnBrowseProject.Text = "Browse"
        Me.btnBrowseProject.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Include Paths"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(339, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Include Files"
        '
        'lbFiles
        '
        Me.lbFiles.FormattingEnabled = True
        Me.lbFiles.Location = New System.Drawing.Point(342, 149)
        Me.lbFiles.Name = "lbFiles"
        Me.lbFiles.Size = New System.Drawing.Size(279, 316)
        Me.lbFiles.Sorted = True
        Me.lbFiles.TabIndex = 8
        '
        'txtSearchFiles
        '
        Me.txtSearchFiles.Location = New System.Drawing.Point(342, 123)
        Me.txtSearchFiles.Name = "txtSearchFiles"
        Me.txtSearchFiles.Size = New System.Drawing.Size(279, 20)
        Me.txtSearchFiles.TabIndex = 9
        '
        'btnReadProject
        '
        Me.btnReadProject.Location = New System.Drawing.Point(683, 191)
        Me.btnReadProject.Name = "btnReadProject"
        Me.btnReadProject.Size = New System.Drawing.Size(88, 25)
        Me.btnReadProject.TabIndex = 10
        Me.btnReadProject.Text = "Read Project"
        Me.btnReadProject.UseVisualStyleBackColor = True
        '
        'btnUpdateProject
        '
        Me.btnUpdateProject.Location = New System.Drawing.Point(683, 244)
        Me.btnUpdateProject.Name = "btnUpdateProject"
        Me.btnUpdateProject.Size = New System.Drawing.Size(88, 25)
        Me.btnUpdateProject.TabIndex = 11
        Me.btnUpdateProject.Text = "Update Project"
        Me.btnUpdateProject.UseVisualStyleBackColor = True
        '
        'txtProjectFile
        '
        Me.txtProjectFile.Location = New System.Drawing.Point(107, 72)
        Me.txtProjectFile.Name = "txtProjectFile"
        Me.txtProjectFile.Size = New System.Drawing.Size(391, 20)
        Me.txtProjectFile.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "SES Project File"
        '
        'lvPaths
        '
        Me.lvPaths.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPath, Me.colFound})
        Me.lvPaths.FullRowSelect = True
        Me.lvPaths.HideSelection = False
        Me.lvPaths.Location = New System.Drawing.Point(19, 149)
        Me.lvPaths.Name = "lvPaths"
        Me.lvPaths.Size = New System.Drawing.Size(317, 316)
        Me.lvPaths.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvPaths.TabIndex = 14
        Me.lvPaths.UseCompatibleStateImageBehavior = False
        Me.lvPaths.View = System.Windows.Forms.View.Details
        '
        'colPath
        '
        Me.colPath.DisplayIndex = 1
        Me.colPath.Text = "Path"
        Me.colPath.Width = 300
        '
        'colFound
        '
        Me.colFound.DisplayIndex = 0
        Me.colFound.Text = ""
        Me.colFound.Width = 18
        '
        'chkOnlyShowFilesInSelectedFolder
        '
        Me.chkOnlyShowFilesInSelectedFolder.AutoSize = True
        Me.chkOnlyShowFilesInSelectedFolder.Location = New System.Drawing.Point(143, 123)
        Me.chkOnlyShowFilesInSelectedFolder.Name = "chkOnlyShowFilesInSelectedFolder"
        Me.chkOnlyShowFilesInSelectedFolder.Size = New System.Drawing.Size(190, 17)
        Me.chkOnlyShowFilesInSelectedFolder.TabIndex = 15
        Me.chkOnlyShowFilesInSelectedFolder.Text = "Only Show Files In Selected Folder"
        Me.chkOnlyShowFilesInSelectedFolder.UseVisualStyleBackColor = True
        '
        'btnBrowseSDK
        '
        Me.btnBrowseSDK.Location = New System.Drawing.Point(521, 4)
        Me.btnBrowseSDK.Name = "btnBrowseSDK"
        Me.btnBrowseSDK.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseSDK.TabIndex = 16
        Me.btnBrowseSDK.Text = "Browse"
        Me.btnBrowseSDK.UseVisualStyleBackColor = True
        '
        'lblCountFolders
        '
        Me.lblCountFolders.AutoSize = True
        Me.lblCountFolders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCountFolders.Location = New System.Drawing.Point(19, 474)
        Me.lblCountFolders.Name = "lblCountFolders"
        Me.lblCountFolders.Size = New System.Drawing.Size(110, 15)
        Me.lblCountFolders.TabIndex = 17
        Me.lblCountFolders.Text = "Total: _ Not Found: _"
        '
        'lblCountFiles
        '
        Me.lblCountFiles.AutoSize = True
        Me.lblCountFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCountFiles.Location = New System.Drawing.Point(342, 474)
        Me.lblCountFiles.Name = "lblCountFiles"
        Me.lblCountFiles.Size = New System.Drawing.Size(45, 15)
        Me.lblCountFiles.TabIndex = 18
        Me.lblCountFiles.Text = "Total: _"
        '
        'cmsPathPopup
        '
        Me.cmsPathPopup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRemoveFolder, Me.mnuRemoveAllNotFoundFolders, Me.mnuAutoFormatPaths})
        Me.cmsPathPopup.Name = "cmsPathPopup"
        Me.cmsPathPopup.Size = New System.Drawing.Size(236, 92)
        '
        'mnuRemoveFolder
        '
        Me.mnuRemoveFolder.Name = "mnuRemoveFolder"
        Me.mnuRemoveFolder.Size = New System.Drawing.Size(235, 22)
        Me.mnuRemoveFolder.Text = "Remove Folder"
        '
        'mnuRemoveAllNotFoundFolders
        '
        Me.mnuRemoveAllNotFoundFolders.Name = "mnuRemoveAllNotFoundFolders"
        Me.mnuRemoveAllNotFoundFolders.Size = New System.Drawing.Size(235, 22)
        Me.mnuRemoveAllNotFoundFolders.Text = "Remove All Not Found Folders"
        '
        'mnuAutoFormatPaths
        '
        Me.mnuAutoFormatPaths.Name = "mnuAutoFormatPaths"
        Me.mnuAutoFormatPaths.Size = New System.Drawing.Size(235, 22)
        Me.mnuAutoFormatPaths.Text = "Auto Format Paths"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 498)
        Me.Controls.Add(Me.lblCountFiles)
        Me.Controls.Add(Me.lblCountFolders)
        Me.Controls.Add(Me.btnBrowseSDK)
        Me.Controls.Add(Me.chkOnlyShowFilesInSelectedFolder)
        Me.Controls.Add(Me.lvPaths)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtProjectFile)
        Me.Controls.Add(Me.btnUpdateProject)
        Me.Controls.Add(Me.btnReadProject)
        Me.Controls.Add(Me.txtSearchFiles)
        Me.Controls.Add(Me.lbFiles)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnBrowseProject)
        Me.Controls.Add(Me.txtProjectFolder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSDKFolder)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "SES Include File Manager"
        Me.cmsPathPopup.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtSDKFolder As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtProjectFolder As TextBox
    Friend WithEvents btnBrowseProject As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbFiles As ListBox
    Friend WithEvents txtSearchFiles As TextBox
    Friend WithEvents btnReadProject As Button
    Friend WithEvents btnUpdateProject As Button
    Friend WithEvents txtProjectFile As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lvPaths As ListView
    Friend WithEvents colFound As ColumnHeader
    Friend WithEvents colPath As ColumnHeader
    Friend WithEvents chkOnlyShowFilesInSelectedFolder As CheckBox
    Friend WithEvents btnBrowseSDK As Button
    Friend WithEvents lblCountFolders As Label
    Friend WithEvents lblCountFiles As Label
    Friend WithEvents cmsPathPopup As ContextMenuStrip
    Friend WithEvents mnuRemoveFolder As ToolStripMenuItem
    Friend WithEvents mnuRemoveAllNotFoundFolders As ToolStripMenuItem
    Friend WithEvents mnuAutoFormatPaths As ToolStripMenuItem
End Class
