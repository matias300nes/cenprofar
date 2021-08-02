<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPedidosWEB
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
        Me.chkCambiarPrecios = New System.Windows.Forms.CheckBox()
        Me.txtIDRepartidor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbRepartidor = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PicDescarga = New System.Windows.Forms.PictureBox()
        Me.btnDescargarPedidos = New System.Windows.Forms.Button()
        Me.btnFinalizar = New System.Windows.Forms.Button()
        Me.txtIDPedido = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.PanelDescuento = New System.Windows.Forms.Panel()
        Me.chkDescuento = New System.Windows.Forms.CheckBox()
        Me.txtDescuento = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.rdPorcentaje = New System.Windows.Forms.RadioButton()
        Me.rdAbsoluto = New System.Windows.Forms.RadioButton()
        Me.rdTodasPed = New System.Windows.Forms.RadioButton()
        Me.rdPendientes = New System.Windows.Forms.RadioButton()
        Me.rdAnuladas = New System.Windows.Forms.RadioButton()
        Me.lblLugarEntrega = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblFechaEntrega = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtidAlmacen = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblNroPedido = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPedidos = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.chkFacturaCancelada = New System.Windows.Forms.CheckBox()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbClientes = New System.Windows.Forms.ComboBox()
        Me.btnEnviarTodos = New System.Windows.Forms.Button()
        Me.btnLlenarGrilla = New System.Windows.Forms.Button()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkAnuladas = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStripIVA = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItemIVA = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerDescargas = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicDescarga, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDescuento.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStripIVA.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkCambiarPrecios)
        Me.GroupBox1.Controls.Add(Me.txtIDRepartidor)
        Me.GroupBox1.Controls.Add(Me.cmbRepartidor)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.PicDescarga)
        Me.GroupBox1.Controls.Add(Me.btnDescargarPedidos)
        Me.GroupBox1.Controls.Add(Me.btnFinalizar)
        Me.GroupBox1.Controls.Add(Me.txtIDPedido)
        Me.GroupBox1.Controls.Add(Me.PanelDescuento)
        Me.GroupBox1.Controls.Add(Me.rdTodasPed)
        Me.GroupBox1.Controls.Add(Me.rdPendientes)
        Me.GroupBox1.Controls.Add(Me.rdAnuladas)
        Me.GroupBox1.Controls.Add(Me.lblLugarEntrega)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblFechaEntrega)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtidAlmacen)
        Me.GroupBox1.Controls.Add(Me.lblNroPedido)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbPedidos)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbAlmacenes)
        Me.GroupBox1.Controls.Add(Me.lblSubtotal)
        Me.GroupBox1.Controls.Add(Me.lblIVA)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.chkGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.chkFacturaCancelada)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbClientes)
        Me.GroupBox1.Controls.Add(Me.btnEnviarTodos)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkAnuladas)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(9, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1814, 539)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkCambiarPrecios
        '
        Me.chkCambiarPrecios.AccessibleName = "Eliminado"
        Me.chkCambiarPrecios.AutoSize = True
        Me.chkCambiarPrecios.Enabled = False
        Me.chkCambiarPrecios.Location = New System.Drawing.Point(578, 103)
        Me.chkCambiarPrecios.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCambiarPrecios.Name = "chkCambiarPrecios"
        Me.chkCambiarPrecios.Size = New System.Drawing.Size(138, 21)
        Me.chkCambiarPrecios.TabIndex = 315
        Me.chkCambiarPrecios.Text = "Modificar Precios"
        Me.chkCambiarPrecios.UseVisualStyleBackColor = True
        '
        'txtIDRepartidor
        '
        Me.txtIDRepartidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDRepartidor.Decimals = CType(2, Byte)
        Me.txtIDRepartidor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDRepartidor.Enabled = False
        Me.txtIDRepartidor.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDRepartidor.Location = New System.Drawing.Point(1241, 11)
        Me.txtIDRepartidor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDRepartidor.MaxLength = 8
        Me.txtIDRepartidor.Name = "txtIDRepartidor"
        Me.txtIDRepartidor.Size = New System.Drawing.Size(29, 22)
        Me.txtIDRepartidor.TabIndex = 314
        Me.txtIDRepartidor.Text_1 = Nothing
        Me.txtIDRepartidor.Text_2 = Nothing
        Me.txtIDRepartidor.Text_3 = Nothing
        Me.txtIDRepartidor.Text_4 = Nothing
        Me.txtIDRepartidor.UserValues = Nothing
        Me.txtIDRepartidor.Visible = False
        '
        'cmbRepartidor
        '
        Me.cmbRepartidor.AccessibleName = "*Repartidor"
        Me.cmbRepartidor.DropDownHeight = 500
        Me.cmbRepartidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRepartidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRepartidor.FormattingEnabled = True
        Me.cmbRepartidor.IntegralHeight = False
        Me.cmbRepartidor.Location = New System.Drawing.Point(1074, 35)
        Me.cmbRepartidor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbRepartidor.Name = "cmbRepartidor"
        Me.cmbRepartidor.Size = New System.Drawing.Size(244, 25)
        Me.cmbRepartidor.TabIndex = 313
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1074, 15)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 17)
        Me.Label10.TabIndex = 312
        Me.Label10.Text = "Repartidor*"
        '
        'PicDescarga
        '
        Me.PicDescarga.BackColor = System.Drawing.Color.White
        Me.PicDescarga.Location = New System.Drawing.Point(542, 102)
        Me.PicDescarga.Name = "PicDescarga"
        Me.PicDescarga.Size = New System.Drawing.Size(16, 16)
        Me.PicDescarga.TabIndex = 311
        Me.PicDescarga.TabStop = False
        '
        'btnDescargarPedidos
        '
        Me.btnDescargarPedidos.BackColor = System.Drawing.Color.White
        Me.btnDescargarPedidos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDescargarPedidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDescargarPedidos.Location = New System.Drawing.Point(383, 92)
        Me.btnDescargarPedidos.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDescargarPedidos.Name = "btnDescargarPedidos"
        Me.btnDescargarPedidos.Size = New System.Drawing.Size(187, 37)
        Me.btnDescargarPedidos.TabIndex = 310
        Me.btnDescargarPedidos.Text = " Descargar Pedidos"
        Me.btnDescargarPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDescargarPedidos.UseVisualStyleBackColor = False
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinalizar.Location = New System.Drawing.Point(239, 92)
        Me.btnFinalizar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(136, 37)
        Me.btnFinalizar.TabIndex = 309
        Me.btnFinalizar.Text = "Finalizar Envío"
        Me.btnFinalizar.UseVisualStyleBackColor = True
        '
        'txtIDPedido
        '
        Me.txtIDPedido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDPedido.Decimals = CType(2, Byte)
        Me.txtIDPedido.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDPedido.Enabled = False
        Me.txtIDPedido.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDPedido.Location = New System.Drawing.Point(982, 10)
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
        'PanelDescuento
        '
        Me.PanelDescuento.Controls.Add(Me.chkDescuento)
        Me.PanelDescuento.Controls.Add(Me.txtDescuento)
        Me.PanelDescuento.Controls.Add(Me.rdPorcentaje)
        Me.PanelDescuento.Controls.Add(Me.rdAbsoluto)
        Me.PanelDescuento.Location = New System.Drawing.Point(916, 472)
        Me.PanelDescuento.Name = "PanelDescuento"
        Me.PanelDescuento.Size = New System.Drawing.Size(214, 60)
        Me.PanelDescuento.TabIndex = 307
        '
        'chkDescuento
        '
        Me.chkDescuento.AutoSize = True
        Me.chkDescuento.BackColor = System.Drawing.Color.Transparent
        Me.chkDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDescuento.ForeColor = System.Drawing.Color.Blue
        Me.chkDescuento.Location = New System.Drawing.Point(16, 3)
        Me.chkDescuento.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDescuento.Name = "chkDescuento"
        Me.chkDescuento.Size = New System.Drawing.Size(159, 22)
        Me.chkDescuento.TabIndex = 303
        Me.chkDescuento.Text = "Habilitar Descuento"
        Me.chkDescuento.UseVisualStyleBackColor = False
        '
        'txtDescuento
        '
        Me.txtDescuento.AccessibleName = ""
        Me.txtDescuento.Decimals = CType(2, Byte)
        Me.txtDescuento.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescuento.Enabled = False
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtDescuento.Location = New System.Drawing.Point(112, 29)
        Me.txtDescuento.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescuento.MaxLength = 100
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(79, 24)
        Me.txtDescuento.TabIndex = 306
        Me.txtDescuento.Text_1 = Nothing
        Me.txtDescuento.Text_2 = Nothing
        Me.txtDescuento.Text_3 = Nothing
        Me.txtDescuento.Text_4 = Nothing
        Me.txtDescuento.UserValues = Nothing
        '
        'rdPorcentaje
        '
        Me.rdPorcentaje.AutoSize = True
        Me.rdPorcentaje.BackColor = System.Drawing.Color.Transparent
        Me.rdPorcentaje.Enabled = False
        Me.rdPorcentaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPorcentaje.Location = New System.Drawing.Point(16, 30)
        Me.rdPorcentaje.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPorcentaje.Name = "rdPorcentaje"
        Me.rdPorcentaje.Size = New System.Drawing.Size(43, 22)
        Me.rdPorcentaje.TabIndex = 304
        Me.rdPorcentaje.TabStop = True
        Me.rdPorcentaje.Text = "%"
        Me.rdPorcentaje.UseVisualStyleBackColor = False
        '
        'rdAbsoluto
        '
        Me.rdAbsoluto.AutoSize = True
        Me.rdAbsoluto.BackColor = System.Drawing.Color.Transparent
        Me.rdAbsoluto.Enabled = False
        Me.rdAbsoluto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAbsoluto.Location = New System.Drawing.Point(67, 30)
        Me.rdAbsoluto.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAbsoluto.Name = "rdAbsoluto"
        Me.rdAbsoluto.Size = New System.Drawing.Size(38, 22)
        Me.rdAbsoluto.TabIndex = 305
        Me.rdAbsoluto.TabStop = True
        Me.rdAbsoluto.Text = "$"
        Me.rdAbsoluto.UseVisualStyleBackColor = False
        '
        'rdTodasPed
        '
        Me.rdTodasPed.AutoSize = True
        Me.rdTodasPed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTodasPed.Location = New System.Drawing.Point(312, 490)
        Me.rdTodasPed.Margin = New System.Windows.Forms.Padding(4)
        Me.rdTodasPed.Name = "rdTodasPed"
        Me.rdTodasPed.Size = New System.Drawing.Size(112, 21)
        Me.rdTodasPed.TabIndex = 302
        Me.rdTodasPed.TabStop = True
        Me.rdTodasPed.Text = "Todos Ped."
        Me.rdTodasPed.UseVisualStyleBackColor = True
        '
        'rdPendientes
        '
        Me.rdPendientes.AutoSize = True
        Me.rdPendientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPendientes.Location = New System.Drawing.Point(15, 490)
        Me.rdPendientes.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPendientes.Name = "rdPendientes"
        Me.rdPendientes.Size = New System.Drawing.Size(148, 21)
        Me.rdPendientes.TabIndex = 301
        Me.rdPendientes.TabStop = True
        Me.rdPendientes.Text = "Ped. Pendientes"
        Me.rdPendientes.UseVisualStyleBackColor = True
        '
        'rdAnuladas
        '
        Me.rdAnuladas.AutoSize = True
        Me.rdAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAnuladas.Location = New System.Drawing.Point(171, 490)
        Me.rdAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAnuladas.Name = "rdAnuladas"
        Me.rdAnuladas.Size = New System.Drawing.Size(134, 21)
        Me.rdAnuladas.TabIndex = 300
        Me.rdAnuladas.TabStop = True
        Me.rdAnuladas.Text = "Ped. Anulados"
        Me.rdAnuladas.UseVisualStyleBackColor = True
        '
        'lblLugarEntrega
        '
        Me.lblLugarEntrega.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLugarEntrega.ForeColor = System.Drawing.Color.Blue
        Me.lblLugarEntrega.Location = New System.Drawing.Point(1345, 103)
        Me.lblLugarEntrega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLugarEntrega.Name = "lblLugarEntrega"
        Me.lblLugarEntrega.Size = New System.Drawing.Size(186, 26)
        Me.lblLugarEntrega.TabIndex = 299
        Me.lblLugarEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1372, 80)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 17)
        Me.Label11.TabIndex = 298
        Me.Label11.Text = "Lugar de Entrega:"
        '
        'lblFechaEntrega
        '
        Me.lblFechaEntrega.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaEntrega.ForeColor = System.Drawing.Color.Blue
        Me.lblFechaEntrega.Location = New System.Drawing.Point(1164, 103)
        Me.lblFechaEntrega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaEntrega.Name = "lblFechaEntrega"
        Me.lblFechaEntrega.Size = New System.Drawing.Size(145, 26)
        Me.lblFechaEntrega.TabIndex = 297
        Me.lblFechaEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1175, 80)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 17)
        Me.Label6.TabIndex = 296
        Me.Label6.Text = "Fecha de Entrega:"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(985, 103)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(145, 26)
        Me.lblStatus.TabIndex = 295
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1005, 80)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 17)
        Me.Label5.TabIndex = 294
        Me.Label5.Text = "Estado Pedido:"
        '
        'txtidAlmacen
        '
        Me.txtidAlmacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidAlmacen.Decimals = CType(2, Byte)
        Me.txtidAlmacen.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidAlmacen.Enabled = False
        Me.txtidAlmacen.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidAlmacen.Location = New System.Drawing.Point(451, 10)
        Me.txtidAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.txtidAlmacen.MaxLength = 8
        Me.txtidAlmacen.Name = "txtidAlmacen"
        Me.txtidAlmacen.Size = New System.Drawing.Size(29, 22)
        Me.txtidAlmacen.TabIndex = 293
        Me.txtidAlmacen.Text_1 = Nothing
        Me.txtidAlmacen.Text_2 = Nothing
        Me.txtidAlmacen.Text_3 = Nothing
        Me.txtidAlmacen.Text_4 = Nothing
        Me.txtidAlmacen.UserValues = Nothing
        Me.txtidAlmacen.Visible = False
        '
        'lblNroPedido
        '
        Me.lblNroPedido.BackColor = System.Drawing.Color.White
        Me.lblNroPedido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNroPedido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNroPedido.Location = New System.Drawing.Point(15, 35)
        Me.lblNroPedido.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNroPedido.Name = "lblNroPedido"
        Me.lblNroPedido.Size = New System.Drawing.Size(133, 25)
        Me.lblNroPedido.TabIndex = 292
        Me.lblNroPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(855, 15)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 17)
        Me.Label4.TabIndex = 291
        Me.Label4.Text = "Pedidos*"
        '
        'cmbPedidos
        '
        Me.cmbPedidos.AccessibleName = "*Pedidos"
        Me.cmbPedidos.DropDownHeight = 500
        Me.cmbPedidos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPedidos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPedidos.FormattingEnabled = True
        Me.cmbPedidos.IntegralHeight = False
        Me.cmbPedidos.Location = New System.Drawing.Point(858, 35)
        Me.cmbPedidos.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPedidos.Name = "cmbPedidos"
        Me.cmbPedidos.Size = New System.Drawing.Size(208, 25)
        Me.cmbPedidos.TabIndex = 290
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(296, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 17)
        Me.Label1.TabIndex = 289
        Me.Label1.Text = "Almacen*"
        '
        'cmbAlmacenes
        '
        Me.cmbAlmacenes.AccessibleName = "*Almacen"
        Me.cmbAlmacenes.DropDownHeight = 500
        Me.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAlmacenes.FormattingEnabled = True
        Me.cmbAlmacenes.IntegralHeight = False
        Me.cmbAlmacenes.Location = New System.Drawing.Point(299, 36)
        Me.cmbAlmacenes.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAlmacenes.Name = "cmbAlmacenes"
        Me.cmbAlmacenes.Size = New System.Drawing.Size(184, 25)
        Me.cmbAlmacenes.TabIndex = 288
        '
        'lblSubtotal
        '
        Me.lblSubtotal.BackColor = System.Drawing.Color.White
        Me.lblSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtotal.Location = New System.Drawing.Point(1203, 486)
        Me.lblSubtotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(113, 25)
        Me.lblSubtotal.TabIndex = 287
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIVA
        '
        Me.lblIVA.BackColor = System.Drawing.Color.White
        Me.lblIVA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIVA.Location = New System.Drawing.Point(1018, 485)
        Me.lblIVA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(113, 25)
        Me.lblIVA.TabIndex = 286
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIVA.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(983, 489)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(29, 17)
        Me.Label15.TabIndex = 285
        Me.Label15.Text = "IVA"
        Me.Label15.Visible = False
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(1369, 485)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(113, 25)
        Me.lblTotal.TabIndex = 282
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(1137, 490)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(60, 17)
        Me.Label12.TabIndex = 284
        Me.Label12.Text = "Subtotal"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1323, 489)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 17)
        Me.Label7.TabIndex = 283
        Me.Label7.Text = "Total"
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
        'chkFacturaCancelada
        '
        Me.chkFacturaCancelada.AutoSize = True
        Me.chkFacturaCancelada.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFacturaCancelada.Location = New System.Drawing.Point(1578, 97)
        Me.chkFacturaCancelada.Margin = New System.Windows.Forms.Padding(4)
        Me.chkFacturaCancelada.Name = "chkFacturaCancelada"
        Me.chkFacturaCancelada.Size = New System.Drawing.Size(204, 22)
        Me.chkFacturaCancelada.TabIndex = 27
        Me.chkFacturaCancelada.Text = "Pago Efectivo Contado"
        Me.chkFacturaCancelada.UseVisualStyleBackColor = True
        Me.chkFacturaCancelada.Visible = False
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(818, 10)
        Me.txtIdCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(29, 22)
        Me.txtIdCliente.TabIndex = 130
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtID.Location = New System.Drawing.Point(1753, 12)
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
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(1326, 35)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(411, 22)
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
        Me.Label8.Location = New System.Drawing.Point(1325, 14)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 17)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(488, 17)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 17)
        Me.Label9.TabIndex = 162
        Me.Label9.Text = "Clientes*"
        '
        'cmbClientes
        '
        Me.cmbClientes.AccessibleName = "*Clientes"
        Me.cmbClientes.DropDownHeight = 500
        Me.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClientes.FormattingEnabled = True
        Me.cmbClientes.IntegralHeight = False
        Me.cmbClientes.Location = New System.Drawing.Point(491, 35)
        Me.cmbClientes.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(359, 25)
        Me.cmbClientes.TabIndex = 4
        '
        'btnEnviarTodos
        '
        Me.btnEnviarTodos.Enabled = False
        Me.btnEnviarTodos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnviarTodos.Location = New System.Drawing.Point(15, 92)
        Me.btnEnviarTodos.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEnviarTodos.Name = "btnEnviarTodos"
        Me.btnEnviarTodos.Size = New System.Drawing.Size(216, 37)
        Me.btnEnviarTodos.TabIndex = 26
        Me.btnEnviarTodos.Text = "Envíar Todos"
        Me.btnEnviarTodos.UseVisualStyleBackColor = True
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(680, 68)
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
        Me.chkEliminado.Location = New System.Drawing.Point(828, 98)
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
        Me.grdItems.Location = New System.Drawing.Point(15, 153)
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
        Me.grdItems.Size = New System.Drawing.Size(1769, 314)
        Me.grdItems.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 17)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro. Pedido"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(156, 38)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 22)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(152, 18)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 17)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'chkAnuladas
        '
        Me.chkAnuladas.AccessibleName = ""
        Me.chkAnuladas.AutoSize = True
        Me.chkAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnuladas.ForeColor = System.Drawing.Color.Red
        Me.chkAnuladas.Location = New System.Drawing.Point(1559, 228)
        Me.chkAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.chkAnuladas.Name = "chkAnuladas"
        Me.chkAnuladas.Size = New System.Drawing.Size(225, 21)
        Me.chkAnuladas.TabIndex = 28
        Me.chkAnuladas.Text = "Ver Recepciones Anuladas"
        Me.chkAnuladas.UseVisualStyleBackColor = True
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
        'frmPedidosWEB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1816, 752)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmPedidosWEB"
        Me.Text = "frmEnvíoPedidos"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicDescarga, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDescuento.ResumeLayout(False)
        Me.PanelDescuento.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStripIVA.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnEnviarTodos As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents chkFacturaCancelada As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnuladas As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStripIVA As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItemIVA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents chkGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPedidos As System.Windows.Forms.ComboBox
    Friend WithEvents lblNroPedido As System.Windows.Forms.Label
    Friend WithEvents txtidAlmacen As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblLugarEntrega As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblFechaEntrega As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents rdTodasPed As System.Windows.Forms.RadioButton
    Friend WithEvents rdPendientes As System.Windows.Forms.RadioButton
    Friend WithEvents rdAnuladas As System.Windows.Forms.RadioButton
    Friend WithEvents txtDescuento As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents rdAbsoluto As System.Windows.Forms.RadioButton
    Friend WithEvents rdPorcentaje As System.Windows.Forms.RadioButton
    Friend WithEvents chkDescuento As System.Windows.Forms.CheckBox
    Friend WithEvents PanelDescuento As System.Windows.Forms.Panel
    Friend WithEvents txtIDPedido As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents btnDescargarPedidos As System.Windows.Forms.Button
    Friend WithEvents TimerDescargas As System.Windows.Forms.Timer
    Friend WithEvents PicDescarga As System.Windows.Forms.PictureBox
    Friend WithEvents txtIDRepartidor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbRepartidor As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkCambiarPrecios As System.Windows.Forms.CheckBox
End Class
