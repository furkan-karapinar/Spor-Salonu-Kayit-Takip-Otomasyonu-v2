Public Class Main_Form
    Dim dc As Database_Control = New Database_Control
    Private Sub load_uc(uc As UserControl)
        reset_menu_color()
        Panel1.Controls.Clear()
        uc.Dock = DockStyle.Fill
        Panel1.Controls.Add(uc)
    End Sub

    Private Sub reset_menu_color()
        UyelerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.MistyRose)
        UyeliklerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.MistyRose)
        SporProgramlarıToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.MistyRose)
        AntrenörlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.MistyRose)
    End Sub

    Private Sub UyelerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UyelerToolStripMenuItem.Click

        load_uc(New UyeYonetimi_UC)
        UyelerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.RosyBrown)
    End Sub

    Private Sub UyeliklerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UyeliklerToolStripMenuItem.Click

        load_uc(New UyelikYonetimi_UC)
        UyeliklerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.RosyBrown)
    End Sub

    Private Sub SporProgramlarıToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SporProgramlarıToolStripMenuItem.Click

        load_uc(New SporProgramlari_UC)
        SporProgramlarıToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.RosyBrown)
    End Sub

    Private Sub AntrenörlerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AntrenörlerToolStripMenuItem.Click

        load_uc(New Antrenorler_UC)
        AntrenörlerToolStripMenuItem.BackColor = Color.FromKnownColor(KnownColor.RosyBrown)
    End Sub

    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim database_full_path As String = Application.StartupPath & "\database.db"


        'Antrenörler Tablosu
        dc.Create_Database(database_full_path, "antrenorler_dtb", New List(Of String) From {"ID INTEGER PRIMARY KEY AUTOINCREMENT", "tam_ad TEXT", "cinsiyet TEXT", "telefon_no TEXT", "adres TEXT", "calisma_saatleri TEXT"})
        'Üyeler ve Üyelikler Tablosu
        dc.Create_Database(database_full_path, "uyeler_dtb", New List(Of String) From {"ID INTEGER PRIMARY KEY AUTOINCREMENT", "tam_ad TEXT", "cinsiyet TEXT", "telefon_no TEXT", "adres TEXT", "dogum_tarihi TEXT", "uyelik_baslangic_tarihi TEXT", "uyelik_bitis_tarihi TEXT", "son_odeme_id INTEGER"})
        'Ödemeler Tablosu
        dc.Create_Database(database_full_path, "odemeler_dtb", New List(Of String) From {"ID INTEGER PRIMARY KEY AUTOINCREMENT", "uyeID INTEGER", "odenen_tutar INTEGER", "odeme_tarihi TEXT", "odeme_yontemi TEXT"})
        'Spor Programları Tarihleri Tablosu
        dc.Create_Database(database_full_path, "spor_programlari_dtb", New List(Of String) From {"ID INTEGER PRIMARY KEY AUTOINCREMENT", "antID INTEGER", "program_adi TEXT", "program_saati TEXT", "katilanlar TEXT", "gun TEXT"})
        'Spor Programları Tablosu
        dc.Create_Database(database_full_path, "spor_programlari_list_dtb", New List(Of String) From {"programID INTEGER PRIMARY KEY AUTOINCREMENT", "programadi TEXT"})
    End Sub
End Class