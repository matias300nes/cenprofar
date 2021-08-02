<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVentasWEB
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStripIVA = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItemIVA = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerDescargas = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Line1 = New DevComponents.DotNetBar.Controls.Line()
        Me.chkAnuladas = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.btnLlenarGrilla = New System.Windows.Forms.Button()
        Me.cmbClientes = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.chkGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.cmbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNroPedido = New System.Windows.Forms.Label()
        Me.txtidAlmacen = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblFechaEntrega = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblLugarEntrega = New System.Windows.Forms.Label()
        Me.rdAnuladas = New System.Windows.Forms.RadioButton()
        Me.rdPendientes = New System.Windows.Forms.RadioButton()
        Me.rdTodasPed = New System.Windows.Forms.RadioButton()
        Me.PanelDescuento = New System.Windows.Forms.Panel()
        Me.chkDescuento = New System.Windows.Forms.CheckBox()
        Me.txtDescuento = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.rdPorcentaje = New System.Windows.Forms.RadioButton()
        Me.rdAbsoluto = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbRepartidor = New System.Windows.Forms.ComboBox()
        Me.txtIDRepartidor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCantidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.txtPeso = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrdenItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodMaterial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EquipoHerramienta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodMarca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc_Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Peso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecioUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descuento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubtotalFinal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescuentoUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaCumplido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nota = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Stock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eliminado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CantidadPACK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reintegrar_Stock = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Bonificacion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Promo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDPrecioLista = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDMarca = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMarca = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPrecioVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PicClientes = New System.Windows.Forms.PictureBox()
        Me.PicRepartidor = New System.Windows.Forms.PictureBox()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnActualizarMat = New System.Windows.Forms.Button()
        Me.chkVentas = New System.Windows.Forms.CheckBox()
        Me.chkDevolucion = New System.Windows.Forms.CheckBox()
        Me.chkTransferencia = New System.Windows.Forms.CheckBox()
        Me.chkReintegrarStock = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblPromo = New System.Windows.Forms.Label()
        Me.txtValorPromo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnXNorte = New DevComponents.DotNetBar.ButtonX()
        Me.lblDescripcionPromo = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblAutorizado = New System.Windows.Forms.Label()
        Me.chkFacturaCancelada = New System.Windows.Forms.CheckBox()
        Me.chkPresupuesto = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblSaldo = New System.Windows.Forms.Label()
        Me.txtIDIngreso = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PanelTotales = New System.Windows.Forms.Panel()
        Me.GroupBoxCaja = New System.Windows.Forms.GroupBox()
        Me.btnRetiros = New DevComponents.DotNetBar.ButtonX()
        Me.btnApCaja = New DevComponents.DotNetBar.ButtonX()
        Me.btnIngresos = New DevComponents.DotNetBar.ButtonX()
        Me.btnGastos = New DevComponents.DotNetBar.ButtonX()
        Me.chkMayorista = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtCodigoIngreso = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStripIVA.SuspendLayout()
        Me.PanelDescuento.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicRepartidor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.PanelTotales.SuspendLayout()
        Me.GroupBoxCaja.SuspendLayout()
        Me.SuspendLayout()
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
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.Color.Transparent
        Me.Line1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Line1.Location = New System.Drawing.Point(151, 16)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(20, 79)
        Me.Line1.TabIndex = 351
        Me.Line1.Text = "Line1"
        Me.Line1.VerticalLine = True
        '
        'chkAnuladas
        '
        Me.chkAnuladas.AccessibleName = ""
        Me.chkAnuladas.AutoSize = True
        Me.chkAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnuladas.ForeColor = System.Drawing.Color.Red
        Me.chkAnuladas.Location = New System.Drawing.Point(1513, 231)
        Me.chkAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.chkAnuladas.Name = "chkAnuladas"
        Me.chkAnuladas.Size = New System.Drawing.Size(225, 21)
        Me.chkAnuladas.TabIndex = 28
        Me.chkAnuladas.Text = "Ver Recepciones Anuladas"
        Me.chkAnuladas.UseVisualStyleBackColor = True
        Me.chkAnuladas.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(315, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 15)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(318, 39)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(112, 21)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(174, 19)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 15)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro. Pedido"
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(1620, 12)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(85, 19)
        Me.chkEliminado.TabIndex = 116
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(1610, 83)
        Me.btnLlenarGrilla.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLlenarGrilla.Name = "btnLlenarGrilla"
        Me.btnLlenarGrilla.Size = New System.Drawing.Size(153, 28)
        Me.btnLlenarGrilla.TabIndex = 12
        Me.btnLlenarGrilla.Text = "Llenar Grilla"
        Me.btnLlenarGrilla.UseVisualStyleBackColor = True
        Me.btnLlenarGrilla.Visible = False
        '
        'cmbClientes
        '
        Me.cmbClientes.AccessibleName = "*Clientes"
        Me.cmbClientes.DropDownHeight = 500
        Me.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClientes.FormattingEnabled = True
        Me.cmbClientes.IntegralHeight = False
        Me.cmbClientes.Location = New System.Drawing.Point(634, 36)
        Me.cmbClientes.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(359, 25)
        Me.cmbClientes.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(631, 18)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 15)
        Me.Label9.TabIndex = 162
        Me.Label9.Text = "Clientes*"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1308, 20)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 15)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(1311, 39)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(452, 21)
        Me.txtNota.TabIndex = 5
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(739, 13)
        Me.txtIdCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(29, 21)
        Me.txtIdCliente.TabIndex = 130
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(145, 459)
        Me.lblCantidadFilas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(37, 15)
        Me.lblCantidadFilas.TabIndex = 270
        Me.lblCantidadFilas.Text = "Items"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 628)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(112, 15)
        Me.Label19.TabIndex = 271
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'chkGrillaInferior
        '
        Me.chkGrillaInferior.AutoSize = True
        Me.chkGrillaInferior.Location = New System.Drawing.Point(256, 625)
        Me.chkGrillaInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGrillaInferior.Name = "chkGrillaInferior"
        Me.chkGrillaInferior.Size = New System.Drawing.Size(155, 19)
        Me.chkGrillaInferior.TabIndex = 272
        Me.chkGrillaInferior.Text = "Aumentar Grilla Inferior"
        Me.chkGrillaInferior.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(724, 26)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 15)
        Me.Label7.TabIndex = 283
        Me.Label7.Text = "Total"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(538, 27)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 15)
        Me.Label12.TabIndex = 284
        Me.Label12.Text = "Subtotal"
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(770, 22)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(113, 25)
        Me.lblTotal.TabIndex = 282
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(869, 368)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(24, 15)
        Me.Label15.TabIndex = 285
        Me.Label15.Text = "IVA"
        Me.Label15.Visible = False
        '
        'lblIVA
        '
        Me.lblIVA.BackColor = System.Drawing.Color.White
        Me.lblIVA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIVA.Location = New System.Drawing.Point(748, 363)
        Me.lblIVA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(113, 25)
        Me.lblIVA.TabIndex = 286
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIVA.Visible = False
        '
        'lblSubtotal
        '
        Me.lblSubtotal.BackColor = System.Drawing.Color.White
        Me.lblSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtotal.Location = New System.Drawing.Point(604, 23)
        Me.lblSubtotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(113, 25)
        Me.lblSubtotal.TabIndex = 287
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAlmacenes
        '
        Me.cmbAlmacenes.AccessibleName = "*Almacen"
        Me.cmbAlmacenes.DropDownHeight = 500
        Me.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAlmacenes.FormattingEnabled = True
        Me.cmbAlmacenes.IntegralHeight = False
        Me.cmbAlmacenes.Location = New System.Drawing.Point(442, 37)
        Me.cmbAlmacenes.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAlmacenes.Name = "cmbAlmacenes"
        Me.cmbAlmacenes.Size = New System.Drawing.Size(184, 25)
        Me.cmbAlmacenes.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(439, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 289
        Me.Label1.Text = "Almacen*"
        '
        'lblNroPedido
        '
        Me.lblNroPedido.BackColor = System.Drawing.Color.White
        Me.lblNroPedido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNroPedido.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNroPedido.Location = New System.Drawing.Point(177, 36)
        Me.lblNroPedido.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNroPedido.Name = "lblNroPedido"
        Me.lblNroPedido.Size = New System.Drawing.Size(133, 25)
        Me.lblNroPedido.TabIndex = 0
        Me.lblNroPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtidAlmacen
        '
        Me.txtidAlmacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidAlmacen.Decimals = CType(2, Byte)
        Me.txtidAlmacen.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidAlmacen.Enabled = False
        Me.txtidAlmacen.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidAlmacen.Location = New System.Drawing.Point(594, 11)
        Me.txtidAlmacen.Margin = New System.Windows.Forms.Padding(4)
        Me.txtidAlmacen.MaxLength = 8
        Me.txtidAlmacen.Name = "txtidAlmacen"
        Me.txtidAlmacen.Size = New System.Drawing.Size(29, 21)
        Me.txtidAlmacen.TabIndex = 293
        Me.txtidAlmacen.Text_1 = Nothing
        Me.txtidAlmacen.Text_2 = Nothing
        Me.txtidAlmacen.Text_3 = Nothing
        Me.txtidAlmacen.Text_4 = Nothing
        Me.txtidAlmacen.UserValues = Nothing
        Me.txtidAlmacen.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1221, 95)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 15)
        Me.Label6.TabIndex = 296
        Me.Label6.Text = "Fecha de Entrega:"
        '
        'lblFechaEntrega
        '
        Me.lblFechaEntrega.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaEntrega.ForeColor = System.Drawing.Color.Blue
        Me.lblFechaEntrega.Location = New System.Drawing.Point(1214, 114)
        Me.lblFechaEntrega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaEntrega.Name = "lblFechaEntrega"
        Me.lblFechaEntrega.Size = New System.Drawing.Size(145, 26)
        Me.lblFechaEntrega.TabIndex = 297
        Me.lblFechaEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1393, 94)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(105, 15)
        Me.Label11.TabIndex = 298
        Me.Label11.Text = "Lugar de Entrega:"
        '
        'lblLugarEntrega
        '
        Me.lblLugarEntrega.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLugarEntrega.ForeColor = System.Drawing.Color.Blue
        Me.lblLugarEntrega.Location = New System.Drawing.Point(1392, 116)
        Me.lblLugarEntrega.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLugarEntrega.Name = "lblLugarEntrega"
        Me.lblLugarEntrega.Size = New System.Drawing.Size(371, 26)
        Me.lblLugarEntrega.TabIndex = 299
        Me.lblLugarEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdAnuladas
        '
        Me.rdAnuladas.AutoSize = True
        Me.rdAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAnuladas.Location = New System.Drawing.Point(103, 490)
        Me.rdAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAnuladas.Name = "rdAnuladas"
        Me.rdAnuladas.Size = New System.Drawing.Size(96, 21)
        Me.rdAnuladas.TabIndex = 300
        Me.rdAnuladas.TabStop = True
        Me.rdAnuladas.Text = "Anulados"
        Me.rdAnuladas.UseVisualStyleBackColor = True
        '
        'rdPendientes
        '
        Me.rdPendientes.AutoSize = True
        Me.rdPendientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPendientes.Location = New System.Drawing.Point(16, 518)
        Me.rdPendientes.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPendientes.Name = "rdPendientes"
        Me.rdPendientes.Size = New System.Drawing.Size(148, 21)
        Me.rdPendientes.TabIndex = 301
        Me.rdPendientes.TabStop = True
        Me.rdPendientes.Text = "Ped. Pendientes"
        Me.rdPendientes.UseVisualStyleBackColor = True
        Me.rdPendientes.Visible = False
        '
        'rdTodasPed
        '
        Me.rdTodasPed.AutoSize = True
        Me.rdTodasPed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTodasPed.Location = New System.Drawing.Point(16, 489)
        Me.rdTodasPed.Margin = New System.Windows.Forms.Padding(4)
        Me.rdTodasPed.Name = "rdTodasPed"
        Me.rdTodasPed.Size = New System.Drawing.Size(74, 21)
        Me.rdTodasPed.TabIndex = 302
        Me.rdTodasPed.TabStop = True
        Me.rdTodasPed.Text = "Todos"
        Me.rdTodasPed.UseVisualStyleBackColor = True
        '
        'PanelDescuento
        '
        Me.PanelDescuento.Controls.Add(Me.chkDescuento)
        Me.PanelDescuento.Controls.Add(Me.txtDescuento)
        Me.PanelDescuento.Controls.Add(Me.rdPorcentaje)
        Me.PanelDescuento.Controls.Add(Me.rdAbsoluto)
        Me.PanelDescuento.Location = New System.Drawing.Point(294, 9)
        Me.PanelDescuento.Name = "PanelDescuento"
        Me.PanelDescuento.Size = New System.Drawing.Size(225, 60)
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
        Me.chkDescuento.Size = New System.Drawing.Size(206, 22)
        Me.chkDescuento.TabIndex = 303
        Me.chkDescuento.Text = "Habilitar Descuento Global"
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
        Me.txtDescuento.Location = New System.Drawing.Point(133, 32)
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
        Me.rdPorcentaje.Location = New System.Drawing.Point(36, 33)
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
        Me.rdAbsoluto.Location = New System.Drawing.Point(87, 33)
        Me.rdAbsoluto.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAbsoluto.Name = "rdAbsoluto"
        Me.rdAbsoluto.Size = New System.Drawing.Size(38, 22)
        Me.rdAbsoluto.TabIndex = 305
        Me.rdAbsoluto.TabStop = True
        Me.rdAbsoluto.Text = "$"
        Me.rdAbsoluto.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1030, 18)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 15)
        Me.Label10.TabIndex = 312
        Me.Label10.Text = "Repartidor*"
        '
        'cmbRepartidor
        '
        Me.cmbRepartidor.AccessibleName = "*Repartidor"
        Me.cmbRepartidor.DropDownHeight = 500
        Me.cmbRepartidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRepartidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRepartidor.FormattingEnabled = True
        Me.cmbRepartidor.IntegralHeight = False
        Me.cmbRepartidor.Location = New System.Drawing.Point(1033, 37)
        Me.cmbRepartidor.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbRepartidor.Name = "cmbRepartidor"
        Me.cmbRepartidor.Size = New System.Drawing.Size(244, 25)
        Me.cmbRepartidor.TabIndex = 4
        '
        'txtIDRepartidor
        '
        Me.txtIDRepartidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDRepartidor.Decimals = CType(2, Byte)
        Me.txtIDRepartidor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDRepartidor.Enabled = False
        Me.txtIDRepartidor.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDRepartidor.Location = New System.Drawing.Point(1215, 11)
        Me.txtIDRepartidor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDRepartidor.MaxLength = 8
        Me.txtIDRepartidor.Name = "txtIDRepartidor"
        Me.txtIDRepartidor.Size = New System.Drawing.Size(29, 21)
        Me.txtIDRepartidor.TabIndex = 314
        Me.txtIDRepartidor.Text_1 = Nothing
        Me.txtIDRepartidor.Text_2 = Nothing
        Me.txtIDRepartidor.Text_3 = Nothing
        Me.txtIDRepartidor.Text_4 = Nothing
        Me.txtIDRepartidor.UserValues = Nothing
        Me.txtIDRepartidor.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Blue
        Me.Label17.Location = New System.Drawing.Point(12, 93)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 20)
        Me.Label17.TabIndex = 321
        Me.Label17.Text = "Producto"
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = ""
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Enabled = False
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(16, 117)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(612, 28)
        Me.cmbProducto.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(829, 93)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 20)
        Me.Label16.TabIndex = 320
        Me.Label16.Text = "Cantidad"
        '
        'txtCantidad
        '
        Me.txtCantidad.AccessibleName = ""
        Me.txtCantidad.Decimals = CType(2, Byte)
        Me.txtCantidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCantidad.Location = New System.Drawing.Point(833, 114)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.MaxLength = 100
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(79, 26)
        Me.txtCantidad.TabIndex = 8
        Me.txtCantidad.Text_1 = Nothing
        Me.txtCantidad.Text_2 = Nothing
        Me.txtCantidad.Text_3 = Nothing
        Me.txtCantidad.Text_4 = Nothing
        Me.txtCantidad.UserValues = Nothing
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(742, 94)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 20)
        Me.Label14.TabIndex = 322
        Me.Label14.Text = "Precio"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(1003, 93)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 20)
        Me.Label13.TabIndex = 323
        Me.Label13.Text = "Subtotal"
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = ""
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotal.Enabled = False
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotal.Location = New System.Drawing.Point(1007, 115)
        Me.txtSubtotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSubtotal.MaxLength = 100
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(79, 26)
        Me.txtSubtotal.TabIndex = 10
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(677, 92)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 20)
        Me.Label4.TabIndex = 324
        Me.Label4.Text = "Stock"
        '
        'lblStock
        '
        Me.lblStock.BackColor = System.Drawing.Color.Red
        Me.lblStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.ForeColor = System.Drawing.Color.White
        Me.lblStock.Location = New System.Drawing.Point(677, 115)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(61, 25)
        Me.lblStock.TabIndex = 325
        Me.lblStock.Text = "Stock"
        '
        'txtPeso
        '
        Me.txtPeso.AccessibleName = ""
        Me.txtPeso.Decimals = CType(2, Byte)
        Me.txtPeso.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPeso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeso.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPeso.Location = New System.Drawing.Point(920, 114)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPeso.MaxLength = 100
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(79, 26)
        Me.txtPeso.TabIndex = 9
        Me.txtPeso.Text_1 = Nothing
        Me.txtPeso.Text_2 = Nothing
        Me.txtPeso.Text_3 = Nothing
        Me.txtPeso.Text_4 = Nothing
        Me.txtPeso.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Blue
        Me.Label18.Location = New System.Drawing.Point(916, 93)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(47, 20)
        Me.Label18.TabIndex = 328
        Me.Label18.Text = "Peso"
        '
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        Me.grdItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.OrdenItem, Me.CodMaterial, Me.EquipoHerramienta, Me.IdProducto, Me.CodMarca, Me.IdUnidad, Me.Unidad, Me.Desc_Unit, Me.Peso, Me.PrecioUni, Me.IVA, Me.Descuento, Me.Subtotal, Me.Cantidad, Me.SubtotalFinal, Me.DescuentoUnidad, Me.FechaCumplido, Me.Nota, Me.Stock, Me.Eliminado, Me.CantidadPACK, Me.Reintegrar_Stock, Me.Bonificacion, Me.Promo, Me.Eliminar})
        Me.grdItems.Location = New System.Drawing.Point(16, 153)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1747, 302)
        Me.grdItems.TabIndex = 11
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        '
        'OrdenItem
        '
        Me.OrdenItem.HeaderText = "Item"
        Me.OrdenItem.Name = "OrdenItem"
        Me.OrdenItem.ReadOnly = True
        Me.OrdenItem.Width = 40
        '
        'CodMaterial
        '
        Me.CodMaterial.HeaderText = "CodMaterial"
        Me.CodMaterial.Name = "CodMaterial"
        Me.CodMaterial.ReadOnly = True
        Me.CodMaterial.Visible = False
        '
        'EquipoHerramienta
        '
        Me.EquipoHerramienta.HeaderText = "Producto"
        Me.EquipoHerramienta.Name = "EquipoHerramienta"
        Me.EquipoHerramienta.ReadOnly = True
        Me.EquipoHerramienta.Width = 260
        '
        'IdProducto
        '
        Me.IdProducto.HeaderText = "CodMarca"
        Me.IdProducto.Name = "IdProducto"
        Me.IdProducto.ReadOnly = True
        Me.IdProducto.Visible = False
        '
        'CodMarca
        '
        Me.CodMarca.HeaderText = "Marca"
        Me.CodMarca.Name = "CodMarca"
        Me.CodMarca.ReadOnly = True
        Me.CodMarca.Visible = False
        Me.CodMarca.Width = 230
        '
        'IdUnidad
        '
        Me.IdUnidad.HeaderText = "CodUnidad"
        Me.IdUnidad.Name = "IdUnidad"
        Me.IdUnidad.ReadOnly = True
        Me.IdUnidad.Visible = False
        '
        'Unidad
        '
        Me.Unidad.HeaderText = "Unidad"
        Me.Unidad.Name = "Unidad"
        Me.Unidad.ReadOnly = True
        Me.Unidad.Width = 70
        '
        'Desc_Unit
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.Desc_Unit.DefaultCellStyle = DataGridViewCellStyle9
        Me.Desc_Unit.HeaderText = "Cantidad"
        Me.Desc_Unit.Name = "Desc_Unit"
        Me.Desc_Unit.ReadOnly = True
        Me.Desc_Unit.Width = 70
        '
        'Peso
        '
        Me.Peso.HeaderText = "Peso"
        Me.Peso.Name = "Peso"
        Me.Peso.ReadOnly = True
        Me.Peso.Width = 60
        '
        'PrecioUni
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrecioUni.DefaultCellStyle = DataGridViewCellStyle10
        Me.PrecioUni.HeaderText = "Precio"
        Me.PrecioUni.Name = "PrecioUni"
        Me.PrecioUni.ReadOnly = True
        '
        'IVA
        '
        Me.IVA.HeaderText = "IVA"
        Me.IVA.Name = "IVA"
        Me.IVA.ReadOnly = True
        Me.IVA.Visible = False
        Me.IVA.Width = 50
        '
        'Descuento
        '
        Me.Descuento.HeaderText = "Descuento(%)"
        Me.Descuento.Name = "Descuento"
        '
        'Subtotal
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Subtotal.DefaultCellStyle = DataGridViewCellStyle11
        Me.Subtotal.HeaderText = "Subtotal"
        Me.Subtotal.Name = "Subtotal"
        Me.Subtotal.ReadOnly = True
        Me.Subtotal.Width = 120
        '
        'Cantidad
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle12
        Me.Cantidad.HeaderText = "QtyPedido"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Visible = False
        Me.Cantidad.Width = 70
        '
        'SubtotalFinal
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.SubtotalFinal.DefaultCellStyle = DataGridViewCellStyle13
        Me.SubtotalFinal.HeaderText = "CantSaldo"
        Me.SubtotalFinal.Name = "SubtotalFinal"
        Me.SubtotalFinal.ReadOnly = True
        Me.SubtotalFinal.Visible = False
        '
        'DescuentoUnidad
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DescuentoUnidad.DefaultCellStyle = DataGridViewCellStyle14
        Me.DescuentoUnidad.HeaderText = "Estado"
        Me.DescuentoUnidad.Name = "DescuentoUnidad"
        Me.DescuentoUnidad.ReadOnly = True
        Me.DescuentoUnidad.Visible = False
        '
        'FechaCumplido
        '
        Me.FechaCumplido.HeaderText = "FechaCumplido"
        Me.FechaCumplido.Name = "FechaCumplido"
        Me.FechaCumplido.ReadOnly = True
        Me.FechaCumplido.Visible = False
        Me.FechaCumplido.Width = 80
        '
        'Nota
        '
        Me.Nota.HeaderText = "Nota"
        Me.Nota.Name = "Nota"
        Me.Nota.Width = 150
        '
        'Stock
        '
        Me.Stock.HeaderText = "Stock"
        Me.Stock.Name = "Stock"
        Me.Stock.ReadOnly = True
        Me.Stock.Visible = False
        Me.Stock.Width = 50
        '
        'Eliminado
        '
        Me.Eliminado.HeaderText = "Eliminado"
        Me.Eliminado.Name = "Eliminado"
        Me.Eliminado.ReadOnly = True
        Me.Eliminado.Visible = False
        Me.Eliminado.Width = 50
        '
        'CantidadPACK
        '
        Me.CantidadPACK.HeaderText = "CantidadPACK"
        Me.CantidadPACK.Name = "CantidadPACK"
        Me.CantidadPACK.ReadOnly = True
        Me.CantidadPACK.Visible = False
        Me.CantidadPACK.Width = 50
        '
        'Reintegrar_Stock
        '
        Me.Reintegrar_Stock.HeaderText = "Rein. a Stock"
        Me.Reintegrar_Stock.Name = "Reintegrar_Stock"
        Me.Reintegrar_Stock.Width = 50
        '
        'Bonificacion
        '
        Me.Bonificacion.HeaderText = "Bonif."
        Me.Bonificacion.Name = "Bonificacion"
        Me.Bonificacion.Visible = False
        '
        'Promo
        '
        Me.Promo.HeaderText = "Promo"
        Me.Promo.Name = "Promo"
        Me.Promo.Visible = False
        '
        'Eliminar
        '
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.ReadOnly = True
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.ToolTipText = "Eliminar Registro"
        Me.Eliminar.UseColumnTextForButtonValue = True
        Me.Eliminar.Width = 80
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Enabled = False
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdUnidad.Location = New System.Drawing.Point(579, 68)
        Me.txtIdUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUnidad.MaxLength = 8
        Me.txtIdUnidad.Name = "txtIdUnidad"
        Me.txtIdUnidad.Size = New System.Drawing.Size(44, 21)
        Me.txtIdUnidad.TabIndex = 330
        Me.txtIdUnidad.Text_1 = Nothing
        Me.txtIdUnidad.Text_2 = Nothing
        Me.txtIdUnidad.Text_3 = Nothing
        Me.txtIdUnidad.Text_4 = Nothing
        Me.txtIdUnidad.UserValues = Nothing
        Me.txtIdUnidad.Visible = False
        '
        'txtIDPrecioLista
        '
        Me.txtIDPrecioLista.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDPrecioLista.Decimals = CType(2, Byte)
        Me.txtIDPrecioLista.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDPrecioLista.Enabled = False
        Me.txtIDPrecioLista.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIDPrecioLista.Location = New System.Drawing.Point(702, 13)
        Me.txtIDPrecioLista.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDPrecioLista.MaxLength = 8
        Me.txtIDPrecioLista.Name = "txtIDPrecioLista"
        Me.txtIDPrecioLista.Size = New System.Drawing.Size(29, 21)
        Me.txtIDPrecioLista.TabIndex = 331
        Me.txtIDPrecioLista.Text_1 = Nothing
        Me.txtIDPrecioLista.Text_2 = Nothing
        Me.txtIDPrecioLista.Text_3 = Nothing
        Me.txtIDPrecioLista.Text_4 = Nothing
        Me.txtIDPrecioLista.UserValues = Nothing
        Me.txtIDPrecioLista.Visible = False
        '
        'txtIDMarca
        '
        Me.txtIDMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDMarca.Decimals = CType(2, Byte)
        Me.txtIDMarca.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDMarca.Enabled = False
        Me.txtIDMarca.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDMarca.Location = New System.Drawing.Point(524, 68)
        Me.txtIDMarca.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDMarca.MaxLength = 8
        Me.txtIDMarca.Name = "txtIDMarca"
        Me.txtIDMarca.Size = New System.Drawing.Size(47, 21)
        Me.txtIDMarca.TabIndex = 332
        Me.txtIDMarca.Text_1 = Nothing
        Me.txtIDMarca.Text_2 = Nothing
        Me.txtIDMarca.Text_3 = Nothing
        Me.txtIDMarca.Text_4 = Nothing
        Me.txtIDMarca.UserValues = Nothing
        Me.txtIDMarca.Visible = False
        '
        'txtUnidad
        '
        Me.txtUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUnidad.Decimals = CType(2, Byte)
        Me.txtUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtUnidad.Enabled = False
        Me.txtUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtUnidad.Location = New System.Drawing.Point(469, 68)
        Me.txtUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUnidad.MaxLength = 8
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.Size = New System.Drawing.Size(47, 21)
        Me.txtUnidad.TabIndex = 333
        Me.txtUnidad.Text_1 = Nothing
        Me.txtUnidad.Text_2 = Nothing
        Me.txtUnidad.Text_3 = Nothing
        Me.txtUnidad.Text_4 = Nothing
        Me.txtUnidad.UserValues = Nothing
        Me.txtUnidad.Visible = False
        '
        'txtMarca
        '
        Me.txtMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMarca.Decimals = CType(2, Byte)
        Me.txtMarca.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMarca.Enabled = False
        Me.txtMarca.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtMarca.Location = New System.Drawing.Point(414, 67)
        Me.txtMarca.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMarca.MaxLength = 8
        Me.txtMarca.Name = "txtMarca"
        Me.txtMarca.Size = New System.Drawing.Size(47, 21)
        Me.txtMarca.TabIndex = 334
        Me.txtMarca.Text_1 = Nothing
        Me.txtMarca.Text_2 = Nothing
        Me.txtMarca.Text_3 = Nothing
        Me.txtMarca.Text_4 = Nothing
        Me.txtMarca.UserValues = Nothing
        Me.txtMarca.Visible = False
        '
        'txtPrecioVta
        '
        Me.txtPrecioVta.AccessibleName = ""
        Me.txtPrecioVta.Decimals = CType(2, Byte)
        Me.txtPrecioVta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPrecioVta.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecioVta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPrecioVta.Location = New System.Drawing.Point(746, 114)
        Me.txtPrecioVta.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrecioVta.MaxLength = 100
        Me.txtPrecioVta.Name = "txtPrecioVta"
        Me.txtPrecioVta.Size = New System.Drawing.Size(79, 27)
        Me.txtPrecioVta.TabIndex = 7
        Me.txtPrecioVta.Text_1 = Nothing
        Me.txtPrecioVta.Text_2 = Nothing
        Me.txtPrecioVta.Text_3 = Nothing
        Me.txtPrecioVta.Text_4 = Nothing
        Me.txtPrecioVta.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 459)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 15)
        Me.Label5.TabIndex = 335
        Me.Label5.Text = "Cantidad de Ítems:"
        '
        'PicClientes
        '
        Me.PicClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicClientes.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicClientes.Location = New System.Drawing.Point(1001, 36)
        Me.PicClientes.Margin = New System.Windows.Forms.Padding(4)
        Me.PicClientes.Name = "PicClientes"
        Me.PicClientes.Size = New System.Drawing.Size(24, 25)
        Me.PicClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicClientes.TabIndex = 337
        Me.PicClientes.TabStop = False
        '
        'PicRepartidor
        '
        Me.PicRepartidor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicRepartidor.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicRepartidor.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicRepartidor.Location = New System.Drawing.Point(1282, 36)
        Me.PicRepartidor.Margin = New System.Windows.Forms.Padding(4)
        Me.PicRepartidor.Name = "PicRepartidor"
        Me.PicRepartidor.Size = New System.Drawing.Size(24, 25)
        Me.PicRepartidor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicRepartidor.TabIndex = 338
        Me.PicRepartidor.TabStop = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtID.Location = New System.Drawing.Point(1720, 12)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(43, 21)
        Me.txtID.TabIndex = 339
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'btnActualizarMat
        '
        Me.btnActualizarMat.BackgroundImage = Global.PORKYS.My.Resources.Resources.Sincro
        Me.btnActualizarMat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnActualizarMat.Location = New System.Drawing.Point(636, 119)
        Me.btnActualizarMat.Name = "btnActualizarMat"
        Me.btnActualizarMat.Size = New System.Drawing.Size(34, 26)
        Me.btnActualizarMat.TabIndex = 340
        Me.btnActualizarMat.UseVisualStyleBackColor = True
        '
        'chkVentas
        '
        Me.chkVentas.AutoSize = True
        Me.chkVentas.BackColor = System.Drawing.Color.Transparent
        Me.chkVentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVentas.ForeColor = System.Drawing.Color.Black
        Me.chkVentas.Location = New System.Drawing.Point(13, 41)
        Me.chkVentas.Name = "chkVentas"
        Me.chkVentas.Size = New System.Drawing.Size(85, 24)
        Me.chkVentas.TabIndex = 341
        Me.chkVentas.Text = " Venta"
        Me.chkVentas.UseVisualStyleBackColor = False
        '
        'chkDevolucion
        '
        Me.chkDevolucion.AutoSize = True
        Me.chkDevolucion.BackColor = System.Drawing.Color.Transparent
        Me.chkDevolucion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDevolucion.ForeColor = System.Drawing.Color.Black
        Me.chkDevolucion.Location = New System.Drawing.Point(13, 65)
        Me.chkDevolucion.Name = "chkDevolucion"
        Me.chkDevolucion.Size = New System.Drawing.Size(130, 24)
        Me.chkDevolucion.TabIndex = 342
        Me.chkDevolucion.Text = " Devolución"
        Me.chkDevolucion.UseVisualStyleBackColor = False
        '
        'chkTransferencia
        '
        Me.chkTransferencia.AccessibleName = "Eliminado"
        Me.chkTransferencia.AutoSize = True
        Me.chkTransferencia.Enabled = False
        Me.chkTransferencia.Location = New System.Drawing.Point(875, 12)
        Me.chkTransferencia.Margin = New System.Windows.Forms.Padding(4)
        Me.chkTransferencia.Name = "chkTransferencia"
        Me.chkTransferencia.Size = New System.Drawing.Size(104, 19)
        Me.chkTransferencia.TabIndex = 343
        Me.chkTransferencia.Text = "Transferencia"
        Me.chkTransferencia.UseVisualStyleBackColor = True
        '
        'chkReintegrarStock
        '
        Me.chkReintegrarStock.AutoSize = True
        Me.chkReintegrarStock.Enabled = False
        Me.chkReintegrarStock.Location = New System.Drawing.Point(1093, 114)
        Me.chkReintegrarStock.Name = "chkReintegrarStock"
        Me.chkReintegrarStock.Size = New System.Drawing.Size(101, 19)
        Me.chkReintegrarStock.TabIndex = 344
        Me.chkReintegrarStock.Text = "Rein. a Stock"
        Me.chkReintegrarStock.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Blue
        Me.Label20.Location = New System.Drawing.Point(1115, 93)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(0, 20)
        Me.Label20.TabIndex = 345
        '
        'lblPromo
        '
        Me.lblPromo.BackColor = System.Drawing.Color.Transparent
        Me.lblPromo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPromo.ForeColor = System.Drawing.Color.Green
        Me.lblPromo.Location = New System.Drawing.Point(742, 93)
        Me.lblPromo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPromo.Name = "lblPromo"
        Me.lblPromo.Size = New System.Drawing.Size(66, 21)
        Me.lblPromo.TabIndex = 347
        Me.lblPromo.Text = "Precio"
        Me.lblPromo.Visible = False
        '
        'txtValorPromo
        '
        Me.txtValorPromo.AccessibleName = ""
        Me.txtValorPromo.BackColor = System.Drawing.Color.DarkGreen
        Me.txtValorPromo.Decimals = CType(2, Byte)
        Me.txtValorPromo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtValorPromo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorPromo.ForeColor = System.Drawing.SystemColors.Window
        Me.txtValorPromo.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtValorPromo.Location = New System.Drawing.Point(746, 115)
        Me.txtValorPromo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValorPromo.MaxLength = 100
        Me.txtValorPromo.Name = "txtValorPromo"
        Me.txtValorPromo.Size = New System.Drawing.Size(79, 27)
        Me.txtValorPromo.TabIndex = 349
        Me.txtValorPromo.Text_1 = Nothing
        Me.txtValorPromo.Text_2 = Nothing
        Me.txtValorPromo.Text_3 = Nothing
        Me.txtValorPromo.Text_4 = Nothing
        Me.txtValorPromo.UserValues = Nothing
        Me.txtValorPromo.Visible = False
        '
        'btnXNorte
        '
        Me.btnXNorte.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnXNorte.BackColor = System.Drawing.Color.Transparent
        Me.btnXNorte.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnXNorte.FocusCuesEnabled = False
        Me.btnXNorte.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnXNorte.Location = New System.Drawing.Point(13, 11)
        Me.btnXNorte.Name = "btnXNorte"
        Me.btnXNorte.Size = New System.Drawing.Size(124, 28)
        Me.btnXNorte.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010
        Me.btnXNorte.Symbol = ""
        Me.btnXNorte.SymbolColor = System.Drawing.Color.Red
        Me.btnXNorte.SymbolSize = 12.0!
        Me.btnXNorte.TabIndex = 350
        Me.btnXNorte.Text = " Norte"
        Me.btnXNorte.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left
        '
        'lblDescripcionPromo
        '
        Me.lblDescripcionPromo.BackColor = System.Drawing.Color.Transparent
        Me.lblDescripcionPromo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcionPromo.ForeColor = System.Drawing.Color.Green
        Me.lblDescripcionPromo.Location = New System.Drawing.Point(103, 93)
        Me.lblDescripcionPromo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescripcionPromo.Name = "lblDescripcionPromo"
        Me.lblDescripcionPromo.Size = New System.Drawing.Size(525, 21)
        Me.lblDescripcionPromo.TabIndex = 348
        Me.lblDescripcionPromo.Text = "Descripcion Promo"
        Me.lblDescripcionPromo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDescripcionPromo.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Red
        Me.Label21.Location = New System.Drawing.Point(232, 467)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(111, 20)
        Me.Label21.TabIndex = 357
        Me.Label21.Text = "Autorizado: "
        Me.Label21.Visible = False
        '
        'lblAutorizado
        '
        Me.lblAutorizado.AutoSize = True
        Me.lblAutorizado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutorizado.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblAutorizado.Location = New System.Drawing.Point(232, 500)
        Me.lblAutorizado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAutorizado.Name = "lblAutorizado"
        Me.lblAutorizado.Size = New System.Drawing.Size(91, 20)
        Me.lblAutorizado.TabIndex = 358
        Me.lblAutorizado.Text = "Empleado"
        Me.lblAutorizado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAutorizado.Visible = False
        '
        'chkFacturaCancelada
        '
        Me.chkFacturaCancelada.AutoSize = True
        Me.chkFacturaCancelada.Location = New System.Drawing.Point(755, 330)
        Me.chkFacturaCancelada.Name = "chkFacturaCancelada"
        Me.chkFacturaCancelada.Size = New System.Drawing.Size(132, 19)
        Me.chkFacturaCancelada.TabIndex = 359
        Me.chkFacturaCancelada.Text = "Factura Cancelada"
        Me.chkFacturaCancelada.UseVisualStyleBackColor = True
        Me.chkFacturaCancelada.Visible = False
        '
        'chkPresupuesto
        '
        '
        '
        '
        Me.chkPresupuesto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkPresupuesto.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPresupuesto.Location = New System.Drawing.Point(177, 66)
        Me.chkPresupuesto.Name = "chkPresupuesto"
        Me.chkPresupuesto.Size = New System.Drawing.Size(138, 23)
        Me.chkPresupuesto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkPresupuesto.TabIndex = 360
        Me.chkPresupuesto.Text = "Presupuesto"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Blue
        Me.Label22.Location = New System.Drawing.Point(631, 65)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(154, 21)
        Me.Label22.TabIndex = 361
        Me.Label22.Text = "Saldo de Cliente:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSaldo
        '
        Me.lblSaldo.AutoSize = True
        Me.lblSaldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaldo.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblSaldo.Location = New System.Drawing.Point(793, 65)
        Me.lblSaldo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSaldo.Name = "lblSaldo"
        Me.lblSaldo.Size = New System.Drawing.Size(44, 20)
        Me.lblSaldo.TabIndex = 362
        Me.lblSaldo.Text = "0.00"
        '
        'txtIDIngreso
        '
        Me.txtIDIngreso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIDIngreso.Decimals = CType(2, Byte)
        Me.txtIDIngreso.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDIngreso.Enabled = False
        Me.txtIDIngreso.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDIngreso.Location = New System.Drawing.Point(1720, 470)
        Me.txtIDIngreso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDIngreso.MaxLength = 8
        Me.txtIDIngreso.Name = "txtIDIngreso"
        Me.txtIDIngreso.Size = New System.Drawing.Size(43, 21)
        Me.txtIDIngreso.TabIndex = 363
        Me.txtIDIngreso.Text_1 = Nothing
        Me.txtIDIngreso.Text_2 = Nothing
        Me.txtIDIngreso.Text_3 = Nothing
        Me.txtIDIngreso.Text_4 = Nothing
        Me.txtIDIngreso.UserValues = Nothing
        Me.txtIDIngreso.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.PanelTotales)
        Me.GroupBox1.Controls.Add(Me.btnXNorte)
        Me.GroupBox1.Controls.Add(Me.chkMayorista)
        Me.GroupBox1.Controls.Add(Me.chkFacturaCancelada)
        Me.GroupBox1.Controls.Add(Me.lblIVA)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtCodigoIngreso)
        Me.GroupBox1.Controls.Add(Me.txtIDIngreso)
        Me.GroupBox1.Controls.Add(Me.lblSaldo)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.chkPresupuesto)
        Me.GroupBox1.Controls.Add(Me.lblAutorizado)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.lblDescripcionPromo)
        Me.GroupBox1.Controls.Add(Me.txtValorPromo)
        Me.GroupBox1.Controls.Add(Me.lblPromo)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.chkReintegrarStock)
        Me.GroupBox1.Controls.Add(Me.chkTransferencia)
        Me.GroupBox1.Controls.Add(Me.chkDevolucion)
        Me.GroupBox1.Controls.Add(Me.chkVentas)
        Me.GroupBox1.Controls.Add(Me.btnActualizarMat)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.PicRepartidor)
        Me.GroupBox1.Controls.Add(Me.PicClientes)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPrecioVta)
        Me.GroupBox1.Controls.Add(Me.txtMarca)
        Me.GroupBox1.Controls.Add(Me.txtUnidad)
        Me.GroupBox1.Controls.Add(Me.txtIDMarca)
        Me.GroupBox1.Controls.Add(Me.txtIDPrecioLista)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtPeso)
        Me.GroupBox1.Controls.Add(Me.lblStock)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtIDRepartidor)
        Me.GroupBox1.Controls.Add(Me.cmbRepartidor)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.rdTodasPed)
        Me.GroupBox1.Controls.Add(Me.rdPendientes)
        Me.GroupBox1.Controls.Add(Me.rdAnuladas)
        Me.GroupBox1.Controls.Add(Me.lblLugarEntrega)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblFechaEntrega)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtidAlmacen)
        Me.GroupBox1.Controls.Add(Me.lblNroPedido)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbAlmacenes)
        Me.GroupBox1.Controls.Add(Me.chkGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbClientes)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkAnuladas)
        Me.GroupBox1.Controls.Add(Me.Line1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(9, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1814, 544)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'PanelTotales
        '
        Me.PanelTotales.Controls.Add(Me.GroupBoxCaja)
        Me.PanelTotales.Controls.Add(Me.Label7)
        Me.PanelTotales.Controls.Add(Me.PanelDescuento)
        Me.PanelTotales.Controls.Add(Me.Label12)
        Me.PanelTotales.Controls.Add(Me.lblTotal)
        Me.PanelTotales.Controls.Add(Me.lblSubtotal)
        Me.PanelTotales.Location = New System.Drawing.Point(661, 459)
        Me.PanelTotales.Name = "PanelTotales"
        Me.PanelTotales.Size = New System.Drawing.Size(897, 78)
        Me.PanelTotales.TabIndex = 367
        '
        'GroupBoxCaja
        '
        Me.GroupBoxCaja.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.GroupBoxCaja.Controls.Add(Me.btnRetiros)
        Me.GroupBoxCaja.Controls.Add(Me.btnApCaja)
        Me.GroupBoxCaja.Controls.Add(Me.btnIngresos)
        Me.GroupBoxCaja.Controls.Add(Me.btnGastos)
        Me.GroupBoxCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxCaja.ForeColor = System.Drawing.Color.Black
        Me.GroupBoxCaja.Location = New System.Drawing.Point(20, 11)
        Me.GroupBoxCaja.Name = "GroupBoxCaja"
        Me.GroupBoxCaja.Size = New System.Drawing.Size(258, 58)
        Me.GroupBoxCaja.TabIndex = 366
        Me.GroupBoxCaja.TabStop = False
        Me.GroupBoxCaja.Text = "Op. Caja"
        '
        'btnRetiros
        '
        Me.btnRetiros.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRetiros.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRetiros.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRetiros.Location = New System.Drawing.Point(203, 15)
        Me.btnRetiros.Name = "btnRetiros"
        Me.btnRetiros.Size = New System.Drawing.Size(37, 32)
        Me.btnRetiros.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnRetiros.Symbol = ""
        Me.btnRetiros.SymbolColor = System.Drawing.Color.Red
        Me.btnRetiros.SymbolSize = 18.0!
        Me.btnRetiros.TabIndex = 368
        Me.btnRetiros.TextColor = System.Drawing.Color.Red
        '
        'btnApCaja
        '
        Me.btnApCaja.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnApCaja.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnApCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApCaja.Location = New System.Drawing.Point(71, 15)
        Me.btnApCaja.Name = "btnApCaja"
        Me.btnApCaja.Size = New System.Drawing.Size(37, 32)
        Me.btnApCaja.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnApCaja.SymbolColor = System.Drawing.Color.Red
        Me.btnApCaja.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnApCaja.SymbolSize = 25.0!
        Me.btnApCaja.TabIndex = 366
        Me.btnApCaja.Text = "Ap."
        Me.btnApCaja.TextColor = System.Drawing.Color.DarkGreen
        '
        'btnIngresos
        '
        Me.btnIngresos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnIngresos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnIngresos.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIngresos.Location = New System.Drawing.Point(115, 15)
        Me.btnIngresos.Name = "btnIngresos"
        Me.btnIngresos.Size = New System.Drawing.Size(37, 32)
        Me.btnIngresos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnIngresos.Symbol = ""
        Me.btnIngresos.SymbolColor = System.Drawing.Color.DarkGreen
        Me.btnIngresos.SymbolSize = 18.0!
        Me.btnIngresos.TabIndex = 365
        Me.btnIngresos.TextColor = System.Drawing.Color.DarkGreen
        '
        'btnGastos
        '
        Me.btnGastos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGastos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGastos.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGastos.Location = New System.Drawing.Point(159, 15)
        Me.btnGastos.Name = "btnGastos"
        Me.btnGastos.Size = New System.Drawing.Size(37, 32)
        Me.btnGastos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGastos.Symbol = ""
        Me.btnGastos.SymbolColor = System.Drawing.Color.Red
        Me.btnGastos.SymbolSize = 18.0!
        Me.btnGastos.TabIndex = 367
        Me.btnGastos.TextColor = System.Drawing.Color.Red
        '
        'chkMayorista
        '
        '
        '
        '
        Me.chkMayorista.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkMayorista.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMayorista.Location = New System.Drawing.Point(13, 16)
        Me.chkMayorista.Name = "chkMayorista"
        Me.chkMayorista.Size = New System.Drawing.Size(138, 23)
        Me.chkMayorista.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkMayorista.TabIndex = 361
        Me.chkMayorista.Text = "Mayorista"
        '
        'txtCodigoIngreso
        '
        Me.txtCodigoIngreso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigoIngreso.Decimals = CType(2, Byte)
        Me.txtCodigoIngreso.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCodigoIngreso.Enabled = False
        Me.txtCodigoIngreso.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCodigoIngreso.Location = New System.Drawing.Point(1685, 502)
        Me.txtCodigoIngreso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoIngreso.MaxLength = 8
        Me.txtCodigoIngreso.Name = "txtCodigoIngreso"
        Me.txtCodigoIngreso.Size = New System.Drawing.Size(78, 21)
        Me.txtCodigoIngreso.TabIndex = 364
        Me.txtCodigoIngreso.Text_1 = Nothing
        Me.txtCodigoIngreso.Text_2 = Nothing
        Me.txtCodigoIngreso.Text_3 = Nothing
        Me.txtCodigoIngreso.Text_4 = Nothing
        Me.txtCodigoIngreso.UserValues = Nothing
        Me.txtCodigoIngreso.Visible = False
        '
        'frmVentasWEB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1816, 752)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmVentasWEB"
        Me.Text = "frmVentasWEB"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStripIVA.ResumeLayout(False)
        Me.PanelDescuento.ResumeLayout(False)
        Me.PanelDescuento.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicRepartidor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PanelTotales.ResumeLayout(False)
        Me.PanelTotales.PerformLayout()
        Me.GroupBoxCaja.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ContextMenuStripIVA As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItemIVA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerDescargas As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Line1 As DevComponents.DotNetBar.Controls.Line
    Friend WithEvents chkAnuladas As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Public WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Public WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblNroPedido As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblFechaEntrega As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblLugarEntrega As System.Windows.Forms.Label
    Friend WithEvents rdAnuladas As System.Windows.Forms.RadioButton
    Friend WithEvents rdPendientes As System.Windows.Forms.RadioButton
    Friend WithEvents rdTodasPed As System.Windows.Forms.RadioButton
    Friend WithEvents PanelDescuento As System.Windows.Forms.Panel
    Friend WithEvents chkDescuento As System.Windows.Forms.CheckBox
    Public WithEvents txtDescuento As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents rdPorcentaje As System.Windows.Forms.RadioButton
    Public WithEvents rdAbsoluto As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents cmbRepartidor As System.Windows.Forms.ComboBox
    Friend WithEvents txtIDRepartidor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents txtPeso As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIDPrecioLista As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIDMarca As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMarca As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPrecioVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PicClientes As System.Windows.Forms.PictureBox
    Friend WithEvents PicRepartidor As System.Windows.Forms.PictureBox
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnActualizarMat As System.Windows.Forms.Button
    Public WithEvents chkVentas As System.Windows.Forms.CheckBox
    Public WithEvents chkDevolucion As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransferencia As System.Windows.Forms.CheckBox
    Friend WithEvents chkReintegrarStock As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblPromo As System.Windows.Forms.Label
    Friend WithEvents txtValorPromo As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents btnXNorte As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblDescripcionPromo As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblAutorizado As System.Windows.Forms.Label
    Friend WithEvents chkFacturaCancelada As System.Windows.Forms.CheckBox
    Friend WithEvents chkPresupuesto As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblSaldo As System.Windows.Forms.Label
    Friend WithEvents txtIDIngreso As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCodigoIngreso As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnIngresos As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnApCaja As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupBoxCaja As System.Windows.Forms.GroupBox
    Friend WithEvents btnGastos As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRetiros As DevComponents.DotNetBar.ButtonX
    Public WithEvents cmbAlmacenes As System.Windows.Forms.ComboBox
    Public WithEvents txtidAlmacen As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkMayorista As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrdenItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodMaterial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EquipoHerramienta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodMarca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Desc_Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Peso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecioUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descuento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subtotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubtotalFinal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescuentoUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaCumplido As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nota As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eliminado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CantidadPACK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reintegrar_Stock As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Bonificacion As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Promo As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents PanelTotales As System.Windows.Forms.Panel
End Class
