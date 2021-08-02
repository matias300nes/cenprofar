<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRemitosSinPresupuesto
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbClientes = New System.Windows.Forms.ComboBox()
        Me.txtQty = New System.Windows.Forms.NumericUpDown()
        Me.btnAceptar = New DevComponents.DotNetBar.ButtonX()
        Me.btnEliminarProducto = New DevComponents.DotNetBar.ButtonX()
        Me.btnModificar = New DevComponents.DotNetBar.ButtonX()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtIdUnidad = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtPrecioVta = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtIdCliente = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtidmaterial = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.txtMaterial = New System.Windows.Forms.TextBox()
        Me.grdItemsOT = New System.Windows.Forms.DataGridView()
        Me.rdNombre = New System.Windows.Forms.RadioButton()
        Me.rdCodigo = New System.Windows.Forms.RadioButton()
        Me.rdCodBarra = New System.Windows.Forms.RadioButton()
        Me.txtBusqNombreMaterial = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.txtID = New TextBoxConFormatoVB.FormattedTextBoxVB()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Controls.Add(Me.ButtonX1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbClientes)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.btnAceptar)
        Me.GroupBox1.Controls.Add(Me.btnEliminarProducto)
        Me.GroupBox1.Controls.Add(Me.btnModificar)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtIdUnidad)
        Me.GroupBox1.Controls.Add(Me.txtPrecioVta)
        Me.GroupBox1.Controls.Add(Me.txtIdCliente)
        Me.GroupBox1.Controls.Add(Me.txtidmaterial)
        Me.GroupBox1.Controls.Add(Me.txtMaterial)
        Me.GroupBox1.Controls.Add(Me.grdItemsOT)
        Me.GroupBox1.Controls.Add(Me.rdNombre)
        Me.GroupBox1.Controls.Add(Me.rdCodigo)
        Me.GroupBox1.Controls.Add(Me.rdCodBarra)
        Me.GroupBox1.Controls.Add(Me.txtBusqNombreMaterial)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdItems)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(922, 444)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(3, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 283
        Me.Label1.Text = "Clientes"
        '
        'cmbClientes
        '
        Me.cmbClientes.AccessibleName = ""
        Me.cmbClientes.DropDownHeight = 500
        Me.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClientes.FormattingEnabled = True
        Me.cmbClientes.IntegralHeight = False
        Me.cmbClientes.Location = New System.Drawing.Point(6, 22)
        Me.cmbClientes.Name = "cmbClientes"
        Me.cmbClientes.Size = New System.Drawing.Size(338, 24)
        Me.cmbClientes.TabIndex = 282
        '
        'txtQty
        '
        Me.txtQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(610, 175)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(56, 29)
        Me.txtQty.TabIndex = 281
        '
        'btnAceptar
        '
        Me.btnAceptar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAceptar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAceptar.Enabled = False
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Location = New System.Drawing.Point(672, 179)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAceptar.TabIndex = 280
        Me.btnAceptar.Text = "Guardar"
        '
        'btnEliminarProducto
        '
        Me.btnEliminarProducto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEliminarProducto.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEliminarProducto.Enabled = False
        Me.btnEliminarProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarProducto.Location = New System.Drawing.Point(834, 179)
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
        Me.btnModificar.Location = New System.Drawing.Point(753, 179)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(75, 23)
        Me.btnModificar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnModificar.TabIndex = 8
        Me.btnModificar.Text = "Modificar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(610, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 275
        Me.Label2.Text = "Cant"
        '
        'txtIdUnidad
        '
        Me.txtIdUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdUnidad.Decimals = CType(2, Byte)
        Me.txtIdUnidad.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtIdUnidad.Enabled = False
        Me.txtIdUnidad.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtIdUnidad.Location = New System.Drawing.Point(593, 13)
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
        Me.txtPrecioVta.Location = New System.Drawing.Point(689, 13)
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
        Me.txtIdCliente.Location = New System.Drawing.Point(806, 182)
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
        Me.txtidmaterial.Location = New System.Drawing.Point(637, 6)
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
        Me.txtMaterial.Location = New System.Drawing.Point(296, 161)
        Me.txtMaterial.Multiline = True
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.ReadOnly = True
        Me.txtMaterial.Size = New System.Drawing.Size(308, 43)
        Me.txtMaterial.TabIndex = 5
        '
        'grdItemsOT
        '
        Me.grdItemsOT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemsOT.Location = New System.Drawing.Point(6, 68)
        Me.grdItemsOT.Name = "grdItemsOT"
        Me.grdItemsOT.Size = New System.Drawing.Size(660, 85)
        Me.grdItemsOT.TabIndex = 0
        '
        'rdNombre
        '
        Me.rdNombre.AutoSize = True
        Me.rdNombre.BackColor = System.Drawing.Color.Transparent
        Me.rdNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdNombre.ForeColor = System.Drawing.Color.Blue
        Me.rdNombre.Location = New System.Drawing.Point(209, 159)
        Me.rdNombre.Name = "rdNombre"
        Me.rdNombre.Size = New System.Drawing.Size(81, 20)
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
        Me.rdCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdCodigo.ForeColor = System.Drawing.Color.Blue
        Me.rdCodigo.Location = New System.Drawing.Point(3, 159)
        Me.rdCodigo.Name = "rdCodigo"
        Me.rdCodigo.Size = New System.Drawing.Size(76, 20)
        Me.rdCodigo.TabIndex = 1
        Me.rdCodigo.TabStop = True
        Me.rdCodigo.Text = "Código"
        Me.rdCodigo.UseVisualStyleBackColor = False
        '
        'rdCodBarra
        '
        Me.rdCodBarra.AutoSize = True
        Me.rdCodBarra.BackColor = System.Drawing.Color.Transparent
        Me.rdCodBarra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdCodBarra.ForeColor = System.Drawing.Color.Blue
        Me.rdCodBarra.Location = New System.Drawing.Point(85, 159)
        Me.rdCodBarra.Name = "rdCodBarra"
        Me.rdCodBarra.Size = New System.Drawing.Size(118, 20)
        Me.rdCodBarra.TabIndex = 2
        Me.rdCodBarra.TabStop = True
        Me.rdCodBarra.Text = "Código Barra"
        Me.rdCodBarra.UseVisualStyleBackColor = False
        '
        'txtBusqNombreMaterial
        '
        Me.txtBusqNombreMaterial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBusqNombreMaterial.Location = New System.Drawing.Point(3, 182)
        Me.txtBusqNombreMaterial.Name = "txtBusqNombreMaterial"
        Me.txtBusqNombreMaterial.Size = New System.Drawing.Size(287, 22)
        Me.txtBusqNombreMaterial.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(3, 207)
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
        Me.Label4.Location = New System.Drawing.Point(3, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(183, 13)
        Me.Label4.TabIndex = 221
        Me.Label4.Text = "Remitos Sin Presupuestos por Cliente"
        '
        'grdItems
        '
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(3, 223)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.Size = New System.Drawing.Size(906, 209)
        Me.grdItems.TabIndex = 7
        '
        'txtID
        '
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Decimals = CType(2, Byte)
        Me.txtID.DecSeparator = Global.Microsoft.VisualBasic.ChrW(44)
        Me.txtID.Enabled = False
        Me.txtID.Format = TextBoxConFormatoVB.tbFormats.UnsignedNumber
        Me.txtID.Location = New System.Drawing.Point(569, 3)
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
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX1.Enabled = False
        Me.ButtonX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonX1.Location = New System.Drawing.Point(739, 100)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(90, 23)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 284
        Me.ButtonX1.Text = "IMPRIMIR"
        '
        'frmRemitosSinPresupuesto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 497)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmRemitosSinPresupuesto"
        Me.Text = "Remitos Sin Presupuestos"
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtBusqNombreMaterial As System.Windows.Forms.TextBox
    Friend WithEvents rdNombre As System.Windows.Forms.RadioButton
    Friend WithEvents rdCodigo As System.Windows.Forms.RadioButton
    Friend WithEvents rdCodBarra As System.Windows.Forms.RadioButton
    Friend WithEvents txtMaterial As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtidmaterial As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdCliente As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtPrecioVta As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents txtIdUnidad As TextBoxConFormatoVB.FormattedTextBoxVB
    Friend WithEvents btnEliminarProducto As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnModificar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnAceptar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents grdItemsOT As System.Windows.Forms.DataGridView
    Friend WithEvents txtQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbClientes As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX

End Class
