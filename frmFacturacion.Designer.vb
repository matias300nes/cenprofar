<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFacturacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFacturacion))
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
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtOC = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtValorCambio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.imgConexion = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblModo = New System.Windows.Forms.Label()
        Me.PicAnularFacturas = New System.Windows.Forms.PictureBox()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.txtCodDocTipo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtemail = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNroRecuperar = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnRecuperar = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtDireccion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtDocTipo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtCondicionIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtCUITCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmbComprobantes = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dtpVtoPago = New System.Windows.Forms.DateTimePicker()
        Me.dtpServicioHasta = New System.Windows.Forms.DateTimePicker()
        Me.dtpServicioDesde = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbConceptos = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbTipoComprobante = New System.Windows.Forms.ComboBox()
        Me.txtptovta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNroFactura = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbCondVTA = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblPorcIva = New System.Windows.Forms.Label()
        Me.chkBuscarClientes = New System.Windows.Forms.CheckBox()
        Me.cmbCliente2 = New System.Windows.Forms.ComboBox()
        Me.txtIdFacturaAnulada = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblNotaCredito = New System.Windows.Forms.Label()
        Me.txtComprobanteFacturacion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblNroPosible = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.txtCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblIVA = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.grdRemitos = New System.Windows.Forms.DataGridView()
        Me.chkVerNotaGestion = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicAnularFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRemitos, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Label3.Location = New System.Drawing.Point(95, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha Factura"
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
        Me.txtCODIGO.Size = New System.Drawing.Size(76, 20)
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
        Me.Label1.Location = New System.Drawing.Point(1252, 20)
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
        Me.txtID.Location = New System.Drawing.Point(1274, 17)
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
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(777, 154)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(553, 266)
        Me.grdItems.TabIndex = 21
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*CLIENTE"
        Me.cmbCliente.DropDownHeight = 500
        Me.cmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(12, 70)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(322, 21)
        Me.cmbCliente.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 125
        Me.Label5.Text = "Cliente*"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtOC)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtValorCambio)
        Me.GroupBox1.Controls.Add(Me.imgConexion)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblModo)
        Me.GroupBox1.Controls.Add(Me.PicAnularFacturas)
        Me.GroupBox1.Controls.Add(Me.btnAnular)
        Me.GroupBox1.Controls.Add(Me.txtCodDocTipo)
        Me.GroupBox1.Controls.Add(Me.txtemail)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtNroRecuperar)
        Me.GroupBox1.Controls.Add(Me.btnRecuperar)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.txtDireccion)
        Me.GroupBox1.Controls.Add(Me.txtDocTipo)
        Me.GroupBox1.Controls.Add(Me.txtCondicionIVA)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtCUITCliente)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.cmbComprobantes)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.dtpVtoPago)
        Me.GroupBox1.Controls.Add(Me.dtpServicioHasta)
        Me.GroupBox1.Controls.Add(Me.dtpServicioDesde)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbConceptos)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.cmbTipoComprobante)
        Me.GroupBox1.Controls.Add(Me.txtptovta)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtNroFactura)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbCondVTA)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.lblPorcIva)
        Me.GroupBox1.Controls.Add(Me.chkBuscarClientes)
        Me.GroupBox1.Controls.Add(Me.cmbCliente2)
        Me.GroupBox1.Controls.Add(Me.txtIdFacturaAnulada)
        Me.GroupBox1.Controls.Add(Me.lblNotaCredito)
        Me.GroupBox1.Controls.Add(Me.txtComprobanteFacturacion)
        Me.GroupBox1.Controls.Add(Me.lblNroPosible)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.txtCliente)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblIVA)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.grdRemitos)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkVerNotaGestion)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1338, 445)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdCliente.Location = New System.Drawing.Point(507, 85)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(23, 20)
        Me.txtIdCliente.TabIndex = 971
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(382, 174)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 13)
        Me.Label17.TabIndex = 970
        Me.Label17.Text = "Orden de Compra"
        Me.Label17.Visible = False
        '
        'txtOC
        '
        Me.txtOC.AccessibleName = ""
        Me.txtOC.BackColor = System.Drawing.SystemColors.Window
        Me.txtOC.Decimals = CType(2, Byte)
        Me.txtOC.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOC.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtOC.Location = New System.Drawing.Point(385, 191)
        Me.txtOC.MaxLength = 300
        Me.txtOC.Name = "txtOC"
        Me.txtOC.Size = New System.Drawing.Size(263, 20)
        Me.txtOC.TabIndex = 18
        Me.txtOC.Text_1 = Nothing
        Me.txtOC.Text_2 = Nothing
        Me.txtOC.Text_3 = Nothing
        Me.txtOC.Text_4 = Nothing
        Me.txtOC.UserValues = Nothing
        Me.txtOC.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(7, 95)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 13)
        Me.Label16.TabIndex = 968
        Me.Label16.Text = "Valor Cambio"
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
        Me.txtValorCambio.Location = New System.Drawing.Point(12, 111)
        Me.txtValorCambio.MaxLength = 300
        Me.txtValorCambio.Name = "txtValorCambio"
        Me.txtValorCambio.Size = New System.Drawing.Size(66, 20)
        Me.txtValorCambio.TabIndex = 16
        Me.txtValorCambio.Text_1 = Nothing
        Me.txtValorCambio.Text_2 = Nothing
        Me.txtValorCambio.Text_3 = Nothing
        Me.txtValorCambio.Text_4 = Nothing
        Me.txtValorCambio.UserValues = Nothing
        '
        'imgConexion
        '
        Me.imgConexion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.imgConexion.Image = CType(resources.GetObject("imgConexion.Image"), System.Drawing.Image)
        Me.imgConexion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.imgConexion.Location = New System.Drawing.Point(1301, 421)
        Me.imgConexion.Name = "imgConexion"
        Me.imgConexion.Size = New System.Drawing.Size(29, 19)
        Me.imgConexion.TabIndex = 966
        Me.imgConexion.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1185, 422)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 16)
        Me.Label15.TabIndex = 965
        Me.Label15.Text = "Conexión AFIP: "
        Me.Label15.Visible = False
        '
        'lblModo
        '
        Me.lblModo.AutoSize = True
        Me.lblModo.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModo.ForeColor = System.Drawing.Color.Red
        Me.lblModo.Location = New System.Drawing.Point(973, 97)
        Me.lblModo.Name = "lblModo"
        Me.lblModo.Size = New System.Drawing.Size(359, 33)
        Me.lblModo.TabIndex = 956
        Me.lblModo.Text = "MODO HOMOLOGACION"
        '
        'PicAnularFacturas
        '
        Me.PicAnularFacturas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicAnularFacturas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicAnularFacturas.Location = New System.Drawing.Point(1086, 214)
        Me.PicAnularFacturas.Name = "PicAnularFacturas"
        Me.PicAnularFacturas.Size = New System.Drawing.Size(18, 20)
        Me.PicAnularFacturas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicAnularFacturas.TabIndex = 964
        Me.PicAnularFacturas.TabStop = False
        Me.PicAnularFacturas.Visible = False
        '
        'btnAnular
        '
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.Location = New System.Drawing.Point(985, 209)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(144, 23)
        Me.btnAnular.TabIndex = 963
        Me.btnAnular.Text = "Anular Factura Física"
        Me.btnAnular.UseVisualStyleBackColor = True
        Me.btnAnular.Visible = False
        '
        'txtCodDocTipo
        '
        Me.txtCodDocTipo.AccessibleName = ""
        Me.txtCodDocTipo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodDocTipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodDocTipo.Decimals = CType(2, Byte)
        Me.txtCodDocTipo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCodDocTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodDocTipo.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtCodDocTipo.Location = New System.Drawing.Point(650, 212)
        Me.txtCodDocTipo.MaxLength = 25
        Me.txtCodDocTipo.Name = "txtCodDocTipo"
        Me.txtCodDocTipo.ReadOnly = True
        Me.txtCodDocTipo.Size = New System.Drawing.Size(38, 20)
        Me.txtCodDocTipo.TabIndex = 962
        Me.txtCodDocTipo.Text_1 = Nothing
        Me.txtCodDocTipo.Text_2 = Nothing
        Me.txtCodDocTipo.Text_3 = Nothing
        Me.txtCodDocTipo.Text_4 = Nothing
        Me.txtCodDocTipo.UserValues = Nothing
        Me.txtCodDocTipo.Visible = False
        '
        'txtemail
        '
        Me.txtemail.AccessibleName = ""
        Me.txtemail.BackColor = System.Drawing.SystemColors.Window
        Me.txtemail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtemail.Decimals = CType(2, Byte)
        Me.txtemail.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtemail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtemail.Location = New System.Drawing.Point(828, 174)
        Me.txtemail.MaxLength = 25
        Me.txtemail.Name = "txtemail"
        Me.txtemail.ReadOnly = True
        Me.txtemail.Size = New System.Drawing.Size(95, 20)
        Me.txtemail.TabIndex = 961
        Me.txtemail.Text_1 = Nothing
        Me.txtemail.Text_2 = Nothing
        Me.txtemail.Text_3 = Nothing
        Me.txtemail.Text_4 = Nothing
        Me.txtemail.UserValues = Nothing
        Me.txtemail.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(774, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 15)
        Me.Label6.TabIndex = 960
        Me.Label6.Text = "Items Facturados"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 15)
        Me.Label4.TabIndex = 959
        Me.Label4.Text = "Remitos Disponibles"
        '
        'txtNroRecuperar
        '
        Me.txtNroRecuperar.AccessibleName = ""
        Me.txtNroRecuperar.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroRecuperar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroRecuperar.Decimals = CType(2, Byte)
        Me.txtNroRecuperar.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroRecuperar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroRecuperar.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroRecuperar.Location = New System.Drawing.Point(260, 223)
        Me.txtNroRecuperar.MaxLength = 25
        Me.txtNroRecuperar.Name = "txtNroRecuperar"
        Me.txtNroRecuperar.Size = New System.Drawing.Size(63, 20)
        Me.txtNroRecuperar.TabIndex = 249
        Me.txtNroRecuperar.Text_1 = Nothing
        Me.txtNroRecuperar.Text_2 = Nothing
        Me.txtNroRecuperar.Text_3 = Nothing
        Me.txtNroRecuperar.Text_4 = Nothing
        Me.txtNroRecuperar.UserValues = Nothing
        Me.txtNroRecuperar.Visible = False
        '
        'btnRecuperar
        '
        Me.btnRecuperar.Location = New System.Drawing.Point(144, 221)
        Me.btnRecuperar.Name = "btnRecuperar"
        Me.btnRecuperar.Size = New System.Drawing.Size(107, 23)
        Me.btnRecuperar.TabIndex = 248
        Me.btnRecuperar.Text = "Recuperar CAE"
        Me.btnRecuperar.UseVisualStyleBackColor = True
        Me.btnRecuperar.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(693, 55)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(52, 13)
        Me.Label28.TabIndex = 246
        Me.Label28.Text = "Dirección"
        '
        'txtDireccion
        '
        Me.txtDireccion.AccessibleName = ""
        Me.txtDireccion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Decimals = CType(2, Byte)
        Me.txtDireccion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDireccion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtDireccion.Location = New System.Drawing.Point(696, 70)
        Me.txtDireccion.MaxLength = 300
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(541, 20)
        Me.txtDireccion.TabIndex = 15
        Me.txtDireccion.Text_1 = Nothing
        Me.txtDireccion.Text_2 = Nothing
        Me.txtDireccion.Text_3 = Nothing
        Me.txtDireccion.Text_4 = Nothing
        Me.txtDireccion.UserValues = Nothing
        '
        'txtDocTipo
        '
        Me.txtDocTipo.AccessibleName = ""
        Me.txtDocTipo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocTipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDocTipo.Decimals = CType(2, Byte)
        Me.txtDocTipo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDocTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocTipo.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphabetic
        Me.txtDocTipo.Location = New System.Drawing.Point(340, 70)
        Me.txtDocTipo.MaxLength = 25
        Me.txtDocTipo.Name = "txtDocTipo"
        Me.txtDocTipo.ReadOnly = True
        Me.txtDocTipo.Size = New System.Drawing.Size(63, 20)
        Me.txtDocTipo.TabIndex = 12
        Me.txtDocTipo.Text_1 = Nothing
        Me.txtDocTipo.Text_2 = Nothing
        Me.txtDocTipo.Text_3 = Nothing
        Me.txtDocTipo.Text_4 = Nothing
        Me.txtDocTipo.UserValues = Nothing
        '
        'txtCondicionIVA
        '
        Me.txtCondicionIVA.AccessibleName = ""
        Me.txtCondicionIVA.Decimals = CType(2, Byte)
        Me.txtCondicionIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCondicionIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCondicionIVA.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCondicionIVA.Location = New System.Drawing.Point(507, 70)
        Me.txtCondicionIVA.Name = "txtCondicionIVA"
        Me.txtCondicionIVA.ReadOnly = True
        Me.txtCondicionIVA.Size = New System.Drawing.Size(183, 20)
        Me.txtCondicionIVA.TabIndex = 14
        Me.txtCondicionIVA.Text_1 = Nothing
        Me.txtCondicionIVA.Text_2 = Nothing
        Me.txtCondicionIVA.Text_3 = Nothing
        Me.txtCondicionIVA.Text_4 = Nothing
        Me.txtCondicionIVA.UserValues = Nothing
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(337, 55)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(129, 13)
        Me.Label26.TabIndex = 245
        Me.Label26.Text = "Tipo y Nro de Documento"
        '
        'txtCUITCliente
        '
        Me.txtCUITCliente.AccessibleName = ""
        Me.txtCUITCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCUITCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCUITCliente.Decimals = CType(2, Byte)
        Me.txtCUITCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCUITCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCUITCliente.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtCUITCliente.Location = New System.Drawing.Point(409, 70)
        Me.txtCUITCliente.MaxLength = 25
        Me.txtCUITCliente.Name = "txtCUITCliente"
        Me.txtCUITCliente.ReadOnly = True
        Me.txtCUITCliente.Size = New System.Drawing.Size(95, 20)
        Me.txtCUITCliente.TabIndex = 13
        Me.txtCUITCliente.Text_1 = Nothing
        Me.txtCUITCliente.Text_2 = Nothing
        Me.txtCUITCliente.Text_3 = Nothing
        Me.txtCUITCliente.Text_4 = Nothing
        Me.txtCUITCliente.UserValues = Nothing
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(504, 55)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(78, 13)
        Me.Label27.TabIndex = 244
        Me.Label27.Text = "Condición IVA*"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(442, 15)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(75, 13)
        Me.Label25.TabIndex = 239
        Me.Label25.Text = "Comprobantes"
        '
        'cmbComprobantes
        '
        Me.cmbComprobantes.AccessibleName = ""
        Me.cmbComprobantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComprobantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbComprobantes.FormattingEnabled = True
        Me.cmbComprobantes.Items.AddRange(New Object() {""})
        Me.cmbComprobantes.Location = New System.Drawing.Point(445, 31)
        Me.cmbComprobantes.Name = "cmbComprobantes"
        Me.cmbComprobantes.Size = New System.Drawing.Size(101, 21)
        Me.cmbComprobantes.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(1198, 15)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(51, 13)
        Me.Label24.TabIndex = 238
        Me.Label24.Text = "Vto Pago"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(1090, 15)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 13)
        Me.Label23.TabIndex = 237
        Me.Label23.Text = "Servicio Hasta"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(982, 15)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(79, 13)
        Me.Label22.TabIndex = 236
        Me.Label22.Text = "Servicio Desde"
        '
        'dtpVtoPago
        '
        Me.dtpVtoPago.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpVtoPago.Location = New System.Drawing.Point(1201, 31)
        Me.dtpVtoPago.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpVtoPago.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpVtoPago.Name = "dtpVtoPago"
        Me.dtpVtoPago.Size = New System.Drawing.Size(102, 20)
        Me.dtpVtoPago.TabIndex = 10
        Me.dtpVtoPago.Tag = "202"
        '
        'dtpServicioHasta
        '
        Me.dtpServicioHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpServicioHasta.Location = New System.Drawing.Point(1093, 31)
        Me.dtpServicioHasta.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpServicioHasta.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpServicioHasta.Name = "dtpServicioHasta"
        Me.dtpServicioHasta.Size = New System.Drawing.Size(102, 20)
        Me.dtpServicioHasta.TabIndex = 9
        Me.dtpServicioHasta.Tag = "202"
        '
        'dtpServicioDesde
        '
        Me.dtpServicioDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpServicioDesde.Location = New System.Drawing.Point(985, 31)
        Me.dtpServicioDesde.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpServicioDesde.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpServicioDesde.Name = "dtpServicioDesde"
        Me.dtpServicioDesde.Size = New System.Drawing.Size(102, 20)
        Me.dtpServicioDesde.TabIndex = 8
        Me.dtpServicioDesde.Tag = "202"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(794, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 235
        Me.Label8.Text = "Concepto*"
        '
        'cmbConceptos
        '
        Me.cmbConceptos.AccessibleName = "*Concepto"
        Me.cmbConceptos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbConceptos.FormattingEnabled = True
        Me.cmbConceptos.Location = New System.Drawing.Point(797, 31)
        Me.cmbConceptos.Name = "cmbConceptos"
        Me.cmbConceptos.Size = New System.Drawing.Size(182, 21)
        Me.cmbConceptos.TabIndex = 7
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(200, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(94, 13)
        Me.Label20.TabIndex = 234
        Me.Label20.Text = "Tipo Comprobante"
        '
        'cmbTipoComprobante
        '
        Me.cmbTipoComprobante.AccessibleName = "*CONDICIÓN_VENTA"
        Me.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoComprobante.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoComprobante.FormattingEnabled = True
        Me.cmbTipoComprobante.Location = New System.Drawing.Point(203, 31)
        Me.cmbTipoComprobante.Name = "cmbTipoComprobante"
        Me.cmbTipoComprobante.Size = New System.Drawing.Size(236, 21)
        Me.cmbTipoComprobante.TabIndex = 2
        '
        'txtptovta
        '
        Me.txtptovta.AccessibleName = "*FACTURA"
        Me.txtptovta.BackColor = System.Drawing.SystemColors.Window
        Me.txtptovta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtptovta.Decimals = CType(2, Byte)
        Me.txtptovta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtptovta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtptovta.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtptovta.Location = New System.Drawing.Point(552, 31)
        Me.txtptovta.MaxLength = 25
        Me.txtptovta.Name = "txtptovta"
        Me.txtptovta.ReadOnly = True
        Me.txtptovta.Size = New System.Drawing.Size(41, 20)
        Me.txtptovta.TabIndex = 4
        Me.txtptovta.Text_1 = Nothing
        Me.txtptovta.Text_2 = Nothing
        Me.txtptovta.Text_3 = Nothing
        Me.txtptovta.Text_4 = Nothing
        Me.txtptovta.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(551, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(42, 13)
        Me.Label18.TabIndex = 233
        Me.Label18.Text = "Pto Vta"
        '
        'txtNroFactura
        '
        Me.txtNroFactura.AccessibleName = "*FACTURA"
        Me.txtNroFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroFactura.Decimals = CType(2, Byte)
        Me.txtNroFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroFactura.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroFactura.Location = New System.Drawing.Point(599, 31)
        Me.txtNroFactura.MaxLength = 25
        Me.txtNroFactura.Name = "txtNroFactura"
        Me.txtNroFactura.Size = New System.Drawing.Size(81, 20)
        Me.txtNroFactura.TabIndex = 5
        Me.txtNroFactura.Text_1 = Nothing
        Me.txtNroFactura.Text_2 = Nothing
        Me.txtNroFactura.Text_3 = Nothing
        Me.txtNroFactura.Text_4 = Nothing
        Me.txtNroFactura.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(598, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 13)
        Me.Label9.TabIndex = 232
        Me.Label9.Text = "Nro Factura"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(683, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 231
        Me.Label10.Text = "Condición Venta*"
        '
        'cmbCondVTA
        '
        Me.cmbCondVTA.AccessibleName = "*CONDICIÓN_VENTA"
        Me.cmbCondVTA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondVTA.FormattingEnabled = True
        Me.cmbCondVTA.Items.AddRange(New Object() {"Cta Cte", "Contado/Efectivo"})
        Me.cmbCondVTA.Location = New System.Drawing.Point(686, 31)
        Me.cmbCondVTA.Name = "cmbCondVTA"
        Me.cmbCondVTA.Size = New System.Drawing.Size(105, 21)
        Me.cmbCondVTA.TabIndex = 6
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(257, 423)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(35, 13)
        Me.Label21.TabIndex = 212
        Me.Label21.Text = "% IVA"
        '
        'lblPorcIva
        '
        Me.lblPorcIva.BackColor = System.Drawing.Color.White
        Me.lblPorcIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPorcIva.Location = New System.Drawing.Point(298, 423)
        Me.lblPorcIva.Name = "lblPorcIva"
        Me.lblPorcIva.Size = New System.Drawing.Size(39, 13)
        Me.lblPorcIva.TabIndex = 211
        Me.lblPorcIva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkBuscarClientes
        '
        Me.chkBuscarClientes.AutoSize = True
        Me.chkBuscarClientes.Location = New System.Drawing.Point(252, 270)
        Me.chkBuscarClientes.Name = "chkBuscarClientes"
        Me.chkBuscarClientes.Size = New System.Drawing.Size(117, 17)
        Me.chkBuscarClientes.TabIndex = 198
        Me.chkBuscarClientes.Text = "Buscar por Clientes"
        Me.chkBuscarClientes.UseVisualStyleBackColor = True
        Me.chkBuscarClientes.Visible = False
        '
        'cmbCliente2
        '
        Me.cmbCliente2.AccessibleName = ""
        Me.cmbCliente2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente2.DropDownHeight = 500
        Me.cmbCliente2.Enabled = False
        Me.cmbCliente2.FormattingEnabled = True
        Me.cmbCliente2.IntegralHeight = False
        Me.cmbCliente2.Location = New System.Drawing.Point(375, 268)
        Me.cmbCliente2.Name = "cmbCliente2"
        Me.cmbCliente2.Size = New System.Drawing.Size(202, 21)
        Me.cmbCliente2.TabIndex = 197
        Me.cmbCliente2.Visible = False
        '
        'txtIdFacturaAnulada
        '
        Me.txtIdFacturaAnulada.AccessibleName = ""
        Me.txtIdFacturaAnulada.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdFacturaAnulada.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdFacturaAnulada.Decimals = CType(2, Byte)
        Me.txtIdFacturaAnulada.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdFacturaAnulada.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdFacturaAnulada.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtIdFacturaAnulada.Location = New System.Drawing.Point(1166, 10)
        Me.txtIdFacturaAnulada.MaxLength = 25
        Me.txtIdFacturaAnulada.Name = "txtIdFacturaAnulada"
        Me.txtIdFacturaAnulada.Size = New System.Drawing.Size(76, 20)
        Me.txtIdFacturaAnulada.TabIndex = 196
        Me.txtIdFacturaAnulada.Text_1 = Nothing
        Me.txtIdFacturaAnulada.Text_2 = Nothing
        Me.txtIdFacturaAnulada.Text_3 = Nothing
        Me.txtIdFacturaAnulada.Text_4 = Nothing
        Me.txtIdFacturaAnulada.UserValues = Nothing
        Me.txtIdFacturaAnulada.Visible = False
        '
        'lblNotaCredito
        '
        Me.lblNotaCredito.AutoSize = True
        Me.lblNotaCredito.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblNotaCredito.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotaCredito.ForeColor = System.Drawing.Color.Red
        Me.lblNotaCredito.Location = New System.Drawing.Point(467, 385)
        Me.lblNotaCredito.Name = "lblNotaCredito"
        Me.lblNotaCredito.Size = New System.Drawing.Size(345, 20)
        Me.lblNotaCredito.TabIndex = 195
        Me.lblNotaCredito.Text = "Documento Emitido como Nota de Crédito"
        Me.lblNotaCredito.Visible = False
        '
        'txtComprobanteFacturacion
        '
        Me.txtComprobanteFacturacion.AccessibleName = ""
        Me.txtComprobanteFacturacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtComprobanteFacturacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComprobanteFacturacion.Decimals = CType(2, Byte)
        Me.txtComprobanteFacturacion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtComprobanteFacturacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComprobanteFacturacion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtComprobanteFacturacion.Location = New System.Drawing.Point(85, 111)
        Me.txtComprobanteFacturacion.MaxLength = 25
        Me.txtComprobanteFacturacion.Name = "txtComprobanteFacturacion"
        Me.txtComprobanteFacturacion.Size = New System.Drawing.Size(148, 20)
        Me.txtComprobanteFacturacion.TabIndex = 17
        Me.txtComprobanteFacturacion.Text_1 = Nothing
        Me.txtComprobanteFacturacion.Text_2 = Nothing
        Me.txtComprobanteFacturacion.Text_3 = Nothing
        Me.txtComprobanteFacturacion.Text_4 = Nothing
        Me.txtComprobanteFacturacion.UserValues = Nothing
        '
        'lblNroPosible
        '
        Me.lblNroPosible.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNroPosible.Location = New System.Drawing.Point(883, 15)
        Me.lblNroPosible.Name = "lblNroPosible"
        Me.lblNroPosible.Size = New System.Drawing.Size(67, 13)
        Me.lblNroPosible.TabIndex = 194
        Me.lblNroPosible.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(237, 95)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 13)
        Me.Label13.TabIndex = 193
        Me.Label13.Text = "Nota de Gestión"
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = ""
        Me.txtNota.BackColor = System.Drawing.SystemColors.Window
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNota.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(239, 111)
        Me.txtNota.MaxLength = 300
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(958, 20)
        Me.txtNota.TabIndex = 19
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'chkAnulados
        '
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.ForeColor = System.Drawing.Color.Red
        Me.chkAnulados.Location = New System.Drawing.Point(1180, 464)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(133, 17)
        Me.chkAnulados.TabIndex = 13
        Me.chkAnulados.Text = "Ver Facturas Anuladas"
        Me.chkAnulados.UseVisualStyleBackColor = True
        '
        'txtCliente
        '
        Me.txtCliente.AccessibleName = ""
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCliente.Decimals = CType(2, Byte)
        Me.txtCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCliente.Location = New System.Drawing.Point(12, 70)
        Me.txtCliente.MaxLength = 25
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(322, 20)
        Me.txtCliente.TabIndex = 11
        Me.txtCliente.Text_1 = Nothing
        Me.txtCliente.Text_2 = Nothing
        Me.txtCliente.Text_3 = Nothing
        Me.txtCliente.Text_4 = Nothing
        Me.txtCliente.UserValues = Nothing
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(82, 95)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(149, 13)
        Me.Label11.TabIndex = 187
        Me.Label11.Text = "Nro Comprobante Facturación"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(466, 423)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 13)
        Me.Label14.TabIndex = 185
        Me.Label14.Text = "Total $"
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(512, 423)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(94, 13)
        Me.lblTotal.TabIndex = 184
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(343, 423)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 183
        Me.Label12.Text = "IVA $"
        '
        'lblIVA
        '
        Me.lblIVA.BackColor = System.Drawing.Color.White
        Me.lblIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIVA.Location = New System.Drawing.Point(382, 423)
        Me.lblIVA.Name = "lblIVA"
        Me.lblIVA.Size = New System.Drawing.Size(78, 13)
        Me.lblIVA.TabIndex = 182
        Me.lblIVA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(112, 423)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 181
        Me.Label7.Text = "Subtotal $"
        '
        'lblSubtotal
        '
        Me.lblSubtotal.BackColor = System.Drawing.Color.White
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtotal.Location = New System.Drawing.Point(173, 423)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(78, 13)
        Me.lblSubtotal.TabIndex = 180
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(747, 423)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 178
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(843, 423)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(36, 13)
        Me.lblCantidadFilas.TabIndex = 177
        Me.lblCantidadFilas.Text = "0 / 16"
        '
        'grdRemitos
        '
        Me.grdRemitos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdRemitos.Location = New System.Drawing.Point(12, 154)
        Me.grdRemitos.Name = "grdRemitos"
        Me.grdRemitos.Size = New System.Drawing.Size(759, 266)
        Me.grdRemitos.TabIndex = 20
        '
        'chkVerNotaGestion
        '
        Me.chkVerNotaGestion.AutoSize = True
        Me.chkVerNotaGestion.Location = New System.Drawing.Point(630, 137)
        Me.chkVerNotaGestion.Name = "chkVerNotaGestion"
        Me.chkVerNotaGestion.Size = New System.Drawing.Size(141, 17)
        Me.chkVerNotaGestion.TabIndex = 199
        Me.chkVerNotaGestion.Text = "Mostrar Nota de Gestión"
        Me.chkVerNotaGestion.UseVisualStyleBackColor = True
        '
        'frmFacturacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 540)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFacturacion"
        Me.Text = "Facturación"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicAnularFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRemitos, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdRemitos As System.Windows.Forms.DataGridView
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblIVA As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtComprobanteFacturacion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblNotaCredito As System.Windows.Forms.Label
    Friend WithEvents txtIdFacturaAnulada As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkBuscarClientes As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCliente2 As System.Windows.Forms.ComboBox
    Friend WithEvents chkVerNotaGestion As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblPorcIva As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbComprobantes As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtpVtoPago As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpServicioHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpServicioDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbConceptos As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoComprobante As System.Windows.Forms.ComboBox
    Friend WithEvents txtptovta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNroFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbCondVTA As System.Windows.Forms.ComboBox
    Friend WithEvents lblNroPosible As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtDireccion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtDocTipo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCondicionIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCUITCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtNroRecuperar As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnRecuperar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtemail As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCodDocTipo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PicAnularFacturas As System.Windows.Forms.PictureBox
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents lblModo As System.Windows.Forms.Label
    Friend WithEvents imgConexion As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtValorCambio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
