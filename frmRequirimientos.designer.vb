<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequirimientos

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
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lblMoneda = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtIdPresupuesto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtReferencia = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.cmbPresupuestos = New System.Windows.Forms.ComboBox()
        Me.lblPorcentajeGanancia = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblMontoGanancia = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblTotalPresup = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblTotalGastos = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtOtrosGastos = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMontoSeg = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMontoMAT = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtMontoMO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtMontoCotizacion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtHsMO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtHsCotizacion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPersonal = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblCantDias = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFechaEntrega = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFechaSolicitud = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbClientes = New System.Windows.Forms.ComboBox()
        Me.chkEliminados = New System.Windows.Forms.CheckBox()
        Me.txtNroReq = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSolicitante = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkPresupuestos = New System.Windows.Forms.CheckBox()
        Me.lblMOPresup = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblMatPresup = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtPersonal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.lblMatPresup)
        Me.GroupBox1.Controls.Add(Me.lblMOPresup)
        Me.GroupBox1.Controls.Add(Me.lblMoneda)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtIdPresupuesto)
        Me.GroupBox1.Controls.Add(Me.txtReferencia)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.cmbPresupuestos)
        Me.GroupBox1.Controls.Add(Me.lblPorcentajeGanancia)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.lblMontoGanancia)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.lblTotalPresup)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.lblTotalGastos)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtOtrosGastos)
        Me.GroupBox1.Controls.Add(Me.txtMontoSeg)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtMontoMAT)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtMontoMO)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtMontoCotizacion)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtHsMO)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtHsCotizacion)
        Me.GroupBox1.Controls.Add(Me.txtPersonal)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblCantDias)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dtpFechaEntrega)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpFechaSolicitud)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbClientes)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.txtNroReq)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSolicitante)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.chkPresupuestos)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1346, 169)
        '
        '
        '
        Me.GroupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupBox1.Style.BackColorGradientAngle = 90
        Me.GroupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupBox1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderBottomWidth = 1
        Me.GroupBox1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupBox1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderLeftWidth = 1
        Me.GroupBox1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderRightWidth = 1
        Me.GroupBox1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBox1.Style.BorderTopWidth = 1
        Me.GroupBox1.Style.CornerDiameter = 4
        Me.GroupBox1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupBox1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupBox1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupBox1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupBox1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupBox1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupBox1.TabIndex = 0
        '
        'lblMoneda
        '
        Me.lblMoneda.BackColor = System.Drawing.Color.Transparent
        Me.lblMoneda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda.ForeColor = System.Drawing.Color.Blue
        Me.lblMoneda.Location = New System.Drawing.Point(365, 71)
        Me.lblMoneda.Name = "lblMoneda"
        Me.lblMoneda.Size = New System.Drawing.Size(45, 20)
        Me.lblMoneda.TabIndex = 298
        Me.lblMoneda.Text = "Pe"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.ForeColor = System.Drawing.Color.Blue
        Me.Label21.Location = New System.Drawing.Point(362, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(46, 13)
        Me.Label21.TabIndex = 299
        Me.Label21.Text = "Moneda"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(1131, 54)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 13)
        Me.Label15.TabIndex = 279
        Me.Label15.Text = "Monto Seguridad"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.ForeColor = System.Drawing.Color.Blue
        Me.Label23.Location = New System.Drawing.Point(1031, 55)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 13)
        Me.Label23.TabIndex = 297
        Me.Label23.Text = "Materiales Presup."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.ForeColor = System.Drawing.Color.Blue
        Me.Label19.Location = New System.Drawing.Point(832, 54)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(99, 13)
        Me.Label19.TabIndex = 295
        Me.Label19.Text = "Mano Obra Presup."
        '
        'txtIdPresupuesto
        '
        Me.txtIdPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdPresupuesto.Decimals = CType(2, Byte)
        Me.txtIdPresupuesto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdPresupuesto.Enabled = False
        Me.txtIdPresupuesto.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdPresupuesto.Location = New System.Drawing.Point(1202, 45)
        Me.txtIdPresupuesto.MaxLength = 8
        Me.txtIdPresupuesto.Name = "txtIdPresupuesto"
        Me.txtIdPresupuesto.Size = New System.Drawing.Size(16, 20)
        Me.txtIdPresupuesto.TabIndex = 293
        Me.txtIdPresupuesto.Text_1 = Nothing
        Me.txtIdPresupuesto.Text_2 = Nothing
        Me.txtIdPresupuesto.Text_3 = Nothing
        Me.txtIdPresupuesto.Text_4 = Nothing
        Me.txtIdPresupuesto.UserValues = Nothing
        Me.txtIdPresupuesto.Visible = False
        '
        'txtReferencia
        '
        Me.txtReferencia.Decimals = CType(2, Byte)
        Me.txtReferencia.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtReferencia.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtReferencia.Location = New System.Drawing.Point(461, 25)
        Me.txtReferencia.MaxLength = 100
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Size = New System.Drawing.Size(280, 20)
        Me.txtReferencia.TabIndex = 291
        Me.txtReferencia.Text_1 = Nothing
        Me.txtReferencia.Text_2 = Nothing
        Me.txtReferencia.Text_3 = Nothing
        Me.txtReferencia.Text_4 = Nothing
        Me.txtReferencia.UserValues = Nothing
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.ForeColor = System.Drawing.Color.Blue
        Me.Label26.Location = New System.Drawing.Point(458, 9)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(59, 13)
        Me.Label26.TabIndex = 292
        Me.Label26.Text = "Referencia"
        '
        'cmbPresupuestos
        '
        Me.cmbPresupuestos.AccessibleName = ""
        Me.cmbPresupuestos.DropDownHeight = 500
        Me.cmbPresupuestos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPresupuestos.Enabled = False
        Me.cmbPresupuestos.FormattingEnabled = True
        Me.cmbPresupuestos.IntegralHeight = False
        Me.cmbPresupuestos.Location = New System.Drawing.Point(3, 71)
        Me.cmbPresupuestos.Name = "cmbPresupuestos"
        Me.cmbPresupuestos.Size = New System.Drawing.Size(356, 21)
        Me.cmbPresupuestos.TabIndex = 14
        '
        'lblPorcentajeGanancia
        '
        Me.lblPorcentajeGanancia.BackColor = System.Drawing.Color.Transparent
        Me.lblPorcentajeGanancia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPorcentajeGanancia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPorcentajeGanancia.ForeColor = System.Drawing.Color.Blue
        Me.lblPorcentajeGanancia.Location = New System.Drawing.Point(769, 132)
        Me.lblPorcentajeGanancia.Name = "lblPorcentajeGanancia"
        Me.lblPorcentajeGanancia.Size = New System.Drawing.Size(83, 20)
        Me.lblPorcentajeGanancia.TabIndex = 18
        Me.lblPorcentajeGanancia.Text = "0"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.ForeColor = System.Drawing.Color.Blue
        Me.Label24.Location = New System.Drawing.Point(766, 116)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(86, 13)
        Me.Label24.TabIndex = 288
        Me.Label24.Text = "Ganancia Aprox."
        '
        'lblMontoGanancia
        '
        Me.lblMontoGanancia.BackColor = System.Drawing.Color.Transparent
        Me.lblMontoGanancia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMontoGanancia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoGanancia.ForeColor = System.Drawing.Color.Blue
        Me.lblMontoGanancia.Location = New System.Drawing.Point(671, 132)
        Me.lblMontoGanancia.Name = "lblMontoGanancia"
        Me.lblMontoGanancia.Size = New System.Drawing.Size(83, 20)
        Me.lblMontoGanancia.TabIndex = 17
        Me.lblMontoGanancia.Text = "0"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.ForeColor = System.Drawing.Color.Blue
        Me.Label22.Location = New System.Drawing.Point(668, 116)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(86, 13)
        Me.Label22.TabIndex = 286
        Me.Label22.Text = "Ganancia Aprox."
        '
        'lblTotalPresup
        '
        Me.lblTotalPresup.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPresup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalPresup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPresup.ForeColor = System.Drawing.Color.Blue
        Me.lblTotalPresup.Location = New System.Drawing.Point(535, 132)
        Me.lblTotalPresup.Name = "lblTotalPresup"
        Me.lblTotalPresup.Size = New System.Drawing.Size(102, 20)
        Me.lblTotalPresup.TabIndex = 16
        Me.lblTotalPresup.Text = "0"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.ForeColor = System.Drawing.Color.Blue
        Me.Label20.Location = New System.Drawing.Point(532, 116)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 13)
        Me.Label20.TabIndex = 284
        Me.Label20.Text = "Total Presupuestado"
        '
        'lblTotalGastos
        '
        Me.lblTotalGastos.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalGastos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalGastos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalGastos.ForeColor = System.Drawing.Color.Blue
        Me.lblTotalGastos.Location = New System.Drawing.Point(419, 132)
        Me.lblTotalGastos.Name = "lblTotalGastos"
        Me.lblTotalGastos.Size = New System.Drawing.Size(102, 20)
        Me.lblTotalGastos.TabIndex = 15
        Me.lblTotalGastos.Text = "0"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.ForeColor = System.Drawing.Color.Blue
        Me.Label18.Location = New System.Drawing.Point(416, 116)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 13)
        Me.Label18.TabIndex = 282
        Me.Label18.Text = "Total Gastos"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(1225, 54)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(68, 13)
        Me.Label16.TabIndex = 281
        Me.Label16.Text = "Otros Gastos"
        '
        'txtOtrosGastos
        '
        Me.txtOtrosGastos.AccessibleName = ""
        Me.txtOtrosGastos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOtrosGastos.Decimals = CType(2, Byte)
        Me.txtOtrosGastos.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtOtrosGastos.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtOtrosGastos.Location = New System.Drawing.Point(1228, 72)
        Me.txtOtrosGastos.MaxLength = 11
        Me.txtOtrosGastos.Name = "txtOtrosGastos"
        Me.txtOtrosGastos.Size = New System.Drawing.Size(83, 20)
        Me.txtOtrosGastos.TabIndex = 13
        Me.txtOtrosGastos.Text_1 = Nothing
        Me.txtOtrosGastos.Text_2 = Nothing
        Me.txtOtrosGastos.Text_3 = Nothing
        Me.txtOtrosGastos.Text_4 = Nothing
        Me.txtOtrosGastos.UserValues = Nothing
        '
        'txtMontoSeg
        '
        Me.txtMontoSeg.AccessibleName = ""
        Me.txtMontoSeg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoSeg.Decimals = CType(2, Byte)
        Me.txtMontoSeg.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoSeg.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoSeg.Location = New System.Drawing.Point(1134, 72)
        Me.txtMontoSeg.MaxLength = 11
        Me.txtMontoSeg.Name = "txtMontoSeg"
        Me.txtMontoSeg.Size = New System.Drawing.Size(83, 20)
        Me.txtMontoSeg.TabIndex = 12
        Me.txtMontoSeg.Text_1 = Nothing
        Me.txtMontoSeg.Text_2 = Nothing
        Me.txtMontoSeg.Text_3 = Nothing
        Me.txtMontoSeg.Text_4 = Nothing
        Me.txtMontoSeg.UserValues = Nothing
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(937, 54)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 13)
        Me.Label14.TabIndex = 277
        Me.Label14.Text = "Monto Materiales"
        '
        'txtMontoMAT
        '
        Me.txtMontoMAT.AccessibleName = ""
        Me.txtMontoMAT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoMAT.Decimals = CType(2, Byte)
        Me.txtMontoMAT.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoMAT.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoMAT.Location = New System.Drawing.Point(940, 72)
        Me.txtMontoMAT.MaxLength = 11
        Me.txtMontoMAT.Name = "txtMontoMAT"
        Me.txtMontoMAT.Size = New System.Drawing.Size(83, 20)
        Me.txtMontoMAT.TabIndex = 11
        Me.txtMontoMAT.Text_1 = Nothing
        Me.txtMontoMAT.Text_2 = Nothing
        Me.txtMontoMAT.Text_3 = Nothing
        Me.txtMontoMAT.Text_4 = Nothing
        Me.txtMontoMAT.UserValues = Nothing
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(744, 54)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 13)
        Me.Label13.TabIndex = 275
        Me.Label13.Text = "Mano Obra"
        '
        'txtMontoMO
        '
        Me.txtMontoMO.AccessibleName = ""
        Me.txtMontoMO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoMO.Decimals = CType(2, Byte)
        Me.txtMontoMO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoMO.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoMO.Location = New System.Drawing.Point(746, 72)
        Me.txtMontoMO.MaxLength = 11
        Me.txtMontoMO.Name = "txtMontoMO"
        Me.txtMontoMO.Size = New System.Drawing.Size(83, 20)
        Me.txtMontoMO.TabIndex = 10
        Me.txtMontoMO.Text_1 = Nothing
        Me.txtMontoMO.Text_2 = Nothing
        Me.txtMontoMO.Text_3 = Nothing
        Me.txtMontoMO.Text_4 = Nothing
        Me.txtMontoMO.UserValues = Nothing
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(488, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 13)
        Me.Label11.TabIndex = 273
        Me.Label11.Text = "Monto x Cotización"
        '
        'txtMontoCotizacion
        '
        Me.txtMontoCotizacion.AccessibleName = ""
        Me.txtMontoCotizacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoCotizacion.Decimals = CType(2, Byte)
        Me.txtMontoCotizacion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoCotizacion.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoCotizacion.Location = New System.Drawing.Point(491, 72)
        Me.txtMontoCotizacion.MaxLength = 11
        Me.txtMontoCotizacion.Name = "txtMontoCotizacion"
        Me.txtMontoCotizacion.Size = New System.Drawing.Size(83, 20)
        Me.txtMontoCotizacion.TabIndex = 7
        Me.txtMontoCotizacion.Text_1 = Nothing
        Me.txtMontoCotizacion.Text_2 = Nothing
        Me.txtMontoCotizacion.Text_3 = Nothing
        Me.txtMontoCotizacion.Text_4 = Nothing
        Me.txtMontoCotizacion.UserValues = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(691, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 13)
        Me.Label10.TabIndex = 271
        Me.Label10.Text = "Hs MO"
        '
        'txtHsMO
        '
        Me.txtHsMO.AccessibleName = ""
        Me.txtHsMO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHsMO.Decimals = CType(2, Byte)
        Me.txtHsMO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtHsMO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtHsMO.Location = New System.Drawing.Point(694, 71)
        Me.txtHsMO.MaxLength = 11
        Me.txtHsMO.Name = "txtHsMO"
        Me.txtHsMO.Size = New System.Drawing.Size(46, 20)
        Me.txtHsMO.TabIndex = 9
        Me.txtHsMO.Text_1 = Nothing
        Me.txtHsMO.Text_2 = Nothing
        Me.txtHsMO.Text_3 = Nothing
        Me.txtHsMO.Text_4 = Nothing
        Me.txtHsMO.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(413, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 269
        Me.Label9.Text = "Hs Cotización"
        '
        'txtHsCotizacion
        '
        Me.txtHsCotizacion.AccessibleName = ""
        Me.txtHsCotizacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHsCotizacion.Decimals = CType(2, Byte)
        Me.txtHsCotizacion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtHsCotizacion.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtHsCotizacion.Location = New System.Drawing.Point(416, 71)
        Me.txtHsCotizacion.MaxLength = 11
        Me.txtHsCotizacion.Name = "txtHsCotizacion"
        Me.txtHsCotizacion.Size = New System.Drawing.Size(69, 20)
        Me.txtHsCotizacion.TabIndex = 6
        Me.txtHsCotizacion.Text_1 = Nothing
        Me.txtHsCotizacion.Text_2 = Nothing
        Me.txtHsCotizacion.Text_3 = Nothing
        Me.txtHsCotizacion.Text_4 = Nothing
        Me.txtHsCotizacion.UserValues = Nothing
        '
        'txtPersonal
        '
        Me.txtPersonal.Location = New System.Drawing.Point(594, 72)
        Me.txtPersonal.Name = "txtPersonal"
        Me.txtPersonal.Size = New System.Drawing.Size(52, 20)
        Me.txtPersonal.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(591, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 13)
        Me.Label7.TabIndex = 266
        Me.Label7.Text = "Personal Afectado"
        '
        'lblCantDias
        '
        Me.lblCantDias.BackColor = System.Drawing.Color.Transparent
        Me.lblCantDias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCantDias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantDias.ForeColor = System.Drawing.Color.Blue
        Me.lblCantDias.Location = New System.Drawing.Point(959, 25)
        Me.lblCantDias.Name = "lblCantDias"
        Me.lblCantDias.Size = New System.Drawing.Size(47, 20)
        Me.lblCantDias.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(956, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 264
        Me.Label6.Text = "Cant Días"
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(280, 9)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(16, 20)
        Me.txtIdCliente.TabIndex = 262
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(840, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 13)
        Me.Label5.TabIndex = 261
        Me.Label5.Text = "Fecha Plazo Entrega"
        '
        'dtpFechaEntrega
        '
        Me.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaEntrega.Location = New System.Drawing.Point(843, 25)
        Me.dtpFechaEntrega.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFechaEntrega.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFechaEntrega.Name = "dtpFechaEntrega"
        Me.dtpFechaEntrega.Size = New System.Drawing.Size(90, 20)
        Me.dtpFechaEntrega.TabIndex = 3
        Me.dtpFechaEntrega.Tag = "202"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(744, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 259
        Me.Label4.Text = "Fecha Solicitud"
        '
        'dtpFechaSolicitud
        '
        Me.dtpFechaSolicitud.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaSolicitud.Location = New System.Drawing.Point(747, 25)
        Me.dtpFechaSolicitud.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFechaSolicitud.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFechaSolicitud.Name = "dtpFechaSolicitud"
        Me.dtpFechaSolicitud.Size = New System.Drawing.Size(90, 20)
        Me.dtpFechaSolicitud.TabIndex = 2
        Me.dtpFechaSolicitud.Tag = "202"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(362, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 257
        Me.Label8.Text = "Nro Req."
        '
        'cmbClientes
        '
        Me.cmbClientes.AccessibleName = ""
        Me.cmbClientes.DropDownHeight = 500
        Me.cmbClientes.FormattingEnabled = True
        Me.cmbClientes.IntegralHeight = False
        Me.cmbClientes.Location = New System.Drawing.Point(66, 25)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(293, 21)
        Me.cmbClientes.TabIndex = 0
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.ForeColor = System.Drawing.Color.Red
        Me.chkEliminados.Location = New System.Drawing.Point(1228, 132)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(109, 17)
        Me.chkEliminados.TabIndex = 19
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.UseVisualStyleBackColor = False
        '
        'txtNroReq
        '
        Me.txtNroReq.AccessibleName = ""
        Me.txtNroReq.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroReq.Decimals = CType(2, Byte)
        Me.txtNroReq.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroReq.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroReq.Location = New System.Drawing.Point(365, 25)
        Me.txtNroReq.MaxLength = 11
        Me.txtNroReq.Name = "txtNroReq"
        Me.txtNroReq.Size = New System.Drawing.Size(90, 20)
        Me.txtNroReq.TabIndex = 1
        Me.txtNroReq.Text_1 = Nothing
        Me.txtNroReq.Text_2 = Nothing
        Me.txtNroReq.Text_3 = Nothing
        Me.txtNroReq.Text_4 = Nothing
        Me.txtNroReq.UserValues = Nothing
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1116, -1)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(16, 20)
        Me.txtID.TabIndex = 238
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
        Me.Label1.Location = New System.Drawing.Point(1094, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 239
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = ""
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Enabled = False
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(3, 25)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(54, 20)
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
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(3, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 240
        Me.Label2.Text = "Código"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(63, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 241
        Me.Label3.Text = "Cliente*"
        '
        'txtSolicitante
        '
        Me.txtSolicitante.Decimals = CType(2, Byte)
        Me.txtSolicitante.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSolicitante.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtSolicitante.Location = New System.Drawing.Point(1018, 25)
        Me.txtSolicitante.MaxLength = 100
        Me.txtSolicitante.Name = "txtSolicitante"
        Me.txtSolicitante.Size = New System.Drawing.Size(280, 20)
        Me.txtSolicitante.TabIndex = 5
        Me.txtSolicitante.Text_1 = Nothing
        Me.txtSolicitante.Text_2 = Nothing
        Me.txtSolicitante.Text_3 = Nothing
        Me.txtSolicitante.Text_4 = Nothing
        Me.txtSolicitante.UserValues = Nothing
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(1015, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 13)
        Me.Label12.TabIndex = 249
        Me.Label12.Text = "Solicitante"
        '
        'chkPresupuestos
        '
        Me.chkPresupuestos.AutoSize = True
        Me.chkPresupuestos.BackColor = System.Drawing.Color.Transparent
        Me.chkPresupuestos.ForeColor = System.Drawing.Color.Blue
        Me.chkPresupuestos.Location = New System.Drawing.Point(3, 55)
        Me.chkPresupuestos.Name = "chkPresupuestos"
        Me.chkPresupuestos.Size = New System.Drawing.Size(132, 17)
        Me.chkPresupuestos.TabIndex = 300
        Me.chkPresupuestos.Text = "Asociar a Presupuesto"
        Me.chkPresupuestos.UseVisualStyleBackColor = False
        '
        'lblMOPresup
        '
        Me.lblMOPresup.AccessibleName = ""
        Me.lblMOPresup.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMOPresup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.lblMOPresup.Decimals = CType(2, Byte)
        Me.lblMOPresup.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.lblMOPresup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMOPresup.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.lblMOPresup.Location = New System.Drawing.Point(835, 72)
        Me.lblMOPresup.MaxLength = 11
        Me.lblMOPresup.Name = "lblMOPresup"
        Me.lblMOPresup.ReadOnly = True
        Me.lblMOPresup.Size = New System.Drawing.Size(91, 21)
        Me.lblMOPresup.TabIndex = 301
        Me.lblMOPresup.Text_1 = Nothing
        Me.lblMOPresup.Text_2 = Nothing
        Me.lblMOPresup.Text_3 = Nothing
        Me.lblMOPresup.Text_4 = Nothing
        Me.lblMOPresup.UserValues = Nothing
        '
        'lblMatPresup
        '
        Me.lblMatPresup.AccessibleName = ""
        Me.lblMatPresup.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMatPresup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.lblMatPresup.Decimals = CType(2, Byte)
        Me.lblMatPresup.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.lblMatPresup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMatPresup.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.lblMatPresup.Location = New System.Drawing.Point(1034, 71)
        Me.lblMatPresup.MaxLength = 11
        Me.lblMatPresup.Name = "lblMatPresup"
        Me.lblMatPresup.ReadOnly = True
        Me.lblMatPresup.Size = New System.Drawing.Size(91, 21)
        Me.lblMatPresup.TabIndex = 302
        Me.lblMatPresup.Text_1 = Nothing
        Me.lblMatPresup.Text_2 = Nothing
        Me.lblMatPresup.Text_3 = Nothing
        Me.lblMatPresup.Text_4 = Nothing
        Me.lblMatPresup.UserValues = Nothing
        '
        'frmRequirimientos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 434)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmRequirimientos"
        Me.Text = "Requerimientos de Presupuestos"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtPersonal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub















    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSolicitante As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkEliminados As System.Windows.Forms.CheckBox
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaSolicitud As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaEntrega As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblCantDias As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtMontoMO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtMontoCotizacion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtHsMO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtHsCotizacion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPersonal As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtOtrosGastos As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtMontoSeg As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMontoMAT As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblPorcentajeGanancia As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblMontoGanancia As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPresup As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblTotalGastos As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtReferencia As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cmbPresupuestos As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdPresupuesto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroReq As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblMoneda As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents chkPresupuestos As System.Windows.Forms.CheckBox
    Friend WithEvents lblMatPresup As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblMOPresup As TextBoxConFormatoVB.FormattedTextBoxVB

End Class
