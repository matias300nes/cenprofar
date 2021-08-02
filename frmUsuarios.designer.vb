<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUsuarios

    Inherits frmBase


    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnReset = New System.Windows.Forms.Button
        Me.txtPASS2 = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNOMBRE = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtAPELLIDO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPASS = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtEMAIL = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label6 = New System.Windows.Forms.Label
        Me.rbIngresaSistema = New System.Windows.Forms.RadioButton
        Me.rbSoloRetira = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.rbSoloRetira)
        Me.GroupBox1.Controls.Add(Me.rbIngresaSistema)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnReset)
        Me.GroupBox1.Controls.Add(Me.txtPASS2)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtNOMBRE)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtAPELLIDO)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPASS)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtEMAIL)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(790, 109)
        Me.GroupBox1.TabIndex = 63
        Me.GroupBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(253, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 96
        Me.Label7.Text = "Tipo de Usuario"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ForeColor = System.Drawing.Color.Blue
        Me.btnReset.Location = New System.Drawing.Point(677, 54)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(38, 25)
        Me.btnReset.TabIndex = 88
        Me.btnReset.Text = "Resetear Pass"
        Me.btnReset.UseVisualStyleBackColor = True
        Me.btnReset.Visible = False
        '
        'txtPASS2
        '
        Me.txtPASS2.AccessibleName = ""
        Me.txtPASS2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPASS2.Decimals = CType(2, Byte)
        Me.txtPASS2.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPASS2.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPASS2.Location = New System.Drawing.Point(625, 57)
        Me.txtPASS2.MaxLength = 50
        Me.txtPASS2.Name = "txtPASS2"
        Me.txtPASS2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPASS2.Size = New System.Drawing.Size(32, 20)
        Me.txtPASS2.TabIndex = 57
        Me.txtPASS2.Text_1 = Nothing
        Me.txtPASS2.Text_2 = Nothing
        Me.txtPASS2.Text_3 = Nothing
        Me.txtPASS2.Text_4 = Nothing
        Me.txtPASS2.UserValues = Nothing
        Me.txtPASS2.Visible = False
        '
        'txtID
        '
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(604, 57)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(15, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(583, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "id"
        Me.Label1.Visible = False
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = "*Alias"
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(10, 25)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.Size = New System.Drawing.Size(115, 20)
        Me.txtCODIGO.TabIndex = 51
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Código"
        '
        'txtNOMBRE
        '
        Me.txtNOMBRE.AccessibleName = "*Nombre"
        Me.txtNOMBRE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNOMBRE.Decimals = CType(2, Byte)
        Me.txtNOMBRE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNOMBRE.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNOMBRE.Location = New System.Drawing.Point(131, 25)
        Me.txtNOMBRE.MaxLength = 30
        Me.txtNOMBRE.Name = "txtNOMBRE"
        Me.txtNOMBRE.Size = New System.Drawing.Size(262, 20)
        Me.txtNOMBRE.TabIndex = 52
        Me.txtNOMBRE.Text_1 = Nothing
        Me.txtNOMBRE.Text_2 = Nothing
        Me.txtNOMBRE.Text_3 = Nothing
        Me.txtNOMBRE.Text_4 = Nothing
        Me.txtNOMBRE.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(128, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Nombre"
        '
        'txtAPELLIDO
        '
        Me.txtAPELLIDO.AccessibleName = "*Nombre"
        Me.txtAPELLIDO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAPELLIDO.Decimals = CType(2, Byte)
        Me.txtAPELLIDO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtAPELLIDO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtAPELLIDO.Location = New System.Drawing.Point(399, 25)
        Me.txtAPELLIDO.MaxLength = 30
        Me.txtAPELLIDO.Name = "txtAPELLIDO"
        Me.txtAPELLIDO.Size = New System.Drawing.Size(220, 20)
        Me.txtAPELLIDO.TabIndex = 53
        Me.txtAPELLIDO.Text_1 = Nothing
        Me.txtAPELLIDO.Text_2 = Nothing
        Me.txtAPELLIDO.Text_3 = Nothing
        Me.txtAPELLIDO.Text_4 = Nothing
        Me.txtAPELLIDO.UserValues = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(396, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Apellido"
        '
        'txtPASS
        '
        Me.txtPASS.AccessibleName = "*Pass"
        Me.txtPASS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPASS.Decimals = CType(2, Byte)
        Me.txtPASS.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPASS.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPASS.Location = New System.Drawing.Point(625, 26)
        Me.txtPASS.MaxLength = 50
        Me.txtPASS.Name = "txtPASS"
        Me.txtPASS.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPASS.Size = New System.Drawing.Size(152, 20)
        Me.txtPASS.TabIndex = 54
        Me.txtPASS.Text_1 = Nothing
        Me.txtPASS.Text_2 = Nothing
        Me.txtPASS.Text_3 = Nothing
        Me.txtPASS.Text_4 = Nothing
        Me.txtPASS.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(622, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Pass"
        '
        'txtEMAIL
        '
        Me.txtEMAIL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEMAIL.Decimals = CType(2, Byte)
        Me.txtEMAIL.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEMAIL.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtEMAIL.Location = New System.Drawing.Point(10, 73)
        Me.txtEMAIL.MaxLength = 50
        Me.txtEMAIL.Name = "txtEMAIL"
        Me.txtEMAIL.Size = New System.Drawing.Size(238, 20)
        Me.txtEMAIL.TabIndex = 55
        Me.txtEMAIL.Text_1 = Nothing
        Me.txtEMAIL.Text_2 = Nothing
        Me.txtEMAIL.Text_3 = Nothing
        Me.txtEMAIL.Text_4 = Nothing
        Me.txtEMAIL.UserValues = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "E-mail"
        '
        'rbIngresaSistema
        '
        Me.rbIngresaSistema.AutoSize = True
        Me.rbIngresaSistema.Location = New System.Drawing.Point(384, 73)
        Me.rbIngresaSistema.Name = "rbIngresaSistema"
        Me.rbIngresaSistema.Size = New System.Drawing.Size(200, 17)
        Me.rbIngresaSistema.TabIndex = 97
        Me.rbIngresaSistema.Text = "Retira Materiales e Ingresa al sistema"
        Me.rbIngresaSistema.UseVisualStyleBackColor = True
        '
        'rbSoloRetira
        '
        Me.rbSoloRetira.AutoSize = True
        Me.rbSoloRetira.Checked = True
        Me.rbSoloRetira.Location = New System.Drawing.Point(256, 74)
        Me.rbSoloRetira.Name = "rbSoloRetira"
        Me.rbSoloRetira.Size = New System.Drawing.Size(122, 17)
        Me.rbSoloRetira.TabIndex = 98
        Me.rbSoloRetira.TabStop = True
        Me.rbSoloRetira.Text = "Solo retira materiales"
        Me.rbSoloRetira.UseVisualStyleBackColor = True
        '
        'frmUsuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 396)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmUsuarios"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Usuarios - Empleados"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label

    Friend WithEvents txtNOMBRE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label

    Friend WithEvents txtAPELLIDO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label

    Friend WithEvents txtPASS As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label

    Friend WithEvents txtEMAIL As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label

    Friend WithEvents txtPASS2 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rbSoloRetira As System.Windows.Forms.RadioButton
    Friend WithEvents rbIngresaSistema As System.Windows.Forms.RadioButton









End Class