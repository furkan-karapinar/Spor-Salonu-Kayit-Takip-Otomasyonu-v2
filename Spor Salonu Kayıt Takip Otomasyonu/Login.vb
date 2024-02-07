Public Class Login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "admin" And TextBox2.Text = "password" Then
            MsgBox("Giriş Başarılı", vbInformation, "ANKA Spor Salonu Sistemi")
            Main_Form.Show()
            Me.Close()
        Else
            MsgBox("Giriş Bilgileri Yanlış", vbCritical, "ANKA Spor Salonu Sistemi")
        End If
    End Sub
End Class