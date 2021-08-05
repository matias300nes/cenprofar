<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecepciones
    Inherits frmBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIdGastoAsociar = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtIdComprobante = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdMoneda = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNroFacturaCompletoControl = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNroRemitoControl = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdImpuestos = New System.Windows.Forms.DataGridView()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbAlmacenes = New System.Windows.Forms.ComboBox()
        Me.chkGrillaInferior = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblMontoIva = New System.Windows.Forms.Label()
        Me.chkFacturaCancelada = New System.Windows.Forms.CheckBox()
        Me.txtidpago = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdGasto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdProveedor = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNota = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.btnRecibirTodos = New System.Windows.Forms.Button()
        Me.btnLlenarGrilla = New System.Windows.Forms.Button()
        Me.cmbOrdenDeCompra = New System.Windows.Forms.ComboBox()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkMostarColumnas = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkAnuladas = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStripIVA = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItemIVA = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdImpuestos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStripIVA.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtIdGastoAsociar)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtIdComprobante)
        Me.GroupBox1.Controls.Add(Me.txtIdMoneda)
        Me.GroupBox1.Controls.Add(Me.txtNroFacturaCompletoControl)
        Me.GroupBox1.Controls.Add(Me.txtNroRemitoControl)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdImpuestos)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.cmbAlmacenes)
        Me.GroupBox1.Controls.Add(Me.chkGrillaInferior)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblMontoIva)
        Me.GroupBox1.Controls.Add(Me.chkFacturaCancelada)
        Me.GroupBox1.Controls.Add(Me.txtidpago)
        Me.GroupBox1.Controls.Add(Me.txtIdGasto)
        Me.GroupBox1.Controls.Add(Me.txtIdProveedor)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtNota)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbProveedor)
        Me.GroupBox1.Controls.Add(Me.btnRecibirTodos)
        Me.GroupBox1.Controls.Add(Me.btnLlenarGrilla)
        Me.GroupBox1.Controls.Add(Me.cmbOrdenDeCompra)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkMostarColumnas)
        Me.GroupBox1.Controls.Add(Me.chkAnuladas)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Blue
        Me.GroupBox1.Location = New System.Drawing.Point(9, 30)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(2249, 654)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtIdGastoAsociar
        '
        Me.txtIdGastoAsociar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdGastoAsociar.Decimals = CType(2, Byte)
        Me.txtIdGastoAsociar.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdGastoAsociar.Enabled = False
        Me.txtIdGastoAsociar.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdGastoAsociar.Location = New System.Drawing.Point(1156, 38)
        Me.txtIdGastoAsociar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIdGastoAsociar.MaxLength = 8
        Me.txtIdGastoAsociar.Name = "txtIdGastoAsociar"
        Me.txtIdGastoAsociar.Size = New System.Drawing.Size(45, 22)
        Me.txtIdGastoAsociar.TabIndex = 344
        Me.txtIdGastoAsociar.Text_1 = Nothing
        Me.txtIdGastoAsociar.Text_2 = Nothing
        Me.txtIdGastoAsociar.Text_3 = Nothing
        Me.txtIdGastoAsociar.Text_4 = Nothing
        Me.txtIdGastoAsociar.UserValues = Nothing
        Me.txtIdGastoAsociar.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(67, 86)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(24, 31)
        Me.Label24.TabIndex = 300
        Me.Label24.Text = "-"
        '
        'txtIdComprobante
        '
        Me.txtIdComprobante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdComprobante.Decimals = CType(2, Byte)
        Me.txtIdComprobante.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdComprobante.Enabled = False
        Me.txtIdComprobante.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdComprobante.Location = New System.Drawing.Point(557, 74)
        Me.txtIdComprobante.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIdComprobante.MaxLength = 8
        Me.txtIdComprobante.Name = "txtIdComprobante"
        Me.txtIdComprobante.Size = New System.Drawing.Size(29, 22)
        Me.txtIdComprobante.TabIndex = 297
        Me.txtIdComprobante.Text_1 = Nothing
        Me.txtIdComprobante.Text_2 = Nothing
        Me.txtIdComprobante.Text_3 = Nothing
        Me.txtIdComprobante.Text_4 = Nothing
        Me.txtIdComprobante.UserValues = Nothing
        Me.txtIdComprobante.Visible = False
        '
        'txtIdMoneda
        '
        Me.txtIdMoneda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdMoneda.Decimals = CType(2, Byte)
        Me.txtIdMoneda.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdMoneda.Enabled = False
        Me.txtIdMoneda.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdMoneda.Location = New System.Drawing.Point(1049, 123)
        Me.txtIdMoneda.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIdMoneda.MaxLength = 8
        Me.txtIdMoneda.Name = "txtIdMoneda"
        Me.txtIdMoneda.Size = New System.Drawing.Size(29, 22)
        Me.txtIdMoneda.TabIndex = 296
        Me.txtIdMoneda.Text_1 = Nothing
        Me.txtIdMoneda.Text_2 = Nothing
        Me.txtIdMoneda.Text_3 = Nothing
        Me.txtIdMoneda.Text_4 = Nothing
        Me.txtIdMoneda.UserValues = Nothing
        Me.txtIdMoneda.Visible = False
        '
        'txtNroFacturaCompletoControl
        '
        Me.txtNroFacturaCompletoControl.AccessibleName = ""
        Me.txtNroFacturaCompletoControl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroFacturaCompletoControl.Decimals = CType(2, Byte)
        Me.txtNroFacturaCompletoControl.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNroFacturaCompletoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroFacturaCompletoControl.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroFacturaCompletoControl.Location = New System.Drawing.Point(821, 311)
        Me.txtNroFacturaCompletoControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNroFacturaCompletoControl.MaxLength = 20
        Me.txtNroFacturaCompletoControl.Name = "txtNroFacturaCompletoControl"
        Me.txtNroFacturaCompletoControl.ReadOnly = True
        Me.txtNroFacturaCompletoControl.Size = New System.Drawing.Size(160, 23)
        Me.txtNroFacturaCompletoControl.TabIndex = 287
        Me.txtNroFacturaCompletoControl.Text_1 = Nothing
        Me.txtNroFacturaCompletoControl.Text_2 = Nothing
        Me.txtNroFacturaCompletoControl.Text_3 = Nothing
        Me.txtNroFacturaCompletoControl.Text_4 = Nothing
        Me.txtNroFacturaCompletoControl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtNroFacturaCompletoControl.UserValues = Nothing
        Me.txtNroFacturaCompletoControl.Visible = False
        '
        'txtNroRemitoControl
        '
        Me.txtNroRemitoControl.AccessibleName = ""
        Me.txtNroRemitoControl.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroRemitoControl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroRemitoControl.Decimals = CType(2, Byte)
        Me.txtNroRemitoControl.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroRemitoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroRemitoControl.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroRemitoControl.Location = New System.Drawing.Point(797, 337)
        Me.txtNroRemitoControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNroRemitoControl.MaxLength = 25
        Me.txtNroRemitoControl.Name = "txtNroRemitoControl"
        Me.txtNroRemitoControl.Size = New System.Drawing.Size(184, 23)
        Me.txtNroRemitoControl.TabIndex = 286
        Me.txtNroRemitoControl.Text_1 = Nothing
        Me.txtNroRemitoControl.Text_2 = Nothing
        Me.txtNroRemitoControl.Text_3 = Nothing
        Me.txtNroRemitoControl.Text_4 = Nothing
        Me.txtNroRemitoControl.UserValues = Nothing
        Me.txtNroRemitoControl.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1153, 70)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 17)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Detalle de Impuestos"
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdImpuestos.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdImpuestos.Location = New System.Drawing.Point(1157, 86)
        Me.grdImpuestos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdImpuestos.Name = "grdImpuestos"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdImpuestos.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdImpuestos.Size = New System.Drawing.Size(597, 164)
        Me.grdImpuestos.TabIndex = 25
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(271, 18)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 17)
        Me.Label16.TabIndex = 274
        Me.Label16.Text = "Depósito*"
        '
        'cmbAlmacenes
        '
        Me.cmbAlmacenes.AccessibleName = "*Depósito"
        Me.cmbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAlmacenes.DropDownHeight = 500
        Me.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAlmacenes.FormattingEnabled = True
        Me.cmbAlmacenes.IntegralHeight = False
        Me.cmbAlmacenes.Location = New System.Drawing.Point(271, 38)
        Me.cmbAlmacenes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbAlmacenes.Name = "cmbAlmacenes"
        Me.cmbAlmacenes.Size = New System.Drawing.Size(120, 25)
        Me.cmbAlmacenes.TabIndex = 2
        '
        'chkGrillaInferior
        '
        Me.chkGrillaInferior.AutoSize = True
        Me.chkGrillaInferior.Location = New System.Drawing.Point(256, 625)
        Me.chkGrillaInferior.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkGrillaInferior.Name = "chkGrillaInferior"
        Me.chkGrillaInferior.Size = New System.Drawing.Size(176, 21)
        Me.chkGrillaInferior.TabIndex = 272
        Me.chkGrillaInferior.Text = "Aumentar Grilla Inferior"
        Me.chkGrillaInferior.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 628)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(129, 17)
        Me.Label19.TabIndex = 271
        Me.Label19.Text = "Cantidad de Ítems: "
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.Location = New System.Drawing.Point(145, 628)
        Me.lblCantidadFilas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(60, 17)
        Me.lblCantidadFilas.TabIndex = 270
        Me.lblCantidadFilas.Text = "Subtotal"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(708, 86)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(24, 31)
        Me.Label14.TabIndex = 264
        Me.Label14.Text = "-"
        '
        'lblMontoIva
        '
        Me.lblMontoIva.BackColor = System.Drawing.Color.White
        Me.lblMontoIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMontoIva.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoIva.Location = New System.Drawing.Point(869, 369)
        Me.lblMontoIva.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMontoIva.Name = "lblMontoIva"
        Me.lblMontoIva.Size = New System.Drawing.Size(113, 25)
        Me.lblMontoIva.TabIndex = 17
        Me.lblMontoIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMontoIva.Visible = False
        '
        'chkFacturaCancelada
        '
        Me.chkFacturaCancelada.AutoSize = True
        Me.chkFacturaCancelada.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFacturaCancelada.Location = New System.Drawing.Point(389, 204)
        Me.chkFacturaCancelada.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkFacturaCancelada.Name = "chkFacturaCancelada"
        Me.chkFacturaCancelada.Size = New System.Drawing.Size(204, 22)
        Me.chkFacturaCancelada.TabIndex = 27
        Me.chkFacturaCancelada.Text = "Pago Efectivo Contado"
        Me.chkFacturaCancelada.UseVisualStyleBackColor = True
        '
        'txtidpago
        '
        Me.txtidpago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidpago.Decimals = CType(2, Byte)
        Me.txtidpago.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidpago.Enabled = False
        Me.txtidpago.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidpago.Location = New System.Drawing.Point(1444, 288)
        Me.txtidpago.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtidpago.MaxLength = 8
        Me.txtidpago.Name = "txtidpago"
        Me.txtidpago.Size = New System.Drawing.Size(29, 22)
        Me.txtidpago.TabIndex = 192
        Me.txtidpago.Text_1 = Nothing
        Me.txtidpago.Text_2 = Nothing
        Me.txtidpago.Text_3 = Nothing
        Me.txtidpago.Text_4 = Nothing
        Me.txtidpago.UserValues = Nothing
        Me.txtidpago.Visible = False
        '
        'txtIdGasto
        '
        Me.txtIdGasto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdGasto.Decimals = CType(2, Byte)
        Me.txtIdGasto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdGasto.Enabled = False
        Me.txtIdGasto.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdGasto.Location = New System.Drawing.Point(1755, 92)
        Me.txtIdGasto.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIdGasto.MaxLength = 8
        Me.txtIdGasto.Name = "txtIdGasto"
        Me.txtIdGasto.Size = New System.Drawing.Size(45, 22)
        Me.txtIdGasto.TabIndex = 191
        Me.txtIdGasto.Text_1 = Nothing
        Me.txtIdGasto.Text_2 = Nothing
        Me.txtIdGasto.Text_3 = Nothing
        Me.txtIdGasto.Text_4 = Nothing
        Me.txtIdGasto.UserValues = Nothing
        Me.txtIdGasto.Visible = False
        '
        'txtIdProveedor
        '
        Me.txtIdProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdProveedor.Decimals = CType(2, Byte)
        Me.txtIdProveedor.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProveedor.Enabled = False
        Me.txtIdProveedor.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdProveedor.Location = New System.Drawing.Point(749, 15)
        Me.txtIdProveedor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIdProveedor.MaxLength = 8
        Me.txtIdProveedor.Name = "txtIdProveedor"
        Me.txtIdProveedor.Size = New System.Drawing.Size(29, 22)
        Me.txtIdProveedor.TabIndex = 130
        Me.txtIdProveedor.Text_1 = Nothing
        Me.txtIdProveedor.Text_2 = Nothing
        Me.txtIdProveedor.Text_3 = Nothing
        Me.txtIdProveedor.Text_4 = Nothing
        Me.txtIdProveedor.UserValues = Nothing
        Me.txtIdProveedor.Visible = False
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(1360, 288)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(29, 22)
        Me.txtID.TabIndex = 50
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'txtNota
        '
        Me.txtNota.AccessibleName = "Nota"
        Me.txtNota.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNota.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Decimals = CType(2, Byte)
        Me.txtNota.DecSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.txtNota.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNota.Location = New System.Drawing.Point(1492, 38)
        Me.txtNota.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Size = New System.Drawing.Size(308, 22)
        Me.txtNota.TabIndex = 8
        Me.txtNota.Text_1 = Nothing
        Me.txtNota.Text_2 = Nothing
        Me.txtNota.Text_3 = Nothing
        Me.txtNota.Text_4 = Nothing
        Me.txtNota.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1488, 20)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 17)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Nota"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AccessibleName = "*Proveedor"
        Me.cmbProveedor.DropDownHeight = 500
        Me.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.IntegralHeight = False
        Me.cmbProveedor.Location = New System.Drawing.Point(667, 38)
        Me.cmbProveedor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(313, 25)
        Me.cmbProveedor.TabIndex = 4
        '
        'btnRecibirTodos
        '
        Me.btnRecibirTodos.Enabled = False
        Me.btnRecibirTodos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecibirTodos.Location = New System.Drawing.Point(139, 202)
        Me.btnRecibirTodos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRecibirTodos.Name = "btnRecibirTodos"
        Me.btnRecibirTodos.Size = New System.Drawing.Size(216, 28)
        Me.btnRecibirTodos.TabIndex = 26
        Me.btnRecibirTodos.Text = "Recibir Todos"
        Me.btnRecibirTodos.UseVisualStyleBackColor = True
        '
        'btnLlenarGrilla
        '
        Me.btnLlenarGrilla.Location = New System.Drawing.Point(53, 288)
        Me.btnLlenarGrilla.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLlenarGrilla.Name = "btnLlenarGrilla"
        Me.btnLlenarGrilla.Size = New System.Drawing.Size(153, 28)
        Me.btnLlenarGrilla.TabIndex = 12
        Me.btnLlenarGrilla.Text = "Llenar Grilla"
        Me.btnLlenarGrilla.UseVisualStyleBackColor = True
        Me.btnLlenarGrilla.Visible = False
        '
        'cmbOrdenDeCompra
        '
        Me.cmbOrdenDeCompra.AccessibleName = "*OrdenCompra"
        Me.cmbOrdenDeCompra.FormattingEnabled = True
        Me.cmbOrdenDeCompra.Location = New System.Drawing.Point(988, 38)
        Me.cmbOrdenDeCompra.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbOrdenDeCompra.Name = "cmbOrdenDeCompra"
        Me.cmbOrdenDeCompra.Size = New System.Drawing.Size(91, 24)
        Me.cmbOrdenDeCompra.TabIndex = 5
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.Enabled = False
        Me.chkEliminado.Location = New System.Drawing.Point(227, 295)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(91, 21)
        Me.chkEliminado.TabIndex = 116
        Me.chkEliminado.Text = "Eliminado"
        Me.chkEliminado.UseVisualStyleBackColor = True
        Me.chkEliminado.Visible = False
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdItems.Location = New System.Drawing.Point(16, 257)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdItems.Name = "grdItems"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdItems.Size = New System.Drawing.Size(2225, 359)
        Me.grdItems.TabIndex = 29
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
        Me.txtCODIGO.Location = New System.Drawing.Point(17, 38)
        Me.txtCODIGO.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(100, 23)
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
        Me.Label2.Location = New System.Drawing.Point(13, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 17)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Nro Recepción"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(127, 38)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 22)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(123, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 17)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Fecha"
        '
        'chkMostarColumnas
        '
        '
        '
        '
        Me.chkMostarColumnas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkMostarColumnas.Location = New System.Drawing.Point(1428, 188)
        Me.chkMostarColumnas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkMostarColumnas.Name = "chkMostarColumnas"
        Me.chkMostarColumnas.Size = New System.Drawing.Size(172, 28)
        Me.chkMostarColumnas.TabIndex = 22
        Me.chkMostarColumnas.Text = "Mostrar Bonif2, etc..."
        Me.chkMostarColumnas.TextColor = System.Drawing.Color.Maroon
        '
        'chkAnuladas
        '
        Me.chkAnuladas.AccessibleName = ""
        Me.chkAnuladas.AutoSize = True
        Me.chkAnuladas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnuladas.ForeColor = System.Drawing.Color.Red
        Me.chkAnuladas.Location = New System.Drawing.Point(869, 223)
        Me.chkAnuladas.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkAnuladas.Name = "chkAnuladas"
        Me.chkAnuladas.Size = New System.Drawing.Size(225, 21)
        Me.chkAnuladas.TabIndex = 28
        Me.chkAnuladas.Text = "Ver Recepciones Anuladas"
        Me.chkAnuladas.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 84)
        '
        'BorrarElItemToolStripMenuItem
        '
        Me.BorrarElItemToolStripMenuItem.Name = "BorrarElItemToolStripMenuItem"
        Me.BorrarElItemToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.BorrarElItemToolStripMenuItem.Text = "Borrar el Item"
        '
        'BuscarToolStripMenuItem
        '
        Me.BuscarToolStripMenuItem.Name = "BuscarToolStripMenuItem"
        Me.BuscarToolStripMenuItem.Size = New System.Drawing.Size(360, 24)
        Me.BuscarToolStripMenuItem.Text = "Buscar..."
        Me.BuscarToolStripMenuItem.Visible = False
        '
        'BuscarDescripcionToolStripMenuItem
        '
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 28)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'ContextMenuStripIVA
        '
        Me.ContextMenuStripIVA.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItemIVA})
        Me.ContextMenuStripIVA.Name = "ContextMenuStrip1"
        Me.ContextMenuStripIVA.Size = New System.Drawing.Size(170, 28)
        '
        'BorrarElItemToolStripMenuItemIVA
        '
        Me.BorrarElItemToolStripMenuItemIVA.Name = "BorrarElItemToolStripMenuItemIVA"
        Me.BorrarElItemToolStripMenuItemIVA.Size = New System.Drawing.Size(169, 24)
        Me.BorrarElItemToolStripMenuItemIVA.Text = "Borrar el Item"
        '
        'frmRecepciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1827, 750)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Name = "frmRecepciones"
        Me.Text = "frmRecepciones"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdImpuestos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStripIVA.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNota As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmbOrdenDeCompra As System.Windows.Forms.ComboBox
    Friend WithEvents btnLlenarGrilla As System.Windows.Forms.Button
    Friend WithEvents txtIdProveedor As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnRecibirTodos As System.Windows.Forms.Button
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdGasto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtidpago As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblMontoIva As System.Windows.Forms.Label
    Friend WithEvents chkFacturaCancelada As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnuladas As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStripIVA As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItemIVA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents chkGrillaInferior As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbAlmacenes As System.Windows.Forms.ComboBox
    Friend WithEvents grdImpuestos As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents chkMostarColumnas As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtNroRemitoControl As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroFacturaCompletoControl As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdMoneda As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdComprobante As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtIdGastoAsociar As TextBoxConFormatoVB.FormattedTextBoxVB
End Class
