<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRemitos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRemitos))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIdMoneda = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbMonedas = New System.Windows.Forms.ComboBox()
        Me.txtPresupuestos = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkFacturaAnuladaNC = New System.Windows.Forms.CheckBox()
        Me.chkFacturado = New System.Windows.Forms.CheckBox()
        Me.lblRemitoFacturado2 = New System.Windows.Forms.Label()
        Me.lblRemitoFacturado = New System.Windows.Forms.Label()
        Me.chkParaFacturar = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.grdItemsEspeciales = New System.Windows.Forms.DataGridView()
        Me.EquipoHerramienta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lblSubtotalItem = New System.Windows.Forms.Label()
        Me.txtSubtotalItem = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbDescripcionRemito = New System.Windows.Forms.ComboBox()
        Me.lblDescripcionRemito = New System.Windows.Forms.Label()
        Me.chkRemitoEspecial = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblFacturado = New System.Windows.Forms.Label()
        Me.lblMoneda = New System.Windows.Forms.Label()
        Me.lblDiferenciaRemito = New System.Windows.Forms.Label()
        Me.lblRemito = New System.Windows.Forms.Label()
        Me.lblPresupuesto = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtSubtotalDO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblSubtotalDO = New System.Windows.Forms.Label()
        Me.txtTotalDO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTotalDO = New System.Windows.Forms.Label()
        Me.txtMontoIvaDO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblIVA21DO = New System.Windows.Forms.Label()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.cmbIVA = New System.Windows.Forms.ComboBox()
        Me.txtNroOC = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkOCA = New System.Windows.Forms.CheckBox()
        Me.txtIdPresupuesto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.BtnFactura = New System.Windows.Forms.Button()
        Me.chkEntregaPendiente = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.picEntregartodos = New System.Windows.Forms.PictureBox()
        Me.btnEntregarTodos = New System.Windows.Forms.Button()
        Me.PicAnularRemito = New System.Windows.Forms.PictureBox()
        Me.txtCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtfactura = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbEntregaren = New System.Windows.Forms.ComboBox()
        Me.cmbUsuario = New System.Windows.Forms.ComboBox()
        Me.cmbComprador = New System.Windows.Forms.ComboBox()
        Me.chkEntrega = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtRemito = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPresupuestos = New System.Windows.Forms.ComboBox()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnLlenarGrilla = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkComprador = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkUsuario = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkPresupuesto = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItemsEspeciales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEntregartodos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicAnularRemito, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtIdMoneda)
        Me.GroupBox1.Controls.Add(Me.cmbMonedas)
        Me.GroupBox1.Controls.Add(Me.txtPresupuestos)
        Me.GroupBox1.Controls.Add(Me.chkFacturaAnuladaNC)
        Me.GroupBox1.Controls.Add(Me.chkFacturado)
        Me.GroupBox1.Controls.Add(Me.lblRemitoFacturado2)
        Me.GroupBox1.Controls.Add(Me.lblRemitoFacturado)
        Me.GroupBox1.Controls.Add(Me.chkParaFacturar)
        Me.GroupBox1.Controls.Add(Me.grdItemsEspeciales)
        Me.GroupBox1.Controls.Add(Me.lblSubtotalItem)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalItem)
        Me.GroupBox1.Controls.Add(Me.cmbDescripcionRemito)
        Me.GroupBox1.Controls.Add(Me.lblDescripcionRemito)
        Me.GroupBox1.Controls.Add(Me.chkRemitoEspecial)
        Me.GroupBox1.Controls.Add(Me.lblFacturado)
        Me.GroupBox1.Controls.Add(Me.lblMoneda)
        Me.GroupBox1.Controls.Add(Me.lblDiferenciaRemito)
        Me.GroupBox1.Controls.Add(Me.lblRemito)
        Me.GroupBox1.Controls.Add(Me.lblPresupuesto)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalDO)
        Me.GroupBox1.Controls.Add(Me.lblSubtotalDO)
        Me.GroupBox1.Controls.Add(Me.txtTotalDO)
        Me.GroupBox1.Controls.Add(Me.lblTotalDO)
        Me.GroupBox1.Controls.Add(Me.txtMontoIvaDO)
        Me.GroupBox1.Controls.Add(Me.lblIVA21DO)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.lblIVA)
        Me.GroupBox1.Controls.Add(Me.cmbIVA)
        Me.GroupBox1.Controls.Add(Me.txtNroOC)
        Me.GroupBox1.Controls.Add(Me.chkOCA)
        Me.GroupBox1.Controls.Add(Me.txtIdPresupuesto)
        Me.GroupBox1.Controls.Add(Me.BtnFactura)
        Me.GroupBox1.Controls.Add(Me.chkEntregaPendiente)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.picEntregartodos)
        Me.GroupBox1.Controls.Add(Me.btnEntregarTodos)
        Me.GroupBox1.Controls.Add(Me.PicAnularRemito)
        Me.GroupBox1.Controls.Add(Me.txtCliente)
        Me.GroupBox1.Controls.Add(Me.txtfactura)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbEntregaren)
        Me.GroupBox1.Controls.Add(Me.cmbUsuario)
        Me.GroupBox1.Controls.Add(Me.cmbComprador)
        Me.GroupBox1.Controls.Add(Me.chkEntrega)
        Me.GroupBox1.Controls.Add(Me.txtRemito)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbPresupuestos)
        Me.GroupBox1.Controls.Add(Me.btnAnular)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkComprador)
        Me.GroupBox1.Controls.Add(Me.chkUsuario)
        Me.GroupBox1.Controls.Add(Me.chkPresupuesto)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1338, 471)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtIdMoneda
        '
        Me.txtIdMoneda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdMoneda.Decimals = CType(2, Byte)
        Me.txtIdMoneda.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdMoneda.Enabled = False
        Me.txtIdMoneda.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdMoneda.Location = New System.Drawing.Point(687, 46)
        Me.txtIdMoneda.MaxLength = 8
        Me.txtIdMoneda.Name = "txtIdMoneda"
        Me.txtIdMoneda.Size = New System.Drawing.Size(23, 20)
        Me.txtIdMoneda.TabIndex = 294
        Me.txtIdMoneda.Text_1 = Nothing
        Me.txtIdMoneda.Text_2 = Nothing
        Me.txtIdMoneda.Text_3 = Nothing
        Me.txtIdMoneda.Text_4 = Nothing
        Me.txtIdMoneda.UserValues = Nothing
        Me.txtIdMoneda.Visible = False
        '
        'cmbMonedas
        '
        Me.cmbMonedas.AccessibleName = ""
        Me.cmbMonedas.DropDownHeight = 500
        Me.cmbMonedas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonedas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMonedas.FormattingEnabled = True
        Me.cmbMonedas.IntegralHeight = False
        Me.cmbMonedas.Location = New System.Drawing.Point(625, 31)
        Me.cmbMonedas.Name = "cmbMonedas"
        Me.cmbMonedas.Size = New System.Drawing.Size(69, 21)
        Me.cmbMonedas.TabIndex = 293
        Me.cmbMonedas.Visible = False
        '
        'txtPresupuestos
        '
        Me.txtPresupuestos.AccessibleName = ""
        Me.txtPresupuestos.BackColor = System.Drawing.SystemColors.Window
        Me.txtPresupuestos.Decimals = CType(2, Byte)
        Me.txtPresupuestos.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPresupuestos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresupuestos.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPresupuestos.Location = New System.Drawing.Point(625, 31)
        Me.txtPresupuestos.MaxLength = 25
        Me.txtPresupuestos.Name = "txtPresupuestos"
        Me.txtPresupuestos.ReadOnly = True
        Me.txtPresupuestos.Size = New System.Drawing.Size(299, 20)
        Me.txtPresupuestos.TabIndex = 8
        Me.txtPresupuestos.Text_1 = Nothing
        Me.txtPresupuestos.Text_2 = Nothing
        Me.txtPresupuestos.Text_3 = Nothing
        Me.txtPresupuestos.Text_4 = Nothing
        Me.txtPresupuestos.UserValues = Nothing
        '
        'chkFacturaAnuladaNC
        '
        Me.chkFacturaAnuladaNC.AccessibleName = "Eliminado"
        Me.chkFacturaAnuladaNC.AutoSize = True
        Me.chkFacturaAnuladaNC.Enabled = False
        Me.chkFacturaAnuladaNC.Location = New System.Drawing.Point(432, 199)
        Me.chkFacturaAnuladaNC.Name = "chkFacturaAnuladaNC"
        Me.chkFacturaAnuladaNC.Size = New System.Drawing.Size(116, 17)
        Me.chkFacturaAnuladaNC.TabIndex = 252
        Me.chkFacturaAnuladaNC.Text = "Facturado Anulada"
        Me.chkFacturaAnuladaNC.UseVisualStyleBackColor = True
        Me.chkFacturaAnuladaNC.Visible = False
        '
        'chkFacturado
        '
        Me.chkFacturado.AccessibleName = "Eliminado"
        Me.chkFacturado.AutoSize = True
        Me.chkFacturado.Enabled = False
        Me.chkFacturado.Location = New System.Drawing.Point(265, 196)
        Me.chkFacturado.Name = "chkFacturado"
        Me.chkFacturado.Size = New System.Drawing.Size(74, 17)
        Me.chkFacturado.TabIndex = 157
        Me.chkFacturado.Text = "Facturado"
        Me.chkFacturado.UseVisualStyleBackColor = True
        Me.chkFacturado.Visible = False
        '
        'lblRemitoFacturado2
        '
        Me.lblRemitoFacturado2.AutoSize = True
        Me.lblRemitoFacturado2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblRemitoFacturado2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemitoFacturado2.ForeColor = System.Drawing.Color.Red
        Me.lblRemitoFacturado2.Location = New System.Drawing.Point(416, 104)
        Me.lblRemitoFacturado2.Name = "lblRemitoFacturado2"
        Me.lblRemitoFacturado2.Size = New System.Drawing.Size(373, 20)
        Me.lblRemitoFacturado2.TabIndex = 164
        Me.lblRemitoFacturado2.Text = "No se puede modificar un remito ya facturado"
        Me.lblRemitoFacturado2.Visible = False
        '
        'lblRemitoFacturado
        '
        Me.lblRemitoFacturado.AutoSize = True
        Me.lblRemitoFacturado.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblRemitoFacturado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemitoFacturado.ForeColor = System.Drawing.Color.Red
        Me.lblRemitoFacturado.Location = New System.Drawing.Point(222, 104)
        Me.lblRemitoFacturado.Name = "lblRemitoFacturado"
        Me.lblRemitoFacturado.Size = New System.Drawing.Size(191, 20)
        Me.lblRemitoFacturado.TabIndex = 163
        Me.lblRemitoFacturado.Text = "REMITO FACTURADO"
        Me.lblRemitoFacturado.Visible = False
        '
        'chkParaFacturar
        '
        Me.chkParaFacturar.AutoSize = True
        '
        '
        '
        Me.chkParaFacturar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkParaFacturar.Location = New System.Drawing.Point(203, 35)
        Me.chkParaFacturar.Name = "chkParaFacturar"
        Me.chkParaFacturar.Size = New System.Drawing.Size(112, 15)
        Me.chkParaFacturar.TabIndex = 3
        Me.chkParaFacturar.Text = "Remito Facturable"
        Me.chkParaFacturar.TextColor = System.Drawing.Color.Blue
        '
        'grdItemsEspeciales
        '
        Me.grdItemsEspeciales.AllowUserToAddRows = False
        Me.grdItemsEspeciales.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.grdItemsEspeciales.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItemsEspeciales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdItemsEspeciales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemsEspeciales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EquipoHerramienta, Me.Subtotal, Me.Eliminar})
        Me.grdItemsEspeciales.Location = New System.Drawing.Point(12, 140)
        Me.grdItemsEspeciales.Name = "grdItemsEspeciales"
        Me.grdItemsEspeciales.Size = New System.Drawing.Size(1328, 299)
        Me.grdItemsEspeciales.TabIndex = 21
        Me.grdItemsEspeciales.Visible = False
        '
        'EquipoHerramienta
        '
        Me.EquipoHerramienta.HeaderText = "Descripción Remito"
        Me.EquipoHerramienta.Name = "EquipoHerramienta"
        Me.EquipoHerramienta.ReadOnly = True
        Me.EquipoHerramienta.Width = 1000
        '
        'Subtotal
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Subtotal.DefaultCellStyle = DataGridViewCellStyle3
        Me.Subtotal.HeaderText = "Subtotal"
        Me.Subtotal.Name = "Subtotal"
        Me.Subtotal.ReadOnly = True
        '
        'Eliminar
        '
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.ToolTipText = "Eliminar Registro"
        Me.Eliminar.UseColumnTextForButtonValue = True
        '
        'lblSubtotalItem
        '
        Me.lblSubtotalItem.AutoSize = True
        Me.lblSubtotalItem.BackColor = System.Drawing.Color.Transparent
        Me.lblSubtotalItem.ForeColor = System.Drawing.Color.Blue
        Me.lblSubtotalItem.Location = New System.Drawing.Point(717, 100)
        Me.lblSubtotalItem.Name = "lblSubtotalItem"
        Me.lblSubtotalItem.Size = New System.Drawing.Size(69, 13)
        Me.lblSubtotalItem.TabIndex = 251
        Me.lblSubtotalItem.Text = "Subtotal Ítem"
        '
        'txtSubtotalItem
        '
        Me.txtSubtotalItem.Decimals = CType(2, Byte)
        Me.txtSubtotalItem.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalItem.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotalItem.Location = New System.Drawing.Point(720, 116)
        Me.txtSubtotalItem.Name = "txtSubtotalItem"
        Me.txtSubtotalItem.Size = New System.Drawing.Size(80, 20)
        Me.txtSubtotalItem.TabIndex = 18
        Me.txtSubtotalItem.Text_1 = Nothing
        Me.txtSubtotalItem.Text_2 = Nothing
        Me.txtSubtotalItem.Text_3 = Nothing
        Me.txtSubtotalItem.Text_4 = Nothing
        Me.txtSubtotalItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotalItem.UserValues = Nothing
        '
        'cmbDescripcionRemito
        '
        Me.cmbDescripcionRemito.AccessibleName = ""
        Me.cmbDescripcionRemito.DropDownHeight = 450
        Me.cmbDescripcionRemito.FormattingEnabled = True
        Me.cmbDescripcionRemito.IntegralHeight = False
        Me.cmbDescripcionRemito.Location = New System.Drawing.Point(9, 115)
        Me.cmbDescripcionRemito.Name = "cmbDescripcionRemito"
        Me.cmbDescripcionRemito.Size = New System.Drawing.Size(705, 21)
        Me.cmbDescripcionRemito.TabIndex = 17
        '
        'lblDescripcionRemito
        '
        Me.lblDescripcionRemito.AutoSize = True
        Me.lblDescripcionRemito.BackColor = System.Drawing.Color.Transparent
        Me.lblDescripcionRemito.ForeColor = System.Drawing.Color.Blue
        Me.lblDescripcionRemito.Location = New System.Drawing.Point(6, 99)
        Me.lblDescripcionRemito.Name = "lblDescripcionRemito"
        Me.lblDescripcionRemito.Size = New System.Drawing.Size(99, 13)
        Me.lblDescripcionRemito.TabIndex = 250
        Me.lblDescripcionRemito.Text = "Descripción Remito"
        '
        'chkRemitoEspecial
        '
        Me.chkRemitoEspecial.AutoSize = True
        '
        '
        '
        Me.chkRemitoEspecial.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkRemitoEspecial.Location = New System.Drawing.Point(203, 16)
        Me.chkRemitoEspecial.Name = "chkRemitoEspecial"
        Me.chkRemitoEspecial.Size = New System.Drawing.Size(102, 15)
        Me.chkRemitoEspecial.TabIndex = 2
        Me.chkRemitoEspecial.Text = "Remito Especial"
        Me.chkRemitoEspecial.TextColor = System.Drawing.Color.Blue
        '
        'lblFacturado
        '
        Me.lblFacturado.BackColor = System.Drawing.Color.White
        Me.lblFacturado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFacturado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacturado.ForeColor = System.Drawing.Color.Red
        Me.lblFacturado.Location = New System.Drawing.Point(1225, 92)
        Me.lblFacturado.Name = "lblFacturado"
        Me.lblFacturado.Size = New System.Drawing.Size(105, 15)
        Me.lblFacturado.TabIndex = 246
        Me.lblFacturado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMoneda
        '
        Me.lblMoneda.BackColor = System.Drawing.Color.White
        Me.lblMoneda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda.ForeColor = System.Drawing.Color.Red
        Me.lblMoneda.Location = New System.Drawing.Point(1225, 52)
        Me.lblMoneda.Name = "lblMoneda"
        Me.lblMoneda.Size = New System.Drawing.Size(105, 15)
        Me.lblMoneda.TabIndex = 245
        Me.lblMoneda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiferenciaRemito
        '
        Me.lblDiferenciaRemito.BackColor = System.Drawing.Color.White
        Me.lblDiferenciaRemito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiferenciaRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferenciaRemito.ForeColor = System.Drawing.Color.Red
        Me.lblDiferenciaRemito.Location = New System.Drawing.Point(1225, 111)
        Me.lblDiferenciaRemito.Name = "lblDiferenciaRemito"
        Me.lblDiferenciaRemito.Size = New System.Drawing.Size(105, 15)
        Me.lblDiferenciaRemito.TabIndex = 244
        Me.lblDiferenciaRemito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRemito
        '
        Me.lblRemito.BackColor = System.Drawing.Color.White
        Me.lblRemito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemito.ForeColor = System.Drawing.Color.Red
        Me.lblRemito.Location = New System.Drawing.Point(1225, 72)
        Me.lblRemito.Name = "lblRemito"
        Me.lblRemito.Size = New System.Drawing.Size(105, 15)
        Me.lblRemito.TabIndex = 243
        Me.lblRemito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPresupuesto
        '
        Me.lblPresupuesto.BackColor = System.Drawing.Color.White
        Me.lblPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPresupuesto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPresupuesto.ForeColor = System.Drawing.Color.Red
        Me.lblPresupuesto.Location = New System.Drawing.Point(1225, 32)
        Me.lblPresupuesto.Name = "lblPresupuesto"
        Me.lblPresupuesto.Size = New System.Drawing.Size(105, 15)
        Me.lblPresupuesto.TabIndex = 242
        Me.lblPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(1126, 33)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 13)
        Me.Label15.TabIndex = 241
        Me.Label15.Text = "Total Presupuesto"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(1129, 113)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(90, 13)
        Me.Label14.TabIndex = 240
        Me.Label14.Text = "Monto por Remitir"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(1137, 94)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 13)
        Me.Label13.TabIndex = 239
        Me.Label13.Text = "Total Facturado"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(1149, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 13)
        Me.Label12.TabIndex = 238
        Me.Label12.Text = "Moneda Pres"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(1144, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 237
        Me.Label9.Text = "Total Remitido"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(1150, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(164, 13)
        Me.Label16.TabIndex = 236
        Me.Label16.Text = "Información sobre el Presupuesto"
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = ""
        Me.txtIVA.BackColor = System.Drawing.SystemColors.Window
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIVA.Location = New System.Drawing.Point(930, 31)
        Me.txtIVA.MaxLength = 25
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.ReadOnly = True
        Me.txtIVA.Size = New System.Drawing.Size(56, 20)
        Me.txtIVA.TabIndex = 10
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        '
        'txtSubtotalDO
        '
        Me.txtSubtotalDO.AccessibleName = ""
        Me.txtSubtotalDO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotalDO.Decimals = CType(2, Byte)
        Me.txtSubtotalDO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalDO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotalDO.ForeColor = System.Drawing.Color.Red
        Me.txtSubtotalDO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtSubtotalDO.Location = New System.Drawing.Point(687, 445)
        Me.txtSubtotalDO.MaxLength = 50
        Me.txtSubtotalDO.Name = "txtSubtotalDO"
        Me.txtSubtotalDO.ReadOnly = True
        Me.txtSubtotalDO.Size = New System.Drawing.Size(84, 20)
        Me.txtSubtotalDO.TabIndex = 22
        Me.txtSubtotalDO.Text_1 = Nothing
        Me.txtSubtotalDO.Text_2 = Nothing
        Me.txtSubtotalDO.Text_3 = Nothing
        Me.txtSubtotalDO.Text_4 = Nothing
        Me.txtSubtotalDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotalDO.UserValues = Nothing
        '
        'lblSubtotalDO
        '
        Me.lblSubtotalDO.AutoSize = True
        Me.lblSubtotalDO.Location = New System.Drawing.Point(635, 448)
        Me.lblSubtotalDO.Name = "lblSubtotalDO"
        Me.lblSubtotalDO.Size = New System.Drawing.Size(46, 13)
        Me.lblSubtotalDO.TabIndex = 221
        Me.lblSubtotalDO.Text = "Subtotal"
        '
        'txtTotalDO
        '
        Me.txtTotalDO.AccessibleName = ""
        Me.txtTotalDO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotalDO.Decimals = CType(2, Byte)
        Me.txtTotalDO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalDO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDO.ForeColor = System.Drawing.Color.Red
        Me.txtTotalDO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotalDO.Location = New System.Drawing.Point(967, 445)
        Me.txtTotalDO.MaxLength = 50
        Me.txtTotalDO.Name = "txtTotalDO"
        Me.txtTotalDO.ReadOnly = True
        Me.txtTotalDO.Size = New System.Drawing.Size(84, 20)
        Me.txtTotalDO.TabIndex = 24
        Me.txtTotalDO.Text_1 = Nothing
        Me.txtTotalDO.Text_2 = Nothing
        Me.txtTotalDO.Text_3 = Nothing
        Me.txtTotalDO.Text_4 = Nothing
        Me.txtTotalDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalDO.UserValues = Nothing
        '
        'lblTotalDO
        '
        Me.lblTotalDO.AutoSize = True
        Me.lblTotalDO.Location = New System.Drawing.Point(930, 448)
        Me.lblTotalDO.Name = "lblTotalDO"
        Me.lblTotalDO.Size = New System.Drawing.Size(31, 13)
        Me.lblTotalDO.TabIndex = 219
        Me.lblTotalDO.Text = "Total"
        '
        'txtMontoIvaDO
        '
        Me.txtMontoIvaDO.AccessibleName = ""
        Me.txtMontoIvaDO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoIvaDO.Decimals = CType(2, Byte)
        Me.txtMontoIvaDO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoIvaDO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIvaDO.ForeColor = System.Drawing.Color.Red
        Me.txtMontoIvaDO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtMontoIvaDO.Location = New System.Drawing.Point(840, 445)
        Me.txtMontoIvaDO.MaxLength = 50
        Me.txtMontoIvaDO.Name = "txtMontoIvaDO"
        Me.txtMontoIvaDO.ReadOnly = True
        Me.txtMontoIvaDO.Size = New System.Drawing.Size(84, 20)
        Me.txtMontoIvaDO.TabIndex = 23
        Me.txtMontoIvaDO.Text_1 = Nothing
        Me.txtMontoIvaDO.Text_2 = Nothing
        Me.txtMontoIvaDO.Text_3 = Nothing
        Me.txtMontoIvaDO.Text_4 = Nothing
        Me.txtMontoIvaDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIvaDO.UserValues = Nothing
        '
        'lblIVA21DO
        '
        Me.lblIVA21DO.AutoSize = True
        Me.lblIVA21DO.Location = New System.Drawing.Point(777, 448)
        Me.lblIVA21DO.Name = "lblIVA21DO"
        Me.lblIVA21DO.Size = New System.Drawing.Size(57, 13)
        Me.lblIVA21DO.TabIndex = 215
        Me.lblIVA21DO.Text = "Monto IVA"
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(537, 11)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(23, 20)
        Me.txtIdCliente.TabIndex = 197
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'lblIVA
        '
        Me.lblIVA.AutoSize = True
        Me.lblIVA.Location = New System.Drawing.Point(927, 15)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(24, 13)
        Me.lblIVA.TabIndex = 196
        Me.lblIVA.Text = "IVA"
        '
        'cmbIVA
        '
        Me.cmbIVA.AccessibleName = ""
        Me.cmbIVA.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbIVA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIVA.DropDownHeight = 300
        Me.cmbIVA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIVA.FormattingEnabled = True
        Me.cmbIVA.IntegralHeight = False
        Me.cmbIVA.Location = New System.Drawing.Point(930, 31)
        Me.cmbIVA.Name = "cmbIVA"
        Me.cmbIVA.Size = New System.Drawing.Size(54, 21)
        Me.cmbIVA.TabIndex = 9
        '
        'txtNroOC
        '
        Me.txtNroOC.AccessibleName = ""
        Me.txtNroOC.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroOC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroOC.Decimals = CType(2, Byte)
        Me.txtNroOC.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroOC.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroOC.Location = New System.Drawing.Point(990, 31)
        Me.txtNroOC.MaxLength = 25
        Me.txtNroOC.Name = "txtNroOC"
        Me.txtNroOC.Size = New System.Drawing.Size(119, 20)
        Me.txtNroOC.TabIndex = 11
        Me.txtNroOC.Text_1 = Nothing
        Me.txtNroOC.Text_2 = Nothing
        Me.txtNroOC.Text_3 = Nothing
        Me.txtNroOC.Text_4 = Nothing
        Me.txtNroOC.UserValues = Nothing
        '
        'chkOCA
        '
        Me.chkOCA.AutoSize = True
        Me.chkOCA.Location = New System.Drawing.Point(930, 277)
        Me.chkOCA.Name = "chkOCA"
        Me.chkOCA.Size = New System.Drawing.Size(44, 17)
        Me.chkOCA.TabIndex = 193
        Me.chkOCA.Text = "oca"
        Me.chkOCA.UseVisualStyleBackColor = True
        Me.chkOCA.Visible = False
        '
        'txtIdPresupuesto
        '
        Me.txtIdPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdPresupuesto.Decimals = CType(2, Byte)
        Me.txtIdPresupuesto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdPresupuesto.Enabled = False
        Me.txtIdPresupuesto.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdPresupuesto.Location = New System.Drawing.Point(870, 19)
        Me.txtIdPresupuesto.MaxLength = 8
        Me.txtIdPresupuesto.Name = "txtIdPresupuesto"
        Me.txtIdPresupuesto.Size = New System.Drawing.Size(23, 20)
        Me.txtIdPresupuesto.TabIndex = 192
        Me.txtIdPresupuesto.Text_1 = Nothing
        Me.txtIdPresupuesto.Text_2 = Nothing
        Me.txtIdPresupuesto.Text_3 = Nothing
        Me.txtIdPresupuesto.Text_4 = Nothing
        Me.txtIdPresupuesto.UserValues = Nothing
        Me.txtIdPresupuesto.Visible = False
        '
        'BtnFactura
        '
        Me.BtnFactura.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnFactura.Enabled = False
        Me.BtnFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFactura.ForeColor = System.Drawing.Color.Red
        Me.BtnFactura.Location = New System.Drawing.Point(1004, 107)
        Me.BtnFactura.Name = "BtnFactura"
        Me.BtnFactura.Size = New System.Drawing.Size(97, 23)
        Me.BtnFactura.TabIndex = 20
        Me.BtnFactura.Text = "Factura (F7) >>"
        Me.BtnFactura.UseVisualStyleBackColor = True
        Me.BtnFactura.Visible = False
        '
        'chkEntregaPendiente
        '
        Me.chkEntregaPendiente.AutoSize = True
        '
        '
        '
        Me.chkEntregaPendiente.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkEntregaPendiente.Location = New System.Drawing.Point(1014, 77)
        Me.chkEntregaPendiente.Name = "chkEntregaPendiente"
        Me.chkEntregaPendiente.Size = New System.Drawing.Size(114, 15)
        Me.chkEntregaPendiente.TabIndex = 16
        Me.chkEntregaPendiente.Text = "Entrega Pendiente"
        Me.chkEntregaPendiente.TextColor = System.Drawing.Color.Blue
        '
        'chkAnulados
        '
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnulados.ForeColor = System.Drawing.Color.Red
        Me.chkAnulados.Location = New System.Drawing.Point(1190, 448)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(150, 17)
        Me.chkAnulados.TabIndex = 25
        Me.chkAnulados.Text = "Ver Remitos Anulados"
        Me.chkAnulados.UseVisualStyleBackColor = True
        '
        'picEntregartodos
        '
        Me.picEntregartodos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picEntregartodos.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picEntregartodos.Image = Global.SEYC.My.Resources.Resources.icono_ayuda
        Me.picEntregartodos.Location = New System.Drawing.Point(967, 110)
        Me.picEntregartodos.Name = "picEntregartodos"
        Me.picEntregartodos.Size = New System.Drawing.Size(18, 20)
        Me.picEntregartodos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEntregartodos.TabIndex = 160
        Me.picEntregartodos.TabStop = False
        '
        'btnEntregarTodos
        '
        Me.btnEntregarTodos.Enabled = False
        Me.btnEntregarTodos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEntregarTodos.Location = New System.Drawing.Point(822, 107)
        Me.btnEntregarTodos.Name = "btnEntregarTodos"
        Me.btnEntregarTodos.Size = New System.Drawing.Size(139, 23)
        Me.btnEntregarTodos.TabIndex = 19
        Me.btnEntregarTodos.Text = "Entregar Todos (F9)"
        Me.btnEntregarTodos.UseVisualStyleBackColor = True
        '
        'PicAnularRemito
        '
        Me.PicAnularRemito.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicAnularRemito.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicAnularRemito.Image = Global.SEYC.My.Resources.Resources.icono_ayuda
        Me.PicAnularRemito.Location = New System.Drawing.Point(726, 212)
        Me.PicAnularRemito.Name = "PicAnularRemito"
        Me.PicAnularRemito.Size = New System.Drawing.Size(18, 20)
        Me.PicAnularRemito.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicAnularRemito.TabIndex = 155
        Me.PicAnularRemito.TabStop = False
        Me.PicAnularRemito.Visible = False
        '
        'txtCliente
        '
        Me.txtCliente.AccessibleName = ""
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCliente.Decimals = CType(2, Byte)
        Me.txtCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCliente.Location = New System.Drawing.Point(326, 31)
        Me.txtCliente.MaxLength = 25
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(293, 20)
        Me.txtCliente.TabIndex = 6
        Me.txtCliente.Text_1 = Nothing
        Me.txtCliente.Text_2 = Nothing
        Me.txtCliente.Text_3 = Nothing
        Me.txtCliente.Text_4 = Nothing
        Me.txtCliente.UserValues = Nothing
        '
        'txtfactura
        '
        Me.txtfactura.AccessibleName = ""
        Me.txtfactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtfactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtfactura.Decimals = CType(2, Byte)
        Me.txtfactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtfactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfactura.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtfactura.Location = New System.Drawing.Point(478, 196)
        Me.txtfactura.MaxLength = 25
        Me.txtfactura.Name = "txtfactura"
        Me.txtfactura.Size = New System.Drawing.Size(147, 20)
        Me.txtfactura.TabIndex = 10
        Me.txtfactura.Text_1 = Nothing
        Me.txtfactura.Text_2 = Nothing
        Me.txtfactura.Text_3 = Nothing
        Me.txtfactura.Text_4 = Nothing
        Me.txtfactura.UserValues = Nothing
        Me.txtfactura.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(475, 182)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 143
        Me.Label10.Text = "Nro Factura"
        Me.Label10.Visible = False
        '
        'cmbEntregaren
        '
        Me.cmbEntregaren.AccessibleName = ""
        Me.cmbEntregaren.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbEntregaren.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEntregaren.DropDownHeight = 300
        Me.cmbEntregaren.Enabled = False
        Me.cmbEntregaren.FormattingEnabled = True
        Me.cmbEntregaren.IntegralHeight = False
        Me.cmbEntregaren.Location = New System.Drawing.Point(417, 74)
        Me.cmbEntregaren.Name = "cmbEntregaren"
        Me.cmbEntregaren.Size = New System.Drawing.Size(185, 21)
        Me.cmbEntregaren.TabIndex = 14
        '
        'cmbUsuario
        '
        Me.cmbUsuario.AccessibleName = ""
        Me.cmbUsuario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbUsuario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbUsuario.DropDownHeight = 300
        Me.cmbUsuario.Enabled = False
        Me.cmbUsuario.FormattingEnabled = True
        Me.cmbUsuario.IntegralHeight = False
        Me.cmbUsuario.Location = New System.Drawing.Point(215, 74)
        Me.cmbUsuario.Name = "cmbUsuario"
        Me.cmbUsuario.Size = New System.Drawing.Size(196, 21)
        Me.cmbUsuario.TabIndex = 13
        '
        'cmbComprador
        '
        Me.cmbComprador.AccessibleName = ""
        Me.cmbComprador.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbComprador.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbComprador.DropDownHeight = 300
        Me.cmbComprador.Enabled = False
        Me.cmbComprador.FormattingEnabled = True
        Me.cmbComprador.IntegralHeight = False
        Me.cmbComprador.Location = New System.Drawing.Point(13, 74)
        Me.cmbComprador.Name = "cmbComprador"
        Me.cmbComprador.Size = New System.Drawing.Size(196, 21)
        Me.cmbComprador.TabIndex = 12
        '
        'chkEntrega
        '
        Me.chkEntrega.AutoSize = True
        '
        '
        '
        Me.chkEntrega.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkEntrega.Location = New System.Drawing.Point(417, 56)
        Me.chkEntrega.Name = "chkEntrega"
        Me.chkEntrega.Size = New System.Drawing.Size(89, 15)
        Me.chkEntrega.TabIndex = 15
        Me.chkEntrega.Text = "Entregar en..."
        Me.chkEntrega.TextColor = System.Drawing.Color.Blue
        '
        'txtRemito
        '
        Me.txtRemito.AccessibleName = ""
        Me.txtRemito.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemito.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemito.Decimals = CType(2, Byte)
        Me.txtRemito.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemito.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtRemito.Location = New System.Drawing.Point(138, 235)
        Me.txtRemito.MaxLength = 25
        Me.txtRemito.Name = "txtRemito"
        Me.txtRemito.Size = New System.Drawing.Size(186, 20)
        Me.txtRemito.TabIndex = 8
        Me.txtRemito.Text_1 = Nothing
        Me.txtRemito.Text_2 = Nothing
        Me.txtRemito.Text_3 = Nothing
        Me.txtRemito.Text_4 = Nothing
        Me.txtRemito.UserValues = Nothing
        Me.txtRemito.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(137, 219)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Nro Remito"
        Me.Label4.Visible = False
        '
        'cmbPresupuestos
        '
        Me.cmbPresupuestos.AccessibleName = ""
        Me.cmbPresupuestos.DropDownHeight = 300
        Me.cmbPresupuestos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPresupuestos.FormattingEnabled = True
        Me.cmbPresupuestos.IntegralHeight = False
        Me.cmbPresupuestos.Location = New System.Drawing.Point(625, 31)
        Me.cmbPresupuestos.Name = "cmbPresupuestos"
        Me.cmbPresupuestos.Size = New System.Drawing.Size(299, 21)
        Me.cmbPresupuestos.TabIndex = 7
        '
        'btnAnular
        '
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.Location = New System.Drawing.Point(751, 258)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(139, 23)
        Me.btnAnular.TabIndex = 19
        Me.btnAnular.Text = "Anular Remito Físico"
        Me.btnAnular.UseVisualStyleBackColor = True
        Me.btnAnular.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(987, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 128
        Me.Label6.Text = "Nro OC"
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(917, 214)
        Me.btnLlenarGrilla.Name = "btnLlenarGrilla"
        Me.btnLlenarGrilla.Size = New System.Drawing.Size(102, 23)
        Me.btnLlenarGrilla.TabIndex = 6
        Me.btnLlenarGrilla.Text = "Cargar Items"
        Me.btnLlenarGrilla.UseVisualStyleBackColor = True
        Me.btnLlenarGrilla.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(323, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 125
        Me.Label5.Text = "Cliente*"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = ""
        Me.cmbCliente.DropDownHeight = 500
        Me.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(326, 31)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(293, 21)
        Me.cmbCliente.TabIndex = 5
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
        Me.txtNota.Location = New System.Drawing.Point(608, 74)
        Me.txtNota.MaxLength = 200
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(392, 20)
        Me.txtNota.TabIndex = 15
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(605, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 13)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota de Gestión"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(12, 140)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1320, 299)
        Me.grdItems.TabIndex = 19
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1078, 5)
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
        Me.Label1.Location = New System.Drawing.Point(1115, 8)
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
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 31)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(76, 20)
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
        Me.Label2.Location = New System.Drawing.Point(9, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro. Mov"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(95, 31)
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
        Me.Label3.Location = New System.Drawing.Point(95, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'chkComprador
        '
        Me.chkComprador.AutoSize = True
        '
        '
        '
        Me.chkComprador.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkComprador.Location = New System.Drawing.Point(13, 56)
        Me.chkComprador.Name = "chkComprador"
        Me.chkComprador.Size = New System.Drawing.Size(78, 15)
        Me.chkComprador.TabIndex = 11
        Me.chkComprador.Text = "Comprador"
        Me.chkComprador.TextColor = System.Drawing.Color.Blue
        '
        'chkUsuario
        '
        Me.chkUsuario.AutoSize = True
        '
        '
        '
        Me.chkUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkUsuario.Location = New System.Drawing.Point(215, 56)
        Me.chkUsuario.Name = "chkUsuario"
        Me.chkUsuario.Size = New System.Drawing.Size(60, 15)
        Me.chkUsuario.TabIndex = 13
        Me.chkUsuario.Text = "Usuario"
        Me.chkUsuario.TextColor = System.Drawing.Color.Blue
        '
        'chkPresupuesto
        '
        Me.chkPresupuesto.AutoSize = True
        Me.chkPresupuesto.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        '
        '
        Me.chkPresupuesto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkPresupuesto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPresupuesto.Location = New System.Drawing.Point(625, 14)
        Me.chkPresupuesto.Name = "chkPresupuesto"
        Me.chkPresupuesto.Size = New System.Drawing.Size(176, 17)
        Me.chkPresupuesto.TabIndex = 254
        Me.chkPresupuesto.Text = "Remito con Presupuesto*"
        Me.chkPresupuesto.TextColor = System.Drawing.Color.White
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
        'frmRemitos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 635)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRemitos"
        Me.Text = "Gestión de Presupuestos"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItemsEspeciales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEntregartodos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicAnularRemito, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
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
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents cmbPresupuestos As System.Windows.Forms.ComboBox
    Friend WithEvents txtRemito As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbEntregaren As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUsuario As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComprador As System.Windows.Forms.ComboBox
    Friend WithEvents chkUsuario As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkEntrega As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkComprador As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtfactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPresupuestos As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PicAnularRemito As System.Windows.Forms.PictureBox
    Friend WithEvents chkFacturado As System.Windows.Forms.CheckBox
    Friend WithEvents picEntregartodos As System.Windows.Forms.PictureBox
    Friend WithEvents btnEntregarTodos As System.Windows.Forms.Button
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents lblRemitoFacturado2 As System.Windows.Forms.Label
    Friend WithEvents lblRemitoFacturado As System.Windows.Forms.Label
    Friend WithEvents chkEntregaPendiente As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents BtnFactura As System.Windows.Forms.Button
    Friend WithEvents txtIdPresupuesto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkOCA As System.Windows.Forms.CheckBox
    Friend WithEvents txtNroOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Friend WithEvents cmbIVA As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtTotalDO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTotalDO As System.Windows.Forms.Label
    Friend WithEvents txtMontoIvaDO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblIVA21DO As System.Windows.Forms.Label
    Friend WithEvents txtSubtotalDO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblSubtotalDO As System.Windows.Forms.Label
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblFacturado As System.Windows.Forms.Label
    Friend WithEvents lblMoneda As System.Windows.Forms.Label
    Friend WithEvents lblDiferenciaRemito As System.Windows.Forms.Label
    Friend WithEvents lblRemito As System.Windows.Forms.Label
    Friend WithEvents lblPresupuesto As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkRemitoEspecial As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblSubtotalItem As System.Windows.Forms.Label
    Friend WithEvents txtSubtotalItem As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbDescripcionRemito As System.Windows.Forms.ComboBox
    Friend WithEvents lblDescripcionRemito As System.Windows.Forms.Label
    Friend WithEvents grdItemsEspeciales As System.Windows.Forms.DataGridView
    Friend WithEvents EquipoHerramienta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subtotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents chkParaFacturar As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkFacturaAnuladaNC As System.Windows.Forms.CheckBox
    Friend WithEvents chkPresupuesto As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cmbMonedas As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdMoneda As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
