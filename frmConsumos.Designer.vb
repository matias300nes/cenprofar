<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsumos

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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtIdFactura = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.cmbCondIVA = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.ChkPago = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkCerrar = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.txtOC = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.chkOC = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.txtFactura = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.chkFactura = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.rdVenta = New System.Windows.Forms.RadioButton
        Me.rdConsumoInterno = New System.Windows.Forms.RadioButton
        Me.chkRecDescGlobal = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkDesc = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkRecargo = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblCantidadFilas = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtNotaGestion = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label17 = New System.Windows.Forms.Label
        Me.cmbVendedor = New System.Windows.Forms.ComboBox
        Me.txtporcrecargo = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtIvaTotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbEntregaren = New System.Windows.Forms.ComboBox
        Me.cmbComprador = New System.Windows.Forms.ComboBox
        Me.PicNotas = New System.Windows.Forms.PictureBox
        Me.lstNotas = New System.Windows.Forms.ListView
        Me.Nota = New System.Windows.Forms.ColumnHeader
        Me.chkNotas = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.grdItems = New System.Windows.Forms.DataGridView
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbCliente = New System.Windows.Forms.ComboBox
        Me.PicClientes = New System.Windows.Forms.PictureBox
        Me.chkEntrega = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkEliminado = New System.Windows.Forms.CheckBox
        Me.chkRetiradopor = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.chkOcultarGanancia = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.picGanancia = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmbUnidadVenta = New System.Windows.Forms.ToolStripComboBox
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGanancia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtIdFactura)
        Me.GroupBox1.Controls.Add(Me.cmbCondIVA)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.ChkPago)
        Me.GroupBox1.Controls.Add(Me.chkCerrar)
        Me.GroupBox1.Controls.Add(Me.txtOC)
        Me.GroupBox1.Controls.Add(Me.chkOC)
        Me.GroupBox1.Controls.Add(Me.txtFactura)
        Me.GroupBox1.Controls.Add(Me.chkFactura)
        Me.GroupBox1.Controls.Add(Me.rdVenta)
        Me.GroupBox1.Controls.Add(Me.rdConsumoInterno)
        Me.GroupBox1.Controls.Add(Me.chkRecDescGlobal)
        Me.GroupBox1.Controls.Add(Me.chkDesc)
        Me.GroupBox1.Controls.Add(Me.chkRecargo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtNotaGestion)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cmbVendedor)
        Me.GroupBox1.Controls.Add(Me.txtporcrecargo)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtIvaTotal)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbEntregaren)
        Me.GroupBox1.Controls.Add(Me.cmbComprador)
        Me.GroupBox1.Controls.Add(Me.PicNotas)
        Me.GroupBox1.Controls.Add(Me.lstNotas)
        Me.GroupBox1.Controls.Add(Me.chkNotas)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.PicClientes)
        Me.GroupBox1.Controls.Add(Me.chkEntrega)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.chkRetiradopor)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.chkOcultarGanancia)
        Me.GroupBox1.Controls.Add(Me.picGanancia)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1324, 560)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtIdFactura
        '
        Me.txtIdFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdFactura.Decimals = CType(2, Byte)
        Me.txtIdFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdFactura.Enabled = False
        Me.txtIdFactura.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdFactura.Location = New System.Drawing.Point(1135, 71)
        Me.txtIdFactura.MaxLength = 8
        Me.txtIdFactura.Name = "txtIdFactura"
        Me.txtIdFactura.Size = New System.Drawing.Size(77, 20)
        Me.txtIdFactura.TabIndex = 188
        Me.txtIdFactura.Text_1 = Nothing
        Me.txtIdFactura.Text_2 = Nothing
        Me.txtIdFactura.Text_3 = Nothing
        Me.txtIdFactura.Text_4 = Nothing
        Me.txtIdFactura.UserValues = Nothing
        Me.txtIdFactura.Visible = False
        '
        'cmbCondIVA
        '
        Me.cmbCondIVA.AccessibleName = ""
        Me.cmbCondIVA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondIVA.FormattingEnabled = True
        Me.cmbCondIVA.Items.AddRange(New Object() {"RI"})
        Me.cmbCondIVA.Location = New System.Drawing.Point(1107, 32)
        Me.cmbCondIVA.Name = "cmbCondIVA"
        Me.cmbCondIVA.Size = New System.Drawing.Size(105, 21)
        Me.cmbCondIVA.TabIndex = 186
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1104, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 13)
        Me.Label10.TabIndex = 187
        Me.Label10.Text = "Condición IVA*"
        '
        'ChkPago
        '
        Me.ChkPago.AutoSize = True
        Me.ChkPago.Location = New System.Drawing.Point(843, 40)
        Me.ChkPago.Name = "ChkPago"
        Me.ChkPago.Size = New System.Drawing.Size(91, 15)
        Me.ChkPago.TabIndex = 185
        Me.ChkPago.Text = "Habilitar Pago"
        Me.ChkPago.TextColor = System.Drawing.Color.Blue
        '
        'chkCerrar
        '
        Me.chkCerrar.AutoSize = True
        Me.chkCerrar.Location = New System.Drawing.Point(843, 19)
        Me.chkCerrar.Name = "chkCerrar"
        Me.chkCerrar.Size = New System.Drawing.Size(122, 15)
        Me.chkCerrar.TabIndex = 184
        Me.chkCerrar.Text = "Cerrar Comprobante"
        Me.chkCerrar.TextColor = System.Drawing.Color.Blue
        '
        'txtOC
        '
        Me.txtOC.AccessibleName = ""
        Me.txtOC.BackColor = System.Drawing.SystemColors.Window
        Me.txtOC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOC.Decimals = CType(2, Byte)
        Me.txtOC.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOC.Enabled = False
        Me.txtOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOC.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtOC.Location = New System.Drawing.Point(1218, 32)
        Me.txtOC.MaxLength = 25
        Me.txtOC.Name = "txtOC"
        Me.txtOC.Size = New System.Drawing.Size(105, 20)
        Me.txtOC.TabIndex = 183
        Me.txtOC.Text_1 = Nothing
        Me.txtOC.Text_2 = Nothing
        Me.txtOC.Text_3 = Nothing
        Me.txtOC.Text_4 = Nothing
        Me.txtOC.UserValues = Nothing
        '
        'chkOC
        '
        Me.chkOC.AutoSize = True
        Me.chkOC.Location = New System.Drawing.Point(1218, 13)
        Me.chkOC.Name = "chkOC"
        Me.chkOC.Size = New System.Drawing.Size(81, 15)
        Me.chkOC.TabIndex = 182
        Me.chkOC.Text = "Habilitar OC"
        Me.chkOC.TextColor = System.Drawing.Color.Blue
        '
        'txtFactura
        '
        Me.txtFactura.AccessibleName = ""
        Me.txtFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFactura.Decimals = CType(2, Byte)
        Me.txtFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtFactura.Enabled = False
        Me.txtFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFactura.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtFactura.Location = New System.Drawing.Point(996, 32)
        Me.txtFactura.MaxLength = 25
        Me.txtFactura.Name = "txtFactura"
        Me.txtFactura.Size = New System.Drawing.Size(105, 20)
        Me.txtFactura.TabIndex = 15
        Me.txtFactura.Text_1 = Nothing
        Me.txtFactura.Text_2 = Nothing
        Me.txtFactura.Text_3 = Nothing
        Me.txtFactura.Text_4 = Nothing
        Me.txtFactura.UserValues = Nothing
        '
        'chkFactura
        '
        Me.chkFactura.AutoSize = True
        Me.chkFactura.Location = New System.Drawing.Point(992, 13)
        Me.chkFactura.Name = "chkFactura"
        Me.chkFactura.Size = New System.Drawing.Size(102, 15)
        Me.chkFactura.TabIndex = 14
        Me.chkFactura.Text = "Habilitar Factura"
        Me.chkFactura.TextColor = System.Drawing.Color.Blue
        '
        'rdVenta
        '
        Me.rdVenta.AutoSize = True
        Me.rdVenta.Checked = True
        Me.rdVenta.Location = New System.Drawing.Point(199, 34)
        Me.rdVenta.Name = "rdVenta"
        Me.rdVenta.Size = New System.Drawing.Size(53, 17)
        Me.rdVenta.TabIndex = 2
        Me.rdVenta.TabStop = True
        Me.rdVenta.Text = "Venta"
        Me.rdVenta.UseVisualStyleBackColor = True
        '
        'rdConsumoInterno
        '
        Me.rdConsumoInterno.AutoSize = True
        Me.rdConsumoInterno.Location = New System.Drawing.Point(258, 34)
        Me.rdConsumoInterno.Name = "rdConsumoInterno"
        Me.rdConsumoInterno.Size = New System.Drawing.Size(105, 17)
        Me.rdConsumoInterno.TabIndex = 3
        Me.rdConsumoInterno.Text = "Consumo Interno"
        Me.rdConsumoInterno.UseVisualStyleBackColor = True
        '
        'chkRecDescGlobal
        '
        Me.chkRecDescGlobal.AutoSize = True
        Me.chkRecDescGlobal.Location = New System.Drawing.Point(704, 16)
        Me.chkRecDescGlobal.Name = "chkRecDescGlobal"
        Me.chkRecDescGlobal.Size = New System.Drawing.Size(110, 15)
        Me.chkRecDescGlobal.TabIndex = 6
        Me.chkRecDescGlobal.Text = "Rec / Desc Global"
        Me.chkRecDescGlobal.TextColor = System.Drawing.Color.Blue
        '
        'chkDesc
        '
        Me.chkDesc.AutoSize = True
        Me.chkDesc.Enabled = False
        Me.chkDesc.Location = New System.Drawing.Point(704, 44)
        Me.chkDesc.Name = "chkDesc"
        Me.chkDesc.Size = New System.Drawing.Size(50, 15)
        Me.chkDesc.TabIndex = 8
        Me.chkDesc.Text = "Desc."
        Me.chkDesc.TextColor = System.Drawing.Color.Blue
        '
        'chkRecargo
        '
        Me.chkRecargo.AutoSize = True
        Me.chkRecargo.Enabled = False
        Me.chkRecargo.Location = New System.Drawing.Point(704, 29)
        Me.chkRecargo.Name = "chkRecargo"
        Me.chkRecargo.Size = New System.Drawing.Size(44, 15)
        Me.chkRecargo.TabIndex = 7
        Me.chkRecargo.Text = "Rec."
        Me.chkRecargo.TextColor = System.Drawing.Color.Blue
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1225, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 534)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(98, 13)
        Me.Label19.TabIndex = 176
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(106, 534)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(46, 13)
        Me.lblCantidadFilas.TabIndex = 175
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(568, 59)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 13)
        Me.Label18.TabIndex = 172
        Me.Label18.Text = "Nota de Gestión"
        '
        'txtNotaGestion
        '
        Me.txtNotaGestion.AccessibleName = ""
        Me.txtNotaGestion.Decimals = CType(2, Byte)
        Me.txtNotaGestion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNotaGestion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotaGestion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNotaGestion.Location = New System.Drawing.Point(571, 74)
        Me.txtNotaGestion.Name = "txtNotaGestion"
        Me.txtNotaGestion.Size = New System.Drawing.Size(530, 20)
        Me.txtNotaGestion.TabIndex = 21
        Me.txtNotaGestion.Text_1 = Nothing
        Me.txtNotaGestion.Text_2 = Nothing
        Me.txtNotaGestion.Text_3 = Nothing
        Me.txtNotaGestion.Text_4 = Nothing
        Me.txtNotaGestion.UserValues = Nothing
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(384, 59)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 13)
        Me.Label17.TabIndex = 170
        Me.Label17.Text = "Vendedor*"
        '
        'cmbVendedor
        '
        Me.cmbVendedor.AccessibleName = "*VENDEDOR"
        Me.cmbVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbVendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbVendedor.DropDownHeight = 300
        Me.cmbVendedor.FormattingEnabled = True
        Me.cmbVendedor.IntegralHeight = False
        Me.cmbVendedor.Location = New System.Drawing.Point(387, 74)
        Me.cmbVendedor.Name = "cmbVendedor"
        Me.cmbVendedor.Size = New System.Drawing.Size(178, 21)
        Me.cmbVendedor.TabIndex = 20
        '
        'txtporcrecargo
        '
        Me.txtporcrecargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtporcrecargo.Decimals = CType(2, Byte)
        Me.txtporcrecargo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtporcrecargo.Enabled = False
        Me.txtporcrecargo.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtporcrecargo.Location = New System.Drawing.Point(760, 36)
        Me.txtporcrecargo.MaxLength = 20
        Me.txtporcrecargo.Name = "txtporcrecargo"
        Me.txtporcrecargo.Size = New System.Drawing.Size(46, 20)
        Me.txtporcrecargo.TabIndex = 9
        Me.txtporcrecargo.Text_1 = Nothing
        Me.txtporcrecargo.Text_2 = Nothing
        Me.txtporcrecargo.Text_3 = Nothing
        Me.txtporcrecargo.Text_4 = Nothing
        Me.txtporcrecargo.UserValues = Nothing
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = ""
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotal.Location = New System.Drawing.Point(929, 534)
        Me.txtTotal.MaxLength = 50
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(70, 20)
        Me.txtTotal.TabIndex = 25
        Me.txtTotal.Text_1 = Nothing
        Me.txtTotal.Text_2 = Nothing
        Me.txtTotal.Text_3 = Nothing
        Me.txtTotal.Text_4 = Nothing
        Me.txtTotal.UserValues = Nothing
        '
        'txtIvaTotal
        '
        Me.txtIvaTotal.AccessibleName = ""
        Me.txtIvaTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIvaTotal.Decimals = CType(2, Byte)
        Me.txtIvaTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIvaTotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIvaTotal.Location = New System.Drawing.Point(813, 534)
        Me.txtIvaTotal.MaxLength = 50
        Me.txtIvaTotal.Name = "txtIvaTotal"
        Me.txtIvaTotal.ReadOnly = True
        Me.txtIvaTotal.Size = New System.Drawing.Size(70, 20)
        Me.txtIvaTotal.TabIndex = 24
        Me.txtIvaTotal.Text_1 = Nothing
        Me.txtIvaTotal.Text_2 = Nothing
        Me.txtIvaTotal.Text_3 = Nothing
        Me.txtIvaTotal.Text_4 = Nothing
        Me.txtIvaTotal.UserValues = Nothing
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = ""
        Me.txtSubtotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtSubtotal.Location = New System.Drawing.Point(707, 534)
        Me.txtSubtotal.MaxLength = 50
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(70, 20)
        Me.txtSubtotal.TabIndex = 23
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = "*IVA"
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtIVA.Location = New System.Drawing.Point(369, 33)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(41, 20)
        Me.txtIVA.TabIndex = 4
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(366, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 13)
        Me.Label15.TabIndex = 151
        Me.Label15.Text = "IVA*"
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
        Me.cmbEntregaren.Location = New System.Drawing.Point(202, 74)
        Me.cmbEntregaren.Name = "cmbEntregaren"
        Me.cmbEntregaren.Size = New System.Drawing.Size(179, 21)
        Me.cmbEntregaren.TabIndex = 19
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
        Me.cmbComprador.Location = New System.Drawing.Point(17, 74)
        Me.cmbComprador.Name = "cmbComprador"
        Me.cmbComprador.Size = New System.Drawing.Size(178, 21)
        Me.cmbComprador.TabIndex = 17
        '
        'PicNotas
        '
        Me.PicNotas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicNotas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicNotas.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicNotas.Location = New System.Drawing.Point(1204, 111)
        Me.PicNotas.Name = "PicNotas"
        Me.PicNotas.Size = New System.Drawing.Size(18, 20)
        Me.PicNotas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicNotas.TabIndex = 142
        Me.PicNotas.TabStop = False
        '
        'lstNotas
        '
        Me.lstNotas.CheckBoxes = True
        Me.lstNotas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nota})
        Me.lstNotas.Enabled = False
        Me.lstNotas.Location = New System.Drawing.Point(1107, 137)
        Me.lstNotas.Name = "lstNotas"
        Me.lstNotas.Size = New System.Drawing.Size(282, 391)
        Me.lstNotas.TabIndex = 24
        Me.lstNotas.UseCompatibleStateImageBehavior = False
        Me.lstNotas.View = System.Windows.Forms.View.Details
        '
        'Nota
        '
        Me.Nota.Text = "Nota"
        Me.Nota.Width = 248
        '
        'chkNotas
        '
        Me.chkNotas.AccessibleName = "Eliminado"
        Me.chkNotas.AutoSize = True
        Me.chkNotas.Location = New System.Drawing.Point(1107, 114)
        Me.chkNotas.Name = "chkNotas"
        Me.chkNotas.Size = New System.Drawing.Size(91, 17)
        Me.chkNotas.TabIndex = 23
        Me.chkNotas.Text = "Incluye Notas"
        Me.chkNotas.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(660, 538)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 134
        Me.Label9.Text = "Subtotal"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(889, 538)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 128
        Me.Label13.Text = "Total "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(783, 538)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 126
        Me.Label11.Text = "IVA"
        '
        'grdItems
        '
        Me.grdItems.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(13, 114)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1088, 414)
        Me.grdItems.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(413, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "Cliente*"
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
        Me.txtCODIGO.Location = New System.Drawing.Point(13, 32)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(83, 20)
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
        Me.Label2.Location = New System.Drawing.Point(10, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro Consumo"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(102, 32)
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
        Me.Label3.Location = New System.Drawing.Point(102, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*CLIENTE"
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.DropDownHeight = 300
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(416, 32)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(244, 21)
        Me.cmbCliente.TabIndex = 5
        '
        'PicClientes
        '
        Me.PicClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicClientes.Image = Global.PORKYS.My.Resources.Resources.Info
        Me.PicClientes.Location = New System.Drawing.Point(666, 32)
        Me.PicClientes.Name = "PicClientes"
        Me.PicClientes.Size = New System.Drawing.Size(18, 20)
        Me.PicClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicClientes.TabIndex = 105
        Me.PicClientes.TabStop = False
        '
        'chkEntrega
        '
        Me.chkEntrega.Location = New System.Drawing.Point(199, 53)
        Me.chkEntrega.Name = "chkEntrega"
        Me.chkEntrega.Size = New System.Drawing.Size(100, 23)
        Me.chkEntrega.TabIndex = 18
        Me.chkEntrega.Text = "Entregar en..."
        Me.chkEntrega.TextColor = System.Drawing.Color.Blue
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(1228, 95)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(71, 17)
        Me.chkEliminado.TabIndex = 20
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'chkRetiradopor
        '
        Me.chkRetiradopor.Location = New System.Drawing.Point(13, 53)
        Me.chkRetiradopor.Name = "chkRetiradopor"
        Me.chkRetiradopor.Size = New System.Drawing.Size(89, 23)
        Me.chkRetiradopor.TabIndex = 16
        Me.chkRetiradopor.Text = "Retirado por"
        Me.chkRetiradopor.TextColor = System.Drawing.Color.Blue
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1247, 71)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(77, 20)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'chkOcultarGanancia
        '
        Me.chkOcultarGanancia.Location = New System.Drawing.Point(640, 93)
        Me.chkOcultarGanancia.Name = "chkOcultarGanancia"
        Me.chkOcultarGanancia.Size = New System.Drawing.Size(114, 23)
        Me.chkOcultarGanancia.TabIndex = 22
        Me.chkOcultarGanancia.Text = "Ocultar PL y GC"
        Me.chkOcultarGanancia.TextColor = System.Drawing.Color.Maroon
        '
        'picGanancia
        '
        Me.picGanancia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picGanancia.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picGanancia.Image = Global.PORKYS.My.Resources.Resources.icono_ayuda
        Me.picGanancia.Location = New System.Drawing.Point(754, 95)
        Me.picGanancia.Name = "picGanancia"
        Me.picGanancia.Size = New System.Drawing.Size(18, 20)
        Me.picGanancia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGanancia.TabIndex = 178
        Me.picGanancia.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(806, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 13)
        Me.Label4.TabIndex = 181
        Me.Label4.Text = "%"
        Me.Label4.Visible = False
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
        'frmConsumos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1348, 636)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmConsumos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicNotas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGanancia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub










    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbEntregaren As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComprador As System.Windows.Forms.ComboBox
    Friend WithEvents PicNotas As System.Windows.Forms.PictureBox
    Friend WithEvents lstNotas As System.Windows.Forms.ListView
    Friend WithEvents Nota As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkNotas As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents PicClientes As System.Windows.Forms.PictureBox
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIvaTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents chkEntrega As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtporcrecargo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtNotaGestion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbVendedor As System.Windows.Forms.ComboBox
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmbUnidadVenta As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents chkRetiradopor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkOcultarGanancia As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents picGanancia As System.Windows.Forms.PictureBox
    Friend WithEvents chkDesc As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkRecargo As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkRecDescGlobal As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents rdVenta As System.Windows.Forms.RadioButton
    Friend WithEvents rdConsumoInterno As System.Windows.Forms.RadioButton
    Friend WithEvents txtFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkFactura As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkOC As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents ChkPago As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkCerrar As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cmbCondIVA As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtIdFactura As TextBoxConFormatoVB.FormattedTextBoxVB

End Class
