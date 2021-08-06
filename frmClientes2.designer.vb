<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClientes2

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
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ImportExcel = New System.Windows.Forms.Button()
        Me.FileName = New System.Windows.Forms.TextBox()
        Me.lblContaNom = New System.Windows.Forms.Label()
        Me.chkCredito = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDiasMax = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMontoMax = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.PicExcelExportar = New System.Windows.Forms.PictureBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbCondicionIVA = New System.Windows.Forms.ComboBox()
        Me.chkPromo = New System.Windows.Forms.CheckBox()
        Me.cmbRepartidor = New System.Windows.Forms.ComboBox()
        Me.lblRepartidor = New System.Windows.Forms.Label()
        Me.chkUsuarioWEB = New System.Windows.Forms.CheckBox()
        Me.lblUsuarioPP0 = New System.Windows.Forms.Label()
        Me.txtusuario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbListaPrecio = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbDocTipo = New System.Windows.Forms.ComboBox()
        Me.txtCuit = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkEliminados = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkCtaCte = New System.Windows.Forms.CheckBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.cmbLocalidad = New System.Windows.Forms.ComboBox()
        Me.cmbProvincia = New System.Windows.Forms.ComboBox()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNOMBRE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDIRECCION = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCODPOSTAL = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTELEFONO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFAX = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicExcelExportar, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.ImportExcel)
        Me.GroupBox1.Controls.Add(Me.FileName)
        Me.GroupBox1.Controls.Add(Me.lblContaNom)
        Me.GroupBox1.Controls.Add(Me.chkCredito)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtDiasMax)
        Me.GroupBox1.Controls.Add(Me.txtMontoMax)
        Me.GroupBox1.Controls.Add(Me.PicExcelExportar)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cmbCondicionIVA)
        Me.GroupBox1.Controls.Add(Me.chkPromo)
        Me.GroupBox1.Controls.Add(Me.cmbRepartidor)
        Me.GroupBox1.Controls.Add(Me.lblRepartidor)
        Me.GroupBox1.Controls.Add(Me.chkUsuarioWEB)
        Me.GroupBox1.Controls.Add(Me.lblUsuarioPP0)
        Me.GroupBox1.Controls.Add(Me.txtusuario)
        Me.GroupBox1.Controls.Add(Me.cmbListaPrecio)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cmbDocTipo)
        Me.GroupBox1.Controls.Add(Me.txtCuit)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.chkCtaCte)
        Me.GroupBox1.Controls.Add(Me.lblEmail)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.cmbLocalidad)
        Me.GroupBox1.Controls.Add(Me.cmbProvincia)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtNOMBRE)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDIRECCION)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCODPOSTAL)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtTELEFONO)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtFAX)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(10, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1348, 243)
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
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(220, 189)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 287
        Me.Label13.Text = "Importar Excel"
        '
        'ImportExcel
        '
        Me.ImportExcel.Location = New System.Drawing.Point(377, 203)
        Me.ImportExcel.Name = "ImportExcel"
        Me.ImportExcel.Size = New System.Drawing.Size(75, 23)
        Me.ImportExcel.TabIndex = 286
        Me.ImportExcel.Text = "Import"
        Me.ImportExcel.UseVisualStyleBackColor = True
        '
        'FileName
        '
        Me.FileName.Location = New System.Drawing.Point(223, 205)
        Me.FileName.Name = "FileName"
        Me.FileName.Size = New System.Drawing.Size(148, 20)
        Me.FileName.TabIndex = 285
        '
        'lblContaNom
        '
        Me.lblContaNom.AutoSize = True
        Me.lblContaNom.BackColor = System.Drawing.Color.Transparent
        Me.lblContaNom.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContaNom.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblContaNom.Location = New System.Drawing.Point(310, 15)
        Me.lblContaNom.Name = "lblContaNom"
        Me.lblContaNom.Size = New System.Drawing.Size(14, 13)
        Me.lblContaNom.TabIndex = 284
        Me.lblContaNom.Text = "0"
        '
        'chkCredito
        '
        Me.chkCredito.AutoSize = True
        Me.chkCredito.BackColor = System.Drawing.Color.Transparent
        Me.chkCredito.ForeColor = System.Drawing.Color.Blue
        Me.chkCredito.Location = New System.Drawing.Point(1057, 97)
        Me.chkCredito.Margin = New System.Windows.Forms.Padding(2)
        Me.chkCredito.Name = "chkCredito"
        Me.chkCredito.Size = New System.Drawing.Size(152, 17)
        Me.chkCredito.TabIndex = 18
        Me.chkCredito.Text = "Monto y Días Máx. Crédito"
        Me.chkCredito.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(1054, 123)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(13, 13)
        Me.Label14.TabIndex = 282
        Me.Label14.Text = "$"
        '
        'txtDiasMax
        '
        Me.txtDiasMax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiasMax.Decimals = CType(2, Byte)
        Me.txtDiasMax.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDiasMax.Enabled = False
        Me.txtDiasMax.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtDiasMax.Location = New System.Drawing.Point(1152, 119)
        Me.txtDiasMax.MaxLength = 10
        Me.txtDiasMax.Name = "txtDiasMax"
        Me.txtDiasMax.Size = New System.Drawing.Size(45, 20)
        Me.txtDiasMax.TabIndex = 20
        Me.txtDiasMax.Text_1 = Nothing
        Me.txtDiasMax.Text_2 = Nothing
        Me.txtDiasMax.Text_3 = Nothing
        Me.txtDiasMax.Text_4 = Nothing
        Me.txtDiasMax.UserValues = Nothing
        '
        'txtMontoMax
        '
        Me.txtMontoMax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMontoMax.Decimals = CType(2, Byte)
        Me.txtMontoMax.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoMax.Enabled = False
        Me.txtMontoMax.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoMax.Location = New System.Drawing.Point(1073, 119)
        Me.txtMontoMax.MaxLength = 10
        Me.txtMontoMax.Name = "txtMontoMax"
        Me.txtMontoMax.Size = New System.Drawing.Size(74, 20)
        Me.txtMontoMax.TabIndex = 19
        Me.txtMontoMax.Text_1 = Nothing
        Me.txtMontoMax.Text_2 = Nothing
        Me.txtMontoMax.Text_3 = Nothing
        Me.txtMontoMax.Text_4 = Nothing
        Me.txtMontoMax.UserValues = Nothing
        '
        'PicExcelExportar
        '
        Me.PicExcelExportar.BackColor = System.Drawing.Color.Transparent
        Me.PicExcelExportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicExcelExportar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicExcelExportar.Image = Global.SEYC.My.Resources.Resources.ms_excel
        Me.PicExcelExportar.Location = New System.Drawing.Point(913, 0)
        Me.PicExcelExportar.Name = "PicExcelExportar"
        Me.PicExcelExportar.Size = New System.Drawing.Size(37, 31)
        Me.PicExcelExportar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicExcelExportar.TabIndex = 278
        Me.PicExcelExportar.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(340, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 13)
        Me.Label12.TabIndex = 262
        Me.Label12.Text = "Condición IVA*"
        '
        'cmbCondicionIVA
        '
        Me.cmbCondicionIVA.AccessibleName = "CondicionIVA*"
        Me.cmbCondicionIVA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondicionIVA.FormattingEnabled = True
        Me.cmbCondicionIVA.Location = New System.Drawing.Point(343, 32)
        Me.cmbCondicionIVA.Name = "cmbCondicionIVA"
        Me.cmbCondicionIVA.Size = New System.Drawing.Size(146, 21)
        Me.cmbCondicionIVA.TabIndex = 2
        '
        'chkPromo
        '
        Me.chkPromo.AutoSize = True
        Me.chkPromo.BackColor = System.Drawing.Color.Transparent
        Me.chkPromo.ForeColor = System.Drawing.Color.Blue
        Me.chkPromo.Location = New System.Drawing.Point(3, 121)
        Me.chkPromo.Name = "chkPromo"
        Me.chkPromo.Size = New System.Drawing.Size(97, 17)
        Me.chkPromo.TabIndex = 11
        Me.chkPromo.Text = "Habilitar Promo"
        Me.chkPromo.UseVisualStyleBackColor = False
        '
        'cmbRepartidor
        '
        Me.cmbRepartidor.AccessibleDescription = "*REPARTIDOR"
        Me.cmbRepartidor.AccessibleName = ""
        Me.cmbRepartidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRepartidor.FormattingEnabled = True
        Me.cmbRepartidor.Location = New System.Drawing.Point(858, 119)
        Me.cmbRepartidor.Name = "cmbRepartidor"
        Me.cmbRepartidor.Size = New System.Drawing.Size(181, 21)
        Me.cmbRepartidor.TabIndex = 17
        '
        'lblRepartidor
        '
        Me.lblRepartidor.AutoSize = True
        Me.lblRepartidor.BackColor = System.Drawing.Color.Transparent
        Me.lblRepartidor.ForeColor = System.Drawing.Color.Blue
        Me.lblRepartidor.Location = New System.Drawing.Point(855, 103)
        Me.lblRepartidor.Name = "lblRepartidor"
        Me.lblRepartidor.Size = New System.Drawing.Size(60, 13)
        Me.lblRepartidor.TabIndex = 259
        Me.lblRepartidor.Text = "Repartidor*"
        '
        'chkUsuarioWEB
        '
        Me.chkUsuarioWEB.AutoSize = True
        Me.chkUsuarioWEB.BackColor = System.Drawing.Color.Transparent
        Me.chkUsuarioWEB.ForeColor = System.Drawing.Color.Blue
        Me.chkUsuarioWEB.Location = New System.Drawing.Point(220, 121)
        Me.chkUsuarioWEB.Name = "chkUsuarioWEB"
        Me.chkUsuarioWEB.Size = New System.Drawing.Size(87, 17)
        Me.chkUsuarioWEB.TabIndex = 13
        Me.chkUsuarioWEB.Text = "UsuarioWEB"
        Me.chkUsuarioWEB.UseVisualStyleBackColor = False
        '
        'lblUsuarioPP0
        '
        Me.lblUsuarioPP0.AutoSize = True
        Me.lblUsuarioPP0.BackColor = System.Drawing.Color.Transparent
        Me.lblUsuarioPP0.ForeColor = System.Drawing.Color.Blue
        Me.lblUsuarioPP0.Location = New System.Drawing.Point(517, 103)
        Me.lblUsuarioPP0.Name = "lblUsuarioPP0"
        Me.lblUsuarioPP0.Size = New System.Drawing.Size(43, 13)
        Me.lblUsuarioPP0.TabIndex = 257
        Me.lblUsuarioPP0.Text = "Usuario"
        '
        'txtusuario
        '
        Me.txtusuario.AccessibleName = ""
        Me.txtusuario.Decimals = CType(2, Byte)
        Me.txtusuario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtusuario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtusuario.Location = New System.Drawing.Point(520, 119)
        Me.txtusuario.MaxLength = 100
        Me.txtusuario.Name = "txtusuario"
        Me.txtusuario.Size = New System.Drawing.Size(153, 20)
        Me.txtusuario.TabIndex = 15
        Me.txtusuario.Text_1 = Nothing
        Me.txtusuario.Text_2 = Nothing
        Me.txtusuario.Text_3 = Nothing
        Me.txtusuario.Text_4 = Nothing
        Me.txtusuario.UserValues = Nothing
        '
        'cmbListaPrecio
        '
        Me.cmbListaPrecio.AccessibleName = "Lista de Precio*"
        Me.cmbListaPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbListaPrecio.FormattingEnabled = True
        Me.cmbListaPrecio.Location = New System.Drawing.Point(678, 119)
        Me.cmbListaPrecio.Name = "cmbListaPrecio"
        Me.cmbListaPrecio.Size = New System.Drawing.Size(174, 21)
        Me.cmbListaPrecio.TabIndex = 16
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(675, 103)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(86, 13)
        Me.Label11.TabIndex = 255
        Me.Label11.Text = "Lista de Precios*"
        '
        'cmbDocTipo
        '
        Me.cmbDocTipo.AccessibleName = ""
        Me.cmbDocTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDocTipo.FormattingEnabled = True
        Me.cmbDocTipo.Location = New System.Drawing.Point(494, 33)
        Me.cmbDocTipo.Name = "cmbDocTipo"
        Me.cmbDocTipo.Size = New System.Drawing.Size(63, 21)
        Me.cmbDocTipo.TabIndex = 3
        '
        'txtCuit
        '
        Me.txtCuit.AccessibleName = ""
        Me.txtCuit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCuit.Decimals = CType(2, Byte)
        Me.txtCuit.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCuit.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtCuit.Location = New System.Drawing.Point(562, 33)
        Me.txtCuit.MaxLength = 11
        Me.txtCuit.Name = "txtCuit"
        Me.txtCuit.Size = New System.Drawing.Size(146, 20)
        Me.txtCuit.TabIndex = 4
        Me.txtCuit.Text_1 = Nothing
        Me.txtCuit.Text_2 = Nothing
        Me.txtCuit.Text_3 = Nothing
        Me.txtCuit.Text_4 = Nothing
        Me.txtCuit.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(490, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 13)
        Me.Label8.TabIndex = 253
        Me.Label8.Text = "Tipo y Nro de Documento"
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chkEliminados.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.Location = New System.Drawing.Point(1240, 122)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(99, 15)
        Me.chkEliminados.TabIndex = 21
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.TextColor = System.Drawing.Color.Red
        '
        'chkCtaCte
        '
        Me.chkCtaCte.AutoSize = True
        Me.chkCtaCte.BackColor = System.Drawing.Color.Transparent
        Me.chkCtaCte.ForeColor = System.Drawing.Color.Blue
        Me.chkCtaCte.Location = New System.Drawing.Point(109, 121)
        Me.chkCtaCte.Name = "chkCtaCte"
        Me.chkCtaCte.Size = New System.Drawing.Size(105, 17)
        Me.chkCtaCte.TabIndex = 12
        Me.chkCtaCte.Text = "Cuenta Corriente"
        Me.chkCtaCte.UseVisualStyleBackColor = False
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.BackColor = System.Drawing.Color.Transparent
        Me.lblEmail.ForeColor = System.Drawing.Color.Blue
        Me.lblEmail.Location = New System.Drawing.Point(310, 103)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(32, 13)
        Me.lblEmail.TabIndex = 97
        Me.lblEmail.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.Decimals = CType(2, Byte)
        Me.txtEmail.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEmail.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtEmail.Location = New System.Drawing.Point(313, 119)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(202, 20)
        Me.txtEmail.TabIndex = 14
        Me.txtEmail.Text_1 = Nothing
        Me.txtEmail.Text_2 = Nothing
        Me.txtEmail.Text_3 = Nothing
        Me.txtEmail.Text_4 = Nothing
        Me.txtEmail.UserValues = Nothing
        '
        'cmbLocalidad
        '
        Me.cmbLocalidad.FormattingEnabled = True
        Me.cmbLocalidad.Location = New System.Drawing.Point(274, 72)
        Me.cmbLocalidad.Name = "cmbLocalidad"
        Me.cmbLocalidad.Size = New System.Drawing.Size(178, 21)
        Me.cmbLocalidad.TabIndex = 8
        '
        'cmbProvincia
        '
        Me.cmbProvincia.FormattingEnabled = True
        Me.cmbProvincia.Location = New System.Drawing.Point(86, 72)
        Me.cmbProvincia.Name = "cmbProvincia"
        Me.cmbProvincia.Size = New System.Drawing.Size(183, 21)
        Me.cmbProvincia.TabIndex = 7
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(892, 3)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(872, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 87
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
        Me.txtCODIGO.Location = New System.Drawing.Point(4, 32)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(77, 20)
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
        Me.Label2.Location = New System.Drawing.Point(1, 17)
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
        Me.txtNOMBRE.Location = New System.Drawing.Point(86, 32)
        Me.txtNOMBRE.MaxLength = 100
        Me.txtNOMBRE.Name = "txtNOMBRE"
        Me.txtNOMBRE.Size = New System.Drawing.Size(252, 20)
        Me.txtNOMBRE.TabIndex = 1
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
        Me.Label3.Location = New System.Drawing.Point(82, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 89
        Me.Label3.Text = "Nombre*"
        '
        'txtDIRECCION
        '
        Me.txtDIRECCION.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDIRECCION.Decimals = CType(2, Byte)
        Me.txtDIRECCION.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDIRECCION.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtDIRECCION.Location = New System.Drawing.Point(716, 33)
        Me.txtDIRECCION.MaxLength = 100
        Me.txtDIRECCION.Name = "txtDIRECCION"
        Me.txtDIRECCION.Size = New System.Drawing.Size(481, 20)
        Me.txtDIRECCION.TabIndex = 5
        Me.txtDIRECCION.Text_1 = Nothing
        Me.txtDIRECCION.Text_2 = Nothing
        Me.txtDIRECCION.Text_3 = Nothing
        Me.txtDIRECCION.Text_4 = Nothing
        Me.txtDIRECCION.UserValues = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(715, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "Dirección"
        '
        'txtCODPOSTAL
        '
        Me.txtCODPOSTAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODPOSTAL.Decimals = CType(2, Byte)
        Me.txtCODPOSTAL.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODPOSTAL.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODPOSTAL.Location = New System.Drawing.Point(4, 72)
        Me.txtCODPOSTAL.MaxLength = 10
        Me.txtCODPOSTAL.Name = "txtCODPOSTAL"
        Me.txtCODPOSTAL.Size = New System.Drawing.Size(77, 20)
        Me.txtCODPOSTAL.TabIndex = 6
        Me.txtCODPOSTAL.Text_1 = Nothing
        Me.txtCODPOSTAL.Text_2 = Nothing
        Me.txtCODPOSTAL.Text_3 = Nothing
        Me.txtCODPOSTAL.Text_4 = Nothing
        Me.txtCODPOSTAL.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(1, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 91
        Me.Label5.Text = "Codpostal"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(271, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 92
        Me.Label6.Text = "Localidad"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(82, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 93
        Me.Label7.Text = "Provincia"
        '
        'txtTELEFONO
        '
        Me.txtTELEFONO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTELEFONO.Decimals = CType(2, Byte)
        Me.txtTELEFONO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTELEFONO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTELEFONO.Location = New System.Drawing.Point(458, 72)
        Me.txtTELEFONO.MaxLength = 50
        Me.txtTELEFONO.Name = "txtTELEFONO"
        Me.txtTELEFONO.Size = New System.Drawing.Size(174, 20)
        Me.txtTELEFONO.TabIndex = 9
        Me.txtTELEFONO.Text_1 = Nothing
        Me.txtTELEFONO.Text_2 = Nothing
        Me.txtTELEFONO.Text_3 = Nothing
        Me.txtTELEFONO.Text_4 = Nothing
        Me.txtTELEFONO.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(454, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 94
        Me.Label9.Text = "Teléfono"
        '
        'txtFAX
        '
        Me.txtFAX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFAX.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFAX.Decimals = CType(2, Byte)
        Me.txtFAX.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtFAX.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtFAX.Location = New System.Drawing.Point(637, 72)
        Me.txtFAX.MaxLength = 30
        Me.txtFAX.Name = "txtFAX"
        Me.txtFAX.Size = New System.Drawing.Size(560, 20)
        Me.txtFAX.TabIndex = 10
        Me.txtFAX.Text_1 = Nothing
        Me.txtFAX.Text_2 = Nothing
        Me.txtFAX.Text_3 = Nothing
        Me.txtFAX.Text_4 = Nothing
        Me.txtFAX.UserValues = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(634, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 95
        Me.Label10.Text = "Fax"
        '
        'frmClientes2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 434)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmClientes2"
        Me.Text = "Clientes"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicExcelExportar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub















    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkCtaCte As System.Windows.Forms.CheckBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents cmbLocalidad As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProvincia As System.Windows.Forms.ComboBox
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNOMBRE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDIRECCION As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCODPOSTAL As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTELEFONO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtFAX As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Private WithEvents chkEliminados As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cmbDocTipo As System.Windows.Forms.ComboBox
    Friend WithEvents txtCuit As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbListaPrecio As System.Windows.Forms.ComboBox
    Friend WithEvents lblUsuarioPP0 As System.Windows.Forms.Label
    Friend WithEvents txtusuario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkUsuarioWEB As System.Windows.Forms.CheckBox
    Friend WithEvents cmbRepartidor As System.Windows.Forms.ComboBox
    Friend WithEvents lblRepartidor As System.Windows.Forms.Label
    Friend WithEvents chkPromo As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbCondicionIVA As System.Windows.Forms.ComboBox
    Friend WithEvents PicExcelExportar As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtDiasMax As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMontoMax As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkCredito As System.Windows.Forms.CheckBox
    Friend WithEvents lblContaNom As System.Windows.Forms.Label
    Friend WithEvents FileName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ImportExcel As System.Windows.Forms.Button

End Class
