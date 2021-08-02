<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIngresos
    Inherits DevComponents.DotNetBar.Office2007Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.gpCheques = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.txtMontoCheque = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtNroCheque = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.grd = New System.Windows.Forms.DataGridView
        Me.NroCheque = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Banco = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Monto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FechaVenc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IdTipoMoneda = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Observaciones = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtObservaciones = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX
        Me.cmbMoneda = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.ComboItem5 = New DevComponents.Editors.ComboItem
        Me.ComboItem6 = New DevComponents.Editors.ComboItem
        Me.btnAgregar = New DevComponents.DotNetBar.ButtonX
        Me.btnEliminar = New DevComponents.DotNetBar.ButtonX
        Me.txtPropietario = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX
        Me.dtiVencimientoCheque = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX
        Me.cmbBanco = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.ComboItem3 = New DevComponents.Editors.ComboItem
        Me.ComboItem4 = New DevComponents.Editors.ComboItem
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX
        Me.gpPago = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.txtRecargoTarjeta = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtEntregaTarjeta = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtEntregaContado = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblMontoCuotas = New System.Windows.Forms.Label
        Me.lblCantCuotas = New DevComponents.DotNetBar.LabelX
        Me.lblMontoRecargo = New System.Windows.Forms.Label
        Me.lblPorcRecargo = New DevComponents.DotNetBar.LabelX
        Me.lblRecargo = New DevComponents.DotNetBar.LabelX
        Me.lblPorcentaje = New DevComponents.DotNetBar.LabelX
        Me.cmbTarjetas = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.ComboItem7 = New DevComponents.Editors.ComboItem
        Me.ComboItem8 = New DevComponents.Editors.ComboItem
        Me.btnCancelar = New DevComponents.DotNetBar.ButtonX
        Me.btnAceptar = New DevComponents.DotNetBar.ButtonX
        Me.dtiPrimerCuota = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.lblPrimeraCuota = New DevComponents.DotNetBar.LabelX
        Me.cmbTipoCuota = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.ComboItem1 = New DevComponents.Editors.ComboItem
        Me.ComboItem2 = New DevComponents.Editors.ComboItem
        Me.lblTipo = New DevComponents.DotNetBar.LabelX
        Me.lblCantidad = New DevComponents.DotNetBar.LabelX
        Me.iiCantidadCuotas = New DevComponents.Editors.IntegerInput
        Me.lblEntregaCheque = New System.Windows.Forms.Label
        Me.chkCuotas = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblEntregado = New System.Windows.Forms.Label
        Me.chkCheque = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkTarjeta = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkContado = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.gpMonto = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.txtMontoIva = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtRetension = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.chkRetensiones = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblTotalaPagar = New System.Windows.Forms.Label
        Me.lblMontoSinIVA = New System.Windows.Forms.Label
        Me.lblSubtotalsinIVA = New DevComponents.DotNetBar.LabelX
        Me.lblMontoAplicaIVA = New DevComponents.DotNetBar.LabelX
        Me.lblMontoIVA = New System.Windows.Forms.Label
        Me.lblIva = New DevComponents.DotNetBar.LabelX
        Me.chkAplicarIvaParcial = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblMontoReal = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.gpCheques.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtiVencimientoCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpPago.SuspendLayout()
        CType(Me.dtiPrimerCuota, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.iiCantidadCuotas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpMonto.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpCheques
        '
        Me.gpCheques.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpCheques.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpCheques.Controls.Add(Me.txtMontoCheque)
        Me.gpCheques.Controls.Add(Me.txtNroCheque)
        Me.gpCheques.Controls.Add(Me.grd)
        Me.gpCheques.Controls.Add(Me.txtObservaciones)
        Me.gpCheques.Controls.Add(Me.LabelX7)
        Me.gpCheques.Controls.Add(Me.LabelX6)
        Me.gpCheques.Controls.Add(Me.cmbMoneda)
        Me.gpCheques.Controls.Add(Me.btnAgregar)
        Me.gpCheques.Controls.Add(Me.btnEliminar)
        Me.gpCheques.Controls.Add(Me.txtPropietario)
        Me.gpCheques.Controls.Add(Me.LabelX5)
        Me.gpCheques.Controls.Add(Me.LabelX4)
        Me.gpCheques.Controls.Add(Me.dtiVencimientoCheque)
        Me.gpCheques.Controls.Add(Me.LabelX3)
        Me.gpCheques.Controls.Add(Me.LabelX2)
        Me.gpCheques.Controls.Add(Me.cmbBanco)
        Me.gpCheques.Controls.Add(Me.LabelX1)
        Me.gpCheques.Enabled = False
        Me.gpCheques.Location = New System.Drawing.Point(409, 221)
        Me.gpCheques.Name = "gpCheques"
        Me.gpCheques.Size = New System.Drawing.Size(391, 318)
        '
        '
        '
        Me.gpCheques.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpCheques.Style.BackColorGradientAngle = 90
        Me.gpCheques.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpCheques.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpCheques.Style.BorderBottomWidth = 1
        Me.gpCheques.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpCheques.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpCheques.Style.BorderLeftWidth = 1
        Me.gpCheques.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpCheques.Style.BorderRightWidth = 1
        Me.gpCheques.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpCheques.Style.BorderTopWidth = 1
        Me.gpCheques.Style.CornerDiameter = 4
        Me.gpCheques.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpCheques.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpCheques.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        Me.gpCheques.TabIndex = 3
        Me.gpCheques.Text = "3) Detalle de Cheques..."
        '
        'txtMontoCheque
        '
        Me.txtMontoCheque.Decimals = CType(2, Byte)
        Me.txtMontoCheque.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoCheque.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoCheque.Location = New System.Drawing.Point(299, 203)
        Me.txtMontoCheque.Name = "txtMontoCheque"
        Me.txtMontoCheque.Size = New System.Drawing.Size(83, 20)
        Me.txtMontoCheque.TabIndex = 4
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
        Me.txtNroCheque.Location = New System.Drawing.Point(3, 157)
        Me.txtNroCheque.Name = "txtNroCheque"
        Me.txtNroCheque.Size = New System.Drawing.Size(141, 20)
        Me.txtNroCheque.TabIndex = 0
        Me.txtNroCheque.Text_1 = Nothing
        Me.txtNroCheque.Text_2 = Nothing
        Me.txtNroCheque.Text_3 = Nothing
        Me.txtNroCheque.Text_4 = Nothing
        Me.txtNroCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNroCheque.UserValues = Nothing
        '
        'grd
        '
        Me.grd.AllowUserToDeleteRows = False
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NroCheque, Me.Banco, Me.Monto, Me.FechaVenc, Me.Propietario, Me.IdTipoMoneda, Me.Observaciones})
        Me.grd.Location = New System.Drawing.Point(3, 3)
        Me.grd.Name = "grd"
        Me.grd.ReadOnly = True
        Me.grd.Size = New System.Drawing.Size(379, 126)
        Me.grd.TabIndex = 116
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
        Me.Banco.Width = 160
        '
        'Monto
        '
        Me.Monto.HeaderText = "Monto"
        Me.Monto.Name = "Monto"
        Me.Monto.ReadOnly = True
        Me.Monto.Width = 60
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
        'txtObservaciones
        '
        '
        '
        '
        Me.txtObservaciones.Border.Class = "TextBoxBorder"
        Me.txtObservaciones.Location = New System.Drawing.Point(3, 252)
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(208, 20)
        Me.txtObservaciones.TabIndex = 5
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        Me.LabelX7.Location = New System.Drawing.Point(3, 231)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX7.Size = New System.Drawing.Size(76, 15)
        Me.LabelX7.TabIndex = 115
        Me.LabelX7.Text = "Observaciones"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        Me.LabelX6.Location = New System.Drawing.Point(220, 184)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX6.Size = New System.Drawing.Size(42, 15)
        Me.LabelX6.TabIndex = 113
        Me.LabelX6.Text = "Moneda"
        Me.LabelX6.Visible = False
        '
        'cmbMoneda
        '
        Me.cmbMoneda.DisplayMember = "Text"
        Me.cmbMoneda.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoneda.FormattingEnabled = True
        Me.cmbMoneda.ItemHeight = 14
        Me.cmbMoneda.Items.AddRange(New Object() {Me.ComboItem5, Me.ComboItem6})
        Me.cmbMoneda.Location = New System.Drawing.Point(220, 205)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(73, 20)
        Me.cmbMoneda.TabIndex = 5
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
        'btnAgregar
        '
        Me.btnAgregar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregar.Location = New System.Drawing.Point(226, 249)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregar.TabIndex = 6
        Me.btnAgregar.Text = "Agregar"
        '
        'btnEliminar
        '
        Me.btnEliminar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminar.Location = New System.Drawing.Point(307, 249)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminar.TabIndex = 7
        Me.btnEliminar.Text = "Eliminar"
        '
        'txtPropietario
        '
        '
        '
        '
        Me.txtPropietario.Border.Class = "TextBoxBorder"
        Me.txtPropietario.Location = New System.Drawing.Point(3, 204)
        Me.txtPropietario.Name = "txtPropietario"
        Me.txtPropietario.Size = New System.Drawing.Size(208, 20)
        Me.txtPropietario.TabIndex = 3
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        Me.LabelX5.Location = New System.Drawing.Point(3, 183)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX5.Size = New System.Drawing.Size(97, 15)
        Me.LabelX5.TabIndex = 111
        Me.LabelX5.Text = "Propietario Cheque"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        Me.LabelX4.Location = New System.Drawing.Point(299, 183)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX4.Size = New System.Drawing.Size(37, 15)
        Me.LabelX4.TabIndex = 109
        Me.LabelX4.Text = "Monto*"
        '
        'dtiVencimientoCheque
        '
        '
        '
        '
        Me.dtiVencimientoCheque.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtiVencimientoCheque.ButtonDropDown.Visible = True
        Me.dtiVencimientoCheque.Location = New System.Drawing.Point(299, 156)
        '
        '
        '
        Me.dtiVencimientoCheque.MonthCalendar.AnnuallyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiVencimientoCheque.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtiVencimientoCheque.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtiVencimientoCheque.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtiVencimientoCheque.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiVencimientoCheque.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtiVencimientoCheque.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtiVencimientoCheque.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtiVencimientoCheque.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtiVencimientoCheque.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtiVencimientoCheque.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.dtiVencimientoCheque.MonthCalendar.MarkedDates = New Date(-1) {}
        Me.dtiVencimientoCheque.MonthCalendar.MonthlyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiVencimientoCheque.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtiVencimientoCheque.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiVencimientoCheque.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtiVencimientoCheque.MonthCalendar.TodayButtonVisible = True
        Me.dtiVencimientoCheque.Name = "dtiVencimientoCheque"
        Me.dtiVencimientoCheque.Size = New System.Drawing.Size(83, 20)
        Me.dtiVencimientoCheque.TabIndex = 2
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        Me.LabelX3.Location = New System.Drawing.Point(299, 135)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX3.Size = New System.Drawing.Size(64, 15)
        Me.LabelX3.TabIndex = 107
        Me.LabelX3.Text = "Fecha Venc."
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        Me.LabelX2.Location = New System.Drawing.Point(150, 135)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX2.Size = New System.Drawing.Size(38, 15)
        Me.LabelX2.TabIndex = 106
        Me.LabelX2.Text = "Banco*"
        '
        'cmbBanco
        '
        Me.cmbBanco.DisplayMember = "Text"
        Me.cmbBanco.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbBanco.FormattingEnabled = True
        Me.cmbBanco.ItemHeight = 14
        Me.cmbBanco.Items.AddRange(New Object() {Me.ComboItem3, Me.ComboItem4})
        Me.cmbBanco.Location = New System.Drawing.Point(150, 156)
        Me.cmbBanco.Name = "cmbBanco"
        Me.cmbBanco.Size = New System.Drawing.Size(143, 20)
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
        Me.LabelX1.Location = New System.Drawing.Point(3, 135)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX1.Size = New System.Drawing.Size(81, 15)
        Me.LabelX1.TabIndex = 103
        Me.LabelX1.Text = "Nro de Cheque*"
        '
        'gpPago
        '
        Me.gpPago.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpPago.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpPago.Controls.Add(Me.txtRecargoTarjeta)
        Me.gpPago.Controls.Add(Me.txtEntregaTarjeta)
        Me.gpPago.Controls.Add(Me.txtEntregaContado)
        Me.gpPago.Controls.Add(Me.lblMontoCuotas)
        Me.gpPago.Controls.Add(Me.lblCantCuotas)
        Me.gpPago.Controls.Add(Me.lblMontoRecargo)
        Me.gpPago.Controls.Add(Me.lblPorcRecargo)
        Me.gpPago.Controls.Add(Me.lblRecargo)
        Me.gpPago.Controls.Add(Me.lblPorcentaje)
        Me.gpPago.Controls.Add(Me.cmbTarjetas)
        Me.gpPago.Controls.Add(Me.btnCancelar)
        Me.gpPago.Controls.Add(Me.btnAceptar)
        Me.gpPago.Controls.Add(Me.dtiPrimerCuota)
        Me.gpPago.Controls.Add(Me.lblPrimeraCuota)
        Me.gpPago.Controls.Add(Me.cmbTipoCuota)
        Me.gpPago.Controls.Add(Me.lblTipo)
        Me.gpPago.Controls.Add(Me.lblCantidad)
        Me.gpPago.Controls.Add(Me.iiCantidadCuotas)
        Me.gpPago.Controls.Add(Me.lblEntregaCheque)
        Me.gpPago.Controls.Add(Me.chkCuotas)
        Me.gpPago.Controls.Add(Me.Label2)
        Me.gpPago.Controls.Add(Me.lblEntregado)
        Me.gpPago.Controls.Add(Me.chkCheque)
        Me.gpPago.Controls.Add(Me.chkTarjeta)
        Me.gpPago.Controls.Add(Me.chkContado)
        Me.gpPago.Location = New System.Drawing.Point(12, 221)
        Me.gpPago.Name = "gpPago"
        Me.gpPago.Size = New System.Drawing.Size(391, 318)
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
        Me.gpPago.TabIndex = 2
        Me.gpPago.Text = "2) Forma de Pago..."
        '
        'txtRecargoTarjeta
        '
        Me.txtRecargoTarjeta.Decimals = CType(2, Byte)
        Me.txtRecargoTarjeta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRecargoTarjeta.Enabled = False
        Me.txtRecargoTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecargoTarjeta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtRecargoTarjeta.Location = New System.Drawing.Point(116, 178)
        Me.txtRecargoTarjeta.Name = "txtRecargoTarjeta"
        Me.txtRecargoTarjeta.Size = New System.Drawing.Size(37, 20)
        Me.txtRecargoTarjeta.TabIndex = 11
        Me.txtRecargoTarjeta.Text = "0"
        Me.txtRecargoTarjeta.Text_1 = Nothing
        Me.txtRecargoTarjeta.Text_2 = Nothing
        Me.txtRecargoTarjeta.Text_3 = Nothing
        Me.txtRecargoTarjeta.Text_4 = Nothing
        Me.txtRecargoTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecargoTarjeta.UserValues = Nothing
        '
        'txtEntregaTarjeta
        '
        Me.txtEntregaTarjeta.Decimals = CType(2, Byte)
        Me.txtEntregaTarjeta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEntregaTarjeta.Enabled = False
        Me.txtEntregaTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntregaTarjeta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtEntregaTarjeta.Location = New System.Drawing.Point(286, 149)
        Me.txtEntregaTarjeta.Name = "txtEntregaTarjeta"
        Me.txtEntregaTarjeta.Size = New System.Drawing.Size(63, 20)
        Me.txtEntregaTarjeta.TabIndex = 10
        Me.txtEntregaTarjeta.Text = "0"
        Me.txtEntregaTarjeta.Text_1 = Nothing
        Me.txtEntregaTarjeta.Text_2 = Nothing
        Me.txtEntregaTarjeta.Text_3 = Nothing
        Me.txtEntregaTarjeta.Text_4 = Nothing
        Me.txtEntregaTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEntregaTarjeta.UserValues = Nothing
        '
        'txtEntregaContado
        '
        Me.txtEntregaContado.Decimals = CType(2, Byte)
        Me.txtEntregaContado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEntregaContado.Enabled = False
        Me.txtEntregaContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntregaContado.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtEntregaContado.Location = New System.Drawing.Point(286, 11)
        Me.txtEntregaContado.Name = "txtEntregaContado"
        Me.txtEntregaContado.Size = New System.Drawing.Size(63, 20)
        Me.txtEntregaContado.TabIndex = 7
        Me.txtEntregaContado.Text = "0"
        Me.txtEntregaContado.Text_1 = Nothing
        Me.txtEntregaContado.Text_2 = Nothing
        Me.txtEntregaContado.Text_3 = Nothing
        Me.txtEntregaContado.Text_4 = Nothing
        Me.txtEntregaContado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEntregaContado.UserValues = Nothing
        '
        'lblMontoCuotas
        '
        Me.lblMontoCuotas.BackColor = System.Drawing.Color.White
        Me.lblMontoCuotas.Enabled = False
        Me.lblMontoCuotas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoCuotas.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoCuotas.Location = New System.Drawing.Point(170, 127)
        Me.lblMontoCuotas.Name = "lblMontoCuotas"
        Me.lblMontoCuotas.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoCuotas.TabIndex = 5
        Me.lblMontoCuotas.Text = "0"
        Me.lblMontoCuotas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCantCuotas
        '
        Me.lblCantCuotas.AutoSize = True
        Me.lblCantCuotas.BackColor = System.Drawing.Color.Transparent
        Me.lblCantCuotas.Enabled = False
        Me.lblCantCuotas.Location = New System.Drawing.Point(108, 129)
        Me.lblCantCuotas.Name = "lblCantCuotas"
        Me.lblCantCuotas.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblCantCuotas.Size = New System.Drawing.Size(56, 15)
        Me.lblCantCuotas.TabIndex = 112
        Me.lblCantCuotas.Text = "Cuotas de:"
        '
        'lblMontoRecargo
        '
        Me.lblMontoRecargo.BackColor = System.Drawing.Color.White
        Me.lblMontoRecargo.Enabled = False
        Me.lblMontoRecargo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoRecargo.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoRecargo.Location = New System.Drawing.Point(286, 179)
        Me.lblMontoRecargo.Name = "lblMontoRecargo"
        Me.lblMontoRecargo.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoRecargo.TabIndex = 111
        Me.lblMontoRecargo.Text = "0"
        Me.lblMontoRecargo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPorcRecargo
        '
        Me.lblPorcRecargo.AutoSize = True
        Me.lblPorcRecargo.BackColor = System.Drawing.Color.Transparent
        Me.lblPorcRecargo.Enabled = False
        Me.lblPorcRecargo.Location = New System.Drawing.Point(66, 181)
        Me.lblPorcRecargo.Name = "lblPorcRecargo"
        Me.lblPorcRecargo.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblPorcRecargo.Size = New System.Drawing.Size(44, 15)
        Me.lblPorcRecargo.TabIndex = 110
        Me.lblPorcRecargo.Text = "Recargo"
        '
        'lblRecargo
        '
        Me.lblRecargo.AutoSize = True
        Me.lblRecargo.BackColor = System.Drawing.Color.Transparent
        Me.lblRecargo.Enabled = False
        Me.lblRecargo.Location = New System.Drawing.Point(183, 181)
        Me.lblRecargo.Name = "lblRecargo"
        Me.lblRecargo.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblRecargo.Size = New System.Drawing.Size(78, 15)
        Me.lblRecargo.TabIndex = 108
        Me.lblRecargo.Text = "Monto Recargo"
        '
        'lblPorcentaje
        '
        Me.lblPorcentaje.AutoSize = True
        Me.lblPorcentaje.BackColor = System.Drawing.Color.Transparent
        Me.lblPorcentaje.Enabled = False
        Me.lblPorcentaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPorcentaje.Location = New System.Drawing.Point(159, 180)
        Me.lblPorcentaje.Name = "lblPorcentaje"
        Me.lblPorcentaje.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblPorcentaje.Size = New System.Drawing.Size(14, 17)
        Me.lblPorcentaje.TabIndex = 109
        Me.lblPorcentaje.Text = "%"
        '
        'cmbTarjetas
        '
        Me.cmbTarjetas.DisplayMember = "Text"
        Me.cmbTarjetas.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbTarjetas.FormattingEnabled = True
        Me.cmbTarjetas.ItemHeight = 14
        Me.cmbTarjetas.Items.AddRange(New Object() {Me.ComboItem7, Me.ComboItem8})
        Me.cmbTarjetas.Location = New System.Drawing.Point(82, 149)
        Me.cmbTarjetas.Name = "cmbTarjetas"
        Me.cmbTarjetas.Size = New System.Drawing.Size(178, 20)
        Me.cmbTarjetas.TabIndex = 9
        '
        'ComboItem7
        '
        Me.ComboItem7.Text = "Mensual"
        '
        'ComboItem8
        '
        Me.ComboItem8.Text = "Quincenal"
        '
        'btnCancelar
        '
        Me.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancelar.Location = New System.Drawing.Point(194, 271)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 16
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAceptar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAceptar.Location = New System.Drawing.Point(113, 271)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 15
        Me.btnAceptar.Text = "Finalizar Pago"
        '
        'dtiPrimerCuota
        '
        '
        '
        '
        Me.dtiPrimerCuota.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtiPrimerCuota.ButtonDropDown.Visible = True
        Me.dtiPrimerCuota.Enabled = False
        Me.dtiPrimerCuota.Location = New System.Drawing.Point(239, 94)
        '
        '
        '
        Me.dtiPrimerCuota.MonthCalendar.AnnuallyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiPrimerCuota.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtiPrimerCuota.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtiPrimerCuota.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtiPrimerCuota.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiPrimerCuota.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtiPrimerCuota.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtiPrimerCuota.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtiPrimerCuota.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtiPrimerCuota.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtiPrimerCuota.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.dtiPrimerCuota.MonthCalendar.MarkedDates = New Date(-1) {}
        Me.dtiPrimerCuota.MonthCalendar.MonthlyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiPrimerCuota.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtiPrimerCuota.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiPrimerCuota.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtiPrimerCuota.MonthCalendar.TodayButtonVisible = True
        Me.dtiPrimerCuota.Name = "dtiPrimerCuota"
        Me.dtiPrimerCuota.Size = New System.Drawing.Size(85, 20)
        Me.dtiPrimerCuota.TabIndex = 4
        '
        'lblPrimeraCuota
        '
        Me.lblPrimeraCuota.AutoSize = True
        Me.lblPrimeraCuota.BackColor = System.Drawing.Color.Transparent
        Me.lblPrimeraCuota.Enabled = False
        Me.lblPrimeraCuota.Location = New System.Drawing.Point(221, 73)
        Me.lblPrimeraCuota.Name = "lblPrimeraCuota"
        Me.lblPrimeraCuota.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblPrimeraCuota.Size = New System.Drawing.Size(103, 15)
        Me.lblPrimeraCuota.TabIndex = 104
        Me.lblPrimeraCuota.Text = "Primera Cuota el día"
        '
        'cmbTipoCuota
        '
        Me.cmbTipoCuota.DisplayMember = "Text"
        Me.cmbTipoCuota.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbTipoCuota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoCuota.Enabled = False
        Me.cmbTipoCuota.FormattingEnabled = True
        Me.cmbTipoCuota.ItemHeight = 14
        Me.cmbTipoCuota.Items.AddRange(New Object() {Me.ComboItem1, Me.ComboItem2})
        Me.cmbTipoCuota.Location = New System.Drawing.Point(130, 94)
        Me.cmbTipoCuota.Name = "cmbTipoCuota"
        Me.cmbTipoCuota.Size = New System.Drawing.Size(103, 20)
        Me.cmbTipoCuota.TabIndex = 3
        '
        'ComboItem1
        '
        Me.ComboItem1.Text = "Mensual"
        '
        'ComboItem2
        '
        Me.ComboItem2.Text = "Quincenal"
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.BackColor = System.Drawing.Color.Transparent
        Me.lblTipo.Enabled = False
        Me.lblTipo.Location = New System.Drawing.Point(130, 73)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblTipo.Size = New System.Drawing.Size(24, 15)
        Me.lblTipo.TabIndex = 102
        Me.lblTipo.Text = "Tipo"
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.BackColor = System.Drawing.Color.Transparent
        Me.lblCantidad.Enabled = False
        Me.lblCantidad.Location = New System.Drawing.Point(77, 73)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblCantidad.Size = New System.Drawing.Size(47, 15)
        Me.lblCantidad.TabIndex = 101
        Me.lblCantidad.Text = "Cantidad"
        '
        'iiCantidadCuotas
        '
        '
        '
        '
        Me.iiCantidadCuotas.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.iiCantidadCuotas.Enabled = False
        Me.iiCantidadCuotas.Location = New System.Drawing.Point(77, 94)
        Me.iiCantidadCuotas.MaxValue = 99
        Me.iiCantidadCuotas.MinValue = 2
        Me.iiCantidadCuotas.Name = "iiCantidadCuotas"
        Me.iiCantidadCuotas.ShowUpDown = True
        Me.iiCantidadCuotas.Size = New System.Drawing.Size(47, 20)
        Me.iiCantidadCuotas.TabIndex = 2
        Me.iiCantidadCuotas.Value = 2
        '
        'lblEntregaCheque
        '
        Me.lblEntregaCheque.BackColor = System.Drawing.Color.White
        Me.lblEntregaCheque.Enabled = False
        Me.lblEntregaCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaCheque.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaCheque.Location = New System.Drawing.Point(286, 42)
        Me.lblEntregaCheque.Name = "lblEntregaCheque"
        Me.lblEntregaCheque.Size = New System.Drawing.Size(63, 19)
        Me.lblEntregaCheque.TabIndex = 13
        Me.lblEntregaCheque.Text = "0"
        Me.lblEntregaCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkCuotas
        '
        Me.chkCuotas.BackColor = System.Drawing.Color.Transparent
        Me.chkCuotas.Location = New System.Drawing.Point(4, 73)
        Me.chkCuotas.Name = "chkCuotas"
        Me.chkCuotas.Size = New System.Drawing.Size(61, 22)
        Me.chkCuotas.TabIndex = 1
        Me.chkCuotas.Text = "Cuotas"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(200, 225)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 16)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Entregado"
        '
        'lblEntregado
        '
        Me.lblEntregado.BackColor = System.Drawing.Color.White
        Me.lblEntregado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregado.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregado.Location = New System.Drawing.Point(286, 224)
        Me.lblEntregado.Name = "lblEntregado"
        Me.lblEntregado.Size = New System.Drawing.Size(63, 19)
        Me.lblEntregado.TabIndex = 14
        Me.lblEntregado.Text = "0"
        Me.lblEntregado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkCheque
        '
        Me.chkCheque.BackColor = System.Drawing.Color.Transparent
        Me.chkCheque.Location = New System.Drawing.Point(9, 38)
        Me.chkCheque.Name = "chkCheque"
        Me.chkCheque.Size = New System.Drawing.Size(79, 22)
        Me.chkCheque.TabIndex = 12
        Me.chkCheque.Text = "Cheque"
        '
        'chkTarjeta
        '
        Me.chkTarjeta.BackColor = System.Drawing.Color.Transparent
        Me.chkTarjeta.Location = New System.Drawing.Point(9, 149)
        Me.chkTarjeta.Name = "chkTarjeta"
        Me.chkTarjeta.Size = New System.Drawing.Size(61, 22)
        Me.chkTarjeta.TabIndex = 8
        Me.chkTarjeta.Text = "Tarjeta"
        '
        'chkContado
        '
        Me.chkContado.BackColor = System.Drawing.Color.Transparent
        Me.chkContado.Location = New System.Drawing.Point(9, 9)
        Me.chkContado.Name = "chkContado"
        Me.chkContado.Size = New System.Drawing.Size(61, 22)
        Me.chkContado.TabIndex = 6
        Me.chkContado.Text = "Contado"
        '
        'gpMonto
        '
        Me.gpMonto.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpMonto.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpMonto.Controls.Add(Me.RadioButton2)
        Me.gpMonto.Controls.Add(Me.RadioButton1)
        Me.gpMonto.Controls.Add(Me.txtMontoIva)
        Me.gpMonto.Controls.Add(Me.txtRetension)
        Me.gpMonto.Controls.Add(Me.chkRetensiones)
        Me.gpMonto.Controls.Add(Me.Label5)
        Me.gpMonto.Controls.Add(Me.lblTotalaPagar)
        Me.gpMonto.Controls.Add(Me.lblMontoSinIVA)
        Me.gpMonto.Controls.Add(Me.lblSubtotalsinIVA)
        Me.gpMonto.Controls.Add(Me.lblMontoAplicaIVA)
        Me.gpMonto.Controls.Add(Me.lblMontoIVA)
        Me.gpMonto.Controls.Add(Me.lblIva)
        Me.gpMonto.Controls.Add(Me.chkAplicarIvaParcial)
        Me.gpMonto.Controls.Add(Me.Label1)
        Me.gpMonto.Controls.Add(Me.lblMontoReal)
        Me.gpMonto.Location = New System.Drawing.Point(12, 9)
        Me.gpMonto.Name = "gpMonto"
        Me.gpMonto.Size = New System.Drawing.Size(391, 206)
        '
        '
        '
        Me.gpMonto.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpMonto.Style.BackColorGradientAngle = 90
        Me.gpMonto.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpMonto.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpMonto.Style.BorderBottomWidth = 1
        Me.gpMonto.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpMonto.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpMonto.Style.BorderLeftWidth = 1
        Me.gpMonto.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpMonto.Style.BorderRightWidth = 1
        Me.gpMonto.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpMonto.Style.BorderTopWidth = 1
        Me.gpMonto.Style.CornerDiameter = 4
        Me.gpMonto.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpMonto.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpMonto.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        Me.gpMonto.TabIndex = 1
        Me.gpMonto.Text = "1) Monto Final a Pagar..."
        '
        'txtMontoIva
        '
        Me.txtMontoIva.Decimals = CType(2, Byte)
        Me.txtMontoIva.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoIva.Enabled = False
        Me.txtMontoIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoIva.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoIva.Location = New System.Drawing.Point(201, 74)
        Me.txtMontoIva.Name = "txtMontoIva"
        Me.txtMontoIva.Size = New System.Drawing.Size(53, 20)
        Me.txtMontoIva.TabIndex = 2
        Me.txtMontoIva.Text = "0"
        Me.txtMontoIva.Text_1 = Nothing
        Me.txtMontoIva.Text_2 = Nothing
        Me.txtMontoIva.Text_3 = Nothing
        Me.txtMontoIva.Text_4 = Nothing
        Me.txtMontoIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoIva.UserValues = Nothing
        '
        'txtRetension
        '
        Me.txtRetension.Decimals = CType(2, Byte)
        Me.txtRetension.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRetension.Enabled = False
        Me.txtRetension.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRetension.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtRetension.Location = New System.Drawing.Point(286, 125)
        Me.txtRetension.Name = "txtRetension"
        Me.txtRetension.Size = New System.Drawing.Size(63, 20)
        Me.txtRetension.TabIndex = 4
        Me.txtRetension.Text = "0"
        Me.txtRetension.Text_1 = Nothing
        Me.txtRetension.Text_2 = Nothing
        Me.txtRetension.Text_3 = Nothing
        Me.txtRetension.Text_4 = Nothing
        Me.txtRetension.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRetension.UserValues = Nothing
        '
        'chkRetensiones
        '
        Me.chkRetensiones.BackColor = System.Drawing.Color.Transparent
        Me.chkRetensiones.Location = New System.Drawing.Point(12, 123)
        Me.chkRetensiones.Name = "chkRetensiones"
        Me.chkRetensiones.Size = New System.Drawing.Size(90, 22)
        Me.chkRetensiones.TabIndex = 3
        Me.chkRetensiones.Text = "Retensiones"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(177, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 16)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Total a Pagar"
        '
        'lblTotalaPagar
        '
        Me.lblTotalaPagar.BackColor = System.Drawing.Color.White
        Me.lblTotalaPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalaPagar.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTotalaPagar.Location = New System.Drawing.Point(286, 153)
        Me.lblTotalaPagar.Name = "lblTotalaPagar"
        Me.lblTotalaPagar.Size = New System.Drawing.Size(63, 19)
        Me.lblTotalaPagar.TabIndex = 5
        Me.lblTotalaPagar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMontoSinIVA
        '
        Me.lblMontoSinIVA.BackColor = System.Drawing.Color.White
        Me.lblMontoSinIVA.Enabled = False
        Me.lblMontoSinIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoSinIVA.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoSinIVA.Location = New System.Drawing.Point(286, 99)
        Me.lblMontoSinIVA.Name = "lblMontoSinIVA"
        Me.lblMontoSinIVA.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoSinIVA.TabIndex = 10
        Me.lblMontoSinIVA.Text = "0"
        Me.lblMontoSinIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubtotalsinIVA
        '
        Me.lblSubtotalsinIVA.AutoSize = True
        Me.lblSubtotalsinIVA.BackColor = System.Drawing.Color.Transparent
        Me.lblSubtotalsinIVA.Enabled = False
        Me.lblSubtotalsinIVA.Location = New System.Drawing.Point(194, 101)
        Me.lblSubtotalsinIVA.Name = "lblSubtotalsinIVA"
        Me.lblSubtotalsinIVA.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblSubtotalsinIVA.Size = New System.Drawing.Size(86, 15)
        Me.lblSubtotalsinIVA.TabIndex = 9
        Me.lblSubtotalsinIVA.Text = "SubTotal Sin IVA"
        '
        'lblMontoAplicaIVA
        '
        Me.lblMontoAplicaIVA.AutoSize = True
        Me.lblMontoAplicaIVA.BackColor = System.Drawing.Color.Transparent
        Me.lblMontoAplicaIVA.Enabled = False
        Me.lblMontoAplicaIVA.Location = New System.Drawing.Point(129, 76)
        Me.lblMontoAplicaIVA.Name = "lblMontoAplicaIVA"
        Me.lblMontoAplicaIVA.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblMontoAplicaIVA.Size = New System.Drawing.Size(66, 15)
        Me.lblMontoAplicaIVA.TabIndex = 3
        Me.lblMontoAplicaIVA.Text = "Monto Aplica"
        '
        'lblMontoIVA
        '
        Me.lblMontoIVA.BackColor = System.Drawing.Color.White
        Me.lblMontoIVA.Enabled = False
        Me.lblMontoIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoIVA.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoIVA.Location = New System.Drawing.Point(286, 74)
        Me.lblMontoIVA.Name = "lblMontoIVA"
        Me.lblMontoIVA.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoIVA.TabIndex = 6
        Me.lblMontoIVA.Text = "0"
        Me.lblMontoIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIva
        '
        Me.lblIva.AutoSize = True
        Me.lblIva.BackColor = System.Drawing.Color.Transparent
        Me.lblIva.Enabled = False
        Me.lblIva.Location = New System.Drawing.Point(260, 76)
        Me.lblIva.Name = "lblIva"
        Me.lblIva.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblIva.Size = New System.Drawing.Size(20, 15)
        Me.lblIva.TabIndex = 5
        Me.lblIva.Text = "IVA"
        '
        'chkAplicarIvaParcial
        '
        Me.chkAplicarIvaParcial.BackColor = System.Drawing.Color.Transparent
        Me.chkAplicarIvaParcial.Location = New System.Drawing.Point(12, 72)
        Me.chkAplicarIvaParcial.Name = "chkAplicarIvaParcial"
        Me.chkAplicarIvaParcial.Size = New System.Drawing.Size(112, 22)
        Me.chkAplicarIvaParcial.TabIndex = 0
        Me.chkAplicarIvaParcial.Text = "Aplicar IVA Parcial"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(184, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Total sin IVA"
        '
        'lblMontoReal
        '
        Me.lblMontoReal.BackColor = System.Drawing.Color.White
        Me.lblMontoReal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoReal.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoReal.Location = New System.Drawing.Point(286, 41)
        Me.lblMontoReal.Name = "lblMontoReal"
        Me.lblMontoReal.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoReal.TabIndex = 7
        Me.lblMontoReal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(373, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "INGRESOS"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(20, 14)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton1.TabIndex = 13
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "RadioButton1"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(129, 14)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton2.TabIndex = 14
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "RadioButton2"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'frmIngresos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 553)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.gpMonto)
        Me.Controls.Add(Me.gpCheques)
        Me.Controls.Add(Me.gpPago)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIngresos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Finalizar Operación - Registrando Pago"
        Me.gpCheques.ResumeLayout(False)
        Me.gpCheques.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtiVencimientoCheque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpPago.ResumeLayout(False)
        Me.gpPago.PerformLayout()
        CType(Me.dtiPrimerCuota, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.iiCantidadCuotas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpMonto.ResumeLayout(False)
        Me.gpMonto.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gpCheques As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnAgregar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtPropietario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtiVencimientoCheque As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbBanco As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem3 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem4 As DevComponents.Editors.ComboItem
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents gpPago As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnCancelar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnAceptar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents dtiPrimerCuota As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents lblPrimeraCuota As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbTipoCuota As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem1 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem2 As DevComponents.Editors.ComboItem
    Friend WithEvents lblTipo As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblCantidad As DevComponents.DotNetBar.LabelX
    Friend WithEvents iiCantidadCuotas As DevComponents.Editors.IntegerInput
    Friend WithEvents lblEntregaCheque As System.Windows.Forms.Label
    Friend WithEvents chkCuotas As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblEntregado As System.Windows.Forms.Label
    Friend WithEvents chkCheque As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkTarjeta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkContado As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents gpMonto As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTotalaPagar As System.Windows.Forms.Label
    Friend WithEvents lblMontoSinIVA As System.Windows.Forms.Label
    Friend WithEvents lblSubtotalsinIVA As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMontoAplicaIVA As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMontoIVA As System.Windows.Forms.Label
    Friend WithEvents lblIva As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkAplicarIvaParcial As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMontoReal As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMoneda As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem5 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem6 As DevComponents.Editors.ComboItem
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents NroCheque As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Banco As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaVenc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Propietario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdTipoMoneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Observaciones As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbTarjetas As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem7 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem8 As DevComponents.Editors.ComboItem
    Friend WithEvents lblMontoRecargo As System.Windows.Forms.Label
    Friend WithEvents lblPorcRecargo As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblRecargo As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPorcentaje As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkRetensiones As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblMontoCuotas As System.Windows.Forms.Label
    Friend WithEvents lblCantCuotas As DevComponents.DotNetBar.LabelX
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEntregaContado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtEntregaTarjeta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtRecargoTarjeta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMontoIva As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtRetension As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtMontoCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
End Class
