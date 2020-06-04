<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSDKFolder = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProjectFolder = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstPaths = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lstFiles = New System.Windows.Forms.ListBox()
        Me.txtSearchFiles = New System.Windows.Forms.TextBox()
        Me.btnReadProject = New System.Windows.Forms.Button()
        Me.btnUpdateProject = New System.Windows.Forms.Button()
        Me.txtProjectFile = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
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
        Me.txtProjectFolder.Size = New System.Drawing.Size(559, 20)
        Me.txtProjectFolder.TabIndex = 3
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(591, 69)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Include Paths"
        '
        'lstPaths
        '
        Me.lstPaths.FormattingEnabled = True
        Me.lstPaths.Location = New System.Drawing.Point(19, 117)
        Me.lstPaths.Name = "lstPaths"
        Me.lstPaths.Size = New System.Drawing.Size(223, 342)
        Me.lstPaths.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(272, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Include Files"
        '
        'lstFiles
        '
        Me.lstFiles.FormattingEnabled = True
        Me.lstFiles.Location = New System.Drawing.Point(274, 137)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.Size = New System.Drawing.Size(238, 316)
        Me.lstFiles.TabIndex = 8
        '
        'txtSearchFiles
        '
        Me.txtSearchFiles.Location = New System.Drawing.Point(275, 111)
        Me.txtSearchFiles.Name = "txtSearchFiles"
        Me.txtSearchFiles.Size = New System.Drawing.Size(237, 20)
        Me.txtSearchFiles.TabIndex = 9
        '
        'btnReadProject
        '
        Me.btnReadProject.Location = New System.Drawing.Point(578, 137)
        Me.btnReadProject.Name = "btnReadProject"
        Me.btnReadProject.Size = New System.Drawing.Size(88, 25)
        Me.btnReadProject.TabIndex = 10
        Me.btnReadProject.Text = "Read Project"
        Me.btnReadProject.UseVisualStyleBackColor = True
        '
        'btnUpdateProject
        '
        Me.btnUpdateProject.Location = New System.Drawing.Point(578, 187)
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 476)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtProjectFile)
        Me.Controls.Add(Me.btnUpdateProject)
        Me.Controls.Add(Me.btnReadProject)
        Me.Controls.Add(Me.txtSearchFiles)
        Me.Controls.Add(Me.lstFiles)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lstPaths)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtProjectFolder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSDKFolder)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "SES Include File Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtSDKFolder As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtProjectFolder As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lstPaths As ListBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lstFiles As ListBox
    Friend WithEvents txtSearchFiles As TextBox
    Friend WithEvents btnReadProject As Button
    Friend WithEvents btnUpdateProject As Button
    Friend WithEvents txtProjectFile As TextBox
    Friend WithEvents Label5 As Label
End Class
