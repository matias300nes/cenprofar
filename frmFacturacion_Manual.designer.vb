<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFacturacion_Manual

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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtValorCambio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbTipoComprobante = New System.Windows.Forms.ComboBox()
        Me.txtPtoVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.chkFacturaAnulada = New System.Windows.Forms.CheckBox()
        Me.lblNroPosible = New System.Windows.Forms.Label()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtComprobante = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbCondIVA = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbCondVTA = New System.Windows.Forms.ComboBox()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFactura = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtMontoIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtValorCambio)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.cmbTipoComprobante)
        Me.GroupBox1.Controls.Add(Me.txtPtoVta)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.chkFacturaAnulada)
        Me.GroupBox1.Controls.Add(Me.lblNroPosible)
        Me.GroupBox1.Controls.Add(Me.btnAnular)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtComprobante)
        Me.GroupBox1.Controls.Add(Me.cmbCondIVA)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cmbCondVTA)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtFactura)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtMontoIVA)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1346, 153)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(802, 102)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(333, 13)
        Me.Label14.TabIndex = 217
        Me.Label14.Text = "Si la factura NO tiene items dolarizados, el valor de cambio es 1 (uno)"
        '
        'txtValorCambio
        '
        Me.txtValorCambio.AccessibleName = "*ValorCambio"
        Me.txtValorCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValorCambio.Decimals = CType(2, Byte)
        Me.txtValorCambio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtValorCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorCambio.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtValorCambio.Location = New System.Drawing.Point(805, 79)
        Me.txtValorCambio.Name = "txtValorCambio"
        Me.txtValorCambio.Size = New System.Drawing.Size(70, 20)
        Me.txtValorCambio.TabIndex = 9
        Me.txtValorCambio.Text_1 = Nothing
        Me.txtValorCambio.Text_2 = Nothing
        Me.txtValorCambio.Text_3 = Nothing
        Me.txtValorCambio.Text_4 = Nothing
        Me.txtValorCambio.UserValues = Nothing
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(802, 63)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 216
        Me.Label13.Text = "Valor Cambio*"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(114, 63)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 13)
        Me.Label20.TabIndex = 214
        Me.Label20.Text = "Tipo Factura"
        '
        'cmbTipoComprobante
        '
        Me.cmbTipoComprobante.AccessibleName = ""
        Me.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoComprobante.FormattingEnabled = True
        Me.cmbTipoComprobante.Items.AddRange(New Object() {"Cta Cte", "Contado/Efectivo"})
        Me.cmbTipoComprobante.Location = New System.Drawing.Point(117, 78)
        Me.cmbTipoComprobante.Name = "cmbTipoComprobante"
        Me.cmbTipoComprobante.Size = New System.Drawing.Size(133, 21)
        Me.cmbTipoComprobante.TabIndex = 3
        '
        'txtPtoVta
        '
        Me.txtPtoVta.AccessibleName = ""
        Me.txtPtoVta.BackColor = System.Drawing.SystemColors.Window
        Me.txtPtoVta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPtoVta.Decimals = CType(2, Byte)
        Me.txtPtoVta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPtoVta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPtoVta.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtPtoVta.Location = New System.Drawing.Point(256, 79)
        Me.txtPtoVta.MaxLength = 25
        Me.txtPtoVta.Name = "txtPtoVta"
        Me.txtPtoVta.ReadOnly = True
        Me.txtPtoVta.Size = New System.Drawing.Size(39, 20)
        Me.txtPtoVta.TabIndex = 4
        Me.txtPtoVta.Text = "2"
        Me.txtPtoVta.Text_1 = Nothing
        Me.txtPtoVta.Text_2 = Nothing
        Me.txtPtoVta.Text_3 = Nothing
        Me.txtPtoVta.Text_4 = Nothing
        Me.txtPtoVta.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(253, 63)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(42, 13)
        Me.Label18.TabIndex = 213
        Me.Label18.Text = "Pto Vta"
        '
        'chkFacturaAnulada
        '
        Me.chkFacturaAnulada.AutoSize = True
        Me.chkFacturaAnulada.Location = New System.Drawing.Point(605, 19)
        Me.chkFacturaAnulada.Name = "chkFacturaAnulada"
        Me.chkFacturaAnulada.Size = New System.Drawing.Size(101, 17)
        Me.chkFacturaAnulada.TabIndex = 198
        Me.chkFacturaAnulada.Text = "FacturaAnulada"
        Me.chkFacturaAnulada.UseVisualStyleBackColor = True
        Me.chkFacturaAnulada.Visible = False
        '
        'lblNroPosible
        '
        Me.lblNroPosible.Location = New System.Drawing.Point(897, 41)
        Me.lblNroPosible.Name = "lblNroPosible"
        Me.lblNroPosible.Size = New System.Drawing.Size(67, 13)
        Me.lblNroPosible.TabIndex = 197
        Me.lblNroPosible.Visible = False
        '
        'btnAnular
        '
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.Location = New System.Drawing.Point(388, 31)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(144, 23)
        Me.btnAnular.TabIndex = 14
        Me.btnAnular.Text = "Anular Factura Física"
        Me.btnAnular.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(406, 63)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(114, 13)
        Me.Label12.TabIndex = 195
        Me.Label12.Text = "Nro Comprobante (OC)"
        '
        'txtComprobante
        '
        Me.txtComprobante.AccessibleName = ""
        Me.txtComprobante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComprobante.Decimals = CType(2, Byte)
        Me.txtComprobante.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtComprobante.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComprobante.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtComprobante.Location = New System.Drawing.Point(409, 79)
        Me.txtComprobante.Name = "txtComprobante"
        Me.txtComprobante.Size = New System.Drawing.Size(150, 20)
        Me.txtComprobante.TabIndex = 6
        Me.txtComprobante.Text_1 = Nothing
        Me.txtComprobante.Text_2 = Nothing
        Me.txtComprobante.Text_3 = Nothing
        Me.txtComprobante.Text_4 = Nothing
        Me.txtComprobante.UserValues = Nothing
        '
        'cmbCondIVA
        '
        Me.cmbCondIVA.AccessibleName = "*CONDICIÓN_IVA"
        Me.cmbCondIVA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondIVA.FormattingEnabled = True
        Me.cmbCondIVA.Items.AddRange(New Object() {"RI"})
        Me.cmbCondIVA.Location = New System.Drawing.Point(688, 79)
        Me.cmbCondIVA.Name = "cmbCondIVA"
        Me.cmbCondIVA.Size = New System.Drawing.Size(111, 21)
        Me.cmbCondIVA.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(685, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 13)
        Me.Label10.TabIndex = 167
        Me.Label10.Text = "Condición IVA*"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(562, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 166
        Me.Label11.Text = "Condición Venta*"
        '
        'cmbCondVTA
        '
        Me.cmbCondVTA.AccessibleName = "*CONDICIÓN_VENTA"
        Me.cmbCondVTA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondVTA.FormattingEnabled = True
        Me.cmbCondVTA.Items.AddRange(New Object() {"Cta Cte", "Contado/Efectivo"})
        Me.cmbCondVTA.Location = New System.Drawing.Point(565, 79)
        Me.cmbCondVTA.Name = "cmbCondVTA"
        Me.cmbCondVTA.Size = New System.Drawing.Size(117, 21)
        Me.cmbCondVTA.TabIndex = 7
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(9, 79)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 2
        Me.dtpFECHA.Tag = "202"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 163
        Me.Label9.Text = "Fecha"
        '
        'txtFactura
        '
        Me.txtFactura.AccessibleName = "*NroFactura"
        Me.txtFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFactura.Decimals = CType(2, Byte)
        Me.txtFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFactura.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtFactura.Location = New System.Drawing.Point(301, 79)
        Me.txtFactura.Name = "txtFactura"
        Me.txtFactura.Size = New System.Drawing.Size(102, 20)
        Me.txtFactura.TabIndex = 5
        Me.txtFactura.Text_1 = Nothing
        Me.txtFactura.Text_2 = Nothing
        Me.txtFactura.Text_3 = Nothing
        Me.txtFactura.Text_4 = Nothing
        Me.txtFactura.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(298, 63)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 161
        Me.Label8.Text = "Nro Factura*"
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = "*Total"
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtTotal.Location = New System.Drawing.Point(1112, 78)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(87, 20)
        Me.txtTotal.TabIndex = 13
        Me.txtTotal.Text_1 = Nothing
        Me.txtTotal.Text_2 = Nothing
        Me.txtTotal.Text_3 = Nothing
        Me.txtTotal.Text_4 = Nothing
        Me.txtTotal.UserValues = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1109, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 159
        Me.Label7.Text = "Total*"
        '
        'txtMontoIVA
        '
        Me.txtMontoIVA.AccessibleName = ""
        Me.txtMontoIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoIVA.Decimals = CType(2, Byte)
        Me.txtMontoIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtMontoIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIVA.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtMontoIVA.Location = New System.Drawing.Point(1041, 79)
        Me.txtMontoIVA.Name = "txtMontoIVA"
        Me.txtMontoIVA.ReadOnly = True
        Me.txtMontoIVA.Size = New System.Drawing.Size(65, 20)
        Me.txtMontoIVA.TabIndex = 12
        Me.txtMontoIVA.Text_1 = Nothing
        Me.txtMontoIVA.Text_2 = Nothing
        Me.txtMontoIVA.Text_3 = Nothing
        Me.txtMontoIVA.Text_4 = Nothing
        Me.txtMontoIVA.UserValues = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1038, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 157
        Me.Label6.Text = "Monto IVA"
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = "*SubTotal"
        Me.txtSubtotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtSubtotal.Location = New System.Drawing.Point(881, 79)
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(87, 20)
        Me.txtSubtotal.TabIndex = 10
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(878, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 155
        Me.Label5.Text = "SubTotal [$]*"
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = "*IVA"
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.UnsignedFloatingPointNumber
        Me.txtIVA.Location = New System.Drawing.Point(974, 79)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(61, 20)
        Me.txtIVA.TabIndex = 11
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(971, 63)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(28, 13)
        Me.Label15.TabIndex = 153
        Me.Label15.Text = "IVA*"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(110, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "Cliente*"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*Cliente"
        Me.cmbCliente.DropDownHeight = 300
        Me.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(113, 33)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(243, 21)
        Me.cmbCliente.TabIndex = 1
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(828, 13)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(16, 20)
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
        Me.Label1.Location = New System.Drawing.Point(1065, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = ""
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(9, 33)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(98, 20)
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
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Código*"
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = ""
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(9, 122)
        Me.txtNota.MaxLength = 50
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(1056, 20)
        Me.txtNota.TabIndex = 14
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Observaciones"
        '
        'frmFacturacion_Manual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 434)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmFacturacion_Manual"
        Me.Text = "Facturación Manual"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub



    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label

    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMontoIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbCondIVA As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbCondVTA As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtComprobante As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents chkFacturaAnulada As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoComprobante As System.Windows.Forms.ComboBox
    Friend WithEvents txtPtoVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtValorCambio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblNroPosible As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label










End Class
