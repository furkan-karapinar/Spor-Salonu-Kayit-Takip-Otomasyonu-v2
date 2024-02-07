Imports System.Data.SQLite
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Antrenorler_UC
    Dim dc As Database_Control = New Database_Control
    Dim database_path As String = Application.StartupPath & "\database.db"
    ' Antrenörler dtb
    Dim antrenorler As New List(Of String) From {"ID", "tam_ad", "cinsiyet", "telefon_no", "adres", "calisma_saatleri"}
    Dim duzenle_antrenor_id As Integer
    Dim selected_person As Integer, duzenle_id As Integer
    Dim connectionString As String = "Data Source=database.db;"

    Private Sub connect_data()






        Using connection3 As New SQLiteConnection(connectionString)
            ' SQL sorgusu
            Dim query As String = "SELECT * FROM antrenorler_dtb"

            ' SQLite veri adaptörü ve veri dtb oluşturun
            Dim adapter As New SQLiteDataAdapter(query, connection3)
            Dim table As New DataTable()

            ' Veri adaptörüyle verileri doldurun
            adapter.Fill(table)

            ' DataGridView'e verileri ata
            DataGridView3.DataSource = table

        End Using

        dc.LoadAntrenorler(ComboBox5)
        Using connection As New SQLiteConnection(connectionString)

            ' Sorguyu hazırla
            Dim query As String = "SELECT DISTINCT programadi FROM spor_programlari_list_dtb"
            Dim command As New SQLiteCommand(query, connection)

            Try
                ' Bağlantıyı aç
                connection.Open()

                ' Sorguyu çalıştır ve verileri al
                Dim reader As SQLiteDataReader = command.ExecuteReader()
                ComboBox2.Items.Clear()
                ' Verileri ComboBox'a ekle
                While reader.Read()
                    ComboBox2.Items.Add(reader("programadi").ToString())
                End While

                ' Okuyucuyu kapat
                reader.Close()
            Catch ex As Exception
                MessageBox.Show("Veritabanı bağlantısı veya sorgu hatası: " & ex.Message)
            Finally
                ' Bağlantıyı kapat
                connection.Close()
            End Try

        End Using
    End Sub

    Private Sub clear_ui()
        TextBox7.Text = String.Empty
        ComboBox2.Text = String.Empty
        TextBox8.Text = String.Empty
        TextBox9.Text = String.Empty
        ComboBox8.Text = String.Empty
        ComboBox9.Text = String.Empty
        ComboBox5.Text = String.Empty
        ComboBox3.Text = String.Empty
        ComboBox4.Text = String.Empty
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If TextBox7.Text = String.Empty Or ComboBox7.Text = String.Empty Or TextBox8.Text = String.Empty Or TextBox9.Text = String.Empty Or ComboBox8.Text = String.Empty Or ComboBox9.Text = String.Empty Then
            MsgBox("Lütfen boş alanları doldurun", vbInformation)
        Else

            Dim calisma_saati As String = ComboBox8.Text & " - " & ComboBox9.Text
            Dim antrenorlerr As New List(Of String)
            antrenorlerr = antrenorler
            antrenorlerr.Remove("ID")
            Dim lastid As Integer = dc.Insert_Data_And_Return_Last_ID(database_path, "antrenorler_dtb", antrenorlerr, New List(Of String) From {TextBox7.Text, ComboBox7.Text, TextBox8.Text, TextBox9.Text, calisma_saati})
            connect_data()
            clear_ui()

        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TextBox7.Text = String.Empty Or ComboBox7.Text = String.Empty Or TextBox8.Text = String.Empty Or TextBox9.Text = String.Empty Or ComboBox8.Text = String.Empty Or ComboBox9.Text = String.Empty Then
            MsgBox("Lütfen boş alanları doldurun", vbInformation)
        Else
            Dim calisma_saati As String = ComboBox8.Text & " - " & ComboBox9.Text
            Dim sutun_list As New List(Of String)
            sutun_list = antrenorler
            sutun_list.Remove("ID")
            Dim liste As New List(Of String) From {TextBox7.Text, ComboBox7.Text, TextBox8.Text, TextBox9.Text, calisma_saati}
            Dim i As Integer = 0
            For Each uyelerr In sutun_list
                dc.Update_Data(database_path, "antrenorler_dtb", uyelerr, liste.Item(i), "ID", duzenle_antrenor_id)
                i += 1
            Next
            connect_data()
            clear_ui()

        End If



    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If TextBox7.Text = String.Empty Or ComboBox7.Text = String.Empty Or TextBox8.Text = String.Empty Or TextBox9.Text = String.Empty Or ComboBox8.Text = String.Empty Or ComboBox9.Text = String.Empty Then
            MsgBox("Lütfen boş alanları doldurun", vbInformation)
        Else
            dc.Delete_Data(database_path, "antrenorler_dtb", "ID", duzenle_antrenor_id)
            connect_data()
            clear_ui()

        End If



    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex >= 0 Then ' Geçerli bir satır seçildiğinden emin olun
            Dim selectedRow As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            ' Verileri TextBox'lara aktar
            TextBox7.Text = selectedRow.Cells("tam_ad").Value.ToString()
            ComboBox5.Text = selectedRow.Cells("tam_ad").Value.ToString()
            ComboBox7.Text = selectedRow.Cells("cinsiyet").Value.ToString()
            TextBox8.Text = selectedRow.Cells("telefon_no").Value.ToString()
            TextBox9.Text = selectedRow.Cells("adres").Value.ToString()
            duzenle_antrenor_id = selectedRow.Cells("ID").Value()

            Dim calisma As String = selectedRow.Cells("calisma_saatleri").Value.ToString()
            Dim calisma_lst As List(Of String) = calisma.Split({" - "}, StringSplitOptions.RemoveEmptyEntries).ToList()
            ComboBox8.Text = calisma_lst(0)
            ComboBox9.Text = calisma_lst(1)


            Using connection2 As New SQLiteConnection(connectionString)
                ' SQL sorgusu
                Dim query As String = "SELECT * FROM spor_programlari_dtb WHERE antID = '" & duzenle_antrenor_id & "'"

                ' SQLite veri adaptörü ve veri dtb oluşturun
                Dim adapter As New SQLiteDataAdapter(query, connection2)
                Dim table As New DataTable()

                ' Veri adaptörüyle verileri doldurun
                adapter.Fill(table)

                ' DataGridView'e verileri ata
                DataGridView1.DataSource = table
                ' DataGridView'de kisi_id, kisi_turu ve ID sütunlarını gizleme

                'DataGridView1.Columns("kisi_id").Visible = False
                'DataGridView1.Columns("kisi_turu").Visible = False
                'DataGridView1.Columns("ID").Visible = False

            End Using
        End If
    End Sub

    Private Sub Antrenorler_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect_data()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text = String.Empty Or ComboBox2.Text = String.Empty Or ComboBox3.Text = String.Empty Or ComboBox4.Text = String.Empty Or ComboBox5.Text = String.Empty Or duzenle_antrenor_id = 0 Then
            MsgBox("Lütfen bir antrenör seçin ve boş alanları doldurun", vbInformation)
        Else
            Dim calisma_saati As String = ComboBox3.Text & " - " & ComboBox4.Text
            dc.Insert_Data(database_path, "spor_programlari_dtb", New List(Of String) From {"antID", "katilanlar", "gun", "program_adi", "program_saati"}, New List(Of String) From {duzenle_antrenor_id, "", ComboBox1.Text, ComboBox2.Text, calisma_saati})
            connect_data()
            clear_ui()

        End If




    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then ' Geçerli bir satır seçildiğinden emin olun
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            ' Verileri TextBox'lara aktar
            Dim kisi_id As String = selectedRow.Cells("antID").Value.ToString
            selected_person = kisi_id
            'Dim kisi_adi As String = dc.Get_Single_Data(database_path, "antrenorler_dtb", "antrenorID", kisi_id, 1, "Integer")

            'TextBox1.Text = kisi_adi
            ComboBox1.Text = selectedRow.Cells("gun").Value.ToString()
            ComboBox2.Text = selectedRow.Cells("program_adi").Value.ToString()

            Dim calisma As String = selectedRow.Cells("program_saati").Value.ToString()
            Dim calisma_lst As List(Of String) = calisma.Split({" - "}, StringSplitOptions.RemoveEmptyEntries).ToList()
            ComboBox3.Text = calisma_lst(0)
            ComboBox4.Text = calisma_lst(1)
            duzenle_id = dc.GetIDByAllColumns(selected_person, ComboBox1.Text, ComboBox2.Text, ComboBox3.Text & " - " & ComboBox4.Text)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox1.Text = String.Empty Or ComboBox2.Text = String.Empty Or ComboBox3.Text = String.Empty Or ComboBox4.Text = String.Empty Or ComboBox5.Text = String.Empty Then
            MsgBox("Lütfen bir antrenör seçin ve boş alanları doldurun", vbInformation)
        Else
            dc.Delete_Data(database_path, "spor_programlari_dtb", "ID", duzenle_id)
            connect_data()
            clear_ui()

        End If



    End Sub

    Private Sub ComboBox5_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedValueChanged
        Try
            Dim kisi_idsi As Integer = dc.Get_Single_Data(database_path, "antrenorler_dtb", "tam_adi", "'" & ComboBox5.Text & "'", 0, "Integer")
            duzenle_antrenor_id = kisi_idsi
            DataGridView1.DataSource = dc.LoadProgramlarByKisiID(DataGridView1, kisi_idsi, 0)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.Text = String.Empty Or ComboBox2.Text = String.Empty Or ComboBox3.Text = String.Empty Or ComboBox4.Text = String.Empty Or ComboBox5.Text = String.Empty Then
            MsgBox("Lütfen bir antrenör seçin ve boş alanları doldurun", vbInformation)
        Else
            Dim columns As New List(Of String) From {"antID", "gun", "program_adi", "program_saati"}
            Dim values As New List(Of String) From {selected_person, ComboBox1.Text, ComboBox2.Text, ComboBox3.Text & " - " & ComboBox4.Text}
            Dim i As Integer = 0
            For Each lst In columns
                dc.Update_Data(database_path, "spor_programlari_dtb", lst, values.Item(i), "ID", duzenle_id)
                i += 1
            Next
            connect_data()
            clear_ui()

        End If



    End Sub
End Class
