Imports System.Text.RegularExpressions
Public Class Form1
    Dim date1 As Date
    Dim date2 As Date
    Dim ts As TimeSpan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            My.Settings.dir = FolderBrowserDialog1.SelectedPath
            txt_dir.Text = My.Settings.dir
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        newTask("Getting directory...")
        txt_dir.Text = My.Settings.dir
        Button3.PerformClick()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Computer.FileSystem.WriteAllText(My.Settings.dir & "\" & txt_new_name.Text & ".txt", "Plant name : " & txt_new_name.Text & vbNewLine & "Date planted : " & dtp_new_date.Value & vbNewLine & "Age : ", True)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        newTask("Reading files...")
        ComboBox1.Items.Clear()
        Try
            For Each file As String In My.Computer.FileSystem.GetFiles(My.Settings.dir)
                ComboBox1.Items.Add(My.Computer.FileSystem.GetName(file))
            Next
        Catch ex As Exception
            MsgBox("Set folder for data storage", vbInformation, "Folder not set")
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        newTask("Retrieving " & ComboBox1.SelectedItem)
        RichTextBox2.Text = My.Computer.FileSystem.ReadAllText(My.Settings.dir & "\" & ComboBox1.SelectedItem)
        updateAge()
    End Sub

    Sub newTask(task As String)
        RichTextBox1.AppendText(task & vbNewLine)
        RichTextBox1.ScrollToCaret()
    End Sub

    Sub updateAge()
        newTask("Updating...")
        date1 = retrieveDate()
        date2 = Date.Now()
        ts = date2.Subtract(date1)
        Dim lines As New List(Of String)
        lines = RichTextBox2.Lines.ToList
        lines(2) = "Age : " & ts.Days
        RichTextBox2.Lines = lines.ToArray
    End Sub

    Function retrieveDate()
        Dim lines As New List(Of String)
        lines = RichTextBox2.Lines.ToList
        Dim datePlanted As Date
        datePlanted = lines(1).Substring(14)

        Return datePlanted
    End Function


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        My.Computer.FileSystem.WriteAllText(My.Settings.dir & "\" & ComboBox1.SelectedItem, RichTextBox2.Text, False)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        updateAge()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        RichTextBox2.AppendText(vbNewLine)
        RichTextBox2.AppendText("-------------------------------------")
        RichTextBox2.AppendText(vbNewLine)
        RichTextBox2.AppendText(Date.Now().ToString("yyyy-mm-dd") & " | ")
        RichTextBox2.AppendText(TextBox3.Text)
    End Sub
End Class
