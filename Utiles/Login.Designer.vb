<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents txtuser As System.Windows.Forms.TextBox
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents OK As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.txtuser = New System.Windows.Forms.TextBox()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.OK = New System.Windows.Forms.Button()
        Me.txtDominio = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        Me.txtpasswordConf = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnContrasena = New System.Windows.Forms.Button()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(16, 11)
        Me.UsernameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(81, 28)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "&Usuario"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(16, 39)
        Me.PasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(81, 28)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Contraseña"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtuser
        '
        Me.txtuser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
        Me.txtuser.Location = New System.Drawing.Point(105, 15)
        Me.txtuser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.Size = New System.Drawing.Size(123, 22)
        Me.txtuser.TabIndex = 1
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(105, 47)
        Me.txtpassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.Size = New System.Drawing.Size(123, 22)
        Me.txtpassword.TabIndex = 3
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(49, 113)
        Me.OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(96, 28)
        Me.OK.TabIndex = 6
        Me.OK.Text = "&Ingresar"
        '
        'txtDominio
        '
        Me.txtDominio.Enabled = False
        Me.txtDominio.Location = New System.Drawing.Point(252, 130)
        Me.txtDominio.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDominio.Name = "txtDominio"
        Me.txtDominio.Size = New System.Drawing.Size(124, 22)
        Me.txtDominio.TabIndex = 4
        Me.txtDominio.Visible = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(248, 70)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 28)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "&Dominio"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Enabled = False
        Me.CheckBox1.Location = New System.Drawing.Point(237, 102)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(162, 21)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "Integrado a Windows"
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.BackgroundImage = CType(resources.GetObject("LogoPictureBox.BackgroundImage"), System.Drawing.Image)
        Me.LogoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.LogoPictureBox.Location = New System.Drawing.Point(237, 14)
        Me.LogoPictureBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(108, 90)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        '
        'txtpasswordConf
        '
        Me.txtpasswordConf.Location = New System.Drawing.Point(104, 79)
        Me.txtpasswordConf.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtpasswordConf.Name = "txtpasswordConf"
        Me.txtpasswordConf.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpasswordConf.Size = New System.Drawing.Size(124, 22)
        Me.txtpasswordConf.TabIndex = 5
        Me.txtpasswordConf.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 79)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 28)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Confirmar"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Visible = False
        '
        'btnContrasena
        '
        Me.btnContrasena.Location = New System.Drawing.Point(153, 113)
        Me.btnContrasena.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnContrasena.Name = "btnContrasena"
        Me.btnContrasena.Size = New System.Drawing.Size(167, 28)
        Me.btnContrasena.TabIndex = 7
        Me.btnContrasena.Text = "&Recordar Contraseña"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 151)
        Me.Controls.Add(Me.btnContrasena)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtpasswordConf)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.txtDominio)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.txtpassword)
        Me.Controls.Add(Me.txtuser)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Login"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Control de Acceso"
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDominio As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents txtpasswordConf As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnContrasena As System.Windows.Forms.Button
    Public WithEvents LogoPictureBox As System.Windows.Forms.PictureBox

End Class
