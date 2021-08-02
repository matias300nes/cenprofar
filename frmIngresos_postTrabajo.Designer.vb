<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIngresos_postTrabajo
    Inherits frmBase
    'System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Status_Msg = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblPaginas = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnEliminarEntrega = New DevComponents.DotNetBar.ButtonX
        Me.btnGuardarEntrega = New DevComponents.DotNetBar.ButtonX
        Me.btnCancelarEntrega = New DevComponents.DotNetBar.ButtonX
        Me.btnNuevaEntrega = New DevComponents.DotNetBar.ButtonX
        Me.lblMontoIva = New System.Windows.Forms.Label
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX
        Me.lblSubtotal = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbCliente = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.btnAgregarConsumo = New DevComponents.DotNetBar.ButtonX
        Me.btnEliminarConsumo = New DevComponents.DotNetBar.ButtonX
        Me.txtIntereses = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtRetensiones = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTotalAPagar = New TextBoxConFormatoVB.FormattedTextBoxVB
        Me.grdConsumosAPagar = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IvaaPagar = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdConsumosRealizados = New System.Windows.Forms.DataGridView
        Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Nro = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FechaConsumo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.IVA = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MontoConsumo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX
        Me.cmbObras = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX
        Me.gpPago = New DevComponents.DotNetBar.Controls.GroupPanel
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
        Me.ComboItem7 = New DevComponents.Editors.ComboItem
        Me.ComboItem8 = New DevComponents.Editors.ComboItem
        Me.lblEntregaCheque = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblEntregado = New System.Windows.Forms.Label
        Me.chkCheque = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkTarjeta = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.chkContado = New DevComponents.DotNetBar.Controls.CheckBoxX
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
        Me.ComboItem5 = New DevComponents.Editors.ComboItem
        Me.ComboItem6 = New DevComponents.Editors.ComboItem
        Me.btnAgregarCheque = New DevComponents.DotNetBar.ButtonX
        Me.btnEliminarCheque = New DevComponents.DotNetBar.ButtonX
        Me.txtPropietario = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX
        Me.dtiVencimientoCheque = New DevComponents.Editors.DateTimeAdv.DateTimeInput
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX
        Me.cmbBanco = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.ComboItem3 = New DevComponents.Editors.ComboItem
        Me.ComboItem4 = New DevComponents.Editors.ComboItem
        Me.txtNroCheque = New DevComponents.DotNetBar.Controls.TextBoxX
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX
        Me.grdItems = New DevComponents.DotNetBar.Controls.DataGridViewX
        Me.ComboItem10 = New DevComponents.Editors.ComboItem
        Me.ComboItem9 = New DevComponents.Editors.ComboItem
        Me.ComboItem2 = New DevComponents.Editors.ComboItem
        Me.ComboItem1 = New DevComponents.Editors.ComboItem
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdConsumosAPagar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdConsumosRealizados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpPago.SuspendLayout()
        CType(Me.dtiFechaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpDetalle.SuspendLayout()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtiVencimientoCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status_Msg})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 597)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1356, 22)
        Me.StatusStrip1.TabIndex = 18
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Status_Msg
        '
        Me.Status_Msg.Name = "Status_Msg"
        Me.Status_Msg.Size = New System.Drawing.Size(0, 17)
        '
        'lblPaginas
        '
        Me.lblPaginas.AutoSize = True
        Me.lblPaginas.Location = New System.Drawing.Point(15, 668)
        Me.lblPaginas.Name = "lblPaginas"
        Me.lblPaginas.Size = New System.Drawing.Size(51, 13)
        Me.lblPaginas.TabIndex = 114
        Me.lblPaginas.Text = "Paginas: "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnEliminarEntrega)
        Me.GroupBox1.Controls.Add(Me.btnGuardarEntrega)
        Me.GroupBox1.Controls.Add(Me.btnCancelarEntrega)
        Me.GroupBox1.Controls.Add(Me.btnNuevaEntrega)
        Me.GroupBox1.Controls.Add(Me.lblMontoIva)
        Me.GroupBox1.Controls.Add(Me.LabelX15)
        Me.GroupBox1.Controls.Add(Me.LabelX14)
        Me.GroupBox1.Controls.Add(Me.LabelX13)
        Me.GroupBox1.Controls.Add(Me.LabelX12)
        Me.GroupBox1.Controls.Add(Me.lblSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.btnAgregarConsumo)
        Me.GroupBox1.Controls.Add(Me.btnEliminarConsumo)
        Me.GroupBox1.Controls.Add(Me.txtIntereses)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtRetensiones)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtTotalAPagar)
        Me.GroupBox1.Controls.Add(Me.grdConsumosAPagar)
        Me.GroupBox1.Controls.Add(Me.grdConsumosRealizados)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.LabelX11)
        Me.GroupBox1.Controls.Add(Me.LabelX10)
        Me.GroupBox1.Controls.Add(Me.LabelX9)
        Me.GroupBox1.Controls.Add(Me.cmbObras)
        Me.GroupBox1.Controls.Add(Me.LabelX8)
        Me.GroupBox1.Controls.Add(Me.gpPago)
        Me.GroupBox1.Controls.Add(Me.gpDetalle)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1321, 348)
        Me.GroupBox1.TabIndex = 117
        Me.GroupBox1.TabStop = False
        '
        'btnEliminarEntrega
        '
        Me.btnEliminarEntrega.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarEntrega.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarEntrega.Location = New System.Drawing.Point(644, 16)
        Me.btnEliminarEntrega.Name = "btnEliminarEntrega"
        Me.btnEliminarEntrega.Size = New System.Drawing.Size(95, 23)
        Me.btnEliminarEntrega.TabIndex = 149
        Me.btnEliminarEntrega.Text = "Eliminar Entrega"
        Me.btnEliminarEntrega.Visible = False
        '
        'btnGuardarEntrega
        '
        Me.btnGuardarEntrega.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGuardarEntrega.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGuardarEntrega.Location = New System.Drawing.Point(442, 16)
        Me.btnGuardarEntrega.Name = "btnGuardarEntrega"
        Me.btnGuardarEntrega.Size = New System.Drawing.Size(95, 23)
        Me.btnGuardarEntrega.TabIndex = 148
        Me.btnGuardarEntrega.Text = "Guardar Entrega"
        Me.btnGuardarEntrega.Visible = False
        '
        'btnCancelarEntrega
        '
        Me.btnCancelarEntrega.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancelarEntrega.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancelarEntrega.Location = New System.Drawing.Point(543, 16)
        Me.btnCancelarEntrega.Name = "btnCancelarEntrega"
        Me.btnCancelarEntrega.Size = New System.Drawing.Size(95, 23)
        Me.btnCancelarEntrega.TabIndex = 147
        Me.btnCancelarEntrega.Text = "Cancelar Entrega"
        Me.btnCancelarEntrega.Visible = False
        '
        'btnNuevaEntrega
        '
        Me.btnNuevaEntrega.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNuevaEntrega.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNuevaEntrega.Location = New System.Drawing.Point(341, 16)
        Me.btnNuevaEntrega.Name = "btnNuevaEntrega"
        Me.btnNuevaEntrega.Size = New System.Drawing.Size(95, 23)
        Me.btnNuevaEntrega.TabIndex = 146
        Me.btnNuevaEntrega.Text = "Nueva Entrega"
        Me.btnNuevaEntrega.Visible = False
        '
        'lblMontoIva
        '
        Me.lblMontoIva.BackColor = System.Drawing.Color.White
        Me.lblMontoIva.Enabled = False
        Me.lblMontoIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoIva.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMontoIva.Location = New System.Drawing.Point(624, 304)
        Me.lblMontoIva.Name = "lblMontoIva"
        Me.lblMontoIva.Size = New System.Drawing.Size(80, 19)
        Me.lblMontoIva.TabIndex = 145
        Me.lblMontoIva.Text = "0"
        Me.lblMontoIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMontoIva.Visible = False
        '
        'LabelX15
        '
        Me.LabelX15.AutoSize = True
        Me.LabelX15.BackColor = System.Drawing.Color.Transparent
        Me.LabelX15.Enabled = False
        Me.LabelX15.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.Location = New System.Drawing.Point(446, 309)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX15.Size = New System.Drawing.Size(39, 12)
        Me.LabelX15.TabIndex = 144
        Me.LabelX15.Text = "(1)-(2)+(3)"
        '
        'LabelX14
        '
        Me.LabelX14.AutoSize = True
        Me.LabelX14.BackColor = System.Drawing.Color.Transparent
        Me.LabelX14.Enabled = False
        Me.LabelX14.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX14.Location = New System.Drawing.Point(356, 308)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX14.Size = New System.Drawing.Size(12, 12)
        Me.LabelX14.TabIndex = 143
        Me.LabelX14.Text = "(3)"
        '
        'LabelX13
        '
        Me.LabelX13.AutoSize = True
        Me.LabelX13.BackColor = System.Drawing.Color.Transparent
        Me.LabelX13.Enabled = False
        Me.LabelX13.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.Location = New System.Drawing.Point(207, 309)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX13.Size = New System.Drawing.Size(12, 12)
        Me.LabelX13.TabIndex = 142
        Me.LabelX13.Text = "(2)"
        '
        'LabelX12
        '
        Me.LabelX12.AutoSize = True
        Me.LabelX12.BackColor = System.Drawing.Color.Transparent
        Me.LabelX12.Enabled = False
        Me.LabelX12.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.Location = New System.Drawing.Point(458, 289)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX12.Size = New System.Drawing.Size(12, 12)
        Me.LabelX12.TabIndex = 141
        Me.LabelX12.Text = "(1)"
        '
        'lblSubtotal
        '
        Me.lblSubtotal.BackColor = System.Drawing.Color.White
        Me.lblSubtotal.Enabled = False
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtotal.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblSubtotal.Location = New System.Drawing.Point(372, 289)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(80, 19)
        Me.lblSubtotal.TabIndex = 140
        Me.lblSubtotal.Text = "0"
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(301, 289)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 16)
        Me.Label5.TabIndex = 139
        Me.Label5.Text = "Subtotal"
        '
        'cmbCliente
        '
        Me.cmbCliente.DisplayMember = "Text"
        Me.cmbCliente.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.ItemHeight = 14
        Me.cmbCliente.Location = New System.Drawing.Point(70, 17)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(247, 20)
        Me.cmbCliente.TabIndex = 138
        '
        'btnAgregarConsumo
        '
        Me.btnAgregarConsumo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarConsumo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarConsumo.Location = New System.Drawing.Point(228, 155)
        Me.btnAgregarConsumo.Name = "btnAgregarConsumo"
        Me.btnAgregarConsumo.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregarConsumo.TabIndex = 136
        Me.btnAgregarConsumo.Text = "Agregar"
        '
        'btnEliminarConsumo
        '
        Me.btnEliminarConsumo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarConsumo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarConsumo.Location = New System.Drawing.Point(334, 155)
        Me.btnEliminarConsumo.Name = "btnEliminarConsumo"
        Me.btnEliminarConsumo.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminarConsumo.TabIndex = 137
        Me.btnEliminarConsumo.Text = "Eliminar"
        '
        'txtIntereses
        '
        Me.txtIntereses.Decimals = CType(2, Byte)
        Me.txtIntereses.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIntereses.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIntereses.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtIntereses.Location = New System.Drawing.Point(303, 322)
        Me.txtIntereses.Name = "txtIntereses"
        Me.txtIntereses.Size = New System.Drawing.Size(63, 20)
        Me.txtIntereses.TabIndex = 135
        Me.txtIntereses.Text = "0"
        Me.txtIntereses.Text_1 = Nothing
        Me.txtIntereses.Text_2 = Nothing
        Me.txtIntereses.Text_3 = Nothing
        Me.txtIntereses.Text_4 = Nothing
        Me.txtIntereses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIntereses.UserValues = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label4.Location = New System.Drawing.Point(225, 324)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 134
        Me.Label4.Text = "Intereses"
        '
        'txtRetensiones
        '
        Me.txtRetensiones.Decimals = CType(2, Byte)
        Me.txtRetensiones.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtRetensiones.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRetensiones.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtRetensiones.Location = New System.Drawing.Point(156, 322)
        Me.txtRetensiones.Name = "txtRetensiones"
        Me.txtRetensiones.Size = New System.Drawing.Size(63, 20)
        Me.txtRetensiones.TabIndex = 133
        Me.txtRetensiones.Text = "0"
        Me.txtRetensiones.Text_1 = Nothing
        Me.txtRetensiones.Text_2 = Nothing
        Me.txtRetensiones.Text_3 = Nothing
        Me.txtRetensiones.Text_4 = Nothing
        Me.txtRetensiones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRetensiones.UserValues = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(55, 324)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 16)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Retensiones"
        '
        'txtTotalAPagar
        '
        Me.txtTotalAPagar.Decimals = CType(2, Byte)
        Me.txtTotalAPagar.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalAPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAPagar.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTotalAPagar.Location = New System.Drawing.Point(422, 322)
        Me.txtTotalAPagar.Name = "txtTotalAPagar"
        Me.txtTotalAPagar.Size = New System.Drawing.Size(63, 20)
        Me.txtTotalAPagar.TabIndex = 131
        Me.txtTotalAPagar.Text = "0"
        Me.txtTotalAPagar.Text_1 = Nothing
        Me.txtTotalAPagar.Text_2 = Nothing
        Me.txtTotalAPagar.Text_3 = Nothing
        Me.txtTotalAPagar.Text_4 = Nothing
        Me.txtTotalAPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalAPagar.UserValues = Nothing
        '
        'grdConsumosAPagar
        '
        Me.grdConsumosAPagar.AllowUserToDeleteRows = False
        Me.grdConsumosAPagar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConsumosAPagar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn3, Me.IvaaPagar, Me.DataGridViewTextBoxColumn2, Me.Column2})
        Me.grdConsumosAPagar.Location = New System.Drawing.Point(110, 183)
        Me.grdConsumosAPagar.Name = "grdConsumosAPagar"
        Me.grdConsumosAPagar.ReadOnly = True
        Me.grdConsumosAPagar.Size = New System.Drawing.Size(414, 102)
        Me.grdConsumosAPagar.TabIndex = 130
        '
        'Column1
        '
        Me.Column1.HeaderText = "Tipo"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 70
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Nro"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 60
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Fecha"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 70
        '
        'IvaaPagar
        '
        Me.IvaaPagar.HeaderText = "Iva"
        Me.IvaaPagar.Name = "IvaaPagar"
        Me.IvaaPagar.ReadOnly = True
        Me.IvaaPagar.Width = 70
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Total"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'Column2
        '
        Me.Column2.HeaderText = "id"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        Me.Column2.Width = 70
        '
        'grdConsumosRealizados
        '
        Me.grdConsumosRealizados.AllowUserToDeleteRows = False
        Me.grdConsumosRealizados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConsumosRealizados.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Tipo, Me.Nro, Me.FechaConsumo, Me.IVA, Me.MontoConsumo, Me.Id})
        Me.grdConsumosRealizados.Location = New System.Drawing.Point(110, 45)
        Me.grdConsumosRealizados.Name = "grdConsumosRealizados"
        Me.grdConsumosRealizados.ReadOnly = True
        Me.grdConsumosRealizados.Size = New System.Drawing.Size(414, 102)
        Me.grdConsumosRealizados.TabIndex = 129
        '
        'Tipo
        '
        Me.Tipo.Frozen = True
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        Me.Tipo.ReadOnly = True
        Me.Tipo.Width = 70
        '
        'Nro
        '
        Me.Nro.Frozen = True
        Me.Nro.HeaderText = "Nro"
        Me.Nro.Name = "Nro"
        Me.Nro.ReadOnly = True
        Me.Nro.Width = 60
        '
        'FechaConsumo
        '
        Me.FechaConsumo.Frozen = True
        Me.FechaConsumo.HeaderText = "Fecha"
        Me.FechaConsumo.Name = "FechaConsumo"
        Me.FechaConsumo.ReadOnly = True
        Me.FechaConsumo.Width = 70
        '
        'IVA
        '
        Me.IVA.Frozen = True
        Me.IVA.HeaderText = "Iva"
        Me.IVA.Name = "IVA"
        Me.IVA.ReadOnly = True
        Me.IVA.Width = 70
        '
        'MontoConsumo
        '
        Me.MontoConsumo.Frozen = True
        Me.MontoConsumo.HeaderText = "Total"
        Me.MontoConsumo.Name = "MontoConsumo"
        Me.MontoConsumo.ReadOnly = True
        Me.MontoConsumo.Width = 80
        '
        'Id
        '
        Me.Id.Frozen = True
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        Me.Id.Width = 70
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(372, 324)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 128
        Me.Label1.Text = "Total"
        '
        'LabelX11
        '
        Me.LabelX11.AutoSize = True
        Me.LabelX11.BackColor = System.Drawing.Color.Transparent
        Me.LabelX11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX11.Location = New System.Drawing.Point(54, 184)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX11.Size = New System.Drawing.Size(50, 15)
        Me.LabelX11.TabIndex = 126
        Me.LabelX11.Text = "A pagar..."
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        Me.LabelX10.BackColor = System.Drawing.Color.Transparent
        Me.LabelX10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.Location = New System.Drawing.Point(6, 45)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX10.Size = New System.Drawing.Size(98, 15)
        Me.LabelX10.TabIndex = 124
        Me.LabelX10.Text = "Consumos / Ventas"
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        Me.LabelX9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.Location = New System.Drawing.Point(323, 19)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX9.Size = New System.Drawing.Size(27, 15)
        Me.LabelX9.TabIndex = 123
        Me.LabelX9.Text = "Obra"
        '
        'cmbObras
        '
        Me.cmbObras.DisplayMember = "Text"
        Me.cmbObras.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbObras.FormattingEnabled = True
        Me.cmbObras.ItemHeight = 14
        Me.cmbObras.Location = New System.Drawing.Point(356, 19)
        Me.cmbObras.Name = "cmbObras"
        Me.cmbObras.Size = New System.Drawing.Size(168, 20)
        Me.cmbObras.TabIndex = 122
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        Me.LabelX8.BackColor = System.Drawing.Color.Transparent
        Me.LabelX8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.Location = New System.Drawing.Point(27, 19)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.SingleLineColor = System.Drawing.Color.Transparent
        Me.LabelX8.Size = New System.Drawing.Size(37, 15)
        Me.LabelX8.TabIndex = 121
        Me.LabelX8.Text = "Cliente"
        '
        'gpPago
        '
        Me.gpPago.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpPago.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
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
        Me.gpPago.Controls.Add(Me.Label2)
        Me.gpPago.Controls.Add(Me.lblEntregado)
        Me.gpPago.Controls.Add(Me.chkCheque)
        Me.gpPago.Controls.Add(Me.chkTarjeta)
        Me.gpPago.Controls.Add(Me.chkContado)
        Me.gpPago.Enabled = False
        Me.gpPago.Location = New System.Drawing.Point(565, 78)
        Me.gpPago.Name = "gpPago"
        Me.gpPago.Size = New System.Drawing.Size(338, 207)
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
        Me.gpPago.TabIndex = 116
        Me.gpPago.Text = "1) Forma de Pago..."
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
        Me.dtiFechaPago.TabIndex = 112
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
        Me.lblMontoRecargo.TabIndex = 111
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
        Me.cmbTarjetas.Items.AddRange(New Object() {Me.ComboItem7, Me.ComboItem8})
        Me.cmbTarjetas.Location = New System.Drawing.Point(70, 69)
        Me.cmbTarjetas.Name = "cmbTarjetas"
        Me.cmbTarjetas.Size = New System.Drawing.Size(184, 20)
        Me.cmbTarjetas.TabIndex = 4
        '
        'ComboItem7
        '
        Me.ComboItem7.Text = "Mensual"
        '
        'ComboItem8
        '
        Me.ComboItem8.Text = "Quincenal"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(174, 163)
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
        Me.lblEntregado.Location = New System.Drawing.Point(260, 160)
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
        Me.chkCheque.TabIndex = 7
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
        Me.gpDetalle.Location = New System.Drawing.Point(909, 19)
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
        Me.gpDetalle.TabIndex = 118
        Me.gpDetalle.Text = "2) Detalle de Cheques..."
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
        Me.txtMontoCheque.TabIndex = 6
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
        Me.txtObservaciones.TabIndex = 7
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
        Me.cmbMoneda.Items.AddRange(New Object() {Me.ComboItem5, Me.ComboItem6})
        Me.cmbMoneda.Location = New System.Drawing.Point(220, 228)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(73, 20)
        Me.cmbMoneda.TabIndex = 5
        '
        'ComboItem5
        '
        Me.ComboItem5.Text = "Mensual"
        '
        'ComboItem6
        '
        Me.ComboItem6.Text = "Quincenal"
        '
        'btnAgregarCheque
        '
        Me.btnAgregarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAgregarCheque.Location = New System.Drawing.Point(226, 272)
        Me.btnAgregarCheque.Name = "btnAgregarCheque"
        Me.btnAgregarCheque.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregarCheque.TabIndex = 8
        Me.btnAgregarCheque.Text = "Agregar"
        '
        'btnEliminarCheque
        '
        Me.btnEliminarCheque.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarCheque.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarCheque.Location = New System.Drawing.Point(307, 272)
        Me.btnEliminarCheque.Name = "btnEliminarCheque"
        Me.btnEliminarCheque.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminarCheque.TabIndex = 9
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
        Me.txtPropietario.TabIndex = 4
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
        Me.dtiVencimientoCheque.TabIndex = 3
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
        Me.cmbBanco.Items.AddRange(New Object() {Me.ComboItem3, Me.ComboItem4})
        Me.cmbBanco.Location = New System.Drawing.Point(150, 179)
        Me.cmbBanco.Name = "cmbBanco"
        Me.cmbBanco.Size = New System.Drawing.Size(143, 20)
        Me.cmbBanco.TabIndex = 2
        '
        'ComboItem3
        '
        Me.ComboItem3.Text = "Mensual"
        '
        'ComboItem4
        '
        Me.ComboItem4.Text = "Quincenal"
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
        Me.txtNroCheque.TabIndex = 1
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
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle1
        Me.grdItems.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.grdItems.Location = New System.Drawing.Point(6, 154)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.ReadOnly = True
        Me.grdItems.Size = New System.Drawing.Size(553, 132)
        Me.grdItems.TabIndex = 117
        Me.grdItems.Visible = False
        '
        'ComboItem10
        '
        Me.ComboItem10.Text = "Quincenal"
        '
        'ComboItem9
        '
        Me.ComboItem9.Text = "Mensual"
        '
        'ComboItem2
        '
        Me.ComboItem2.Text = "Quincenal"
        '
        'ComboItem1
        '
        Me.ComboItem1.Text = "Mensual"
        '
        'frmIngresos_postTrabajo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1356, 640)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblPaginas)
        Me.Name = "frmIngresos_postTrabajo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmIngresos_postTrabajo"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.lblPaginas, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdConsumosAPagar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdConsumosRealizados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpPago.ResumeLayout(False)
        Me.gpPago.PerformLayout()
        CType(Me.dtiFechaPago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpDetalle.ResumeLayout(False)
        Me.gpDetalle.PerformLayout()
        CType(Me.grdCheques, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtiVencimientoCheque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    'Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    'Friend WithEvents FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents FiltroExcluyendoLaSelecciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents FiltrarPorToolStrip As System.Windows.Forms.ToolStripTextBox
    'Friend WithEvents QuitarElFitroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripTextBox
    'Friend WithEvents ToolMovilizar As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Status_Msg As System.Windows.Forms.ToolStripStatusLabel
    'Friend WithEvents ToolStripPagina As System.Windows.Forms.ComboBox
    Friend WithEvents lblPaginas As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
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
    Friend WithEvents ComboItem5 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem6 As DevComponents.Editors.ComboItem
    Friend WithEvents btnAgregarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarCheque As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtPropietario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtiVencimientoCheque As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbBanco As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem3 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem4 As DevComponents.Editors.ComboItem
    Friend WithEvents txtNroCheque As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents grdItems As DevComponents.DotNetBar.Controls.DataGridViewX
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
    Friend WithEvents ComboItem7 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem8 As DevComponents.Editors.ComboItem
    Friend WithEvents lblEntregaCheque As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblEntregado As System.Windows.Forms.Label
    Friend WithEvents chkCheque As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkTarjeta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkContado As DevComponents.DotNetBar.Controls.CheckBoxX
    'Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbObras As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents ComboItem10 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem9 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem2 As DevComponents.Editors.ComboItem
    Friend WithEvents ComboItem1 As DevComponents.Editors.ComboItem
    'Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents grdConsumosAPagar As System.Windows.Forms.DataGridView
    Friend WithEvents grdConsumosRealizados As System.Windows.Forms.DataGridView
    'Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents txtRetensiones As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAPagar As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIntereses As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnAgregarConsumo As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEliminarConsumo As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cmbCliente As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMontoIva As System.Windows.Forms.Label
    Friend WithEvents btnNuevaEntrega As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCancelarEntrega As DevComponents.DotNetBar.ButtonX
    Friend Shadows WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnGuardarEntrega As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IvaaPagar As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tipo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaConsumo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MontoConsumo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnEliminarEntrega As DevComponents.DotNetBar.ButtonX
End Class
