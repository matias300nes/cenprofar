<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrdenTrabajo_Det
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
        Me.btnEliminarProducto = New DevComponents.DotNetBar.ButtonX()
        Me.btnModificar = New DevComponents.DotNetBar.ButtonX()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPrecioVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtidmaterial = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMaterial = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtQty = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.grdItemsOT = New System.Windows.Forms.DataGridView()
        Me.rdNombre = New System.Windows.Forms.RadioButton()
        Me.rdCodigo = New System.Windows.Forms.RadioButton()
        Me.rdCodBarra = New System.Windows.Forms.RadioButton()
        Me.txtBusqNombreMaterial = New System.Windows.Forms.TextBox()
        Me.txtTitulo = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTiempoEstimado = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtNroOC = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtObservaciones = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItemsOT, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupBox1.Controls.Add(Me.btnEliminarProducto)
        Me.GroupBox1.Controls.Add(Me.btnModificar)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.txtPrecioVta)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtidmaterial)
        Me.GroupBox1.Controls.Add(Me.txtMaterial)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.grdItemsOT)
        Me.GroupBox1.Controls.Add(Me.rdNombre)
        Me.GroupBox1.Controls.Add(Me.rdCodigo)
        Me.GroupBox1.Controls.Add(Me.rdCodBarra)
        Me.GroupBox1.Controls.Add(Me.txtBusqNombreMaterial)
        Me.GroupBox1.Controls.Add(Me.txtTitulo)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtTiempoEstimado)
        Me.GroupBox1.Controls.Add(Me.txtNroOC)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtObservaciones)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(2704, 689)
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
        'btnEliminarProducto
        '
        Me.btnEliminarProducto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarProducto.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarProducto.Enabled = False
        Me.btnEliminarProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarProducto.Location = New System.Drawing.Point(1245, 84)
        Me.btnEliminarProducto.Name = "btnEliminarProducto"
        Me.btnEliminarProducto.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminarProducto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnEliminarProducto.TabIndex = 9
        Me.btnEliminarProducto.Text = "Eliminar"
        '
        'btnModificar
        '
        Me.btnModificar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnModificar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnModificar.Enabled = False
        Me.btnModificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModificar.Location = New System.Drawing.Point(1164, 84)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(75, 23)
        Me.btnModificar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnModificar.TabIndex = 8
        Me.btnModificar.Text = "Modificar"
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Enabled = False
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdUnidad.Location = New System.Drawing.Point(1014, 30)
        Me.txtIdUnidad.MaxLength = 8
        Me.txtIdUnidad.Name = "txtIdUnidad"
        Me.txtIdUnidad.Size = New System.Drawing.Size(23, 20)
        Me.txtIdUnidad.TabIndex = 279
        Me.txtIdUnidad.Text_1 = Nothing
        Me.txtIdUnidad.Text_2 = Nothing
        Me.txtIdUnidad.Text_3 = Nothing
        Me.txtIdUnidad.Text_4 = Nothing
        Me.txtIdUnidad.UserValues = Nothing
        Me.txtIdUnidad.Visible = False
        '
        'txtPrecioVta
        '
        Me.txtPrecioVta.Decimals = CType(2, Byte)
        Me.txtPrecioVta.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtPrecioVta.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtPrecioVta.Location = New System.Drawing.Point(930, 23)
        Me.txtPrecioVta.Name = "txtPrecioVta"
        Me.txtPrecioVta.Size = New System.Drawing.Size(54, 20)
        Me.txtPrecioVta.TabIndex = 278
        Me.txtPrecioVta.Text_1 = Nothing
        Me.txtPrecioVta.Text_2 = Nothing
        Me.txtPrecioVta.Text_3 = Nothing
        Me.txtPrecioVta.Text_4 = Nothing
        Me.txtPrecioVta.UserValues = Nothing
        Me.txtPrecioVta.Visible = False
        '
        'txtIdCliente
        '
        Me.txtIdCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdCliente.Decimals = CType(2, Byte)
        Me.txtIdCliente.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdCliente.Location = New System.Drawing.Point(1043, 46)
        Me.txtIdCliente.MaxLength = 8
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(23, 20)
        Me.txtIdCliente.TabIndex = 277
        Me.txtIdCliente.Text_1 = Nothing
        Me.txtIdCliente.Text_2 = Nothing
        Me.txtIdCliente.Text_3 = Nothing
        Me.txtIdCliente.Text_4 = Nothing
        Me.txtIdCliente.UserValues = Nothing
        Me.txtIdCliente.Visible = False
        '
        'txtidmaterial
        '
        Me.txtidmaterial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtidmaterial.Decimals = CType(2, Byte)
        Me.txtidmaterial.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtidmaterial.Enabled = False
        Me.txtidmaterial.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtidmaterial.Location = New System.Drawing.Point(1058, 23)
        Me.txtidmaterial.MaxLength = 8
        Me.txtidmaterial.Name = "txtidmaterial"
        Me.txtidmaterial.Size = New System.Drawing.Size(23, 20)
        Me.txtidmaterial.TabIndex = 276
        Me.txtidmaterial.Text_1 = Nothing
        Me.txtidmaterial.Text_2 = Nothing
        Me.txtidmaterial.Text_3 = Nothing
        Me.txtidmaterial.Text_4 = Nothing
        Me.txtidmaterial.UserValues = Nothing
        Me.txtidmaterial.Visible = False
        '
        'txtMaterial
        '
        Me.txtMaterial.Location = New System.Drawing.Point(682, 72)
        Me.txtMaterial.Multiline = True
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.ReadOnly = True
        Me.txtMaterial.Size = New System.Drawing.Size(416, 50)
        Me.txtMaterial.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(1101, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 275
        Me.Label2.Text = "Cant"
        '
        'txtQty
        '
        Me.txtQty.Decimals = CType(2, Byte)
        Me.txtQty.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtQty.Format = TextBoxConFormatoVB.tbFormats.SignedFloatingPointNumber
        Me.txtQty.Location = New System.Drawing.Point(1104, 87)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(54, 20)
        Me.txtQty.TabIndex = 6
        Me.txtQty.Text_1 = Nothing
        Me.txtQty.Text_2 = Nothing
        Me.txtQty.Text_3 = Nothing
        Me.txtQty.Text_4 = Nothing
        Me.txtQty.UserValues = Nothing
        '
        'grdItemsOT
        '
        Me.grdItemsOT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemsOT.Location = New System.Drawing.Point(18, 23)
        Me.grdItemsOT.Name = "grdItemsOT"
        Me.grdItemsOT.Size = New System.Drawing.Size(658, 350)
        Me.grdItemsOT.TabIndex = 0
        '
        'rdNombre
        '
        Me.rdNombre.AutoSize = True
        Me.rdNombre.BackColor = System.Drawing.Color.Transparent
        Me.rdNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdNombre.ForeColor = System.Drawing.Color.Blue
        Me.rdNombre.Location = New System.Drawing.Point(856, 23)
        Me.rdNombre.Name = "rdNombre"
        Me.rdNombre.Size = New System.Drawing.Size(68, 17)
        Me.rdNombre.TabIndex = 3
        Me.rdNombre.TabStop = True
        Me.rdNombre.Text = "Nombre"
        Me.rdNombre.UseVisualStyleBackColor = False
        '
        'rdCodigo
        '
        Me.rdCodigo.AutoSize = True
        Me.rdCodigo.BackColor = System.Drawing.Color.Transparent
        Me.rdCodigo.Checked = True
        Me.rdCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdCodigo.ForeColor = System.Drawing.Color.Blue
        Me.rdCodigo.Location = New System.Drawing.Point(682, 23)
        Me.rdCodigo.Name = "rdCodigo"
        Me.rdCodigo.Size = New System.Drawing.Size(64, 17)
        Me.rdCodigo.TabIndex = 1
        Me.rdCodigo.TabStop = True
        Me.rdCodigo.Text = "Código"
        Me.rdCodigo.UseVisualStyleBackColor = False
        '
        'rdCodBarra
        '
        Me.rdCodBarra.AutoSize = True
        Me.rdCodBarra.BackColor = System.Drawing.Color.Transparent
        Me.rdCodBarra.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdCodBarra.ForeColor = System.Drawing.Color.Blue
        Me.rdCodBarra.Location = New System.Drawing.Point(752, 23)
        Me.rdCodBarra.Name = "rdCodBarra"
        Me.rdCodBarra.Size = New System.Drawing.Size(98, 17)
        Me.rdCodBarra.TabIndex = 2
        Me.rdCodBarra.TabStop = True
        Me.rdCodBarra.Text = "Código Barra"
        Me.rdCodBarra.UseVisualStyleBackColor = False
        '
        'txtBusqNombreMaterial
        '
        Me.txtBusqNombreMaterial.Location = New System.Drawing.Point(682, 46)
        Me.txtBusqNombreMaterial.Name = "txtBusqNombreMaterial"
        Me.txtBusqNombreMaterial.Size = New System.Drawing.Size(335, 20)
        Me.txtBusqNombreMaterial.TabIndex = 4
        '
        'txtTitulo
        '
        Me.txtTitulo.AccessibleName = ""
        Me.txtTitulo.Decimals = CType(2, Byte)
        Me.txtTitulo.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTitulo.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTitulo.Location = New System.Drawing.Point(18, 393)
        Me.txtTitulo.MaxLength = 300
        Me.txtTitulo.Multiline = True
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(446, 20)
        Me.txtTitulo.TabIndex = 267
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
        Me.Label11.Location = New System.Drawing.Point(15, 376)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 13)
        Me.Label11.TabIndex = 268
        Me.Label11.Text = "Título de la OT"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(134, 542)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 13)
        Me.Label7.TabIndex = 266
        Me.Label7.Text = "Tiempo Estimado [Hs]"
        '
        'txtTiempoEstimado
        '
        Me.txtTiempoEstimado.AccessibleName = ""
        Me.txtTiempoEstimado.BackColor = System.Drawing.SystemColors.Window
        Me.txtTiempoEstimado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTiempoEstimado.Decimals = CType(2, Byte)
        Me.txtTiempoEstimado.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtTiempoEstimado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTiempoEstimado.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtTiempoEstimado.Location = New System.Drawing.Point(137, 558)
        Me.txtTiempoEstimado.MaxLength = 25
        Me.txtTiempoEstimado.Name = "txtTiempoEstimado"
        Me.txtTiempoEstimado.ReadOnly = True
        Me.txtTiempoEstimado.Size = New System.Drawing.Size(48, 20)
        Me.txtTiempoEstimado.TabIndex = 264
        Me.txtTiempoEstimado.Text_1 = Nothing
        Me.txtTiempoEstimado.Text_2 = Nothing
        Me.txtTiempoEstimado.Text_3 = Nothing
        Me.txtTiempoEstimado.Text_4 = Nothing
        Me.txtTiempoEstimado.UserValues = Nothing
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
        Me.txtNroOC.Location = New System.Drawing.Point(18, 558)
        Me.txtNroOC.MaxLength = 25
        Me.txtNroOC.Name = "txtNroOC"
        Me.txtNroOC.ReadOnly = True
        Me.txtNroOC.Size = New System.Drawing.Size(113, 20)
        Me.txtNroOC.TabIndex = 262
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
        Me.Label6.Location = New System.Drawing.Point(15, 542)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 263
        Me.Label6.Text = "Nro OC"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AccessibleName = ""
        Me.txtObservaciones.Decimals = CType(2, Byte)
        Me.txtObservaciones.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtObservaciones.Format = TextBoxConFormatoVB.tbFormats.SpacedAlphaNumeric
        Me.txtObservaciones.Location = New System.Drawing.Point(18, 432)
        Me.txtObservaciones.MaxLength = 300
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ReadOnly = True
        Me.txtObservaciones.Size = New System.Drawing.Size(661, 107)
        Me.txtObservaciones.TabIndex = 260
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
        Me.Label1.Location = New System.Drawing.Point(15, 416)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 13)
        Me.Label1.TabIndex = 261
        Me.Label1.Text = "Descripción del Trabajo"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(682, 125)
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
        Me.Label4.Location = New System.Drawing.Point(15, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 13)
        Me.Label4.TabIndex = 221
        Me.Label4.Text = "Órdenes de Trabajo Activas"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(685, 141)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(2008, 539)
        Me.grdItems.TabIndex = 7
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
        'frmOrdenTrabajo_Det
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2728, 742)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmOrdenTrabajo_Det"
        Me.Text = "frmAjustes"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItemsOT, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents grdItems As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdItemsOT As System.Windows.Forms.DataGridView
    Friend WithEvents txtObservaciones As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTiempoEstimado As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtNroOC As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTitulo As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtBusqNombreMaterial As System.Windows.Forms.TextBox
    Friend WithEvents rdNombre As System.Windows.Forms.RadioButton
    Friend WithEvents rdCodigo As System.Windows.Forms.RadioButton
    Friend WithEvents rdCodBarra As System.Windows.Forms.RadioButton
    Friend WithEvents txtMaterial As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtQty As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtidmaterial As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPrecioVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnEliminarProducto As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnModificar As DevComponents.DotNetBar.ButtonX

End Class
