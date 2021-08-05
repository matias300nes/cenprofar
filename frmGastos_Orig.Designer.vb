<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGastoS_ORIG

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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.gpDetallePropios = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.grdChequesAsignados = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdChequesPropios = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnAgregarChequePropio = New DevComponents.DotNetBar.ButtonX
        Me.btnEliminarChequePropio = New DevComponents.DotNetBar.ButtonX
        Me.gpPago = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.rbOtros = New System.Windows.Forms.RadioButton
        Me.rbPropios = New System.Windows.Forms.RadioButton
        Me.dtiFechaPago = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.lblFechaDatosPerfo = New DevComponents.DotNetBar.LabelX
        Me.txtRecargoTarjeta = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtEntregaContado = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtEntregaTarjeta = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.lblMontoRecargo = New System.Windows.Forms.Label
        Me.lblPorcRecargo = New DevComponents.DotNetBar.LabelX
        Me.lblRecargo = New DevComponents.DotNetBar.LabelX
        Me.lblPorcentaje = New DevComponents.DotNetBar.LabelX
        Me.cmbTarjetas = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.lblEntregaCheque = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblEntregado = New System.Windows.Forms.Label
        Me.chkCheque = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkTarjeta = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkContado = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkObra = New System.Windows.Forms.CheckBox
        Me.chkCuentaCorriente = New System.Windows.Forms.CheckBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.gpDetalle = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.grdCheques = New System.Windows.Forms.DataGridView
        Me.NroCheque = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Banco = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Monto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FechaVenc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Propietario = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IdTipoMoneda = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Observaciones = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtMontoCheque = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtObservaciones = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX
        Me.cmbMoneda = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.btnAgregarCheque = New DevComponents.DotNetBar.ButtonX
        Me.btnEliminarCheque = New DevComponents.DotNetBar.ButtonX
        Me.txtPropietario = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX
        Me.dtiVencimientoCheque = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX
        Me.cmbBanco = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.txtNroCheque = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTipoFactura = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtIVA = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label10 = New System.Windows.Forms.Label
        Me.picProveedores = New System.Windows.Forms.PictureBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbProveedor = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTotal = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtNroRemitoFactura = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblObra = New System.Windows.Forms.Label
        Me.lblCliente = New System.Windows.Forms.Label
        Me.txtNroGasto = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbCliente = New System.Windows.Forms.ComboBox
        Me.PicClientes = New System.Windows.Forms.PictureBox
        Me.cmbObras = New System.Windows.Forms.ComboBox
        Me.PicObras = New System.Windows.Forms.PictureBox
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.dtiFechaGasto = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.GroupBox1.SuspendLayout()
        Me.gpDetallePropios.SuspendLayout()
        CType(Me.grdChequesAsignados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdChequesPropios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpPago.SuspendLayout()
        CType(Me.dtiFechaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpDetalle.SuspendLayout()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtiVencimientoCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picProveedores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicObras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.dtiFechaGasto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dtiFechaGasto)
        Me.GroupBox1.Controls.Add(Me.gpDetallePropios)
        Me.GroupBox1.Controls.Add(Me.gpPago)
        Me.GroupBox1.Controls.Add(Me.chkObra)
        Me.GroupBox1.Controls.Add(Me.chkCuentaCorriente)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.gpDetalle)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTipoFactura)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.picProveedores)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbProveedor)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtNroRemitoFactura)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblObra)
        Me.GroupBox1.Controls.Add(Me.lblCliente)
        Me.GroupBox1.Controls.Add(Me.txtNroGasto)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.PicClientes)
        Me.GroupBox1.Controls.Add(Me.cmbObras)
        Me.GroupBox1.Controls.Add(Me.PicObras)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1233, 353)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'gpDetallePropios
        '
        Me.gpDetallePropios.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpDetallePropios.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpDetallePropios.Controls.Add(Me.grdChequesAsignados)
        Me.gpDetallePropios.Controls.Add(Me.grdChequesPropios)
        Me.gpDetallePropios.Controls.Add(Me.btnAgregarChequePropio)
        Me.gpDetallePropios.Controls.Add(Me.btnEliminarChequePropio)
        Me.gpDetallePropios.Enabled = False
        Me.gpDetallePropios.Location = New System.Drawing.Point(810, 19)
        Me.gpDetallePropios.Name = "gpDetallePropios"
        Me.gpDetallePropios.Size = New System.Drawing.Size(391, 323)
        '
        '
        '
        Me.gpDetallePropios.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpDetallePropios.Style.BackColorGradientAngle = 90
        Me.gpDetallePropios.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpDetallePropios.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetallePropios.Style.BorderBottomWidth = 1
        Me.gpDetallePropios.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpDetallePropios.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetallePropios.Style.BorderLeftWidth = 1
        Me.gpDetallePropios.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetallePropios.Style.BorderRightWidth = 1
        Me.gpDetallePropios.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetallePropios.Style.BorderTopWidth = 1
        Me.gpDetallePropios.Style.CornerDiameter = 4
        Me.gpDetallePropios.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpDetallePropios.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpDetallePropios.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        Me.gpDetallePropios.TabIndex = 143
        Me.gpDetallePropios.Text = "2) Cheques en Cartera..."
        '
        'grdChequesAsignados
        '
        Me.grdChequesAsignados.AllowUserToDeleteRows = False
        Me.grdChequesAsignados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChequesAsignados.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11})
        Me.grdChequesAsignados.Location = New System.Drawing.Point(3, 168)
        Me.grdChequesAsignados.Name = "grdChequesAsignados"
        Me.grdChequesAsignados.ReadOnly = True
        Me.grdChequesAsignados.Size = New System.Drawing.Size(379, 131)
        Me.grdChequesAsignados.TabIndex = 118
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "NroCheque"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "Banco"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 160
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "Monto"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 60
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "IdCheque"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Visible = False
        '
        'grdChequesPropios
        '
        Me.grdChequesPropios.AllowUserToDeleteRows = False
        Me.grdChequesPropios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChequesPropios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.grdChequesPropios.Location = New System.Drawing.Point(3, 3)
        Me.grdChequesPropios.Name = "grdChequesPropios"
        Me.grdChequesPropios.ReadOnly = True
        Me.grdChequesPropios.Size = New System.Drawing.Size(379, 130)
        Me.grdChequesPropios.TabIndex = 117
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "NroCheque"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Banco"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 160
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Monto"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "IdCheque"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'btnAgregarChequePropio
        '
        Me.btnAgregarChequePropio.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarChequePropio.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarChequePropio.Location = New System.Drawing.Point(118, 139)
        Me.btnAgregarChequePropio.Name = "btnAgregarChequePropio"
        Me.btnAgregarChequePropio.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregarChequePropio.TabIndex = 7
        Me.btnAgregarChequePropio.Text = "Agregar"
        '
        'btnEliminarChequePropio
        '
        Me.btnEliminarChequePropio.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarChequePropio.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarChequePropio.Location = New System.Drawing.Point(199, 139)
        Me.btnEliminarChequePropio.Name = "btnEliminarChequePropio"
        Me.btnEliminarChequePropio.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminarChequePropio.TabIndex = 8
        Me.btnEliminarChequePropio.Text = "Eliminar"
        '
        'gpPago
        '
        Me.gpPago.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpPago.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpPago.Controls.Add(Me.rbOtros)
        Me.gpPago.Controls.Add(Me.rbPropios)
        Me.gpPago.Controls.Add(Me.dtiFechaPago)
        Me.gpPago.Controls.Add(Me.lblFechaDatosPerfo)
        Me.gpPago.Controls.Add(Me.txtRecargoTarjeta)
        Me.gpPago.Controls.Add(Me.txtEntregaContado)
        Me.gpPago.Controls.Add(Me.txtEntregaTarjeta)
        Me.gpPago.Controls.Add(Me.lblMontoRecargo)
        Me.gpPago.Controls.Add(Me.lblPorcRecargo)
        Me.gpPago.Controls.Add(Me.lblRecargo)
        Me.gpPago.Controls.Add(Me.lblPorcentaje)
        Me.gpPago.Controls.Add(Me.cmbTarjetas)
        Me.gpPago.Controls.Add(Me.lblEntregaCheque)
        Me.gpPago.Controls.Add(Me.Label3)
        Me.gpPago.Controls.Add(Me.lblEntregado)
        Me.gpPago.Controls.Add(Me.chkCheque)
        Me.gpPago.Controls.Add(Me.chkTarjeta)
        Me.gpPago.Controls.Add(Me.chkContado)
        Me.gpPago.Location = New System.Drawing.Point(418, 69)
        Me.gpPago.Name = "gpPago"
        Me.gpPago.Size = New System.Drawing.Size(361, 223)
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
        Me.gpPago.TabIndex = 142
        Me.gpPago.Text = "1) Forma de Pago..."
        '
        'rbOtros
        '
        Me.rbOtros.AutoSize = True
        Me.rbOtros.BackColor = System.Drawing.Color.Transparent
        Me.rbOtros.Location = New System.Drawing.Point(165, 145)
        Me.rbOtros.Name = "rbOtros"
        Me.rbOtros.Size = New System.Drawing.Size(50, 17)
        Me.rbOtros.TabIndex = 115
        Me.rbOtros.Text = "Otros"
        Me.rbOtros.UseVisualStyleBackColor = False
        '
        'rbPropios
        '
        Me.rbPropios.AutoSize = True
        Me.rbPropios.BackColor = System.Drawing.Color.Transparent
        Me.rbPropios.Checked = True
        Me.rbPropios.Location = New System.Drawing.Point(87, 145)
        Me.rbPropios.Name = "rbPropios"
        Me.rbPropios.Size = New System.Drawing.Size(60, 17)
        Me.rbPropios.TabIndex = 114
        Me.rbPropios.TabStop = True
        Me.rbPropios.Text = "Propios"
        Me.rbPropios.UseVisualStyleBackColor = False
        '
        'dtiFechaPago
        '
        '
        '
        '
        Me.dtiFechaPago.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtiFechaPago.ButtonDropDown.Visible = True
        Me.dtiFechaPago.Location = New System.Drawing.Point(74, 7)
        '
        '
        '
        Me.dtiFechaPago.MonthCalendar.AnnuallyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiFechaPago.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtiFechaPago.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtiFechaPago.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtiFechaPago.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiFechaPago.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtiFechaPago.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtiFechaPago.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtiFechaPago.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtiFechaPago.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtiFechaPago.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.dtiFechaPago.MonthCalendar.MarkedDates = New Date(-1) {}
        Me.dtiFechaPago.MonthCalendar.MonthlyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiFechaPago.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtiFechaPago.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiFechaPago.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtiFechaPago.MonthCalendar.TodayButtonVisible = True
        Me.dtiFechaPago.Name = "dtiFechaPago"
        Me.dtiFechaPago.Size = New System.Drawing.Size(85, 20)
        Me.dtiFechaPago.TabIndex = 0
        '
        'lblFechaDatosPerfo
        '
        Me.lblFechaDatosPerfo.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaDatosPerfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaDatosPerfo.Location = New System.Drawing.Point(3, 10)
        Me.lblFechaDatosPerfo.Name = "lblFechaDatosPerfo"
        Me.lblFechaDatosPerfo.SingleLineColor = System.Drawing.Color.Transparent
        Me.lblFechaDatosPerfo.Size = New System.Drawing.Size(65, 17)
        Me.lblFechaDatosPerfo.TabIndex = 113
        Me.lblFechaDatosPerfo.Text = "Fecha Pago"
        '
        'txtRecargoTarjeta
        '
        Me.txtRecargoTarjeta.Decimals = CType(2, Byte)
        Me.txtRecargoTarjeta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRecargoTarjeta.Enabled = False
        Me.txtRecargoTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecargoTarjeta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtRecargoTarjeta.Location = New System.Drawing.Point(110, 99)
        Me.txtRecargoTarjeta.Name = "txtRecargoTarjeta"
        Me.txtRecargoTarjeta.Size = New System.Drawing.Size(37, 20)
        Me.txtRecargoTarjeta.TabIndex = 6
        Me.txtRecargoTarjeta.Text = "0"
        Me.txtRecargoTarjeta.Text_1 = Nothing
        Me.txtRecargoTarjeta.Text_2 = Nothing
        Me.txtRecargoTarjeta.Text_3 = Nothing
        Me.txtRecargoTarjeta.Text_4 = Nothing
        Me.txtRecargoTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecargoTarjeta.UserValues = Nothing
        '
        'txtEntregaContado
        '
        Me.txtEntregaContado.Decimals = CType(2, Byte)
        Me.txtEntregaContado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEntregaContado.Enabled = False
        Me.txtEntregaContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntregaContado.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtEntregaContado.Location = New System.Drawing.Point(260, 43)
        Me.txtEntregaContado.Name = "txtEntregaContado"
        Me.txtEntregaContado.Size = New System.Drawing.Size(63, 20)
        Me.txtEntregaContado.TabIndex = 2
        Me.txtEntregaContado.Text = "0"
        Me.txtEntregaContado.Text_1 = Nothing
        Me.txtEntregaContado.Text_2 = Nothing
        Me.txtEntregaContado.Text_3 = Nothing
        Me.txtEntregaContado.Text_4 = Nothing
        Me.txtEntregaContado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEntregaContado.UserValues = Nothing
        '
        'txtEntregaTarjeta
        '
        Me.txtEntregaTarjeta.Decimals = CType(2, Byte)
        Me.txtEntregaTarjeta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtEntregaTarjeta.Enabled = False
        Me.txtEntregaTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntregaTarjeta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtEntregaTarjeta.Location = New System.Drawing.Point(260, 69)
        Me.txtEntregaTarjeta.Name = "txtEntregaTarjeta"
        Me.txtEntregaTarjeta.Size = New System.Drawing.Size(63, 20)
        Me.txtEntregaTarjeta.TabIndex = 5
        Me.txtEntregaTarjeta.Text = "0"
        Me.txtEntregaTarjeta.Text_1 = Nothing
        Me.txtEntregaTarjeta.Text_2 = Nothing
        Me.txtEntregaTarjeta.Text_3 = Nothing
        Me.txtEntregaTarjeta.Text_4 = Nothing
        Me.txtEntregaTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEntregaTarjeta.UserValues = Nothing
        '
        'lblMontoRecargo
        '
        Me.lblMontoRecargo.BackColor = System.Drawing.Color.White
        Me.lblMontoRecargo.Enabled = False
        Me.lblMontoRecargo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoRecargo.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoRecargo.Location = New System.Drawing.Point(260, 99)
        Me.lblMontoRecargo.Name = "lblMontoRecargo"
        Me.lblMontoRecargo.Size = New System.Drawing.Size(63, 19)
        Me.lblMontoRecargo.TabIndex = 7
        Me.lblMontoRecargo.Text = "0"
        Me.lblMontoRecargo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPorcRecargo
        '
        Me.lblPorcRecargo.AutoSize = True
        Me.lblPorcRecargo.BackColor = System.Drawing.Color.Transparent
        Me.lblPorcRecargo.Enabled = False
        Me.lblPorcRecargo.Location = New System.Drawing.Point(60, 101)
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
        Me.lblRecargo.Location = New System.Drawing.Point(177, 101)
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
        Me.lblPorcentaje.Location = New System.Drawing.Point(153, 100)
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
        Me.cmbTarjetas.Location = New System.Drawing.Point(70, 69)
        Me.cmbTarjetas.Name = "cmbTarjetas"
        Me.cmbTarjetas.Size = New System.Drawing.Size(184, 20)
        Me.cmbTarjetas.TabIndex = 4
        '
        'lblEntregaCheque
        '
        Me.lblEntregaCheque.BackColor = System.Drawing.Color.White
        Me.lblEntregaCheque.Enabled = False
        Me.lblEntregaCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregaCheque.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregaCheque.Location = New System.Drawing.Point(260, 123)
        Me.lblEntregaCheque.Name = "lblEntregaCheque"
        Me.lblEntregaCheque.Size = New System.Drawing.Size(63, 19)
        Me.lblEntregaCheque.TabIndex = 8
        Me.lblEntregaCheque.Text = "0"
        Me.lblEntregaCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(174, 180)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Entregado"
        '
        'lblEntregado
        '
        Me.lblEntregado.BackColor = System.Drawing.Color.White
        Me.lblEntregado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntregado.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblEntregado.Location = New System.Drawing.Point(260, 177)
        Me.lblEntregado.Name = "lblEntregado"
        Me.lblEntregado.Size = New System.Drawing.Size(63, 19)
        Me.lblEntregado.TabIndex = 13
        Me.lblEntregado.Text = "0"
        Me.lblEntregado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkCheque
        '
        Me.chkCheque.BackColor = System.Drawing.Color.Transparent
        Me.chkCheque.Location = New System.Drawing.Point(3, 120)
        Me.chkCheque.Name = "chkCheque"
        Me.chkCheque.Size = New System.Drawing.Size(79, 22)
        Me.chkCheque.TabIndex = 8
        Me.chkCheque.Text = "Cheque"
        '
        'chkTarjeta
        '
        Me.chkTarjeta.BackColor = System.Drawing.Color.Transparent
        Me.chkTarjeta.Location = New System.Drawing.Point(3, 69)
        Me.chkTarjeta.Name = "chkTarjeta"
        Me.chkTarjeta.Size = New System.Drawing.Size(61, 22)
        Me.chkTarjeta.TabIndex = 3
        Me.chkTarjeta.Text = "Tarjeta"
        '
        'chkContado
        '
        Me.chkContado.BackColor = System.Drawing.Color.Transparent
        Me.chkContado.Location = New System.Drawing.Point(3, 39)
        Me.chkContado.Name = "chkContado"
        Me.chkContado.Size = New System.Drawing.Size(61, 22)
        Me.chkContado.TabIndex = 1
        Me.chkContado.Text = "Contado"
        '
        'chkObra
        '
        Me.chkObra.AutoSize = True
        Me.chkObra.Location = New System.Drawing.Point(304, 32)
        Me.chkObra.Name = "chkObra"
        Me.chkObra.Size = New System.Drawing.Size(97, 17)
        Me.chkObra.TabIndex = 3
        Me.chkObra.Text = "Asignar A Obra"
        Me.chkObra.UseVisualStyleBackColor = True
        '
        'chkCuentaCorriente
        '
        Me.chkCuentaCorriente.AutoSize = True
        Me.chkCuentaCorriente.Location = New System.Drawing.Point(193, 32)
        Me.chkCuentaCorriente.Name = "chkCuentaCorriente"
        Me.chkCuentaCorriente.Size = New System.Drawing.Size(105, 17)
        Me.chkCuentaCorriente.TabIndex = 2
        Me.chkCuentaCorriente.Text = "Cuenta Corriente"
        Me.chkCuentaCorriente.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AccessibleName = ""
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(99, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 13)
        Me.Label14.TabIndex = 139
        Me.Label14.Text = "Fecha Gasto*"
        '
        'gpDetalle
        '
        Me.gpDetalle.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpDetalle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpDetalle.Controls.Add(Me.grdCheques)
        Me.gpDetalle.Controls.Add(Me.txtMontoCheque)
        Me.gpDetalle.Controls.Add(Me.txtObservaciones)
        Me.gpDetalle.Controls.Add(Me.LabelX7)
        Me.gpDetalle.Controls.Add(Me.LabelX6)
        Me.gpDetalle.Controls.Add(Me.cmbMoneda)
        Me.gpDetalle.Controls.Add(Me.btnAgregarCheque)
        Me.gpDetalle.Controls.Add(Me.btnEliminarCheque)
        Me.gpDetalle.Controls.Add(Me.txtPropietario)
        Me.gpDetalle.Controls.Add(Me.LabelX5)
        Me.gpDetalle.Controls.Add(Me.LabelX4)
        Me.gpDetalle.Controls.Add(Me.dtiVencimientoCheque)
        Me.gpDetalle.Controls.Add(Me.LabelX3)
        Me.gpDetalle.Controls.Add(Me.LabelX2)
        Me.gpDetalle.Controls.Add(Me.cmbBanco)
        Me.gpDetalle.Controls.Add(Me.txtNroCheque)
        Me.gpDetalle.Controls.Add(Me.LabelX1)
        Me.gpDetalle.Enabled = False
        Me.gpDetalle.Location = New System.Drawing.Point(810, 19)
        Me.gpDetalle.Name = "gpDetalle"
        Me.gpDetalle.Size = New System.Drawing.Size(391, 323)
        '
        '
        '
        Me.gpDetalle.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.gpDetalle.Style.BackColorGradientAngle = 90
        Me.gpDetalle.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.gpDetalle.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalle.Style.BorderBottomWidth = 1
        Me.gpDetalle.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.gpDetalle.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalle.Style.BorderLeftWidth = 1
        Me.gpDetalle.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalle.Style.BorderRightWidth = 1
        Me.gpDetalle.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalle.Style.BorderTopWidth = 1
        Me.gpDetalle.Style.CornerDiameter = 4
        Me.gpDetalle.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpDetalle.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.gpDetalle.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        Me.gpDetalle.TabIndex = 137
        Me.gpDetalle.Text = "2) Otros Cheques..."
        '
        'grdCheques
        '
        Me.grdCheques.AllowUserToDeleteRows = False
        Me.grdCheques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCheques.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NroCheque, Me.Banco, Me.Monto, Me.FechaVenc, Me.Propietario, Me.IdTipoMoneda, Me.Observaciones})
        Me.grdCheques.Location = New System.Drawing.Point(3, 3)
        Me.grdCheques.Name = "grdCheques"
        Me.grdCheques.ReadOnly = True
        Me.grdCheques.Size = New System.Drawing.Size(379, 149)
        Me.grdCheques.TabIndex = 117
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
        'txtMontoCheque
        '
        Me.txtMontoCheque.Decimals = CType(2, Byte)
        Me.txtMontoCheque.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoCheque.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoCheque.Location = New System.Drawing.Point(300, 227)
        Me.txtMontoCheque.Name = "txtMontoCheque"
        Me.txtMontoCheque.Size = New System.Drawing.Size(82, 20)
        Me.txtMontoCheque.TabIndex = 5
        Me.txtMontoCheque.Text_1 = Nothing
        Me.txtMontoCheque.Text_2 = Nothing
        Me.txtMontoCheque.Text_3 = Nothing
        Me.txtMontoCheque.Text_4 = Nothing
        Me.txtMontoCheque.UserValues = Nothing
        '
        'txtObservaciones
        '
        '
        '
        '
        Me.txtObservaciones.Border.Class = "TextBoxBorder"
        Me.txtObservaciones.Location = New System.Drawing.Point(3, 275)
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(208, 20)
        Me.txtObservaciones.TabIndex = 6
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        Me.LabelX7.Location = New System.Drawing.Point(3, 254)
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
        Me.LabelX6.Location = New System.Drawing.Point(220, 207)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX6.Size = New System.Drawing.Size(42, 15)
        Me.LabelX6.TabIndex = 113
        Me.LabelX6.Text = "Moneda"
        '
        'cmbMoneda
        '
        Me.cmbMoneda.DisplayMember = "Text"
        Me.cmbMoneda.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoneda.FormattingEnabled = True
        Me.cmbMoneda.ItemHeight = 14
        Me.cmbMoneda.Location = New System.Drawing.Point(220, 228)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(73, 20)
        Me.cmbMoneda.TabIndex = 4
        '
        'btnAgregarCheque
        '
        Me.btnAgregarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarCheque.Location = New System.Drawing.Point(226, 272)
        Me.btnAgregarCheque.Name = "btnAgregarCheque"
        Me.btnAgregarCheque.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregarCheque.TabIndex = 7
        Me.btnAgregarCheque.Text = "Agregar"
        '
        'btnEliminarCheque
        '
        Me.btnEliminarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarCheque.Location = New System.Drawing.Point(307, 272)
        Me.btnEliminarCheque.Name = "btnEliminarCheque"
        Me.btnEliminarCheque.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminarCheque.TabIndex = 8
        Me.btnEliminarCheque.Text = "Eliminar"
        '
        'txtPropietario
        '
        '
        '
        '
        Me.txtPropietario.Border.Class = "TextBoxBorder"
        Me.txtPropietario.Location = New System.Drawing.Point(3, 227)
        Me.txtPropietario.Name = "txtPropietario"
        Me.txtPropietario.Size = New System.Drawing.Size(208, 20)
        Me.txtPropietario.TabIndex = 3
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        Me.LabelX5.Location = New System.Drawing.Point(3, 206)
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
        Me.LabelX4.Location = New System.Drawing.Point(299, 206)
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
        Me.dtiVencimientoCheque.Location = New System.Drawing.Point(299, 179)
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
        Me.LabelX3.Location = New System.Drawing.Point(299, 158)
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
        Me.LabelX2.Location = New System.Drawing.Point(150, 158)
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
        Me.cmbBanco.Location = New System.Drawing.Point(150, 179)
        Me.cmbBanco.Name = "cmbBanco"
        Me.cmbBanco.Size = New System.Drawing.Size(143, 20)
        Me.cmbBanco.TabIndex = 1
        '
        'txtNroCheque
        '
        '
        '
        '
        Me.txtNroCheque.Border.Class = "TextBoxBorder"
        Me.txtNroCheque.Location = New System.Drawing.Point(3, 179)
        Me.txtNroCheque.Name = "txtNroCheque"
        Me.txtNroCheque.Size = New System.Drawing.Size(141, 20)
        Me.txtNroCheque.TabIndex = 0
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        Me.LabelX1.Location = New System.Drawing.Point(3, 158)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX1.Size = New System.Drawing.Size(81, 15)
        Me.LabelX1.TabIndex = 103
        Me.LabelX1.Text = "Nro de Cheque*"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(503, 15)
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
        Me.Label1.Location = New System.Drawing.Point(497, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "Id"
        Me.Label1.Visible = False
        '
        'txtTipoFactura
        '
        Me.txtTipoFactura.AccessibleName = "Nota"
        Me.txtTipoFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTipoFactura.Decimals = CType(2, Byte)
        Me.txtTipoFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtTipoFactura.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTipoFactura.Location = New System.Drawing.Point(13, 198)
        Me.txtTipoFactura.MaxLength = 1
        Me.txtTipoFactura.Name = "txtTipoFactura"
        Me.txtTipoFactura.Size = New System.Drawing.Size(64, 20)
        Me.txtTipoFactura.TabIndex = 7
        Me.txtTipoFactura.Text_1 = Nothing
        Me.txtTipoFactura.Text_2 = Nothing
        Me.txtTipoFactura.Text_3 = Nothing
        Me.txtTipoFactura.Text_4 = Nothing
        Me.txtTipoFactura.UserValues = Nothing
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(74, 221)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 133
        Me.Label12.Text = "Subtotal"
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = "Nota"
        Me.txtSubtotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotal.Location = New System.Drawing.Point(77, 237)
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(85, 20)
        Me.txtSubtotal.TabIndex = 10
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 221)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 13)
        Me.Label11.TabIndex = 131
        Me.Label11.Text = "IVA"
        '
        'txtIVA
        '
        Me.txtIVA.AccessibleName = "Nota"
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Decimals = CType(2, Byte)
        Me.txtIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtIVA.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtIVA.Location = New System.Drawing.Point(13, 237)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(61, 20)
        Me.txtIVA.TabIndex = 9
        Me.txtIVA.Text_1 = Nothing
        Me.txtIVA.Text_2 = Nothing
        Me.txtIVA.Text_3 = Nothing
        Me.txtIVA.Text_4 = Nothing
        Me.txtIVA.UserValues = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 182)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 13)
        Me.Label10.TabIndex = 129
        Me.Label10.Text = "Tipo Factura"
        '
        'picProveedores
        '
        Me.picProveedores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picProveedores.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picProveedores.Image = Global.SEYC.My.Resources.Resources.Info
        Me.picProveedores.Location = New System.Drawing.Point(352, 71)
        Me.picProveedores.Name = "picProveedores"
        Me.picProveedores.Size = New System.Drawing.Size(18, 20)
        Me.picProveedores.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picProveedores.TabIndex = 125
        Me.picProveedores.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 55)
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
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(13, 71)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(333, 21)
        Me.cmbProveedor.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(165, 221)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 122
        Me.Label7.Text = "Total*"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(82, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 13)
        Me.Label4.TabIndex = 121
        Me.Label4.Text = "Nro Factura / Remito"
        '
        'txtTotal
        '
        Me.txtTotal.AccessibleName = "Nota"
        Me.txtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTotal.Decimals = CType(2, Byte)
        Me.txtTotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtTotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTotal.Location = New System.Drawing.Point(168, 237)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(85, 20)
        Me.txtTotal.TabIndex = 11
        Me.txtTotal.Text_1 = Nothing
        Me.txtTotal.Text_2 = Nothing
        Me.txtTotal.Text_3 = Nothing
        Me.txtTotal.Text_4 = Nothing
        Me.txtTotal.UserValues = Nothing
        '
        'txtNroRemitoFactura
        '
        Me.txtNroRemitoFactura.AccessibleName = "Nota"
        Me.txtNroRemitoFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroRemitoFactura.Decimals = CType(2, Byte)
        Me.txtNroRemitoFactura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroRemitoFactura.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroRemitoFactura.Location = New System.Drawing.Point(82, 198)
        Me.txtNroRemitoFactura.MaxLength = 20
        Me.txtNroRemitoFactura.Name = "txtNroRemitoFactura"
        Me.txtNroRemitoFactura.Size = New System.Drawing.Size(232, 20)
        Me.txtNroRemitoFactura.TabIndex = 8
        Me.txtNroRemitoFactura.Text_1 = Nothing
        Me.txtNroRemitoFactura.Text_2 = Nothing
        Me.txtNroRemitoFactura.Text_3 = Nothing
        Me.txtNroRemitoFactura.Text_4 = Nothing
        Me.txtNroRemitoFactura.UserValues = Nothing
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(13, 276)
        Me.txtNota.MaxLength = 200
        Me.txtNota.Multiline = True
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(355, 54)
        Me.txtNota.TabIndex = 12
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 260)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'lblObra
        '
        Me.lblObra.AutoSize = True
        Me.lblObra.Enabled = False
        Me.lblObra.Location = New System.Drawing.Point(13, 135)
        Me.lblObra.Name = "lblObra"
        Me.lblObra.Size = New System.Drawing.Size(30, 13)
        Me.lblObra.TabIndex = 107
        Me.lblObra.Text = "Obra"
        '
        'lblCliente
        '
        Me.lblCliente.AutoSize = True
        Me.lblCliente.Enabled = False
        Me.lblCliente.Location = New System.Drawing.Point(13, 95)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(39, 13)
        Me.lblCliente.TabIndex = 106
        Me.lblCliente.Text = "Cliente"
        '
        'txtNroGasto
        '
        Me.txtNroGasto.AccessibleName = ""
        Me.txtNroGasto.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroGasto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroGasto.Decimals = CType(2, Byte)
        Me.txtNroGasto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroGasto.Enabled = False
        Me.txtNroGasto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroGasto.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroGasto.Location = New System.Drawing.Point(13, 32)
        Me.txtNroGasto.MaxLength = 25
        Me.txtNroGasto.Name = "txtNroGasto"
        Me.txtNroGasto.ReadOnly = True
        Me.txtNroGasto.Size = New System.Drawing.Size(82, 20)
        Me.txtNroGasto.TabIndex = 0
        Me.txtNroGasto.Text_1 = Nothing
        Me.txtNroGasto.Text_2 = Nothing
        Me.txtNroGasto.Text_3 = Nothing
        Me.txtNroGasto.Text_4 = Nothing
        Me.txtNroGasto.UserValues = Nothing
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
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = ""
        Me.cmbCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCliente.Enabled = False
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.Location = New System.Drawing.Point(13, 111)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(333, 21)
        Me.cmbCliente.TabIndex = 5
        '
        'PicClientes
        '
        Me.PicClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicClientes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicClientes.Enabled = False
        Me.PicClientes.Image = Global.SEYC.My.Resources.Resources.Info
        Me.PicClientes.Location = New System.Drawing.Point(352, 110)
        Me.PicClientes.Name = "PicClientes"
        Me.PicClientes.Size = New System.Drawing.Size(18, 20)
        Me.PicClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicClientes.TabIndex = 105
        Me.PicClientes.TabStop = False
        '
        'cmbObras
        '
        Me.cmbObras.AccessibleName = ""
        Me.cmbObras.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbObras.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbObras.Enabled = False
        Me.cmbObras.FormattingEnabled = True
        Me.cmbObras.Location = New System.Drawing.Point(13, 149)
        Me.cmbObras.Name = "cmbObras"
        Me.cmbObras.Size = New System.Drawing.Size(333, 21)
        Me.cmbObras.TabIndex = 6
        '
        'PicObras
        '
        Me.PicObras.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicObras.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PicObras.Enabled = False
        Me.PicObras.Image = Global.SEYC.My.Resources.Resources.Info
        Me.PicObras.Location = New System.Drawing.Point(352, 149)
        Me.PicObras.Name = "PicObras"
        Me.PicObras.Size = New System.Drawing.Size(18, 20)
        Me.PicObras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicObras.TabIndex = 105
        Me.PicObras.TabStop = False
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
        'dtiFechaGasto
        '
        Me.dtiFechaGasto.AccessibleName = "*FECHAGASTO"
        '
        '
        '
        Me.dtiFechaGasto.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtiFechaGasto.ButtonDropDown.Visible = True
        Me.dtiFechaGasto.Location = New System.Drawing.Point(102, 32)
        '
        '
        '
        Me.dtiFechaGasto.MonthCalendar.AnnuallyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiFechaGasto.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtiFechaGasto.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtiFechaGasto.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtiFechaGasto.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiFechaGasto.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtiFechaGasto.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtiFechaGasto.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtiFechaGasto.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtiFechaGasto.MonthCalendar.DisplayMonth = New Date(2011, 8, 1, 0, 0, 0, 0)
        Me.dtiFechaGasto.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.dtiFechaGasto.MonthCalendar.MarkedDates = New Date(-1) {}
        Me.dtiFechaGasto.MonthCalendar.MonthlyMarkedDates = New Date(-1) {}
        '
        '
        '
        Me.dtiFechaGasto.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtiFechaGasto.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtiFechaGasto.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtiFechaGasto.MonthCalendar.TodayButtonVisible = True
        Me.dtiFechaGasto.Name = "dtiFechaGasto"
        Me.dtiFechaGasto.Size = New System.Drawing.Size(85, 20)
        Me.dtiFechaGasto.TabIndex = 144
        '
        'frmGastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1257, 431)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "frmGastos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Administración de Gastos"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gpDetallePropios.ResumeLayout(False)
        CType(Me.grdChequesAsignados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdChequesPropios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpPago.ResumeLayout(False)
        Me.gpPago.PerformLayout()
        CType(Me.dtiFechaPago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpDetalle.ResumeLayout(False)
        Me.gpDetalle.PerformLayout()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtiVencimientoCheque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picProveedores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicClientes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicObras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.dtiFechaGasto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub



    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents PicClientes As System.Windows.Forms.PictureBox

    Friend WithEvents cmbObras As System.Windows.Forms.ComboBox
    Friend WithEvents PicObras As System.Windows.Forms.PictureBox


    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox


    Friend WithEvents txtNroGasto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label


    Friend WithEvents lblObra As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroRemitoFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents picProveedores As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTipoFactura As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gpDetalle As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grdCheques As System.Windows.Forms.DataGridView
    Friend WithEvents NroCheque As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Banco As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Monto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaVenc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Propietario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdTipoMoneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Observaciones As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtMontoCheque As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtObservaciones As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMoneda As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnAgregarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtPropietario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtiVencimientoCheque As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbBanco As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents txtNroCheque As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkCuentaCorriente As System.Windows.Forms.CheckBox
    Friend WithEvents chkObra As System.Windows.Forms.CheckBox
    Friend WithEvents gpPago As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents dtiFechaPago As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents lblFechaDatosPerfo As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtRecargoTarjeta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtEntregaContado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtEntregaTarjeta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblMontoRecargo As System.Windows.Forms.Label
    Friend WithEvents lblPorcRecargo As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblRecargo As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPorcentaje As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbTarjetas As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents lblEntregaCheque As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblEntregado As System.Windows.Forms.Label
    Friend WithEvents chkCheque As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkTarjeta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkContado As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents rbOtros As System.Windows.Forms.RadioButton
    Friend WithEvents rbPropios As System.Windows.Forms.RadioButton
    Friend WithEvents gpDetallePropios As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grdChequesAsignados As System.Windows.Forms.DataGridView
    Friend WithEvents grdChequesPropios As System.Windows.Forms.DataGridView
    Friend WithEvents btnAgregarChequePropio As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarChequePropio As DevComponents.DotNetBar.ButtonX
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtiFechaGasto As DevComponents.Editors.DateTimeAdv.DateTimeInput

End Class
