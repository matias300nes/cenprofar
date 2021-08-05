<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPagodeClientes
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPagodeClientes))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbClientes = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkDescuento = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDescuento = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbRepartidor = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtValorCambio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdFacturacion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtMontoIva = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblMontoSinIVA = New System.Windows.Forms.Label()
        Me.lblMontoIVA = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotalaPagarSinIva = New System.Windows.Forms.Label()
        Me.chkAplicarIvaParcial = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabImpuestos = New System.Windows.Forms.TabPage()
        Me.grdImpuestos = New System.Windows.Forms.DataGridView()
        Me.TabCheques = New System.Windows.Forms.TabPage()
        Me.btnModificarCheque = New DevComponents.DotNetBar.ButtonX()
        Me.txtObservacionesCheque = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPropietario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnNuevoCheque = New DevComponents.DotNetBar.ButtonX()
        Me.txtMontoCheque = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNroCheque = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.cmbMoneda = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem5 = New DevComponents.Editors.ComboItem()
        Me.ComboItem6 = New DevComponents.Editors.ComboItem()
        Me.btnAgregarCheque = New DevComponents.DotNetBar.ButtonX()
        Me.btnEliminarCheque = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.dtpFechaCheque = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.cmbBanco = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem3 = New DevComponents.Editors.ComboItem()
        Me.ComboItem4 = New DevComponents.Editors.ComboItem()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.grdCheques = New System.Windows.Forms.DataGridView()
        Me.NroCheque = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Banco = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Monto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaVenc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdTipoMoneda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Observaciones = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdCheque = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Utilizado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabTransferencias = New System.Windows.Forms.TabPage()
        Me.btnModificarTransf = New DevComponents.DotNetBar.ButtonX()
        Me.txtObservacionesTransf = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX22 = New DevComponents.DotNetBar.LabelX()
        Me.btnNuevoTransferencia = New DevComponents.DotNetBar.ButtonX()
        Me.cmbCuentaDestino = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem17 = New DevComponents.Editors.ComboItem()
        Me.ComboItem18 = New DevComponents.Editors.ComboItem()
        Me.cmbCuentaOrigen = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem13 = New DevComponents.Editors.ComboItem()
        Me.ComboItem14 = New DevComponents.Editors.ComboItem()
        Me.txtNroOpCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX21 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.cmbBancoOrigen = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem11 = New DevComponents.Editors.ComboItem()
        Me.ComboItem12 = New DevComponents.Editors.ComboItem()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.txtMontoTransf = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.cmbMonedaTransf = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem7 = New DevComponents.Editors.ComboItem()
        Me.ComboItem8 = New DevComponents.Editors.ComboItem()
        Me.btnAgregarTransf = New DevComponents.DotNetBar.ButtonX()
        Me.btnEliminarTransf = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.dtpFechaTransf = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX18 = New DevComponents.DotNetBar.LabelX()
        Me.cmbBancoDestino = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem9 = New DevComponents.Editors.ComboItem()
        Me.ComboItem10 = New DevComponents.Editors.ComboItem()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.grdTransferencias = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BancoDestino = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObservacionesTransf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabTarjetas = New System.Windows.Forms.TabPage()
        Me.lblRecargoTarjeta = New System.Windows.Forms.Label()
        Me.lblCuotasTarjeta = New System.Windows.Forms.Label()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.lblMontoTotalTarjeta = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.btnModificarTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.btnNuevoTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.txtMontoTarjeta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnAgregarTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.btnEliminarTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX23 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX25 = New DevComponents.DotNetBar.LabelX()
        Me.cmbTarjetas = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.grdTarjetas = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cuotas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MontoSinRecargo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodigoTarjeta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gpPago = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtAyIDepUsado = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.ChkAyIDep = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRedondeo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblResto = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblEntregaTransferencias = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblEntregaTarjetas = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblEntregaImpuestos = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblTotalaPagar = New System.Windows.Forms.Label()
        Me.txtEntregaContado = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblEntregaCheques = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblEntregado = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.txtOrdenPago = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdFacturasConsumos = New System.Windows.Forms.DataGridView()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.ComboItem28 = New DevComponents.Editors.ComboItem()
        Me.ComboItem27 = New DevComponents.Editors.ComboItem()
        Me.ComboItem26 = New DevComponents.Editors.ComboItem()
        Me.ComboItem25 = New DevComponents.Editors.ComboItem()
        Me.ComboItem24 = New DevComponents.Editors.ComboItem()
        Me.ComboItem23 = New DevComponents.Editors.ComboItem()
        Me.ComboItem22 = New DevComponents.Editors.ComboItem()
        Me.ComboItem21 = New DevComponents.Editors.ComboItem()
        Me.ComboItem16 = New DevComponents.Editors.ComboItem()
        Me.ComboItem15 = New DevComponents.Editors.ComboItem()
        Me.ComboItem2 = New DevComponents.Editors.ComboItem()
        Me.ComboItem1 = New DevComponents.Editors.ComboItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabImpuestos.SuspendLayout()
        CType(Me.grdImpuestos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCheques.SuspendLayout()
        CType(Me.dtpFechaCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTransferencias.SuspendLayout()
        CType(Me.dtpFechaTransf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTransferencias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTarjetas.SuspendLayout()
        CType(Me.grdTarjetas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpPago.SuspendLayout()
        CType(Me.grdFacturasConsumos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(82, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(85, 31)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro Mov"
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
        Me.txtCODIGO.Size = New System.Drawing.Size(66, 20)
        Me.txtCODIGO.TabIndex = 0
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1093, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(634, 167)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(71, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'cmbClientes
        '
        Me.cmbClientes.AccessibleName = "*CLIENTE"
        Me.cmbClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbClientes.DropDownHeight = 300
        Me.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClientes.FormattingEnabled = True
        Me.cmbClientes.IntegralHeight = False
        Me.cmbClientes.Location = New System.Drawing.Point(381, 29)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(272, 21)
        Me.cmbClientes.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(379, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 125
        Me.Label5.Text = "Cliente*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkDescuento)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtDescuento)
        Me.GroupBox1.Controls.Add(Me.cmbRepartidor)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtValorCambio)
        Me.GroupBox1.Controls.Add(Me.txtIdFacturacion)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtMontoIva)
        Me.GroupBox1.Controls.Add(Me.lblMontoSinIVA)
        Me.GroupBox1.Controls.Add(Me.lblMontoIVA)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblTotalaPagarSinIva)
        Me.GroupBox1.Controls.Add(Me.chkAplicarIvaParcial)
        Me.GroupBox1.Controls.Add(Me.txtCliente)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.TabControl1)
        Me.GroupBox1.Controls.Add(Me.gpPago)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblIVA)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblSubtotal)
        Me.GroupBox1.Controls.Add(Me.txtOrdenPago)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdFacturasConsumos)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbClientes)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1680, 376)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkDescuento
        '
        Me.chkDescuento.AutoSize = True
        Me.chkDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDescuento.Location = New System.Drawing.Point(416, 351)
        Me.chkDescuento.Name = "chkDescuento"
        Me.chkDescuento.Size = New System.Drawing.Size(94, 19)
        Me.chkDescuento.TabIndex = 973
        Me.chkDescuento.Text = "Descuento"
        Me.chkDescuento.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(193, 12)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 13)
        Me.Label20.TabIndex = 972
        Me.Label20.Text = "Repartidor"
        '
        'txtDescuento
        '
        Me.txtDescuento.Decimals = CType(2, Byte)
        Me.txtDescuento.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescuento.Enabled = False
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtDescuento.Location = New System.Drawing.Point(516, 350)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(73, 20)
        Me.txtDescuento.TabIndex = 45
        Me.txtDescuento.Text = "0"
        Me.txtDescuento.Text_1 = Nothing
        Me.txtDescuento.Text_2 = Nothing
        Me.txtDescuento.Text_3 = Nothing
        Me.txtDescuento.Text_4 = Nothing
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDescuento.UserValues = Nothing
        '
        'cmbRepartidor
        '
        Me.cmbRepartidor.AccessibleName = ""
        Me.cmbRepartidor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbRepartidor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRepartidor.DropDownHeight = 300
        Me.cmbRepartidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRepartidor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRepartidor.FormattingEnabled = True
        Me.cmbRepartidor.IntegralHeight = False
        Me.cmbRepartidor.Location = New System.Drawing.Point(192, 29)
        Me.cmbRepartidor.Name = "cmbRepartidor"
        Me.cmbRepartidor.Size = New System.Drawing.Size(184, 21)
        Me.cmbRepartidor.TabIndex = 2
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(8, 54)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(69, 13)
        Me.Label26.TabIndex = 970
        Me.Label26.Text = "Valor Cambio"
        '
        'txtValorCambio
        '
        Me.txtValorCambio.AccessibleName = ""
        Me.txtValorCambio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtValorCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtValorCambio.Decimals = CType(2, Byte)
        Me.txtValorCambio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtValorCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorCambio.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtValorCambio.Location = New System.Drawing.Point(13, 70)
        Me.txtValorCambio.MaxLength = 300
        Me.txtValorCambio.Name = "txtValorCambio"
        Me.txtValorCambio.Size = New System.Drawing.Size(408, 20)
        Me.txtValorCambio.TabIndex = 5
        Me.txtValorCambio.Text_1 = Nothing
        Me.txtValorCambio.Text_2 = Nothing
        Me.txtValorCambio.Text_3 = Nothing
        Me.txtValorCambio.Text_4 = Nothing
        Me.txtValorCambio.UserValues = Nothing
        '
        'txtIdFacturacion
        '
        Me.txtIdFacturacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdFacturacion.Decimals = CType(2, Byte)
        Me.txtIdFacturacion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdFacturacion.Enabled = False
        Me.txtIdFacturacion.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdFacturacion.Location = New System.Drawing.Point(634, 128)
        Me.txtIdFacturacion.MaxLength = 8
        Me.txtIdFacturacion.Name = "txtIdFacturacion"
        Me.txtIdFacturacion.Size = New System.Drawing.Size(71, 20)
        Me.txtIdFacturacion.TabIndex = 195
        Me.txtIdFacturacion.Text_1 = Nothing
        Me.txtIdFacturacion.Text_2 = Nothing
        Me.txtIdFacturacion.Text_3 = Nothing
        Me.txtIdFacturacion.Text_4 = Nothing
        Me.txtIdFacturacion.UserValues = Nothing
        Me.txtIdFacturacion.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Blue
        Me.Label24.Location = New System.Drawing.Point(500, 192)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(27, 15)
        Me.Label24.TabIndex = 44
        Me.Label24.Text = "IVA"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label24.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Blue
        Me.Label23.Location = New System.Drawing.Point(420, 215)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(107, 15)
        Me.Label23.TabIndex = 43
        Me.Label23.Text = "Subtotal sin IVA"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label23.Visible = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Blue
        Me.Label22.Location = New System.Drawing.Point(379, 184)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 31)
        Me.Label22.TabIndex = 42
        Me.Label22.Text = "Monto Aplica"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label22.Visible = False
        '
        'txtMontoIva
        '
        Me.txtMontoIva.Decimals = CType(2, Byte)
        Me.txtMontoIva.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoIva.Enabled = False
        Me.txtMontoIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIva.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoIva.Location = New System.Drawing.Point(441, 191)
        Me.txtMontoIva.Name = "txtMontoIva"
        Me.txtMontoIva.Size = New System.Drawing.Size(53, 20)
        Me.txtMontoIva.TabIndex = 18
        Me.txtMontoIva.Text = "0"
        Me.txtMontoIva.Text_1 = Nothing
        Me.txtMontoIva.Text_2 = Nothing
        Me.txtMontoIva.Text_3 = Nothing
        Me.txtMontoIva.Text_4 = Nothing
        Me.txtMontoIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIva.UserValues = Nothing
        Me.txtMontoIva.Visible = False
        '
        'lblMontoSinIVA
        '
        Me.lblMontoSinIVA.BackColor = System.Drawing.Color.White
        Me.lblMontoSinIVA.Enabled = False
        Me.lblMontoSinIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoSinIVA.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoSinIVA.Location = New System.Drawing.Point(533, 214)
        Me.lblMontoSinIVA.Name = "lblMontoSinIVA"
        Me.lblMontoSinIVA.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoSinIVA.TabIndex = 28
        Me.lblMontoSinIVA.Text = "0"
        Me.lblMontoSinIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMontoSinIVA.Visible = False
        '
        'lblMontoIVA
        '
        Me.lblMontoIVA.BackColor = System.Drawing.Color.White
        Me.lblMontoIVA.Enabled = False
        Me.lblMontoIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoIVA.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoIVA.Location = New System.Drawing.Point(533, 191)
        Me.lblMontoIVA.Name = "lblMontoIVA"
        Me.lblMontoIVA.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoIVA.TabIndex = 24
        Me.lblMontoIVA.Text = "0"
        Me.lblMontoIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMontoIVA.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label10.Location = New System.Drawing.Point(431, 151)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 16)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Total sin IVA"
        Me.Label10.Visible = False
        '
        'lblTotalaPagarSinIva
        '
        Me.lblTotalaPagarSinIva.BackColor = System.Drawing.Color.White
        Me.lblTotalaPagarSinIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalaPagarSinIva.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTotalaPagarSinIva.Location = New System.Drawing.Point(533, 148)
        Me.lblTotalaPagarSinIva.Name = "lblTotalaPagarSinIva"
        Me.lblTotalaPagarSinIva.Size = New System.Drawing.Size(63, 19)
        Me.lblTotalaPagarSinIva.TabIndex = 25
        Me.lblTotalaPagarSinIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalaPagarSinIva.Visible = False
        '
        'chkAplicarIvaParcial
        '
        Me.chkAplicarIvaParcial.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chkAplicarIvaParcial.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkAplicarIvaParcial.Location = New System.Drawing.Point(484, 167)
        Me.chkAplicarIvaParcial.Name = "chkAplicarIvaParcial"
        Me.chkAplicarIvaParcial.Size = New System.Drawing.Size(112, 22)
        Me.chkAplicarIvaParcial.TabIndex = 17
        Me.chkAplicarIvaParcial.Text = "Aplicar IVA Parcial"
        Me.chkAplicarIvaParcial.Visible = False
        '
        'txtCliente
        '
        Me.txtCliente.AccessibleName = ""
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCliente.Decimals = CType(2, Byte)
        Me.txtCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCliente.Location = New System.Drawing.Point(381, 30)
        Me.txtCliente.MaxLength = 25
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(272, 20)
        Me.txtCliente.TabIndex = 3
        Me.txtCliente.Text_1 = Nothing
        Me.txtCliente.Text_2 = Nothing
        Me.txtCliente.Text_3 = Nothing
        Me.txtCliente.Text_4 = Nothing
        Me.txtCliente.UserValues = Nothing
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = ""
        Me.txtNota.BackColor = System.Drawing.SystemColors.Window
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(85, 70)
        Me.txtNota.MaxLength = 25
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(672, 20)
        Me.txtNota.TabIndex = 6
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(83, 54)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 13)
        Me.Label18.TabIndex = 190
        Me.Label18.Text = "Nota de Gestión"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabImpuestos)
        Me.TabControl1.Controls.Add(Me.TabCheques)
        Me.TabControl1.Controls.Add(Me.TabTransferencias)
        Me.TabControl1.Controls.Add(Me.TabTarjetas)
        Me.TabControl1.Location = New System.Drawing.Point(977, 15)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(363, 340)
        Me.TabControl1.TabIndex = 0
        '
        'TabImpuestos
        '
        Me.TabImpuestos.Controls.Add(Me.grdImpuestos)
        Me.TabImpuestos.Location = New System.Drawing.Point(4, 22)
        Me.TabImpuestos.Name = "TabImpuestos"
        Me.TabImpuestos.Padding = New System.Windows.Forms.Padding(3)
        Me.TabImpuestos.Size = New System.Drawing.Size(355, 314)
        Me.TabImpuestos.TabIndex = 0
        Me.TabImpuestos.Text = "Impuestos"
        Me.TabImpuestos.UseVisualStyleBackColor = True
        '
        'grdImpuestos
        '
        Me.grdImpuestos.AllowUserToAddRows = False
        Me.grdImpuestos.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdImpuestos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdImpuestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdImpuestos.Location = New System.Drawing.Point(9, 8)
        Me.grdImpuestos.MultiSelect = False
        Me.grdImpuestos.Name = "grdImpuestos"
        Me.grdImpuestos.Size = New System.Drawing.Size(340, 300)
        Me.grdImpuestos.TabIndex = 151
        '
        'TabCheques
        '
        Me.TabCheques.Controls.Add(Me.btnModificarCheque)
        Me.TabCheques.Controls.Add(Me.txtObservacionesCheque)
        Me.TabCheques.Controls.Add(Me.txtPropietario)
        Me.TabCheques.Controls.Add(Me.btnNuevoCheque)
        Me.TabCheques.Controls.Add(Me.txtMontoCheque)
        Me.TabCheques.Controls.Add(Me.txtNroCheque)
        Me.TabCheques.Controls.Add(Me.LabelX7)
        Me.TabCheques.Controls.Add(Me.LabelX6)
        Me.TabCheques.Controls.Add(Me.cmbMoneda)
        Me.TabCheques.Controls.Add(Me.btnAgregarCheque)
        Me.TabCheques.Controls.Add(Me.btnEliminarCheque)
        Me.TabCheques.Controls.Add(Me.LabelX5)
        Me.TabCheques.Controls.Add(Me.LabelX4)
        Me.TabCheques.Controls.Add(Me.dtpFechaCheque)
        Me.TabCheques.Controls.Add(Me.LabelX3)
        Me.TabCheques.Controls.Add(Me.LabelX2)
        Me.TabCheques.Controls.Add(Me.cmbBanco)
        Me.TabCheques.Controls.Add(Me.LabelX1)
        Me.TabCheques.Controls.Add(Me.grdCheques)
        Me.TabCheques.Location = New System.Drawing.Point(4, 22)
        Me.TabCheques.Name = "TabCheques"
        Me.TabCheques.Padding = New System.Windows.Forms.Padding(3)
        Me.TabCheques.Size = New System.Drawing.Size(355, 314)
        Me.TabCheques.TabIndex = 1
        Me.TabCheques.Text = "Cheques"
        Me.TabCheques.UseVisualStyleBackColor = True
        '
        'btnModificarCheque
        '
        Me.btnModificarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModificarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnModificarCheque.Location = New System.Drawing.Point(233, 287)
        Me.btnModificarCheque.Name = "btnModificarCheque"
        Me.btnModificarCheque.Size = New System.Drawing.Size(55, 23)
        Me.btnModificarCheque.TabIndex = 9
        Me.btnModificarCheque.Text = "Modificar"
        '
        'txtObservacionesCheque
        '
        Me.txtObservacionesCheque.Decimals = CType(2, Byte)
        Me.txtObservacionesCheque.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtObservacionesCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesCheque.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtObservacionesCheque.Location = New System.Drawing.Point(91, 266)
        Me.txtObservacionesCheque.Name = "txtObservacionesCheque"
        Me.txtObservacionesCheque.Size = New System.Drawing.Size(258, 20)
        Me.txtObservacionesCheque.TabIndex = 6
        Me.txtObservacionesCheque.Text_1 = Nothing
        Me.txtObservacionesCheque.Text_2 = Nothing
        Me.txtObservacionesCheque.Text_3 = Nothing
        Me.txtObservacionesCheque.Text_4 = Nothing
        Me.txtObservacionesCheque.UserValues = Nothing
        '
        'txtPropietario
        '
        Me.txtPropietario.Decimals = CType(2, Byte)
        Me.txtPropietario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPropietario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPropietario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPropietario.Location = New System.Drawing.Point(104, 239)
        Me.txtPropietario.Name = "txtPropietario"
        Me.txtPropietario.Size = New System.Drawing.Size(168, 20)
        Me.txtPropietario.TabIndex = 3
        Me.txtPropietario.Text_1 = Nothing
        Me.txtPropietario.Text_2 = Nothing
        Me.txtPropietario.Text_3 = Nothing
        Me.txtPropietario.Text_4 = Nothing
        Me.txtPropietario.UserValues = Nothing
        '
        'btnNuevoCheque
        '
        Me.btnNuevoCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevoCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNuevoCheque.Location = New System.Drawing.Point(111, 287)
        Me.btnNuevoCheque.Name = "btnNuevoCheque"
        Me.btnNuevoCheque.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoCheque.TabIndex = 7
        Me.btnNuevoCheque.Text = "Nuevo"
        '
        'txtMontoCheque
        '
        Me.txtMontoCheque.Decimals = CType(2, Byte)
        Me.txtMontoCheque.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoCheque.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoCheque.Location = New System.Drawing.Point(278, 239)
        Me.txtMontoCheque.Name = "txtMontoCheque"
        Me.txtMontoCheque.Size = New System.Drawing.Size(71, 20)
        Me.txtMontoCheque.TabIndex = 5
        Me.txtMontoCheque.Text_1 = Nothing
        Me.txtMontoCheque.Text_2 = Nothing
        Me.txtMontoCheque.Text_3 = Nothing
        Me.txtMontoCheque.Text_4 = Nothing
        Me.txtMontoCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoCheque.UserValues = Nothing
        '
        'txtNroCheque
        '
        Me.txtNroCheque.Decimals = CType(2, Byte)
        Me.txtNroCheque.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroCheque.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtNroCheque.Location = New System.Drawing.Point(9, 196)
        Me.txtNroCheque.Name = "txtNroCheque"
        Me.txtNroCheque.Size = New System.Drawing.Size(140, 20)
        Me.txtNroCheque.TabIndex = 0
        Me.txtNroCheque.Text_1 = Nothing
        Me.txtNroCheque.Text_2 = Nothing
        Me.txtNroCheque.Text_3 = Nothing
        Me.txtNroCheque.Text_4 = Nothing
        Me.txtNroCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNroCheque.UserValues = Nothing
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(9, 269)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX7.Size = New System.Drawing.Size(76, 15)
        Me.LabelX7.TabIndex = 150
        Me.LabelX7.Text = "Observaciones"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(215, 94)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX6.Size = New System.Drawing.Size(42, 15)
        Me.LabelX6.TabIndex = 149
        Me.LabelX6.Text = "Moneda"
        Me.LabelX6.Visible = False
        '
        'cmbMoneda
        '
        Me.cmbMoneda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbMoneda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMoneda.DisplayMember = "Text"
        Me.cmbMoneda.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoneda.FormattingEnabled = True
        Me.cmbMoneda.ItemHeight = 14
        Me.cmbMoneda.Items.AddRange(New Object() {Me.ComboItem5, Me.ComboItem6})
        Me.cmbMoneda.Location = New System.Drawing.Point(215, 112)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(73, 20)
        Me.cmbMoneda.TabIndex = 4
        Me.cmbMoneda.Visible = False
        '
        'ComboItem5
        '
        Me.ComboItem5.Text = "Mensual"
        '
        'ComboItem6
        '
        Me.ComboItem6.Text = "Quincenal"
        '
        'btnAgregarCheque
        '
        Me.btnAgregarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarCheque.Location = New System.Drawing.Point(172, 287)
        Me.btnAgregarCheque.Name = "btnAgregarCheque"
        Me.btnAgregarCheque.Size = New System.Drawing.Size(55, 23)
        Me.btnAgregarCheque.TabIndex = 8
        Me.btnAgregarCheque.Text = "Guardar"
        '
        'btnEliminarCheque
        '
        Me.btnEliminarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarCheque.Location = New System.Drawing.Point(294, 287)
        Me.btnEliminarCheque.Name = "btnEliminarCheque"
        Me.btnEliminarCheque.Size = New System.Drawing.Size(55, 23)
        Me.btnEliminarCheque.TabIndex = 10
        Me.btnEliminarCheque.Text = "Eliminar"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(104, 222)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX5.Size = New System.Drawing.Size(97, 15)
        Me.LabelX5.TabIndex = 148
        Me.LabelX5.Text = "Propietario Cheque"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(278, 221)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX4.Size = New System.Drawing.Size(37, 15)
        Me.LabelX4.TabIndex = 147
        Me.LabelX4.Text = "Monto*"
        '
        'dtpFechaCheque
        '
        '
        '
        '
        Me.dtpFechaCheque.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtpFechaCheque.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaCheque.ButtonDropDown.Visible = True
        Me.dtpFechaCheque.IsPopupCalendarOpen = False
        Me.dtpFechaCheque.Location = New System.Drawing.Point(9, 239)
        '
        '
        '
        '
        '
        '
        Me.dtpFechaCheque.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtpFechaCheque.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaCheque.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtpFechaCheque.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaCheque.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtpFechaCheque.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtpFechaCheque.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtpFechaCheque.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpFechaCheque.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtpFechaCheque.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaCheque.MonthCalendar.TodayButtonVisible = True
        Me.dtpFechaCheque.Name = "dtpFechaCheque"
        Me.dtpFechaCheque.Size = New System.Drawing.Size(83, 20)
        Me.dtpFechaCheque.TabIndex = 2
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(9, 222)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX3.Size = New System.Drawing.Size(64, 15)
        Me.LabelX3.TabIndex = 146
        Me.LabelX3.Text = "Fecha Venc."
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(155, 178)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX2.Size = New System.Drawing.Size(38, 15)
        Me.LabelX2.TabIndex = 145
        Me.LabelX2.Text = "Banco*"
        '
        'cmbBanco
        '
        Me.cmbBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBanco.DisplayMember = "Text"
        Me.cmbBanco.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbBanco.FormattingEnabled = True
        Me.cmbBanco.ItemHeight = 14
        Me.cmbBanco.Items.AddRange(New Object() {Me.ComboItem3, Me.ComboItem4})
        Me.cmbBanco.Location = New System.Drawing.Point(155, 196)
        Me.cmbBanco.Name = "cmbBanco"
        Me.cmbBanco.Size = New System.Drawing.Size(165, 20)
        Me.cmbBanco.TabIndex = 1
        '
        'ComboItem3
        '
        Me.ComboItem3.Text = "Mensual"
        '
        'ComboItem4
        '
        Me.ComboItem4.Text = "Quincenal"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(9, 178)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX1.Size = New System.Drawing.Size(81, 15)
        Me.LabelX1.TabIndex = 144
        Me.LabelX1.Text = "Nro de Cheque*"
        '
        'grdCheques
        '
        Me.grdCheques.AllowUserToAddRows = False
        Me.grdCheques.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdCheques.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdCheques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCheques.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NroCheque, Me.Banco, Me.Monto, Me.FechaVenc, Me.Propietario, Me.IdTipoMoneda, Me.Observaciones, Me.IdCheque, Me.Utilizado})
        Me.grdCheques.Location = New System.Drawing.Point(9, 8)
        Me.grdCheques.MultiSelect = False
        Me.grdCheques.Name = "grdCheques"
        Me.grdCheques.ReadOnly = True
        Me.grdCheques.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdCheques.Size = New System.Drawing.Size(340, 164)
        Me.grdCheques.TabIndex = 134
        '
        'NroCheque
        '
        Me.NroCheque.HeaderText = "NroCheque"
        Me.NroCheque.Name = "NroCheque"
        Me.NroCheque.ReadOnly = True
        '
        'Banco
        '
        Me.Banco.HeaderText = "Banco"
        Me.Banco.Name = "Banco"
        Me.Banco.ReadOnly = True
        Me.Banco.Width = 110
        '
        'Monto
        '
        Me.Monto.HeaderText = "Monto"
        Me.Monto.Name = "Monto"
        Me.Monto.ReadOnly = True
        Me.Monto.Width = 75
        '
        'FechaVenc
        '
        Me.FechaVenc.HeaderText = "FechaVenc"
        Me.FechaVenc.Name = "FechaVenc"
        Me.FechaVenc.ReadOnly = True
        Me.FechaVenc.Visible = False
        '
        'Propietario
        '
        Me.Propietario.HeaderText = "Propietario"
        Me.Propietario.Name = "Propietario"
        Me.Propietario.ReadOnly = True
        Me.Propietario.Visible = False
        '
        'IdTipoMoneda
        '
        Me.IdTipoMoneda.HeaderText = "IdtipoMoneda"
        Me.IdTipoMoneda.Name = "IdTipoMoneda"
        Me.IdTipoMoneda.ReadOnly = True
        Me.IdTipoMoneda.Visible = False
        '
        'Observaciones
        '
        Me.Observaciones.HeaderText = "Observaciones"
        Me.Observaciones.Name = "Observaciones"
        Me.Observaciones.ReadOnly = True
        Me.Observaciones.Visible = False
        '
        'IdCheque
        '
        Me.IdCheque.HeaderText = "IdCheque"
        Me.IdCheque.Name = "IdCheque"
        Me.IdCheque.ReadOnly = True
        Me.IdCheque.Visible = False
        '
        'Utilizado
        '
        Me.Utilizado.HeaderText = "Utilizado"
        Me.Utilizado.Name = "Utilizado"
        Me.Utilizado.ReadOnly = True
        Me.Utilizado.Visible = False
        '
        'TabTransferencias
        '
        Me.TabTransferencias.Controls.Add(Me.btnModificarTransf)
        Me.TabTransferencias.Controls.Add(Me.txtObservacionesTransf)
        Me.TabTransferencias.Controls.Add(Me.LabelX22)
        Me.TabTransferencias.Controls.Add(Me.btnNuevoTransferencia)
        Me.TabTransferencias.Controls.Add(Me.cmbCuentaDestino)
        Me.TabTransferencias.Controls.Add(Me.cmbCuentaOrigen)
        Me.TabTransferencias.Controls.Add(Me.txtNroOpCliente)
        Me.TabTransferencias.Controls.Add(Me.LabelX21)
        Me.TabTransferencias.Controls.Add(Me.LabelX15)
        Me.TabTransferencias.Controls.Add(Me.cmbBancoOrigen)
        Me.TabTransferencias.Controls.Add(Me.LabelX11)
        Me.TabTransferencias.Controls.Add(Me.txtMontoTransf)
        Me.TabTransferencias.Controls.Add(Me.LabelX14)
        Me.TabTransferencias.Controls.Add(Me.cmbMonedaTransf)
        Me.TabTransferencias.Controls.Add(Me.btnAgregarTransf)
        Me.TabTransferencias.Controls.Add(Me.btnEliminarTransf)
        Me.TabTransferencias.Controls.Add(Me.LabelX16)
        Me.TabTransferencias.Controls.Add(Me.dtpFechaTransf)
        Me.TabTransferencias.Controls.Add(Me.LabelX17)
        Me.TabTransferencias.Controls.Add(Me.LabelX18)
        Me.TabTransferencias.Controls.Add(Me.cmbBancoDestino)
        Me.TabTransferencias.Controls.Add(Me.LabelX19)
        Me.TabTransferencias.Controls.Add(Me.grdTransferencias)
        Me.TabTransferencias.Location = New System.Drawing.Point(4, 22)
        Me.TabTransferencias.Name = "TabTransferencias"
        Me.TabTransferencias.Padding = New System.Windows.Forms.Padding(3)
        Me.TabTransferencias.Size = New System.Drawing.Size(355, 314)
        Me.TabTransferencias.TabIndex = 2
        Me.TabTransferencias.Text = "Transferencias"
        Me.TabTransferencias.UseVisualStyleBackColor = True
        '
        'btnModificarTransf
        '
        Me.btnModificarTransf.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModificarTransf.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnModificarTransf.Location = New System.Drawing.Point(233, 287)
        Me.btnModificarTransf.Name = "btnModificarTransf"
        Me.btnModificarTransf.Size = New System.Drawing.Size(55, 23)
        Me.btnModificarTransf.TabIndex = 11
        Me.btnModificarTransf.Text = "Modificar"
        '
        'txtObservacionesTransf
        '
        Me.txtObservacionesTransf.Decimals = CType(2, Byte)
        Me.txtObservacionesTransf.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtObservacionesTransf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesTransf.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtObservacionesTransf.Location = New System.Drawing.Point(91, 266)
        Me.txtObservacionesTransf.Name = "txtObservacionesTransf"
        Me.txtObservacionesTransf.Size = New System.Drawing.Size(258, 20)
        Me.txtObservacionesTransf.TabIndex = 8
        Me.txtObservacionesTransf.Text_1 = Nothing
        Me.txtObservacionesTransf.Text_2 = Nothing
        Me.txtObservacionesTransf.Text_3 = Nothing
        Me.txtObservacionesTransf.Text_4 = Nothing
        Me.txtObservacionesTransf.UserValues = Nothing
        '
        'LabelX22
        '
        Me.LabelX22.AutoSize = True
        Me.LabelX22.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX22.Location = New System.Drawing.Point(9, 269)
        Me.LabelX22.Name = "LabelX22"
        Me.LabelX22.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX22.Size = New System.Drawing.Size(76, 15)
        Me.LabelX22.TabIndex = 174
        Me.LabelX22.Text = "Observaciones"
        '
        'btnNuevoTransferencia
        '
        Me.btnNuevoTransferencia.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevoTransferencia.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNuevoTransferencia.Location = New System.Drawing.Point(111, 287)
        Me.btnNuevoTransferencia.Name = "btnNuevoTransferencia"
        Me.btnNuevoTransferencia.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoTransferencia.TabIndex = 9
        Me.btnNuevoTransferencia.Text = "Nuevo"
        '
        'cmbCuentaDestino
        '
        Me.cmbCuentaDestino.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCuentaDestino.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCuentaDestino.DisplayMember = "Text"
        Me.cmbCuentaDestino.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCuentaDestino.FormattingEnabled = True
        Me.cmbCuentaDestino.ItemHeight = 14
        Me.cmbCuentaDestino.Items.AddRange(New Object() {Me.ComboItem17, Me.ComboItem18})
        Me.cmbCuentaDestino.Location = New System.Drawing.Point(142, 154)
        Me.cmbCuentaDestino.Name = "cmbCuentaDestino"
        Me.cmbCuentaDestino.Size = New System.Drawing.Size(128, 20)
        Me.cmbCuentaDestino.TabIndex = 1
        '
        'ComboItem17
        '
        Me.ComboItem17.Text = "Mensual"
        '
        'ComboItem18
        '
        Me.ComboItem18.Text = "Quincenal"
        '
        'cmbCuentaOrigen
        '
        Me.cmbCuentaOrigen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCuentaOrigen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCuentaOrigen.DisplayMember = "Text"
        Me.cmbCuentaOrigen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCuentaOrigen.FormattingEnabled = True
        Me.cmbCuentaOrigen.ItemHeight = 14
        Me.cmbCuentaOrigen.Items.AddRange(New Object() {Me.ComboItem13, Me.ComboItem14})
        Me.cmbCuentaOrigen.Location = New System.Drawing.Point(8, 154)
        Me.cmbCuentaOrigen.Name = "cmbCuentaOrigen"
        Me.cmbCuentaOrigen.Size = New System.Drawing.Size(128, 20)
        Me.cmbCuentaOrigen.TabIndex = 0
        '
        'ComboItem13
        '
        Me.ComboItem13.Text = "Mensual"
        '
        'ComboItem14
        '
        Me.ComboItem14.Text = "Quincenal"
        '
        'txtNroOpCliente
        '
        Me.txtNroOpCliente.Decimals = CType(2, Byte)
        Me.txtNroOpCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroOpCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroOpCliente.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtNroOpCliente.Location = New System.Drawing.Point(9, 236)
        Me.txtNroOpCliente.Name = "txtNroOpCliente"
        Me.txtNroOpCliente.Size = New System.Drawing.Size(115, 20)
        Me.txtNroOpCliente.TabIndex = 5
        Me.txtNroOpCliente.Text_1 = Nothing
        Me.txtNroOpCliente.Text_2 = Nothing
        Me.txtNroOpCliente.Text_3 = Nothing
        Me.txtNroOpCliente.Text_4 = Nothing
        Me.txtNroOpCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNroOpCliente.UserValues = Nothing
        '
        'LabelX21
        '
        Me.LabelX21.AutoSize = True
        Me.LabelX21.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX21.Location = New System.Drawing.Point(9, 219)
        Me.LabelX21.Name = "LabelX21"
        Me.LabelX21.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX21.Size = New System.Drawing.Size(74, 15)
        Me.LabelX21.TabIndex = 172
        Me.LabelX21.Text = "Nro Operación"
        '
        'LabelX15
        '
        Me.LabelX15.AutoSize = True
        Me.LabelX15.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Location = New System.Drawing.Point(9, 176)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX15.Size = New System.Drawing.Size(70, 15)
        Me.LabelX15.TabIndex = 170
        Me.LabelX15.Text = "Banco Origen"
        '
        'cmbBancoOrigen
        '
        Me.cmbBancoOrigen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBancoOrigen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBancoOrigen.DisplayMember = "Text"
        Me.cmbBancoOrigen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbBancoOrigen.FormattingEnabled = True
        Me.cmbBancoOrigen.ItemHeight = 14
        Me.cmbBancoOrigen.Items.AddRange(New Object() {Me.ComboItem11, Me.ComboItem12})
        Me.cmbBancoOrigen.Location = New System.Drawing.Point(9, 193)
        Me.cmbBancoOrigen.Name = "cmbBancoOrigen"
        Me.cmbBancoOrigen.Size = New System.Drawing.Size(165, 20)
        Me.cmbBancoOrigen.TabIndex = 3
        '
        'ComboItem11
        '
        Me.ComboItem11.Text = "Mensual"
        '
        'ComboItem12
        '
        Me.ComboItem12.Text = "Quincenal"
        '
        'LabelX11
        '
        Me.LabelX11.AutoSize = True
        Me.LabelX11.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Location = New System.Drawing.Point(142, 137)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX11.Size = New System.Drawing.Size(78, 15)
        Me.LabelX11.TabIndex = 168
        Me.LabelX11.Text = "Cuenta Destino"
        '
        'txtMontoTransf
        '
        Me.txtMontoTransf.Decimals = CType(2, Byte)
        Me.txtMontoTransf.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoTransf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoTransf.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoTransf.Location = New System.Drawing.Point(219, 236)
        Me.txtMontoTransf.Name = "txtMontoTransf"
        Me.txtMontoTransf.Size = New System.Drawing.Size(62, 20)
        Me.txtMontoTransf.TabIndex = 7
        Me.txtMontoTransf.Text_1 = Nothing
        Me.txtMontoTransf.Text_2 = Nothing
        Me.txtMontoTransf.Text_3 = Nothing
        Me.txtMontoTransf.Text_4 = Nothing
        Me.txtMontoTransf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoTransf.UserValues = Nothing
        '
        'LabelX14
        '
        Me.LabelX14.AutoSize = True
        Me.LabelX14.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Location = New System.Drawing.Point(276, 137)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX14.Size = New System.Drawing.Size(42, 15)
        Me.LabelX14.TabIndex = 166
        Me.LabelX14.Text = "Moneda"
        Me.LabelX14.Visible = False
        '
        'cmbMonedaTransf
        '
        Me.cmbMonedaTransf.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbMonedaTransf.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMonedaTransf.DisplayMember = "Text"
        Me.cmbMonedaTransf.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMonedaTransf.FormattingEnabled = True
        Me.cmbMonedaTransf.ItemHeight = 14
        Me.cmbMonedaTransf.Items.AddRange(New Object() {Me.ComboItem7, Me.ComboItem8})
        Me.cmbMonedaTransf.Location = New System.Drawing.Point(276, 154)
        Me.cmbMonedaTransf.Name = "cmbMonedaTransf"
        Me.cmbMonedaTransf.Size = New System.Drawing.Size(73, 20)
        Me.cmbMonedaTransf.TabIndex = 2
        Me.cmbMonedaTransf.Visible = False
        '
        'ComboItem7
        '
        Me.ComboItem7.Text = "Mensual"
        '
        'ComboItem8
        '
        Me.ComboItem8.Text = "Quincenal"
        '
        'btnAgregarTransf
        '
        Me.btnAgregarTransf.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarTransf.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarTransf.Location = New System.Drawing.Point(172, 287)
        Me.btnAgregarTransf.Name = "btnAgregarTransf"
        Me.btnAgregarTransf.Size = New System.Drawing.Size(55, 23)
        Me.btnAgregarTransf.TabIndex = 10
        Me.btnAgregarTransf.Text = "Guardar"
        '
        'btnEliminarTransf
        '
        Me.btnEliminarTransf.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarTransf.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarTransf.Location = New System.Drawing.Point(294, 287)
        Me.btnEliminarTransf.Name = "btnEliminarTransf"
        Me.btnEliminarTransf.Size = New System.Drawing.Size(55, 23)
        Me.btnEliminarTransf.TabIndex = 12
        Me.btnEliminarTransf.Text = "Eliminar"
        '
        'LabelX16
        '
        Me.LabelX16.AutoSize = True
        Me.LabelX16.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Location = New System.Drawing.Point(219, 219)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX16.Size = New System.Drawing.Size(37, 15)
        Me.LabelX16.TabIndex = 164
        Me.LabelX16.Text = "Monto*"
        '
        'dtpFechaTransf
        '
        '
        '
        '
        Me.dtpFechaTransf.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtpFechaTransf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaTransf.ButtonDropDown.Visible = True
        Me.dtpFechaTransf.IsPopupCalendarOpen = False
        Me.dtpFechaTransf.Location = New System.Drawing.Point(130, 236)
        '
        '
        '
        '
        '
        '
        Me.dtpFechaTransf.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.dtpFechaTransf.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaTransf.MonthCalendar.DisplayMonth = New Date(2013, 5, 1, 0, 0, 0, 0)
        Me.dtpFechaTransf.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtpFechaTransf.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaTransf.Name = "dtpFechaTransf"
        Me.dtpFechaTransf.Size = New System.Drawing.Size(83, 20)
        Me.dtpFechaTransf.TabIndex = 6
        '
        'LabelX17
        '
        Me.LabelX17.AutoSize = True
        Me.LabelX17.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Location = New System.Drawing.Point(130, 219)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX17.Size = New System.Drawing.Size(70, 15)
        Me.LabelX17.TabIndex = 163
        Me.LabelX17.Text = "Fecha Transf."
        '
        'LabelX18
        '
        Me.LabelX18.AutoSize = True
        Me.LabelX18.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX18.Location = New System.Drawing.Point(184, 176)
        Me.LabelX18.Name = "LabelX18"
        Me.LabelX18.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX18.Size = New System.Drawing.Size(74, 15)
        Me.LabelX18.TabIndex = 162
        Me.LabelX18.Text = "Banco Destino"
        '
        'cmbBancoDestino
        '
        Me.cmbBancoDestino.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBancoDestino.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBancoDestino.DisplayMember = "Text"
        Me.cmbBancoDestino.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbBancoDestino.FormattingEnabled = True
        Me.cmbBancoDestino.ItemHeight = 14
        Me.cmbBancoDestino.Items.AddRange(New Object() {Me.ComboItem9, Me.ComboItem10})
        Me.cmbBancoDestino.Location = New System.Drawing.Point(184, 193)
        Me.cmbBancoDestino.Name = "cmbBancoDestino"
        Me.cmbBancoDestino.Size = New System.Drawing.Size(165, 20)
        Me.cmbBancoDestino.TabIndex = 4
        '
        'ComboItem9
        '
        Me.ComboItem9.Text = "Mensual"
        '
        'ComboItem10
        '
        Me.ComboItem10.Text = "Quincenal"
        '
        'LabelX19
        '
        Me.LabelX19.AutoSize = True
        Me.LabelX19.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Location = New System.Drawing.Point(9, 137)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX19.Size = New System.Drawing.Size(74, 15)
        Me.LabelX19.TabIndex = 161
        Me.LabelX19.Text = "Cuenta Origen"
        '
        'grdTransferencias
        '
        Me.grdTransferencias.AllowUserToAddRows = False
        Me.grdTransferencias.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTransferencias.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTransferencias.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.BancoDestino, Me.ObservacionesTransf, Me.ID})
        Me.grdTransferencias.Location = New System.Drawing.Point(9, 8)
        Me.grdTransferencias.MultiSelect = False
        Me.grdTransferencias.Name = "grdTransferencias"
        Me.grdTransferencias.ReadOnly = True
        Me.grdTransferencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdTransferencias.Size = New System.Drawing.Size(340, 125)
        Me.grdTransferencias.TabIndex = 151
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "NroOpCliente"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "CuentaOrigen"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 160
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Monto"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 70
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "CuentaDestino"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "FechaTransf"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "IdtipoMoneda"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "BancoOrigen"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 85
        '
        'BancoDestino
        '
        Me.BancoDestino.HeaderText = "BancoDestino"
        Me.BancoDestino.Name = "BancoDestino"
        Me.BancoDestino.ReadOnly = True
        Me.BancoDestino.Width = 90
        '
        'ObservacionesTransf
        '
        Me.ObservacionesTransf.HeaderText = "Observaciones"
        Me.ObservacionesTransf.Name = "ObservacionesTransf"
        Me.ObservacionesTransf.ReadOnly = True
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Visible = False
        '
        'TabTarjetas
        '
        Me.TabTarjetas.Controls.Add(Me.lblRecargoTarjeta)
        Me.TabTarjetas.Controls.Add(Me.lblCuotasTarjeta)
        Me.TabTarjetas.Controls.Add(Me.LabelX10)
        Me.TabTarjetas.Controls.Add(Me.lblMontoTotalTarjeta)
        Me.TabTarjetas.Controls.Add(Me.LabelX9)
        Me.TabTarjetas.Controls.Add(Me.btnModificarTarjeta)
        Me.TabTarjetas.Controls.Add(Me.btnNuevoTarjeta)
        Me.TabTarjetas.Controls.Add(Me.LabelX20)
        Me.TabTarjetas.Controls.Add(Me.txtMontoTarjeta)
        Me.TabTarjetas.Controls.Add(Me.btnAgregarTarjeta)
        Me.TabTarjetas.Controls.Add(Me.btnEliminarTarjeta)
        Me.TabTarjetas.Controls.Add(Me.LabelX23)
        Me.TabTarjetas.Controls.Add(Me.LabelX25)
        Me.TabTarjetas.Controls.Add(Me.cmbTarjetas)
        Me.TabTarjetas.Controls.Add(Me.grdTarjetas)
        Me.TabTarjetas.Location = New System.Drawing.Point(4, 22)
        Me.TabTarjetas.Name = "TabTarjetas"
        Me.TabTarjetas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabTarjetas.Size = New System.Drawing.Size(355, 314)
        Me.TabTarjetas.TabIndex = 3
        Me.TabTarjetas.Text = "Tarjetas"
        Me.TabTarjetas.UseVisualStyleBackColor = True
        '
        'lblRecargoTarjeta
        '
        Me.lblRecargoTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRecargoTarjeta.Location = New System.Drawing.Point(101, 256)
        Me.lblRecargoTarjeta.Name = "lblRecargoTarjeta"
        Me.lblRecargoTarjeta.Size = New System.Drawing.Size(44, 19)
        Me.lblRecargoTarjeta.TabIndex = 172
        Me.lblRecargoTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCuotasTarjeta
        '
        Me.lblCuotasTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCuotasTarjeta.Location = New System.Drawing.Point(261, 214)
        Me.lblCuotasTarjeta.Name = "lblCuotasTarjeta"
        Me.lblCuotasTarjeta.Size = New System.Drawing.Size(44, 19)
        Me.lblCuotasTarjeta.TabIndex = 1
        Me.lblCuotasTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        Me.LabelX10.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Location = New System.Drawing.Point(263, 196)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX10.Size = New System.Drawing.Size(42, 15)
        Me.LabelX10.TabIndex = 171
        Me.LabelX10.Text = "Cuotas*"
        '
        'lblMontoTotalTarjeta
        '
        Me.lblMontoTotalTarjeta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lblMontoTotalTarjeta.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.lblMontoTotalTarjeta.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.lblMontoTotalTarjeta.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.lblMontoTotalTarjeta.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.lblMontoTotalTarjeta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMontoTotalTarjeta.Location = New System.Drawing.Point(167, 256)
        Me.lblMontoTotalTarjeta.Name = "lblMontoTotalTarjeta"
        Me.lblMontoTotalTarjeta.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblMontoTotalTarjeta.Size = New System.Drawing.Size(88, 20)
        Me.lblMontoTotalTarjeta.TabIndex = 4
        Me.lblMontoTotalTarjeta.Text = "0"
        Me.lblMontoTotalTarjeta.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Location = New System.Drawing.Point(167, 239)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX9.Size = New System.Drawing.Size(61, 15)
        Me.LabelX9.TabIndex = 169
        Me.LabelX9.Text = "Monto Total"
        '
        'btnModificarTarjeta
        '
        Me.btnModificarTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModificarTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnModificarTarjeta.Location = New System.Drawing.Point(233, 287)
        Me.btnModificarTarjeta.Name = "btnModificarTarjeta"
        Me.btnModificarTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnModificarTarjeta.TabIndex = 7
        Me.btnModificarTarjeta.Text = "Modificar"
        '
        'btnNuevoTarjeta
        '
        Me.btnNuevoTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevoTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNuevoTarjeta.Location = New System.Drawing.Point(111, 287)
        Me.btnNuevoTarjeta.Name = "btnNuevoTarjeta"
        Me.btnNuevoTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoTarjeta.TabIndex = 5
        Me.btnNuevoTarjeta.Text = "Nuevo"
        '
        'LabelX20
        '
        Me.LabelX20.AutoSize = True
        Me.LabelX20.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Location = New System.Drawing.Point(101, 239)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX20.Size = New System.Drawing.Size(44, 15)
        Me.LabelX20.TabIndex = 166
        Me.LabelX20.Text = "Recargo"
        '
        'txtMontoTarjeta
        '
        Me.txtMontoTarjeta.Decimals = CType(2, Byte)
        Me.txtMontoTarjeta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoTarjeta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoTarjeta.Location = New System.Drawing.Point(10, 257)
        Me.txtMontoTarjeta.Name = "txtMontoTarjeta"
        Me.txtMontoTarjeta.Size = New System.Drawing.Size(83, 20)
        Me.txtMontoTarjeta.TabIndex = 2
        Me.txtMontoTarjeta.Text_1 = Nothing
        Me.txtMontoTarjeta.Text_2 = Nothing
        Me.txtMontoTarjeta.Text_3 = Nothing
        Me.txtMontoTarjeta.Text_4 = Nothing
        Me.txtMontoTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoTarjeta.UserValues = Nothing
        '
        'btnAgregarTarjeta
        '
        Me.btnAgregarTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarTarjeta.Location = New System.Drawing.Point(172, 287)
        Me.btnAgregarTarjeta.Name = "btnAgregarTarjeta"
        Me.btnAgregarTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnAgregarTarjeta.TabIndex = 6
        Me.btnAgregarTarjeta.Text = "Guardar"
        '
        'btnEliminarTarjeta
        '
        Me.btnEliminarTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarTarjeta.Location = New System.Drawing.Point(294, 287)
        Me.btnEliminarTarjeta.Name = "btnEliminarTarjeta"
        Me.btnEliminarTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnEliminarTarjeta.TabIndex = 8
        Me.btnEliminarTarjeta.Text = "Eliminar"
        '
        'LabelX23
        '
        Me.LabelX23.AutoSize = True
        Me.LabelX23.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX23.Location = New System.Drawing.Point(10, 238)
        Me.LabelX23.Name = "LabelX23"
        Me.LabelX23.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX23.Size = New System.Drawing.Size(37, 15)
        Me.LabelX23.TabIndex = 164
        Me.LabelX23.Text = "Monto*"
        '
        'LabelX25
        '
        Me.LabelX25.AutoSize = True
        Me.LabelX25.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX25.Location = New System.Drawing.Point(9, 196)
        Me.LabelX25.Name = "LabelX25"
        Me.LabelX25.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX25.Size = New System.Drawing.Size(41, 15)
        Me.LabelX25.TabIndex = 162
        Me.LabelX25.Text = "Tarjeta*"
        '
        'cmbTarjetas
        '
        Me.cmbTarjetas.DisplayMember = "Text"
        Me.cmbTarjetas.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbTarjetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTarjetas.FormattingEnabled = True
        Me.cmbTarjetas.ItemHeight = 14
        Me.cmbTarjetas.Location = New System.Drawing.Point(10, 213)
        Me.cmbTarjetas.Name = "cmbTarjetas"
        Me.cmbTarjetas.Size = New System.Drawing.Size(245, 20)
        Me.cmbTarjetas.TabIndex = 0
        '
        'grdTarjetas
        '
        Me.grdTarjetas.AllowUserToAddRows = False
        Me.grdTarjetas.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTarjetas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdTarjetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTarjetas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.Cuotas, Me.MontoSinRecargo, Me.DataGridViewTextBoxColumn13, Me.CodigoTarjeta})
        Me.grdTarjetas.Location = New System.Drawing.Point(9, 8)
        Me.grdTarjetas.MultiSelect = False
        Me.grdTarjetas.Name = "grdTarjetas"
        Me.grdTarjetas.ReadOnly = True
        Me.grdTarjetas.RowHeadersVisible = False
        Me.grdTarjetas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdTarjetas.Size = New System.Drawing.Size(340, 182)
        Me.grdTarjetas.TabIndex = 9
        '
        'DataGridViewTextBoxColumn11
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn11.HeaderText = "Tarjeta"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 115
        '
        'DataGridViewTextBoxColumn12
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn12.HeaderText = "Recargo"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 60
        '
        'Cuotas
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Cuotas.DefaultCellStyle = DataGridViewCellStyle7
        Me.Cuotas.HeaderText = "Cuotas"
        Me.Cuotas.Name = "Cuotas"
        Me.Cuotas.ReadOnly = True
        Me.Cuotas.Width = 50
        '
        'MontoSinRecargo
        '
        Me.MontoSinRecargo.HeaderText = "MontoSinRecargo"
        Me.MontoSinRecargo.Name = "MontoSinRecargo"
        Me.MontoSinRecargo.ReadOnly = True
        Me.MontoSinRecargo.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn13.HeaderText = "Monto"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 80
        '
        'CodigoTarjeta
        '
        Me.CodigoTarjeta.HeaderText = "CodigoTarjeta"
        Me.CodigoTarjeta.Name = "CodigoTarjeta"
        Me.CodigoTarjeta.ReadOnly = True
        Me.CodigoTarjeta.Visible = False
        '
        'gpPago
        '
        Me.gpPago.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpPago.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpPago.Controls.Add(Me.txtAyIDepUsado)
        Me.gpPago.Controls.Add(Me.ChkAyIDep)
        Me.gpPago.Controls.Add(Me.Label21)
        Me.gpPago.Controls.Add(Me.txtRedondeo)
        Me.gpPago.Controls.Add(Me.lblResto)
        Me.gpPago.Controls.Add(Me.Label19)
        Me.gpPago.Controls.Add(Me.Label17)
        Me.gpPago.Controls.Add(Me.lblEntregaTransferencias)
        Me.gpPago.Controls.Add(Me.Label16)
        Me.gpPago.Controls.Add(Me.lblEntregaTarjetas)
        Me.gpPago.Controls.Add(Me.Label15)
        Me.gpPago.Controls.Add(Me.lblEntregaImpuestos)
        Me.gpPago.Controls.Add(Me.Label13)
        Me.gpPago.Controls.Add(Me.Label11)
        Me.gpPago.Controls.Add(Me.Label8)
        Me.gpPago.Controls.Add(Me.lblTotalaPagar)
        Me.gpPago.Controls.Add(Me.txtEntregaContado)
        Me.gpPago.Controls.Add(Me.lblEntregaCheques)
        Me.gpPago.Controls.Add(Me.Label6)
        Me.gpPago.Controls.Add(Me.lblEntregado)
        Me.gpPago.Controls.Add(Me.ShapeContainer1)
        Me.gpPago.DisabledBackColor = System.Drawing.Color.Empty
        Me.gpPago.Location = New System.Drawing.Point(763, 15)
        Me.gpPago.Name = "gpPago"
        Me.gpPago.Size = New System.Drawing.Size(208, 340)
        '
        '
        '
        Me.gpPago.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpPago.Style.BackColorGradientAngle = 90
        Me.gpPago.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpPago.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpPago.Style.BorderBottomWidth = 1
        Me.gpPago.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpPago.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpPago.Style.BorderLeftWidth = 1
        Me.gpPago.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpPago.Style.BorderRightWidth = 1
        Me.gpPago.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpPago.Style.BorderTopWidth = 1
        Me.gpPago.Style.CornerDiameter = 4
        Me.gpPago.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpPago.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpPago.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.gpPago.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.gpPago.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.gpPago.TabIndex = 186
        Me.gpPago.Text = "Resumen Forma de Pago..."
        '
        'txtAyIDepUsado
        '
        Me.txtAyIDepUsado.Decimals = CType(2, Byte)
        Me.txtAyIDepUsado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtAyIDepUsado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAyIDepUsado.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtAyIDepUsado.Location = New System.Drawing.Point(86, 209)
        Me.txtAyIDepUsado.Name = "txtAyIDepUsado"
        Me.txtAyIDepUsado.Size = New System.Drawing.Size(109, 20)
        Me.txtAyIDepUsado.TabIndex = 44
        Me.txtAyIDepUsado.Text = "0"
        Me.txtAyIDepUsado.Text_1 = Nothing
        Me.txtAyIDepUsado.Text_2 = Nothing
        Me.txtAyIDepUsado.Text_3 = Nothing
        Me.txtAyIDepUsado.Text_4 = Nothing
        Me.txtAyIDepUsado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAyIDepUsado.UserValues = Nothing
        '
        'ChkAyIDep
        '
        Me.ChkAyIDep.AutoSize = True
        Me.ChkAyIDep.BackColor = System.Drawing.Color.Transparent
        Me.ChkAyIDep.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAyIDep.ForeColor = System.Drawing.Color.MidnightBlue
        Me.ChkAyIDep.Location = New System.Drawing.Point(8, 211)
        Me.ChkAyIDep.Margin = New System.Windows.Forms.Padding(2)
        Me.ChkAyIDep.Name = "ChkAyIDep"
        Me.ChkAyIDep.Size = New System.Drawing.Size(71, 17)
        Me.ChkAyIDep.TabIndex = 43
        Me.ChkAyIDep.Text = "A. Y I.D"
        Me.ChkAyIDep.UseVisualStyleBackColor = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label21.Location = New System.Drawing.Point(7, 243)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 15)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "Redondeo"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRedondeo
        '
        Me.txtRedondeo.Decimals = CType(2, Byte)
        Me.txtRedondeo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRedondeo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRedondeo.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtRedondeo.Location = New System.Drawing.Point(86, 240)
        Me.txtRedondeo.Name = "txtRedondeo"
        Me.txtRedondeo.Size = New System.Drawing.Size(109, 20)
        Me.txtRedondeo.TabIndex = 40
        Me.txtRedondeo.Text = "0"
        Me.txtRedondeo.Text_1 = Nothing
        Me.txtRedondeo.Text_2 = Nothing
        Me.txtRedondeo.Text_3 = Nothing
        Me.txtRedondeo.Text_4 = Nothing
        Me.txtRedondeo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRedondeo.UserValues = Nothing
        '
        'lblResto
        '
        Me.lblResto.AutoSize = True
        Me.lblResto.BackColor = System.Drawing.Color.Transparent
        Me.lblResto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResto.ForeColor = System.Drawing.Color.Red
        Me.lblResto.Location = New System.Drawing.Point(87, 304)
        Me.lblResto.Name = "lblResto"
        Me.lblResto.Size = New System.Drawing.Size(14, 13)
        Me.lblResto.TabIndex = 39
        Me.lblResto.Text = "0"
        Me.lblResto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Red
        Me.Label19.Location = New System.Drawing.Point(40, 304)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(40, 13)
        Me.Label19.TabIndex = 38
        Me.Label19.Text = "Resto"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label17.Location = New System.Drawing.Point(29, 114)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(51, 15)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "Transf."
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEntregaTransferencias
        '
        Me.lblEntregaTransferencias.BackColor = System.Drawing.Color.White
        Me.lblEntregaTransferencias.Enabled = False
        Me.lblEntregaTransferencias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaTransferencias.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaTransferencias.Location = New System.Drawing.Point(86, 113)
        Me.lblEntregaTransferencias.Name = "lblEntregaTransferencias"
        Me.lblEntregaTransferencias.Size = New System.Drawing.Size(109, 19)
        Me.lblEntregaTransferencias.TabIndex = 36
        Me.lblEntregaTransferencias.Text = "0"
        Me.lblEntregaTransferencias.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label16.Location = New System.Drawing.Point(21, 147)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 15)
        Me.Label16.TabIndex = 35
        Me.Label16.Text = "Tarjetas"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEntregaTarjetas
        '
        Me.lblEntregaTarjetas.BackColor = System.Drawing.Color.White
        Me.lblEntregaTarjetas.Enabled = False
        Me.lblEntregaTarjetas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaTarjetas.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaTarjetas.Location = New System.Drawing.Point(86, 146)
        Me.lblEntregaTarjetas.Name = "lblEntregaTarjetas"
        Me.lblEntregaTarjetas.Size = New System.Drawing.Size(109, 19)
        Me.lblEntregaTarjetas.TabIndex = 34
        Me.lblEntregaTarjetas.Text = "0"
        Me.lblEntregaTarjetas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label15.Location = New System.Drawing.Point(7, 179)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(73, 15)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "Impuestos"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEntregaImpuestos
        '
        Me.lblEntregaImpuestos.BackColor = System.Drawing.Color.White
        Me.lblEntregaImpuestos.Enabled = False
        Me.lblEntregaImpuestos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaImpuestos.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaImpuestos.Location = New System.Drawing.Point(86, 178)
        Me.lblEntregaImpuestos.Name = "lblEntregaImpuestos"
        Me.lblEntregaImpuestos.Size = New System.Drawing.Size(109, 19)
        Me.lblEntregaImpuestos.TabIndex = 32
        Me.lblEntregaImpuestos.Text = "0"
        Me.lblEntregaImpuestos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label13.Location = New System.Drawing.Point(20, 49)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 15)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Contado"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label11.Location = New System.Drawing.Point(17, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 15)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Cheques"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label8.Location = New System.Drawing.Point(36, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 16)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Total"
        '
        'lblTotalaPagar
        '
        Me.lblTotalaPagar.BackColor = System.Drawing.Color.White
        Me.lblTotalaPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalaPagar.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTotalaPagar.Location = New System.Drawing.Point(86, 10)
        Me.lblTotalaPagar.Name = "lblTotalaPagar"
        Me.lblTotalaPagar.Size = New System.Drawing.Size(109, 19)
        Me.lblTotalaPagar.TabIndex = 22
        Me.lblTotalaPagar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEntregaContado
        '
        Me.txtEntregaContado.Decimals = CType(2, Byte)
        Me.txtEntregaContado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEntregaContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntregaContado.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtEntregaContado.Location = New System.Drawing.Point(86, 48)
        Me.txtEntregaContado.Name = "txtEntregaContado"
        Me.txtEntregaContado.Size = New System.Drawing.Size(109, 20)
        Me.txtEntregaContado.TabIndex = 7
        Me.txtEntregaContado.Text = "0"
        Me.txtEntregaContado.Text_1 = Nothing
        Me.txtEntregaContado.Text_2 = Nothing
        Me.txtEntregaContado.Text_3 = Nothing
        Me.txtEntregaContado.Text_4 = Nothing
        Me.txtEntregaContado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEntregaContado.UserValues = Nothing
        '
        'lblEntregaCheques
        '
        Me.lblEntregaCheques.BackColor = System.Drawing.Color.White
        Me.lblEntregaCheques.Enabled = False
        Me.lblEntregaCheques.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaCheques.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaCheques.Location = New System.Drawing.Point(86, 80)
        Me.lblEntregaCheques.Name = "lblEntregaCheques"
        Me.lblEntregaCheques.Size = New System.Drawing.Size(109, 19)
        Me.lblEntregaCheques.TabIndex = 13
        Me.lblEntregaCheques.Text = "0"
        Me.lblEntregaCheques.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label6.Location = New System.Drawing.Point(0, 280)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 16)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Total Neto"
        '
        'lblEntregado
        '
        Me.lblEntregado.BackColor = System.Drawing.Color.White
        Me.lblEntregado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregado.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregado.Location = New System.Drawing.Point(87, 279)
        Me.lblEntregado.Name = "lblEntregado"
        Me.lblEntregado.Size = New System.Drawing.Size(109, 19)
        Me.lblEntregado.TabIndex = 14
        Me.lblEntregado.Text = "0"
        Me.lblEntregado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(202, 319)
        Me.ShapeContainer1.TabIndex = 42
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 6
        Me.LineShape2.X2 = 194
        Me.LineShape2.Y1 = 271
        Me.LineShape2.Y2 = 271
        '
        'LineShape1
        '
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 6
        Me.LineShape1.X2 = 194
        Me.LineShape1.Y1 = 38
        Me.LineShape1.Y2 = 38
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(163, 353)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(119, 15)
        Me.Label14.TabIndex = 185
        Me.Label14.Text = "Total por pagar"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(288, 350)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(80, 20)
        Me.lblTotal.TabIndex = 184
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(163, 278)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 13)
        Me.Label12.TabIndex = 183
        Me.Label12.Text = "IVA"
        Me.Label12.Visible = False
        '
        'lblIVA
        '
        Me.lblIVA.BackColor = System.Drawing.Color.White
        Me.lblIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIVA.Location = New System.Drawing.Point(193, 278)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(62, 13)
        Me.lblIVA.TabIndex = 182
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIVA.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 278)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 181
        Me.Label7.Text = "Subtotal"
        Me.Label7.Visible = False
        '
        'lblSubtotal
        '
        Me.lblSubtotal.BackColor = System.Drawing.Color.White
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtotal.Location = New System.Drawing.Point(77, 278)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(80, 13)
        Me.lblSubtotal.TabIndex = 180
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSubtotal.Visible = False
        '
        'txtOrdenPago
        '
        Me.txtOrdenPago.AccessibleName = ""
        Me.txtOrdenPago.BackColor = System.Drawing.SystemColors.Window
        Me.txtOrdenPago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOrdenPago.Decimals = CType(2, Byte)
        Me.txtOrdenPago.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOrdenPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrdenPago.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtOrdenPago.Location = New System.Drawing.Point(658, 31)
        Me.txtOrdenPago.MaxLength = 25
        Me.txtOrdenPago.Name = "txtOrdenPago"
        Me.txtOrdenPago.Size = New System.Drawing.Size(98, 20)
        Me.txtOrdenPago.TabIndex = 4
        Me.txtOrdenPago.Text_1 = Nothing
        Me.txtOrdenPago.Text_2 = Nothing
        Me.txtOrdenPago.Text_3 = Nothing
        Me.txtOrdenPago.Text_4 = Nothing
        Me.txtOrdenPago.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(658, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 13)
        Me.Label9.TabIndex = 141
        Me.Label9.Text = "Nro Orden de Pago"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Facturas Pendientes"
        '
        'grdFacturasConsumos
        '
        Me.grdFacturasConsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFacturasConsumos.Location = New System.Drawing.Point(15, 110)
        Me.grdFacturasConsumos.Name = "grdFacturasConsumos"
        Me.grdFacturasConsumos.Size = New System.Drawing.Size(742, 235)
        Me.grdFacturasConsumos.TabIndex = 7
        '
        'chkAnulados
        '
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.Location = New System.Drawing.Point(1218, 357)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(122, 17)
        Me.chkAnulados.TabIndex = 194
        Me.chkAnulados.Text = "Ver Pagos Anulados"
        Me.chkAnulados.UseVisualStyleBackColor = True
        '
        'ComboItem28
        '
        Me.ComboItem28.Text = "18"
        '
        'ComboItem27
        '
        Me.ComboItem27.Text = "12"
        '
        'ComboItem26
        '
        Me.ComboItem26.Text = "10"
        '
        'ComboItem25
        '
        Me.ComboItem25.Text = "9"
        '
        'ComboItem24
        '
        Me.ComboItem24.Text = "8"
        '
        'ComboItem23
        '
        Me.ComboItem23.Text = "7"
        '
        'ComboItem22
        '
        Me.ComboItem22.Text = "6"
        '
        'ComboItem21
        '
        Me.ComboItem21.Text = "5"
        '
        'ComboItem16
        '
        Me.ComboItem16.Text = "4"
        '
        'ComboItem15
        '
        Me.ComboItem15.Text = "3"
        '
        'ComboItem2
        '
        Me.ComboItem2.Text = "2"
        '
        'ComboItem1
        '
        Me.ComboItem1.Text = "1"
        '
        'frmPagodeClientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 540)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmPagodeClientes"
        Me.Text = "Registro de Pagos de Clientes"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabImpuestos.ResumeLayout(False)
        CType(Me.grdImpuestos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCheques.ResumeLayout(False)
        Me.TabCheques.PerformLayout()
        CType(Me.dtpFechaCheque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTransferencias.ResumeLayout(False)
        Me.TabTransferencias.PerformLayout()
        CType(Me.dtpFechaTransf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTransferencias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTarjetas.ResumeLayout(False)
        Me.TabTarjetas.PerformLayout()
        CType(Me.grdTarjetas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpPago.ResumeLayout(False)
        Me.gpPago.PerformLayout()
        CType(Me.grdFacturasConsumos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdFacturasConsumos As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOrdenPago As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents gpPago As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtEntregaContado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblEntregaCheques As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblEntregado As System.Windows.Forms.Label
    Friend WithEvents txtMontoIva As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblTotalaPagar As System.Windows.Forms.Label
    Friend WithEvents lblMontoSinIVA As System.Windows.Forms.Label
    Friend WithEvents lblMontoIVA As System.Windows.Forms.Label
    Friend WithEvents chkAplicarIvaParcial As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblTotalaPagarSinIva As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabImpuestos As System.Windows.Forms.TabPage
    Friend WithEvents TabCheques As System.Windows.Forms.TabPage
    Friend WithEvents TabTransferencias As System.Windows.Forms.TabPage
    Friend WithEvents TabTarjetas As System.Windows.Forms.TabPage
    Friend WithEvents txtMontoCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMoneda As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem5 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem6 As DevComponents.Editors.ComboItem
    Friend WithEvents btnAgregarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtpFechaCheque As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbBanco As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem3 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem4 As DevComponents.Editors.ComboItem
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents grdCheques As System.Windows.Forms.DataGridView
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblEntregaTarjetas As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblEntregaImpuestos As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblEntregaTransferencias As System.Windows.Forms.Label
    Friend WithEvents grdImpuestos As System.Windows.Forms.DataGridView
    Friend WithEvents txtMontoTransf As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMonedaTransf As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem7 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem8 As DevComponents.Editors.ComboItem
    Friend WithEvents btnAgregarTransf As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarTransf As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtpFechaTransf As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX18 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbBancoDestino As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem9 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem10 As DevComponents.Editors.ComboItem
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents grdTransferencias As System.Windows.Forms.DataGridView
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbBancoOrigen As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem11 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem12 As DevComponents.Editors.ComboItem
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnAgregarTarjeta As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarTarjeta As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX23 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX25 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbTarjetas As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents grdTarjetas As System.Windows.Forms.DataGridView
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtMontoTarjeta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroOpCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX21 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbCuentaDestino As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem17 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem18 As DevComponents.Editors.ComboItem
    Friend WithEvents cmbCuentaOrigen As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem13 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem14 As DevComponents.Editors.ComboItem
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNuevoTransferencia As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNuevoTarjeta As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX22 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPropietario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtObservacionesCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnModificarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnModificarTransf As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtObservacionesTransf As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblResto As System.Windows.Forms.Label
    Friend WithEvents btnModificarTarjeta As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRedondeo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BancoDestino As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ObservacionesTransf As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NroCheque As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Banco As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaVenc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Propietario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdTipoMoneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Observaciones As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdCheque As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Utilizado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents txtIdFacturacion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtValorCambio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbRepartidor As System.Windows.Forms.ComboBox
    Friend WithEvents ChkAyIDep As System.Windows.Forms.CheckBox
    Friend WithEvents txtAyIDepUsado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtDescuento As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkDescuento As System.Windows.Forms.CheckBox
    Friend WithEvents lblMontoTotalTarjeta As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents ComboItem28 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem27 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem26 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem25 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem24 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem23 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem22 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem21 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem16 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem15 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem2 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem1 As DevComponents.Editors.ComboItem
    Friend WithEvents lblCuotasTarjeta As System.Windows.Forms.Label
    Friend WithEvents lblRecargoTarjeta As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cuotas As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MontoSinRecargo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodigoTarjeta As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
