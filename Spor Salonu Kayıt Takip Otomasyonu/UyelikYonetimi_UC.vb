Imports System.Data.SQLite

Public Class UyelikYonetimi_UC
    Dim dc As Database_Control = New Database_Control
    Dim database_path As String = Application.StartupPath & "\database.db"

    ' Üyelikler dtb
    Dim uyelikler As New List(Of String) From {"uyelik_baslangic_tarihi", "uyelik_bitis_tarihi", "son_odeme_id"}

    ' Ödemeler dtb
    Dim odemeler As New List(Of String) From {"ID", "uyeID", "odenen_tutar", "odeme_tarihi", "odeme_yontemi"}

    Dim selected_uye_id As Integer, selected_odeme_id As Integer
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox4.Text = String.Empty Or TextBox6.Text = String.Empty Or ComboBox2.Text = String.Empty Then
            MsgBox("Lütfen bir üye seçin ve boş alanları doldurun", vbInformation)
        Else
            Dim odemelerr As New List(Of String)
            odemelerr = odemeler
            odemelerr.Remove("ID")
            Dim last_id As Integer = dc.Insert_Data_And_Return_Last_ID(database_path, "odemeler_dtb", odemelerr, New List(Of String) From {selected_uye_id, TextBox4.Text, DateTimePicker2.Text, ComboBox2.Text})
            Dim a As New List(Of String) From {DateTimePicker3.Text, DateTimePicker4.Text, last_id}
            Dim i As Integer = 0
            For Each u In uyelikler
                dc.Update_Data(database_path, "uyeler_dtb", u, a.Item(i), "ID", selected_uye_id)
                i += 1
            Next
            clear_ui()
        End If


        connect_Data()
    End Sub

    Private Sub connect_Data()
        Dim connectionString As String = "Data Source=database.db;"

        ' SQLite bağlantısı oluşturun
        Using connection As New SQLiteConnection(connectionString)
            ' SQL sorgusu
            Dim query As String = "SELECT * FROM uyeler_dtb"

            ' SQLite veri adaptörü ve veri dtb oluşturun
            Dim adapter As New SQLiteDataAdapter(query, connection)
            Dim table As New DataTable()

            ' Veri adaptörüyle verileri doldurun
            adapter.Fill(table)

            ' DataGridView'e verileri ata
            DataGridView1.DataSource = table
            DataGridView1.Columns("cinsiyet").Visible = False
            DataGridView1.Columns("telefon_no").Visible = False
            DataGridView1.Columns("adres").Visible = False
            DataGridView1.Columns("dogum_tarihi").Visible = False
            DataGridView1.Columns("son_odeme_id").Visible = False


        End Using
    End Sub

    Private Sub UyelikYonetimi_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect_Data()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        DataGridView2.DataSource = vbNull
        Try
            If e.RowIndex >= 0 Then ' Geçerli bir satır seçildiğinden emin olun
                Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                ' Verileri TextBox'lara aktar
                selected_uye_id = selectedRow.Cells("ID").Value()
                TextBox6.Text = selectedRow.Cells("tam_ad").Value.ToString()
                DateTimePicker3.Text = selectedRow.Cells("uyelik_baslangic_tarihi").Value.ToString()
                DateTimePicker4.Text = selectedRow.Cells("uyelik_bitis_tarihi").Value.ToString()
                DataGridView2.DataSource = dc.ListeleByUyeID(selected_uye_id)
                Dim sonodemeid As Integer = dc.GetSonOdemeID(selected_uye_id)
                selected_odeme_id = sonodemeid
                Dim get_list As New List(Of String) From {2, 3, 4}
                Dim selected_list As New List(Of String)
                For Each lst In get_list
                    selected_list.Add(dc.Get_Single_Data(database_path, "odemeler_dtb", "ID", sonodemeid.ToString, lst, "Integer"))
                Next
                TextBox4.Text = selected_list.Item(0)
                DateTimePicker2.Text = selected_list.Item(1)
                ComboBox2.Text = selected_list.Item(2)
            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub clear_ui()
        TextBox4.Clear()
        TextBox6.Clear()
        ComboBox2.Text = String.Empty
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox4.Text = String.Empty Or TextBox6.Text = String.Empty Or ComboBox2.Text = String.Empty Then
            MsgBox("Lütfen bir üye seçin ve boş alanları doldurun", vbInformation)
        Else
            If selected_odeme_id <> 0 Then
                Try

                    dc.Delete_Data(database_path, "odemeler_dtb", "ID", selected_odeme_id)
                    dc.Update_Data(database_path, "uyeler_dtb", "son_odeme_id", "IPTAL", "ID", selected_uye_id)
                    dc.Update_Data(database_path, "uyeler_dtb", "uyelik_baslangic_tarihi", "IPTAL", "ID", selected_uye_id)
                    dc.Update_Data(database_path, "uyeler_dtb", "uyelik_bitis_tarihi", "IPTAL", "ID", selected_uye_id)
                Catch ex As Exception
                End Try
                connect_Data()

                selected_odeme_id = 0
            Else
                MsgBox("Lütfen Listeden Bir Kişiyi Seçiniz!", vbInformation)
            End If
        End If

        clear_ui()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clear_ui()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox4.Text = String.Empty Or TextBox6.Text = String.Empty Or ComboBox2.Text = String.Empty Then
            MsgBox("Lütfen bir üye seçin ve boş alanları doldurun", vbInformation)
        Else
            dc.Update_Data(database_path, "odemeler_dtb", "tutar", TextBox4.Text, "ID", selected_odeme_id)
            dc.Update_Data(database_path, "odemeler_dtb", "odemetarihi", DateTimePicker2.Text, "ID", selected_odeme_id)
            dc.Update_Data(database_path, "odemeler_dtb", "odemeyontemi", ComboBox2.Text, "ID", selected_odeme_id)
            dc.Update_Data(database_path, "uyeler_dtb", "uyelik_baslangic_tarihi", DateTimePicker3.Text, "ID", selected_uye_id)
            dc.Update_Data(database_path, "uyeler_dtb", "uyelik_bitis_tarihi", DateTimePicker4.Text, "ID", selected_uye_id)
            clear_ui()
        End If
        connect_Data()
    End Sub
End Class
