Imports System.Data.SQLite

Public Class SporProgramlari_UC
    Dim dc As Database_Control = New Database_Control
    Dim database_path As String = Application.StartupPath & "\database.db"

    ' Spor Programları Tablosu
    Dim sporProgramlari As New List(Of String) From {"programID", "programadi"}
    Dim duzenle_spor_id As Integer

    Private Sub connect_data()
        Dim connectionString As String = "Data Source=database.db;"
        Using connection2 As New SQLiteConnection(connectionString)
            ' SQL sorgusu
            Dim query As String = "SELECT * FROM spor_programlari_list_dtb"

            ' SQLite veri adaptörü ve veri tablosu oluşturun
            Dim adapter As New SQLiteDataAdapter(query, connection2)
            Dim table As New DataTable()

            ' Veri adaptörüyle verileri doldurun
            adapter.Fill(table)

            ' DataGridView'e verileri ata
            DataGridView2.DataSource = table

        End Using
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox5.Text = String.Empty Then
            MsgBox("Lütfen boş alanı doldurunuz", vbInformation)
        Else
            Dim sports As New List(Of String)
            sports = sporProgramlari
            sports.Remove("programID")
            dc.Insert_Data(database_path, "spor_programlari_list_dtb", sports, New List(Of String) From {TextBox5.Text})
            connect_data()
        End If


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim sports As New List(Of String)
        sports = sporProgramlari
        sports.Remove("programID")

        Dim sports_value_list As New List(Of String) From {TextBox5.Text}

        Dim i As Integer = 0
        For Each uyelerr In sports
            dc.Update_Data(database_path, "spor_programlari_list_dtb", uyelerr, sports_value_list.Item(i), "programID", duzenle_spor_id)
            i += 1
        Next
        connect_data()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try

            dc.Delete_Data(database_path, "spor_programlari_list_dtb", "programID", duzenle_spor_id)
            connect_data()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SporProgramlari_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect_data()
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then ' Geçerli bir satır seçildiğinden emin olun
            Dim selectedRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            ' Verileri TextBox'lara aktar
            TextBox5.Text = selectedRow.Cells("programadi").Value.ToString()
            duzenle_spor_id = selectedRow.Cells("programID").Value
        End If
    End Sub
End Class
