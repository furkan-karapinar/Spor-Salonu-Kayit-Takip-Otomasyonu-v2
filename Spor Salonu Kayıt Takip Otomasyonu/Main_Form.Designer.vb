<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Form
    Inherits System.Windows.Forms.Form

    'Form, bileşen listesini temizlemeyi bırakmayı geçersiz kılar.
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

    'Windows Form Tasarımcısı tarafından gerektirilir
    Private components As System.ComponentModel.IContainer

    'NOT: Aşağıdaki yordam Windows Form Tasarımcısı için gereklidir
    'Windows Form Tasarımcısı kullanılarak değiştirilebilir.  
    'Kod düzenleyicisini kullanarak değiştirmeyin.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.UyelerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UyeliklerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AntrenörlerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SporProgramlarıToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.RosyBrown
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(963, 478)
        Me.Panel1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.MistyRose
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UyelerToolStripMenuItem, Me.UyeliklerToolStripMenuItem, Me.AntrenörlerToolStripMenuItem, Me.SporProgramlarıToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(963, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'UyelerToolStripMenuItem
        '
        Me.UyelerToolStripMenuItem.Name = "UyelerToolStripMenuItem"
        Me.UyelerToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.UyelerToolStripMenuItem.Text = "Üyeler"
        '
        'UyeliklerToolStripMenuItem
        '
        Me.UyeliklerToolStripMenuItem.Name = "UyeliklerToolStripMenuItem"
        Me.UyeliklerToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.UyeliklerToolStripMenuItem.Text = "Üyelikler"
        '
        'AntrenörlerToolStripMenuItem
        '
        Me.AntrenörlerToolStripMenuItem.Name = "AntrenörlerToolStripMenuItem"
        Me.AntrenörlerToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.AntrenörlerToolStripMenuItem.Text = "Antrenörler"
        '
        'SporProgramlarıToolStripMenuItem
        '
        Me.SporProgramlarıToolStripMenuItem.Name = "SporProgramlarıToolStripMenuItem"
        Me.SporProgramlarıToolStripMenuItem.Size = New System.Drawing.Size(108, 20)
        Me.SporProgramlarıToolStripMenuItem.Text = "Spor Programları"
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 502)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Main_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ANKA Spor Salonu Sistemi"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents UyelerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UyeliklerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AntrenörlerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SporProgramlarıToolStripMenuItem As ToolStripMenuItem
End Class
