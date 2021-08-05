<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGastos

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGastos))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbUsuarioGasto = New System.Windows.Forms.ComboBox()
        Me.txtidpago = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lstFacturasPendientes = New System.Windows.Forms.ListView()
        Me.Codigo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NroCompr = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtSubtotalExento = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtNroFacturaCompleto = New System.Windows.Forms.Label()
        Me.txtNroRemitoCompleto = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtNroCompRemito = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPtoVtaRemito = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTipoMoneda = New System.Windows.Forms.Label()
        Me.FormattedTextBoxVB1 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtCantIVA = New System.Windows.Forms.NumericUpDown()
        Me.txtValorCambio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtMontoIVA27 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMontoIVA10 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMontoIVA21 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblImpuestos = New System.Windows.Forms.Label()
        Me.label40 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtIdComprobante = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdTipoGasto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdMoneda = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbTipoComprobante = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.grdImpuestos = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNroFactura = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPtoVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkAnulados = New System.Windows.Forms.CheckBox()
        Me.chkPeriodo = New System.Windows.Forms.CheckBox()
        Me.dtpPeriodo = New System.Windows.Forms.DateTimePicker()
        Me.txtIdProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkCancelado = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbTipoGasto = New System.Windows.Forms.ComboBox()
        Me.lblNroRecepcion = New System.Windows.Forms.Label()
        Me.lblNroRemito1 = New System.Windows.Forms.Label()
        Me.lblNroRemito = New System.Windows.Forms.Label()
        Me.chkRecepcion = New System.Windows.Forms.CheckBox()
        Me.cmbRecepcion = New System.Windows.Forms.ComboBox()
        Me.chkFacturaCancelada = New System.Windows.Forms.CheckBox()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picProveedores = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.txtCodigo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtNroRemitoControl = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdRecepcion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNroFacturaCompletoControl = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblMontoIva = New System.Windows.Forms.Label()
        Me.label30 = New System.Windows.Forms.Label()
        Me.ContextMenuStripIVA = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItemIVA = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtCantIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdImpuestos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picProveedores, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripIVA.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.cmbUsuarioGasto)
        Me.GroupBox1.Controls.Add(Me.txtidpago)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.lstFacturasPendientes)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalExento)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtNroFacturaCompleto)
        Me.GroupBox1.Controls.Add(Me.txtNroRemitoCompleto)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.txtNroCompRemito)
        Me.GroupBox1.Controls.Add(Me.txtPtoVtaRemito)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTipoMoneda)
        Me.GroupBox1.Controls.Add(Me.FormattedTextBoxVB1)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.txtCantIVA)
        Me.GroupBox1.Controls.Add(Me.txtValorCambio)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtMontoIVA27)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtMontoIVA10)
        Me.GroupBox1.Controls.Add(Me.txtMontoIVA21)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblImpuestos)
        Me.GroupBox1.Controls.Add(Me.label40)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtIdComprobante)
        Me.GroupBox1.Controls.Add(Me.txtIdTipoGasto)
        Me.GroupBox1.Controls.Add(Me.txtIdMoneda)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbTipoComprobante)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.grdImpuestos)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtNroFactura)
        Me.GroupBox1.Controls.Add(Me.txtPtoVta)
        Me.GroupBox1.Controls.Add(Me.chkAnulados)
        Me.GroupBox1.Controls.Add(Me.chkPeriodo)
        Me.GroupBox1.Controls.Add(Me.dtpPeriodo)
        Me.GroupBox1.Controls.Add(Me.txtIdProveedor)
        Me.GroupBox1.Controls.Add(Me.chkCancelado)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbTipoGasto)
        Me.GroupBox1.Controls.Add(Me.lblNroRecepcion)
        Me.GroupBox1.Controls.Add(Me.lblNroRemito1)
        Me.GroupBox1.Controls.Add(Me.lblNroRemito)
        Me.GroupBox1.Controls.Add(Me.chkRecepcion)
        Me.GroupBox1.Controls.Add(Me.cmbRecepcion)
        Me.GroupBox1.Controls.Add(Me.chkFacturaCancelada)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.picProveedores)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbProveedor)
        Me.GroupBox1.Controls.Add(Me.txtCodigo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(7, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1338, 210)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(418, 16)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(74, 13)
        Me.Label27.TabIndex = 343
        Me.Label27.Text = "Usuario Gasto"
        '
        'cmbUsuarioGasto
        '
        Me.cmbUsuarioGasto.AccessibleName = ""
        Me.cmbUsuarioGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbUsuarioGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbUsuarioGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsuarioGasto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsuarioGasto.FormattingEnabled = True
        Me.cmbUsuarioGasto.Location = New System.Drawing.Point(421, 32)
        Me.cmbUsuarioGasto.Name = "cmbUsuarioGasto"
        Me.cmbUsuarioGasto.Size = New System.Drawing.Size(194, 21)
        Me.cmbUsuarioGasto.TabIndex = 3
        '
        'txtidpago
        '
        Me.txtidpago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidpago.Decimals = CType(2, Byte)
        Me.txtidpago.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidpago.Enabled = False
        Me.txtidpago.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidpago.Location = New System.Drawing.Point(1209, 102)
        Me.txtidpago.MaxLength = 8
        Me.txtidpago.Name = "txtidpago"
        Me.txtidpago.Size = New System.Drawing.Size(40, 20)
        Me.txtidpago.TabIndex = 193
        Me.txtidpago.Text_1 = Nothing
        Me.txtidpago.Text_2 = Nothing
        Me.txtidpago.Text_3 = Nothing
        Me.txtidpago.Text_4 = Nothing
        Me.txtidpago.UserValues = Nothing
        Me.txtidpago.Visible = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(748, 60)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(104, 13)
        Me.Label26.TabIndex = 341
        Me.Label26.Text = "Facturas Pendientes"
        '
        'lstFacturasPendientes
        '
        Me.lstFacturasPendientes.CheckBoxes = True
        Me.lstFacturasPendientes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Codigo, Me.NroCompr})
        Me.lstFacturasPendientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstFacturasPendientes.Location = New System.Drawing.Point(751, 75)
        Me.lstFacturasPendientes.Name = "lstFacturasPendientes"
        Me.lstFacturasPendientes.Size = New System.Drawing.Size(194, 105)
        Me.lstFacturasPendientes.TabIndex = 340
        Me.lstFacturasPendientes.UseCompatibleStateImageBehavior = False
        Me.lstFacturasPendientes.View = System.Windows.Forms.View.Details
        '
        'Codigo
        '
        Me.Codigo.Text = "Codigo"
        '
        'NroCompr
        '
        Me.NroCompr.Text = "Nro Compr."
        Me.NroCompr.Width = 110
        '
        'txtSubtotalExento
        '
        Me.txtSubtotalExento.AccessibleName = ""
        Me.txtSubtotalExento.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubtotalExento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotalExento.Decimals = CType(2, Byte)
        Me.txtSubtotalExento.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalExento.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotalExento.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotalExento.Location = New System.Drawing.Point(140, 124)
        Me.txtSubtotalExento.MaxLength = 25
        Me.txtSubtotalExento.Name = "txtSubtotalExento"
        Me.txtSubtotalExento.Size = New System.Drawing.Size(85, 20)
        Me.txtSubtotalExento.TabIndex = 17
        Me.txtSubtotalExento.Text_1 = Nothing
        Me.txtSubtotalExento.Text_2 = Nothing
        Me.txtSubtotalExento.Text_3 = Nothing
        Me.txtSubtotalExento.Text_4 = Nothing
        Me.txtSubtotalExento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotalExento.UserValues = Nothing
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(139, 109)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(82, 13)
        Me.Label25.TabIndex = 339
        Me.Label25.Text = "Subtotal Exento"
        '
        'txtNroFacturaCompleto
        '
        Me.txtNroFacturaCompleto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtNroFacturaCompleto.Location = New System.Drawing.Point(621, 75)
        Me.txtNroFacturaCompleto.Name = "txtNroFacturaCompleto"
        Me.txtNroFacturaCompleto.Size = New System.Drawing.Size(115, 20)
        Me.txtNroFacturaCompleto.TabIndex = 14
        Me.txtNroFacturaCompleto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNroRemitoCompleto
        '
        Me.txtNroRemitoCompleto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtNroRemitoCompleto.Location = New System.Drawing.Point(140, 75)
        Me.txtNroRemitoCompleto.Name = "txtNroRemitoCompleto"
        Me.txtNroRemitoCompleto.Size = New System.Drawing.Size(115, 20)
        Me.txtNroRemitoCompleto.TabIndex = 10
        Me.txtNroRemitoCompleto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(63, 60)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(64, 13)
        Me.Label22.TabIndex = 337
        Me.Label22.Text = "Nro Compr.*"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(10, 60)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(46, 13)
        Me.Label23.TabIndex = 336
        Me.Label23.Text = "Pto Vta*"
        '
        'txtNroCompRemito
        '
        Me.txtNroCompRemito.AccessibleName = ""
        Me.txtNroCompRemito.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroCompRemito.Decimals = CType(2, Byte)
        Me.txtNroCompRemito.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroCompRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroCompRemito.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroCompRemito.Location = New System.Drawing.Point(66, 75)
        Me.txtNroCompRemito.MaxLength = 20
        Me.txtNroCompRemito.Name = "txtNroCompRemito"
        Me.txtNroCompRemito.Size = New System.Drawing.Size(68, 20)
        Me.txtNroCompRemito.TabIndex = 9
        Me.txtNroCompRemito.Text_1 = Nothing
        Me.txtNroCompRemito.Text_2 = Nothing
        Me.txtNroCompRemito.Text_3 = Nothing
        Me.txtNroCompRemito.Text_4 = Nothing
        Me.txtNroCompRemito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNroCompRemito.UserValues = Nothing
        '
        'txtPtoVtaRemito
        '
        Me.txtPtoVtaRemito.AccessibleName = ""
        Me.txtPtoVtaRemito.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPtoVtaRemito.Decimals = CType(2, Byte)
        Me.txtPtoVtaRemito.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtPtoVtaRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPtoVtaRemito.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtPtoVtaRemito.Location = New System.Drawing.Point(13, 75)
        Me.txtPtoVtaRemito.MaxLength = 20
        Me.txtPtoVtaRemito.Name = "txtPtoVtaRemito"
        Me.txtPtoVtaRemito.Size = New System.Drawing.Size(40, 20)
        Me.txtPtoVtaRemito.TabIndex = 8
        Me.txtPtoVtaRemito.Text_1 = Nothing
        Me.txtPtoVtaRemito.Text_2 = Nothing
        Me.txtPtoVtaRemito.Text_3 = Nothing
        Me.txtPtoVtaRemito.Text_4 = Nothing
        Me.txtPtoVtaRemito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPtoVtaRemito.UserValues = Nothing
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(50, 73)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(20, 25)
        Me.Label24.TabIndex = 335
        Me.Label24.Text = "-"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(137, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 332
        Me.Label4.Text = "Nro Remito"
        '
        'txtTipoMoneda
        '
        Me.txtTipoMoneda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtTipoMoneda.Location = New System.Drawing.Point(13, 124)
        Me.txtTipoMoneda.Name = "txtTipoMoneda"
        Me.txtTipoMoneda.Size = New System.Drawing.Size(46, 20)
        Me.txtTipoMoneda.TabIndex = 15
        '
        'FormattedTextBoxVB1
        '
        Me.FormattedTextBoxVB1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FormattedTextBoxVB1.Decimals = CType(2, Byte)
        Me.FormattedTextBoxVB1.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.FormattedTextBoxVB1.Enabled = False
        Me.FormattedTextBoxVB1.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.FormattedTextBoxVB1.Location = New System.Drawing.Point(672, 153)
        Me.FormattedTextBoxVB1.MaxLength = 8
        Me.FormattedTextBoxVB1.Name = "FormattedTextBoxVB1"
        Me.FormattedTextBoxVB1.Size = New System.Drawing.Size(23, 20)
        Me.FormattedTextBoxVB1.TabIndex = 330
        Me.FormattedTextBoxVB1.Text_1 = Nothing
        Me.FormattedTextBoxVB1.Text_2 = Nothing
        Me.FormattedTextBoxVB1.Text_3 = Nothing
        Me.FormattedTextBoxVB1.Text_4 = Nothing
        Me.FormattedTextBoxVB1.UserValues = Nothing
        Me.FormattedTextBoxVB1.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(322, 109)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(49, 13)
        Me.Label21.TabIndex = 329
        Me.Label21.Text = "Cant IVA"
        '
        'txtCantIVA
        '
        Me.txtCantIVA.Location = New System.Drawing.Point(322, 124)
        Me.txtCantIVA.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.txtCantIVA.Name = "txtCantIVA"
        Me.txtCantIVA.Size = New System.Drawing.Size(46, 20)
        Me.txtCantIVA.TabIndex = 19
        Me.txtCantIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtValorCambio
        '
        Me.txtValorCambio.AccessibleName = ""
        Me.txtValorCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtValorCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValorCambio.Decimals = CType(3, Byte)
        Me.txtValorCambio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtValorCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorCambio.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtValorCambio.Location = New System.Drawing.Point(65, 124)
        Me.txtValorCambio.MaxLength = 25
        Me.txtValorCambio.Name = "txtValorCambio"
        Me.txtValorCambio.Size = New System.Drawing.Size(69, 20)
        Me.txtValorCambio.TabIndex = 16
        Me.txtValorCambio.Text_1 = Nothing
        Me.txtValorCambio.Text_2 = Nothing
        Me.txtValorCambio.Text_3 = Nothing
        Me.txtValorCambio.Text_4 = Nothing
        Me.txtValorCambio.UserValues = Nothing
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(65, 109)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 13)
        Me.Label20.TabIndex = 328
        Me.Label20.Text = "Valor Cambio*"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(13, 109)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(46, 13)
        Me.Label17.TabIndex = 327
        Me.Label17.Text = "Moneda"
        '
        'txtMontoIVA27
        '
        Me.txtMontoIVA27.AccessibleName = ""
        Me.txtMontoIVA27.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoIVA27.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoIVA27.Decimals = CType(2, Byte)
        Me.txtMontoIVA27.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoIVA27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIVA27.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoIVA27.Location = New System.Drawing.Point(498, 124)
        Me.txtMontoIVA27.MaxLength = 25
        Me.txtMontoIVA27.Name = "txtMontoIVA27"
        Me.txtMontoIVA27.Size = New System.Drawing.Size(56, 20)
        Me.txtMontoIVA27.TabIndex = 22
        Me.txtMontoIVA27.Text_1 = Nothing
        Me.txtMontoIVA27.Text_2 = Nothing
        Me.txtMontoIVA27.Text_3 = Nothing
        Me.txtMontoIVA27.Text_4 = Nothing
        Me.txtMontoIVA27.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIVA27.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(495, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 326
        Me.Label3.Text = "IVA 27%"
        '
        'txtMontoIVA10
        '
        Me.txtMontoIVA10.AccessibleName = ""
        Me.txtMontoIVA10.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoIVA10.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoIVA10.Decimals = CType(2, Byte)
        Me.txtMontoIVA10.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoIVA10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIVA10.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoIVA10.Location = New System.Drawing.Point(374, 124)
        Me.txtMontoIVA10.MaxLength = 25
        Me.txtMontoIVA10.Name = "txtMontoIVA10"
        Me.txtMontoIVA10.Size = New System.Drawing.Size(56, 20)
        Me.txtMontoIVA10.TabIndex = 20
        Me.txtMontoIVA10.Text_1 = Nothing
        Me.txtMontoIVA10.Text_2 = Nothing
        Me.txtMontoIVA10.Text_3 = Nothing
        Me.txtMontoIVA10.Text_4 = Nothing
        Me.txtMontoIVA10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIVA10.UserValues = Nothing
        '
        'txtMontoIVA21
        '
        Me.txtMontoIVA21.AccessibleName = ""
        Me.txtMontoIVA21.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoIVA21.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoIVA21.Decimals = CType(2, Byte)
        Me.txtMontoIVA21.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoIVA21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIVA21.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoIVA21.Location = New System.Drawing.Point(436, 124)
        Me.txtMontoIVA21.MaxLength = 25
        Me.txtMontoIVA21.Name = "txtMontoIVA21"
        Me.txtMontoIVA21.Size = New System.Drawing.Size(56, 20)
        Me.txtMontoIVA21.TabIndex = 21
        Me.txtMontoIVA21.Text_1 = Nothing
        Me.txtMontoIVA21.Text_2 = Nothing
        Me.txtMontoIVA21.Text_3 = Nothing
        Me.txtMontoIVA21.Text_4 = Nothing
        Me.txtMontoIVA21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIVA21.UserValues = Nothing
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = "*Subtotal"
        Me.txtSubtotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubtotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotal.Location = New System.Drawing.Point(231, 124)
        Me.txtSubtotal.MaxLength = 25
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(85, 20)
        Me.txtSubtotal.TabIndex = 18
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSubtotal.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(374, 109)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 13)
        Me.Label18.TabIndex = 325
        Me.Label18.Text = "IVA 10,5%"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(436, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 324
        Me.Label5.Text = "IVA 21%"
        '
        'lblImpuestos
        '
        Me.lblImpuestos.BackColor = System.Drawing.Color.White
        Me.lblImpuestos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblImpuestos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImpuestos.Location = New System.Drawing.Point(560, 124)
        Me.lblImpuestos.Name = "lblImpuestos"
        Me.lblImpuestos.Size = New System.Drawing.Size(85, 20)
        Me.lblImpuestos.TabIndex = 23
        Me.lblImpuestos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label40
        '
        Me.label40.AutoSize = True
        Me.label40.Location = New System.Drawing.Point(557, 109)
        Me.label40.Name = "label40"
        Me.label40.Size = New System.Drawing.Size(82, 13)
        Me.label40.TabIndex = 323
        Me.label40.Text = "Total Impuestos"
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(651, 124)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(85, 20)
        Me.lblTotal.TabIndex = 24
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(231, 109)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 13)
        Me.Label12.TabIndex = 322
        Me.Label12.Text = "Subtotal*"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(651, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 321
        Me.Label7.Text = "Total"
        '
        'txtIdComprobante
        '
        Me.txtIdComprobante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdComprobante.Decimals = CType(2, Byte)
        Me.txtIdComprobante.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdComprobante.Enabled = False
        Me.txtIdComprobante.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdComprobante.Location = New System.Drawing.Point(396, 53)
        Me.txtIdComprobante.MaxLength = 8
        Me.txtIdComprobante.Name = "txtIdComprobante"
        Me.txtIdComprobante.Size = New System.Drawing.Size(16, 20)
        Me.txtIdComprobante.TabIndex = 312
        Me.txtIdComprobante.Text_1 = Nothing
        Me.txtIdComprobante.Text_2 = Nothing
        Me.txtIdComprobante.Text_3 = Nothing
        Me.txtIdComprobante.Text_4 = Nothing
        Me.txtIdComprobante.UserValues = Nothing
        Me.txtIdComprobante.Visible = False
        '
        'txtIdTipoGasto
        '
        Me.txtIdTipoGasto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdTipoGasto.Decimals = CType(2, Byte)
        Me.txtIdTipoGasto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdTipoGasto.Enabled = False
        Me.txtIdTipoGasto.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdTipoGasto.Location = New System.Drawing.Point(295, 16)
        Me.txtIdTipoGasto.MaxLength = 8
        Me.txtIdTipoGasto.Name = "txtIdTipoGasto"
        Me.txtIdTipoGasto.Size = New System.Drawing.Size(16, 20)
        Me.txtIdTipoGasto.TabIndex = 311
        Me.txtIdTipoGasto.Text_1 = Nothing
        Me.txtIdTipoGasto.Text_2 = Nothing
        Me.txtIdTipoGasto.Text_3 = Nothing
        Me.txtIdTipoGasto.Text_4 = Nothing
        Me.txtIdTipoGasto.UserValues = Nothing
        Me.txtIdTipoGasto.Visible = False
        '
        'txtIdMoneda
        '
        Me.txtIdMoneda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdMoneda.Decimals = CType(2, Byte)
        Me.txtIdMoneda.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdMoneda.Enabled = False
        Me.txtIdMoneda.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdMoneda.Location = New System.Drawing.Point(758, 17)
        Me.txtIdMoneda.MaxLength = 8
        Me.txtIdMoneda.Name = "txtIdMoneda"
        Me.txtIdMoneda.Size = New System.Drawing.Size(16, 20)
        Me.txtIdMoneda.TabIndex = 310
        Me.txtIdMoneda.Text_1 = Nothing
        Me.txtIdMoneda.Text_2 = Nothing
        Me.txtIdMoneda.Text_3 = Nothing
        Me.txtIdMoneda.Text_4 = Nothing
        Me.txtIdMoneda.UserValues = Nothing
        Me.txtIdMoneda.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(265, 60)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 309
        Me.Label19.Text = "Tipo Comprobante*"
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(14, 172)
        Me.txtNota.MaxLength = 200
        Me.txtNota.Multiline = True
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(320, 20)
        Me.txtNota.TabIndex = 26
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'cmbTipoComprobante
        '
        Me.cmbTipoComprobante.AccessibleName = "*TipoComprobante"
        Me.cmbTipoComprobante.DropDownHeight = 500
        Me.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoComprobante.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoComprobante.FormattingEnabled = True
        Me.cmbTipoComprobante.IntegralHeight = False
        Me.cmbTipoComprobante.Location = New System.Drawing.Point(265, 75)
        Me.cmbTipoComprobante.Name = "cmbTipoComprobante"
        Me.cmbTipoComprobante.Size = New System.Drawing.Size(223, 21)
        Me.cmbTipoComprobante.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(618, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 13)
        Me.Label16.TabIndex = 301
        Me.Label16.Text = "Nro Factura"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(948, 56)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(106, 13)
        Me.Label13.TabIndex = 261
        Me.Label13.Text = "Detalle de Impuestos"
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
        Me.grdImpuestos.Location = New System.Drawing.Point(951, 70)
        Me.grdImpuestos.Name = "grdImpuestos"
        Me.grdImpuestos.Size = New System.Drawing.Size(368, 133)
        Me.grdImpuestos.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(544, 60)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 259
        Me.Label11.Text = "Nro Compr."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(491, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 13)
        Me.Label10.TabIndex = 258
        Me.Label10.Text = "Pto Vta"
        '
        'txtNroFactura
        '
        Me.txtNroFactura.AccessibleName = ""
        Me.txtNroFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroFactura.Decimals = CType(2, Byte)
        Me.txtNroFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroFactura.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtNroFactura.Location = New System.Drawing.Point(547, 75)
        Me.txtNroFactura.MaxLength = 20
        Me.txtNroFactura.Name = "txtNroFactura"
        Me.txtNroFactura.Size = New System.Drawing.Size(68, 20)
        Me.txtNroFactura.TabIndex = 13
        Me.txtNroFactura.Text_1 = Nothing
        Me.txtNroFactura.Text_2 = Nothing
        Me.txtNroFactura.Text_3 = Nothing
        Me.txtNroFactura.Text_4 = Nothing
        Me.txtNroFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNroFactura.UserValues = Nothing
        '
        'txtPtoVta
        '
        Me.txtPtoVta.AccessibleName = ""
        Me.txtPtoVta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPtoVta.Decimals = CType(2, Byte)
        Me.txtPtoVta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtPtoVta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPtoVta.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtPtoVta.Location = New System.Drawing.Point(494, 75)
        Me.txtPtoVta.MaxLength = 20
        Me.txtPtoVta.Name = "txtPtoVta"
        Me.txtPtoVta.Size = New System.Drawing.Size(40, 20)
        Me.txtPtoVta.TabIndex = 12
        Me.txtPtoVta.Text_1 = Nothing
        Me.txtPtoVta.Text_2 = Nothing
        Me.txtPtoVta.Text_3 = Nothing
        Me.txtPtoVta.Text_4 = Nothing
        Me.txtPtoVta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPtoVta.UserValues = Nothing
        '
        'chkAnulados
        '
        Me.chkAnulados.AutoSize = True
        Me.chkAnulados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnulados.ForeColor = System.Drawing.Color.Red
        Me.chkAnulados.Location = New System.Drawing.Point(751, 186)
        Me.chkAnulados.Name = "chkAnulados"
        Me.chkAnulados.Size = New System.Drawing.Size(109, 17)
        Me.chkAnulados.TabIndex = 30
        Me.chkAnulados.Text = "Ver Eliminados"
        Me.chkAnulados.UseVisualStyleBackColor = True
        '
        'chkPeriodo
        '
        Me.chkPeriodo.AutoSize = True
        Me.chkPeriodo.Location = New System.Drawing.Point(347, 155)
        Me.chkPeriodo.Name = "chkPeriodo"
        Me.chkPeriodo.Size = New System.Drawing.Size(109, 17)
        Me.chkPeriodo.TabIndex = 27
        Me.chkPeriodo.Text = "Periodo a Imputar"
        Me.chkPeriodo.UseVisualStyleBackColor = True
        '
        'dtpPeriodo
        '
        Me.dtpPeriodo.CustomFormat = "MMMM/yyyy"
        Me.dtpPeriodo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPeriodo.Location = New System.Drawing.Point(347, 172)
        Me.dtpPeriodo.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpPeriodo.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpPeriodo.Name = "dtpPeriodo"
        Me.dtpPeriodo.Size = New System.Drawing.Size(142, 20)
        Me.dtpPeriodo.TabIndex = 28
        Me.dtpPeriodo.Tag = "202"
        '
        'txtIdProveedor
        '
        Me.txtIdProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdProveedor.Decimals = CType(2, Byte)
        Me.txtIdProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProveedor.Enabled = False
        Me.txtIdProveedor.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdProveedor.Location = New System.Drawing.Point(683, 7)
        Me.txtIdProveedor.MaxLength = 8
        Me.txtIdProveedor.Name = "txtIdProveedor"
        Me.txtIdProveedor.Size = New System.Drawing.Size(16, 20)
        Me.txtIdProveedor.TabIndex = 243
        Me.txtIdProveedor.Text_1 = Nothing
        Me.txtIdProveedor.Text_2 = Nothing
        Me.txtIdProveedor.Text_3 = Nothing
        Me.txtIdProveedor.Text_4 = Nothing
        Me.txtIdProveedor.UserValues = Nothing
        Me.txtIdProveedor.Visible = False
        '
        'chkCancelado
        '
        Me.chkCancelado.AutoSize = True
        Me.chkCancelado.Enabled = False
        Me.chkCancelado.Location = New System.Drawing.Point(1076, 59)
        Me.chkCancelado.Name = "chkCancelado"
        Me.chkCancelado.Size = New System.Drawing.Size(77, 17)
        Me.chkCancelado.TabIndex = 21
        Me.chkCancelado.Text = "Cancelado"
        Me.chkCancelado.UseVisualStyleBackColor = True
        Me.chkCancelado.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(206, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(63, 13)
        Me.Label15.TabIndex = 241
        Me.Label15.Text = "Tipo Gasto*"
        '
        'cmbTipoGasto
        '
        Me.cmbTipoGasto.AccessibleName = "*TipoGasto"
        Me.cmbTipoGasto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbTipoGasto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTipoGasto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoGasto.FormattingEnabled = True
        Me.cmbTipoGasto.Location = New System.Drawing.Point(209, 32)
        Me.cmbTipoGasto.Name = "cmbTipoGasto"
        Me.cmbTipoGasto.Size = New System.Drawing.Size(206, 21)
        Me.cmbTipoGasto.TabIndex = 2
        '
        'lblNroRecepcion
        '
        Me.lblNroRecepcion.AutoSize = True
        Me.lblNroRecepcion.Enabled = False
        Me.lblNroRecepcion.Location = New System.Drawing.Point(987, 17)
        Me.lblNroRecepcion.Name = "lblNroRecepcion"
        Me.lblNroRecepcion.Size = New System.Drawing.Size(79, 13)
        Me.lblNroRecepcion.TabIndex = 239
        Me.lblNroRecepcion.Text = "Nro Recepción"
        '
        'lblNroRemito1
        '
        Me.lblNroRemito1.AutoSize = True
        Me.lblNroRemito1.Enabled = False
        Me.lblNroRemito1.Location = New System.Drawing.Point(1173, 17)
        Me.lblNroRemito1.Name = "lblNroRemito1"
        Me.lblNroRemito1.Size = New System.Drawing.Size(60, 13)
        Me.lblNroRemito1.TabIndex = 237
        Me.lblNroRemito1.Text = "Nro Remito"
        '
        'lblNroRemito
        '
        Me.lblNroRemito.BackColor = System.Drawing.Color.White
        Me.lblNroRemito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNroRemito.Enabled = False
        Me.lblNroRemito.Location = New System.Drawing.Point(1176, 33)
        Me.lblNroRemito.Name = "lblNroRemito"
        Me.lblNroRemito.Size = New System.Drawing.Size(143, 19)
        Me.lblNroRemito.TabIndex = 7
        '
        'chkRecepcion
        '
        Me.chkRecepcion.AutoSize = True
        Me.chkRecepcion.Enabled = False
        Me.chkRecepcion.Location = New System.Drawing.Point(906, 34)
        Me.chkRecepcion.Name = "chkRecepcion"
        Me.chkRecepcion.Size = New System.Drawing.Size(78, 17)
        Me.chkRecepcion.TabIndex = 5
        Me.chkRecepcion.Text = "Recepción"
        Me.chkRecepcion.UseVisualStyleBackColor = True
        '
        'cmbRecepcion
        '
        Me.cmbRecepcion.AccessibleName = ""
        Me.cmbRecepcion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbRecepcion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRecepcion.Enabled = False
        Me.cmbRecepcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRecepcion.FormattingEnabled = True
        Me.cmbRecepcion.Location = New System.Drawing.Point(990, 32)
        Me.cmbRecepcion.Name = "cmbRecepcion"
        Me.cmbRecepcion.Size = New System.Drawing.Size(180, 21)
        Me.cmbRecepcion.TabIndex = 6
        '
        'chkFacturaCancelada
        '
        Me.chkFacturaCancelada.AutoSize = True
        Me.chkFacturaCancelada.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFacturaCancelada.Location = New System.Drawing.Point(536, 172)
        Me.chkFacturaCancelada.Name = "chkFacturaCancelada"
        Me.chkFacturaCancelada.Size = New System.Drawing.Size(170, 19)
        Me.chkFacturaCancelada.TabIndex = 29
        Me.chkFacturaCancelada.Text = "Pago Efectivo Contado"
        Me.chkFacturaCancelada.UseVisualStyleBackColor = True
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(101, 32)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label14
        '
        Me.Label14.AccessibleName = ""
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(99, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(37, 13)
        Me.Label14.TabIndex = 139
        Me.Label14.Text = "Fecha"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(142, 6)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(16, 20)
        Me.txtID.TabIndex = 135
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
        Me.Label1.Location = New System.Drawing.Point(164, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'picProveedores
        '
        Me.picProveedores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picProveedores.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picProveedores.Image = Global.SEYC.My.Resources.Resources.Info
        Me.picProveedores.Location = New System.Drawing.Point(882, 32)
        Me.picProveedores.Name = "picProveedores"
        Me.picProveedores.Size = New System.Drawing.Size(18, 20)
        Me.picProveedores.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picProveedores.TabIndex = 125
        Me.picProveedores.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(621, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 124
        Me.Label9.Text = "Proveedor*"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AccessibleName = "*Proveedor"
        Me.cmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(621, 32)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(255, 21)
        Me.cmbProveedor.TabIndex = 4
        '
        'txtCodigo
        '
        Me.txtCodigo.AccessibleName = ""
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigo.Decimals = CType(2, Byte)
        Me.txtCodigo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCodigo.Enabled = False
        Me.txtCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigo.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCodigo.Location = New System.Drawing.Point(13, 32)
        Me.txtCodigo.MaxLength = 25
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(82, 20)
        Me.txtCodigo.TabIndex = 0
        Me.txtCodigo.Text_1 = Nothing
        Me.txtCodigo.Text_2 = Nothing
        Me.txtCodigo.Text_3 = Nothing
        Me.txtCodigo.Text_4 = Nothing
        Me.txtCodigo.UserValues = Nothing
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Gasto Nro."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(531, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 25)
        Me.Label6.TabIndex = 257
        Me.Label6.Text = "-"
        '
        'txtNroRemitoControl
        '
        Me.txtNroRemitoControl.AccessibleName = ""
        Me.txtNroRemitoControl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroRemitoControl.Decimals = CType(2, Byte)
        Me.txtNroRemitoControl.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroRemitoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroRemitoControl.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroRemitoControl.Location = New System.Drawing.Point(154, 258)
        Me.txtNroRemitoControl.MaxLength = 20
        Me.txtNroRemitoControl.Name = "txtNroRemitoControl"
        Me.txtNroRemitoControl.Size = New System.Drawing.Size(121, 20)
        Me.txtNroRemitoControl.TabIndex = 304
        Me.txtNroRemitoControl.Text_1 = Nothing
        Me.txtNroRemitoControl.Text_2 = Nothing
        Me.txtNroRemitoControl.Text_3 = Nothing
        Me.txtNroRemitoControl.Text_4 = Nothing
        Me.txtNroRemitoControl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtNroRemitoControl.UserValues = Nothing
        Me.txtNroRemitoControl.Visible = False
        '
        'txtIdRecepcion
        '
        Me.txtIdRecepcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdRecepcion.Decimals = CType(2, Byte)
        Me.txtIdRecepcion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdRecepcion.Enabled = False
        Me.txtIdRecepcion.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdRecepcion.Location = New System.Drawing.Point(85, 258)
        Me.txtIdRecepcion.MaxLength = 8
        Me.txtIdRecepcion.Name = "txtIdRecepcion"
        Me.txtIdRecepcion.Size = New System.Drawing.Size(38, 20)
        Me.txtIdRecepcion.TabIndex = 303
        Me.txtIdRecepcion.Text_1 = Nothing
        Me.txtIdRecepcion.Text_2 = Nothing
        Me.txtIdRecepcion.Text_3 = Nothing
        Me.txtIdRecepcion.Text_4 = Nothing
        Me.txtIdRecepcion.UserValues = Nothing
        Me.txtIdRecepcion.Visible = False
        '
        'txtNroFacturaCompletoControl
        '
        Me.txtNroFacturaCompletoControl.AccessibleName = ""
        Me.txtNroFacturaCompletoControl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroFacturaCompletoControl.Decimals = CType(2, Byte)
        Me.txtNroFacturaCompletoControl.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroFacturaCompletoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroFacturaCompletoControl.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroFacturaCompletoControl.Location = New System.Drawing.Point(284, 258)
        Me.txtNroFacturaCompletoControl.MaxLength = 20
        Me.txtNroFacturaCompletoControl.Name = "txtNroFacturaCompletoControl"
        Me.txtNroFacturaCompletoControl.ReadOnly = True
        Me.txtNroFacturaCompletoControl.Size = New System.Drawing.Size(121, 20)
        Me.txtNroFacturaCompletoControl.TabIndex = 302
        Me.txtNroFacturaCompletoControl.Text_1 = Nothing
        Me.txtNroFacturaCompletoControl.Text_2 = Nothing
        Me.txtNroFacturaCompletoControl.Text_3 = Nothing
        Me.txtNroFacturaCompletoControl.Text_4 = Nothing
        Me.txtNroFacturaCompletoControl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtNroFacturaCompletoControl.UserValues = Nothing
        Me.txtNroFacturaCompletoControl.Visible = False
        '
        'lblMontoIva
        '
        Me.lblMontoIva.BackColor = System.Drawing.Color.White
        Me.lblMontoIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMontoIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoIva.Location = New System.Drawing.Point(516, 258)
        Me.lblMontoIva.Name = "lblMontoIva"
        Me.lblMontoIva.Size = New System.Drawing.Size(85, 20)
        Me.lblMontoIva.TabIndex = 290
        Me.lblMontoIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMontoIva.Visible = False
        '
        'label30
        '
        Me.label30.AutoSize = True
        Me.label30.Location = New System.Drawing.Point(453, 262)
        Me.label30.Name = "label30"
        Me.label30.Size = New System.Drawing.Size(57, 13)
        Me.label30.TabIndex = 295
        Me.label30.Text = "Monto IVA"
        Me.label30.Visible = False
        '
        'ContextMenuStripIVA
        '
        Me.ContextMenuStripIVA.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItemIVA})
        Me.ContextMenuStripIVA.Name = "ContextMenuStrip1"
        Me.ContextMenuStripIVA.Size = New System.Drawing.Size(146, 26)
        '
        'BorrarElItemToolStripMenuItemIVA
        '
        Me.BorrarElItemToolStripMenuItemIVA.Name = "BorrarElItemToolStripMenuItemIVA"
        Me.BorrarElItemToolStripMenuItemIVA.Size = New System.Drawing.Size(145, 22)
        Me.BorrarElItemToolStripMenuItemIVA.Text = "Borrar el Item"
        '
        'frmGastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 431)
        Me.Controls.Add(Me.txtNroFacturaCompletoControl)
        Me.Controls.Add(Me.txtNroRemitoControl)
        Me.Controls.Add(Me.txtIdRecepcion)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblMontoIva)
        Me.Controls.Add(Me.label30)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGastos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Administración de Gastos"
        Me.Controls.SetChildIndex(Me.label30, 0)
        Me.Controls.SetChildIndex(Me.lblMontoIva, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.txtIdRecepcion, 0)
        Me.Controls.SetChildIndex(Me.txtNroRemitoControl, 0)
        Me.Controls.SetChildIndex(Me.txtNroFacturaCompletoControl, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtCantIVA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdImpuestos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picProveedores, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripIVA.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub






    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox


    Friend WithEvents txtCodigo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label


    Friend Shadows WithEvents ContextMenuStripIVA As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItemIVA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents picProveedores As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkFacturaCancelada As System.Windows.Forms.CheckBox
    Friend WithEvents txtidpago As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkRecepcion As System.Windows.Forms.CheckBox
    Friend WithEvents cmbRecepcion As System.Windows.Forms.ComboBox
    Friend WithEvents lblNroRemito As System.Windows.Forms.Label
    Friend WithEvents lblNroRecepcion As System.Windows.Forms.Label
    Friend WithEvents lblNroRemito1 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoGasto As System.Windows.Forms.ComboBox
    Friend WithEvents chkCancelado As System.Windows.Forms.CheckBox
    Friend WithEvents txtIdProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents dtpPeriodo As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkPeriodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnulados As System.Windows.Forms.CheckBox
    Friend WithEvents txtNroFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPtoVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grdImpuestos As System.Windows.Forms.DataGridView
    Friend WithEvents lblMontoIva As System.Windows.Forms.Label
    Friend WithEvents label30 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNroFacturaCompletoControl As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdRecepcion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroRemitoControl As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbTipoComprobante As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtIdMoneda As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdTipoGasto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdComprobante As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtTipoMoneda As System.Windows.Forms.Label
    Friend WithEvents FormattedTextBoxVB1 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtCantIVA As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtValorCambio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtMontoIVA27 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMontoIVA10 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMontoIVA21 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblImpuestos As System.Windows.Forms.Label
    Friend WithEvents label40 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNroRemitoCompleto As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtNroCompRemito As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPtoVtaRemito As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNroFacturaCompleto As System.Windows.Forms.Label
    Friend WithEvents txtSubtotalExento As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lstFacturasPendientes As System.Windows.Forms.ListView
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Codigo As System.Windows.Forms.ColumnHeader
    Friend WithEvents NroCompr As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cmbUsuarioGasto As System.Windows.Forms.ComboBox

End Class
