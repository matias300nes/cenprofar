<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPagodeGastos
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
        Me.cmbProveedores = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIdProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtOrdenPago = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtValorCambio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtMontoIva = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblMontoSinIVA = New System.Windows.Forms.Label()
        Me.lblMontoIVA = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotalaPagarSinIva = New System.Windows.Forms.Label()
        Me.chkAplicarIvaParcial = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.txtProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabChequesPropios = New System.Windows.Forms.TabPage()
        Me.grdChequesPropios = New System.Windows.Forms.DataGridView()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.cmbMoneda = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem5 = New DevComponents.Editors.ComboItem()
        Me.ComboItem6 = New DevComponents.Editors.ComboItem()
        Me.TabCheques = New System.Windows.Forms.TabPage()
        Me.grdChequesTerceros = New System.Windows.Forms.DataGridView()
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
        Me.btnModificarTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.btnNuevoTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.FormattedTextBoxVB2 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.FormattedTextBoxVB1 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnAgregarTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.btnEliminarTarjeta = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX23 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX25 = New DevComponents.DotNetBar.LabelX()
        Me.ComboBoxEx2 = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem15 = New DevComponents.Editors.ComboItem()
        Me.ComboItem16 = New DevComponents.Editors.ComboItem()
        Me.grdTarjetas = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabNC = New System.Windows.Forms.TabPage()
        Me.FormattedTextBoxVB3 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.LabelX24 = New DevComponents.DotNetBar.LabelX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX2 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX3 = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX4 = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.cmbNC = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.ComboItem19 = New DevComponents.Editors.ComboItem()
        Me.ComboItem20 = New DevComponents.Editors.ComboItem()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gpPago = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRedondeo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblResto = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblEntregaTransferencias = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblEntregaTarjetas = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblEntregaChequesPropios = New System.Windows.Forms.Label()
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdFacturasConsumos = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabChequesPropios.SuspendLayout()
        CType(Me.grdChequesPropios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCheques.SuspendLayout()
        CType(Me.grdChequesTerceros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTransferencias.SuspendLayout()
        CType(Me.dtpFechaTransf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTransferencias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTarjetas.SuspendLayout()
        CType(Me.grdTarjetas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabNC.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Label3.Location = New System.Drawing.Point(73, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha Pago"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(73, 31)
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
        Me.txtCODIGO.Size = New System.Drawing.Size(56, 20)
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
        Me.txtID.Location = New System.Drawing.Point(593, 18)
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
        'cmbProveedores
        '
        Me.cmbProveedores.AccessibleName = "*Proveedor"
        Me.cmbProveedores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbProveedores.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedores.DropDownHeight = 300
        Me.cmbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProveedores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedores.FormattingEnabled = True
        Me.cmbProveedores.IntegralHeight = False
        Me.cmbProveedores.Location = New System.Drawing.Point(181, 31)
        Me.cmbProveedores.Name = "cmbProveedores"
        Me.cmbProveedores.Size = New System.Drawing.Size(300, 21)
        Me.cmbProveedores.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(178, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 125
        Me.Label5.Text = "Proveedor*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtIdProveedor)
        Me.GroupBox1.Controls.Add(Me.txtOrdenPago)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtValorCambio)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtMontoIva)
        Me.GroupBox1.Controls.Add(Me.lblMontoSinIVA)
        Me.GroupBox1.Controls.Add(Me.lblMontoIVA)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblTotalaPagarSinIva)
        Me.GroupBox1.Controls.Add(Me.chkAplicarIvaParcial)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.txtProveedor)
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
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdFacturasConsumos)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbProveedores)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1346, 376)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtIdProveedor
        '
        Me.txtIdProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdProveedor.Decimals = CType(2, Byte)
        Me.txtIdProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProveedor.Enabled = False
        Me.txtIdProveedor.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdProveedor.Location = New System.Drawing.Point(458, 52)
        Me.txtIdProveedor.MaxLength = 8
        Me.txtIdProveedor.Name = "txtIdProveedor"
        Me.txtIdProveedor.Size = New System.Drawing.Size(71, 20)
        Me.txtIdProveedor.TabIndex = 331
        Me.txtIdProveedor.Text_1 = Nothing
        Me.txtIdProveedor.Text_2 = Nothing
        Me.txtIdProveedor.Text_3 = Nothing
        Me.txtIdProveedor.Text_4 = Nothing
        Me.txtIdProveedor.UserValues = Nothing
        Me.txtIdProveedor.Visible = False
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
        Me.txtOrdenPago.Location = New System.Drawing.Point(12, 70)
        Me.txtOrdenPago.MaxLength = 25
        Me.txtOrdenPago.Name = "txtOrdenPago"
        Me.txtOrdenPago.Size = New System.Drawing.Size(160, 20)
        Me.txtOrdenPago.TabIndex = 3
        Me.txtOrdenPago.Text_1 = Nothing
        Me.txtOrdenPago.Text_2 = Nothing
        Me.txtOrdenPago.Text_3 = Nothing
        Me.txtOrdenPago.Text_4 = Nothing
        Me.txtOrdenPago.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 13)
        Me.Label9.TabIndex = 141
        Me.Label9.Text = "Nro Orden de Pago"
        '
        'txtValorCambio
        '
        Me.txtValorCambio.AccessibleName = ""
        Me.txtValorCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtValorCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValorCambio.Decimals = CType(2, Byte)
        Me.txtValorCambio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtValorCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorCambio.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtValorCambio.Location = New System.Drawing.Point(487, 31)
        Me.txtValorCambio.MaxLength = 25
        Me.txtValorCambio.Name = "txtValorCambio"
        Me.txtValorCambio.Size = New System.Drawing.Size(69, 20)
        Me.txtValorCambio.TabIndex = 5
        Me.txtValorCambio.Text_1 = Nothing
        Me.txtValorCambio.Text_2 = Nothing
        Me.txtValorCambio.Text_3 = Nothing
        Me.txtValorCambio.Text_4 = Nothing
        Me.txtValorCambio.UserValues = Nothing
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(485, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 13)
        Me.Label20.TabIndex = 330
        Me.Label20.Text = "Valor Cambio*"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Blue
        Me.Label24.Location = New System.Drawing.Point(407, 195)
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
        Me.Label23.Location = New System.Drawing.Point(327, 218)
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
        Me.Label22.Location = New System.Drawing.Point(286, 187)
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
        Me.txtMontoIva.Location = New System.Drawing.Point(348, 194)
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
        Me.lblMontoSinIVA.Location = New System.Drawing.Point(440, 217)
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
        Me.lblMontoIVA.Location = New System.Drawing.Point(440, 194)
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
        Me.Label10.Location = New System.Drawing.Point(338, 153)
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
        Me.lblTotalaPagarSinIva.Location = New System.Drawing.Point(440, 150)
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
        Me.chkAplicarIvaParcial.Location = New System.Drawing.Point(391, 168)
        Me.chkAplicarIvaParcial.Name = "chkAplicarIvaParcial"
        Me.chkAplicarIvaParcial.Size = New System.Drawing.Size(112, 22)
        Me.chkAplicarIvaParcial.TabIndex = 17
        Me.chkAplicarIvaParcial.Text = "Aplicar IVA Parcial"
        Me.chkAplicarIvaParcial.Visible = False
        '
        'chkAnulados
        '
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.Location = New System.Drawing.Point(1151, 357)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(122, 17)
        Me.chkAnulados.TabIndex = 195
        Me.chkAnulados.Text = "Ver Pagos Anulados"
        Me.chkAnulados.UseVisualStyleBackColor = True
        '
        'txtProveedor
        '
        Me.txtProveedor.AccessibleName = ""
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Decimals = CType(2, Byte)
        Me.txtProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProveedor.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtProveedor.Location = New System.Drawing.Point(180, 31)
        Me.txtProveedor.MaxLength = 25
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.Size = New System.Drawing.Size(301, 20)
        Me.txtProveedor.TabIndex = 3
        Me.txtProveedor.Text_1 = Nothing
        Me.txtProveedor.Text_2 = Nothing
        Me.txtProveedor.Text_3 = Nothing
        Me.txtProveedor.Text_4 = Nothing
        Me.txtProveedor.UserValues = Nothing
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
        Me.txtNota.Location = New System.Drawing.Point(178, 70)
        Me.txtNota.MaxLength = 25
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(429, 20)
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
        Me.Label18.Location = New System.Drawing.Point(178, 54)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 13)
        Me.Label18.TabIndex = 190
        Me.Label18.Text = "Nota de Gesti?n"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabChequesPropios)
        Me.TabControl1.Controls.Add(Me.TabCheques)
        Me.TabControl1.Controls.Add(Me.TabTransferencias)
        Me.TabControl1.Controls.Add(Me.TabTarjetas)
        Me.TabControl1.Controls.Add(Me.TabNC)
        Me.TabControl1.Location = New System.Drawing.Point(844, 15)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(429, 340)
        Me.TabControl1.TabIndex = 0
        '
        'TabChequesPropios
        '
        Me.TabChequesPropios.Controls.Add(Me.grdChequesPropios)
        Me.TabChequesPropios.Controls.Add(Me.LabelX6)
        Me.TabChequesPropios.Controls.Add(Me.cmbMoneda)
        Me.TabChequesPropios.Location = New System.Drawing.Point(4, 22)
        Me.TabChequesPropios.Name = "TabChequesPropios"
        Me.TabChequesPropios.Padding = New System.Windows.Forms.Padding(3)
        Me.TabChequesPropios.Size = New System.Drawing.Size(421, 314)
        Me.TabChequesPropios.TabIndex = 0
        Me.TabChequesPropios.Text = "Cheques Propios"
        Me.TabChequesPropios.UseVisualStyleBackColor = True
        '
        'grdChequesPropios
        '
        Me.grdChequesPropios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChequesPropios.Location = New System.Drawing.Point(6, 6)
        Me.grdChequesPropios.Name = "grdChequesPropios"
        Me.grdChequesPropios.Size = New System.Drawing.Size(409, 302)
        Me.grdChequesPropios.TabIndex = 172
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(248, 94)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX6.Size = New System.Drawing.Size(42, 15)
        Me.LabelX6.TabIndex = 169
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
        Me.cmbMoneda.Location = New System.Drawing.Point(248, 112)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(73, 20)
        Me.cmbMoneda.TabIndex = 168
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
        'TabCheques
        '
        Me.TabCheques.Controls.Add(Me.grdChequesTerceros)
        Me.TabCheques.Location = New System.Drawing.Point(4, 22)
        Me.TabCheques.Name = "TabCheques"
        Me.TabCheques.Padding = New System.Windows.Forms.Padding(3)
        Me.TabCheques.Size = New System.Drawing.Size(421, 314)
        Me.TabCheques.TabIndex = 1
        Me.TabCheques.Text = "Cheques Terceros"
        Me.TabCheques.UseVisualStyleBackColor = True
        '
        'grdChequesTerceros
        '
        Me.grdChequesTerceros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChequesTerceros.Location = New System.Drawing.Point(6, 6)
        Me.grdChequesTerceros.Name = "grdChequesTerceros"
        Me.grdChequesTerceros.Size = New System.Drawing.Size(409, 302)
        Me.grdChequesTerceros.TabIndex = 12
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
        Me.TabTransferencias.Size = New System.Drawing.Size(421, 314)
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
        Me.txtObservacionesTransf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.LabelX21.Text = "Nro Operaci?n"
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTransferencias.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTransferencias.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.BancoDestino, Me.ObservacionesTransf, Me.ID})
        Me.grdTransferencias.Location = New System.Drawing.Point(9, 8)
        Me.grdTransferencias.Name = "grdTransferencias"
        Me.grdTransferencias.ReadOnly = True
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
        Me.TabTarjetas.Controls.Add(Me.btnModificarTarjeta)
        Me.TabTarjetas.Controls.Add(Me.btnNuevoTarjeta)
        Me.TabTarjetas.Controls.Add(Me.FormattedTextBoxVB2)
        Me.TabTarjetas.Controls.Add(Me.LabelX20)
        Me.TabTarjetas.Controls.Add(Me.FormattedTextBoxVB1)
        Me.TabTarjetas.Controls.Add(Me.btnAgregarTarjeta)
        Me.TabTarjetas.Controls.Add(Me.btnEliminarTarjeta)
        Me.TabTarjetas.Controls.Add(Me.LabelX23)
        Me.TabTarjetas.Controls.Add(Me.LabelX25)
        Me.TabTarjetas.Controls.Add(Me.ComboBoxEx2)
        Me.TabTarjetas.Controls.Add(Me.grdTarjetas)
        Me.TabTarjetas.Location = New System.Drawing.Point(4, 22)
        Me.TabTarjetas.Name = "TabTarjetas"
        Me.TabTarjetas.Padding = New System.Windows.Forms.Padding(3)
        Me.TabTarjetas.Size = New System.Drawing.Size(421, 314)
        Me.TabTarjetas.TabIndex = 3
        Me.TabTarjetas.Text = "Tarjetas"
        Me.TabTarjetas.UseVisualStyleBackColor = True
        '
        'btnModificarTarjeta
        '
        Me.btnModificarTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModificarTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnModificarTarjeta.Location = New System.Drawing.Point(239, 287)
        Me.btnModificarTarjeta.Name = "btnModificarTarjeta"
        Me.btnModificarTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnModificarTarjeta.TabIndex = 167
        Me.btnModificarTarjeta.Text = "Modificar"
        '
        'btnNuevoTarjeta
        '
        Me.btnNuevoTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevoTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNuevoTarjeta.Location = New System.Drawing.Point(117, 287)
        Me.btnNuevoTarjeta.Name = "btnNuevoTarjeta"
        Me.btnNuevoTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnNuevoTarjeta.TabIndex = 3
        Me.btnNuevoTarjeta.Text = "Nuevo"
        '
        'FormattedTextBoxVB2
        '
        Me.FormattedTextBoxVB2.Decimals = CType(2, Byte)
        Me.FormattedTextBoxVB2.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.FormattedTextBoxVB2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormattedTextBoxVB2.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.FormattedTextBoxVB2.Location = New System.Drawing.Point(189, 266)
        Me.FormattedTextBoxVB2.Name = "FormattedTextBoxVB2"
        Me.FormattedTextBoxVB2.Size = New System.Drawing.Size(60, 20)
        Me.FormattedTextBoxVB2.TabIndex = 1
        Me.FormattedTextBoxVB2.Text_1 = Nothing
        Me.FormattedTextBoxVB2.Text_2 = Nothing
        Me.FormattedTextBoxVB2.Text_3 = Nothing
        Me.FormattedTextBoxVB2.Text_4 = Nothing
        Me.FormattedTextBoxVB2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.FormattedTextBoxVB2.UserValues = Nothing
        '
        'LabelX20
        '
        Me.LabelX20.AutoSize = True
        Me.LabelX20.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Location = New System.Drawing.Point(189, 246)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX20.Size = New System.Drawing.Size(44, 15)
        Me.LabelX20.TabIndex = 166
        Me.LabelX20.Text = "Recargo"
        '
        'FormattedTextBoxVB1
        '
        Me.FormattedTextBoxVB1.Decimals = CType(2, Byte)
        Me.FormattedTextBoxVB1.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.FormattedTextBoxVB1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormattedTextBoxVB1.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.FormattedTextBoxVB1.Location = New System.Drawing.Point(255, 266)
        Me.FormattedTextBoxVB1.Name = "FormattedTextBoxVB1"
        Me.FormattedTextBoxVB1.Size = New System.Drawing.Size(83, 20)
        Me.FormattedTextBoxVB1.TabIndex = 2
        Me.FormattedTextBoxVB1.Text_1 = Nothing
        Me.FormattedTextBoxVB1.Text_2 = Nothing
        Me.FormattedTextBoxVB1.Text_3 = Nothing
        Me.FormattedTextBoxVB1.Text_4 = Nothing
        Me.FormattedTextBoxVB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.FormattedTextBoxVB1.UserValues = Nothing
        '
        'btnAgregarTarjeta
        '
        Me.btnAgregarTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarTarjeta.Location = New System.Drawing.Point(178, 287)
        Me.btnAgregarTarjeta.Name = "btnAgregarTarjeta"
        Me.btnAgregarTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnAgregarTarjeta.TabIndex = 159
        Me.btnAgregarTarjeta.Text = "Guardar"
        '
        'btnEliminarTarjeta
        '
        Me.btnEliminarTarjeta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarTarjeta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarTarjeta.Location = New System.Drawing.Point(300, 287)
        Me.btnEliminarTarjeta.Name = "btnEliminarTarjeta"
        Me.btnEliminarTarjeta.Size = New System.Drawing.Size(55, 23)
        Me.btnEliminarTarjeta.TabIndex = 160
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
        Me.LabelX23.Location = New System.Drawing.Point(255, 246)
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
        Me.LabelX25.Location = New System.Drawing.Point(10, 245)
        Me.LabelX25.Name = "LabelX25"
        Me.LabelX25.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX25.Size = New System.Drawing.Size(41, 15)
        Me.LabelX25.TabIndex = 162
        Me.LabelX25.Text = "Tarjeta*"
        '
        'ComboBoxEx2
        '
        Me.ComboBoxEx2.DisplayMember = "Text"
        Me.ComboBoxEx2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBoxEx2.FormattingEnabled = True
        Me.ComboBoxEx2.ItemHeight = 14
        Me.ComboBoxEx2.Items.AddRange(New Object() {Me.ComboItem15, Me.ComboItem16})
        Me.ComboBoxEx2.Location = New System.Drawing.Point(10, 266)
        Me.ComboBoxEx2.Name = "ComboBoxEx2"
        Me.ComboBoxEx2.Size = New System.Drawing.Size(173, 20)
        Me.ComboBoxEx2.TabIndex = 0
        '
        'ComboItem15
        '
        Me.ComboItem15.Text = "Mensual"
        '
        'ComboItem16
        '
        Me.ComboItem16.Text = "Quincenal"
        '
        'grdTarjetas
        '
        Me.grdTarjetas.AllowUserToAddRows = False
        Me.grdTarjetas.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTarjetas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdTarjetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTarjetas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13})
        Me.grdTarjetas.Location = New System.Drawing.Point(9, 8)
        Me.grdTarjetas.Name = "grdTarjetas"
        Me.grdTarjetas.ReadOnly = True
        Me.grdTarjetas.Size = New System.Drawing.Size(340, 231)
        Me.grdTarjetas.TabIndex = 151
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Tarjeta"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 120
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Recargo"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 80
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "Monto"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.Width = 80
        '
        'TabNC
        '
        Me.TabNC.Controls.Add(Me.FormattedTextBoxVB3)
        Me.TabNC.Controls.Add(Me.LabelX24)
        Me.TabNC.Controls.Add(Me.ButtonX1)
        Me.TabNC.Controls.Add(Me.ButtonX2)
        Me.TabNC.Controls.Add(Me.ButtonX3)
        Me.TabNC.Controls.Add(Me.ButtonX4)
        Me.TabNC.Controls.Add(Me.LabelX8)
        Me.TabNC.Controls.Add(Me.cmbNC)
        Me.TabNC.Controls.Add(Me.DataGridView1)
        Me.TabNC.Location = New System.Drawing.Point(4, 22)
        Me.TabNC.Name = "TabNC"
        Me.TabNC.Padding = New System.Windows.Forms.Padding(3)
        Me.TabNC.Size = New System.Drawing.Size(421, 314)
        Me.TabNC.TabIndex = 4
        Me.TabNC.Text = "Notas Credito"
        Me.TabNC.UseVisualStyleBackColor = True
        '
        'FormattedTextBoxVB3
        '
        Me.FormattedTextBoxVB3.Decimals = CType(2, Byte)
        Me.FormattedTextBoxVB3.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.FormattedTextBoxVB3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormattedTextBoxVB3.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.FormattedTextBoxVB3.Location = New System.Drawing.Point(157, 236)
        Me.FormattedTextBoxVB3.Name = "FormattedTextBoxVB3"
        Me.FormattedTextBoxVB3.Size = New System.Drawing.Size(70, 20)
        Me.FormattedTextBoxVB3.TabIndex = 1
        Me.FormattedTextBoxVB3.Text_1 = Nothing
        Me.FormattedTextBoxVB3.Text_2 = Nothing
        Me.FormattedTextBoxVB3.Text_3 = Nothing
        Me.FormattedTextBoxVB3.Text_4 = Nothing
        Me.FormattedTextBoxVB3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.FormattedTextBoxVB3.UserValues = Nothing
        '
        'LabelX24
        '
        Me.LabelX24.AutoSize = True
        Me.LabelX24.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX24.Location = New System.Drawing.Point(157, 219)
        Me.LabelX24.Name = "LabelX24"
        Me.LabelX24.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX24.Size = New System.Drawing.Size(37, 15)
        Me.LabelX24.TabIndex = 171
        Me.LabelX24.Text = "Monto*"
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX1.Location = New System.Drawing.Point(231, 287)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(55, 23)
        Me.ButtonX1.TabIndex = 4
        Me.ButtonX1.Text = "Modificar"
        '
        'ButtonX2
        '
        Me.ButtonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX2.Location = New System.Drawing.Point(109, 287)
        Me.ButtonX2.Name = "ButtonX2"
        Me.ButtonX2.Size = New System.Drawing.Size(55, 23)
        Me.ButtonX2.TabIndex = 2
        Me.ButtonX2.Text = "Nuevo"
        '
        'ButtonX3
        '
        Me.ButtonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX3.Location = New System.Drawing.Point(170, 287)
        Me.ButtonX3.Name = "ButtonX3"
        Me.ButtonX3.Size = New System.Drawing.Size(55, 23)
        Me.ButtonX3.TabIndex = 3
        Me.ButtonX3.Text = "Guardar"
        '
        'ButtonX4
        '
        Me.ButtonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX4.Location = New System.Drawing.Point(292, 287)
        Me.ButtonX4.Name = "ButtonX4"
        Me.ButtonX4.Size = New System.Drawing.Size(55, 23)
        Me.ButtonX4.TabIndex = 5
        Me.ButtonX4.Text = "Eliminar"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        Me.LabelX8.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Location = New System.Drawing.Point(8, 219)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX8.Size = New System.Drawing.Size(64, 15)
        Me.LabelX8.TabIndex = 169
        Me.LabelX8.Text = "Nota Cr?dito"
        '
        'cmbNC
        '
        Me.cmbNC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbNC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNC.DisplayMember = "Text"
        Me.cmbNC.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbNC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNC.FormattingEnabled = True
        Me.cmbNC.ItemHeight = 14
        Me.cmbNC.Items.AddRange(New Object() {Me.ComboItem19, Me.ComboItem20})
        Me.cmbNC.Location = New System.Drawing.Point(8, 236)
        Me.cmbNC.Name = "cmbNC"
        Me.cmbNC.Size = New System.Drawing.Size(143, 20)
        Me.cmbNC.TabIndex = 0
        '
        'ComboItem19
        '
        Me.ComboItem19.Text = "Mensual"
        '
        'ComboItem20
        '
        Me.ComboItem20.Text = "Quincenal"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn16})
        Me.DataGridView1.Location = New System.Drawing.Point(7, 7)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(340, 202)
        Me.DataGridView1.TabIndex = 167
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "Nota Cr?dito"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 110
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "Monto"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 75
        '
        'gpPago
        '
        Me.gpPago.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpPago.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpPago.Controls.Add(Me.Label21)
        Me.gpPago.Controls.Add(Me.txtRedondeo)
        Me.gpPago.Controls.Add(Me.lblResto)
        Me.gpPago.Controls.Add(Me.Label19)
        Me.gpPago.Controls.Add(Me.Label17)
        Me.gpPago.Controls.Add(Me.lblEntregaTransferencias)
        Me.gpPago.Controls.Add(Me.Label16)
        Me.gpPago.Controls.Add(Me.lblEntregaTarjetas)
        Me.gpPago.Controls.Add(Me.Label15)
        Me.gpPago.Controls.Add(Me.lblEntregaChequesPropios)
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
        Me.gpPago.Location = New System.Drawing.Point(613, 15)
        Me.gpPago.Name = "gpPago"
        Me.gpPago.Size = New System.Drawing.Size(225, 340)
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
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label21.Location = New System.Drawing.Point(24, 210)
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
        Me.txtRedondeo.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtRedondeo.Location = New System.Drawing.Point(103, 209)
        Me.txtRedondeo.Name = "txtRedondeo"
        Me.txtRedondeo.Size = New System.Drawing.Size(110, 20)
        Me.txtRedondeo.TabIndex = 5
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
        Me.lblResto.Location = New System.Drawing.Point(103, 272)
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
        Me.Label19.Location = New System.Drawing.Point(57, 272)
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
        Me.Label17.Location = New System.Drawing.Point(46, 148)
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
        Me.lblEntregaTransferencias.Location = New System.Drawing.Point(103, 147)
        Me.lblEntregaTransferencias.Name = "lblEntregaTransferencias"
        Me.lblEntregaTransferencias.Size = New System.Drawing.Size(110, 19)
        Me.lblEntregaTransferencias.TabIndex = 3
        Me.lblEntregaTransferencias.Text = "0"
        Me.lblEntregaTransferencias.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label16.Location = New System.Drawing.Point(38, 180)
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
        Me.lblEntregaTarjetas.Location = New System.Drawing.Point(103, 179)
        Me.lblEntregaTarjetas.Name = "lblEntregaTarjetas"
        Me.lblEntregaTarjetas.Size = New System.Drawing.Size(110, 19)
        Me.lblEntregaTarjetas.TabIndex = 4
        Me.lblEntregaTarjetas.Text = "0"
        Me.lblEntregaTarjetas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label15.Location = New System.Drawing.Point(0, 84)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(97, 15)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "Cheq. Propios"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEntregaChequesPropios
        '
        Me.lblEntregaChequesPropios.BackColor = System.Drawing.Color.White
        Me.lblEntregaChequesPropios.Enabled = False
        Me.lblEntregaChequesPropios.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaChequesPropios.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaChequesPropios.Location = New System.Drawing.Point(103, 83)
        Me.lblEntregaChequesPropios.Name = "lblEntregaChequesPropios"
        Me.lblEntregaChequesPropios.Size = New System.Drawing.Size(110, 19)
        Me.lblEntregaChequesPropios.TabIndex = 1
        Me.lblEntregaChequesPropios.Text = "0"
        Me.lblEntregaChequesPropios.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label13.Location = New System.Drawing.Point(1, 49)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 15)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Contado Efec."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label11.Location = New System.Drawing.Point(34, 117)
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
        Me.Label8.Location = New System.Drawing.Point(53, 11)
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
        Me.lblTotalaPagar.Location = New System.Drawing.Point(103, 10)
        Me.lblTotalaPagar.Name = "lblTotalaPagar"
        Me.lblTotalaPagar.Size = New System.Drawing.Size(110, 19)
        Me.lblTotalaPagar.TabIndex = 22
        Me.lblTotalaPagar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEntregaContado
        '
        Me.txtEntregaContado.Decimals = CType(2, Byte)
        Me.txtEntregaContado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEntregaContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntregaContado.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtEntregaContado.Location = New System.Drawing.Point(103, 48)
        Me.txtEntregaContado.Name = "txtEntregaContado"
        Me.txtEntregaContado.Size = New System.Drawing.Size(110, 20)
        Me.txtEntregaContado.TabIndex = 0
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
        Me.lblEntregaCheques.Location = New System.Drawing.Point(103, 116)
        Me.lblEntregaCheques.Name = "lblEntregaCheques"
        Me.lblEntregaCheques.Size = New System.Drawing.Size(110, 19)
        Me.lblEntregaCheques.TabIndex = 2
        Me.lblEntregaCheques.Text = "0"
        Me.lblEntregaCheques.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label6.Location = New System.Drawing.Point(16, 248)
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
        Me.lblEntregado.Location = New System.Drawing.Point(103, 247)
        Me.lblEntregado.Name = "lblEntregado"
        Me.lblEntregado.Size = New System.Drawing.Size(110, 19)
        Me.lblEntregado.TabIndex = 6
        Me.lblEntregado.Text = "0"
        Me.lblEntregado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(219, 319)
        Me.ShapeContainer1.TabIndex = 42
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 9
        Me.LineShape2.X2 = 210
        Me.LineShape2.Y1 = 236
        Me.LineShape2.Y2 = 236
        '
        'LineShape1
        '
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 9
        Me.LineShape1.X2 = 210
        Me.LineShape1.Y1 = 38
        Me.LineShape1.Y2 = 38
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(341, 354)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 13)
        Me.Label14.TabIndex = 185
        Me.Label14.Text = "Total por Pagar"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.Location = New System.Drawing.Point(430, 354)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(62, 13)
        Me.lblTotal.TabIndex = 184
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(243, 354)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 13)
        Me.Label12.TabIndex = 183
        Me.Label12.Text = "IVA"
        '
        'lblIVA
        '
        Me.lblIVA.BackColor = System.Drawing.Color.White
        Me.lblIVA.Location = New System.Drawing.Point(273, 354)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(62, 13)
        Me.lblIVA.TabIndex = 182
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(123, 354)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 181
        Me.Label7.Text = "Subtotal"
        '
        'lblSubtotal
        '
        Me.lblSubtotal.BackColor = System.Drawing.Color.White
        Me.lblSubtotal.Location = New System.Drawing.Point(175, 354)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(62, 13)
        Me.lblSubtotal.TabIndex = 180
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 13)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Gastos Pendientes"
        '
        'grdFacturasConsumos
        '
        Me.grdFacturasConsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFacturasConsumos.Location = New System.Drawing.Point(15, 110)
        Me.grdFacturasConsumos.Name = "grdFacturasConsumos"
        Me.grdFacturasConsumos.Size = New System.Drawing.Size(592, 241)
        Me.grdFacturasConsumos.TabIndex = 7
        '
        'frmPagodeGastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 540)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmPagodeGastos"
        Me.Text = "Registro de Pagos de Clientes"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabChequesPropios.ResumeLayout(False)
        Me.TabChequesPropios.PerformLayout()
        CType(Me.grdChequesPropios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCheques.ResumeLayout(False)
        CType(Me.grdChequesTerceros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTransferencias.ResumeLayout(False)
        Me.TabTransferencias.PerformLayout()
        CType(Me.dtpFechaTransf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTransferencias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTarjetas.ResumeLayout(False)
        Me.TabTarjetas.PerformLayout()
        CType(Me.grdTarjetas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabNC.ResumeLayout(False)
        Me.TabNC.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cmbProveedores As System.Windows.Forms.ComboBox
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
    Friend WithEvents TabChequesPropios As System.Windows.Forms.TabPage
    Friend WithEvents TabCheques As System.Windows.Forms.TabPage
    Friend WithEvents TabTransferencias As System.Windows.Forms.TabPage
    Friend WithEvents TabTarjetas As System.Windows.Forms.TabPage
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblEntregaTarjetas As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblEntregaChequesPropios As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblEntregaTransferencias As System.Windows.Forms.Label
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
    Friend WithEvents ComboBoxEx2 As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem15 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem16 As DevComponents.Editors.ComboItem
    Friend WithEvents grdTarjetas As System.Windows.Forms.DataGridView
    Friend WithEvents FormattedTextBoxVB2 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents FormattedTextBoxVB1 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroOpCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX21 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbCuentaDestino As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem17 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem18 As DevComponents.Editors.ComboItem
    Friend WithEvents cmbCuentaOrigen As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem13 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem14 As DevComponents.Editors.ComboItem
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoTransferencia As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNuevoTarjeta As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX22 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnModificarTransf As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtObservacionesTransf As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblResto As System.Windows.Forms.Label
    Friend WithEvents btnModificarTarjeta As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRedondeo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TabNC As System.Windows.Forms.TabPage
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX2 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX3 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonX4 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbNC As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem19 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem20 As DevComponents.Editors.ComboItem
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FormattedTextBoxVB3 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents LabelX24 As DevComponents.DotNetBar.LabelX
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents grdChequesTerceros As System.Windows.Forms.DataGridView
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMoneda As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem5 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem6 As DevComponents.Editors.ComboItem
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents txtValorCambio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents grdChequesPropios As System.Windows.Forms.DataGridView
    Friend WithEvents txtIdProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
