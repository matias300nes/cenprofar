<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVentaSalon
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVentaSalon))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lblContadorCuit = New System.Windows.Forms.Label()
        Me.lblContadorCliente = New System.Windows.Forms.Label()
        Me.chkConexion = New System.Windows.Forms.CheckBox()
        Me.lblModo = New System.Windows.Forms.Label()
        Me.PicSincro = New System.Windows.Forms.PictureBox()
        Me.btnSincronizar = New System.Windows.Forms.Button()
        Me.bntVerFactura = New DevComponents.DotNetBar.ButtonX()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblPVI = New System.Windows.Forms.Label()
        Me.lblCodigo = New DevComponents.DotNetBar.LabelX()
        Me.PanelDescuento = New System.Windows.Forms.Panel()
        Me.lblValorDescSinIVa = New System.Windows.Forms.Label()
        Me.chkDescuentoGlobal = New System.Windows.Forms.CheckBox()
        Me.rdPorcentaje = New System.Windows.Forms.RadioButton()
        Me.rdAbsoluto = New System.Windows.Forms.RadioButton()
        Me.txtDescuento = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblValorDescontadoL = New System.Windows.Forms.Label()
        Me.chkDescuentoParticular = New System.Windows.Forms.CheckBox()
        Me.lblValorDescontado = New System.Windows.Forms.Label()
        Me.txtTotalOriginal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtCantidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblCantidadFilas = New System.Windows.Forms.Label()
        Me.txtClienteDireccion = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDTipoComprobante = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDCondicionIVA = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbCondicionIVA = New System.Windows.Forms.ComboBox()
        Me.chkCtaCte = New System.Windows.Forms.CheckBox()
        Me.lblClienteDireccion = New System.Windows.Forms.Label()
        Me.txtCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.btnModCant = New DevComponents.DotNetBar.ButtonX()
        Me.txtSubtotalVista = New System.Windows.Forms.Label()
        Me.txtIVAVista = New System.Windows.Forms.Label()
        Me.txtTotalVista = New System.Windows.Forms.Label()
        Me.txtSubtotal = New System.Windows.Forms.Label()
        Me.txtIVA = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.Label()
        Me.btnBuscarCliente = New DevComponents.DotNetBar.ButtonX()
        Me.GroupBoxPago = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lstTarjetas1 = New System.Windows.Forms.ListBox()
        Me.LstTarjetas2 = New System.Windows.Forms.ListBox()
        Me.lblCandadoTarjetas2 = New System.Windows.Forms.Label()
        Me.lblCandadoTarjetas1 = New System.Windows.Forms.Label()
        Me.lblCandadoContado = New System.Windows.Forms.Label()
        Me.txtFormaPago = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtTarjetas2ImporteFinal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblImporteFinal2 = New System.Windows.Forms.Label()
        Me.txtTarjetas2Importe = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTarjetaImporte2 = New System.Windows.Forms.Label()
        Me.txtVuelto = New System.Windows.Forms.Label()
        Me.lblResto = New System.Windows.Forms.Label()
        Me.lblVuelto = New System.Windows.Forms.Label()
        Me.txtResto = New System.Windows.Forms.Label()
        Me.txtTarjeta2 = New System.Windows.Forms.TextBox()
        Me.lblInteres2 = New System.Windows.Forms.Label()
        Me.lblMontoPorCuotas2 = New System.Windows.Forms.Label()
        Me.txtMontoRecarga2 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblM2 = New System.Windows.Forms.Label()
        Me.txtTarjetas1ImporteFinal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblImporteFinal1 = New System.Windows.Forms.Label()
        Me.txtTarjetas1Importe = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblTarjetasImporte1 = New System.Windows.Forms.Label()
        Me.txtTarjeta1 = New System.Windows.Forms.TextBox()
        Me.lblMontoPorCuotas1 = New System.Windows.Forms.Label()
        Me.lblInteres1 = New System.Windows.Forms.Label()
        Me.txtMontoRecarga1 = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtContado = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblM1 = New System.Windows.Forms.Label()
        Me.chkTarjetas2 = New System.Windows.Forms.CheckBox()
        Me.chkTarjetas1 = New System.Windows.Forms.CheckBox()
        Me.chkContado = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDevolucion = New DevComponents.DotNetBar.LabelX()
        Me.txtCuit = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbTipoComprobante = New System.Windows.Forms.ComboBox()
        Me.lblNumVendedor = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtIDPrecioLista = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCodigoBarra = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblPeso = New System.Windows.Forms.Label()
        Me.txtPeso = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.lblVendedor = New System.Windows.Forms.Label()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkDevolucion = New System.Windows.Forms.CheckBox()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSubtotalItem = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPrecio = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodMaterial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodigoBarra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EquipoHerramienta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Peso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Presio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecioUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecioSinIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc_Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubtotalOrig = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubtotalSinIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProdPesable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.chkDevuelto = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtIdProducto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.TimerVentas = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PicSincro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDescuento.SuspendLayout()
        Me.GroupBoxPago.SuspendLayout()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.lblContadorCuit)
        Me.GroupBox1.Controls.Add(Me.lblContadorCliente)
        Me.GroupBox1.Controls.Add(Me.chkConexion)
        Me.GroupBox1.Controls.Add(Me.lblModo)
        Me.GroupBox1.Controls.Add(Me.PicSincro)
        Me.GroupBox1.Controls.Add(Me.btnSincronizar)
        Me.GroupBox1.Controls.Add(Me.bntVerFactura)
        Me.GroupBox1.Controls.Add(Me.lblFecha)
        Me.GroupBox1.Controls.Add(Me.lblPVI)
        Me.GroupBox1.Controls.Add(Me.lblCodigo)
        Me.GroupBox1.Controls.Add(Me.PanelDescuento)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.lblCantidadFilas)
        Me.GroupBox1.Controls.Add(Me.txtClienteDireccion)
        Me.GroupBox1.Controls.Add(Me.txtIDTipoComprobante)
        Me.GroupBox1.Controls.Add(Me.txtIDCondicionIVA)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.cmbCondicionIVA)
        Me.GroupBox1.Controls.Add(Me.chkCtaCte)
        Me.GroupBox1.Controls.Add(Me.lblClienteDireccion)
        Me.GroupBox1.Controls.Add(Me.txtCliente)
        Me.GroupBox1.Controls.Add(Me.btnModCant)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalVista)
        Me.GroupBox1.Controls.Add(Me.txtIVAVista)
        Me.GroupBox1.Controls.Add(Me.txtTotalVista)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.txtIVA)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.btnBuscarCliente)
        Me.GroupBox1.Controls.Add(Me.GroupBoxPago)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblDevolucion)
        Me.GroupBox1.Controls.Add(Me.txtCuit)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbTipoComprobante)
        Me.GroupBox1.Controls.Add(Me.lblNumVendedor)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtIDPrecioLista)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtCodigoBarra)
        Me.GroupBox1.Controls.Add(Me.lblPeso)
        Me.GroupBox1.Controls.Add(Me.txtPeso)
        Me.GroupBox1.Controls.Add(Me.lblVendedor)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.chkDevolucion)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalItem)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPrecio)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.chkDevuelto)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtIdProducto)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(0, 36)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(2225, 790)
        '
        '
        '
        Me.GroupBox1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.GroupBox1.Style.BackColorGradientAngle = 90
        Me.GroupBox1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
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
        'lblContadorCuit
        '
        Me.lblContadorCuit.BackColor = System.Drawing.Color.Transparent
        Me.lblContadorCuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContadorCuit.ForeColor = System.Drawing.Color.Blue
        Me.lblContadorCuit.Location = New System.Drawing.Point(1045, 55)
        Me.lblContadorCuit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblContadorCuit.Name = "lblContadorCuit"
        Me.lblContadorCuit.Size = New System.Drawing.Size(77, 25)
        Me.lblContadorCuit.TabIndex = 959
        Me.lblContadorCuit.Text = "8"
        Me.lblContadorCuit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblContadorCliente
        '
        Me.lblContadorCliente.BackColor = System.Drawing.Color.Transparent
        Me.lblContadorCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContadorCliente.ForeColor = System.Drawing.Color.Blue
        Me.lblContadorCliente.Location = New System.Drawing.Point(501, 117)
        Me.lblContadorCliente.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblContadorCliente.Name = "lblContadorCliente"
        Me.lblContadorCliente.Size = New System.Drawing.Size(43, 22)
        Me.lblContadorCliente.TabIndex = 958
        Me.lblContadorCliente.Text = "30"
        Me.lblContadorCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkConexion
        '
        Me.chkConexion.AutoSize = True
        Me.chkConexion.Location = New System.Drawing.Point(1131, 14)
        Me.chkConexion.Margin = New System.Windows.Forms.Padding(4)
        Me.chkConexion.Name = "chkConexion"
        Me.chkConexion.Size = New System.Drawing.Size(88, 21)
        Me.chkConexion.TabIndex = 957
        Me.chkConexion.Text = "Conexion"
        Me.chkConexion.UseVisualStyleBackColor = True
        Me.chkConexion.Visible = False
        '
        'lblModo
        '
        Me.lblModo.AutoSize = True
        Me.lblModo.BackColor = System.Drawing.Color.Transparent
        Me.lblModo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModo.ForeColor = System.Drawing.Color.Red
        Me.lblModo.Location = New System.Drawing.Point(660, 610)
        Me.lblModo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblModo.Name = "lblModo"
        Me.lblModo.Size = New System.Drawing.Size(232, 24)
        Me.lblModo.TabIndex = 956
        Me.lblModo.Text = "MODO HOMOLOGACIÓN"
        '
        'PicSincro
        '
        Me.PicSincro.BackColor = System.Drawing.Color.White
        Me.PicSincro.Image = Global.SEYC.My.Resources.Resources.Sincro
        Me.PicSincro.ImageLocation = ""
        Me.PicSincro.Location = New System.Drawing.Point(1150, 156)
        Me.PicSincro.Margin = New System.Windows.Forms.Padding(4)
        Me.PicSincro.Name = "PicSincro"
        Me.PicSincro.Size = New System.Drawing.Size(22, 18)
        Me.PicSincro.TabIndex = 347
        Me.PicSincro.TabStop = False
        '
        'btnSincronizar
        '
        Me.btnSincronizar.BackColor = System.Drawing.Color.White
        Me.btnSincronizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSincronizar.Location = New System.Drawing.Point(1132, 147)
        Me.btnSincronizar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSincronizar.Name = "btnSincronizar"
        Me.btnSincronizar.Size = New System.Drawing.Size(179, 34)
        Me.btnSincronizar.TabIndex = 346
        Me.btnSincronizar.Text = "Act. Balanza"
        Me.btnSincronizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSincronizar.UseVisualStyleBackColor = False
        '
        'bntVerFactura
        '
        Me.bntVerFactura.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.bntVerFactura.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.bntVerFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bntVerFactura.Location = New System.Drawing.Point(1132, 114)
        Me.bntVerFactura.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.bntVerFactura.Name = "bntVerFactura"
        Me.bntVerFactura.Size = New System.Drawing.Size(179, 28)
        Me.bntVerFactura.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.bntVerFactura.Symbol = "58902"
        Me.bntVerFactura.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.bntVerFactura.TabIndex = 344
        Me.bntVerFactura.Text = "Ver"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.BackColor = System.Drawing.Color.Transparent
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.Color.Blue
        Me.lblFecha.Location = New System.Drawing.Point(21, 84)
        Me.lblFecha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(114, 25)
        Me.lblFecha.TabIndex = 343
        Me.lblFecha.Text = "-- / -- / ----"
        '
        'lblPVI
        '
        Me.lblPVI.BackColor = System.Drawing.Color.Transparent
        Me.lblPVI.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPVI.ForeColor = System.Drawing.Color.White
        Me.lblPVI.Image = Global.SEYC.My.Resources.Resources.PtoVtaPorkys
        Me.lblPVI.Location = New System.Drawing.Point(1231, -1)
        Me.lblPVI.Name = "lblPVI"
        Me.lblPVI.Size = New System.Drawing.Size(80, 74)
        Me.lblPVI.TabIndex = 342
        Me.lblPVI.Text = "0"
        Me.lblPVI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCodigo
        '
        '
        '
        '
        Me.lblCodigo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCodigo.Location = New System.Drawing.Point(847, 33)
        Me.lblCodigo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(87, 23)
        Me.lblCodigo.TabIndex = 341
        Me.lblCodigo.Text = "Codigo"
        Me.lblCodigo.Visible = False
        '
        'PanelDescuento
        '
        Me.PanelDescuento.BackColor = System.Drawing.Color.Transparent
        Me.PanelDescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelDescuento.Controls.Add(Me.lblValorDescSinIVa)
        Me.PanelDescuento.Controls.Add(Me.chkDescuentoGlobal)
        Me.PanelDescuento.Controls.Add(Me.rdPorcentaje)
        Me.PanelDescuento.Controls.Add(Me.rdAbsoluto)
        Me.PanelDescuento.Controls.Add(Me.txtDescuento)
        Me.PanelDescuento.Controls.Add(Me.lblValorDescontadoL)
        Me.PanelDescuento.Controls.Add(Me.chkDescuentoParticular)
        Me.PanelDescuento.Controls.Add(Me.lblValorDescontado)
        Me.PanelDescuento.Controls.Add(Me.txtTotalOriginal)
        Me.PanelDescuento.Location = New System.Drawing.Point(21, 654)
        Me.PanelDescuento.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelDescuento.Name = "PanelDescuento"
        Me.PanelDescuento.Size = New System.Drawing.Size(871, 126)
        Me.PanelDescuento.TabIndex = 340
        '
        'lblValorDescSinIVa
        '
        Me.lblValorDescSinIVa.AutoSize = True
        Me.lblValorDescSinIVa.BackColor = System.Drawing.Color.Transparent
        Me.lblValorDescSinIVa.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValorDescSinIVa.Location = New System.Drawing.Point(90, 84)
        Me.lblValorDescSinIVa.Name = "lblValorDescSinIVa"
        Me.lblValorDescSinIVa.Size = New System.Drawing.Size(58, 29)
        Me.lblValorDescSinIVa.TabIndex = 319
        Me.lblValorDescSinIVa.Text = "0.00"
        '
        'chkDescuentoGlobal
        '
        Me.chkDescuentoGlobal.AutoSize = True
        Me.chkDescuentoGlobal.BackColor = System.Drawing.Color.Transparent
        Me.chkDescuentoGlobal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDescuentoGlobal.ForeColor = System.Drawing.Color.Blue
        Me.chkDescuentoGlobal.Location = New System.Drawing.Point(277, 4)
        Me.chkDescuentoGlobal.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDescuentoGlobal.Name = "chkDescuentoGlobal"
        Me.chkDescuentoGlobal.Size = New System.Drawing.Size(229, 29)
        Me.chkDescuentoGlobal.TabIndex = 14
        Me.chkDescuentoGlobal.Text = "Descuento Global [F9]"
        Me.chkDescuentoGlobal.UseVisualStyleBackColor = False
        '
        'rdPorcentaje
        '
        Me.rdPorcentaje.AutoSize = True
        Me.rdPorcentaje.BackColor = System.Drawing.Color.Transparent
        Me.rdPorcentaje.Enabled = False
        Me.rdPorcentaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPorcentaje.Location = New System.Drawing.Point(272, 41)
        Me.rdPorcentaje.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPorcentaje.Name = "rdPorcentaje"
        Me.rdPorcentaje.Size = New System.Drawing.Size(52, 29)
        Me.rdPorcentaje.TabIndex = 15
        Me.rdPorcentaje.TabStop = True
        Me.rdPorcentaje.Text = "%"
        Me.rdPorcentaje.UseVisualStyleBackColor = False
        '
        'rdAbsoluto
        '
        Me.rdAbsoluto.AutoSize = True
        Me.rdAbsoluto.BackColor = System.Drawing.Color.Transparent
        Me.rdAbsoluto.Enabled = False
        Me.rdAbsoluto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAbsoluto.Location = New System.Drawing.Point(336, 41)
        Me.rdAbsoluto.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAbsoluto.Name = "rdAbsoluto"
        Me.rdAbsoluto.Size = New System.Drawing.Size(45, 29)
        Me.rdAbsoluto.TabIndex = 16
        Me.rdAbsoluto.TabStop = True
        Me.rdAbsoluto.Text = "$"
        Me.rdAbsoluto.UseVisualStyleBackColor = False
        '
        'txtDescuento
        '
        Me.txtDescuento.AccessibleName = ""
        Me.txtDescuento.Decimals = CType(2, Byte)
        Me.txtDescuento.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescuento.Enabled = False
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtDescuento.Location = New System.Drawing.Point(403, 41)
        Me.txtDescuento.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescuento.MaxLength = 100
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(116, 30)
        Me.txtDescuento.TabIndex = 17
        Me.txtDescuento.Text_1 = Nothing
        Me.txtDescuento.Text_2 = Nothing
        Me.txtDescuento.Text_3 = Nothing
        Me.txtDescuento.Text_4 = Nothing
        Me.txtDescuento.UserValues = Nothing
        '
        'lblValorDescontadoL
        '
        Me.lblValorDescontadoL.AutoSize = True
        Me.lblValorDescontadoL.BackColor = System.Drawing.Color.Transparent
        Me.lblValorDescontadoL.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValorDescontadoL.Location = New System.Drawing.Point(16, 9)
        Me.lblValorDescontadoL.Name = "lblValorDescontadoL"
        Me.lblValorDescontadoL.Size = New System.Drawing.Size(198, 25)
        Me.lblValorDescontadoL.TabIndex = 295
        Me.lblValorDescontadoL.Text = "Valor Descontado ($)"
        '
        'chkDescuentoParticular
        '
        Me.chkDescuentoParticular.AutoSize = True
        Me.chkDescuentoParticular.BackColor = System.Drawing.Color.Transparent
        Me.chkDescuentoParticular.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDescuentoParticular.ForeColor = System.Drawing.Color.Blue
        Me.chkDescuentoParticular.Location = New System.Drawing.Point(548, 4)
        Me.chkDescuentoParticular.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDescuentoParticular.Name = "chkDescuentoParticular"
        Me.chkDescuentoParticular.Size = New System.Drawing.Size(254, 29)
        Me.chkDescuentoParticular.TabIndex = 318
        Me.chkDescuentoParticular.Text = "Descuento Particular [F9]"
        Me.chkDescuentoParticular.UseVisualStyleBackColor = False
        '
        'lblValorDescontado
        '
        Me.lblValorDescontado.AutoSize = True
        Me.lblValorDescontado.BackColor = System.Drawing.Color.Transparent
        Me.lblValorDescontado.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValorDescontado.Location = New System.Drawing.Point(89, 55)
        Me.lblValorDescontado.Name = "lblValorDescontado"
        Me.lblValorDescontado.Size = New System.Drawing.Size(58, 29)
        Me.lblValorDescontado.TabIndex = 296
        Me.lblValorDescontado.Text = "0.00"
        '
        'txtTotalOriginal
        '
        Me.txtTotalOriginal.AccessibleName = ""
        Me.txtTotalOriginal.BackColor = System.Drawing.Color.White
        Me.txtTotalOriginal.Decimals = CType(2, Byte)
        Me.txtTotalOriginal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalOriginal.Enabled = False
        Me.txtTotalOriginal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalOriginal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTotalOriginal.Location = New System.Drawing.Point(691, 41)
        Me.txtTotalOriginal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalOriginal.MaxLength = 100
        Me.txtTotalOriginal.Name = "txtTotalOriginal"
        Me.txtTotalOriginal.ReadOnly = True
        Me.txtTotalOriginal.Size = New System.Drawing.Size(116, 30)
        Me.txtTotalOriginal.TabIndex = 303
        Me.txtTotalOriginal.Text_1 = Nothing
        Me.txtTotalOriginal.Text_2 = Nothing
        Me.txtTotalOriginal.Text_3 = Nothing
        Me.txtTotalOriginal.Text_4 = Nothing
        Me.txtTotalOriginal.UserValues = Nothing
        Me.txtTotalOriginal.Visible = False
        '
        'txtCantidad
        '
        Me.txtCantidad.AccessibleName = ""
        Me.txtCantidad.BackColor = System.Drawing.Color.White
        Me.txtCantidad.Decimals = CType(2, Byte)
        Me.txtCantidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCantidad.Location = New System.Drawing.Point(1132, 220)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.MaxLength = 7
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.ReadOnly = True
        Me.txtCantidad.Size = New System.Drawing.Size(124, 30)
        Me.txtCantidad.TabIndex = 338
        Me.txtCantidad.Text_1 = Nothing
        Me.txtCantidad.Text_2 = Nothing
        Me.txtCantidad.Text_3 = Nothing
        Me.txtCantidad.Text_4 = Nothing
        Me.txtCantidad.UserValues = Nothing
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(21, 610)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(77, 29)
        Me.Label18.TabIndex = 337
        Me.Label18.Text = "Ítems:"
        '
        'lblCantidadFilas
        '
        Me.lblCantidadFilas.AutoSize = True
        Me.lblCantidadFilas.BackColor = System.Drawing.Color.Transparent
        Me.lblCantidadFilas.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidadFilas.Location = New System.Drawing.Point(108, 610)
        Me.lblCantidadFilas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantidadFilas.Name = "lblCantidadFilas"
        Me.lblCantidadFilas.Size = New System.Drawing.Size(39, 29)
        Me.lblCantidadFilas.TabIndex = 336
        Me.lblCantidadFilas.Text = "00"
        Me.lblCantidadFilas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtClienteDireccion
        '
        Me.txtClienteDireccion.AccessibleName = ""
        Me.txtClienteDireccion.Decimals = CType(2, Byte)
        Me.txtClienteDireccion.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtClienteDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClienteDireccion.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtClienteDireccion.Location = New System.Drawing.Point(556, 140)
        Me.txtClienteDireccion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClienteDireccion.MaxLength = 300
        Me.txtClienteDireccion.Name = "txtClienteDireccion"
        Me.txtClienteDireccion.Size = New System.Drawing.Size(567, 30)
        Me.txtClienteDireccion.TabIndex = 6
        Me.txtClienteDireccion.Text_1 = Nothing
        Me.txtClienteDireccion.Text_2 = Nothing
        Me.txtClienteDireccion.Text_3 = Nothing
        Me.txtClienteDireccion.Text_4 = Nothing
        Me.txtClienteDireccion.UserValues = Nothing
        '
        'txtIDTipoComprobante
        '
        Me.txtIDTipoComprobante.AccessibleName = ""
        Me.txtIDTipoComprobante.Decimals = CType(2, Byte)
        Me.txtIDTipoComprobante.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDTipoComprobante.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDTipoComprobante.Location = New System.Drawing.Point(464, 50)
        Me.txtIDTipoComprobante.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDTipoComprobante.MaxLength = 100
        Me.txtIDTipoComprobante.Name = "txtIDTipoComprobante"
        Me.txtIDTipoComprobante.Size = New System.Drawing.Size(29, 22)
        Me.txtIDTipoComprobante.TabIndex = 316
        Me.txtIDTipoComprobante.Text_1 = Nothing
        Me.txtIDTipoComprobante.Text_2 = Nothing
        Me.txtIDTipoComprobante.Text_3 = Nothing
        Me.txtIDTipoComprobante.Text_4 = Nothing
        Me.txtIDTipoComprobante.UserValues = Nothing
        Me.txtIDTipoComprobante.Visible = False
        '
        'txtIDCondicionIVA
        '
        Me.txtIDCondicionIVA.AccessibleName = ""
        Me.txtIDCondicionIVA.Decimals = CType(2, Byte)
        Me.txtIDCondicionIVA.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDCondicionIVA.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDCondicionIVA.Location = New System.Drawing.Point(427, 50)
        Me.txtIDCondicionIVA.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDCondicionIVA.MaxLength = 100
        Me.txtIDCondicionIVA.Name = "txtIDCondicionIVA"
        Me.txtIDCondicionIVA.Size = New System.Drawing.Size(29, 22)
        Me.txtIDCondicionIVA.TabIndex = 315
        Me.txtIDCondicionIVA.Text_1 = Nothing
        Me.txtIDCondicionIVA.Text_2 = Nothing
        Me.txtIDCondicionIVA.Text_3 = Nothing
        Me.txtIDCondicionIVA.Text_4 = Nothing
        Me.txtIDCondicionIVA.UserValues = Nothing
        Me.txtIDCondicionIVA.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Blue
        Me.Label17.Location = New System.Drawing.Point(184, 55)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(157, 25)
        Me.Label17.TabIndex = 314
        Me.Label17.Text = "Condición IVA "
        '
        'cmbCondicionIVA
        '
        Me.cmbCondicionIVA.AccessibleName = "*CondicionIVA"
        Me.cmbCondicionIVA.DropDownHeight = 450
        Me.cmbCondicionIVA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondicionIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCondicionIVA.FormattingEnabled = True
        Me.cmbCondicionIVA.IntegralHeight = False
        Me.cmbCondicionIVA.Location = New System.Drawing.Point(189, 82)
        Me.cmbCondicionIVA.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbCondicionIVA.Name = "cmbCondicionIVA"
        Me.cmbCondicionIVA.Size = New System.Drawing.Size(345, 28)
        Me.cmbCondicionIVA.TabIndex = 1
        '
        'chkCtaCte
        '
        Me.chkCtaCte.AccessibleName = "Eliminado"
        Me.chkCtaCte.AutoCheck = False
        Me.chkCtaCte.AutoSize = True
        Me.chkCtaCte.BackColor = System.Drawing.Color.Transparent
        Me.chkCtaCte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCtaCte.ForeColor = System.Drawing.Color.DarkGreen
        Me.chkCtaCte.Location = New System.Drawing.Point(1132, 51)
        Me.chkCtaCte.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCtaCte.Name = "chkCtaCte"
        Me.chkCtaCte.Size = New System.Drawing.Size(78, 21)
        Me.chkCtaCte.TabIndex = 312
        Me.chkCtaCte.Text = "CtaCte"
        Me.chkCtaCte.UseVisualStyleBackColor = False
        '
        'lblClienteDireccion
        '
        Me.lblClienteDireccion.AutoSize = True
        Me.lblClienteDireccion.BackColor = System.Drawing.Color.Transparent
        Me.lblClienteDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteDireccion.ForeColor = System.Drawing.Color.Blue
        Me.lblClienteDireccion.Location = New System.Drawing.Point(552, 118)
        Me.lblClienteDireccion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClienteDireccion.Name = "lblClienteDireccion"
        Me.lblClienteDireccion.Size = New System.Drawing.Size(102, 25)
        Me.lblClienteDireccion.TabIndex = 311
        Me.lblClienteDireccion.Text = "Dirección"
        '
        'txtCliente
        '
        Me.txtCliente.AccessibleName = ""
        Me.txtCliente.Decimals = CType(2, Byte)
        Me.txtCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCliente.Location = New System.Drawing.Point(24, 140)
        Me.txtCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCliente.MaxLength = 30
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(520, 30)
        Me.txtCliente.TabIndex = 5
        Me.txtCliente.Text_1 = Nothing
        Me.txtCliente.Text_2 = Nothing
        Me.txtCliente.Text_3 = Nothing
        Me.txtCliente.Text_4 = Nothing
        Me.txtCliente.UserValues = Nothing
        '
        'btnModCant
        '
        Me.btnModCant.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModCant.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnModCant.Location = New System.Drawing.Point(1275, 215)
        Me.btnModCant.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnModCant.Name = "btnModCant"
        Me.btnModCant.Size = New System.Drawing.Size(36, 37)
        Me.btnModCant.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnModCant.Symbol = ""
        Me.btnModCant.TabIndex = 12
        Me.btnModCant.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left
        '
        'txtSubtotalVista
        '
        Me.txtSubtotalVista.BackColor = System.Drawing.Color.Lime
        Me.txtSubtotalVista.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotalVista.Location = New System.Drawing.Point(901, 681)
        Me.txtSubtotalVista.Name = "txtSubtotalVista"
        Me.txtSubtotalVista.Size = New System.Drawing.Size(280, 85)
        Me.txtSubtotalVista.TabIndex = 309
        Me.txtSubtotalVista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtSubtotalVista.Visible = False
        '
        'txtIVAVista
        '
        Me.txtIVAVista.BackColor = System.Drawing.Color.Lime
        Me.txtIVAVista.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVAVista.Location = New System.Drawing.Point(1201, 681)
        Me.txtIVAVista.Name = "txtIVAVista"
        Me.txtIVAVista.Size = New System.Drawing.Size(280, 85)
        Me.txtIVAVista.TabIndex = 308
        Me.txtIVAVista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtIVAVista.Visible = False
        '
        'txtTotalVista
        '
        Me.txtTotalVista.BackColor = System.Drawing.Color.Lime
        Me.txtTotalVista.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalVista.Location = New System.Drawing.Point(1501, 681)
        Me.txtTotalVista.Name = "txtTotalVista"
        Me.txtTotalVista.Size = New System.Drawing.Size(280, 85)
        Me.txtTotalVista.TabIndex = 307
        Me.txtTotalVista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotalVista.Visible = False
        '
        'txtSubtotal
        '
        Me.txtSubtotal.BackColor = System.Drawing.Color.White
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Location = New System.Drawing.Point(901, 681)
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(280, 85)
        Me.txtSubtotal.TabIndex = 306
        Me.txtSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIVA
        '
        Me.txtIVA.BackColor = System.Drawing.Color.White
        Me.txtIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Location = New System.Drawing.Point(1201, 681)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(280, 85)
        Me.txtIVA.TabIndex = 305
        Me.txtIVA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.Color.White
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(1501, 681)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(280, 85)
        Me.txtTotal.TabIndex = 304
        Me.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBuscarCliente.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCliente.Location = New System.Drawing.Point(1132, 80)
        Me.btnBuscarCliente.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(179, 28)
        Me.btnBuscarCliente.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnBuscarCliente.Symbol = "59603"
        Me.btnBuscarCliente.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnBuscarCliente.TabIndex = 4
        Me.btnBuscarCliente.Text = "Buscar"
        '
        'GroupBoxPago
        '
        Me.GroupBoxPago.BackColor = System.Drawing.Color.Transparent
        Me.GroupBoxPago.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBoxPago.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2013
        Me.GroupBoxPago.Controls.Add(Me.lstTarjetas1)
        Me.GroupBoxPago.Controls.Add(Me.LstTarjetas2)
        Me.GroupBoxPago.Controls.Add(Me.lblCandadoTarjetas2)
        Me.GroupBoxPago.Controls.Add(Me.lblCandadoTarjetas1)
        Me.GroupBoxPago.Controls.Add(Me.lblCandadoContado)
        Me.GroupBoxPago.Controls.Add(Me.txtFormaPago)
        Me.GroupBoxPago.Controls.Add(Me.txtTarjetas2ImporteFinal)
        Me.GroupBoxPago.Controls.Add(Me.lblImporteFinal2)
        Me.GroupBoxPago.Controls.Add(Me.txtTarjetas2Importe)
        Me.GroupBoxPago.Controls.Add(Me.lblTarjetaImporte2)
        Me.GroupBoxPago.Controls.Add(Me.txtVuelto)
        Me.GroupBoxPago.Controls.Add(Me.lblResto)
        Me.GroupBoxPago.Controls.Add(Me.lblVuelto)
        Me.GroupBoxPago.Controls.Add(Me.txtResto)
        Me.GroupBoxPago.Controls.Add(Me.txtTarjeta2)
        Me.GroupBoxPago.Controls.Add(Me.lblInteres2)
        Me.GroupBoxPago.Controls.Add(Me.lblMontoPorCuotas2)
        Me.GroupBoxPago.Controls.Add(Me.txtMontoRecarga2)
        Me.GroupBoxPago.Controls.Add(Me.lblM2)
        Me.GroupBoxPago.Controls.Add(Me.txtTarjetas1ImporteFinal)
        Me.GroupBoxPago.Controls.Add(Me.lblImporteFinal1)
        Me.GroupBoxPago.Controls.Add(Me.txtTarjetas1Importe)
        Me.GroupBoxPago.Controls.Add(Me.lblTarjetasImporte1)
        Me.GroupBoxPago.Controls.Add(Me.txtTarjeta1)
        Me.GroupBoxPago.Controls.Add(Me.lblMontoPorCuotas1)
        Me.GroupBoxPago.Controls.Add(Me.lblInteres1)
        Me.GroupBoxPago.Controls.Add(Me.txtMontoRecarga1)
        Me.GroupBoxPago.Controls.Add(Me.txtContado)
        Me.GroupBoxPago.Controls.Add(Me.lblM1)
        Me.GroupBoxPago.Controls.Add(Me.chkTarjetas2)
        Me.GroupBoxPago.Controls.Add(Me.chkTarjetas1)
        Me.GroupBoxPago.Controls.Add(Me.chkContado)
        Me.GroupBoxPago.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBoxPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxPago.Location = New System.Drawing.Point(1339, 18)
        Me.GroupBoxPago.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBoxPago.Name = "GroupBoxPago"
        Me.GroupBoxPago.Size = New System.Drawing.Size(443, 582)
        '
        '
        '
        Me.GroupBoxPago.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupBoxPago.Style.BackColorGradientAngle = 90
        Me.GroupBoxPago.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupBoxPago.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBoxPago.Style.BorderBottomWidth = 1
        Me.GroupBoxPago.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupBoxPago.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBoxPago.Style.BorderLeftWidth = 1
        Me.GroupBoxPago.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBoxPago.Style.BorderRightWidth = 1
        Me.GroupBoxPago.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupBoxPago.Style.BorderTopWidth = 1
        Me.GroupBoxPago.Style.CornerDiameter = 4
        Me.GroupBoxPago.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupBoxPago.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupBoxPago.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupBoxPago.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupBoxPago.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupBoxPago.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupBoxPago.TabIndex = 18
        Me.GroupBoxPago.Text = "Formas de Pago"
        '
        'lstTarjetas1
        '
        Me.lstTarjetas1.BackColor = System.Drawing.Color.Khaki
        Me.lstTarjetas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTarjetas1.FormattingEnabled = True
        Me.lstTarjetas1.ItemHeight = 25
        Me.lstTarjetas1.Location = New System.Drawing.Point(1, 167)
        Me.lstTarjetas1.Margin = New System.Windows.Forms.Padding(4)
        Me.lstTarjetas1.Name = "lstTarjetas1"
        Me.lstTarjetas1.Size = New System.Drawing.Size(428, 29)
        Me.lstTarjetas1.TabIndex = 59
        Me.lstTarjetas1.Visible = False
        '
        'LstTarjetas2
        '
        Me.LstTarjetas2.BackColor = System.Drawing.Color.Khaki
        Me.LstTarjetas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstTarjetas2.FormattingEnabled = True
        Me.LstTarjetas2.ItemHeight = 25
        Me.LstTarjetas2.Location = New System.Drawing.Point(3, 354)
        Me.LstTarjetas2.Margin = New System.Windows.Forms.Padding(4)
        Me.LstTarjetas2.Name = "LstTarjetas2"
        Me.LstTarjetas2.Size = New System.Drawing.Size(427, 29)
        Me.LstTarjetas2.TabIndex = 69
        Me.LstTarjetas2.Visible = False
        '
        'lblCandadoTarjetas2
        '
        Me.lblCandadoTarjetas2.Image = Global.SEYC.My.Resources.Resources.CandadoCerrado
        Me.lblCandadoTarjetas2.Location = New System.Drawing.Point(-3, 288)
        Me.lblCandadoTarjetas2.Name = "lblCandadoTarjetas2"
        Me.lblCandadoTarjetas2.Size = New System.Drawing.Size(29, 23)
        Me.lblCandadoTarjetas2.TabIndex = 81
        '
        'lblCandadoTarjetas1
        '
        Me.lblCandadoTarjetas1.Image = Global.SEYC.My.Resources.Resources.CandadoCerrado
        Me.lblCandadoTarjetas1.Location = New System.Drawing.Point(-3, 105)
        Me.lblCandadoTarjetas1.Name = "lblCandadoTarjetas1"
        Me.lblCandadoTarjetas1.Size = New System.Drawing.Size(29, 23)
        Me.lblCandadoTarjetas1.TabIndex = 80
        '
        'lblCandadoContado
        '
        Me.lblCandadoContado.Image = Global.SEYC.My.Resources.Resources.CandadoCerrado
        Me.lblCandadoContado.Location = New System.Drawing.Point(-3, 37)
        Me.lblCandadoContado.Name = "lblCandadoContado"
        Me.lblCandadoContado.Size = New System.Drawing.Size(29, 23)
        Me.lblCandadoContado.TabIndex = 79
        '
        'txtFormaPago
        '
        Me.txtFormaPago.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFormaPago.Decimals = CType(2, Byte)
        Me.txtFormaPago.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtFormaPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormaPago.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphabetic
        Me.txtFormaPago.Location = New System.Drawing.Point(191, 4)
        Me.txtFormaPago.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFormaPago.Name = "txtFormaPago"
        Me.txtFormaPago.Size = New System.Drawing.Size(57, 22)
        Me.txtFormaPago.TabIndex = 78
        Me.txtFormaPago.Text_1 = Nothing
        Me.txtFormaPago.Text_2 = Nothing
        Me.txtFormaPago.Text_3 = Nothing
        Me.txtFormaPago.Text_4 = Nothing
        Me.txtFormaPago.UserValues = Nothing
        Me.txtFormaPago.Visible = False
        '
        'txtTarjetas2ImporteFinal
        '
        Me.txtTarjetas2ImporteFinal.BackColor = System.Drawing.Color.White
        Me.txtTarjetas2ImporteFinal.Decimals = CType(2, Byte)
        Me.txtTarjetas2ImporteFinal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTarjetas2ImporteFinal.Enabled = False
        Me.txtTarjetas2ImporteFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjetas2ImporteFinal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTarjetas2ImporteFinal.Location = New System.Drawing.Point(172, 415)
        Me.txtTarjetas2ImporteFinal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTarjetas2ImporteFinal.Name = "txtTarjetas2ImporteFinal"
        Me.txtTarjetas2ImporteFinal.ReadOnly = True
        Me.txtTarjetas2ImporteFinal.Size = New System.Drawing.Size(256, 30)
        Me.txtTarjetas2ImporteFinal.TabIndex = 77
        Me.txtTarjetas2ImporteFinal.Text_1 = Nothing
        Me.txtTarjetas2ImporteFinal.Text_2 = Nothing
        Me.txtTarjetas2ImporteFinal.Text_3 = Nothing
        Me.txtTarjetas2ImporteFinal.Text_4 = Nothing
        Me.txtTarjetas2ImporteFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarjetas2ImporteFinal.UserValues = Nothing
        '
        'lblImporteFinal2
        '
        Me.lblImporteFinal2.AutoSize = True
        Me.lblImporteFinal2.BackColor = System.Drawing.Color.Transparent
        Me.lblImporteFinal2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteFinal2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblImporteFinal2.Location = New System.Drawing.Point(16, 425)
        Me.lblImporteFinal2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblImporteFinal2.Name = "lblImporteFinal2"
        Me.lblImporteFinal2.Size = New System.Drawing.Size(138, 24)
        Me.lblImporteFinal2.TabIndex = 76
        Me.lblImporteFinal2.Text = "Importe Final:"
        '
        'txtTarjetas2Importe
        '
        Me.txtTarjetas2Importe.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTarjetas2Importe.Decimals = CType(2, Byte)
        Me.txtTarjetas2Importe.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTarjetas2Importe.Enabled = False
        Me.txtTarjetas2Importe.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjetas2Importe.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTarjetas2Importe.Location = New System.Drawing.Point(173, 375)
        Me.txtTarjetas2Importe.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTarjetas2Importe.Name = "txtTarjetas2Importe"
        Me.txtTarjetas2Importe.Size = New System.Drawing.Size(256, 30)
        Me.txtTarjetas2Importe.TabIndex = 6
        Me.txtTarjetas2Importe.Text_1 = Nothing
        Me.txtTarjetas2Importe.Text_2 = Nothing
        Me.txtTarjetas2Importe.Text_3 = Nothing
        Me.txtTarjetas2Importe.Text_4 = Nothing
        Me.txtTarjetas2Importe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarjetas2Importe.UserValues = Nothing
        '
        'lblTarjetaImporte2
        '
        Me.lblTarjetaImporte2.AutoSize = True
        Me.lblTarjetaImporte2.BackColor = System.Drawing.Color.Transparent
        Me.lblTarjetaImporte2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTarjetaImporte2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTarjetaImporte2.Location = New System.Drawing.Point(67, 380)
        Me.lblTarjetaImporte2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTarjetaImporte2.Name = "lblTarjetaImporte2"
        Me.lblTarjetaImporte2.Size = New System.Drawing.Size(86, 24)
        Me.lblTarjetaImporte2.TabIndex = 75
        Me.lblTarjetaImporte2.Text = "Importe:"
        '
        'txtVuelto
        '
        Me.txtVuelto.BackColor = System.Drawing.Color.White
        Me.txtVuelto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVuelto.Location = New System.Drawing.Point(307, 508)
        Me.txtVuelto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtVuelto.Name = "txtVuelto"
        Me.txtVuelto.Size = New System.Drawing.Size(123, 32)
        Me.txtVuelto.TabIndex = 73
        Me.txtVuelto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblResto
        '
        Me.lblResto.AutoSize = True
        Me.lblResto.BackColor = System.Drawing.Color.Transparent
        Me.lblResto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblResto.Location = New System.Drawing.Point(3, 516)
        Me.lblResto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblResto.Name = "lblResto"
        Me.lblResto.Size = New System.Drawing.Size(58, 20)
        Me.lblResto.TabIndex = 70
        Me.lblResto.Text = "Resto"
        '
        'lblVuelto
        '
        Me.lblVuelto.AutoSize = True
        Me.lblVuelto.BackColor = System.Drawing.Color.Transparent
        Me.lblVuelto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVuelto.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblVuelto.Location = New System.Drawing.Point(229, 516)
        Me.lblVuelto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVuelto.Name = "lblVuelto"
        Me.lblVuelto.Size = New System.Drawing.Size(62, 20)
        Me.lblVuelto.TabIndex = 71
        Me.lblVuelto.Text = "Vuelto"
        '
        'txtResto
        '
        Me.txtResto.BackColor = System.Drawing.Color.White
        Me.txtResto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResto.Location = New System.Drawing.Point(76, 508)
        Me.txtResto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtResto.Name = "txtResto"
        Me.txtResto.Size = New System.Drawing.Size(123, 32)
        Me.txtResto.TabIndex = 72
        Me.txtResto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTarjeta2
        '
        Me.txtTarjeta2.BackColor = System.Drawing.Color.Khaki
        Me.txtTarjeta2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjeta2.Location = New System.Drawing.Point(3, 327)
        Me.txtTarjeta2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTarjeta2.Name = "txtTarjeta2"
        Me.txtTarjeta2.Size = New System.Drawing.Size(427, 30)
        Me.txtTarjeta2.TabIndex = 5
        '
        'lblInteres2
        '
        Me.lblInteres2.BackColor = System.Drawing.Color.Yellow
        Me.lblInteres2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInteres2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInteres2.Location = New System.Drawing.Point(172, 282)
        Me.lblInteres2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblInteres2.Name = "lblInteres2"
        Me.lblInteres2.Size = New System.Drawing.Size(63, 29)
        Me.lblInteres2.TabIndex = 65
        Me.lblInteres2.Text = "0.00"
        Me.lblInteres2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblInteres2.Visible = False
        '
        'lblMontoPorCuotas2
        '
        Me.lblMontoPorCuotas2.AutoSize = True
        Me.lblMontoPorCuotas2.BackColor = System.Drawing.Color.Transparent
        Me.lblMontoPorCuotas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoPorCuotas2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMontoPorCuotas2.Location = New System.Drawing.Point(304, 295)
        Me.lblMontoPorCuotas2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMontoPorCuotas2.Name = "lblMontoPorCuotas2"
        Me.lblMontoPorCuotas2.Size = New System.Drawing.Size(113, 17)
        Me.lblMontoPorCuotas2.TabIndex = 66
        Me.lblMontoPorCuotas2.Text = "Monto Cuota 2"
        Me.lblMontoPorCuotas2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMontoPorCuotas2.Visible = False
        '
        'txtMontoRecarga2
        '
        Me.txtMontoRecarga2.BackColor = System.Drawing.Color.DarkKhaki
        Me.txtMontoRecarga2.Decimals = CType(2, Byte)
        Me.txtMontoRecarga2.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoRecarga2.Enabled = False
        Me.txtMontoRecarga2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoRecarga2.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoRecarga2.Location = New System.Drawing.Point(324, 4)
        Me.txtMontoRecarga2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMontoRecarga2.Name = "txtMontoRecarga2"
        Me.txtMontoRecarga2.ReadOnly = True
        Me.txtMontoRecarga2.Size = New System.Drawing.Size(104, 22)
        Me.txtMontoRecarga2.TabIndex = 64
        Me.txtMontoRecarga2.Text_1 = Nothing
        Me.txtMontoRecarga2.Text_2 = Nothing
        Me.txtMontoRecarga2.Text_3 = Nothing
        Me.txtMontoRecarga2.Text_4 = Nothing
        Me.txtMontoRecarga2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoRecarga2.UserValues = Nothing
        Me.txtMontoRecarga2.Visible = False
        '
        'lblM2
        '
        Me.lblM2.AutoSize = True
        Me.lblM2.BackColor = System.Drawing.Color.Transparent
        Me.lblM2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblM2.Location = New System.Drawing.Point(307, 278)
        Me.lblM2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblM2.Name = "lblM2"
        Me.lblM2.Size = New System.Drawing.Size(105, 18)
        Me.lblM2.TabIndex = 67
        Me.lblM2.Text = "Mto. x Ctas.:"
        Me.lblM2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblM2.Visible = False
        '
        'txtTarjetas1ImporteFinal
        '
        Me.txtTarjetas1ImporteFinal.BackColor = System.Drawing.Color.White
        Me.txtTarjetas1ImporteFinal.Decimals = CType(2, Byte)
        Me.txtTarjetas1ImporteFinal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTarjetas1ImporteFinal.Enabled = False
        Me.txtTarjetas1ImporteFinal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjetas1ImporteFinal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTarjetas1ImporteFinal.Location = New System.Drawing.Point(173, 222)
        Me.txtTarjetas1ImporteFinal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTarjetas1ImporteFinal.Name = "txtTarjetas1ImporteFinal"
        Me.txtTarjetas1ImporteFinal.ReadOnly = True
        Me.txtTarjetas1ImporteFinal.Size = New System.Drawing.Size(256, 30)
        Me.txtTarjetas1ImporteFinal.TabIndex = 63
        Me.txtTarjetas1ImporteFinal.Text_1 = Nothing
        Me.txtTarjetas1ImporteFinal.Text_2 = Nothing
        Me.txtTarjetas1ImporteFinal.Text_3 = Nothing
        Me.txtTarjetas1ImporteFinal.Text_4 = Nothing
        Me.txtTarjetas1ImporteFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarjetas1ImporteFinal.UserValues = Nothing
        '
        'lblImporteFinal1
        '
        Me.lblImporteFinal1.AutoSize = True
        Me.lblImporteFinal1.BackColor = System.Drawing.Color.Transparent
        Me.lblImporteFinal1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteFinal1.Location = New System.Drawing.Point(12, 231)
        Me.lblImporteFinal1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblImporteFinal1.Name = "lblImporteFinal1"
        Me.lblImporteFinal1.Size = New System.Drawing.Size(138, 24)
        Me.lblImporteFinal1.TabIndex = 62
        Me.lblImporteFinal1.Text = "Importe Final:"
        '
        'txtTarjetas1Importe
        '
        Me.txtTarjetas1Importe.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTarjetas1Importe.Decimals = CType(2, Byte)
        Me.txtTarjetas1Importe.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTarjetas1Importe.Enabled = False
        Me.txtTarjetas1Importe.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjetas1Importe.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTarjetas1Importe.Location = New System.Drawing.Point(173, 182)
        Me.txtTarjetas1Importe.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTarjetas1Importe.Name = "txtTarjetas1Importe"
        Me.txtTarjetas1Importe.Size = New System.Drawing.Size(256, 30)
        Me.txtTarjetas1Importe.TabIndex = 3
        Me.txtTarjetas1Importe.Text_1 = Nothing
        Me.txtTarjetas1Importe.Text_2 = Nothing
        Me.txtTarjetas1Importe.Text_3 = Nothing
        Me.txtTarjetas1Importe.Text_4 = Nothing
        Me.txtTarjetas1Importe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarjetas1Importe.UserValues = Nothing
        '
        'lblTarjetasImporte1
        '
        Me.lblTarjetasImporte1.AutoSize = True
        Me.lblTarjetasImporte1.BackColor = System.Drawing.Color.Transparent
        Me.lblTarjetasImporte1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTarjetasImporte1.Location = New System.Drawing.Point(65, 187)
        Me.lblTarjetasImporte1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTarjetasImporte1.Name = "lblTarjetasImporte1"
        Me.lblTarjetasImporte1.Size = New System.Drawing.Size(86, 24)
        Me.lblTarjetasImporte1.TabIndex = 61
        Me.lblTarjetasImporte1.Text = "Importe:"
        '
        'txtTarjeta1
        '
        Me.txtTarjeta1.BackColor = System.Drawing.Color.Khaki
        Me.txtTarjeta1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjeta1.Location = New System.Drawing.Point(1, 142)
        Me.txtTarjeta1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTarjeta1.Name = "txtTarjeta1"
        Me.txtTarjeta1.Size = New System.Drawing.Size(428, 30)
        Me.txtTarjeta1.TabIndex = 2
        '
        'lblMontoPorCuotas1
        '
        Me.lblMontoPorCuotas1.AutoSize = True
        Me.lblMontoPorCuotas1.BackColor = System.Drawing.Color.Transparent
        Me.lblMontoPorCuotas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoPorCuotas1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMontoPorCuotas1.Location = New System.Drawing.Point(304, 111)
        Me.lblMontoPorCuotas1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMontoPorCuotas1.Name = "lblMontoPorCuotas1"
        Me.lblMontoPorCuotas1.Size = New System.Drawing.Size(113, 17)
        Me.lblMontoPorCuotas1.TabIndex = 56
        Me.lblMontoPorCuotas1.Text = "Monto Cuota 1"
        Me.lblMontoPorCuotas1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMontoPorCuotas1.Visible = False
        '
        'lblInteres1
        '
        Me.lblInteres1.BackColor = System.Drawing.Color.Yellow
        Me.lblInteres1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInteres1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInteres1.Location = New System.Drawing.Point(173, 103)
        Me.lblInteres1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblInteres1.Name = "lblInteres1"
        Me.lblInteres1.Size = New System.Drawing.Size(63, 28)
        Me.lblInteres1.TabIndex = 55
        Me.lblInteres1.Text = "0.00"
        Me.lblInteres1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblInteres1.Visible = False
        '
        'txtMontoRecarga1
        '
        Me.txtMontoRecarga1.BackColor = System.Drawing.Color.DarkKhaki
        Me.txtMontoRecarga1.Decimals = CType(2, Byte)
        Me.txtMontoRecarga1.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtMontoRecarga1.Enabled = False
        Me.txtMontoRecarga1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoRecarga1.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtMontoRecarga1.Location = New System.Drawing.Point(3, 4)
        Me.txtMontoRecarga1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMontoRecarga1.Name = "txtMontoRecarga1"
        Me.txtMontoRecarga1.ReadOnly = True
        Me.txtMontoRecarga1.Size = New System.Drawing.Size(104, 22)
        Me.txtMontoRecarga1.TabIndex = 54
        Me.txtMontoRecarga1.Text_1 = Nothing
        Me.txtMontoRecarga1.Text_2 = Nothing
        Me.txtMontoRecarga1.Text_3 = Nothing
        Me.txtMontoRecarga1.Text_4 = Nothing
        Me.txtMontoRecarga1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMontoRecarga1.UserValues = Nothing
        Me.txtMontoRecarga1.Visible = False
        '
        'txtContado
        '
        Me.txtContado.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtContado.Decimals = CType(2, Byte)
        Me.txtContado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContado.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtContado.Location = New System.Drawing.Point(173, 38)
        Me.txtContado.Margin = New System.Windows.Forms.Padding(4)
        Me.txtContado.Name = "txtContado"
        Me.txtContado.Size = New System.Drawing.Size(256, 30)
        Me.txtContado.TabIndex = 1
        Me.txtContado.Text_1 = Nothing
        Me.txtContado.Text_2 = Nothing
        Me.txtContado.Text_3 = Nothing
        Me.txtContado.Text_4 = Nothing
        Me.txtContado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtContado.UserValues = Nothing
        '
        'lblM1
        '
        Me.lblM1.AutoSize = True
        Me.lblM1.BackColor = System.Drawing.Color.Transparent
        Me.lblM1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblM1.Location = New System.Drawing.Point(304, 94)
        Me.lblM1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblM1.Name = "lblM1"
        Me.lblM1.Size = New System.Drawing.Size(105, 18)
        Me.lblM1.TabIndex = 57
        Me.lblM1.Text = "Mto. x Ctas.:"
        Me.lblM1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblM1.Visible = False
        '
        'chkTarjetas2
        '
        Me.chkTarjetas2.AutoSize = True
        Me.chkTarjetas2.BackColor = System.Drawing.Color.Transparent
        Me.chkTarjetas2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTarjetas2.Location = New System.Drawing.Point(3, 286)
        Me.chkTarjetas2.Margin = New System.Windows.Forms.Padding(4)
        Me.chkTarjetas2.Name = "chkTarjetas2"
        Me.chkTarjetas2.Size = New System.Drawing.Size(141, 28)
        Me.chkTarjetas2.TabIndex = 4
        Me.chkTarjetas2.Tag = "2"
        Me.chkTarjetas2.Text = "Tarjeta 2 [F8]"
        Me.chkTarjetas2.UseVisualStyleBackColor = False
        '
        'chkTarjetas1
        '
        Me.chkTarjetas1.AutoSize = True
        Me.chkTarjetas1.BackColor = System.Drawing.Color.Transparent
        Me.chkTarjetas1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTarjetas1.Location = New System.Drawing.Point(3, 105)
        Me.chkTarjetas1.Margin = New System.Windows.Forms.Padding(4)
        Me.chkTarjetas1.Name = "chkTarjetas1"
        Me.chkTarjetas1.Size = New System.Drawing.Size(141, 28)
        Me.chkTarjetas1.TabIndex = 2
        Me.chkTarjetas1.Tag = "1"
        Me.chkTarjetas1.Text = "Tarjeta 1 [F7]"
        Me.chkTarjetas1.UseVisualStyleBackColor = False
        '
        'chkContado
        '
        Me.chkContado.AutoSize = True
        Me.chkContado.BackColor = System.Drawing.Color.Transparent
        Me.chkContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContado.ForeColor = System.Drawing.Color.Black
        Me.chkContado.Location = New System.Drawing.Point(4, 37)
        Me.chkContado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkContado.Name = "chkContado"
        Me.chkContado.Size = New System.Drawing.Size(149, 29)
        Me.chkContado.TabIndex = 0
        Me.chkContado.Tag = "0"
        Me.chkContado.Text = "Contado [F6]"
        Me.chkContado.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(20, 114)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 25)
        Me.Label1.TabIndex = 283
        Me.Label1.Text = "Cliente"
        '
        'lblDevolucion
        '
        Me.lblDevolucion.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.lblDevolucion.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash
        Me.lblDevolucion.BackgroundStyle.BorderBottomWidth = 1
        Me.lblDevolucion.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionInactiveText
        Me.lblDevolucion.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Dash
        Me.lblDevolucion.BackgroundStyle.BorderLeftWidth = 1
        Me.lblDevolucion.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Dash
        Me.lblDevolucion.BackgroundStyle.BorderRightWidth = 1
        Me.lblDevolucion.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Dash
        Me.lblDevolucion.BackgroundStyle.BorderTopWidth = 1
        Me.lblDevolucion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDevolucion.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDevolucion.ForeColor = System.Drawing.Color.Black
        Me.lblDevolucion.Location = New System.Drawing.Point(612, 6)
        Me.lblDevolucion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblDevolucion.Name = "lblDevolucion"
        Me.lblDevolucion.Size = New System.Drawing.Size(211, 34)
        Me.lblDevolucion.TabIndex = 282
        Me.lblDevolucion.Text = "Devolución"
        Me.lblDevolucion.Visible = False
        '
        'txtCuit
        '
        Me.txtCuit.AccessibleName = ""
        Me.txtCuit.Decimals = CType(2, Byte)
        Me.txtCuit.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCuit.Format = TextBoxConFormatoVB.tbFormats.SignedNumber
        Me.txtCuit.Location = New System.Drawing.Point(865, 81)
        Me.txtCuit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCuit.MaxLength = 11
        Me.txtCuit.Name = "txtCuit"
        Me.txtCuit.Size = New System.Drawing.Size(257, 30)
        Me.txtCuit.TabIndex = 3
        Me.txtCuit.Text_1 = Nothing
        Me.txtCuit.Text_2 = Nothing
        Me.txtCuit.Text_3 = Nothing
        Me.txtCuit.Text_4 = Nothing
        Me.txtCuit.UserValues = Nothing
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(539, 54)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(227, 25)
        Me.Label15.TabIndex = 280
        Me.Label15.Text = "Tipo de Comprobante "
        '
        'cmbTipoComprobante
        '
        Me.cmbTipoComprobante.AccessibleName = "*Comprobante"
        Me.cmbTipoComprobante.DropDownHeight = 450
        Me.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoComprobante.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoComprobante.FormattingEnabled = True
        Me.cmbTipoComprobante.IntegralHeight = False
        Me.cmbTipoComprobante.Location = New System.Drawing.Point(544, 84)
        Me.cmbTipoComprobante.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTipoComprobante.Name = "cmbTipoComprobante"
        Me.cmbTipoComprobante.Size = New System.Drawing.Size(313, 28)
        Me.cmbTipoComprobante.TabIndex = 2
        '
        'lblNumVendedor
        '
        Me.lblNumVendedor.BackColor = System.Drawing.Color.Transparent
        Me.lblNumVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumVendedor.ForeColor = System.Drawing.Color.Black
        Me.lblNumVendedor.Location = New System.Drawing.Point(203, 31)
        Me.lblNumVendedor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNumVendedor.Name = "lblNumVendedor"
        Me.lblNumVendedor.Size = New System.Drawing.Size(45, 25)
        Me.lblNumVendedor.TabIndex = 278
        Me.lblNumVendedor.Text = "N°"
        Me.lblNumVendedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(21, 31)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(165, 25)
        Me.Label11.TabIndex = 277
        Me.Label11.Text = "Nro. Vendedor@:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(1313, 639)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 31)
        Me.Label2.TabIndex = 276
        Me.Label2.Text = "IVA"
        '
        'txtIDPrecioLista
        '
        Me.txtIDPrecioLista.AccessibleName = ""
        Me.txtIDPrecioLista.Decimals = CType(2, Byte)
        Me.txtIDPrecioLista.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDPrecioLista.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDPrecioLista.Location = New System.Drawing.Point(337, 114)
        Me.txtIDPrecioLista.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIDPrecioLista.MaxLength = 100
        Me.txtIDPrecioLista.Name = "txtIDPrecioLista"
        Me.txtIDPrecioLista.Size = New System.Drawing.Size(29, 22)
        Me.txtIDPrecioLista.TabIndex = 272
        Me.txtIDPrecioLista.Text_1 = Nothing
        Me.txtIDPrecioLista.Text_2 = Nothing
        Me.txtIDPrecioLista.Text_3 = Nothing
        Me.txtIDPrecioLista.Text_4 = Nothing
        Me.txtIDPrecioLista.UserValues = Nothing
        Me.txtIDPrecioLista.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(20, 193)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(126, 25)
        Me.Label16.TabIndex = 270
        Me.Label16.Text = "Código [F5]"
        '
        'txtCodigoBarra
        '
        Me.txtCodigoBarra.AccessibleName = ""
        Me.txtCodigoBarra.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoBarra.Decimals = CType(2, Byte)
        Me.txtCodigoBarra.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCodigoBarra.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoBarra.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCodigoBarra.Location = New System.Drawing.Point(20, 218)
        Me.txtCodigoBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoBarra.MaxLength = 100
        Me.txtCodigoBarra.Name = "txtCodigoBarra"
        Me.txtCodigoBarra.Size = New System.Drawing.Size(227, 30)
        Me.txtCodigoBarra.TabIndex = 7
        Me.txtCodigoBarra.Text_1 = Nothing
        Me.txtCodigoBarra.Text_2 = Nothing
        Me.txtCodigoBarra.Text_3 = Nothing
        Me.txtCodigoBarra.Text_4 = Nothing
        Me.txtCodigoBarra.UserValues = Nothing
        '
        'lblPeso
        '
        Me.lblPeso.AutoSize = True
        Me.lblPeso.BackColor = System.Drawing.Color.Transparent
        Me.lblPeso.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeso.ForeColor = System.Drawing.Color.Blue
        Me.lblPeso.Location = New System.Drawing.Point(993, 192)
        Me.lblPeso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPeso.Name = "lblPeso"
        Me.lblPeso.Size = New System.Drawing.Size(61, 25)
        Me.lblPeso.TabIndex = 268
        Me.lblPeso.Text = "Peso"
        '
        'txtPeso
        '
        Me.txtPeso.AccessibleName = ""
        Me.txtPeso.BackColor = System.Drawing.Color.White
        Me.txtPeso.Decimals = CType(2, Byte)
        Me.txtPeso.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPeso.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeso.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPeso.Location = New System.Drawing.Point(999, 220)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPeso.MaxLength = 7
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.ReadOnly = True
        Me.txtPeso.Size = New System.Drawing.Size(124, 30)
        Me.txtPeso.TabIndex = 10
        Me.txtPeso.Text_1 = Nothing
        Me.txtPeso.Text_2 = Nothing
        Me.txtPeso.Text_3 = Nothing
        Me.txtPeso.Text_4 = Nothing
        Me.txtPeso.UserValues = Nothing
        '
        'lblVendedor
        '
        Me.lblVendedor.BackColor = System.Drawing.Color.Transparent
        Me.lblVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendedor.ForeColor = System.Drawing.Color.Black
        Me.lblVendedor.Location = New System.Drawing.Point(161, 6)
        Me.lblVendedor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVendedor.Name = "lblVendedor"
        Me.lblVendedor.Size = New System.Drawing.Size(381, 25)
        Me.lblVendedor.TabIndex = 0
        Me.lblVendedor.Text = "Vendedor"
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.AccessibleName = ""
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdUnidad.Location = New System.Drawing.Point(789, 190)
        Me.txtIdUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUnidad.MaxLength = 100
        Me.txtIdUnidad.Name = "txtIdUnidad"
        Me.txtIdUnidad.Size = New System.Drawing.Size(31, 22)
        Me.txtIdUnidad.TabIndex = 250
        Me.txtIdUnidad.Text_1 = Nothing
        Me.txtIdUnidad.Text_2 = Nothing
        Me.txtIdUnidad.Text_3 = Nothing
        Me.txtIdUnidad.Text_4 = Nothing
        Me.txtIdUnidad.UserValues = Nothing
        Me.txtIdUnidad.Visible = False
        '
        'chkDevolucion
        '
        Me.chkDevolucion.AccessibleName = "Eliminado"
        Me.chkDevolucion.AutoSize = True
        Me.chkDevolucion.BackColor = System.Drawing.Color.Transparent
        Me.chkDevolucion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDevolucion.ForeColor = System.Drawing.Color.Red
        Me.chkDevolucion.Location = New System.Drawing.Point(260, 619)
        Me.chkDevolucion.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDevolucion.Name = "chkDevolucion"
        Me.chkDevolucion.Size = New System.Drawing.Size(124, 21)
        Me.chkDevolucion.TabIndex = 249
        Me.chkDevolucion.Text = "Ver Fac. Anu"
        Me.chkDevolucion.UseVisualStyleBackColor = False
        '
        'txtIdCliente
        '
        Me.txtIdCliente.AccessibleName = ""
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdCliente.Location = New System.Drawing.Point(300, 114)
        Me.txtIdCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdCliente.MaxLength = 100
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(29, 22)
        Me.txtIdCliente.TabIndex = 248
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(1584, 639)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 31)
        Me.Label8.TabIndex = 247
        Me.Label8.Text = "TOTAL"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(960, 639)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(158, 31)
        Me.Label7.TabIndex = 241
        Me.Label7.Text = "SUBTOTAL"
        '
        'txtSubtotalItem
        '
        Me.txtSubtotalItem.AccessibleName = ""
        Me.txtSubtotalItem.BackColor = System.Drawing.Color.White
        Me.txtSubtotalItem.Decimals = CType(2, Byte)
        Me.txtSubtotalItem.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalItem.Enabled = False
        Me.txtSubtotalItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotalItem.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotalItem.Location = New System.Drawing.Point(508, 610)
        Me.txtSubtotalItem.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSubtotalItem.MaxLength = 100
        Me.txtSubtotalItem.Name = "txtSubtotalItem"
        Me.txtSubtotalItem.ReadOnly = True
        Me.txtSubtotalItem.Size = New System.Drawing.Size(79, 26)
        Me.txtSubtotalItem.TabIndex = 10
        Me.txtSubtotalItem.Text_1 = Nothing
        Me.txtSubtotalItem.Text_2 = Nothing
        Me.txtSubtotalItem.Text_3 = Nothing
        Me.txtSubtotalItem.Text_4 = Nothing
        Me.txtSubtotalItem.UserValues = Nothing
        Me.txtSubtotalItem.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(403, 613)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 20)
        Me.Label5.TabIndex = 238
        Me.Label5.Text = "Subtotalitem"
        Me.Label5.Visible = False
        '
        'txtPrecio
        '
        Me.txtPrecio.AccessibleName = ""
        Me.txtPrecio.BackColor = System.Drawing.Color.White
        Me.txtPrecio.Decimals = CType(2, Byte)
        Me.txtPrecio.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPrecio.Enabled = False
        Me.txtPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecio.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPrecio.Location = New System.Drawing.Point(865, 220)
        Me.txtPrecio.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrecio.MaxLength = 100
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.ReadOnly = True
        Me.txtPrecio.Size = New System.Drawing.Size(124, 30)
        Me.txtPrecio.TabIndex = 9
        Me.txtPrecio.Text_1 = Nothing
        Me.txtPrecio.Text_2 = Nothing
        Me.txtPrecio.Text_3 = Nothing
        Me.txtPrecio.Text_4 = Nothing
        Me.txtPrecio.UserValues = Nothing
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(861, 192)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 25)
        Me.Label12.TabIndex = 236
        Me.Label12.Text = "Precio"
        '
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        Me.grdItems.AllowUserToResizeColumns = False
        Me.grdItems.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.grdItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.CodMaterial, Me.CodigoBarra, Me.EquipoHerramienta, Me.Cantidad, Me.Peso, Me.Presio, Me.PrecioUni, Me.PrecioSinIVA, Me.Desc_Unit, Me.Subtotal, Me.IdUnidad, Me.SubtotalOrig, Me.SubtotalSinIVA, Me.ProdPesable, Me.Eliminar})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle8
        Me.grdItems.Location = New System.Drawing.Point(21, 262)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.MultiSelect = False
        Me.grdItems.Name = "grdItems"
        Me.grdItems.ReadOnly = True
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.grdItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdItems.Size = New System.Drawing.Size(1289, 338)
        Me.grdItems.TabIndex = 13
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Visible = False
        '
        'CodMaterial
        '
        Me.CodMaterial.HeaderText = "CodMaterial"
        Me.CodMaterial.Name = "CodMaterial"
        Me.CodMaterial.ReadOnly = True
        Me.CodMaterial.Visible = False
        '
        'CodigoBarra
        '
        Me.CodigoBarra.HeaderText = "CodBarra"
        Me.CodigoBarra.Name = "CodigoBarra"
        Me.CodigoBarra.ReadOnly = True
        '
        'EquipoHerramienta
        '
        Me.EquipoHerramienta.HeaderText = "Producto"
        Me.EquipoHerramienta.Name = "EquipoHerramienta"
        Me.EquipoHerramienta.ReadOnly = True
        '
        'Cantidad
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle3
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.MaxInputLength = 2
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        '
        'Peso
        '
        Me.Peso.HeaderText = "Peso"
        Me.Peso.Name = "Peso"
        Me.Peso.ReadOnly = True
        '
        'Presio
        '
        Me.Presio.HeaderText = "Precio"
        Me.Presio.Name = "Presio"
        Me.Presio.ReadOnly = True
        '
        'PrecioUni
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrecioUni.DefaultCellStyle = DataGridViewCellStyle4
        Me.PrecioUni.HeaderText = "PrecioVenta"
        Me.PrecioUni.Name = "PrecioUni"
        Me.PrecioUni.ReadOnly = True
        Me.PrecioUni.Visible = False
        '
        'PrecioSinIVA
        '
        Me.PrecioSinIVA.HeaderText = "PrecioSinIVA"
        Me.PrecioSinIVA.Name = "PrecioSinIVA"
        Me.PrecioSinIVA.ReadOnly = True
        Me.PrecioSinIVA.Visible = False
        '
        'Desc_Unit
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.Desc_Unit.DefaultCellStyle = DataGridViewCellStyle5
        Me.Desc_Unit.HeaderText = "Desc($)"
        Me.Desc_Unit.Name = "Desc_Unit"
        Me.Desc_Unit.ReadOnly = True
        '
        'Subtotal
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Subtotal.DefaultCellStyle = DataGridViewCellStyle6
        Me.Subtotal.HeaderText = "Subtotal"
        Me.Subtotal.Name = "Subtotal"
        Me.Subtotal.ReadOnly = True
        '
        'IdUnidad
        '
        Me.IdUnidad.HeaderText = "IdUnidad"
        Me.IdUnidad.Name = "IdUnidad"
        Me.IdUnidad.ReadOnly = True
        Me.IdUnidad.Visible = False
        '
        'SubtotalOrig
        '
        Me.SubtotalOrig.HeaderText = "SubtotalOrig"
        Me.SubtotalOrig.Name = "SubtotalOrig"
        Me.SubtotalOrig.ReadOnly = True
        Me.SubtotalOrig.Visible = False
        '
        'SubtotalSinIVA
        '
        Me.SubtotalSinIVA.HeaderText = "SubtotalSinIVA"
        Me.SubtotalSinIVA.Name = "SubtotalSinIVA"
        Me.SubtotalSinIVA.ReadOnly = True
        Me.SubtotalSinIVA.Visible = False
        '
        'ProdPesable
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.NullValue = False
        Me.ProdPesable.DefaultCellStyle = DataGridViewCellStyle7
        Me.ProdPesable.HeaderText = "ProdPesable"
        Me.ProdPesable.Name = "ProdPesable"
        Me.ProdPesable.ReadOnly = True
        Me.ProdPesable.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ProdPesable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ProdPesable.Visible = False
        '
        'Eliminar
        '
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.ReadOnly = True
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.ToolTipText = "Eliminar Registro"
        Me.Eliminar.UseColumnTextForButtonValue = True
        '
        'chkDevuelto
        '
        Me.chkDevuelto.AutoSize = True
        Me.chkDevuelto.Location = New System.Drawing.Point(833, 10)
        Me.chkDevuelto.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDevuelto.Name = "chkDevuelto"
        Me.chkDevuelto.Size = New System.Drawing.Size(100, 21)
        Me.chkDevuelto.TabIndex = 211
        Me.chkDevuelto.Text = "Devolucion"
        Me.chkDevuelto.UseVisualStyleBackColor = True
        Me.chkDevuelto.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(23, 1)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(124, 25)
        Me.Label10.TabIndex = 152
        Me.Label10.Text = "Vendedor@:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(1127, 190)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 25)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Cantidad"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(863, 58)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 25)
        Me.Label6.TabIndex = 146
        Me.Label6.Text = "DNI/CUIT"
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = ""
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Enabled = False
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(256, 218)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(600, 33)
        Me.cmbProducto.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(255, 193)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(380, 25)
        Me.Label4.TabIndex = 139
        Me.Label4.Text = "Producto (Ctrl+P para abrir búsqueda)"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(141, 57)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.MaxLength = 8
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(29, 22)
        Me.txtID.TabIndex = 128
        Me.txtID.Text_1 = Nothing
        Me.txtID.Text_2 = Nothing
        Me.txtID.Text_3 = Nothing
        Me.txtID.Text_4 = Nothing
        Me.txtID.UserValues = Nothing
        Me.txtID.Visible = False
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(964, 14)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(3000, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(159, 26)
        Me.dtpFECHA.TabIndex = 0
        Me.dtpFECHA.Tag = "202"
        Me.dtpFECHA.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(20, 55)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 25)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Fecha"
        '
        'txtIdProducto
        '
        Me.txtIdProducto.AccessibleName = ""
        Me.txtIdProducto.Decimals = CType(2, Byte)
        Me.txtIdProducto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProducto.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdProducto.Location = New System.Drawing.Point(264, 224)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdProducto.MaxLength = 100
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(82, 22)
        Me.txtIdProducto.TabIndex = 239
        Me.txtIdProducto.Text_1 = Nothing
        Me.txtIdProducto.Text_2 = Nothing
        Me.txtIdProducto.Text_3 = Nothing
        Me.txtIdProducto.Text_4 = Nothing
        Me.txtIdProducto.UserValues = Nothing
        '
        'TimerVentas
        '
        Me.TimerVentas.Interval = 10000
        '
        'frmVentaSalon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1796, 856)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.Name = "frmVentaSalon"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ventas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PicSincro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDescuento.ResumeLayout(False)
        Me.PanelDescuento.PerformLayout()
        Me.GroupBoxPago.ResumeLayout(False)
        Me.GroupBoxPago.PerformLayout()
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
    Friend WithEvents dtpFECHA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkDevuelto As System.Windows.Forms.CheckBox
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotalItem As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPrecio As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkDescuentoGlobal As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents rdAbsoluto As System.Windows.Forms.RadioButton
    Friend WithEvents rdPorcentaje As System.Windows.Forms.RadioButton
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblVendedor As System.Windows.Forms.Label
    Friend WithEvents lblPeso As System.Windows.Forms.Label
    Friend WithEvents txtPeso As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtIDPrecioLista As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblNumVendedor As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCliente As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupBoxPago As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDevolucion As DevComponents.DotNetBar.LabelX
    Public WithEvents chkTarjetas2 As System.Windows.Forms.CheckBox
    Public WithEvents chkTarjetas1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkContado As System.Windows.Forms.CheckBox
    Friend WithEvents lblMontoPorCuotas1 As System.Windows.Forms.Label
    Friend WithEvents lblInteres1 As System.Windows.Forms.Label
    Public WithEvents txtMontoRecarga1 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtContado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblM1 As System.Windows.Forms.Label
    Friend WithEvents txtTarjeta1 As System.Windows.Forms.TextBox
    Friend WithEvents lstTarjetas1 As System.Windows.Forms.ListBox
    Public WithEvents txtTarjetas1ImporteFinal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblImporteFinal1 As System.Windows.Forms.Label
    Public WithEvents txtTarjetas1Importe As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTarjetasImporte1 As System.Windows.Forms.Label
    Friend WithEvents lblInteres2 As System.Windows.Forms.Label
    Friend WithEvents lblMontoPorCuotas2 As System.Windows.Forms.Label
    Public WithEvents txtMontoRecarga2 As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblM2 As System.Windows.Forms.Label
    Friend WithEvents LstTarjetas2 As System.Windows.Forms.ListBox
    Friend WithEvents txtTarjeta2 As System.Windows.Forms.TextBox
    Public WithEvents txtVuelto As System.Windows.Forms.Label
    Friend WithEvents lblResto As System.Windows.Forms.Label
    Friend WithEvents lblVuelto As System.Windows.Forms.Label
    Public WithEvents txtResto As System.Windows.Forms.Label
    Public WithEvents txtTarjetas2ImporteFinal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblImporteFinal2 As System.Windows.Forms.Label
    Public WithEvents txtTarjetas2Importe As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblTarjetaImporte2 As System.Windows.Forms.Label
    Friend WithEvents txtFormaPago As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblCandadoContado As System.Windows.Forms.Label
    Friend WithEvents lblCandadoTarjetas1 As System.Windows.Forms.Label
    Friend WithEvents lblCandadoTarjetas2 As System.Windows.Forms.Label
    Friend WithEvents lblValorDescontado As System.Windows.Forms.Label
    Friend WithEvents lblValorDescontadoL As System.Windows.Forms.Label
    Friend WithEvents txtTotalOriginal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtSubtotalVista As System.Windows.Forms.Label
    Friend WithEvents txtIVAVista As System.Windows.Forms.Label
    Friend WithEvents txtTotalVista As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As System.Windows.Forms.Label
    Friend WithEvents txtIVA As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.Label
    Friend WithEvents btnModCant As DevComponents.DotNetBar.ButtonX
    Public WithEvents grdItems As System.Windows.Forms.DataGridView
    Public WithEvents txtCuit As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents cmbTipoComprobante As System.Windows.Forms.ComboBox
    Public WithEvents txtCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents lblClienteDireccion As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents cmbCondicionIVA As System.Windows.Forms.ComboBox
    Friend WithEvents txtIDTipoComprobante As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIDCondicionIVA As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents txtClienteDireccion As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkDescuentoParticular As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblCantidadFilas As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Public WithEvents txtCodigoBarra As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents PanelDescuento As System.Windows.Forms.Panel
    Public WithEvents lblCodigo As DevComponents.DotNetBar.LabelX
    Public WithEvents chkDevolucion As System.Windows.Forms.CheckBox
    Friend WithEvents lblPVI As System.Windows.Forms.Label
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents bntVerFactura As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PicSincro As System.Windows.Forms.PictureBox
    Friend WithEvents btnSincronizar As System.Windows.Forms.Button
    Friend WithEvents TimerVentas As System.Windows.Forms.Timer
    Friend WithEvents lblModo As System.Windows.Forms.Label
    Friend WithEvents lblContadorCuit As System.Windows.Forms.Label
    Friend WithEvents lblContadorCliente As System.Windows.Forms.Label
    Public WithEvents chkConexion As System.Windows.Forms.CheckBox
    Public WithEvents chkCtaCte As System.Windows.Forms.CheckBox
    Public WithEvents txtIdProducto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodMaterial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodigoBarra As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EquipoHerramienta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Peso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Presio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecioUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecioSinIVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Desc_Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subtotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubtotalOrig As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubtotalSinIVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProdPesable As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents lblValorDescSinIVa As System.Windows.Forms.Label

End Class
