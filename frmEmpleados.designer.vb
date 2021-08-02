<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpleados

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
        Me.chkAutoriza = New System.Windows.Forms.CheckBox()
        Me.chkRepartidor = New System.Windows.Forms.CheckBox()
        Me.chkRevendedor = New System.Windows.Forms.CheckBox()
        Me.txtIdJornada = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbJornadas = New System.Windows.Forms.ComboBox()
        Me.btnRestablecerContraseña = New DevComponents.DotNetBar.ButtonX()
        Me.chkUsuarioSistema = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtusuario = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblFechaNac = New System.Windows.Forms.Label()
        Me.dtpFechaNac = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpVencPoliza = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.txtART = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPrecioHora = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCelular = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpFechaIngreso = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkEliminados = New System.Windows.Forms.CheckBox()
        Me.txtCuit = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNOMBRE = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDIRECCION = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTELEFONO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEMAIL = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtApellido = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkVendedor = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dtpFechaNac, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpVencPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFechaIngreso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.chkVendedor)
        Me.GroupBox1.Controls.Add(Me.chkAutoriza)
        Me.GroupBox1.Controls.Add(Me.chkRepartidor)
        Me.GroupBox1.Controls.Add(Me.chkRevendedor)
        Me.GroupBox1.Controls.Add(Me.txtIdJornada)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.cmbJornadas)
        Me.GroupBox1.Controls.Add(Me.btnRestablecerContraseña)
        Me.GroupBox1.Controls.Add(Me.chkUsuarioSistema)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtusuario)
        Me.GroupBox1.Controls.Add(Me.lblFechaNac)
        Me.GroupBox1.Controls.Add(Me.dtpFechaNac)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.dtpVencPoliza)
        Me.GroupBox1.Controls.Add(Me.txtART)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtPrecioHora)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtCelular)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.dtpFechaIngreso)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.chkEliminados)
        Me.GroupBox1.Controls.Add(Me.txtCuit)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtNOMBRE)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDIRECCION)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTELEFONO)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtEMAIL)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtApellido)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1480, 166)
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
        'chkAutoriza
        '
        Me.chkAutoriza.AutoSize = True
        Me.chkAutoriza.BackColor = System.Drawing.Color.Transparent
        Me.chkAutoriza.ForeColor = System.Drawing.Color.Blue
        Me.chkAutoriza.Location = New System.Drawing.Point(559, 130)
        Me.chkAutoriza.Margin = New System.Windows.Forms.Padding(4)
        Me.chkAutoriza.Name = "chkAutoriza"
        Me.chkAutoriza.Size = New System.Drawing.Size(82, 21)
        Me.chkAutoriza.TabIndex = 290
        Me.chkAutoriza.Text = "Autoriza"
        Me.chkAutoriza.UseVisualStyleBackColor = False
        '
        'chkRepartidor
        '
        Me.chkRepartidor.AutoSize = True
        Me.chkRepartidor.BackColor = System.Drawing.Color.Transparent
        Me.chkRepartidor.ForeColor = System.Drawing.Color.Blue
        Me.chkRepartidor.Location = New System.Drawing.Point(778, 130)
        Me.chkRepartidor.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRepartidor.Name = "chkRepartidor"
        Me.chkRepartidor.Size = New System.Drawing.Size(102, 21)
        Me.chkRepartidor.TabIndex = 289
        Me.chkRepartidor.Text = "Distribuidor"
        Me.chkRepartidor.UseVisualStyleBackColor = False
        '
        'chkRevendedor
        '
        Me.chkRevendedor.AutoSize = True
        Me.chkRevendedor.BackColor = System.Drawing.Color.Transparent
        Me.chkRevendedor.ForeColor = System.Drawing.Color.Blue
        Me.chkRevendedor.Location = New System.Drawing.Point(662, 130)
        Me.chkRevendedor.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRevendedor.Name = "chkRevendedor"
        Me.chkRevendedor.Size = New System.Drawing.Size(108, 21)
        Me.chkRevendedor.TabIndex = 288
        Me.chkRevendedor.Text = "Revendedor"
        Me.chkRevendedor.UseVisualStyleBackColor = False
        '
        'txtIdJornada
        '
        Me.txtIdJornada.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdJornada.Decimals = CType(2, Byte)
        Me.txtIdJornada.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdJornada.Enabled = False
        Me.txtIdJornada.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdJornada.Location = New System.Drawing.Point(892, 5)
        Me.txtIdJornada.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdJornada.MaxLength = 8
        Me.txtIdJornada.Name = "txtIdJornada"
        Me.txtIdJornada.Size = New System.Drawing.Size(20, 22)
        Me.txtIdJornada.TabIndex = 287
        Me.txtIdJornada.Text_1 = Nothing
        Me.txtIdJornada.Text_2 = Nothing
        Me.txtIdJornada.Text_3 = Nothing
        Me.txtIdJornada.Text_4 = Nothing
        Me.txtIdJornada.UserValues = Nothing
        Me.txtIdJornada.Visible = False
        '
        'Label16
        '
        Me.Label16.AccessibleName = "*PaseAsociado"
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(739, 12)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(139, 17)
        Me.Label16.TabIndex = 276
        Me.Label16.Text = "Jornada Asignada"
        '
        'cmbJornadas
        '
        Me.cmbJornadas.AccessibleName = ""
        Me.cmbJornadas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbJornadas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbJornadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.cmbJornadas.FormattingEnabled = True
        Me.cmbJornadas.Location = New System.Drawing.Point(739, 32)
        Me.cmbJornadas.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbJornadas.Name = "cmbJornadas"
        Me.cmbJornadas.Size = New System.Drawing.Size(279, 24)
        Me.cmbJornadas.TabIndex = 7
        '
        'btnRestablecerContraseña
        '
        Me.btnRestablecerContraseña.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRestablecerContraseña.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRestablecerContraseña.Location = New System.Drawing.Point(345, 123)
        Me.btnRestablecerContraseña.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRestablecerContraseña.Name = "btnRestablecerContraseña"
        Me.btnRestablecerContraseña.Size = New System.Drawing.Size(191, 28)
        Me.btnRestablecerContraseña.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnRestablecerContraseña.TabIndex = 23
        Me.btnRestablecerContraseña.Text = "Restablecer Contraseña"
        '
        'chkUsuarioSistema
        '
        Me.chkUsuarioSistema.AutoSize = True
        Me.chkUsuarioSistema.BackColor = System.Drawing.Color.Transparent
        Me.chkUsuarioSistema.ForeColor = System.Drawing.Color.Blue
        Me.chkUsuarioSistema.Location = New System.Drawing.Point(201, 129)
        Me.chkUsuarioSistema.Margin = New System.Windows.Forms.Padding(4)
        Me.chkUsuarioSistema.Name = "chkUsuarioSistema"
        Me.chkUsuarioSistema.Size = New System.Drawing.Size(133, 21)
        Me.chkUsuarioSistema.TabIndex = 22
        Me.chkUsuarioSistema.Text = "Usuario Sistema"
        Me.chkUsuarioSistema.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(4, 107)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 17)
        Me.Label15.TabIndex = 273
        Me.Label15.Text = "Usuario*"
        '
        'txtusuario
        '
        Me.txtusuario.AccessibleName = ""
        Me.txtusuario.Decimals = CType(2, Byte)
        Me.txtusuario.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtusuario.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtusuario.Location = New System.Drawing.Point(8, 127)
        Me.txtusuario.Margin = New System.Windows.Forms.Padding(4)
        Me.txtusuario.MaxLength = 50
        Me.txtusuario.Name = "txtusuario"
        Me.txtusuario.Size = New System.Drawing.Size(184, 22)
        Me.txtusuario.TabIndex = 21
        Me.txtusuario.Text_1 = Nothing
        Me.txtusuario.Text_2 = Nothing
        Me.txtusuario.Text_3 = Nothing
        Me.txtusuario.Text_4 = Nothing
        Me.txtusuario.UserValues = Nothing
        '
        'lblFechaNac
        '
        Me.lblFechaNac.AutoSize = True
        Me.lblFechaNac.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaNac.ForeColor = System.Drawing.Color.Blue
        Me.lblFechaNac.Location = New System.Drawing.Point(503, 13)
        Me.lblFechaNac.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFechaNac.Name = "lblFechaNac"
        Me.lblFechaNac.Size = New System.Drawing.Size(76, 17)
        Me.lblFechaNac.TabIndex = 271
        Me.lblFechaNac.Text = "Fecha Nac"
        '
        'dtpFechaNac
        '
        '
        '
        '
        Me.dtpFechaNac.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtpFechaNac.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaNac.ButtonDropDown.Visible = True
        Me.dtpFechaNac.IsPopupCalendarOpen = False
        Me.dtpFechaNac.Location = New System.Drawing.Point(507, 33)
        Me.dtpFechaNac.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.dtpFechaNac.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaNac.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtpFechaNac.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaNac.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtpFechaNac.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtpFechaNac.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtpFechaNac.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpFechaNac.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtpFechaNac.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaNac.MonthCalendar.TodayButtonVisible = True
        Me.dtpFechaNac.Name = "dtpFechaNac"
        Me.dtpFechaNac.Size = New System.Drawing.Size(108, 22)
        Me.dtpFechaNac.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(1298, 13)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(118, 17)
        Me.Label14.TabIndex = 268
        Me.Label14.Text = "Venc, Poliza ART"
        '
        'dtpVencPoliza
        '
        '
        '
        '
        Me.dtpVencPoliza.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtpVencPoliza.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpVencPoliza.ButtonDropDown.Visible = True
        Me.dtpVencPoliza.IsPopupCalendarOpen = False
        Me.dtpVencPoliza.Location = New System.Drawing.Point(1302, 32)
        Me.dtpVencPoliza.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.dtpVencPoliza.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpVencPoliza.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtpVencPoliza.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpVencPoliza.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtpVencPoliza.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtpVencPoliza.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtpVencPoliza.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpVencPoliza.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtpVencPoliza.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpVencPoliza.MonthCalendar.TodayButtonVisible = True
        Me.dtpVencPoliza.Name = "dtpVencPoliza"
        Me.dtpVencPoliza.Size = New System.Drawing.Size(108, 22)
        Me.dtpVencPoliza.TabIndex = 11
        '
        'txtART
        '
        Me.txtART.Decimals = CType(2, Byte)
        Me.txtART.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtART.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtART.Location = New System.Drawing.Point(1026, 31)
        Me.txtART.Margin = New System.Windows.Forms.Padding(4)
        Me.txtART.MaxLength = 50
        Me.txtART.Name = "txtART"
        Me.txtART.Size = New System.Drawing.Size(268, 22)
        Me.txtART.TabIndex = 10
        Me.txtART.Text_1 = Nothing
        Me.txtART.Text_2 = Nothing
        Me.txtART.Text_3 = Nothing
        Me.txtART.Text_4 = Nothing
        Me.txtART.UserValues = Nothing
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(1022, 11)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(36, 17)
        Me.Label13.TabIndex = 266
        Me.Label13.Text = "ART"
        '
        'txtPrecioHora
        '
        Me.txtPrecioHora.Decimals = CType(2, Byte)
        Me.txtPrecioHora.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPrecioHora.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPrecioHora.Location = New System.Drawing.Point(1117, 124)
        Me.txtPrecioHora.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrecioHora.MaxLength = 30
        Me.txtPrecioHora.Name = "txtPrecioHora"
        Me.txtPrecioHora.Size = New System.Drawing.Size(103, 22)
        Me.txtPrecioHora.TabIndex = 6
        Me.txtPrecioHora.Text_1 = Nothing
        Me.txtPrecioHora.Text_2 = Nothing
        Me.txtPrecioHora.Text_3 = Nothing
        Me.txtPrecioHora.Text_4 = Nothing
        Me.txtPrecioHora.UserValues = Nothing
        Me.txtPrecioHora.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(1113, 104)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 17)
        Me.Label12.TabIndex = 264
        Me.Label12.Text = "Precio por Hora"
        Me.Label12.Visible = False
        '
        'txtCelular
        '
        Me.txtCelular.Decimals = CType(2, Byte)
        Me.txtCelular.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCelular.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCelular.Location = New System.Drawing.Point(634, 79)
        Me.txtCelular.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCelular.MaxLength = 50
        Me.txtCelular.Name = "txtCelular"
        Me.txtCelular.Size = New System.Drawing.Size(224, 22)
        Me.txtCelular.TabIndex = 14
        Me.txtCelular.Text_1 = Nothing
        Me.txtCelular.Text_2 = Nothing
        Me.txtCelular.Text_3 = Nothing
        Me.txtCelular.Text_4 = Nothing
        Me.txtCelular.UserValues = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(631, 59)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 17)
        Me.Label7.TabIndex = 260
        Me.Label7.Text = "Celular"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(619, 13)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 17)
        Me.Label6.TabIndex = 258
        Me.Label6.Text = "Fecha Ingreso"
        '
        'dtpFechaIngreso
        '
        '
        '
        '
        Me.dtpFechaIngreso.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtpFechaIngreso.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaIngreso.ButtonDropDown.Visible = True
        Me.dtpFechaIngreso.IsPopupCalendarOpen = False
        Me.dtpFechaIngreso.Location = New System.Drawing.Point(623, 33)
        Me.dtpFechaIngreso.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.dtpFechaIngreso.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaIngreso.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtpFechaIngreso.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaIngreso.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtpFechaIngreso.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtpFechaIngreso.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtpFechaIngreso.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtpFechaIngreso.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtpFechaIngreso.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtpFechaIngreso.MonthCalendar.TodayButtonVisible = True
        Me.dtpFechaIngreso.Name = "dtpFechaIngreso"
        Me.dtpFechaIngreso.Size = New System.Drawing.Size(108, 22)
        Me.dtpFechaIngreso.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(197, 13)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 17)
        Me.Label5.TabIndex = 256
        Me.Label5.Text = "Nombre*"
        '
        'chkEliminados
        '
        Me.chkEliminados.AutoSize = True
        Me.chkEliminados.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminados.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminados.ForeColor = System.Drawing.Color.Red
        Me.chkEliminados.Location = New System.Drawing.Point(1301, 135)
        Me.chkEliminados.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminados.Name = "chkEliminados"
        Me.chkEliminados.Size = New System.Drawing.Size(138, 21)
        Me.chkEliminados.TabIndex = 24
        Me.chkEliminados.Text = "Ver Eliminados"
        Me.chkEliminados.UseVisualStyleBackColor = False
        '
        'txtCuit
        '
        Me.txtCuit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCuit.Decimals = CType(2, Byte)
        Me.txtCuit.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCuit.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtCuit.Location = New System.Drawing.Point(395, 33)
        Me.txtCuit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCuit.MaxLength = 11
        Me.txtCuit.Name = "txtCuit"
        Me.txtCuit.Size = New System.Drawing.Size(103, 22)
        Me.txtCuit.TabIndex = 3
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
        Me.Label8.Location = New System.Drawing.Point(391, 13)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 17)
        Me.Label8.TabIndex = 250
        Me.Label8.Text = "CUIT / CUIL"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1251, 116)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(20, 22)
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
        Me.Label1.Location = New System.Drawing.Point(1224, 121)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 17)
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
        Me.txtCODIGO.Location = New System.Drawing.Point(1038, 124)
        Me.txtCODIGO.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(71, 22)
        Me.txtCODIGO.TabIndex = 29
        Me.txtCODIGO.Text_1 = Nothing
        Me.txtCODIGO.Text_2 = Nothing
        Me.txtCODIGO.Text_3 = Nothing
        Me.txtCODIGO.Text_4 = Nothing
        Me.txtCODIGO.UserValues = Nothing
        Me.txtCODIGO.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(1035, 105)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 17)
        Me.Label2.TabIndex = 240
        Me.Label2.Text = "Código"
        Me.Label2.Visible = False
        '
        'txtNOMBRE
        '
        Me.txtNOMBRE.AccessibleName = "*Nombre"
        Me.txtNOMBRE.Decimals = CType(2, Byte)
        Me.txtNOMBRE.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNOMBRE.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNOMBRE.Location = New System.Drawing.Point(201, 33)
        Me.txtNOMBRE.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNOMBRE.MaxLength = 50
        Me.txtNOMBRE.Name = "txtNOMBRE"
        Me.txtNOMBRE.Size = New System.Drawing.Size(184, 22)
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
        Me.Label3.Location = New System.Drawing.Point(4, 13)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 17)
        Me.Label3.TabIndex = 241
        Me.Label3.Text = "Apellido*"
        '
        'txtDIRECCION
        '
        Me.txtDIRECCION.Decimals = CType(2, Byte)
        Me.txtDIRECCION.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDIRECCION.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtDIRECCION.Location = New System.Drawing.Point(8, 79)
        Me.txtDIRECCION.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDIRECCION.MaxLength = 50
        Me.txtDIRECCION.Name = "txtDIRECCION"
        Me.txtDIRECCION.Size = New System.Drawing.Size(385, 22)
        Me.txtDIRECCION.TabIndex = 12
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
        Me.Label4.Location = New System.Drawing.Point(4, 59)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 17)
        Me.Label4.TabIndex = 242
        Me.Label4.Text = "Dirección"
        '
        'txtTELEFONO
        '
        Me.txtTELEFONO.Decimals = CType(2, Byte)
        Me.txtTELEFONO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTELEFONO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTELEFONO.Location = New System.Drawing.Point(402, 80)
        Me.txtTELEFONO.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTELEFONO.MaxLength = 50
        Me.txtTELEFONO.Name = "txtTELEFONO"
        Me.txtTELEFONO.Size = New System.Drawing.Size(224, 22)
        Me.txtTELEFONO.TabIndex = 13
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
        Me.Label9.Location = New System.Drawing.Point(399, 59)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 17)
        Me.Label9.TabIndex = 246
        Me.Label9.Text = "Teléfono"
        '
        'txtEMAIL
        '
        Me.txtEMAIL.Decimals = CType(2, Byte)
        Me.txtEMAIL.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEMAIL.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtEMAIL.Location = New System.Drawing.Point(866, 79)
        Me.txtEMAIL.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEMAIL.MaxLength = 150
        Me.txtEMAIL.Name = "txtEMAIL"
        Me.txtEMAIL.Size = New System.Drawing.Size(468, 22)
        Me.txtEMAIL.TabIndex = 16
        Me.txtEMAIL.Text_1 = Nothing
        Me.txtEMAIL.Text_2 = Nothing
        Me.txtEMAIL.Text_3 = Nothing
        Me.txtEMAIL.Text_4 = Nothing
        Me.txtEMAIL.UserValues = Nothing
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(863, 58)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 17)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Email"
        '
        'txtApellido
        '
        Me.txtApellido.AccessibleName = "*Apellido"
        Me.txtApellido.Decimals = CType(2, Byte)
        Me.txtApellido.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtApellido.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtApellido.Location = New System.Drawing.Point(8, 33)
        Me.txtApellido.Margin = New System.Windows.Forms.Padding(4)
        Me.txtApellido.MaxLength = 50
        Me.txtApellido.Name = "txtApellido"
        Me.txtApellido.Size = New System.Drawing.Size(184, 22)
        Me.txtApellido.TabIndex = 1
        Me.txtApellido.Text_1 = Nothing
        Me.txtApellido.Text_2 = Nothing
        Me.txtApellido.Text_3 = Nothing
        Me.txtApellido.Text_4 = Nothing
        Me.txtApellido.UserValues = Nothing
        '
        'chkVendedor
        '
        Me.chkVendedor.AutoSize = True
        Me.chkVendedor.BackColor = System.Drawing.Color.Transparent
        Me.chkVendedor.ForeColor = System.Drawing.Color.Blue
        Me.chkVendedor.Location = New System.Drawing.Point(888, 130)
        Me.chkVendedor.Margin = New System.Windows.Forms.Padding(4)
        Me.chkVendedor.Name = "chkVendedor"
        Me.chkVendedor.Size = New System.Drawing.Size(92, 21)
        Me.chkVendedor.TabIndex = 291
        Me.chkVendedor.Text = "Vendedor"
        Me.chkVendedor.UseVisualStyleBackColor = False
        '
        'frmEmpleados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1496, 534)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmEmpleados"
        Me.Text = "Empleados"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dtpFechaNac, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpVencPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFechaIngreso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub















    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtCuit As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNOMBRE As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDIRECCION As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTELEFONO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEMAIL As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkEliminados As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtApellido As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaIngreso As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents txtCelular As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpVencPoliza As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents txtART As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblFechaNac As System.Windows.Forms.Label
    Friend WithEvents dtpFechaNac As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtusuario As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkUsuarioSistema As System.Windows.Forms.CheckBox
    Friend WithEvents btnRestablecerContraseña As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbJornadas As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdJornada As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkRevendedor As System.Windows.Forms.CheckBox
    Friend WithEvents txtPrecioHora As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkRepartidor As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoriza As System.Windows.Forms.CheckBox
    Friend WithEvents chkVendedor As System.Windows.Forms.CheckBox

End Class
