<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferencias
    Inherits frmBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtOBSERVACIONES2 = New System.Windows.Forms.TextBox
        Me.cmbALMACENDESTINO = New System.Windows.Forms.ComboBox
        Me.PicALMACENES = New System.Windows.Forms.PictureBox
        Me.cmbALMACENORIGEN = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkEliminado = New System.Windows.Forms.CheckBox
        Me.grdItems = New System.Windows.Forms.DataGridView
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicALMACENES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtOBSERVACIONES2)
        Me.GroupBox1.Controls.Add(Me.cmbALMACENDESTINO)
        Me.GroupBox1.Controls.Add(Me.PicALMACENES)
        Me.GroupBox1.Controls.Add(Me.cmbALMACENORIGEN)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(780, 278)
        Me.GroupBox1.TabIndex = 64
        Me.GroupBox1.TabStop = False
        '
        'txtOBSERVACIONES2
        '
        Me.txtOBSERVACIONES2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOBSERVACIONES2.Location = New System.Drawing.Point(13, 77)
        Me.txtOBSERVACIONES2.Name = "txtOBSERVACIONES2"
        Me.txtOBSERVACIONES2.Size = New System.Drawing.Size(618, 20)
        Me.txtOBSERVACIONES2.TabIndex = 125
        '
        'cmbALMACENDESTINO
        '
        Me.cmbALMACENDESTINO.FormattingEnabled = True
        Me.cmbALMACENDESTINO.Location = New System.Drawing.Point(510, 25)
        Me.cmbALMACENDESTINO.Name = "cmbALMACENDESTINO"
        Me.cmbALMACENDESTINO.Size = New System.Drawing.Size(183, 21)
        Me.cmbALMACENDESTINO.TabIndex = 124
        '
        'PicALMACENES
        '
        Me.PicALMACENES.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicALMACENES.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicALMACENES.Image = Global.SEYC.My.Resources.Resources.Info
        Me.PicALMACENES.Location = New System.Drawing.Point(699, 24)
        Me.PicALMACENES.Name = "PicALMACENES"
        Me.PicALMACENES.Size = New System.Drawing.Size(18, 20)
        Me.PicALMACENES.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicALMACENES.TabIndex = 122
        Me.PicALMACENES.TabStop = False
        '
        'cmbALMACENORIGEN
        '
        Me.cmbALMACENORIGEN.AccessibleName = "*ALMACENORIGEN"
        Me.cmbALMACENORIGEN.FormattingEnabled = True
        Me.cmbALMACENORIGEN.Location = New System.Drawing.Point(319, 25)
        Me.cmbALMACENORIGEN.Name = "cmbALMACENORIGEN"
        Me.cmbALMACENORIGEN.Size = New System.Drawing.Size(183, 21)
        Me.cmbALMACENORIGEN.TabIndex = 120
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(316, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 119
        Me.Label6.Text = "Almacen Origen"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(506, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "Almacen Destino"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 117
        Me.Label4.Text = "Notas"
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(689, 77)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
        Me.chkEliminado.TabIndex = 116
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        '
        'grdItems
        '
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(13, 103)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(759, 159)
        Me.grdItems.TabIndex = 109
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(723, 27)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(47, 20)
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
        Me.Label1.Location = New System.Drawing.Point(744, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = "CODIGO"
        Me.txtCODIGO.BackColor = System.Drawing.SystemColors.Window
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 25)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
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
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Transferencia Nro."
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(134, 25)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 52
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(131, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 73)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(360, 22)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(360, 22)
        Me.BuscarToolStripMenuItem.Text = "Buscar..."
        Me.BuscarToolStripMenuItem.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem
        '
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 21)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'frmTransferencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 361)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmTransferencias"
        Me.Text = "frmTransferencias"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicALMACENES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbALMACENORIGEN As System.Windows.Forms.ComboBox
    Friend WithEvents PicALMACENES As System.Windows.Forms.PictureBox
    Friend WithEvents cmbALMACENDESTINO As System.Windows.Forms.ComboBox
    Friend WithEvents txtOBSERVACIONES2 As System.Windows.Forms.TextBox
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
End Class
