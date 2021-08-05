<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDevoluciones

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
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkFacturado = New System.Windows.Forms.CheckBox()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.cmbRemitos = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.chkRemitos = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PicCENTROSCOSTOS = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicCENTROSCOSTOS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkFacturado)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.cmbAlmacenes)
        Me.GroupBox1.Controls.Add(Me.cmbRemitos)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.chkRemitos)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.PicCENTROSCOSTOS)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1038, 368)
        Me.GroupBox1.TabIndex = 63
        Me.GroupBox1.TabStop = False
        '
        'chkFacturado
        '
        Me.chkFacturado.AutoSize = True
        Me.chkFacturado.Enabled = False
        Me.chkFacturado.Location = New System.Drawing.Point(894, 40)
        Me.chkFacturado.Name = "chkFacturado"
        Me.chkFacturado.Size = New System.Drawing.Size(74, 17)
        Me.chkFacturado.TabIndex = 278
        Me.chkFacturado.Text = "Facturado"
        Me.chkFacturado.UseVisualStyleBackColor = True
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(797, 7)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(23, 20)
        Me.txtIdCliente.TabIndex = 277
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(531, 22)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 13)
        Me.Label16.TabIndex = 276
        Me.Label16.Text = "Depósito*"
        '
        'cmbAlmacenes
        '
        Me.cmbAlmacenes.AccessibleName = "*Depósito"
        Me.cmbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAlmacenes.DropDownHeight = 500
        Me.cmbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAlmacenes.FormattingEnabled = True
        Me.cmbAlmacenes.IntegralHeight = False
        Me.cmbAlmacenes.Location = New System.Drawing.Point(531, 38)
        Me.cmbAlmacenes.Name = "cmbAlmacenes"
        Me.cmbAlmacenes.Size = New System.Drawing.Size(91, 21)
        Me.cmbAlmacenes.TabIndex = 3
        '
        'cmbRemitos
        '
        Me.cmbRemitos.AccessibleName = ""
        Me.cmbRemitos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbRemitos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRemitos.Enabled = False
        Me.cmbRemitos.FormattingEnabled = True
        Me.cmbRemitos.Location = New System.Drawing.Point(628, 38)
        Me.cmbRemitos.Name = "cmbRemitos"
        Me.cmbRemitos.Size = New System.Drawing.Size(260, 21)
        Me.cmbRemitos.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(222, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 140
        Me.Label4.Text = "Cliente*"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*Clientes"
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.Location = New System.Drawing.Point(225, 38)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(300, 21)
        Me.cmbCliente.TabIndex = 2
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(13, 77)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(942, 20)
        Me.txtNota.TabIndex = 6
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(961, 80)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
        Me.chkEliminado.TabIndex = 7
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        '
        'chkRemitos
        '
        Me.chkRemitos.AutoSize = True
        Me.chkRemitos.Enabled = False
        Me.chkRemitos.Location = New System.Drawing.Point(628, 18)
        Me.chkRemitos.Name = "chkRemitos"
        Me.chkRemitos.Size = New System.Drawing.Size(78, 17)
        Me.chkRemitos.TabIndex = 4
        Me.chkRemitos.Text = "Por Remito"
        Me.chkRemitos.UseVisualStyleBackColor = True
        '
        'grdItems
        '
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(13, 103)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1019, 259)
        Me.grdItems.TabIndex = 8
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(768, 7)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(23, 20)
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
        Me.Label1.Location = New System.Drawing.Point(746, 7)
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
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 38)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(98, 20)
        Me.txtCODIGO.TabIndex = 0
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Devolución Nro."
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(117, 38)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(114, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'PicCENTROSCOSTOS
        '
        Me.PicCENTROSCOSTOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicCENTROSCOSTOS.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicCENTROSCOSTOS.Image = Global.SEYC.My.Resources.Resources.Info
        Me.PicCENTROSCOSTOS.Location = New System.Drawing.Point(268, 16)
        Me.PicCENTROSCOSTOS.Name = "PicCENTROSCOSTOS"
        Me.PicCENTROSCOSTOS.Size = New System.Drawing.Size(18, 20)
        Me.PicCENTROSCOSTOS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicCENTROSCOSTOS.TabIndex = 105
        Me.PicCENTROSCOSTOS.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 75)
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
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 23)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'frmDevoluciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1062, 516)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmDevoluciones"
        Me.Text = "Devoluciones"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicCENTROSCOSTOS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub



    Friend WithEvents PicCENTROSCOSTOS As System.Windows.Forms.PictureBox



    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label

    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label

    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents chkRemitos As System.Windows.Forms.CheckBox
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRemitos As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkFacturado As System.Windows.Forms.CheckBox

End Class
