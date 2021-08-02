<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVentasMostrador
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarElItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuscarDescripcionToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox()
        Me.GroupBox1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtLectura = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIDPrecioLista = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkPagoConEfec = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCodigoBarra = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPeso = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkTransInterna = New System.Windows.Forms.CheckBox()
        Me.chkPrecioMayorista = New System.Windows.Forms.CheckBox()
        Me.lblTipoOperacion = New System.Windows.Forms.Label()
        Me.lblContado = New System.Windows.Forms.Label()
        Me.lblCtaCte = New System.Windows.Forms.Label()
        Me.lblFacturado = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblVendedor = New System.Windows.Forms.Label()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.chkEliminado = New System.Windows.Forms.CheckBox()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtTotalVenta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDescuento = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.rdAbsoluto = New System.Windows.Forms.RadioButton()
        Me.rdPorcentaje = New System.Windows.Forms.RadioButton()
        Me.chkDescuento = New System.Windows.Forms.CheckBox()
        Me.txtSubtotalVenta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtIdProducto = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtSubtotal = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPrecioVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CodMaterial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EquipoHerramienta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Peso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrecioUni = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc_Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc_Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubtotalFinal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Eliminar = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.chkDevuelto = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCantidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.cmbCliente = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtCODIGO = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFECHA = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.txtLectura)
        Me.GroupBox1.Controls.Add(Me.txtIDPrecioLista)
        Me.GroupBox1.Controls.Add(Me.chkPagoConEfec)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtCodigoBarra)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtPeso)
        Me.GroupBox1.Controls.Add(Me.chkTransInterna)
        Me.GroupBox1.Controls.Add(Me.chkPrecioMayorista)
        Me.GroupBox1.Controls.Add(Me.lblTipoOperacion)
        Me.GroupBox1.Controls.Add(Me.lblContado)
        Me.GroupBox1.Controls.Add(Me.lblCtaCte)
        Me.GroupBox1.Controls.Add(Me.lblFacturado)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblStock)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblVendedor)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.chkEliminado)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtTotalVenta)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtDescuento)
        Me.GroupBox1.Controls.Add(Me.rdAbsoluto)
        Me.GroupBox1.Controls.Add(Me.rdPorcentaje)
        Me.GroupBox1.Controls.Add(Me.chkDescuento)
        Me.GroupBox1.Controls.Add(Me.txtSubtotalVenta)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtIdProducto)
        Me.GroupBox1.Controls.Add(Me.txtSubtotal)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPrecioVta)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtObservaciones)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.chkDevuelto)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbProducto)
        Me.GroupBox1.Controls.Add(Me.cmbCliente)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.txtCODIGO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpFECHA)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(16, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1359, 655)
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
        'txtLectura
        '
        Me.txtLectura.AccessibleName = ""
        Me.txtLectura.Decimals = CType(2, Byte)
        Me.txtLectura.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtLectura.Enabled = False
        Me.txtLectura.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLectura.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtLectura.Location = New System.Drawing.Point(810, 121)
        Me.txtLectura.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLectura.MaxLength = 100
        Me.txtLectura.Name = "txtLectura"
        Me.txtLectura.Size = New System.Drawing.Size(254, 24)
        Me.txtLectura.TabIndex = 273
        Me.txtLectura.Text_1 = Nothing
        Me.txtLectura.Text_2 = Nothing
        Me.txtLectura.Text_3 = Nothing
        Me.txtLectura.Text_4 = Nothing
        Me.txtLectura.UserValues = Nothing
        Me.txtLectura.Visible = False
        '
        'txtIDPrecioLista
        '
        Me.txtIDPrecioLista.AccessibleName = ""
        Me.txtIDPrecioLista.Decimals = CType(2, Byte)
        Me.txtIDPrecioLista.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIDPrecioLista.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIDPrecioLista.Location = New System.Drawing.Point(304, 67)
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
        'chkPagoConEfec
        '
        Me.chkPagoConEfec.AccessibleName = "Eliminado"
        Me.chkPagoConEfec.AutoSize = True
        Me.chkPagoConEfec.BackColor = System.Drawing.Color.Transparent
        Me.chkPagoConEfec.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPagoConEfec.ForeColor = System.Drawing.Color.Blue
        Me.chkPagoConEfec.Location = New System.Drawing.Point(1098, 32)
        Me.chkPagoConEfec.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPagoConEfec.Name = "chkPagoConEfec"
        Me.chkPagoConEfec.Size = New System.Drawing.Size(248, 24)
        Me.chkPagoConEfec.TabIndex = 271
        Me.chkPagoConEfec.Text = "Pago de Contado Efectivo"
        Me.chkPagoConEfec.UseVisualStyleBackColor = False
        Me.chkPagoConEfec.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(17, 143)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(61, 20)
        Me.Label16.TabIndex = 270
        Me.Label16.Text = "Código"
        '
        'txtCodigoBarra
        '
        Me.txtCodigoBarra.AccessibleName = ""
        Me.txtCodigoBarra.Decimals = CType(2, Byte)
        Me.txtCodigoBarra.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCodigoBarra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoBarra.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCodigoBarra.Location = New System.Drawing.Point(21, 166)
        Me.txtCodigoBarra.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoBarra.MaxLength = 100
        Me.txtCodigoBarra.Name = "txtCodigoBarra"
        Me.txtCodigoBarra.Size = New System.Drawing.Size(67, 26)
        Me.txtCodigoBarra.TabIndex = 4
        Me.txtCodigoBarra.Text_1 = Nothing
        Me.txtCodigoBarra.Text_2 = Nothing
        Me.txtCodigoBarra.Text_3 = Nothing
        Me.txtCodigoBarra.Text_4 = Nothing
        Me.txtCodigoBarra.UserValues = Nothing
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(898, 147)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 20)
        Me.Label13.TabIndex = 268
        Me.Label13.Text = "Peso"
        '
        'txtPeso
        '
        Me.txtPeso.AccessibleName = ""
        Me.txtPeso.Decimals = CType(2, Byte)
        Me.txtPeso.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPeso.Enabled = False
        Me.txtPeso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeso.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPeso.Location = New System.Drawing.Point(903, 168)
        Me.txtPeso.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPeso.MaxLength = 100
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(79, 26)
        Me.txtPeso.TabIndex = 7
        Me.txtPeso.Text_1 = Nothing
        Me.txtPeso.Text_2 = Nothing
        Me.txtPeso.Text_3 = Nothing
        Me.txtPeso.Text_4 = Nothing
        Me.txtPeso.UserValues = Nothing
        '
        'chkTransInterna
        '
        Me.chkTransInterna.AccessibleName = "Eliminado"
        Me.chkTransInterna.AutoSize = True
        Me.chkTransInterna.BackColor = System.Drawing.Color.Transparent
        Me.chkTransInterna.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransInterna.ForeColor = System.Drawing.Color.Black
        Me.chkTransInterna.Location = New System.Drawing.Point(420, 14)
        Me.chkTransInterna.Margin = New System.Windows.Forms.Padding(4)
        Me.chkTransInterna.Name = "chkTransInterna"
        Me.chkTransInterna.Size = New System.Drawing.Size(213, 21)
        Me.chkTransInterna.TabIndex = 266
        Me.chkTransInterna.Text = "Transferencia a Sucursal"
        Me.chkTransInterna.UseVisualStyleBackColor = False
        Me.chkTransInterna.Visible = False
        '
        'chkPrecioMayorista
        '
        Me.chkPrecioMayorista.AccessibleName = "Eliminado"
        Me.chkPrecioMayorista.AutoSize = True
        Me.chkPrecioMayorista.BackColor = System.Drawing.Color.Transparent
        Me.chkPrecioMayorista.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrecioMayorista.ForeColor = System.Drawing.Color.Blue
        Me.chkPrecioMayorista.Location = New System.Drawing.Point(469, 143)
        Me.chkPrecioMayorista.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPrecioMayorista.Name = "chkPrecioMayorista"
        Me.chkPrecioMayorista.Size = New System.Drawing.Size(239, 21)
        Me.chkPrecioMayorista.TabIndex = 265
        Me.chkPrecioMayorista.Text = "Vender con Precio Mayorista"
        Me.chkPrecioMayorista.UseVisualStyleBackColor = False
        Me.chkPrecioMayorista.Visible = False
        '
        'lblTipoOperacion
        '
        Me.lblTipoOperacion.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoOperacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoOperacion.ForeColor = System.Drawing.Color.Green
        Me.lblTipoOperacion.Location = New System.Drawing.Point(1183, -3)
        Me.lblTipoOperacion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTipoOperacion.Name = "lblTipoOperacion"
        Me.lblTipoOperacion.Size = New System.Drawing.Size(166, 31)
        Me.lblTipoOperacion.TabIndex = 264
        Me.lblTipoOperacion.Text = "Nueva Venta"
        Me.lblTipoOperacion.Visible = False
        '
        'lblContado
        '
        Me.lblContado.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContado.ForeColor = System.Drawing.Color.Black
        Me.lblContado.Location = New System.Drawing.Point(1245, 88)
        Me.lblContado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblContado.Name = "lblContado"
        Me.lblContado.Size = New System.Drawing.Size(96, 25)
        Me.lblContado.TabIndex = 263
        Me.lblContado.Text = "0"
        Me.lblContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCtaCte
        '
        Me.lblCtaCte.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCtaCte.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtaCte.ForeColor = System.Drawing.Color.Black
        Me.lblCtaCte.Location = New System.Drawing.Point(1245, 115)
        Me.lblCtaCte.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCtaCte.Name = "lblCtaCte"
        Me.lblCtaCte.Size = New System.Drawing.Size(96, 25)
        Me.lblCtaCte.TabIndex = 262
        Me.lblCtaCte.Text = "0"
        Me.lblCtaCte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFacturado
        '
        Me.lblFacturado.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFacturado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFacturado.ForeColor = System.Drawing.Color.Black
        Me.lblFacturado.Location = New System.Drawing.Point(1245, 60)
        Me.lblFacturado.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFacturado.Name = "lblFacturado"
        Me.lblFacturado.Size = New System.Drawing.Size(96, 25)
        Me.lblFacturado.TabIndex = 261
        Me.lblFacturado.Text = "0"
        Me.lblFacturado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Blue
        Me.Label17.Location = New System.Drawing.Point(1169, 115)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(66, 20)
        Me.Label17.TabIndex = 260
        Me.Label17.Text = "Cta Cte"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(1158, 88)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 20)
        Me.Label15.TabIndex = 258
        Me.Label15.Text = "Contado"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(1079, 61)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(149, 20)
        Me.Label14.TabIndex = 256
        Me.Label14.Text = "Facturación Actual"
        '
        'lblStock
        '
        Me.lblStock.BackColor = System.Drawing.Color.Red
        Me.lblStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStock.ForeColor = System.Drawing.Color.White
        Me.lblStock.Location = New System.Drawing.Point(725, 169)
        Me.lblStock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(71, 25)
        Me.lblStock.TabIndex = 254
        Me.lblStock.Text = "Stock"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(725, 146)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 20)
        Me.Label11.TabIndex = 253
        Me.Label11.Text = "Stock"
        '
        'lblVendedor
        '
        Me.lblVendedor.BackColor = System.Drawing.Color.Transparent
        Me.lblVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendedor.ForeColor = System.Drawing.Color.Blue
        Me.lblVendedor.Location = New System.Drawing.Point(741, 40)
        Me.lblVendedor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVendedor.Name = "lblVendedor"
        Me.lblVendedor.Size = New System.Drawing.Size(231, 25)
        Me.lblVendedor.TabIndex = 251
        Me.lblVendedor.Text = "Vendedor"
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.AccessibleName = ""
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdUnidad.Location = New System.Drawing.Point(214, 141)
        Me.txtIdUnidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdUnidad.MaxLength = 100
        Me.txtIdUnidad.Name = "txtIdUnidad"
        Me.txtIdUnidad.Size = New System.Drawing.Size(30, 22)
        Me.txtIdUnidad.TabIndex = 250
        Me.txtIdUnidad.Text_1 = Nothing
        Me.txtIdUnidad.Text_2 = Nothing
        Me.txtIdUnidad.Text_3 = Nothing
        Me.txtIdUnidad.Text_4 = Nothing
        Me.txtIdUnidad.UserValues = Nothing
        Me.txtIdUnidad.Visible = False
        '
        'chkEliminado
        '
        Me.chkEliminado.AccessibleName = "Eliminado"
        Me.chkEliminado.AutoSize = True
        Me.chkEliminado.BackColor = System.Drawing.Color.Transparent
        Me.chkEliminado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEliminado.ForeColor = System.Drawing.Color.Red
        Me.chkEliminado.Location = New System.Drawing.Point(1185, 171)
        Me.chkEliminado.Margin = New System.Windows.Forms.Padding(4)
        Me.chkEliminado.Name = "chkEliminado"
        Me.chkEliminado.Size = New System.Drawing.Size(166, 21)
        Me.chkEliminado.TabIndex = 249
        Me.chkEliminado.Text = "Ver Mov. Anulados"
        Me.chkEliminado.UseVisualStyleBackColor = False
        '
        'txtIdCliente
        '
        Me.txtIdCliente.AccessibleName = ""
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdCliente.Location = New System.Drawing.Point(338, 67)
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
        'txtTotalVenta
        '
        Me.txtTotalVenta.AccessibleName = ""
        Me.txtTotalVenta.Decimals = CType(2, Byte)
        Me.txtTotalVenta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTotalVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalVenta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtTotalVenta.Location = New System.Drawing.Point(1126, 611)
        Me.txtTotalVenta.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTotalVenta.MaxLength = 100
        Me.txtTotalVenta.Name = "txtTotalVenta"
        Me.txtTotalVenta.ReadOnly = True
        Me.txtTotalVenta.Size = New System.Drawing.Size(97, 26)
        Me.txtTotalVenta.TabIndex = 246
        Me.txtTotalVenta.Text_1 = Nothing
        Me.txtTotalVenta.Text_2 = Nothing
        Me.txtTotalVenta.Text_3 = Nothing
        Me.txtTotalVenta.Text_4 = Nothing
        Me.txtTotalVenta.UserValues = Nothing
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(1122, 588)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 20)
        Me.Label8.TabIndex = 247
        Me.Label8.Text = "Total"
        '
        'txtDescuento
        '
        Me.txtDescuento.AccessibleName = ""
        Me.txtDescuento.Decimals = CType(2, Byte)
        Me.txtDescuento.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtDescuento.Enabled = False
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtDescuento.Location = New System.Drawing.Point(927, 611)
        Me.txtDescuento.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDescuento.MaxLength = 100
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(79, 26)
        Me.txtDescuento.TabIndex = 13
        Me.txtDescuento.Text_1 = Nothing
        Me.txtDescuento.Text_2 = Nothing
        Me.txtDescuento.Text_3 = Nothing
        Me.txtDescuento.Text_4 = Nothing
        Me.txtDescuento.UserValues = Nothing
        '
        'rdAbsoluto
        '
        Me.rdAbsoluto.AutoSize = True
        Me.rdAbsoluto.BackColor = System.Drawing.Color.Transparent
        Me.rdAbsoluto.Enabled = False
        Me.rdAbsoluto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAbsoluto.Location = New System.Drawing.Point(872, 615)
        Me.rdAbsoluto.Margin = New System.Windows.Forms.Padding(4)
        Me.rdAbsoluto.Name = "rdAbsoluto"
        Me.rdAbsoluto.Size = New System.Drawing.Size(40, 24)
        Me.rdAbsoluto.TabIndex = 12
        Me.rdAbsoluto.TabStop = True
        Me.rdAbsoluto.Text = "$"
        Me.rdAbsoluto.UseVisualStyleBackColor = False
        '
        'rdPorcentaje
        '
        Me.rdPorcentaje.AutoSize = True
        Me.rdPorcentaje.BackColor = System.Drawing.Color.Transparent
        Me.rdPorcentaje.Enabled = False
        Me.rdPorcentaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdPorcentaje.Location = New System.Drawing.Point(812, 615)
        Me.rdPorcentaje.Margin = New System.Windows.Forms.Padding(4)
        Me.rdPorcentaje.Name = "rdPorcentaje"
        Me.rdPorcentaje.Size = New System.Drawing.Size(46, 24)
        Me.rdPorcentaje.TabIndex = 11
        Me.rdPorcentaje.TabStop = True
        Me.rdPorcentaje.Text = "%"
        Me.rdPorcentaje.UseVisualStyleBackColor = False
        '
        'chkDescuento
        '
        Me.chkDescuento.AutoSize = True
        Me.chkDescuento.BackColor = System.Drawing.Color.Transparent
        Me.chkDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDescuento.ForeColor = System.Drawing.Color.Blue
        Me.chkDescuento.Location = New System.Drawing.Point(810, 588)
        Me.chkDescuento.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDescuento.Name = "chkDescuento"
        Me.chkDescuento.Size = New System.Drawing.Size(180, 24)
        Me.chkDescuento.TabIndex = 10
        Me.chkDescuento.Text = "Habilitar Descuento"
        Me.chkDescuento.UseVisualStyleBackColor = False
        '
        'txtSubtotalVenta
        '
        Me.txtSubtotalVenta.AccessibleName = ""
        Me.txtSubtotalVenta.Decimals = CType(2, Byte)
        Me.txtSubtotalVenta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotalVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotalVenta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotalVenta.Location = New System.Drawing.Point(1018, 611)
        Me.txtSubtotalVenta.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSubtotalVenta.MaxLength = 100
        Me.txtSubtotalVenta.Name = "txtSubtotalVenta"
        Me.txtSubtotalVenta.ReadOnly = True
        Me.txtSubtotalVenta.Size = New System.Drawing.Size(97, 26)
        Me.txtSubtotalVenta.TabIndex = 240
        Me.txtSubtotalVenta.Text_1 = Nothing
        Me.txtSubtotalVenta.Text_2 = Nothing
        Me.txtSubtotalVenta.Text_3 = Nothing
        Me.txtSubtotalVenta.Text_4 = Nothing
        Me.txtSubtotalVenta.UserValues = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(1014, 588)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 20)
        Me.Label7.TabIndex = 241
        Me.Label7.Text = "Subtotal"
        '
        'txtIdProducto
        '
        Me.txtIdProducto.AccessibleName = ""
        Me.txtIdProducto.Decimals = CType(2, Byte)
        Me.txtIdProducto.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdProducto.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtIdProducto.Location = New System.Drawing.Point(176, 140)
        Me.txtIdProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdProducto.MaxLength = 100
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.Size = New System.Drawing.Size(30, 22)
        Me.txtIdProducto.TabIndex = 239
        Me.txtIdProducto.Text_1 = Nothing
        Me.txtIdProducto.Text_2 = Nothing
        Me.txtIdProducto.Text_3 = Nothing
        Me.txtIdProducto.Text_4 = Nothing
        Me.txtIdProducto.UserValues = Nothing
        Me.txtIdProducto.Visible = False
        '
        'txtSubtotal
        '
        Me.txtSubtotal.AccessibleName = ""
        Me.txtSubtotal.Decimals = CType(2, Byte)
        Me.txtSubtotal.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtSubtotal.Enabled = False
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtSubtotal.Location = New System.Drawing.Point(1089, 168)
        Me.txtSubtotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSubtotal.MaxLength = 100
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(79, 26)
        Me.txtSubtotal.TabIndex = 9
        Me.txtSubtotal.Text_1 = Nothing
        Me.txtSubtotal.Text_2 = Nothing
        Me.txtSubtotal.Text_3 = Nothing
        Me.txtSubtotal.Text_4 = Nothing
        Me.txtSubtotal.UserValues = Nothing
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(1088, 146)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 20)
        Me.Label5.TabIndex = 238
        Me.Label5.Text = "Subtotal"
        '
        'txtPrecioVta
        '
        Me.txtPrecioVta.AccessibleName = ""
        Me.txtPrecioVta.Decimals = CType(2, Byte)
        Me.txtPrecioVta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPrecioVta.Enabled = False
        Me.txtPrecioVta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecioVta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPrecioVta.Location = New System.Drawing.Point(810, 168)
        Me.txtPrecioVta.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPrecioVta.MaxLength = 100
        Me.txtPrecioVta.Name = "txtPrecioVta"
        Me.txtPrecioVta.ReadOnly = True
        Me.txtPrecioVta.Size = New System.Drawing.Size(79, 26)
        Me.txtPrecioVta.TabIndex = 6
        Me.txtPrecioVta.Text_1 = Nothing
        Me.txtPrecioVta.Text_2 = Nothing
        Me.txtPrecioVta.Text_3 = Nothing
        Me.txtPrecioVta.Text_4 = Nothing
        Me.txtPrecioVta.UserValues = Nothing
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(806, 147)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 20)
        Me.Label12.TabIndex = 236
        Me.Label12.Text = "Precio"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AccessibleName = ""
        Me.txtObservaciones.Decimals = CType(2, Byte)
        Me.txtObservaciones.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtObservaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservaciones.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtObservaciones.Location = New System.Drawing.Point(21, 92)
        Me.txtObservaciones.Margin = New System.Windows.Forms.Padding(4)
        Me.txtObservaciones.MaxLength = 300
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(951, 26)
        Me.txtObservaciones.TabIndex = 3
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
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(17, 69)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 20)
        Me.Label1.TabIndex = 214
        Me.Label1.Text = "Observaciones"
        '
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.grdItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Id, Me.CodMaterial, Me.EquipoHerramienta, Me.Cantidad, Me.Peso, Me.PrecioUni, Me.Subtotal, Me.Desc_Unit, Me.Desc_Total, Me.SubtotalFinal, Me.IdProducto, Me.IdUnidad, Me.Eliminar})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdItems.DefaultCellStyle = DataGridViewCellStyle9
        Me.grdItems.Location = New System.Drawing.Point(21, 202)
        Me.grdItems.Margin = New System.Windows.Forms.Padding(4)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(1325, 365)
        Me.grdItems.TabIndex = 9
        '
        'Id
        '
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        Me.Id.Width = 30
        '
        'CodMaterial
        '
        Me.CodMaterial.HeaderText = "Código"
        Me.CodMaterial.Name = "CodMaterial"
        Me.CodMaterial.Width = 70
        '
        'EquipoHerramienta
        '
        Me.EquipoHerramienta.HeaderText = "Producto"
        Me.EquipoHerramienta.Name = "EquipoHerramienta"
        Me.EquipoHerramienta.ReadOnly = True
        Me.EquipoHerramienta.Width = 300
        '
        'Cantidad
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cantidad.DefaultCellStyle = DataGridViewCellStyle3
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 80
        '
        'Peso
        '
        Me.Peso.HeaderText = "Peso"
        Me.Peso.Name = "Peso"
        Me.Peso.Width = 60
        '
        'PrecioUni
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrecioUni.DefaultCellStyle = DataGridViewCellStyle4
        Me.PrecioUni.HeaderText = "PrecioUni"
        Me.PrecioUni.Name = "PrecioUni"
        Me.PrecioUni.ReadOnly = True
        Me.PrecioUni.Width = 90
        '
        'Subtotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Subtotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.Subtotal.HeaderText = "Subtotal"
        Me.Subtotal.Name = "Subtotal"
        Me.Subtotal.ReadOnly = True
        Me.Subtotal.Width = 90
        '
        'Desc_Unit
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.Desc_Unit.DefaultCellStyle = DataGridViewCellStyle6
        Me.Desc_Unit.HeaderText = "Desc. x Unidad"
        Me.Desc_Unit.Name = "Desc_Unit"
        '
        'Desc_Total
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Desc_Total.DefaultCellStyle = DataGridViewCellStyle7
        Me.Desc_Total.HeaderText = "Total Descuento"
        Me.Desc_Total.Name = "Desc_Total"
        Me.Desc_Total.ReadOnly = True
        '
        'SubtotalFinal
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.SubtotalFinal.DefaultCellStyle = DataGridViewCellStyle8
        Me.SubtotalFinal.HeaderText = "Subtotal Final"
        Me.SubtotalFinal.Name = "SubtotalFinal"
        Me.SubtotalFinal.ReadOnly = True
        '
        'IdProducto
        '
        Me.IdProducto.HeaderText = "IdProducto"
        Me.IdProducto.Name = "IdProducto"
        Me.IdProducto.Visible = False
        Me.IdProducto.Width = 80
        '
        'IdUnidad
        '
        Me.IdUnidad.HeaderText = "IdUnidad"
        Me.IdUnidad.Name = "IdUnidad"
        Me.IdUnidad.Visible = False
        Me.IdUnidad.Width = 80
        '
        'Eliminar
        '
        Me.Eliminar.HeaderText = "Eliminar"
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.ToolTipText = "Eliminar Registro"
        Me.Eliminar.UseColumnTextForButtonValue = True
        Me.Eliminar.Width = 80
        '
        'chkDevuelto
        '
        Me.chkDevuelto.AutoSize = True
        Me.chkDevuelto.Location = New System.Drawing.Point(1599, 39)
        Me.chkDevuelto.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDevuelto.Name = "chkDevuelto"
        Me.chkDevuelto.Size = New System.Drawing.Size(100, 21)
        Me.chkDevuelto.TabIndex = 211
        Me.chkDevuelto.Text = "CheckBox1"
        Me.chkDevuelto.UseVisualStyleBackColor = True
        Me.chkDevuelto.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(653, 40)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 20)
        Me.Label10.TabIndex = 152
        Me.Label10.Text = "Vendedor"
        '
        'txtCantidad
        '
        Me.txtCantidad.AccessibleName = ""
        Me.txtCantidad.Decimals = CType(2, Byte)
        Me.txtCantidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtCantidad.Location = New System.Drawing.Point(996, 168)
        Me.txtCantidad.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantidad.MaxLength = 100
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(79, 26)
        Me.txtCantidad.TabIndex = 8
        Me.txtCantidad.Text_1 = Nothing
        Me.txtCantidad.Text_2 = Nothing
        Me.txtCantidad.Text_3 = Nothing
        Me.txtCantidad.Text_4 = Nothing
        Me.txtCantidad.UserValues = Nothing
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(989, 147)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 20)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Cantidad"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(300, 15)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 20)
        Me.Label6.TabIndex = 146
        Me.Label6.Text = "Cliente*"
        '
        'cmbProducto
        '
        Me.cmbProducto.AccessibleName = ""
        Me.cmbProducto.DropDownHeight = 450
        Me.cmbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.FormattingEnabled = True
        Me.cmbProducto.IntegralHeight = False
        Me.cmbProducto.Location = New System.Drawing.Point(96, 166)
        Me.cmbProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(612, 28)
        Me.cmbProducto.TabIndex = 5
        '
        'cmbCliente
        '
        Me.cmbCliente.AccessibleName = "*Cliente"
        Me.cmbCliente.DropDownHeight = 450
        Me.cmbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCliente.FormattingEnabled = True
        Me.cmbCliente.IntegralHeight = False
        Me.cmbCliente.Location = New System.Drawing.Point(304, 37)
        Me.cmbCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbCliente.Name = "cmbCliente"
        Me.cmbCliente.Size = New System.Drawing.Size(329, 28)
        Me.cmbCliente.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(92, 142)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 20)
        Me.Label4.TabIndex = 139
        Me.Label4.Text = "Producto"
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(119, 15)
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
        'txtCODIGO
        '
        Me.txtCODIGO.AccessibleName = "CODIGO"
        Me.txtCODIGO.BackColor = System.Drawing.SystemColors.Window
        Me.txtCODIGO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCODIGO.Decimals = CType(2, Byte)
        Me.txtCODIGO.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtCODIGO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCODIGO.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtCODIGO.Location = New System.Drawing.Point(21, 38)
        Me.txtCODIGO.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCODIGO.MaxLength = 25
        Me.txtCODIGO.Name = "txtCODIGO"
        Me.txtCODIGO.ReadOnly = True
        Me.txtCODIGO.Size = New System.Drawing.Size(129, 26)
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
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(17, 15)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 131
        Me.Label2.Text = "Nro Mov"
        '
        'dtpFECHA
        '
        Me.dtpFECHA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFECHA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFECHA.Location = New System.Drawing.Point(160, 38)
        Me.dtpFECHA.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFECHA.MaxDate = New Date(3000, 12, 31, 0, 0, 0, 0)
        Me.dtpFECHA.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtpFECHA.Name = "dtpFECHA"
        Me.dtpFECHA.Size = New System.Drawing.Size(135, 26)
        Me.dtpFECHA.TabIndex = 1
        Me.dtpFECHA.Tag = "202"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(156, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 20)
        Me.Label3.TabIndex = 132
        Me.Label3.Text = "Fecha"
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'frmVentasMostrador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1391, 913)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmVentasMostrador"
        Me.Text = "Ventas"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkDevuelto As System.Windows.Forms.CheckBox
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCliente As System.Windows.Forms.ComboBox
    Friend WithEvents txtIdProducto As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtSubtotal As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPrecioVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotalVenta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkDescuento As System.Windows.Forms.CheckBox
    Friend WithEvents txtTotalVenta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents rdAbsoluto As System.Windows.Forms.RadioButton
    Friend WithEvents rdPorcentaje As System.Windows.Forms.RadioButton
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkEliminado As System.Windows.Forms.CheckBox
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents lblVendedor As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblContado As System.Windows.Forms.Label
    Friend WithEvents lblCtaCte As System.Windows.Forms.Label
    Friend WithEvents lblFacturado As System.Windows.Forms.Label
    Friend WithEvents lblTipoOperacion As System.Windows.Forms.Label
    Friend WithEvents chkPrecioMayorista As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransInterna As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPeso As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoBarra As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents chkPagoConEfec As System.Windows.Forms.CheckBox
    Friend WithEvents txtIDPrecioLista As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CodMaterial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EquipoHerramienta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Peso As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecioUni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subtotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Desc_Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Desc_Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SubtotalFinal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Eliminar As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents txtLectura As TextBoxConFormatoVB.FormattedTextBoxVB

End Class
