<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsumos_OLD

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
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblOrdendeCompra = New System.Windows.Forms.Label
        Me.txtOrdendeCompra = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblTotalconIVA = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblIVA = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblTotalVentasinIVA = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.PicRetira = New System.Windows.Forms.PictureBox
        Me.chkVenta = New System.Windows.Forms.CheckBox
        Me.btnStock = New System.Windows.Forms.Button
        Me.chkVerGrilla = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtcopias = New System.Windows.Forms.TextBox
        Me.btnReimprimirVale = New System.Windows.Forms.Button
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label8 = New System.Windows.Forms.Label
        Me.chkEliminado = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbRetira = New System.Windows.Forms.ComboBox
        Me.grdItems = New System.Windows.Forms.DataGridView
        Me.lblObra = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbCliente = New System.Windows.Forms.ComboBox
        Me.PicClientes = New System.Windows.Forms.PictureBox
        Me.cmbObras = New System.Windows.Forms.ComboBox
        Me.PicObras = New System.Windows.Forms.PictureBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicRetira, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicObras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblOrdendeCompra)
        Me.GroupBox1.Controls.Add(Me.txtOrdendeCompra)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblTotalconIVA)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.lblIVA)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblTotalVentasinIVA)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.PicRetira)
        Me.GroupBox1.Controls.Add(Me.chkVenta)
        Me.GroupBox1.Controls.Add(Me.btnStock)
        Me.GroupBox1.Controls.Add(Me.chkVerGrilla)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtcopias)
        Me.GroupBox1.Controls.Add(Me.btnReimprimirVale)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbRetira)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.lblObra)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.PicClientes)
        Me.GroupBox1.Controls.Add(Me.cmbObras)
        Me.GroupBox1.Controls.Add(Me.PicObras)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1001, 377)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblOrdendeCompra
        '
        Me.lblOrdendeCompra.AutoSize = True
        Me.lblOrdendeCompra.Location = New System.Drawing.Point(627, 10)
        Me.lblOrdendeCompra.Name = "lblOrdendeCompra"
        Me.lblOrdendeCompra.Size = New System.Drawing.Size(90, 13)
        Me.lblOrdendeCompra.TabIndex = 135
        Me.lblOrdendeCompra.Text = "Orden de Compra"
        Me.lblOrdendeCompra.Visible = False
        '
        'txtOrdendeCompra
        '
        Me.txtOrdendeCompra.AccessibleName = ""
        Me.txtOrdendeCompra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOrdendeCompra.Decimals = CType(2, Byte)
        Me.txtOrdendeCompra.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtOrdendeCompra.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrdendeCompra.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtOrdendeCompra.Location = New System.Drawing.Point(630, 25)
        Me.txtOrdendeCompra.Name = "txtOrdendeCompra"
        Me.txtOrdendeCompra.Size = New System.Drawing.Size(202, 20)
        Me.txtOrdendeCompra.TabIndex = 4
        Me.txtOrdendeCompra.Text_1 = Nothing
        Me.txtOrdendeCompra.Text_2 = Nothing
        Me.txtOrdendeCompra.Text_3 = Nothing
        Me.txtOrdendeCompra.Text_4 = Nothing
        Me.txtOrdendeCompra.UserValues = Nothing
        Me.txtOrdendeCompra.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(57, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(19, 15)
        Me.Label12.TabIndex = 133
        Me.Label12.Text = "%"
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = "*IVA"
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtIVA.Location = New System.Drawing.Point(13, 64)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(41, 20)
        Me.txtIVA.TabIndex = 5
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(10, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 13)
        Me.Label10.TabIndex = 131
        Me.Label10.Text = "IVA*"
        '
        'lblTotalconIVA
        '
        Me.lblTotalconIVA.BackColor = System.Drawing.Color.White
        Me.lblTotalconIVA.Location = New System.Drawing.Point(744, 356)
        Me.lblTotalconIVA.Name = "lblTotalconIVA"
        Me.lblTotalconIVA.Size = New System.Drawing.Size(65, 13)
        Me.lblTotalconIVA.TabIndex = 129
        Me.lblTotalconIVA.Text = "0"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(704, 356)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 128
        Me.Label13.Text = "Total "
        '
        'lblIVA
        '
        Me.lblIVA.BackColor = System.Drawing.Color.White
        Me.lblIVA.Location = New System.Drawing.Point(633, 356)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(65, 13)
        Me.lblIVA.TabIndex = 127
        Me.lblIVA.Text = "0"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(603, 356)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 126
        Me.Label11.Text = "IVA"
        '
        'lblTotalVentasinIVA
        '
        Me.lblTotalVentasinIVA.BackColor = System.Drawing.Color.White
        Me.lblTotalVentasinIVA.Location = New System.Drawing.Point(530, 356)
        Me.lblTotalVentasinIVA.Name = "lblTotalVentasinIVA"
        Me.lblTotalVentasinIVA.Size = New System.Drawing.Size(65, 13)
        Me.lblTotalVentasinIVA.TabIndex = 125
        Me.lblTotalVentasinIVA.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(478, 356)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 124
        Me.Label7.Text = "Subtotal"
        '
        'PicRetira
        '
        Me.PicRetira.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicRetira.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicRetira.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicRetira.Location = New System.Drawing.Point(339, 65)
        Me.PicRetira.Name = "PicRetira"
        Me.PicRetira.Size = New System.Drawing.Size(18, 20)
        Me.PicRetira.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicRetira.TabIndex = 123
        Me.PicRetira.TabStop = False
        '
        'chkVenta
        '
        Me.chkVenta.AccessibleName = "Eliminado"
        Me.chkVenta.AutoSize = True
        Me.chkVenta.Location = New System.Drawing.Point(213, 28)
        Me.chkVenta.Name = "chkVenta"
        Me.chkVenta.Size = New System.Drawing.Size(54, 17)
        Me.chkVenta.TabIndex = 2
        Me.chkVenta.Text = "Venta"
        Me.chkVenta.UseVisualStyleBackColor = True
        '
        'btnStock
        '
        Me.btnStock.Location = New System.Drawing.Point(934, 63)
        Me.btnStock.Name = "btnStock"
        Me.btnStock.Size = New System.Drawing.Size(61, 21)
        Me.btnStock.TabIndex = 12
        Me.btnStock.Text = "Stock"
        Me.btnStock.UseVisualStyleBackColor = True
        '
        'chkVerGrilla
        '
        Me.chkVerGrilla.AccessibleName = "Eliminado"
        Me.chkVerGrilla.AutoSize = True
        Me.chkVerGrilla.Location = New System.Drawing.Point(860, 65)
        Me.chkVerGrilla.Name = "chkVerGrilla"
        Me.chkVerGrilla.Size = New System.Drawing.Size(68, 17)
        Me.chkVerGrilla.TabIndex = 11
        Me.chkVerGrilla.Text = "Ver Grilla"
        Me.chkVerGrilla.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(820, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 120
        Me.Label9.Text = "Copias"
        '
        'txtcopias
        '
        Me.txtcopias.Location = New System.Drawing.Point(823, 65)
        Me.txtcopias.Name = "txtcopias"
        Me.txtcopias.Size = New System.Drawing.Size(31, 20)
        Me.txtcopias.TabIndex = 10
        Me.txtcopias.Text = "2"
        '
        'btnReimprimirVale
        '
        Me.btnReimprimirVale.Location = New System.Drawing.Point(769, 64)
        Me.btnReimprimirVale.Name = "btnReimprimirVale"
        Me.btnReimprimirVale.Size = New System.Drawing.Size(48, 21)
        Me.btnReimprimirVale.TabIndex = 9
        Me.btnReimprimirVale.Text = "Remito"
        Me.btnReimprimirVale.UseVisualStyleBackColor = True
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(363, 65)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(323, 20)
        Me.txtNota.TabIndex = 7
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(360, 49)
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
        Me.chkEliminado.Location = New System.Drawing.Point(692, 65)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
        Me.chkEliminado.TabIndex = 8
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(84, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 115
        Me.Label4.Text = "Retirado por*"
        '
        'cmbRetira
        '
        Me.cmbRetira.AccessibleName = "*Retira"
        Me.cmbRetira.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbRetira.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRetira.FormattingEnabled = True
        Me.cmbRetira.Location = New System.Drawing.Point(87, 65)
        Me.cmbRetira.Name = "cmbRetira"
        Me.cmbRetira.Size = New System.Drawing.Size(246, 21)
        Me.cmbRetira.TabIndex = 6
        '
        'grdItems
        '
        Me.grdItems.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(13, 92)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(982, 261)
        Me.grdItems.TabIndex = 13
        '
        'lblObra
        '
        Me.lblObra.AutoSize = True
        Me.lblObra.Location = New System.Drawing.Point(627, 10)
        Me.lblObra.Name = "lblObra"
        Me.lblObra.Size = New System.Drawing.Size(34, 13)
        Me.lblObra.TabIndex = 107
        Me.lblObra.Text = "Obra*"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(270, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "Cliente*"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(478, 7)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(77, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(561, 10)
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
        Me.Label2.Location = New System.Drawing.Point(10, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Consumo Nro."
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(117, 25)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(90, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(114, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*Clientes"
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.Location = New System.Drawing.Point(273, 25)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(321, 21)
        Me.cmbCliente.TabIndex = 3
        '
        'PicClientes
        '
        Me.PicClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicClientes.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicClientes.Location = New System.Drawing.Point(606, 26)
        Me.PicClientes.Name = "PicClientes"
        Me.PicClientes.Size = New System.Drawing.Size(18, 20)
        Me.PicClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicClientes.TabIndex = 105
        Me.PicClientes.TabStop = False
        '
        'cmbObras
        '
        Me.cmbObras.AccessibleName = "*Obras"
        Me.cmbObras.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbObras.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbObras.FormattingEnabled = True
        Me.cmbObras.Location = New System.Drawing.Point(630, 25)
        Me.cmbObras.Name = "cmbObras"
        Me.cmbObras.Size = New System.Drawing.Size(341, 21)
        Me.cmbObras.TabIndex = 4
        '
        'PicObras
        '
        Me.PicObras.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicObras.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicObras.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicObras.Location = New System.Drawing.Point(977, 25)
        Me.PicObras.Name = "PicObras"
        Me.PicObras.Size = New System.Drawing.Size(18, 20)
        Me.PicObras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicObras.TabIndex = 105
        Me.PicObras.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 202)
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
        Me.BuscarDescripcionToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 150)
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'frmConsumos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 638)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmConsumos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ventas y Consumos..."
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicRetira, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicObras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub



    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents PicClientes As System.Windows.Forms.PictureBox

    Friend WithEvents cmbObras As System.Windows.Forms.ComboBox
    Friend WithEvents PicObras As System.Windows.Forms.PictureBox


    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label

    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label

    Friend WithEvents lblObra As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbRetira As System.Windows.Forms.ComboBox
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnReimprimirVale As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtcopias As System.Windows.Forms.TextBox
    Friend WithEvents chkVerGrilla As System.Windows.Forms.CheckBox
    Friend WithEvents btnStock As System.Windows.Forms.Button
    Friend WithEvents chkVenta As System.Windows.Forms.CheckBox
    Friend WithEvents PicRetira As System.Windows.Forms.PictureBox
    Friend WithEvents lblTotalconIVA As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblTotalVentasinIVA As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblOrdendeCompra As System.Windows.Forms.Label
    Friend WithEvents txtOrdendeCompra As TextBoxConFormatoVB.FormattedTextBoxVB

End Class
