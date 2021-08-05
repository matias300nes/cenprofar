<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaqHerramientas

    Inherits SEYC.frmBase

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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtNroSerie = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkCertificado = New System.Windows.Forms.CheckBox()
        Me.txtidMarca = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PicMarcas = New System.Windows.Forms.PictureBox()
        Me.cmbMarcas = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtModelo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtidrubro = New System.Windows.Forms.TextBox()
        Me.chkEliminados = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpFechaAlta = New System.Windows.Forms.DateTimePicker()
        Me.cmbMotivoBaja = New System.Windows.Forms.ComboBox()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNOMBRE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpFechaBaja = New System.Windows.Forms.DateTimePicker()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicMarcas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(146, 26)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.txtNroSerie)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.chkCertificado)
        Me.GroupBox1.Controls.Add(Me.txtidMarca)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.PicMarcas)
        Me.GroupBox1.Controls.Add(Me.cmbMarcas)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtModelo)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtidrubro)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.dtpFechaBaja)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.dtpFechaAlta)
        Me.GroupBox1.Controls.Add(Me.cmbMotivoBaja)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtNOMBRE)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDescripcion)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1338, 331)
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
        Me.GroupBox1.TabIndex = 64
        '
        'txtNroSerie
        '
        Me.txtNroSerie.Decimals = CType(2, Byte)
        Me.txtNroSerie.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroSerie.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroSerie.Location = New System.Drawing.Point(874, 23)
        Me.txtNroSerie.MaxLength = 300
        Me.txtNroSerie.Name = "txtNroSerie"
        Me.txtNroSerie.Size = New System.Drawing.Size(191, 20)
        Me.txtNroSerie.TabIndex = 6
        Me.txtNroSerie.Text_1 = Nothing
        Me.txtNroSerie.Text_2 = Nothing
        Me.txtNroSerie.Text_3 = Nothing
        Me.txtNroSerie.Text_4 = Nothing
        Me.txtNroSerie.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(871, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 167
        Me.Label5.Text = "Nro de Serie"
        '
        'chkCertificado
        '
        Me.chkCertificado.AutoSize = True
        Me.chkCertificado.BackColor = System.Drawing.Color.Transparent
        Me.chkCertificado.ForeColor = System.Drawing.Color.Blue
        Me.chkCertificado.Location = New System.Drawing.Point(6, 64)
        Me.chkCertificado.Name = "chkCertificado"
        Me.chkCertificado.Size = New System.Drawing.Size(146, 17)
        Me.chkCertificado.TabIndex = 7
        Me.chkCertificado.Text = "Certificado de Calibración"
        Me.chkCertificado.UseVisualStyleBackColor = False
        '
        'txtidMarca
        '
        Me.txtidMarca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidMarca.Decimals = CType(2, Byte)
        Me.txtidMarca.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidMarca.Enabled = False
        Me.txtidMarca.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidMarca.Location = New System.Drawing.Point(568, 7)
        Me.txtidMarca.MaxLength = 8
        Me.txtidMarca.Name = "txtidMarca"
        Me.txtidMarca.Size = New System.Drawing.Size(16, 20)
        Me.txtidMarca.TabIndex = 165
        Me.txtidMarca.Text_1 = Nothing
        Me.txtidMarca.Text_2 = Nothing
        Me.txtidMarca.Text_3 = Nothing
        Me.txtidMarca.Text_4 = Nothing
        Me.txtidMarca.UserValues = Nothing
        Me.txtidMarca.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(453, 7)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 164
        Me.Label12.Text = "Marca"
        '
        'PicMarcas
        '
        Me.PicMarcas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicMarcas.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicMarcas.Image = Global.SEYC.My.Resources.Resources.Info
        Me.PicMarcas.Location = New System.Drawing.Point(653, 23)
        Me.PicMarcas.Name = "PicMarcas"
        Me.PicMarcas.Size = New System.Drawing.Size(18, 20)
        Me.PicMarcas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicMarcas.TabIndex = 163
        Me.PicMarcas.TabStop = False
        '
        'cmbMarcas
        '
        Me.cmbMarcas.AccessibleName = ""
        Me.cmbMarcas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbMarcas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMarcas.DropDownHeight = 300
        Me.cmbMarcas.FormattingEnabled = True
        Me.cmbMarcas.IntegralHeight = False
        Me.cmbMarcas.Location = New System.Drawing.Point(456, 23)
        Me.cmbMarcas.Name = "cmbMarcas"
        Me.cmbMarcas.Size = New System.Drawing.Size(191, 21)
        Me.cmbMarcas.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(0, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(347, 16)
        Me.Label1.TabIndex = 136
        Me.Label1.Text = "Listado de Trabajos de Mantenimiento / Ensayos"
        '
        'txtModelo
        '
        Me.txtModelo.Decimals = CType(2, Byte)
        Me.txtModelo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtModelo.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtModelo.Location = New System.Drawing.Point(677, 23)
        Me.txtModelo.MaxLength = 300
        Me.txtModelo.Name = "txtModelo"
        Me.txtModelo.Size = New System.Drawing.Size(191, 20)
        Me.txtModelo.TabIndex = 4
        Me.txtModelo.Text_1 = Nothing
        Me.txtModelo.Text_2 = Nothing
        Me.txtModelo.Text_3 = Nothing
        Me.txtModelo.Text_4 = Nothing
        Me.txtModelo.UserValues = Nothing
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(674, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 13)
        Me.Label11.TabIndex = 135
        Me.Label11.Text = "Modelo / Nro"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(1145, 308)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 13)
        Me.Label10.TabIndex = 133
        Me.Label10.Text = "Total"
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = "Nota"
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTotal.Location = New System.Drawing.Point(1187, 305)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(98, 20)
        Me.txtTotal.TabIndex = 132
        Me.txtTotal.Text_1 = Nothing
        Me.txtTotal.Text_2 = Nothing
        Me.txtTotal.Text_3 = Nothing
        Me.txtTotal.Text_4 = Nothing
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotal.UserValues = Nothing
        '
        'txtidrubro
        '
        Me.txtidrubro.Location = New System.Drawing.Point(1118, 42)
        Me.txtidrubro.Name = "txtidrubro"
        Me.txtidrubro.Size = New System.Drawing.Size(43, 20)
        Me.txtidrubro.TabIndex = 109
        Me.txtidrubro.Visible = False
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.ForeColor = System.Drawing.Color.Red
        Me.chkEliminados.Location = New System.Drawing.Point(1220, 86)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(109, 17)
        Me.chkEliminados.TabIndex = 12
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.UseVisualStyleBackColor = False
        '
        'grdItems
        '
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(3, 109)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1326, 195)
        Me.grdItems.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(619, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 104
        Me.Label9.Text = "Fecha Baja"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(81, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 102
        Me.Label8.Text = "Fecha Alta"
        '
        'dtpFechaAlta
        '
        Me.dtpFechaAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaAlta.Location = New System.Drawing.Point(84, 23)
        Me.dtpFechaAlta.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFechaAlta.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFechaAlta.Name = "dtpFechaAlta"
        Me.dtpFechaAlta.Size = New System.Drawing.Size(90, 20)
        Me.dtpFechaAlta.TabIndex = 1
        Me.dtpFechaAlta.Tag = "202"
        '
        'cmbMotivoBaja
        '
        Me.cmbMotivoBaja.FormattingEnabled = True
        Me.cmbMotivoBaja.Location = New System.Drawing.Point(718, 62)
        Me.cmbMotivoBaja.Name = "cmbMotivoBaja"
        Me.cmbMotivoBaja.Size = New System.Drawing.Size(474, 21)
        Me.cmbMotivoBaja.TabIndex = 10
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1167, 42)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(16, 20)
        Me.txtID.TabIndex = 86
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = ""
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Enabled = False
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(6, 23)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(72, 20)
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
        Me.Label2.Location = New System.Drawing.Point(3, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "Código"
        '
        'txtNOMBRE
        '
        Me.txtNOMBRE.AccessibleName = "*NOMBRE"
        Me.txtNOMBRE.Decimals = CType(2, Byte)
        Me.txtNOMBRE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNOMBRE.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNOMBRE.Location = New System.Drawing.Point(180, 23)
        Me.txtNOMBRE.MaxLength = 100
        Me.txtNOMBRE.Name = "txtNOMBRE"
        Me.txtNOMBRE.Size = New System.Drawing.Size(270, 20)
        Me.txtNOMBRE.TabIndex = 2
        Me.txtNOMBRE.Text_1 = Nothing
        Me.txtNOMBRE.Text_2 = Nothing
        Me.txtNOMBRE.Text_3 = Nothing
        Me.txtNOMBRE.Text_4 = Nothing
        Me.txtNOMBRE.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(177, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 89
        Me.Label3.Text = "Nombre*"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescripcion.Decimals = CType(2, Byte)
        Me.txtDescripcion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescripcion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtDescripcion.Location = New System.Drawing.Point(158, 62)
        Me.txtDescripcion.MaxLength = 300
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(458, 20)
        Me.txtDescripcion.TabIndex = 8
        Me.txtDescripcion.Text_1 = Nothing
        Me.txtDescripcion.Text_2 = Nothing
        Me.txtDescripcion.Text_3 = Nothing
        Me.txtDescripcion.Text_4 = Nothing
        Me.txtDescripcion.UserValues = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(155, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "Descripción"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(718, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 13)
        Me.Label7.TabIndex = 93
        Me.Label7.Text = "Motivo Baja"
        '
        'dtpFechaBaja
        '
        Me.dtpFechaBaja.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaBaja.Location = New System.Drawing.Point(622, 62)
        Me.dtpFechaBaja.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFechaBaja.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFechaBaja.Name = "dtpFechaBaja"
        Me.dtpFechaBaja.Size = New System.Drawing.Size(90, 20)
        Me.dtpFechaBaja.TabIndex = 9
        Me.dtpFechaBaja.Tag = "202"
        '
        'frmMaqHerramientas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 434)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmMaqHerramientas"
        Me.Text = "Maquinas y Herramientas"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicMarcas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub















    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cmbMotivoBaja As System.Windows.Forms.ComboBox
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNOMBRE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents chkEliminados As System.Windows.Forms.CheckBox
    Friend WithEvents txtidrubro As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtModelo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PicMarcas As System.Windows.Forms.PictureBox
    Friend WithEvents cmbMarcas As System.Windows.Forms.ComboBox
    Friend WithEvents txtidMarca As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkCertificado As System.Windows.Forms.CheckBox
    Friend WithEvents txtNroSerie As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaBaja As System.Windows.Forms.DateTimePicker

End Class
