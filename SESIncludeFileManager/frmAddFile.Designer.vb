<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddFile
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblItemProgress = New System.Windows.Forms.Label()
        Me.cmbFiles = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAddFile = New System.Windows.Forms.Button()
        Me.txtSearchFor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFileFolder = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(15, 25)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(336, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 1
        '
        'lblItemProgress
        '
        Me.lblItemProgress.AutoSize = True
        Me.lblItemProgress.Location = New System.Drawing.Point(12, 9)
        Me.lblItemProgress.Name = "lblItemProgress"
        Me.lblItemProgress.Size = New System.Drawing.Size(87, 13)
        Me.lblItemProgress.TabIndex = 2
        Me.lblItemProgress.Text = "Finding Folders..."
        Me.lblItemProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmbFiles
        '
        Me.cmbFiles.FormattingEnabled = True
        Me.cmbFiles.Location = New System.Drawing.Point(66, 124)
        Me.cmbFiles.Name = "cmbFiles"
        Me.cmbFiles.Size = New System.Drawing.Size(285, 21)
        Me.cmbFiles.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 127)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "File"
        '
        'btnAddFile
        '
        Me.btnAddFile.Enabled = False
        Me.btnAddFile.Location = New System.Drawing.Point(357, 124)
        Me.btnAddFile.Name = "btnAddFile"
        Me.btnAddFile.Size = New System.Drawing.Size(75, 23)
        Me.btnAddFile.TabIndex = 5
        Me.btnAddFile.Text = "Add File"
        Me.btnAddFile.UseVisualStyleBackColor = True
        '
        'txtSearchFor
        '
        Me.txtSearchFor.Location = New System.Drawing.Point(66, 71)
        Me.txtSearchFor.Name = "txtSearchFor"
        Me.txtSearchFor.Size = New System.Drawing.Size(285, 20)
        Me.txtSearchFor.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Filter by"
        '
        'lblFileFolder
        '
        Me.lblFileFolder.AutoSize = True
        Me.lblFileFolder.Location = New System.Drawing.Point(69, 108)
        Me.lblFileFolder.Name = "lblFileFolder"
        Me.lblFileFolder.Size = New System.Drawing.Size(10, 13)
        Me.lblFileFolder.TabIndex = 8
        Me.lblFileFolder.Text = "-"
        '
        'frmAddFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 157)
        Me.Controls.Add(Me.lblFileFolder)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSearchFor)
        Me.Controls.Add(Me.btnAddFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbFiles)
        Me.Controls.Add(Me.lblItemProgress)
        Me.Controls.Add(Me.ProgressBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmAddFile"
        Me.Text = "Add File"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents lblItemProgress As Label
    Friend WithEvents cmbFiles As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAddFile As Button
    Friend WithEvents txtSearchFor As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblFileFolder As Label
End Class
