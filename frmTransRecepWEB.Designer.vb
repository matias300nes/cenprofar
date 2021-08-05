<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransRecepWEB
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPrueba = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.rdEnviados = New System.Windows.Forms.RadioButton()
        Me.chkCambiarPrecios = New System.Windows.Forms.CheckBox()
        Me.btnFinalizar = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblNroAsoc = New System.Windows.Forms.Label()
        Me.lblEncargado = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblFechaEmicion = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotalPedido = New System.Windows.Forms.Label()
        Me.lblDestino = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkAnuladas = New System.Windows.Forms.CheckBox()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.PicDescarga = New System.Windows.Forms.PictureBox()
        Me.btnDescargarPedidos = New System.Windows.Forms.Button()
        Me.txtIDPedido = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.rdTodasPed = New System.Windows.Forms.RadioButton()
        Me.rdPendientes = New System.Windows.Forms.RadioButton()
        Me.rdAnuladas = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblNroMov = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPedidos = New System.Windows.Forms.ComboBox()
        Me.lblTotalRecep = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.txtIDAlmacen = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbOrigen = New System.Windows.Forms.ComboBox()
        Me.btnEnviarTodos = New System.Windows.Forms.Button()
        Me.btnLlenarGrilla = New System.Windows.Forms.Button()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStripIVA = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItemIVA = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerDescargas = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicDescarga, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStripIVA.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtPrueba)
        Me.GroupBox1.Controls.Add(Me.rdEnviados)
        Me.GroupBox1.Controls.Add(Me.chkCambiarPrecios)
        Me.GroupBox1.Controls.Add(Me.btnFinalizar)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblNroAsoc)
        Me.GroupBox1.Controls.Add(Me.lblEncargado)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblFechaEmicion)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblTotalPedido)
        Me.GroupBox1.Controls.Add(Me.lblDestino)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkAnuladas)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblTipo)
        Me.GroupBox1.Controls.Add(Me.PicDescarga)
        Me.GroupBox1.Controls.Add(Me.btnDescargarPedidos)
        Me.GroupBox1.Controls.Add(Me.txtIDPedido)
        Me.GroupBox1.Controls.Add(Me.rdTodasPed)
        Me.GroupBox1.Controls.Add(Me.rdPendientes)
        Me.GroupBox1.Controls.Add(Me.rdAnuladas)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblNroMov)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbPedidos)
        Me.GroupBox1.Controls.Add(Me.lblTotalRecep)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.txtIDAlmacen)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbOrigen)
        Me.GroupBox1.Controls.Add(Me.btnEnviarTodos)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(9, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1381, 534)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtPrueba
        '
        Me.txtPrueba.AccessibleName = "Nota"
        Me.txtPrueba.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrueba.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrueba.Decimals = CType(2, Byte)
        Me.txtPrueba.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtPrueba.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPrueba.Location = New System.Drawing.Point(16, 439)
        Me.txtPrueba.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrueba.Name = "txtPrueba"
        Me.txtPrueba.Size = New System.Drawing.Size(1341, 22)
        Me.txtPrueba.TabIndex = 330
        Me.txtPrueba.Text_1 = Nothing
        Me.txtPrueba.Text_2 = Nothing
        Me.txtPrueba.Text_3 = Nothing
        Me.txtPrueba.Text_4 = Nothing
        Me.txtPrueba.UserValues = Nothing
        '
        'rdEnviados
        '
        Me.rdEnviados.AutoSize = True
        Me.rdEnviados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdEnviados.Location = New System.Drawing.Point(368, 491)
        Me.rdEnviados.Margin = New System.Windows.Forms.Padding(4)
        Me.rdEnviados.Name = "rdEnviados"
        Me.rdEnviados.Size = New System.Drawing.Size(95, 21)
        Me.rdEnviados.TabIndex = 17
        Me.rdEnviados.TabStop = True
        Me.rdEnviados.Text = "Enviados"
        Me.rdEnviados.UseVisualStyleBackColor = True
        '
        'chkCambiarPrecios
        '
        Me.chkCambiarPrecios.AccessibleName = "Eliminado"
        Me.chkCambiarPrecios.AutoSize = True
        Me.chkCambiarPrecios.Enabled = False
        Me.chkCambiarPrecios.Location = New System.Drawing.Point(1223, 489)
        Me.chkCambiarPrecios.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCambiarPrecios.Name = "chkCambiarPrecios"
        Me.chkCambiarPrecios.Size = New System.Drawing.Size(138, 21)
        Me.chkCambiarPrecios.TabIndex = 315
        Me.chkCambiarPrecios.Text = "Modificar Precios"
        Me.chkCambiarPrecios.UseVisualStyleBackColor = True
        Me.chkCambiarPrecios.Visible = False
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinalizar.ForeColor = System.Drawing.Color.Red
        Me.btnFinalizar.Location = New System.Drawing.Point(219, 78)
        Me.btnFinalizar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(143, 37)
        Me.btnFinalizar.TabIndex = 7
        Me.btnFinalizar.Text = "Finalizar Mov."
        Me.btnFinalizar.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(168, 16)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 17)
        Me.Label15.TabIndex = 329
        Me.Label15.Text = "Nro. Asoc."
        '
        'lblNroAsoc
        '
        Me.lblNroAsoc.BackColor = System.Drawing.Color.White
        Me.lblNroAsoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNroAsoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNroAsoc.Location = New System.Drawing.Point(171, 35)
        Me.lblNroAsoc.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNroAsoc.Name = "lblNroAsoc"
        Me.lblNroAsoc.Size = New System.Drawing.Size(118, 25)
        Me.lblNroAsoc.TabIndex = 1
        Me.lblNroAsoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEncargado
        '
        Me.lblEncargado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncargado.ForeColor = System.Drawing.Color.Blue
        Me.lblEncargado.Location = New System.Drawing.Point(608, 89)
        Me.lblEncargado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEncargado.Name = "lblEncargado"
        Me.lblEncargado.Size = New System.Drawing.Size(157, 26)
        Me.lblEncargado.TabIndex = 9
        Me.lblEncargado.Text = "Empleado"
        Me.lblEncargado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(655, 72)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 17)
        Me.Label14.TabIndex = 326
        Me.Label14.Text = "Encargado:"
        '
        'lblFechaEmicion
        '
        Me.lblFechaEmicion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaEmicion.ForeColor = System.Drawing.Color.Blue
        Me.lblFechaEmicion.Location = New System.Drawing.Point(821, 88)
        Me.lblFechaEmicion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaEmicion.Name = "lblFechaEmicion"
        Me.lblFechaEmicion.Size = New System.Drawing.Size(145, 26)
        Me.lblFechaEmicion.TabIndex = 10
        Me.lblFechaEmicion.Text = "- - / - - / - -"
        Me.lblFechaEmicion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(835, 70)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 17)
        Me.Label11.TabIndex = 324
        Me.Label11.Text = "Fecha de Emición:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(1003, 490)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 17)
        Me.Label12.TabIndex = 284
        Me.Label12.Text = "Total Pedido"
        '
        'lblTotalPedido
        '
        Me.lblTotalPedido.BackColor = System.Drawing.Color.White
        Me.lblTotalPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalPedido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPedido.Location = New System.Drawing.Point(1102, 487)
        Me.lblTotalPedido.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotalPedido.Name = "lblTotalPedido"
        Me.lblTotalPedido.Size = New System.Drawing.Size(113, 25)
        Me.lblTotalPedido.TabIndex = 287
        Me.lblTotalPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDestino
        '
        Me.lblDestino.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDestino.ForeColor = System.Drawing.Color.Black
        Me.lblDestino.Location = New System.Drawing.Point(1026, 89)
        Me.lblDestino.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(145, 26)
        Me.lblDestino.TabIndex = 11
        Me.lblDestino.Text = "DEPOSITO"
        Me.lblDestino.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1049, 71)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 17)
        Me.Label1.TabIndex = 322
        Me.Label1.Text = "Recepciona:"
        '
        'chkAnuladas
        '
        Me.chkAnuladas.AccessibleName = ""
        Me.chkAnuladas.AutoSize = True
        Me.chkAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnuladas.ForeColor = System.Drawing.Color.Red
        Me.chkAnuladas.Location = New System.Drawing.Point(815, 410)
        Me.chkAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.chkAnuladas.Name = "chkAnuladas"
        Me.chkAnuladas.Size = New System.Drawing.Size(225, 21)
        Me.chkAnuladas.TabIndex = 321
        Me.chkAnuladas.Text = "Ver Recepciones Anuladas"
        Me.chkAnuladas.UseVisualStyleBackColor = True
        Me.chkAnuladas.Visible = False
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(297, 37)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(118, 22)
        Me.dtpFECHA.TabIndex = 2
        Me.dtpFECHA.Tag = "202"
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(920, 36)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(443, 22)
        Me.txtNota.TabIndex = 5
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(919, 16)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 17)
        Me.Label8.TabIndex = 319
        Me.Label8.Text = "Nota"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(494, 71)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 17)
        Me.Label3.TabIndex = 317
        Me.Label3.Text = "Tipo"
        '
        'lblTipo
        '
        Me.lblTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipo.ForeColor = System.Drawing.Color.Green
        Me.lblTipo.Location = New System.Drawing.Point(445, 89)
        Me.lblTipo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(142, 26)
        Me.lblTipo.TabIndex = 8
        Me.lblTipo.Text = "TIPO"
        Me.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicDescarga
        '
        Me.PicDescarga.BackColor = System.Drawing.Color.White
        Me.PicDescarga.Location = New System.Drawing.Point(711, 490)
        Me.PicDescarga.Name = "PicDescarga"
        Me.PicDescarga.Size = New System.Drawing.Size(28, 22)
        Me.PicDescarga.TabIndex = 311
        Me.PicDescarga.TabStop = False
        '
        'btnDescargarPedidos
        '
        Me.btnDescargarPedidos.BackColor = System.Drawing.Color.White
        Me.btnDescargarPedidos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDescargarPedidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDescargarPedidos.Location = New System.Drawing.Point(519, 482)
        Me.btnDescargarPedidos.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDescargarPedidos.Name = "btnDescargarPedidos"
        Me.btnDescargarPedidos.Size = New System.Drawing.Size(231, 37)
        Me.btnDescargarPedidos.TabIndex = 18
        Me.btnDescargarPedidos.Text = "Descargar Movimientos"
        Me.btnDescargarPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDescargarPedidos.UseVisualStyleBackColor = False
        '
        'txtIDPedido
        '
        Me.txtIDPedido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDPedido.Decimals = CType(2, Byte)
        Me.txtIDPedido.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDPedido.Enabled = False
        Me.txtIDPedido.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDPedido.Location = New System.Drawing.Point(830, 8)
        Me.txtIDPedido.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDPedido.MaxLength = 8
        Me.txtIDPedido.Name = "txtIDPedido"
        Me.txtIDPedido.Size = New System.Drawing.Size(84, 22)
        Me.txtIDPedido.TabIndex = 308
        Me.txtIDPedido.Text_1 = Nothing
        Me.txtIDPedido.Text_2 = Nothing
        Me.txtIDPedido.Text_3 = Nothing
        Me.txtIDPedido.Text_4 = Nothing
        Me.txtIDPedido.UserValues = Nothing
        Me.txtIDPedido.Visible = False
        '
        'rdTodasPed
        '
        Me.rdTodasPed.AutoSize = True
        Me.rdTodasPed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTodasPed.Location = New System.Drawing.Point(272, 490)
        Me.rdTodasPed.Margin = New System.Windows.Forms.Padding(4)
        Me.rdTodasPed.Name = "rdTodasPed"
        Me.rdTodasPed.Size = New System.Drawing.Size(79, 21)
        Me.rdTodasPed.TabIndex = 16
        Me.rdTodasPed.TabStop = True
        Me.rdTodasPed.Text = "Todos "
        Me.rdTodasPed.UseVisualStyleBackColor = True
        '
        'rdPendientes
        '
        Me.rdPendientes.AutoSize = True
        Me.rdPendientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPendientes.Location = New System.Drawing.Point(15, 490)
        Me.rdPendientes.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPendientes.Name = "rdPendientes"
        Me.rdPendientes.Size = New System.Drawing.Size(110, 21)
        Me.rdPendientes.TabIndex = 14
        Me.rdPendientes.TabStop = True
        Me.rdPendientes.Text = "Pendientes"
        Me.rdPendientes.UseVisualStyleBackColor = True
        '
        'rdAnuladas
        '
        Me.rdAnuladas.AutoSize = True
        Me.rdAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAnuladas.Location = New System.Drawing.Point(143, 490)
        Me.rdAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAnuladas.Name = "rdAnuladas"
        Me.rdAnuladas.Size = New System.Drawing.Size(111, 21)
        Me.rdAnuladas.TabIndex = 15
        Me.rdAnuladas.TabStop = True
        Me.rdAnuladas.Text = "Finalizados"
        Me.rdAnuladas.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(280, 18)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(142, 17)
        Me.Label6.TabIndex = 296
        Me.Label6.Text = "Fecha de Recepción:"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(1211, 88)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(145, 26)
        Me.lblStatus.TabIndex = 12
        Me.lblStatus.Text = "STATUS"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1231, 70)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 17)
        Me.Label5.TabIndex = 294
        Me.Label5.Text = "Estado Pedido:"
        '
        'lblNroMov
        '
        Me.lblNroMov.BackColor = System.Drawing.Color.White
        Me.lblNroMov.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNroMov.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNroMov.Location = New System.Drawing.Point(15, 35)
        Me.lblNroMov.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNroMov.Name = "lblNroMov"
        Me.lblNroMov.Size = New System.Drawing.Size(148, 25)
        Me.lblNroMov.TabIndex = 0
        Me.lblNroMov.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(703, 14)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 17)
        Me.Label4.TabIndex = 291
        Me.Label4.Text = "Movimientos*"
        '
        'cmbPedidos
        '
        Me.cmbPedidos.AccessibleName = "*Pedidos"
        Me.cmbPedidos.DropDownHeight = 500
        Me.cmbPedidos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPedidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPedidos.FormattingEnabled = True
        Me.cmbPedidos.IntegralHeight = False
        Me.cmbPedidos.Location = New System.Drawing.Point(704, 35)
        Me.cmbPedidos.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPedidos.Name = "cmbPedidos"
        Me.cmbPedidos.Size = New System.Drawing.Size(208, 25)
        Me.cmbPedidos.TabIndex = 4
        '
        'lblTotalRecep
        '
        Me.lblTotalRecep.BackColor = System.Drawing.Color.White
        Me.lblTotalRecep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalRecep.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalRecep.Location = New System.Drawing.Point(868, 485)
        Me.lblTotalRecep.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotalRecep.Name = "lblTotalRecep"
        Me.lblTotalRecep.Size = New System.Drawing.Size(113, 25)
        Me.lblTotalRecep.TabIndex = 282
        Me.lblTotalRecep.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(774, 490)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 17)
        Me.Label7.TabIndex = 283
        Me.Label7.Text = "Total Recep."
        '
        'chkGrillaInferior
        '
        Me.chkGrillaInferior.AutoSize = True
        Me.chkGrillaInferior.Location = New System.Drawing.Point(256, 625)
        Me.chkGrillaInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGrillaInferior.Name = "chkGrillaInferior"
        Me.chkGrillaInferior.Size = New System.Drawing.Size(176, 21)
        Me.chkGrillaInferior.TabIndex = 272
        Me.chkGrillaInferior.Text = "Aumentar Grilla Inferior"
        Me.chkGrillaInferior.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 628)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(129, 17)
        Me.Label19.TabIndex = 271
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(145, 628)
        Me.lblCantidadFilas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(60, 17)
        Me.lblCantidadFilas.TabIndex = 270
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'txtIDAlmacen
        '
        Me.txtIDAlmacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDAlmacen.Decimals = CType(2, Byte)
        Me.txtIDAlmacen.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDAlmacen.Enabled = False
        Me.txtIDAlmacen.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtIDAlmacen.Location = New System.Drawing.Point(659, 9)
        Me.txtIDAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDAlmacen.MaxLength = 8
        Me.txtIDAlmacen.Name = "txtIDAlmacen"
        Me.txtIDAlmacen.Size = New System.Drawing.Size(29, 22)
        Me.txtIDAlmacen.TabIndex = 130
        Me.txtIDAlmacen.Text_1 = Nothing
        Me.txtIDAlmacen.Text_2 = Nothing
        Me.txtIDAlmacen.Text_3 = Nothing
        Me.txtIDAlmacen.Text_4 = Nothing
        Me.txtIDAlmacen.UserValues = Nothing
        Me.txtIDAlmacen.Visible = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtID.Location = New System.Drawing.Point(1456, 7)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(29, 22)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(430, 14)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 17)
        Me.Label9.TabIndex = 162
        Me.Label9.Text = "Origen"
        '
        'cmbOrigen
        '
        Me.cmbOrigen.AccessibleName = "*Origen"
        Me.cmbOrigen.DropDownHeight = 500
        Me.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrigen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrigen.FormattingEnabled = True
        Me.cmbOrigen.IntegralHeight = False
        Me.cmbOrigen.Location = New System.Drawing.Point(433, 35)
        Me.cmbOrigen.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbOrigen.Name = "cmbOrigen"
        Me.cmbOrigen.Size = New System.Drawing.Size(263, 25)
        Me.cmbOrigen.TabIndex = 3
        '
        'btnEnviarTodos
        '
        Me.btnEnviarTodos.Enabled = False
        Me.btnEnviarTodos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnviarTodos.Location = New System.Drawing.Point(15, 78)
        Me.btnEnviarTodos.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEnviarTodos.Name = "btnEnviarTodos"
        Me.btnEnviarTodos.Size = New System.Drawing.Size(190, 37)
        Me.btnEnviarTodos.TabIndex = 6
        Me.btnEnviarTodos.Text = "Recibir Todos"
        Me.btnEnviarTodos.UseVisualStyleBackColor = True
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(653, 410)
        Me.btnLlenarGrilla.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLlenarGrilla.Name = "btnLlenarGrilla"
        Me.btnLlenarGrilla.Size = New System.Drawing.Size(153, 28)
        Me.btnLlenarGrilla.TabIndex = 12
        Me.btnLlenarGrilla.Text = "Llenar Grilla"
        Me.btnLlenarGrilla.UseVisualStyleBackColor = True
        Me.btnLlenarGrilla.Visible = False
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(544, 415)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(91, 21)
        Me.chkEliminado.TabIndex = 116
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdItems.Location = New System.Drawing.Point(15, 134)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdItems.Size = New System.Drawing.Size(1350, 333)
        Me.grdItems.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 17)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro. Movimiento"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 84)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.BuscarToolStripMenuItem.Text = "Buscar..."
        Me.BuscarToolStripMenuItem.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem
        '
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 28)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'ContextMenuStripIVA
        '
        Me.ContextMenuStripIVA.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItemIVA})
        Me.ContextMenuStripIVA.Name = "ContextMenuStrip1"
        Me.ContextMenuStripIVA.Size = New System.Drawing.Size(170, 28)
        '
        'BorrarElItemToolStripMenuItemIVA
        '
        Me.BorrarElItemToolStripMenuItemIVA.Name = "BorrarElItemToolStripMenuItemIVA"
        Me.BorrarElItemToolStripMenuItemIVA.Size = New System.Drawing.Size(169, 24)
        Me.BorrarElItemToolStripMenuItemIVA.Text = "Borrar el Item"
        '
        'TimerDescargas
        '
        Me.TimerDescargas.Interval = 18000
        '
        'frmTransRecepWEB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1390, 735)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmTransRecepWEB"
        Me.Text = "frmMovimientos"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicDescarga, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStripIVA.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Friend WithEvents txtIDAlmacen As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnEnviarTodos As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbOrigen As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuStripIVA As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItemIVA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents chkGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalRecep As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPedidos As System.Windows.Forms.ComboBox
    Friend WithEvents lblNroMov As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents rdTodasPed As System.Windows.Forms.RadioButton
    Friend WithEvents rdPendientes As System.Windows.Forms.RadioButton
    Friend WithEvents rdAnuladas As System.Windows.Forms.RadioButton
    Friend WithEvents txtIDPedido As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents btnDescargarPedidos As System.Windows.Forms.Button
    Friend WithEvents TimerDescargas As System.Windows.Forms.Timer
    Friend WithEvents PicDescarga As System.Windows.Forms.PictureBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkAnuladas As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotalPedido As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkCambiarPrecios As System.Windows.Forms.CheckBox
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFechaEmicion As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblEncargado As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblNroAsoc As System.Windows.Forms.Label
    Friend WithEvents rdEnviados As System.Windows.Forms.RadioButton
    Friend WithEvents txtPrueba As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
