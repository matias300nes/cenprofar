<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDevolucionesProveedor
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDevolucionesProveedor))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.chkMaterialesProveedor = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EquipoHerramienta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Lote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoteProveed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remito = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ConCambio = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Nota_Det = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCantidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.picEmpleados = New System.Windows.Forms.PictureBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtMontoIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.chkGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.rdTodas = New System.Windows.Forms.RadioButton()
        Me.rdPendientes = New System.Windows.Forms.RadioButton()
        Me.rdAnuladas = New System.Windows.Forms.RadioButton()
        Me.cmbEmpleado = New System.Windows.Forms.ComboBox()
        Me.txtIdProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnEntregar = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cmbPROVEEDORES = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PicProveedores = New System.Windows.Forms.PictureBox()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.BuscarDescripcionToolStripMenuItem2 = New System.Windows.Forms.ToolStripComboBox()
        Me.BuscarDescripcionToolStripMenuItem3 = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbUnidadVenta = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbMonedasCompra = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuMarcas = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ActivarNuevaMarcaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbMarcaCompra = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEmpleados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicProveedores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.ContextMenuMarcas.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbAlmacenes)
        Me.GroupBox1.Controls.Add(Me.chkMaterialesProveedor)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.lblStock)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.picEmpleados)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtMontoIVA)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.chkGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.rdTodas)
        Me.GroupBox1.Controls.Add(Me.rdPendientes)
        Me.GroupBox1.Controls.Add(Me.rdAnuladas)
        Me.GroupBox1.Controls.Add(Me.cmbEmpleado)
        Me.GroupBox1.Controls.Add(Me.txtIdProveedor)
        Me.GroupBox1.Controls.Add(Me.btnEntregar)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.cmbPROVEEDORES)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.PicProveedores)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(9, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1611, 548)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(292, 23)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(67, 17)
        Me.Label15.TabIndex = 333
        Me.Label15.Text = "Almacen*"
        '
        'cmbAlmacenes
        '
        Me.cmbAlmacenes.AccessibleName = "*ALMACEN"
        Me.cmbAlmacenes.DropDownHeight = 500
        Me.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAlmacenes.FormattingEnabled = True
        Me.cmbAlmacenes.IntegralHeight = False
        Me.cmbAlmacenes.Location = New System.Drawing.Point(295, 44)
        Me.cmbAlmacenes.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAlmacenes.Name = "cmbAlmacenes"
        Me.cmbAlmacenes.Size = New System.Drawing.Size(218, 25)
        Me.cmbAlmacenes.TabIndex = 2
        '
        'chkMaterialesProveedor
        '
        Me.chkMaterialesProveedor.AutoSize = True
        Me.chkMaterialesProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMaterialesProveedor.Location = New System.Drawing.Point(96, 127)
        Me.chkMaterialesProveedor.Name = "chkMaterialesProveedor"
        Me.chkMaterialesProveedor.Size = New System.Drawing.Size(127, 22)
        Me.chkMaterialesProveedor.TabIndex = 331
        Me.chkMaterialesProveedor.Text = "Por Proveedor"
        Me.chkMaterialesProveedor.UseVisualStyleBackColor = True
        '
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.grdItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.Codigo, Me.Item, Me.IdProducto, Me.EquipoHerramienta, Me.Lote, Me.Cantidad, Me.IdUnidad, Me.Unidad, Me.LoteProveed, Me.Remito, Me.Estado, Me.ConCambio, Me.Nota_Det, Me.Eliminar})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdItems.Location = New System.Drawing.Point(13, 188)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1583, 299)
        Me.grdItems.TabIndex = 11
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        '
        'Codigo
        '
        Me.Codigo.HeaderText = "Codigo"
        Me.Codigo.Name = "Codigo"
        '
        'Item
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Item.DefaultCellStyle = DataGridViewCellStyle3
        Me.Item.HeaderText = "Item"
        Me.Item.Name = "Item"
        Me.Item.Width = 60
        '
        'IdProducto
        '
        Me.IdProducto.HeaderText = "IdProducto"
        Me.IdProducto.Name = "IdProducto"
        Me.IdProducto.Visible = False
        '
        'EquipoHerramienta
        '
        Me.EquipoHerramienta.HeaderText = "Producto"
        Me.EquipoHerramienta.Name = "EquipoHerramienta"
        Me.EquipoHerramienta.ReadOnly = True
        Me.EquipoHerramienta.Width = 350
        '
        'Lote
        '
        Me.Lote.HeaderText = "Lote"
        Me.Lote.Name = "Lote"
        Me.Lote.Visible = False
        '
        'Cantidad
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle4
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 90
        '
        'IdUnidad
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.IdUnidad.DefaultCellStyle = DataGridViewCellStyle5
        Me.IdUnidad.HeaderText = "Unidad"
        Me.IdUnidad.Name = "IdUnidad"
        '
        'Unidad
        '
        Me.Unidad.HeaderText = "NomUnidad"
        Me.Unidad.Name = "Unidad"
        Me.Unidad.Visible = False
        '
        'LoteProveed
        '
        Me.LoteProveed.HeaderText = "LoteProveed"
        Me.LoteProveed.Name = "LoteProveed"
        Me.LoteProveed.Visible = False
        '
        'Remito
        '
        Me.Remito.HeaderText = "Remito"
        Me.Remito.Name = "Remito"
        Me.Remito.Visible = False
        '
        'Estado
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.Estado.DefaultCellStyle = DataGridViewCellStyle6
        Me.Estado.HeaderText = "Estado"
        Me.Estado.Name = "Estado"
        Me.Estado.Width = 120
        '
        'ConCambio
        '
        Me.ConCambio.HeaderText = "Con Cambio"
        Me.ConCambio.Name = "ConCambio"
        '
        'Nota_Det
        '
        Me.Nota_Det.HeaderText = "Nota"
        Me.Nota_Det.Name = "Nota_Det"
        Me.Nota_Det.Width = 200
        '
        'Eliminar
        '
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.ToolTipText = "Eliminar Registro"
        Me.Eliminar.UseColumnTextForButtonValue = True
        Me.Eliminar.Width = 80
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.AccessibleName = ""
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdUnidad.Location = New System.Drawing.Point(574, 125)
        Me.txtIdUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUnidad.MaxLength = 100
        Me.txtIdUnidad.Name = "txtIdUnidad"
        Me.txtIdUnidad.Size = New System.Drawing.Size(51, 22)
        Me.txtIdUnidad.TabIndex = 309
        Me.txtIdUnidad.Text_1 = Nothing
        Me.txtIdUnidad.Text_2 = Nothing
        Me.txtIdUnidad.Text_3 = Nothing
        Me.txtIdUnidad.Text_4 = Nothing
        Me.txtIdUnidad.UserValues = Nothing
        Me.txtIdUnidad.Visible = False
        '
        'lblStock
        '
        Me.lblStock.BackColor = System.Drawing.Color.Blue
        Me.lblStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.ForeColor = System.Drawing.Color.White
        Me.lblStock.Location = New System.Drawing.Point(635, 153)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(71, 25)
        Me.lblStock.TabIndex = 8
        Me.lblStock.Text = "Stock"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(635, 127)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 20)
        Me.Label11.TabIndex = 307
        Me.Label11.Text = "Stock"
        '
        'txtCantidad
        '
        Me.txtCantidad.AccessibleName = ""
        Me.txtCantidad.Decimals = CType(2, Byte)
        Me.txtCantidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCantidad.Location = New System.Drawing.Point(715, 152)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.MaxLength = 100
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(79, 26)
        Me.txtCantidad.TabIndex = 7
        Me.txtCantidad.Text_1 = Nothing
        Me.txtCantidad.Text_2 = Nothing
        Me.txtCantidad.Text_3 = Nothing
        Me.txtCantidad.Text_4 = Nothing
        Me.txtCantidad.UserValues = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(715, 129)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 20)
        Me.Label10.TabIndex = 303
        Me.Label10.Text = "Cantidad"
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = ""
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(13, 150)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(612, 28)
        Me.cmbProducto.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(13, 127)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 20)
        Me.Label13.TabIndex = 304
        Me.Label13.Text = "Producto"
        '
        'picEmpleados
        '
        Me.picEmpleados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picEmpleados.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picEmpleados.Image = CType(resources.GetObject("picEmpleados.Image"), System.Drawing.Image)
        Me.picEmpleados.Location = New System.Drawing.Point(1404, 45)
        Me.picEmpleados.Margin = New System.Windows.Forms.Padding(4)
        Me.picEmpleados.Name = "picEmpleados"
        Me.picEmpleados.Size = New System.Drawing.Size(24, 25)
        Me.picEmpleados.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEmpleados.TabIndex = 298
        Me.picEmpleados.TabStop = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(1312, 594)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(85, 18)
        Me.Label20.TabIndex = 214
        Me.Label20.Text = "Monto IVA"
        Me.Label20.Visible = False
        '
        'txtMontoIVA
        '
        Me.txtMontoIVA.AccessibleName = "Nota"
        Me.txtMontoIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoIVA.Decimals = CType(2, Byte)
        Me.txtMontoIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtMontoIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIVA.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoIVA.Location = New System.Drawing.Point(1423, 591)
        Me.txtMontoIVA.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMontoIVA.Name = "txtMontoIVA"
        Me.txtMontoIVA.ReadOnly = True
        Me.txtMontoIVA.Size = New System.Drawing.Size(95, 24)
        Me.txtMontoIVA.TabIndex = 213
        Me.txtMontoIVA.Text_1 = Nothing
        Me.txtMontoIVA.Text_2 = Nothing
        Me.txtMontoIVA.Text_3 = Nothing
        Me.txtMontoIVA.Text_4 = Nothing
        Me.txtMontoIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIVA.UserValues = Nothing
        Me.txtMontoIVA.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1520, 594)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 18)
        Me.Label18.TabIndex = 212
        Me.Label18.Text = "Total"
        Me.Label18.Visible = False
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = "Nota"
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTotal.Location = New System.Drawing.Point(1580, 591)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(129, 24)
        Me.txtTotal.TabIndex = 211
        Me.txtTotal.Text_1 = Nothing
        Me.txtTotal.Text_2 = Nothing
        Me.txtTotal.Text_3 = Nothing
        Me.txtTotal.Text_4 = Nothing
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotal.UserValues = Nothing
        Me.txtTotal.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(13, 74)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 20)
        Me.Label14.TabIndex = 197
        Me.Label14.Text = "Nota"
        '
        'chkGrillaInferior
        '
        Me.chkGrillaInferior.AutoSize = True
        Me.chkGrillaInferior.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGrillaInferior.Location = New System.Drawing.Point(237, 507)
        Me.chkGrillaInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.chkGrillaInferior.Name = "chkGrillaInferior"
        Me.chkGrillaInferior.Size = New System.Drawing.Size(200, 21)
        Me.chkGrillaInferior.TabIndex = 187
        Me.chkGrillaInferior.Text = "Aumentar Grilla Inferior"
        Me.chkGrillaInferior.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(14, 509)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(129, 17)
        Me.Label19.TabIndex = 186
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(142, 509)
        Me.lblCantidadFilas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(60, 17)
        Me.lblCantidadFilas.TabIndex = 185
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'rdTodas
        '
        Me.rdTodas.AutoSize = True
        Me.rdTodas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTodas.Location = New System.Drawing.Point(1496, 505)
        Me.rdTodas.Margin = New System.Windows.Forms.Padding(4)
        Me.rdTodas.Name = "rdTodas"
        Me.rdTodas.Size = New System.Drawing.Size(100, 21)
        Me.rdTodas.TabIndex = 14
        Me.rdTodas.TabStop = True
        Me.rdTodas.Text = "Todas DP"
        Me.rdTodas.UseVisualStyleBackColor = True
        '
        'rdPendientes
        '
        Me.rdPendientes.AutoSize = True
        Me.rdPendientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPendientes.Location = New System.Drawing.Point(1204, 505)
        Me.rdPendientes.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPendientes.Name = "rdPendientes"
        Me.rdPendientes.Size = New System.Drawing.Size(136, 21)
        Me.rdPendientes.TabIndex = 12
        Me.rdPendientes.TabStop = True
        Me.rdPendientes.Text = "DP Pendientes"
        Me.rdPendientes.UseVisualStyleBackColor = True
        '
        'rdAnuladas
        '
        Me.rdAnuladas.AutoSize = True
        Me.rdAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAnuladas.Location = New System.Drawing.Point(1357, 505)
        Me.rdAnuladas.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAnuladas.Name = "rdAnuladas"
        Me.rdAnuladas.Size = New System.Drawing.Size(122, 21)
        Me.rdAnuladas.TabIndex = 13
        Me.rdAnuladas.TabStop = True
        Me.rdAnuladas.Text = "DP Anuladas"
        Me.rdAnuladas.UseVisualStyleBackColor = True
        '
        'cmbEmpleado
        '
        Me.cmbEmpleado.AccessibleName = "*DEVUELTO POR"
        Me.cmbEmpleado.DropDownHeight = 300
        Me.cmbEmpleado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpleado.FormattingEnabled = True
        Me.cmbEmpleado.IntegralHeight = False
        Me.cmbEmpleado.Location = New System.Drawing.Point(992, 43)
        Me.cmbEmpleado.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbEmpleado.Name = "cmbEmpleado"
        Me.cmbEmpleado.Size = New System.Drawing.Size(404, 28)
        Me.cmbEmpleado.TabIndex = 4
        '
        'txtIdProveedor
        '
        Me.txtIdProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdProveedor.Decimals = CType(2, Byte)
        Me.txtIdProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProveedor.Enabled = False
        Me.txtIdProveedor.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdProveedor.Location = New System.Drawing.Point(923, 14)
        Me.txtIdProveedor.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdProveedor.MaxLength = 8
        Me.txtIdProveedor.Name = "txtIdProveedor"
        Me.txtIdProveedor.Size = New System.Drawing.Size(29, 22)
        Me.txtIdProveedor.TabIndex = 179
        Me.txtIdProveedor.Text_1 = Nothing
        Me.txtIdProveedor.Text_2 = Nothing
        Me.txtIdProveedor.Text_3 = Nothing
        Me.txtIdProveedor.Text_4 = Nothing
        Me.txtIdProveedor.UserValues = Nothing
        Me.txtIdProveedor.Visible = False
        '
        'btnEntregar
        '
        Me.btnEntregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEntregar.Location = New System.Drawing.Point(1184, 115)
        Me.btnEntregar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEntregar.Name = "btnEntregar"
        Me.btnEntregar.Size = New System.Drawing.Size(136, 28)
        Me.btnEntregar.TabIndex = 9
        Me.btnEntregar.Text = "Entregar DP"
        Me.btnEntregar.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(988, 20)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Devuelto por"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Green
        Me.lblStatus.Location = New System.Drawing.Point(945, 121)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(209, 28)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "STATUS"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbPROVEEDORES
        '
        Me.cmbPROVEEDORES.AccessibleName = "*PROVEEDOR"
        Me.cmbPROVEEDORES.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbPROVEEDORES.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPROVEEDORES.DropDownHeight = 300
        Me.cmbPROVEEDORES.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPROVEEDORES.FormattingEnabled = True
        Me.cmbPROVEEDORES.IntegralHeight = False
        Me.cmbPROVEEDORES.Location = New System.Drawing.Point(521, 42)
        Me.cmbPROVEEDORES.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPROVEEDORES.Name = "cmbPROVEEDORES"
        Me.cmbPROVEEDORES.Size = New System.Drawing.Size(431, 28)
        Me.cmbPROVEEDORES.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1002, 100)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 20)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "Estado DP"
        '
        'PicProveedores
        '
        Me.PicProveedores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicProveedores.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicProveedores.Image = CType(resources.GetObject("PicProveedores.Image"), System.Drawing.Image)
        Me.PicProveedores.Location = New System.Drawing.Point(960, 44)
        Me.PicProveedores.Margin = New System.Windows.Forms.Padding(4)
        Me.PicProveedores.Name = "PicProveedores"
        Me.PicProveedores.Size = New System.Drawing.Size(24, 25)
        Me.PicProveedores.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicProveedores.TabIndex = 122
        Me.PicProveedores.TabStop = False
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(13, 97)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(725, 26)
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
        Me.Label8.Location = New System.Drawing.Point(-96, 69)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 17)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(1519, 42)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(91, 21)
        Me.chkEliminado.TabIndex = 10
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(513, 21)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 20)
        Me.Label7.TabIndex = 108
        Me.Label7.Text = "Proveedor*"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1402, 14)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1366, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
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
        Me.txtCODIGO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 43)
        Me.txtCODIGO.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(128, 26)
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
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 20)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro DP"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(149, 43)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 26)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(141, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 20)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem2, Me.BuscarDescripcionToolStripMenuItem3})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 148)
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
        Me.BuscarDescripcionToolStripMenuItem.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem2
        '
        Me.BuscarDescripcionToolStripMenuItem2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem2.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem2.Name = "BuscarDescripcionToolStripMenuItem2"
        Me.BuscarDescripcionToolStripMenuItem2.Size = New System.Drawing.Size(300, 28)
        Me.BuscarDescripcionToolStripMenuItem2.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem2.Text = "Buscar Descripcion"
        Me.BuscarDescripcionToolStripMenuItem2.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem3
        '
        Me.BuscarDescripcionToolStripMenuItem3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem3.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem3.Name = "BuscarDescripcionToolStripMenuItem3"
        Me.BuscarDescripcionToolStripMenuItem3.Size = New System.Drawing.Size(300, 28)
        Me.BuscarDescripcionToolStripMenuItem3.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem3.Text = "Buscar Descripcion"
        Me.BuscarDescripcionToolStripMenuItem3.Visible = False
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbUnidadVenta})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(361, 158)
        '
        'cmbUnidadVenta
        '
        Me.cmbUnidadVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbUnidadVenta.DropDownWidth = 500
        Me.cmbUnidadVenta.Name = "cmbUnidadVenta"
        Me.cmbUnidadVenta.Size = New System.Drawing.Size(300, 150)
        Me.cmbUnidadVenta.Text = "Unidad de Venta"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbMonedasCompra})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(361, 158)
        '
        'cmbMonedasCompra
        '
        Me.cmbMonedasCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbMonedasCompra.DropDownWidth = 500
        Me.cmbMonedasCompra.Name = "cmbMonedasCompra"
        Me.cmbMonedasCompra.Size = New System.Drawing.Size(300, 150)
        Me.cmbMonedasCompra.Text = "Buscar Moneda"
        '
        'ContextMenuMarcas
        '
        Me.ContextMenuMarcas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActivarNuevaMarcaToolStripMenuItem, Me.cmbMarcaCompra})
        Me.ContextMenuMarcas.Name = "ContextMenuStrip1"
        Me.ContextMenuMarcas.Size = New System.Drawing.Size(361, 182)
        '
        'ActivarNuevaMarcaToolStripMenuItem
        '
        Me.ActivarNuevaMarcaToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ActivarNuevaMarcaToolStripMenuItem.Name = "ActivarNuevaMarcaToolStripMenuItem"
        Me.ActivarNuevaMarcaToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.ActivarNuevaMarcaToolStripMenuItem.Text = "Activar Nueva Marca"
        '
        'cmbMarcaCompra
        '
        Me.cmbMarcaCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cmbMarcaCompra.DropDownWidth = 500
        Me.cmbMarcaCompra.Name = "cmbMarcaCompra"
        Me.cmbMarcaCompra.Size = New System.Drawing.Size(300, 150)
        Me.cmbMarcaCompra.Text = "Buscar Marca"
        '
        'frmDevolucionesProveedor
        '
        Me.AccessibleName = "OrdenDeCompra"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1620, 719)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmDevolucionesProveedor"
        Me.Text = "frmOrdenCompra"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEmpleados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicProveedores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.ContextMenuMarcas.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PicProveedores As System.Windows.Forms.PictureBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbPROVEEDORES As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents BuscarDescripcionToolStripMenuItem2 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents BuscarDescripcionToolStripMenuItem3 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbUnidadVenta As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbMonedasCompra As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnEntregar As System.Windows.Forms.Button
    Friend WithEvents txtIdProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbEmpleado As System.Windows.Forms.ComboBox
    Friend WithEvents rdAnuladas As System.Windows.Forms.RadioButton
    Friend WithEvents rdTodas As System.Windows.Forms.RadioButton
    Friend WithEvents rdPendientes As System.Windows.Forms.RadioButton
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents chkGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtMontoIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents ContextMenuMarcas As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ActivarNuevaMarcaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbMarcaCompra As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents picEmpleados As System.Windows.Forms.PictureBox
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkMaterialesProveedor As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents cmbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Codigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Item As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EquipoHerramienta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lote As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LoteProveed As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remito As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Estado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConCambio As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Nota_Det As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewButtonColumn
End Class
