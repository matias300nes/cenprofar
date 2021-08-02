<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrdenTrabajo
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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtTitulo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdItemsPersonal = New System.Windows.Forms.DataGridView()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.lblFacturado = New System.Windows.Forms.Label()
        Me.lblMoneda = New System.Windows.Forms.Label()
        Me.lblDiferenciaRemito = New System.Windows.Forms.Label()
        Me.lblRemito = New System.Windows.Forms.Label()
        Me.lblPresupuesto = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdPresupuesto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPresupuestos = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbPresupuestos = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.txtTiempoEstimado = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNroOC = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtObservaciones = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItemsPersonal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTiempoEstimado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarElItemToolStripMenuItem, Me.BuscarToolStripMenuItem, Me.BuscarDescripcionToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(361, 75)
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
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.BuscarDescripcionToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.BuscarDescripcionToolStripMenuItem.DropDownWidth = 500
        Me.BuscarDescripcionToolStripMenuItem.Name = "BuscarDescripcionToolStripMenuItem"
        Me.BuscarDescripcionToolStripMenuItem.Size = New System.Drawing.Size(300, 23)
        Me.BuscarDescripcionToolStripMenuItem.Sorted = True
        Me.BuscarDescripcionToolStripMenuItem.Text = "Buscar Descripcion"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.txtTitulo)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.grdItemsPersonal)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.lblFacturado)
        Me.GroupBox1.Controls.Add(Me.lblMoneda)
        Me.GroupBox1.Controls.Add(Me.lblDiferenciaRemito)
        Me.GroupBox1.Controls.Add(Me.lblRemito)
        Me.GroupBox1.Controls.Add(Me.lblPresupuesto)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtIdPresupuesto)
        Me.GroupBox1.Controls.Add(Me.txtPresupuestos)
        Me.GroupBox1.Controls.Add(Me.txtCliente)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbPresupuestos)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.txtTiempoEstimado)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtNroOC)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtObservaciones)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1338, 339)
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
        Me.GroupBox1.TabIndex = 67
        '
        'txtTitulo
        '
        Me.txtTitulo.AccessibleName = ""
        Me.txtTitulo.Decimals = CType(2, Byte)
        Me.txtTitulo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTitulo.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTitulo.Location = New System.Drawing.Point(16, 67)
        Me.txtTitulo.MaxLength = 300
        Me.txtTitulo.Multiline = True
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(446, 20)
        Me.txtTitulo.TabIndex = 8
        Me.txtTitulo.Text_1 = Nothing
        Me.txtTitulo.Text_2 = Nothing
        Me.txtTitulo.Text_3 = Nothing
        Me.txtTitulo.Text_4 = Nothing
        Me.txtTitulo.UserValues = Nothing
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(13, 50)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 13)
        Me.Label11.TabIndex = 260
        Me.Label11.Text = "Título de la OT"
        '
        'grdItemsPersonal
        '
        Me.grdItemsPersonal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItemsPersonal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemsPersonal.Location = New System.Drawing.Point(468, 67)
        Me.grdItemsPersonal.Name = "grdItemsPersonal"
        Me.grdItemsPersonal.Size = New System.Drawing.Size(338, 238)
        Me.grdItemsPersonal.TabIndex = 10
        '
        'chkEliminado
        '
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminado.ForeColor = System.Drawing.Color.Red
        Me.chkEliminado.Location = New System.Drawing.Point(1213, 313)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(122, 17)
        Me.chkEliminado.TabIndex = 258
        Me.chkEliminado.Text = "Ver OT Anuladas"
        Me.chkEliminado.UseVisualStyleBackColor = False
        '
        'lblFacturado
        '
        Me.lblFacturado.BackColor = System.Drawing.Color.White
        Me.lblFacturado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFacturado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacturado.ForeColor = System.Drawing.Color.Red
        Me.lblFacturado.Location = New System.Drawing.Point(1232, 87)
        Me.lblFacturado.Name = "lblFacturado"
        Me.lblFacturado.Size = New System.Drawing.Size(105, 15)
        Me.lblFacturado.TabIndex = 257
        Me.lblFacturado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMoneda
        '
        Me.lblMoneda.BackColor = System.Drawing.Color.White
        Me.lblMoneda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoneda.ForeColor = System.Drawing.Color.Red
        Me.lblMoneda.Location = New System.Drawing.Point(1232, 47)
        Me.lblMoneda.Name = "lblMoneda"
        Me.lblMoneda.Size = New System.Drawing.Size(105, 15)
        Me.lblMoneda.TabIndex = 256
        Me.lblMoneda.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiferenciaRemito
        '
        Me.lblDiferenciaRemito.BackColor = System.Drawing.Color.White
        Me.lblDiferenciaRemito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiferenciaRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferenciaRemito.ForeColor = System.Drawing.Color.Red
        Me.lblDiferenciaRemito.Location = New System.Drawing.Point(1232, 106)
        Me.lblDiferenciaRemito.Name = "lblDiferenciaRemito"
        Me.lblDiferenciaRemito.Size = New System.Drawing.Size(105, 15)
        Me.lblDiferenciaRemito.TabIndex = 255
        Me.lblDiferenciaRemito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRemito
        '
        Me.lblRemito.BackColor = System.Drawing.Color.White
        Me.lblRemito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRemito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemito.ForeColor = System.Drawing.Color.Red
        Me.lblRemito.Location = New System.Drawing.Point(1232, 67)
        Me.lblRemito.Name = "lblRemito"
        Me.lblRemito.Size = New System.Drawing.Size(105, 15)
        Me.lblRemito.TabIndex = 254
        Me.lblRemito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPresupuesto
        '
        Me.lblPresupuesto.BackColor = System.Drawing.Color.White
        Me.lblPresupuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPresupuesto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPresupuesto.ForeColor = System.Drawing.Color.Red
        Me.lblPresupuesto.Location = New System.Drawing.Point(1232, 27)
        Me.lblPresupuesto.Name = "lblPresupuesto"
        Me.lblPresupuesto.Size = New System.Drawing.Size(105, 15)
        Me.lblPresupuesto.TabIndex = 253
        Me.lblPresupuesto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(1133, 28)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 13)
        Me.Label15.TabIndex = 252
        Me.Label15.Text = "Total Presupuesto"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(1136, 108)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(90, 13)
        Me.Label14.TabIndex = 251
        Me.Label14.Text = "Monto por Remitir"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(1144, 89)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 13)
        Me.Label13.TabIndex = 250
        Me.Label13.Text = "Total Facturado"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(1156, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 13)
        Me.Label12.TabIndex = 249
        Me.Label12.Text = "Moneda Pres"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(1151, 69)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 248
        Me.Label10.Text = "Total Remitido"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(1157, 7)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(164, 13)
        Me.Label16.TabIndex = 247
        Me.Label16.Text = "Información sobre el Presupuesto"
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(439, 7)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(23, 20)
        Me.txtIdCliente.TabIndex = 235
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'txtIdPresupuesto
        '
        Me.txtIdPresupuesto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdPresupuesto.Decimals = CType(2, Byte)
        Me.txtIdPresupuesto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdPresupuesto.Enabled = False
        Me.txtIdPresupuesto.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdPresupuesto.Location = New System.Drawing.Point(772, 15)
        Me.txtIdPresupuesto.MaxLength = 8
        Me.txtIdPresupuesto.Name = "txtIdPresupuesto"
        Me.txtIdPresupuesto.Size = New System.Drawing.Size(23, 20)
        Me.txtIdPresupuesto.TabIndex = 234
        Me.txtIdPresupuesto.Text_1 = Nothing
        Me.txtIdPresupuesto.Text_2 = Nothing
        Me.txtIdPresupuesto.Text_3 = Nothing
        Me.txtIdPresupuesto.Text_4 = Nothing
        Me.txtIdPresupuesto.UserValues = Nothing
        Me.txtIdPresupuesto.Visible = False
        '
        'txtPresupuestos
        '
        Me.txtPresupuestos.AccessibleName = ""
        Me.txtPresupuestos.BackColor = System.Drawing.SystemColors.Window
        Me.txtPresupuestos.Decimals = CType(2, Byte)
        Me.txtPresupuestos.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPresupuestos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresupuestos.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtPresupuestos.Location = New System.Drawing.Point(527, 27)
        Me.txtPresupuestos.MaxLength = 25
        Me.txtPresupuestos.Name = "txtPresupuestos"
        Me.txtPresupuestos.ReadOnly = True
        Me.txtPresupuestos.Size = New System.Drawing.Size(372, 20)
        Me.txtPresupuestos.TabIndex = 5
        Me.txtPresupuestos.Text_1 = Nothing
        Me.txtPresupuestos.Text_2 = Nothing
        Me.txtPresupuestos.Text_3 = Nothing
        Me.txtPresupuestos.Text_4 = Nothing
        Me.txtPresupuestos.UserValues = Nothing
        '
        'txtCliente
        '
        Me.txtCliente.AccessibleName = ""
        Me.txtCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtCliente.Decimals = CType(2, Byte)
        Me.txtCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCliente.Location = New System.Drawing.Point(228, 27)
        Me.txtCliente.MaxLength = 25
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(293, 20)
        Me.txtCliente.TabIndex = 3
        Me.txtCliente.Text_1 = Nothing
        Me.txtCliente.Text_2 = Nothing
        Me.txtCliente.Text_3 = Nothing
        Me.txtCliente.Text_4 = Nothing
        Me.txtCliente.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(524, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 13)
        Me.Label8.TabIndex = 233
        Me.Label8.Text = "Presupuesto*"
        '
        'cmbPresupuestos
        '
        Me.cmbPresupuestos.AccessibleName = ""
        Me.cmbPresupuestos.DropDownHeight = 300
        Me.cmbPresupuestos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPresupuestos.FormattingEnabled = True
        Me.cmbPresupuestos.IntegralHeight = False
        Me.cmbPresupuestos.Location = New System.Drawing.Point(527, 27)
        Me.cmbPresupuestos.Name = "cmbPresupuestos"
        Me.cmbPresupuestos.Size = New System.Drawing.Size(372, 21)
        Me.cmbPresupuestos.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(225, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 13)
        Me.Label9.TabIndex = 232
        Me.Label9.Text = "Cliente*"
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = ""
        Me.cmbCliente.DropDownHeight = 500
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(228, 27)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(293, 21)
        Me.cmbCliente.TabIndex = 2
        '
        'txtTiempoEstimado
        '
        Me.txtTiempoEstimado.Location = New System.Drawing.Point(1024, 27)
        Me.txtTiempoEstimado.Name = "txtTiempoEstimado"
        Me.txtTiempoEstimado.Size = New System.Drawing.Size(60, 20)
        Me.txtTiempoEstimado.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(1020, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 13)
        Me.Label7.TabIndex = 226
        Me.Label7.Text = "Tiempo Estimado [Hs]"
        '
        'txtNroOC
        '
        Me.txtNroOC.AccessibleName = ""
        Me.txtNroOC.BackColor = System.Drawing.SystemColors.Window
        Me.txtNroOC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroOC.Decimals = CType(2, Byte)
        Me.txtNroOC.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtNroOC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroOC.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtNroOC.Location = New System.Drawing.Point(904, 27)
        Me.txtNroOC.MaxLength = 25
        Me.txtNroOC.Name = "txtNroOC"
        Me.txtNroOC.Size = New System.Drawing.Size(113, 20)
        Me.txtNroOC.TabIndex = 6
        Me.txtNroOC.Text_1 = Nothing
        Me.txtNroOC.Text_2 = Nothing
        Me.txtNroOC.Text_3 = Nothing
        Me.txtNroOC.Text_4 = Nothing
        Me.txtNroOC.UserValues = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(901, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 224
        Me.Label6.Text = "Nro OC"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(820, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 13)
        Me.Label5.TabIndex = 222
        Me.Label5.Text = "Listado de Materiales"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(468, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 221
        Me.Label4.Text = "Personal Asignado"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(820, 137)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(507, 168)
        Me.grdItems.TabIndex = 11
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AccessibleName = ""
        Me.txtObservaciones.Decimals = CType(2, Byte)
        Me.txtObservaciones.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtObservaciones.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtObservaciones.Location = New System.Drawing.Point(16, 106)
        Me.txtObservaciones.MaxLength = 300
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(446, 199)
        Me.txtObservaciones.TabIndex = 9
        Me.txtObservaciones.Text_1 = Nothing
        Me.txtObservaciones.Text_2 = Nothing
        Me.txtObservaciones.Text_3 = Nothing
        Me.txtObservaciones.Text_4 = Nothing
        Me.txtObservaciones.UserValues = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(13, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 13)
        Me.Label1.TabIndex = 214
        Me.Label1.Text = "Descripción del Trabajo"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(994, 4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(23, 20)
        Me.txtID.TabIndex = 128
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
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
        Me.txtCODIGO.Location = New System.Drawing.Point(16, 27)
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
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(13, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 131
        Me.Label2.Text = "Nro OT"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(120, 27)
        Me.dtpFECHA.MaxDate = New Date(2099, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(102, 20)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(117, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Fecha Inicio"
        '
        'frmOrdenTrabajo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 742)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmOrdenTrabajo"
        Me.Text = "frmAjustes"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItemsPersonal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTiempoEstimado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend Shadows WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BorrarElItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuscarDescripcionToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents GroupBox1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents txtID As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCODIGO As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTiempoEstimado As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNroOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdPresupuesto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPresupuestos As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbPresupuestos As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents lblFacturado As System.Windows.Forms.Label
    Friend WithEvents lblMoneda As System.Windows.Forms.Label
    Friend WithEvents lblDiferenciaRemito As System.Windows.Forms.Label
    Friend WithEvents lblRemito As System.Windows.Forms.Label
    Friend WithEvents lblPresupuesto As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents grdItemsPersonal As System.Windows.Forms.DataGridView
    Friend WithEvents txtTitulo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label

End Class
