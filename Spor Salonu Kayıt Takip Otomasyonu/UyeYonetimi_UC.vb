Imports System.Data.SQLite
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class UyeYonetimi_UC
    Dim dc As Database_Control = New Database_Control
    Dim database_path As String = Application.StartupPath & "\database.db"
    Dim selected_uye_id As Integer = 0
    Dim selected_spr_id As Integer
    Dim katilan_name As String
    Dim selected_person As Integer, duzenle_id, duzenle_uye_id As Integer
    ' Üyeler Tablosu
    Dim uyeler As New List(Of String) From {"tam_ad", "cinsiyet", "telefon_no", "adres", "dogum_tarihi"} ' , "uyelik_baslangic_tarihi", "uyelik_bitis_tarihi", "son_odeme_id"
    Private Sub UyeYonetimi_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect_Data()
    End Sub
    Private Function check_empty()
        If tamad_box.Text = String.Empty Or cinsiyet_box.Text = String.Empty Or telefon_box.Text = String.Empty Or adres_box.Text = String.Empty Then
            MsgBox("Lütfen boş alanları doldurunuz!", vbInformation)
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ekle_btn_Click(sender As Object, e As EventArgs) Handles ekle_btn.Click
        If check_empty() = False Then
            dc.Insert_Data(database_path, "uyeler_dtb", uyeler, New List(Of String) From {tamad_box.Text, cinsiyet_box.Text, telefon_box.Text, adres_box.Text, dt_box.Text})
            connect_Data()
            clear_boxes()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If check_empty() = False Then
            If selected_uye_id <> 0 Then

                Dim liste As New List(Of String) From {tamad_box.Text, cinsiyet_box.Text, adres_box.Text, telefon_box.Text, dt_box.Text}
                Dim i As Integer = 0
                For Each uyelerr In uyeler


                    dc.Update_Data(database_path, "uyeler_dtb", uyelerr, liste.Item(i), "ID", selected_uye_id)

                    i += 1
                Next
                connect_Data()
            Else
                MsgBox("Lütfen Listeden Bir Kişiyi Seçiniz!", vbInformation)
            End If
        End If



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If selected_uye_id <> 0 Then
            Try

                dc.Delete_Data(database_path, "uyeler_dtb", "ID", selected_uye_id)

            Catch ex As Exception
            End Try
            connect_Data()
            clear_boxes()
            selected_uye_id = 0
        Else
            MsgBox("Lütfen Listeden Bir Kişiyi Seçiniz!", vbInformation)
        End If


    End Sub

    Private Sub connect_Data()
        Dim connectionString As String = "Data Source=database.db;"

        ' SQLite bağlantısı oluşturun
        Using connection As New SQLiteConnection(connectionString)
            ' SQL sorgusu
            Dim query As String = "SELECT * FROM uyeler_dtb"

            ' SQLite veri adaptörü ve veri tablosu oluşturun
            Dim adapter As New SQLiteDataAdapter(query, connection)
            Dim table As New DataTable()

            ' Veri adaptörüyle verileri doldurun
            adapter.Fill(table)

            ' DataGridView'e verileri ata
            DataGridView1.DataSource = table
            DataGridView1.Columns("uyelik_baslangic_tarihi").Visible = False
            DataGridView1.Columns("uyelik_bitis_tarihi").Visible = False
            DataGridView1.Columns("son_odeme_id").Visible = False
        End Using

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

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex >= 0 Then ' Geçerli bir satır seçildiğinden emin olun
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            ' Verileri TextBox'lara aktar
            selected_uye_id = selectedRow.Cells("ID").Value()
            tamad_box.Text = selectedRow.Cells("tam_ad").Value.ToString()
            ComboBox5.Text = tamad_box.Text
            cinsiyet_box.Text = selectedRow.Cells("cinsiyet").Value.ToString()
            telefon_box.Text = selectedRow.Cells("telefon_no").Value.ToString()
            adres_box.Text = selectedRow.Cells("adres").Value.ToString()
            dt_box.Text = selectedRow.Cells("dogum_tarihi").Value().ToString()

            katilan_name = selected_uye_id & "-" & tamad_box.Text
        End If
        list_katilanlar()
    End Sub



    Public Function GetKatilanlarByGunVeProgramAdi() As List(Of String)
        Dim katilanlar As New List(Of String)()

        ' Veritabanı bağlantısı oluştur
        Using connection As New SQLiteConnection("Data Source=database.db")
            connection.Open()

            ' Sorgu hazırlığı
            Dim query As String = "SELECT ID, katilanlar FROM spor_programlari_dtb WHERE gun = @gun AND program_adi = @program_adi"
            Using command As New SQLiteCommand(query, connection)
                command.Parameters.AddWithValue("@gun", ComboBox1.Text)
                command.Parameters.AddWithValue("@program_adi", ComboBox2.Text)

                ' Sorguyu çalıştır ve sonuçları oku
                Using reader As SQLiteDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        ' Kayıt bulundu, verileri al
                        Dim id As Integer = reader.GetInt32(0)
                        Dim katilanlarVeri As String = reader.GetString(1)

                        ' ID'yi bildir
                        MessageBox.Show("Kayıt bulundu! ID: " & id.ToString())
                        selected_spr_id = id
                        ' Katilanlar verisini listeye ekle
                        katilanlar.AddRange(katilanlarVeri.Split(","c))
                    Else
                        ' Kayıt bulunamadı
                        MessageBox.Show("Kayıt bulunamadı!")
                    End If
                End Using
            End Using
        End Using

        Return katilanlar
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox5.Text = String.Empty Or ComboBox1.Text = String.Empty Or ComboBox2.Text = String.Empty Then
            MsgBox("Lüften bir üye seçiniz", vbInformation)
        Else
            Dim katilanlar As List(Of String) = GetKatilanlarByGunVeProgramAdi()

            ' uyeID değerini kontrol et


            If katilanlar.Contains(katilan_name) Then
                katilanlar.Remove(katilan_name)
                Dim katilanlarString As String = String.Join(",", katilanlar)
                dc.Update_Data(database_path, "spor_programlari_dtb", "katilanlar", katilanlarString, "ID", selected_spr_id)
            Else
                MessageBox.Show("uyeID değeri listede bulunamadı!")
            End If
        End If



    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ComboBox5.Text = String.Empty Or ComboBox1.Text = String.Empty Or ComboBox2.Text = String.Empty Then
            MsgBox("Lüften bir üye seçiniz", vbInformation)
        Else
            Dim katilanlar As List(Of String) = GetKatilanlarByGunVeProgramAdi()

            ' uye değerini kontrol et
            If katilanlar.Contains(katilan_name) Then
                MessageBox.Show("uyeID değeri zaten listede mevcut!")
            Else
                katilanlar.Add(katilan_name)
                Dim katilanlarString As String = String.Join(",", katilanlar)
                dc.Update_Data(database_path, "spor_programlari_dtb", "katilanlar", katilanlarString, "ID", selected_spr_id)
            End If
        End If


    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged, ComboBox2.SelectedValueChanged

        list_katilanlar()

    End Sub

    Private Sub list_katilanlar()
        Try
            Dim connectionString As String = "Data Source=database.db"
            Using connection As New SQLiteConnection(connectionString)
                connection.Open()

                ' Sorgu oluşturma
                Dim query As String = "SELECT * FROM spor_programlari_dtb WHERE gun = @gun AND program_adi = @program_adi AND katilanlar LIKE '%' || @deger || '%'"
                Using command As New SQLiteCommand(query, connection)
                    ' Parametrelerin değerlerini ayarlama
                    command.Parameters.AddWithValue("@gun", ComboBox1.Text)
                    command.Parameters.AddWithValue("@program_adi", ComboBox2.Text)
                    command.Parameters.AddWithValue("@deger", katilan_name)

                    ' Verileri almak için bir veri okuyucu oluşturma
                    Using reader As SQLiteDataReader = command.ExecuteReader()
                        ' DataTable oluşturma
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        ' DataGridView kontrolüne sonuçları bağlama
                        DataGridView2.DataSource = dataTable
                        If DataGridView2.Columns.Count <> 0 Then
                            DataGridView2.Columns("ID").Visible = False
                            DataGridView2.Columns("antID").Visible = False
                            DataGridView2.Columns("katilanlar").Visible = False
                        End If

                    End Using
                End Using

                connection.Close()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub clear_boxes()
        tamad_box.Clear()
        cinsiyet_box.Text = "Erkek"
        telefon_box.Clear()
        adres_box.Clear()
        dt_box.ResetText()
        selected_uye_id = 0
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        clear_boxes()
        connect_Data()
    End Sub


End Class
