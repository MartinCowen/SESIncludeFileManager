﻿Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSDKFolder.Text = My.Settings.SDKFolder
        txtProjectFolder.Text = My.Settings.SESProjectFolder
        txtProjectFile.Text = My.Settings.SESProjectFile

    End Sub
End Class
